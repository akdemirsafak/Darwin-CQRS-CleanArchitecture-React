import {Outlet} from "react-router-dom";
export default function MoodLayout(){

    return(
        <div className="container" >
            <Outlet/>
        </div>
    )
}
