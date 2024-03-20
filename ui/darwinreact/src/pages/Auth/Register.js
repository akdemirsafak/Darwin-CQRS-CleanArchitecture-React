import { Form, Formik } from 'formik';
import { RegisterScheme } from '../../validations/RegisterScheme'
import * as React from 'react';
import TextField from '@mui/material/TextField'
import Button from '@mui/material/Button'
import Stack from '@mui/material/Stack'
import { register } from '../../services/auth';



export default function Register(){

    const logo=require("../../attachment_logo.png");

    const initialValues= {
        email: '',
        password: '',
        confirmPassword:''
    };


    return(

        <>    
        <div className='d-flex justify-content-center mt-5'>
            <img src={logo} alt='Logo' width={128} />
        </div>
        <div className='row'>

            <div className='col-4 offset-4'>
                <Formik
                initialValues={initialValues}
                validationSchema={RegisterScheme}
                onSubmit={(values, actions)=>{
                    actions.setSubmitting(true)
                    register({email:values.email,
                        password:values.password,
                        confirmPassword:values.confirmPassword})
                        .then((res)=>{
                            if(res.ok && res.status === 201){
                                values.email=''
                                values.password=''
                                values.confirmPassword=''
                                alert('Registration Successful!')
                                actions.setSubmitting(false);
                            }else{
                                actions.setSubmitting(false);
                                alert('Registration Failed!')
                                //throw new Error('Registration Failed!')
                            }                            
                        })
                }}
            >
            {({values, errors, touched, isSubmitting, handleChange, handleReset})=>(
            
            <Form>
                <Stack direction='column'  alignItems='center' padding={1} spacing={1}> 
                    <TextField fullWidth
                        id="email"
                        label="Email"
                        name='email'
                        onChange={handleChange}
                        value={values.email}
                        error={touched.email && Boolean(errors.email)}
                        helperText={touched.email && errors.email}
                    />                   

                    <TextField fullWidth
                        id="password"
                        label="Password"
                        name='password'
                        type='password'
                        value={values.password}
                        onChange={handleChange}
                        error={touched.password && Boolean(errors.password)}
                        helperText={touched.password && errors.password}
                    />
                    
                    <TextField fullWidth
                        id="confirmPassword"
                        label="Confirm Password"
                        name='confirmPassword'
                        type='password'
                        value={values.confirmPassword}
                        onChange={handleChange}
                        error={touched.confirmPassword && Boolean(errors.confirmPassword)}
                        helperText={touched.confirmPassword && errors.confirmPassword}
                    />
                        
                    <Button type='reset' variant='outlined' onClick={handleReset} disabled={isSubmitting}  fullWidth color='error'>Reset</Button>
                    <Button type='submit' variant='outlined' disabled={isSubmitting} fullWidth> 
                     {isSubmitting? 'Registering..' : 'Register'}</Button>
                </Stack>

            </Form>
            )}
            </Formik> 
            </div>
        </div>
    </>   
    )
}





                     

                    