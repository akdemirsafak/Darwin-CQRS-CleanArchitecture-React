import * as React from 'react';
import Avatar from '@mui/material/Avatar';
import Button from '@mui/material/Button';
import CssBaseline from '@mui/material/CssBaseline';
import TextField from '@mui/material/TextField';
import FormControlLabel from '@mui/material/FormControlLabel';
import Checkbox from '@mui/material/Checkbox';
import Link from '@mui/material/Link';
import Grid from '@mui/material/Grid';
import Box from '@mui/material/Box';
import LockOutlinedIcon from '@mui/icons-material/LockOutlined';
import Typography from '@mui/material/Typography';
import Container from '@mui/material/Container';
import { createTheme, ThemeProvider } from '@mui/material/styles';
import { InputAdornment } from '@mui/material';
import { EmailRounded } from '@mui/icons-material'
import { useState } from 'react';
import GoogleButton from 'react-google-button';




// TODO remove, this demo shouldn't need to reset the theme.
const defaultTheme = createTheme();

export default function SignUp() {
  const [value, setValue] = useState<string|null>(null);
  const handleSubmit = (event: React.FormEvent<HTMLFormElement>) => {
    event.preventDefault();
    const data = new FormData(event.currentTarget);
    console.log({
      email: data.get('email'),
      password: data.get('password'),
    });
  };

  return (
    <ThemeProvider theme={defaultTheme}>
        <Container component="main" sx={{mt:5}} maxWidth="xs">
          <Typography variant='h3'>Darwin </Typography>
          <Typography variant="h5" color="primary" > Listen Music & Podcasts</Typography>
        <CssBaseline />
        <Box
          sx={{
            marginTop: 5,
            display: 'flex',
            flexDirection: 'column',
            alignItems: 'center',
          }}
        >
          <Avatar sx={{ m: 1, bgcolor: 'secondary.main' }}>
            <LockOutlinedIcon />
          </Avatar>
          <Typography component="h1" variant="h5">
            Sign up
          </Typography>
          <Box component="form" noValidate onSubmit={handleSubmit} sx={{ mt: 3 }}>
            <Grid container spacing={2}>
    
              <Grid item xs={12}>
                <TextField
                  required
                  fullWidth
                  id="email"
                  label="Email Address"
                  placeholder='Email Adress'
                  name="email"
                  autoComplete="email"
                  InputProps={{
          startAdornment: (
            <InputAdornment position="start">
              <EmailRounded  />
            </InputAdornment>
          ),
        }}
                />
              </Grid>
              <Grid item xs={12}>
                <TextField
                  required
                  fullWidth
                  name="password"
                  label="Password"
                  type="password"
                  id="password"
                  autoComplete="new-password"
                />
              </Grid>

              </Grid>
            </Grid>
            <Button
              type="submit"
              fullWidth
              variant="contained"
              sx={{ mt: 3, mb: 2 }}
            >
              Sign Up
            </Button>
          
          </Box>
        </Box>
        <Typography variant="h6" color="initial">veya </Typography>
          <Grid item xs={12} margin={5}
          style={{
          display:'flex',
          justifyContent:'center'
          }}>
              <GoogleButton 
                style={{backgroundColor:'white',color:'black',borderRadius:'0.5rem', display:'block'}}
                onClick={() => { console.log('Google button clicked') }}
                label='Google ile kaydol'
                />
              </Grid>
            <Grid container justifyContent="flex-end">
                <Grid item xs={12} margin={5}>
                <Link href="#" variant="h6">
                  Already have an account? Sign in
                </Link>
              </Grid>
            </Grid>
          </Box>
        </Box>
        <Copyright sx={{ mt: 5 }} />
      </Container>
    </ThemeProvider>
  );
}