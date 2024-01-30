import React from 'react'
import "./HomeStyle.css"
import { Header } from '../../components/Header/Header'
import Navbar from '../../components/Navbar/Navbar'
import Footer from '../../components/Footer/Footer'
import HeaderTeste from '../../components/headerTeste/headerTeste'

const Home = () => {
  return (
    <>
      {/* <HeaderTeste/> */}
      <Navbar/>
       <div className='container'>
      
          <h1 className='texto-home'>Página Home</h1>
       </div>
       <Footer/>
      </>
  )
}

export default Home