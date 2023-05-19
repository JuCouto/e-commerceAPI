import {BrowserRouter as Router, Routes, Route, Link} from 'react-router-dom';
import React from "react";
import './FormLoginStyle.css';

export function FormularioLogin(){
   return(
    <div>
        <form>
        <div className="inputContainer">
          <label htmlFor='email'>E-mail</label>
          <input 
          type="text"
           name="email" 
           placeholder="email@email.com.br"/>
        </div>

        <div className="inputContainer">
          <label htmlFor='password'>Password</label>
          <input 
          type="password" 
          name="password" 
          id="password"
          placeholder="******"/>
        </div>
        <p>Esqueceu a senha?
            <Link to="/recuperarSenha">Clique aqui</Link>
        </p>
        

        <button className="button">
          Entrar
        </button>

        <div className="footer">
          <p>Não é cliente?</p>
          <Link to=''>Cadastre-se</Link>
        </div>
      </form>
      </div>
   )
}