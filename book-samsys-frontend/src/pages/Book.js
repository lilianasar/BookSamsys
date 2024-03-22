//import { Modal, bottomNavigationActionClasses } from "@mui/material";
import React, { Component, useState, useEffect} from "react";
//import { unstable_useViewTransitionState } from "react-router-dom";
import { variables } from "../Variables";
//import { Pagination } from "../components/Pagination";

export class Book extends Component {
  constructor(props) {
    //chamar construtor da classe pai
    super(props);

    this.state = {
      books: [],
      modalTitle: "",
      id: 0,
      isbn: "",
      nome: "",
      autor: "",
      preco: 0,

      IdFilter:"",
      IsbnFilter:"",
      NomeFilter:"",
      AutorFilter:"",
      PrecoFilter: "",
      booksWithoutFilter:[]
    };
  }
  

  FilterFn(){
    var IdFilter=this.state.IdFilter;
    var IsbnFilter = this.state.IsbnFilter;
    var NomeFilter = this.state.NomeFilter;
    var AutorFilter = this.state.AutorFilter;
    var PrecoFilter = this.state.PrecoFilter;

    var filteredData=this.state.booksWithoutFilter.filter(
        function(el){
            return el.id.toString().toLowerCase().includes(
              IdFilter.toString().trim().toLowerCase()
            )&&
            el.isbn.toString().toLowerCase().includes(
              IsbnFilter.toString().trim().toLowerCase()
            )&&
            el.nome.toString().toLowerCase().includes(
              NomeFilter.toString().trim().toLowerCase()
            )&&
            el.autor.toString().toLowerCase().includes(
              AutorFilter.toString().trim().toLowerCase()
            )&&
            el.preco.toString().toLowerCase().includes(
              PrecoFilter.toString().trim().toLowerCase()
            )
        }
    );

    this.setState({books:filteredData});

}

sortResult(prop,asc){
  var sortedData=this.state.booksWithoutFilter.sort(function(a,b){
      if(asc){
          return (a[prop]>b[prop])?1:((a[prop]<b[prop])?-1:0);
      }
      else{
          return (b[prop]>a[prop])?1:((b[prop]<a[prop])?-1:0);
      }
  });

  this.setState({books:sortedData});
}

changeIdFilter = (e)=>{
  this.state.IdFilter=e.target.value;
  this.FilterFn();
}

changeIsbnFilter = (e)=>{
  this.state.IsbnFilter=e.target.value;
  this.FilterFn();
}

changeNomeFilter = (e)=>{
  this.state.NomeFilter=e.target.value;
  this.FilterFn();
}
  
changeAutorFilter = (e)=>{
  this.state.AutorFilter=e.target.value;
  this.FilterFn();
}
    
changePrecoFilter = (e)=>{
  this.state.PrecoFilter=e.target.value;
  this.FilterFn();
}     
  
  
  refreshList() {


    fetch(variables.API_URL + "book/pg?pageNumber=0&pageQuantity=5")
      .then((response) => response.json())
      .then((data) => {
        this.setState({ books: data.obj, booksWithoutFilter: data });
        
        /*const total = data.headers["totalRows"];
        const limit = data.headers["pageQuantity"];
        const totalPages = Math.ceil(total / limit)
        const arrayPages = [];
        for(let i = 0; i<= totalPages-1; i++){
          arrayPages.push(i);
        } 

        this.setState({ books: data.obj, booksWithoutFilter: data });
      */
      });
  }


  componentDidMount() {
    this.refreshList();
  }

  changeIsbn = (e) => {
    this.setState({ isbn: e.target.value });
  };

  changeNome = (e) => {
    this.setState({ nome: e.target.value });
  };

  changeAutor = (e) => {
    this.setState({ autor: e.target.value });
  };
  changePreco = (e) => {
    this.setState({ preco: e.target.value });
  };

  addClick() {
    this.setState({
      modalTitle: "Adicionar Livro",
      id: 0,
      isbn: "",
      nome: "",
      autor: "",
      preco: 0.0,
    });
  }

  editClick(book) {
    this.setState({
      modalTitle: "Editar Livro",
      id: book.id,
      isbn: book.isbn,
      nome: book.nome,
      autor: book.autor,
      preco: book.preco,
    });
  }

