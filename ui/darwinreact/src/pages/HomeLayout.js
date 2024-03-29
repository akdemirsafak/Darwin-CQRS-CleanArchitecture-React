import {Outlet} from "react-router-dom";
import Navbar from "../components/Navbar";
import Stack from "@mui/material/Stack";
export default function HomeLayout(){
    return(
      <Stack>
          <Navbar></Navbar>
          <Outlet />
      </Stack>
    )
}