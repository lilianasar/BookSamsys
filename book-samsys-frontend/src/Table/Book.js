import React, {useState, useEffect} from "react";
import api from "../services/api";

function Table(){
    const [books, setBooks] = useState([]);
    const [total, setTotal] = useState(0);
    const [quantity, setQuantity] = useState(5);
    const [pages, setPages] = useState([])

    useEffect(() =>{
        async function loadBooks(){
            const response = await api.get('/book?pageNumber=1&pageQuantity=5');
            setTotal(response.data.obj.totalCount);
            const totalPages = response.data.obj.totalPages;
            
            //(const totalPages = Math.ceil(total / quantity);
            console.log(response);

            const arrayPages = [];
            for(let i = 1; i <= totalPages; i++){
                arrayPages.push(i);
            }
            setPages(arrayPages);
            setBooks(response.data.obj.books);
        }
        loadBooks();
    }, [quantity, total])

    return (
    <div>
    <h3>Table</h3>
    <table>
        <thead>
            <tr>
                <th>ID</th>
                <th>ISBN</th>
                <th>Nome</th>
                <th>Autor</th>
                <th>Preço</th>
            </tr>
        </thead>
        <tbody>
            {books.map((book) =>(
                <tr key = {book.id}>
                    <td>{book.id}</td>
                    <td>{book.isbn}</td>
                    <td>{book.nome}</td>
                    <td>{book.autor}</td>
                    <td>{book.preco}</td>
                </tr>

            ))}
        </tbody>
    </table>
    <div>Total {total}</div>
    
    <div>
        <button type="button" className="btn btn-primary m-2 float-end">Anterior</button>

        {pages.map(page => (
            <button type="button" className="btn btn-light">{page}</button>
        ))}

        <button type="button" className="btn btn-primary m-2 float-end">Próximo</button>

    </div>
    
    </div>


    );
}

export default Table;