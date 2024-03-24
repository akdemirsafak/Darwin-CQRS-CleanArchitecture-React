import { useAuth } from "../contexts/AuthContext";
import { Navigate, useLocation } from "react-router-dom";
export default function PrivateRoute({children}) {
//Kullanıcı oturum açmış mı?
//Eğer oturum açmışsa yönlendirme işlemi yap
//eğer oturum açmışsa children'ı return et.


    const { user } = useAuth();
    const location = useLocation();
    if(!user){
        //Oturum açmadıysa giriş sayfasına yönlendir.
     return <Navigate to="/auth/login"
     replace={true} //kullanıcı geri geldiğinde 
     state={{
        return_url:location.pathname+location.search
    }}/> //navigate kullanırken state ile return_url gibi datalar yollayabiliriz.
    }


    return children;

}