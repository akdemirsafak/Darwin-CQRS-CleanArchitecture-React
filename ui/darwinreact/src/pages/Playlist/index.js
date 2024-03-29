import { Outlet } from "react-router-dom";
import {Container} from "@mui/material";
export default function PlayListLayout (){

    return (

        <Container>
            <Outlet/>
        </Container>
    )
}