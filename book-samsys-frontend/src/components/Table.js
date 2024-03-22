import React, {useEffect, useState} from "react";
import api from '../Variables';

function Table(){
  useEffect(() => {
    async function loadBooks(){
      const response = await api.get("book");
      console.log(response.data);
    }

    loadBooks();
  }, []);

  return(
      <h3>Tabela de livros</h3>
  );
}

export default Table();