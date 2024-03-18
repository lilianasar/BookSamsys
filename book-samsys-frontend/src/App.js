import './App.css';
import { TableAxios } from './tables/TableAxios';
import { TableBasic } from './tables/TableBasic';
/*import { TableBasic } from './tables/TableBasic';*/
import { TableJson } from './tables/TableJson';

function App() {
  return (
    <div className="App">
      {/*<TableBasic/>*/}
      {/*<TableJson/>*/}
      <h1 align = "center">Book Samsys</h1>
      <h4 align = "center">Lista de Livros</h4>
       
        <TableAxios/>
      
    </div>
  );
}

export default App;
