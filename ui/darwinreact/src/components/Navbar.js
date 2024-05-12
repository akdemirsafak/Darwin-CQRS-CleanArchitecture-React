import logo from '../assets/logo.png'
import React,{ useState } from'react';
import {Link} from 'react-router-dom'
import MenuIcon from '@mui/icons-material/Menu';


import { 
  AppBar, 
  Toolbar,
  IconButton, 
  Typography, 
  Button, 
  Box,
  Menu,MenuList,MenuItem
} from '@mui/material';

export default function Navbar(){
    
  const[anchorNav, setAnchorNav] = useState(null);
  const openMenu=(event)=>{
    setAnchorNav(event.currentTarget);
  }
  const closeMenu=()=>{
    setAnchorNav(null);
  }
  return(

      <AppBar position='static' sx={{
        backgroundColor:'#4A5C6A'
      }}>
        <Toolbar>
        <IconButton 
          color='inherit' 
          aria-label='open menu' 
          edge='start' 
          size='large' 
          sx={{
            display:{xs:'none',md:'flex',lg:'flex'}
          }}
        ></IconButton>
    


        <Typography sx={{ flexGrow:1,display:{xs:'none',md:'flex',lg:'flex'}}}
          color='inherit' variant='h6' component='div' >
            Darwin
        </Typography>

        <Box sx={{display:{xs:'none',md:'flex'}}}>
          <Button color='inherit' component={Link} to={'/contents'}>İÇERİKLER</Button>
          <Button color='inherit' component={Link} to={'/categories'}>KATEGORİLER</Button>
          <Button color='inherit' component={Link} to={'/moods'}>KEŞFET</Button>
          <Button color='inherit' component={Link} to={'/playlists'}>LİSTELER</Button>
          <Button color='inherit' component={Link} to={'/profile'}>PROFİL</Button>
        </Box>
        <Box sx={{display:{xs:'flex',md:'none'}}}>
          <IconButton size='large' edge='start' color='inherit' onClick={openMenu}>
            <MenuIcon/>
          </IconButton>


        <Menu open={Boolean(anchorNav)} sx={{
            display:{xs:'flex',md:'none'}
          }}
          onClose={closeMenu}
        >
          <MenuList sx={{height:'100'}}>
            <MenuItem>Anasayfa</MenuItem>
            <MenuItem component={Link} to={'/contents'}>İçerikler</MenuItem>
            <MenuItem component={Link} to={'/categories'}>Kategoriler</MenuItem>
            <MenuItem component={Link} to={'/moods'}>Keşfet</MenuItem>
            <MenuItem component={Link} to={'/playlists'}>Listeler</MenuItem>
            <MenuItem component={Link} to={'/profile'}>Profil</MenuItem>
          </MenuList>
        </Menu>

        </Box>
          <IconButton 
          color='inherit' 
          aria-label='open menu' 
          edge='start' 
          size='large'
          sx={{
            display:{xs:'flex',md:'none'}
            
          }}

        >
          {/* <img src={logo} alt='logo' height={48} /> */}
        </IconButton>
        <Typography color='inherit' variant='h5' component='div' sx={{ flexGrow:1,display:{xs:'flex',md:'none'}}}>
        Darwin
        </Typography>

        </Toolbar>
      </AppBar>
    )
}