  createClick() {
    fetch(variables.API_URL + "book", {
      method: "POST",
      headers: {
        Accept: "application/json",
        "Content-Type": "application/json",
      },
      body: JSON.stringify({
        isbn: this.state.isbn,
        nome: this.state.nome,
        autor: this.state.autor,
        preco: this.state.preco,
      }),
    })
    .then((res) => res.json())
    .then(
      (result) => {
        alert(result);
        this.refreshList();
      },
        
        (error) => {
          alert("Ocorreu um erro ao criar o livro.");
        }
      );      
  }

  updateClick() {
    fetch(variables.API_URL + "book", {
      method: "PUT",
      headers: {
        Accept: "application/json",
        "Content-Type": "application/json",
      },
      body: JSON.stringify({
        id: this.state.id,
        isbn: this.state.isbn,
        nome: this.state.nome,
        autor: this.state.autor,
        preco: this.state.preco,
      }),
    })
      .then((res) => res.json())
      .then(
        (result) => {
          alert(result);
          this.refreshList();
        },
        (error) => {
          alert("Falhou ao atualizar o livro.");
        }
      );
  }

  deleteClick(id) {
    if (window.confirm("Deseja mesmo eliminar o livro?")) {
      fetch(variables.API_URL + "book/?id=" + id, { //?id=1'
        method: "DELETE",
        headers: {
          Accept: "application/json",
          "Content-Type": "application/json",
        },
      })
        .then((res) => res.json())
        .then(
          (result) => {
            alert(result);
            this.refreshList();
          },
          (error) => {
            alert("Falhou ao eliminar o livro.");
          })
    }
  }

