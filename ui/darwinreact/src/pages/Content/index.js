import { Outlet } from "react-router-dom";
export default function ContentLayout(){
    return(
        <div className="container">
        <Outlet />
        </div>
    )
}