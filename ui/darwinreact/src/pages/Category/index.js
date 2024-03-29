import { Outlet } from "react-router-dom";
import {Container} from "@mui/material";
export default function CategoriesLayout(){

    return(
        <Container>
        <Outlet />
        </Container>
    )
}
