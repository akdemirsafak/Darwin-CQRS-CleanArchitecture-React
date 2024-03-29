import {Outlet} from "react-router-dom";
import Navbar from "../components/Navbar";
import {Paper} from "@mui/material";
export default function HomeLayout(){
    return(
      <Paper p={2} elevation={3}>
          
          <Navbar></Navbar>
          <Outlet />

      </Paper>
    )
}