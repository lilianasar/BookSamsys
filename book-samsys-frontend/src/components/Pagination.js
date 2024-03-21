import React from 'react';

const MAX_ITEMS = 1;
const MAX_LEFT = (MAX_ITEMS - 1) / 2;

const Pagination = ({
  limit,
  total,
  offset,
  setOffset
}) => {
  const current = offset ? offset / limit + 1 : 1;
  const pages = Math.ceil(total / limit);
  const first = Math.max(current - MAX_LEFT, 1);

  function onPageChange(page) {
    setOffset((page - 1) * limit);
  }

  return (
    <ul className="pagination">
      <li>
        <button
          onClick={() => onPageChange(current - 1)}
          disabled={current === 1}
        >
          Anterior
        </button>
      </li>
      {Array.from({ length: Math.min(MAX_ITEMS, pages) })
        .map((_, index) => index + first)
        .map((page) => (
          <li key={page}>
            <button
              onClick={() => onPageChange(page)}
              className={
                page === current
                  ? 'pagination__item--active'
                  : null
              }
            >
              {page}
            </button>
          </li>
        ))}
      <li>
        <button
          onClick={() => onPageChange(current + 1)}
          disabled={current === pages}
        >
          Próxima
        </button>
      </li>
    </ul>
  );
};

export default Pagination;

/*import {Book} from '../pages/Book'
import React, { useState } from "react"

export function Pagination() {
  const [currentPage, setCurrentPage] = useState(1);
  const recordsPerPage = 5;
  const lastIndex = currentPage * recordsPerPage;
  const firstIndex = lastIndex - recordsPerPage;
  const records = Book.Slice(firstIndex, lastIndex);
  const npage = Math.ceil(Book.length / recordsPerPage);
  const numbers = [...Array(npage + 1).keys()].slice(1);

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

          
  function prePage(){
  
  }

  function changeCPage(id){
    
  }

  function nextPage(){
  
  }
}
*/