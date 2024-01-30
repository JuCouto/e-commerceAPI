import {BrowserRouter as Router, Routes, Route, Link} from 'react-router-dom';
import React from "react";
import './FormLoginStyle.css';
import { useState } from 'react';

export function FormularioLogin(){
  const [email, setEmail] = useState();
  const [password, setPassword] = useState();

  const handleEmail = (e) => {
    setEmail(e.target.value);
  };

  const handleSubmit= (e) =>{
    e.preventDefault();
    console.log("Testando envio")
    console.log(e)
  }

   return(
    <div>
        <form onSubmit={handleSubmit}>
          
        <div className="inputContainer">
          <label htmlFor='email'>E-mail</label>
          <input 
          type="text"
           name="email" 
           placeholder="email@email.com.br"
           onChange={handleEmail}
           value={email}/>
        </div>

        <div className="inputContainer">
          <label htmlFor='password'>Password</label>
          <input 
          type="password" 
          name="password" 
          id="password"
          placeholder="******"
          onChange={(e) => setPassword(e.target.value)}
          value={password}/>
        </div>

        <p>Esqueceu a senha?
            <Link to="/recuperarSenha">Clique aqui</Link>
        </p>
        

        <button className="button">
          Entrar
        </button>

        <div className="texto-cadastro">
          <p className='texto-cadastro1'>Não é cliente?</p>
          <Link to='/cadastro'>Cadastre-se</Link>
        </div>
      </form>
      </div>
   )
}