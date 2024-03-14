import MUIDataTable from "mui-datatables";
import books from './books.json';

//colunas
const columns = [
    {
        name: "id",
        label: "ID",
        //options: {filter: true, sort: true}
    },
    {
        name: "isbn",
        label: "ISBN",
        //options: {filter: true, sort: true}
    },
    {
        name: "nome",
        label: "Nome",
        //options: {filter: true, sort: true}
    },
    {
        name: "autor",
        label: "Autor",
        //options: {filter: true, sort: true}
    },
    {
        name: "preco",
        label: "Preço",
        //options: {filter: true, sort: true}
    }
]

export const TableJson = () => {
    return (
        <MUIDataTable 
        title = {"Lista de livros"}
        data = {books}
        columns={columns}
        />
    )
}