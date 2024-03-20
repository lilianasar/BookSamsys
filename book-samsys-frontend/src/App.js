import './App.css';
//import { TableAxios } from './tables/TableAxios';
//import { TableBasic } from './tables/TableBasic';
//import { TableBasic } from './tables/TableBasic';
//import { TableJson } from './tables/TableJson';
import { Home } from './pages/Home';
import { Book } from './pages/Book';
import {BrowserRouter, Routes, Route, NavLink} from 'react-router-dom';
import { useState } from 'react';

function App() {
  const [currentPage, setCurrentPage] = useState(1);
  const recordsPerPage = 5;
  const lastIndex = currentPage * recordsPerPage;
  const firstIndex = lastIndex - recordsPerPage;
  const records = Book.Slice(firstIndex, lastIndex);
  const npage = Math.ceil(Book.length / recordsPerPage);
  const numbers = [...Array(npage + 1).keys()].slice(1)

  return (
    //<div className="App">
    //  {/*<TableBasic/>*/}
    //  {/*<TableJson/>*/}
    //  <h1 align = "center">Book Samsys</h1>
    //  <h4 align = "center">Lista de Livros</h4> 
    //    <TableAxios/>
    //</div>
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
          {/*Adicionar mais componentes*/}
        </Routes>

        <nav>
          <ul className='pagination'>
            <li className='page-item'>
              <a href='#' className='page-link'
              onClick={prePage}>Anterior</a>
            </li>
            {
              numbers.map((n, i) =>(
                <li className={`page-item ${currentPage === n ? 'active' : '' }`} key={i}>
                  <a href='#' className='page-item'
                  onClick={() => changeCPage(n)} >{n}</a>
                </li>

              ))
            }
            <li className='page-item'>
              <a href='#' className='page-link'
              onClick={nextPage}>Próxima</a>
            </li> 
            
          </ul>
        </nav>
    </div>
    </BrowserRouter>

    
  );

  function prePage(){
  
  }

  function changeCPage(id){
    
  }

  function nextPage(){
  
  }
}

export default App;
