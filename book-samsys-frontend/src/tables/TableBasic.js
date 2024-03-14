import MUIDataTable from "mui-datatables";

const columns = ["ID", "ISBN", "Nome", "Autor", "Preço"]
const data = [
    [1, "não sei", "Memorial do Convento", "José Saramago", "15.6"],
    [2, "já soube", "Nada menos que tudo", "Raul Minh'alma", "16"],
    [3, "já não me lembro", "Batatinha", "Anónimo", "10"],
    [4, "nada", "Cebolinha", "Livraria", ""]
]
const options = {filterType: 'checkbox', }

export const TableBasic = () => {
    return(
        <MUIDataTable
        title={"Lista de livros"}
        data={data}
        columns={columns}
        options={options}
        
        />

    )
}