/*import { useEffect } from "react";
import {Box, Typography} from '@mui/material'
import {DataGrid} from '@mui/x-data-grid'
import {useValue} from '../../../context/ContextProvider'

const Books = ({ setSelectedLink, link}) => {
    const {state:{books}} = useValue()
    useEffect(() => {
        setSelectedLink(link);
    }, []);
    return (
        <Box
        sx={{
            height:400,
            width:'100%'
        }}
        >
        <Typography
        variant = 'h3'
        component='h3'
        sx={{textAlign: 'center', mt:3, mb:3}}
        >
            Lista de Livros
        </Typography>
        <DataGrid>
        columns={}
        rows={}

        </DataGrid>

        </Box>
    )
}*/