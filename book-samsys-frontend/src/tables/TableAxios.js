import { useState, useEffect } from "react";
import MUIDataTable from "mui-datatables";
import axios from "axios";

//export const TableAxios = () => {
export function TableAxios(){  

//Configurar ligação
    const [books, setBooks] = useState ([]);

    //Mostrar dados com axios

    /*const endpoint = 'https://jsonplaceholder.typicode.com/posts'//'https://10.0.2.2:7063/api/book/' //   

    const getData = async () => {
        await axios.get(endpoint).then((response) => {
            const data = response.data
            console.log(data)
            setBooks(data)
        })
    }*/

    const getBooks = async() => {
        try{
            const response = await axios.get("https://localhost:7063/api/book"); //10.0.2.2
            setBooks(response.data);
        }catch(error){
            if (error.response) {
                // The request was made and the server responded with a status code
                // that falls out of the range of 2xx
                console.log(error.response.data);
                console.log(error.response.status);
                console.log(error.response.headers);
              } else if (error.request) {
                // The request was made but no response was received
                // `error.request` is an instance of XMLHttpRequest in the browser and an instance of
                // http.ClientRequest in node.js
                console.log(error.request);
              } else {
                // Something happened in setting up the request that triggered an Error
                console.log('Error', error.message);
              }
              console.log(error.config);
        }
        
    }

    //executa 1x
    useEffect(()=>{
        getBooks()
    }, [])


    //Definir as colunas
    const columns = [
        
       {
            name: "id",
            label: "ID"
        },
        {
            name: "isbn",
            label: "ISBN"
        },
        {
            name: "nome",
            label: "Nome"
        },
        {
            name: "autor",
            label: "Autor"
        },
        {
            name: "preco",
            label: "Preço"
        }
        /*{
            name: "userId",
            label: "UserID"
        },
        /*{
            name: "id",
            label: "ID"
        },
        {
            name: "title",
            label: "Titulo"
        },
        {
            name: "body",
            label: "Mensagem"
        }*/
    ]
    //Mostrar a datatable
    return(
        <MUIDataTable
        title={"Listagem de livros"}
        data={books}
        columns = {columns}
        />
    )
}