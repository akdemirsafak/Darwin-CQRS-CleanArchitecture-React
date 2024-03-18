import { useState } from "react";
import { login } from "../../services/auth";
import  logo from "../../attachment_logo.png";
import {
    Button,
    TextField,
    Card,
    CardMedia,CardContent,CardActions } from "@mui/material";

import { useAuth } from "../../contexts/AuthContext";
import { useNavigate, useLocation } from "react-router-dom"; //Use location girişten sonra yönlendirme işlemi için dahil edilir.


export default function Login(){


const navigate= useNavigate();
const location = useLocation();
const {setUser}=useAuth();


    const[email,setEmail]=useState('');
    const[password,setPassword]=useState('');

    function handleSubmit(e){
        e.preventDefault();
        userLogin(email,password);
        setUser({
                username:'yalandan'
        });
        navigate(location?.state?.return_url || '/')
    }
    const userLogin = (email,password) => {
        login({email,password})
            .then(res=>{
                if(res.ok && res.status === 200){
                    return res.json()
                }
            })
            .then(data=>
            {
                setEmail('')
                setPassword('')
                localStorage.setItem("token",data.data.accessToken);
              
                
            }).catch(err=>console.error(err))
    }

        return(
        <div>
            <h1>Hoşgeldiniz</h1>
            <form onSubmit={handleSubmit}>
                    <Card sx={{
                    width: '25%',
                    display: 'block',
                    justifyContent: 'center',
                    m: 'auto',
                    textAlign: 'center'
                }}>
                    <CardMedia component='img' sx={{
                        width:'128px',
                        height:'128px',
                        objectFit: 'cover',
                        m: '1rem auto 0 auto',
                        textAlign: 'center'
                    }} image={logo}/>
                    <CardContent >
                        <TextField sx={{
                            width: '100%',
                            margin: '1rem auto'
                        }} type="text" variant="outlined" label="Email" value={email} onChange={(e)=>setEmail(e.target.value)}></TextField><br/>
                        <TextField 
                        sx={{width:'100%'}}
                        type="password" value={password} label="Şifre" onChange={(e)=>setPassword(e.target.value)}></TextField>
                    </CardContent>
                    <CardActions sx={{display: "block"}}>
                        <Button type="submit" sx={{
                            alignSelf: 'center'
                        }} variant="contained">Giriş yap</Button>
                    </CardActions>
                </Card>
            </form>
        </div>
    )
}