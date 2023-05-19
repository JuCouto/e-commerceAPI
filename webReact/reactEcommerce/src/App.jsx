import './css/css-config/colors.css'
import './css/css-config/reset.css'

import './css/global.css';
import logo from "./assets/logo.svg";

import { Login } from './pages/Login/Login';
import { Root } from './routes/Root';

export function App() {

  return (
    <div className='container'>
        <Root/>
    </div>
  )
}

