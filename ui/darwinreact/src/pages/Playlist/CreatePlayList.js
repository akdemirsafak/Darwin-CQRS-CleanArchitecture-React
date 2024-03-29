import {createPlaylist } from "../../services/playlist";
import 'bootstrap/dist/css/bootstrap.min.css';
import {
    Card,
    CardContent,
    Typography,
    TextField,
    Checkbox,
    Button,
    CardMedia,
    FormControlLabel,
    Stack} from '@mui/material';
import { Formik, Form } from 'formik';
import { PlayListSchema } from '../../validations/PlayListSchema';


export default function CreatePlayList() {


const initialValues={
    name:'',
    description:'',
    isPublic:false
}

 return (

            <Stack maxWidth='md' spacing={2}>
                    
                <Typography variant="h3" color="initial" className='text-center my-2'>Oynatma listesi oluştur</Typography>
                    <img src={require("../../assets/logo.png")}   className='align-self-center' width={256} alt="logo"/>

                    <Formik
                        initialValues={initialValues}
                        validationSchema={PlayListSchema}
                        onSubmit={(values,actions)=>
                            {
                                createPlaylist(values).then(res =>{
                                    if(res.ok && res.status === 201)
                                    { 
                                        actions.resetForm()
                                    }
                                    actions.setSubmitting(false);
                                })
                                .catch((err)=>console.log(err))
                            }}
                    >
                        {({values, errors, touched, isSubmitting, handleChange, handleReset})=>(
                            <Form>
                                <Stack direction='column'  alignItems='center' padding={1} spacing={1}> 
                                    <TextField fullWidth
                                        id="name"
                                        label="Name"
                                        name='name'
                                        onChange={handleChange}
                                        value={values.name}
                                        error={touched.name && Boolean(errors.name)}
                                        helperText={touched.name && errors.name}
                                        disabled={isSubmitting}
                                    />
                                    
                                    <TextField 
                                        multiline 
                                        fullWidth 
                                        rows={4} 
                                        id='description'
                                        label='Açıklama(Opsiyonel)'
                                        name='description'
                                        onChange={handleChange}
                                        value={values.description}
                                        error={touched.description && Boolean(errors.description)}
                                        helperText={touched.description && errors.description}
                                        disabled={isSubmitting}
                                        />

                                         <FormControlLabel control={
                                        <Checkbox 
                                            value={values.isPublic}
                                            name="isPublic"
                                            onChange={handleChange}
                                            disabled={isSubmitting} 
                                        />} label="Bu listeyi herkes görebilsin mi ?" />

                                        <Button 
                                            type='reset' 
                                            variant='outlined' 
                                            color='error' 
                                            onClick={handleReset} 
                                            disabled={isSubmitting} 
                                            fullWidth
                                        >
                                            Temizle
                                        </Button>
                                        <Button 
                                            type='submit' 
                                            variant='outlined' 
                                            disabled={isSubmitting} 
                                            fullWidth
                                        >
                                            Oluştur
                                        </Button>                   
                                    </Stack>
                                </Form>
                            )}        
                        </Formik>
                    </Stack>
        
 )
}