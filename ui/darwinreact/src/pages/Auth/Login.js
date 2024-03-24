//import { useState } from "react";
import { login } from "../../services/auth";
import  logo from "../../attachment_logo.png";
import {
    Button,
    TextField,
    Card,
    CardMedia,
    CardContent,
    Typography,
    Stack } from "@mui/material";

import { useAuth } from "../../contexts/AuthContext";
import { useNavigate, useLocation } from "react-router-dom"; //Use location girişten sonra yönlendirme işlemi için dahil edilir.
import { Formik, Form } from "formik";
import { LoginSchema } from "../../validations/LoginSchema";




export default function Login(){

  const initialValues= {
        email: '',
        password:''
    };


const navigate= useNavigate();
const location = useLocation();
const {setUser}=useAuth();


        return(
 <div className='container'>
            <div className='row'>
                <div className='col-6 offset-3'>
                    <Card>
                        <Typography variant="h3" color="initial" className='text-center my-2'>Giriş yap</Typography>
                        <div className='align-content-center justify-content-center d-flex'>
                            <CardMedia component='img' image={logo}   className='align-self-center w-25' />
                        </div>
                        <CardContent>
                            <Formik
                                initialValues={initialValues}
                                validationSchema={LoginSchema}
                                onSubmit={(values,actions)=>
                                    {
                                        login(values)
                                            .then(res=>{
                                                if(res.ok && res.status === 200){
                                                    return res.json()
                                                }else{
                                                    //throw new Error('Authentication Failed!')
                                                    console.log("Authentication Failed!")
                                                    actions.setSubmitting(false);
                                                }
                                            })
                                            .then(data=>
                                            {
                                                
                                                localStorage.setItem("token",data.data.accessToken);
                                                setUser(values)
                                                navigate(location?.state?.return_url || '/',{ replace: true })

                                            }).catch(err=>console.error(err))
                                    }}
                            >
                                {({values,errors,touched,isSubmitting,handleChange})=>(

                                    <Form>
                                        <Stack direction='column'  alignItems='center' padding={1} spacing={1}> 
                                            <TextField fullWidth
                                                id="email"
                                                label="email"
                                                name='email'
                                                onChange={handleChange}
                                                value={values.email}
                                                error={touched.email && Boolean(errors.email)}
                                                helperText={touched.email && errors.email}
                                                disabled={isSubmitting}
                                            />
                                                <TextField fullWidth
                                                id="password"
                                                label="password"
                                                name='password'
                                                type='password'
                                                onChange={handleChange}
                                                value={values.name}
                                                error={touched.password && Boolean(errors.password)}
                                                helperText={touched.password && errors.password}
                                                disabled={isSubmitting}
                                            />

                                            <Button type='submit' variant='outlined' disabled={isSubmitting} fullWidth>Giriş yap</Button>                   
                                        </Stack>
                                    </Form>
                                )}  
                            </Formik>
                        </CardContent>
                    </Card>
                </div>
            </div>
        </div>



    )
}