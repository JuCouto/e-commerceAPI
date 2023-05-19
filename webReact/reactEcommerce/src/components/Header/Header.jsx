import {BrowserRouter as Router, Routes, Route, Link} from 'react-router-dom';
import logo from '../../assets/logo.svg';
import './HeaderStyle.css';

export function Header(){
    return(
        <>
        <header className='header'>
          <Link to="/"><img src={logo} alt="logo" /></Link>
       </header>
       </>
    )
}