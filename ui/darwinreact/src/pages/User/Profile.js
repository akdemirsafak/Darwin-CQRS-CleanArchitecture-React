import { Button } from "@mui/material";
import { useAuth } from "../../contexts/AuthContext";
import { Link } from "react-router-dom";


export default function Profile() {
    
    const {setUser,user }=useAuth()
    const logoutHandle=()=>{
        localStorage.removeItem("token");
        setUser(false)
    }
    
    return (
       <>
        <h1> Kullanıcının profil sayfası</h1>
        {!user && <Link to='/auth/login'>Giriş yap</Link>}
        { user && <Button onClick={logoutHandle}> Çıkış yap</Button>}
       </>
    );
}
