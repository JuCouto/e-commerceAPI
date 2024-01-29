import React from 'react'
import logo from '../../assets/logo.svg';
import './NavbarStyle.css'

import { NavLink, Link } from 'react-router-dom'
import {BsSearch, BsHouseDoorFill,BsFillPersonFill,
  BsFillCameraFill,} from 'react-icons/bs'
import { Avatar } from '@mui/material';
import HeaderTeste from '../headerTeste/headerTeste';



const Navbar = () => {
  return (
    <nav className="inputContainer " id="nav">
      {/* <HeaderTeste/> */}
      <Link to="/">
        <img className='imagem-logo-nav' src={logo} alt="logo" />
        </Link>

      <form id="search-form">
        <BsSearch/>
        <input type="text"
         placeholder="Pesquisar"/>
      </form>

      <ul id="nav-links">
      
      <li>
        <NavLink to="/login">
          {/* <Avatar alt="Remy Sharp" src="/src/assets/flor.jpg" /> */}
          <BsFillPersonFill/>
        </NavLink>
      </li>

      <li>
          <NavLink to="/cadastro">Cadastrar</NavLink>
      </li>
        
      </ul>
    </nav>
  )
}

export default Navbar