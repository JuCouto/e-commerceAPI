import React from "react";
import './loginStyle.css';
import { FormularioLogin } from "../../components/FormularioLogin/FormularioLogin";
import { Header } from "../../components/Header/Header";
import HeaderTeste from "../../components/headerTeste/headerTeste";

export function Login(){
    return(
         <>  
         <HeaderTeste/>   
            <div className="container-login">
               <h1  className="texto-login">Olá! <br/> Seja bem-vindo(a)</h1>
               <FormularioLogin/>
            </div>
       </> 
    )
   }