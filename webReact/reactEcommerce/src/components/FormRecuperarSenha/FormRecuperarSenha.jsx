import {BrowserRouter as Router, Routes, Route, Link} from 'react-router-dom';
import "./FormRecuperarSenhaStyles.css"
import Swal from 'sweetalert2';

export function FormRecuperarSenha(){
    
    function emailEnviado(){
        Swal.fire(
            'E-mail enviado!',
            'Um E-mail foi enviado para redefinir sua senha!',
            'success'
          )
      }

    return(
        
        <div>
        <form className="inputContainer">
        <div >
          <label htmlFor='email'>E-mail</label>
          <input 
          type="text"
           name="email" 
           placeholder="email@email.com.br"/>
        </div>        
        </form>
        <button  className="button"
                 onClick={emailEnviado}>
          Enviar
        </button>
      </div>
        
    )
}