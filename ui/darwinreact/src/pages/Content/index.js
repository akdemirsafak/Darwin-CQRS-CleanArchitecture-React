import { Outlet } from "react-router-dom";
import {Container} from "@mui/material";
export default function ContentLayout(){
    return(
        <Container>
            <Outlet />
        </Container>
    )
}