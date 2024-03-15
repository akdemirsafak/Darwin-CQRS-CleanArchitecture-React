import { useState } from "react";
import { login } from "../services/auth";
import {Typography, 
    Button,
    Box,
    TextField } from "@mui/material";

export default function Signin(){

    const[email,setEmail]=useState('');
    const[password,setPassword]=useState('');

    function handleSubmit(e){
        e.preventDefault();
        userLogin(email,password);
    }
    const userLogin = (email,password) => {
        login({email,password})
            .then(res=>{
                if(res.ok && res.status === 200){
                    return res.json()
                }
            }).then(data=>{localStorage.setItem("token",data.data.accessToken);})
            .catch(err=>console.error(err))
    }

        return(
        <div>
            <h1>Login</h1>
            <form onSubmit={handleSubmit}>
                <Box>
                    <Typography>Email</Typography>
                    <TextField type="text" variant="outlined" value={email} onChange={(e)=>setEmail(e.target.value)}></TextField><br/>
                    <Typography>Password</Typography>
                    <TextField type="password" value={password} onChange={(e)=>setPassword(e.target.value)}></TextField><br/>
                    <Button type="submit" variant="contained">Login</Button>
                </Box>
            </form>
        </div>
    )
}