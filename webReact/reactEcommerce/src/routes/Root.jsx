
import Footer from "../components/Footer/Footer";
import Navbar from "../components/Navbar/Navbar";
import { Cadastro } from "../pages/Cadastro/Cadastro";
import Home from "../pages/Home/Home";
import { Login } from "../pages/Login/Login";
import { RecuperarSenha } from "../pages/RecuperarSenha/RecuperarSenha";
import {BrowserRouter as Router, Routes, Route} from 'react-router-dom';


export function Root(){
    return(
       <Router> 
        <div >
            <Routes>
                <Route path="/login" element={<Login />} />
                <Route path="/recuperarSenha" element={<RecuperarSenha />} />
                <Route path="/cadastro" element={<Cadastro />} />
                <Route path="/" element={<Home />} />
            </Routes>
            </div>
        
       </Router>
    )
}