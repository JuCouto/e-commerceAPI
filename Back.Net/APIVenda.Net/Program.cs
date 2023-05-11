using APIVenda.Net;
using APIVenda.Net.Context;
using APIVenda.Net.Validations;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options => options.AddDefaultPolicy(p =>
{
    p.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
})
);

// Conexão com banco de dados.
builder.Services.AddDbContext<ApiVendaDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Configuração para o postgres reconhecer a DATA LOCAL.
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();


builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddServices(builder.Configuration);

// Configuração para formatar data.
builder.Services.AddControllers()
    .AddJsonOptions(options => { options.JsonSerializerOptions.Converters.Add(new CustomDateTimeConverter()); });

builder.Services.AddSwaggerGen(
c =>
{
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);

    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Api Venda .Net",
        Description = "API e-commerce com autorização",
       
    });
    
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = @"Autenticação em JWT. /n
                        Ex: Digite: Bearer {token}",
        Name = "Autorization",
        In = ParameterLocation.Header,
        Scheme = "Bearer"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement()
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                },
                Scheme = "oauth2",
                Name = "Bearer",
                In = ParameterLocation.Header
            },
            new List<string>()
        }
    });
});

// Remover nulos do retorno do swagger.(message, errors).
builder.Services.AddMvc().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;   
});

//Converte o Enum no json do swagger.
builder.Services.AddControllers().AddJsonOptions(opt =>
{
    opt.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});


builder.Services.AddAuthentication(authOptions =>
{
    authOptions.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    authOptions.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        ValidateLifetime = true,
         IssuerSigningKey =
        new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Settings.secret)),
        ValidateAudience = false,
        ValidateIssuer = false
    };
});

var app = builder.Build();
    
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
//app.UseStaticFiles();
//app.UseRouting();
app.UseCors();

app.MapControllers();

app.Run();
