import { FormRecuperarSenha } from "../../components/FormRecuperarSenha/FormRecuperarSenha";
import { Header } from "../../components/Header/Header";
import './RecuperarSenhaStyle.css'

export function RecuperarSenha(){
    return(
        <>
            <div className="container-recuperar-senha">
         <Header/>
               <h1 className="texto-recuperar-senha" >Olá! <br/> Digite abaixo seu <br/> E-mail  cadastrado</h1>
               <FormRecuperarSenha/>  
            </div>               
        </>
    )
}