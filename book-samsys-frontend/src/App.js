import './App.css';
//import { TableAxios } from './tables/TableAxios';
//import { TableBasic } from './tables/TableBasic';
//import { TableBasic } from './tables/TableBasic';
//import { TableJson } from './tables/TableJson';
//import { Home } from './pages/Home';
import { Book } from './pages/Book';
import {BrowserRouter, Routes, Route, NavLink} from 'react-router-dom';
import { useState, useEffect} from 'react';
//import Pagination from './components/Pagination';
import Table from './Table/Book';


function App() {
  return (
    <div className="App">
      {/*<TableBasic/>*/}
      {/*<TableJson/>*/}
      <h1 align = "center">Book Samsys</h1>
      <h4 align = "center">Lista de Livros</h4> 
      {/*<TableAxios/>*/}
    
   <BrowserRouter>
   <div className="App container">
        <h3 className="d-flex justify-content-center m-3">
        BookSamsys
        </h3>  

        <nav className='navbar navbar-expand-sm bg-light navbar-dark'>
          <ul className='navbar-nav'>
            <li className='nav-item- m-1'>
              {/*<NavLink className="btn btn-light btn-outline-primary" to="/home">
                Home
  </NavLink>*/}
              <NavLink className="btn btn-light btn-outline-primary" to="/book">
                BookSamsys
              </NavLink>
              {/*Adicionar mais páginas*/}
            </li>
          </ul>
        </nav>

         <Routes>
          {/*<Route path='/home' Component={Home}/>*/}
          <Route path='/' Component={Book}/>
          Adicionar mais componentes
        </Routes>

        
    </div>

    </BrowserRouter>

 
 {/*<Table/>*/}
</div>
);
}

export default App;
