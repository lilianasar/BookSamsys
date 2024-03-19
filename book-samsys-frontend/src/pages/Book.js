import { Modal, bottomNavigationActionClasses } from "@mui/material";
import React, { Component } from "react";
import { unstable_useViewTransitionState } from "react-router-dom";
import { variables } from "../Variables";

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
    };
  }

  refreshList() {
    fetch(variables.API_URL + "book")
      .then((response) => response.json())
      .then((data) => {
        this.setState({ books: data });
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
              <th>ID</th>
              <th>ISBN</th>
              <th>Nome</th>
              <th>Autor</th>
              <th>Preço</th>
              <th>Opções</th>
            </tr>
          </thead>
          <tbody>
            {books.map((book) => (
              <tr key={book.id}>
                <td>{book.id}</td>
                <td>{book.isbn}</td>
                <td>{book.nome}</td>
                <td>{book.autor}</td>
                <td>{book.preco}</td>
                <td>
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
