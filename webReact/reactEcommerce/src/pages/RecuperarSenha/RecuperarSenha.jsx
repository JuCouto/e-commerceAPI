import { FormRecuperarSenha } from "../../components/FormRecuperarSenha/FormRecuperarSenha";
import { Header } from "../../components/Header/Header";
import './RecuperarSenhaStyle.css'

export function RecuperarSenha(){
    return(
        <>
         <Header/>
            <div className="container-recuperarSenha">
               <h1  >Olá! <br/> Digite abaixo seu <br/> E-mail  cadastrado</h1>
               <FormRecuperarSenha/>  
            </div>               
        </>
    )
}