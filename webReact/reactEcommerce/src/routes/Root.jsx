import { Login } from "../pages/Login/Login";
import { RecuperarSenha } from "../pages/RecuperarSenha/RecuperarSenha";
import {BrowserRouter as Router, Routes, Route} from 'react-router-dom';


export function Root(){
    return(
       <Router> 
            <Routes>
                <Route path="/" element={<Login />} />
                <Route path="/recuperarSenha" element={<RecuperarSenha />} />
            </Routes>
       </Router>
    )
}