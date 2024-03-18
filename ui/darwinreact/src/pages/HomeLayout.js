import {Outlet,NavLink} from "react-router-dom";

export default function HomeLayout(){
    return(
        <>
          <nav className='navbar container' >
        {/* <NavLink to="/" className={({isActive})=>isActive && 'aktif'} >Home</NavLink> */}
        <NavLink to="/">Home</NavLink>

        <NavLink to="/categories">Kategoriler</NavLink>
        <NavLink to="/contents">İçerikler</NavLink>
        <NavLink to="/moods">Ruh hali</NavLink>
        <NavLink to="/playlists">İçerik listeleri</NavLink>
{/*         
        <NavLink to="/login">Login</NavLink>
        <NavLink to="/register">Register</NavLink> */}
        <NavLink to="/profile">Profil</NavLink>
      </nav>
      <Outlet />
        
        </>
    )
}