import {Outlet} from "react-router-dom";
import {Container} from "@mui/material";
export default function MoodLayout(){

    return(

        <Container>
            <Outlet/>
        </Container>
    )
}