  render() {
    const { books, modalTitle, id, isbn, nome, autor, preco } = this.state;

    return (
      <div>
        <button
          type="button"
          className="btn btn-primary m-2 float-end"
          data-bs-toggle="modal"
          data-bs-target="#exampleModal"
          onClick={() => this.addClick()}
        >
          Adicionar Livro
        </button>

        <table className="table table-striped">
          <thead>
          <tr>
            <th>
                <div className="d-flex flex-row">

                <input className="form-control"
                onChange={this.changeIdFilter}
                placeholder="Filtro"/>

                <button type="button" className="btn btn-light"
                onClick={()=>this.sortResult('id',true)}>
                    <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" className="bi bi-arrow-up-square-fill" viewBox="0 0 16 16">
                    <path d="M2 16a2 2 0 0 1-2-2V2a2 2 0 0 1 2-2h12a2 2 0 0 1 2 2v12a2 2 0 0 1-2 2H2zm6.5-4.5V5.707l2.146 2.147a.5.5 0 0 0 .708-.708l-3-3a.5.5 0 0 0-.708 0l-3 3a.5.5 0 1 0 .708.708L7.5 5.707V11.5a.5.5 0 0 0 1 0z"/>
                    </svg>
                </button>

                <button type="button" className="btn btn-light"
                onClick={()=>this.sortResult('id',false)}>
                    <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" className="bi bi-arrow-down-square-fill" viewBox="0 0 16 16">
                    <path d="M2 0a2 2 0 0 0-2 2v12a2 2 0 0 0 2 2h12a2 2 0 0 0 2-2V2a2 2 0 0 0-2-2H2zm6.5 4.5v5.793l2.146-2.147a.5.5 0 0 1 .708.708l-3 3a.5.5 0 0 1-.708 0l-3-3a.5.5 0 1 1 .708-.708L7.5 10.293V4.5a.5.5 0 0 1 1 0z"/>
                    </svg>
                </button>
                </div>
                ID
            </th>
            <th>
              <div className="d-flex flex-row">
              <input className="form-control"
                  onChange={this.changeIsbnFilter}
                  placeholder="Filtro"/>

                  <button type="button" className="btn btn-light"
                  onClick={()=>this.sortResult('isbn',true)}>
                      <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" className="bi bi-arrow-up-square-fill" viewBox="0 0 16 16">
                      <path d="M2 16a2 2 0 0 1-2-2V2a2 2 0 0 1 2-2h12a2 2 0 0 1 2 2v12a2 2 0 0 1-2 2H2zm6.5-4.5V5.707l2.146 2.147a.5.5 0 0 0 .708-.708l-3-3a.5.5 0 0 0-.708 0l-3 3a.5.5 0 1 0 .708.708L7.5 5.707V11.5a.5.5 0 0 0 1 0z"/>
                      </svg>
                  </button>

                  <button type="button" className="btn btn-light"
                  onClick={()=>this.sortResult('isbn',false)}>
                      <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" className="bi bi-arrow-down-square-fill" viewBox="0 0 16 16">
                      <path d="M2 0a2 2 0 0 0-2 2v12a2 2 0 0 0 2 2h12a2 2 0 0 0 2-2V2a2 2 0 0 0-2-2H2zm6.5 4.5v5.793l2.146-2.147a.5.5 0 0 1 .708.708l-3 3a.5.5 0 0 1-.708 0l-3-3a.5.5 0 1 1 .708-.708L7.5 10.293V4.5a.5.5 0 0 1 1 0z"/>
                      </svg>
                  </button>
                  </div>
                  ISBN
          
            </th>
            <th>
              <div className="d-flex flex-row">
              <input className="form-control"
                  onChange={this.changeNomeFilter}
                  placeholder="Filtro"/>

                  <button type="button" className="btn btn-light"
                  onClick={()=>this.sortResult('nome',true)}>
                      <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" className="bi bi-arrow-up-square-fill" viewBox="0 0 16 16">
                      <path d="M2 16a2 2 0 0 1-2-2V2a2 2 0 0 1 2-2h12a2 2 0 0 1 2 2v12a2 2 0 0 1-2 2H2zm6.5-4.5V5.707l2.146 2.147a.5.5 0 0 0 .708-.708l-3-3a.5.5 0 0 0-.708 0l-3 3a.5.5 0 1 0 .708.708L7.5 5.707V11.5a.5.5 0 0 0 1 0z"/>
                      </svg>
                  </button>
                  <button type="button" className="btn btn-light"
                  onClick={()=>this.sortResult('nome',false)}>
                      <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" className="bi bi-arrow-down-square-fill" viewBox="0 0 16 16">
                      <path d="M2 0a2 2 0 0 0-2 2v12a2 2 0 0 0 2 2h12a2 2 0 0 0 2-2V2a2 2 0 0 0-2-2H2zm6.5 4.5v5.793l2.146-2.147a.5.5 0 0 1 .708.708l-3 3a.5.5 0 0 1-.708 0l-3-3a.5.5 0 1 1 .708-.708L7.5 10.293V4.5a.5.5 0 0 1 1 0z"/>
                      </svg>
                  </button>
                  </div>
                  Nome
          
            </th>
            <th>
              <div className="d-flex flex-row">
              <input className="form-control"
                  onChange={this.changeAutorFilter}
                  placeholder="Filtro"/>

                  <button type="button" className="btn btn-light"
                  onClick={()=>this.sortResult('autor',true)}>
                      <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" className="bi bi-arrow-up-square-fill" viewBox="0 0 16 16">
                      <path d="M2 16a2 2 0 0 1-2-2V2a2 2 0 0 1 2-2h12a2 2 0 0 1 2 2v12a2 2 0 0 1-2 2H2zm6.5-4.5V5.707l2.146 2.147a.5.5 0 0 0 .708-.708l-3-3a.5.5 0 0 0-.708 0l-3 3a.5.5 0 1 0 .708.708L7.5 5.707V11.5a.5.5 0 0 0 1 0z"/>
                      </svg>
                  </button>
                  <button type="button" className="btn btn-light"
                  onClick={()=>this.sortResult('autor',false)}>
                      <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" className="bi bi-arrow-down-square-fill" viewBox="0 0 16 16">
                      <path d="M2 0a2 2 0 0 0-2 2v12a2 2 0 0 0 2 2h12a2 2 0 0 0 2-2V2a2 2 0 0 0-2-2H2zm6.5 4.5v5.793l2.146-2.147a.5.5 0 0 1 .708.708l-3 3a.5.5 0 0 1-.708 0l-3-3a.5.5 0 1 1 .708-.708L7.5 10.293V4.5a.5.5 0 0 1 1 0z"/>
                      </svg>
                  </button>

                  </div>
                  Autor
          
            </th>
            <th>
              <div className="d-flex flex-row">
              <input className="form-control"
                  onChange={this.changePrecoFilter}
                  placeholder="Filtro"/>

                  <button type="button" className="btn btn-light"
                  onClick={()=>this.sortResult('preco',true)}>
                      <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" className="bi bi-arrow-up-square-fill" viewBox="0 0 16 16">
                      <path d="M2 16a2 2 0 0 1-2-2V2a2 2 0 0 1 2-2h12a2 2 0 0 1 2 2v12a2 2 0 0 1-2 2H2zm6.5-4.5V5.707l2.146 2.147a.5.5 0 0 0 .708-.708l-3-3a.5.5 0 0 0-.708 0l-3 3a.5.5 0 1 0 .708.708L7.5 5.707V11.5a.5.5 0 0 0 1 0z"/>
                      </svg>
                  </button>
                  <button type="button" className="btn btn-light"
                  onClick={()=>this.sortResult('preco',false)}>
                      <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" className="bi bi-arrow-down-square-fill" viewBox="0 0 16 16">
                      <path d="M2 0a2 2 0 0 0-2 2v12a2 2 0 0 0 2 2h12a2 2 0 0 0 2-2V2a2 2 0 0 0-2-2H2zm6.5 4.5v5.793l2.146-2.147a.5.5 0 0 1 .708.708l-3 3a.5.5 0 0 1-.708 0l-3-3a.5.5 0 1 1 .708-.708L7.5 10.293V4.5a.5.5 0 0 1 1 0z"/>
                      </svg>
                  </button>

                  </div>
                  Preço
            </th>
            <th>
                Options
            </th>
        </tr>            
       {/* <tr>
              <th>ID</th>
              <th>ISBN</th>
              <th>Nome</th>
              <th>Autor</th>
              <th>Preço</th>
              <th>Opções</th>
            </tr>*/}
          </thead>
          <tbody>
            {books.map((book) => (
              <tr key={book.id}>
                <td width="15%">{book.id}</td>
                <td>{book.isbn}</td>
                <td>{book.nome}</td>
                <td>{book.autor}</td>
                <td width="15%">{book.preco}</td>
                <td width="10%">
                  <button
                    type="button"
                    className="btn btn-light mr-1"
                    data-bs-toggle="modal"
                    data-bs-target="#exampleModal"
                    onClick={() => this.editClick(book)}
                  >
                    <svg
                      xmlns="http://www.w3.org/2000/svg"
                      width="16"
                      height="16"
                      fill="currentColor"
                      className="bi bi-pencil-square"
                      viewBox="0 0 16 16"
                    >
                      <path d="M15.502 1.94a.5.5 0 0 1 0 .706L14.459 3.69l-2-2L13.502.646a.5.5 0 0 1 .707 0l1.293 1.293zm-1.75 2.456-2-2L4.939 9.21a.5.5 0 0 0-.121.196l-.805 2.414a.25.25 0 0 0 .316.316l2.414-.805a.5.5 0 0 0 .196-.12l6.813-6.814z" />
                      <path
                        fillRule="evenodd"
                        d="M1 13.5A1.5 1.5 0 0 0 2.5 15h11a1.5 1.5 0 0 0 1.5-1.5v-6a.5.5 0 0 0-1 0v6a.5.5 0 0 1-.5.5h-11a.5.5 0 0 1-.5-.5v-11a.5.5 0 0 1 .5-.5H9a.5.5 0 0 0 0-1H2.5A1.5 1.5 0 0 0 1 2.5z"
                      />
                    </svg>
                  </button>
                  <button type="button" className="btn btn-light mr-1"
                  onClick={()=>this.deleteClick(book.id)}>
                    <svg
                      xmlns="http://www.w3.org/2000/svg"
                      width="16"
                      height="16"
                      fill="currentColor"
                      className="bi bi-trash-fill"
                      viewBox="0 0 16 16"
                    >
                      <path d="M2.5 1a1 1 0 0 0-1 1v1a1 1 0 0 0 1 1H3v9a2 2 0 0 0 2 2h6a2 2 0 0 0 2-2V4h.5a1 1 0 0 0 1-1V2a1 1 0 0 0-1-1H10a1 1 0 0 0-1-1H7a1 1 0 0 0-1 1zm3 4a.5.5 0 0 1 .5.5v7a.5.5 0 0 1-1 0v-7a.5.5 0 0 1 .5-.5M8 5a.5.5 0 0 1 .5.5v7a.5.5 0 0 1-1 0v-7A.5.5 0 0 1 8 5m3 .5v7a.5.5 0 0 1-1 0v-7a.5.5 0 0 1 1 0" />
                    </svg>
                  </button>
                </td>
              </tr>
            ))}
          </tbody>
        </table>

      {/*  <ul className="pagination">
      <li>
        <button
          onClick={() => onPageChange(Pagination.current - 1)}
          disabled={current === 1}
          >
          Anterior
        </button>
      </li>
  
{/* 
        <ul className="pagination">
      <li>
        <button
          onClick={() => Pagination.onPageChange(Pagination.current - 1)}
          disabled={Pagination.current === 1}
        >
          Anterior
        </button>
      </li>
      {Array.from({ length: Math.min(MAX_ITEMS, Pagination.pages) })
        .map((_, index) => index + Pagination.first)
        .map((page) => (
          <li key={page}>
            <button
              onClick={() => Pagination.onPageChange(page)}
              className={
                page === Pagination.current
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
          onClick={() => Pagination.onPageChange(Pagination.current + 1)}
          disabled={Pagination.current === Pagination.pages}
        >
          Próxima
        </button>
      </li>
            </ul>*/}
        <nav>
          <ul className='pagination'>
            <li className='page-item'>
              <a href='#' className='page-link'
              //onClick={}
              >Anterior</a>
            </li>
            <li className='page-item'>
             1
            </li>
            {
              /*this.refreshList.setPages.map((n, i) =>(
                <li className='page-item' key={i}>
                  <a href='#' className='page-item'
                 //onClick={} 
                 >{n}</a>
                </li>

              ))*/
            }
            <li className='page-item'>
              <a href='#' className='page-link'
              //onClick={}
              >Próxima</a>
            </li> 
            
          </ul>
          </nav>


        <div
          className="modal fade"
          id="exampleModal"
          tabIndex="-1"
          aria-hidden="true"
        >
          <div className="modal-dialog modal-lg modal-dialog-centered">
            <div className="modal-content">
              <div className="modal-header">
                <h5 className="modal-title">{modalTitle}</h5>
                <button
                  type="button"
                  className="btn-close"
                  data-bs-dismiss="modal"
                  aria-label="Fechar"
                ></button>
              </div>
              <div className="modal-body">
                {/*<div className="d-flex flex-row bd-highlight mb-3">*/}
                  {/* <div className="p-1 w-50 bd-highlight">*/}
                    <div className="input-group mb-3">
                      <span className="input-group-text">ISBN</span>
                      <input
                        type="text"
                        className="form-control"
                        value={isbn}
                        onChange={this.changeIsbn}
                      ></input>
                    </div>
                    <div className="input-group mb-3">
                      <span className="input-group-text">Nome</span>
                      <input
                        type="text"
                        className="form-control"
                        value={nome}
                        onChange={this.changeNome}
                      ></input>
                    </div>
                    <div className="input-group mb-3">
                      <span className="input-group-text">Autor</span>
                      <input
                        type="text"
                        className="form-control"
                        value={autor}
                        onChange={this.changeAutor}
                      ></input>
                    </div>
                    <div className="input-group mb-3">
                      <span className="input-group-text">Preço</span>
                      <input
                        type="text"
                        className="form-control"
                        value={preco}
                        onChange={this.changePreco}
                      ></input>
                    </div>

                  {id == 0 ? (
                    <button
                      type="button"
                      className="btn btn-primary float-start"
                      onClick={() => this.createClick()}
                    >
                      Criar
                    </button>
                  ) : null}
                  {id != 0 ? (
                    <button
                      type="button"
                      className="btn btn-primary float-start"
                      onClick={() => this.updateClick()}
                    >
                      Atualizar
                    </button>
                  ) : null}
                </div>
              
            </div>
          </div>
        </div>
      </div>

      
    );
  }
  
}
