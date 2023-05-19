import React from "react";
import './loginStyle.css';
import { FormularioLogin } from "../../components/FormularioLogin/FormularioLogin";
import { Header } from "../../components/Header/Header";

export function Login(){
    return(
         <>     
         <Header/>
            <div className="container-login">
               <h1  >Olá! <br/> Seja bem-vindo(a)</h1>
               <FormularioLogin/>
            </div>
       </> 
    )
   }