import { FormCadastro } from "../../components/FormCadastro/FormCadastro";
import { Header } from "../../components/Header/Header";
import {BrowserRouter as Router, Routes, Route, Link} from 'react-router-dom';
import React from "react";


import './CadastroStyles.css'



export function Cadastro (){
   
    return(
        <>
            <Header/>         
            <div>
            <h1 className="texto-cadastro"> Cadastre-se!</h1>
                <FormCadastro/>
            </div>
            
        </>
    )
}
