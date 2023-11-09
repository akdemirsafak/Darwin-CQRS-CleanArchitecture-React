import * as React from 'react';
import CssBaseline from '@mui/material/CssBaseline';
import Link from '@mui/material/Link';
import Grid from '@mui/material/Grid';
import Typography from '@mui/material/Typography';
import { createTheme, ThemeProvider } from '@mui/material/styles';
import image from "../img/attachment_logo.png"
import SignIn from '../components/Sign/SignIn';

function Copyright(props: any) {
    return (
    <Typography variant="body2" color="text.secondary" align="center" {...props}>
        
        {'Copyright © '}
        <Link color="inherit" href="https://mui.com/">
        Your Website
        </Link>{' '}
        {new Date().getFullYear()}
        {'.'}
        
    </Typography>

    );
}

// TODO remove, this demo shouldn't need to reset the theme.
const defaultTheme = createTheme();

export default function SignInSide() {

    return (

    <ThemeProvider theme={defaultTheme}>
        <Grid container component="main" sx={{ height: '100vh' }}>
        <CssBaseline />
        <Grid
            item
            xs={false}
            sm={4}
            md={7}
            sx={{
            backgroundImage:`url(${image})`,
            backgroundRepeat: 'no-repeat',
            backgroundColor: (t) =>
                t.palette.mode === 'light' ? t.palette.grey[50] : t.palette.grey[900],
            backgroundSize: 'cover',
            backgroundPosition: 'center',
            }}
        />
  
            <SignIn/> 
        </Grid>
    </ThemeProvider>
    
    );
}