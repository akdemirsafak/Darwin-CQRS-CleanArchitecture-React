import { useState } from "react";
import { register } from "../services/auth";
import {Typography, 
    Button,
    Stack,
    Box,
    TextField } from "@mui/material";

export default function Register(){

    const[email,setEmail]=useState('');
    const[password,setPassword]=useState('');
    const[confirmPassword,setConfirmPassword]=useState('');

    function handleSubmit(e){
        e.preventDefault();
        userRegister(email,password,confirmPassword);
    }
    const userRegister = (email,password,confirmPassword) => {
        register({email,password,confirmPassword})
            .then(res=>{
                if(res.ok && res.status === 200){
                    return res.json()
                }
            }).then(data=>console.log(data.data))            
            .catch(err=>console.error(err))
    }

        return(
        <div className="container">
            <h1>Kayıt ol</h1>
            <form onSubmit={handleSubmit}>
                <Box sx={{
                    width: '40%',
                    display: 'block',
                    justifyContent: 'center',
                    m: 'auto',
                    textAlign: 'center',

                }}>
                    <Stack spacing={2}>
                        <Typography>Email</Typography>
                        <TextField type="text" variant="outlined" value={email} onChange={(e)=>setEmail(e.target.value)}></TextField><br/>
                    </Stack>
                    <Stack spacing={2}>
                        <Typography>Şifre</Typography>
                        <TextField type="password" value={password} onChange={(e)=>setPassword(e.target.value)}></TextField><br/>
                    </Stack>

                    <Stack spacing={2}>
                        <Typography>Şifreyi onaylayın</Typography>
                        <TextField type="password" value={confirmPassword} onChange={(e)=>setConfirmPassword(e.target.value)}></TextField><br/>
                    </Stack>
                    <Button type="submit" variant="contained" >Kayıt ol</Button>
                </Box>
            </form>
        </div>
    )
}