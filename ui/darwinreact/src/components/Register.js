import { useState } from "react";
import { register } from "../services/auth";
import {
    Button,
    Card,
    CardActions,
    CardMedia,
    CardContent,
    TextField, Typography } from "@mui/material";
import  logo from "../attachment_logo.png";
import darwin from "../darwin.png";

export default function Register(){

    const[email,setEmail]=useState('');
    const[password,setPassword]=useState('');
    const[confirmPassword,setConfirmPassword]=useState('');
    const[isActive,setIsActive]=useState(true);

    function handleSubmit(e){
        e.preventDefault();
        userRegister(email,password,confirmPassword);
    }
    const userRegister = (email,password,confirmPassword) => {
        setIsActive(false)
        register({email,password,confirmPassword})
            .then(res=>{
                if(res.ok && res.status === 201){
                    return res.json()
                }
            }).then(data=>{
                alert("Aramıza hoşgeldin.")
                setEmail('')
                setPassword('')
                setConfirmPassword('')
                setIsActive(true)
            })            
            .catch(err=>console.error(err))
    }

        return(
        <div className="container">
           
            <Typography variant="h4"  color="initial">Darwin'e Kayıt ol </Typography>

            <form onSubmit={handleSubmit}>
                  <form onSubmit={handleSubmit}>
                    <Card sx={{
                    width: '25%',
                    display: 'block',
                    justifyContent: 'center',
                    m: 'auto',
                    textAlign: 'center'
                }}>
                    <CardMedia component='img' sx={{
                        width:'96px',
                        height:'96px',
                        objectFit: 'cover',
                        m: '1rem auto 0 auto',
                        textAlign: 'center'
                    }} image={darwin}/>
                    <CardContent  >
                        <TextField sx={{
                            width: '100%'
                        }} type="text" variant="outlined" label="Email" value={email} onChange={(e)=>setEmail(e.target.value)}></TextField><br/>
                        <TextField 
                        sx={{width:'100%',  margin: '1rem auto'}}
                        type="password" value={password} label="Şifre" onChange={(e)=>setPassword(e.target.value)}></TextField>
                         <TextField 
                        sx={{width:'100%'}}
                        type="password" value={confirmPassword} label="Şifreyi doğrulayın" onChange={(e)=>setConfirmPassword(e.target.value)}></TextField>
                    </CardContent>
                    <CardActions sx={{display: "block"}}>
                        <Button type="submit" sx={{
                            alignSelf: 'center'
                        }} variant="contained">Kayıt ol</Button>
                    </CardActions>
                </Card>
            </form>
            </form>
        </div>
    )
}