import {BrowserRouter as Router, Routes, Route, Link} from 'react-router-dom';
import logo from '../../assets/logo.svg';
import './HeaderStyle.css';

export function Header(){
    return(
        <>
        <header className='container-header'>
          <Link to="/"><img className='imagem-logo' src={logo} alt="logo" /></Link>
       </header>
       </>
    )
}