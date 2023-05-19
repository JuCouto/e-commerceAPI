import {BrowserRouter as Router, Routes, Route, Link} from 'react-router-dom';

export function FormRecuperarSenha(){
    
    function emailEnviado(){
        alert('E-mail enviado');
      }

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

        <button className="button"
        onClick={emailEnviado()}
        >
          Enviar
        </button>

        
      </form>
      </div>
        
    )
}