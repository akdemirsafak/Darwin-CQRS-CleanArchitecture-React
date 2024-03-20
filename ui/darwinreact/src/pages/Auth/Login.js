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
import { Formik,Form, Field } from "formik";
import File from '../../components/File';
import Checkbox from "../../components/Checkbox";



export default function Login(){


const navigate= useNavigate();
const location = useLocation();
const {setUser}=useAuth();

   
    const userLogin = (email,password) => {
        login({email,password})
            .then(res=>{
                if(res.ok && res.status === 200){
                    return res.json()
                }
            })
            .then(data=>
            {
                localStorage.setItem("token",data.data.accessToken);

            }).catch(err=>console.error(err))
    }


        return(
        <div>
            <h1>Hoşgeldiniz</h1>

            <Formik 
            
                initialValues={{
                    email:'akdemirsafak@gmail.com',
                    password:'Deneme_1234',
                    photo:'',
                    agree:true

                }}
                onSubmit={values=>{
                    userLogin(values.email,values.password)
                    setUser(values)
                    navigate(location?.state?.return_url || '/',{ replace: true })
                }}
            >

            
                {({values})=>(
                    <Form>
                        <label>
                            Email address
                        <Field  name='email' type='email'/>
                        </label><br/>
                        <Field  name='password' type='password'/><br/>
                        <File name='photo' label="Upload Image"/><br/>
                        <Checkbox name='agree' label="I agree!"/><br/><br/>
                        <button type="submit">Giriş yap</button>
                    </Form>
                )}
                

            </Formik>




            {/* <form onSubmit={handleSubmit}>
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
            </form> */}
        </div>
    )
}