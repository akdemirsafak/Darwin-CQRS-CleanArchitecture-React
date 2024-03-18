import './App.css';
import { Routes, Route,Link, NavLink } from 'react-router-dom';

import Home  from './pages/Home'
import Signin from './pages/Signin';
import Register from './pages/Register';


import Moods from './pages/Mood/Moods';
import MoodLayout from './pages/Mood/index';
import CreateMood from './pages/Mood/CreateMood';

import Contents from './pages/Content/Contents';
import ContentLayout from './pages/Content/index';
import ContentDetail from './pages/Content/ContentDetail';
import CreateContent from './pages/Content/CreateContent';

import PlayLists from './pages/Playlist/PlayLists';
import PlayListLayout from './pages/Playlist/index';
import CreatePlayList from './pages/Playlist/CreatePlayList';
import PlayListDetail from './pages/Playlist/PlayListDetail';

import CategoryLayout from './pages/Category/index';
import Categories from './pages/Category/Categories';
import CategoryDetail from './pages/Category/CategoryDetail';
import CreateCategory from './pages/Category/CreateCategory';

import Page404 from './pages/Page404';
import Category404 from './pages/Category/404';


function App() {
  return (
    <div className="App">
      <nav className='navbar container' >
        {/* <NavLink to="/" className={({isActive})=>isActive && 'aktif'} >Home</NavLink> */}
        <NavLink to="/">Home</NavLink>
        <NavLink to="/signin">Signin</NavLink>
        <NavLink to="/register">Register</NavLink>
        <NavLink to="/categories">Kategoriler</NavLink>
        <NavLink to="/contents">İçerikler</NavLink>
        <NavLink to="/moods">Ruh hali</NavLink>
        <NavLink to="/playlists">İçerik listeleri</NavLink>
      </nav>

     <Routes>
      
      <Route path="/" element={<Home />} />
      
      <Route path="/signin" element={<Signin />} />
      
      <Route path="/register" element={<Register />} />

      <Route path="/home" element={<Home />} />
      
      <Route path="/categories" element={<CategoryLayout />} >
        <Route index={true} element={<Categories/>} />
        <Route path=':id' element={<CategoryDetail />} />  
        <Route path='create' element={<CreateCategory />} />
        <Route path='*' element={<Category404 />} />
      </Route>
      
      <Route path="/moods" element={<MoodLayout />} >
         <Route index={true} element={<Moods />} />
         <Route path='create' element={<CreateMood />} />
      </Route>
      <Route path="/contents" element={<ContentLayout />} >
        <Route index={true} element={<Contents />} />
        <Route path=":id" element={<ContentDetail />} />
        <Route path='create' element={<CreateContent />} /> 
      </Route>

      <Route path="/playlists" element={<PlayListLayout />} >
        <Route index={true} element={<PlayLists />} />  
        <Route path=':id' element={<PlayListDetail />} />  
        <Route path='create' element={<CreatePlayList />} /> 
      </Route>

      <Route path="*" element={<Page404/>} />

    </Routes> 
    </div>
  );
}

export default App;
