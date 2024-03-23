import HomeLayout from "./pages/HomeLayout"
import ContentLayout from './pages/Content/index';
import PlaylistLayout from './pages/Playlist/index';
import CategoryLayout from './pages/Category/index';


import Home  from './pages/Home'
import AuthLayout from './pages/Auth/index';
import Login from './pages/Auth/Login';
import Register from './pages/Auth/Register';


import Moods from './pages/Mood/Moods';
import MoodLayout from './pages/Mood/index';
import CreateMood from './pages/Mood/CreateMood';

import Contents from './pages/Content/Contents';

import ContentDetail from './pages/Content/ContentDetail';
import CreateContent from './pages/Content/CreateContent';

import PlayLists from './pages/Playlist/PlayLists';
import CreatePlayList from './pages/Playlist/CreatePlayList';
import PlayListDetail from './pages/Playlist/PlayListDetail';


import Categories from './pages/Category/Categories';
import CategoryDetail from './pages/Category/CategoryDetail';
import CreateCategory from './pages/Category/CreateCategory';

import Page404 from './pages/Page404';
import Category404 from './pages/Category/404';
import PrivateRoute from './components/PrivateRoute';
import Profile from './pages/User/Profile';


const routes=[
    {
        name:'home',
        path:'/',
        element:<HomeLayout/>,
        children:[
            {   name:'index',
                index:true,
                element:<Home />
            },
            //Categories
            {
                name:'categories',
                path:"categories",
                element:<CategoryLayout />,
                children:[
                    {   
                        name:'index',
                        index:true,
                        element:<Categories/>
                    },
                    {   name:'categorydetail',
                        path:':id',
                        element:<CategoryDetail />
                    },
                    {
                        name:'CreateCategory',
                        path:'create',
                        element:<CreateCategory />,
                        auth:true
                    },
                    {
                        name:'category404NotFound',
                        path:'*',
                        element:<Category404 />
                    }
                ]
            },
            //Moods
            {
                name:'moods',
                path:"moods",
                element:<MoodLayout />,
                children:[
                    {
                        name:'index',
                        index:true,
                        element:<Moods />
                    },
                    {
                        name:'CreateMood',
                        path:'create',
                        element:<CreateMood />,
                        auth:true
                    }
                ]
            },
            //Contents
            {
                name:'contents',
                path:'contents',
                element: <ContentLayout />,
                children:[
                    {
                        name:'index',
                        index:true,
                        element:<Contents />
                    },
                    {
                        name:'contentdetail',
                        path:':id',
                        element:<ContentDetail />
                    },
                    {
                        name:'CreateContent',
                        path:'create',
                        element:<CreateContent />,
                        auth:true
                    }
                ]
            },
            //Playlists
            {
                name:'playlists',
                path:"playlists", 
                element: <PlaylistLayout/>,            
                children:[
                    {
                        name:'index',
                        index:true,
                        element:<PlayLists />
                    },
                    {
                        name:'playlistdetail',
                        path:':id',
                        element:<PlayListDetail />
                    },
                    {
                        name:'CreatePlayList',
                        path:'create',
                        element:<CreatePlayList />,
                        auth:true,
                    }
                ]
            },
            //Profile
            {
                name:'profile',
                path:'/profile',
                element:<Profile/>,
                auth:true,
            },

        ]
    },
    {
        name:'auth',
        path:'/auth',
        element:<AuthLayout/>,
        children:[
            {
                name:'login',
                path:"login",
                element:<Login />
            },
            {
                name:'register',
                path:"register",
                element:<Register />
            }
        ]
    },
    {
        name:'PageNotFound',
        path:'*',
        element:<Page404/>
    }
]

const authMap = routes=> routes.map(route=>{
    if(route?.auth)
    {
        //route.element= <PrivateRoute {...route} />
        route.element=<PrivateRoute>{route.element}</PrivateRoute>
    }
    if(route?.children){
        route.children=authMap(route.children)
    }
    return route
})

export default authMap(routes);