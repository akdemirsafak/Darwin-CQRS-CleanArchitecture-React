//import { useState } from "react";
import { login } from "../../services/auth";
// import  logo from "../../attachment_logo.png";
// import {
//     Button,
//     TextField,
//     Card,
//     CardMedia,CardContent,CardActions } from "@mui/material";

import { useAuth } from "../../contexts/AuthContext";
import { useNavigate, useLocation } from "react-router-dom"; //Use location girişten sonra yönlendirme işlemi için dahil edilir.
import { Formik, Form, Field, getFieldProps  } from "formik";
import { LoginSchema } from "../../validations/LoginSchema";
import classNames from "classnames";




export default function Login(){


const navigate= useNavigate();
const location = useLocation();
const {setUser}=useAuth();

   
    const userLogin = (email,password) => {
        login({email,password})
            .then(res=>{
                if(res.ok && res.status === 200){
                    console.log("Response : ",res)
                    return res.json()
                }else{
                    console.log("Response : ",res.json().data)
                    throw new Error('Authentication Failed!')
                }
            })
            .then(data=>
            {
                console.log("Datanın datası : ",data.data)
                console.log(data.data.accessToken)
                localStorage.setItem("token",data.data.accessToken);

            }).catch(err=>console.error(err))
    }


        return(
        <div>
            <h1>Hoşgeldiniz</h1>

            <Formik 
            
                initialValues={{
                    email:'',
                    password:'',

                }}
                validationSchema={LoginSchema}
                onSubmit={(values,actions)=>{
                    userLogin(values.email,values.password)
                    // setUser(values)
                    // navigate(location?.state?.return_url || '/',{ replace: true })

                    //Value'lar api'a gönderilir. api cevap verdiğinde buton tıklanılabilir hale gelir. Api'ın 3 saniyede cevap verdiğini varsaydık.

                    setTimeout(()=>
                    {
                        actions.setSubmitting(false)
                        actions.resetForm()
                    },3000)
                }}
            >
            
                {({values, errors, touched, isSubmitting})=>(
                    <Form>
                        <label>
                            Email address
                        <Field  name='email'
                         type='email'

                        />
                        {errors.email && touched.email && <div className="text-danger small">{errors.email}</div>}
                      
                        </label><br/>
                        <label>
                            Password
                        <Field  name='password' type='password'
                        />
                        </label><br/>
                          {errors.password && touched.password && <div className="text-danger small">{errors.password}</div>}
                        
                        <button type="reset" className="btn btn-outline-danger btn-sm mx-3">Resetle</button>
                        <button type="submit" className={
                            classNames(
                            {
                                "btn my-3":true,
                                "btn-secondary": isSubmitting,
                                "btn-primary":!isSubmitting
                            }
                        )
                    } 
                        disabled={isSubmitting}>Giriş yap</button>
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