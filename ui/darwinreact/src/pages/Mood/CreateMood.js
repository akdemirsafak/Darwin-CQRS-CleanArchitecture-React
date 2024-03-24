import { newMood } from "../../services/mood";
import {
    Card,
    CardMedia,
    TextField,
    Button,CardContent,
    Stack, Typography    
} from "@mui/material";
import { MoodSchema } from "../../validations/MoodSchema";
import CloudUploadIcon from '@mui/icons-material/CloudUpload';
import { Formik, Form } from "formik";
import { styled } from '@mui/material/styles';





const VisuallyHiddenInput = styled('input')({
  clip: 'rect(0 0 0 0)',
  clipPath: 'inset(50%)',
  height: 1,
  overflow: 'hidden',
  position: 'absolute',
  bottom: 0,
  left: 0,
  whiteSpace: 'nowrap',
  width: 1,
});




export default function CreateMood() {
    const initialValues= {
        name: '',
        imageFile:''
    };

    return(
        <div className='container'>
            <div className='row'>
                <div className='col-6 offset-3'>
                    <Card>
                        <Typography variant="h3" color="initial" className='text-center my-2'>Mood oluştur</Typography>
                        <div className='align-content-center justify-content-center d-flex'>
                            <CardMedia component='img' image={require("../../attachment_logo.png")}   className='align-self-center w-25' />
                        </div>
                        <CardContent>
                            <Formik
                                initialValues={initialValues}
                                validationSchema={MoodSchema}
                                onSubmit={(values,actions)=>
                                    {
                                        const formData=new FormData();
                                        console.log(localStorage.getItem('token'))
                                        for(let [key,value] of Object.entries(values)){
                                            formData.append(key,value);
                                        }
                                        newMood(formData).then((res)=>{
                                            if(res.ok && res.status === 201){ 
                                                actions.resetForm()
                                                actions.setSubmitting(false);
                                            }else{
                                                actions.setSubmitting(false);
                                            }
                                        })
                                    }}
                            >
                                {({values,errors,touched,isSubmitting,handleChange,handleReset})=>(

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
                                            />
                                            
                                            <Button
                                                component="label"
                                                role={undefined}
                                                variant="contained"
                                                tabIndex={-1}
                                                fullWidth
                                                className='mb-5'
                                                startIcon={<CloudUploadIcon />}
                                                >
                                                Upload
                                                <VisuallyHiddenInput type="file" name='imageFile'
                                                    onChange={e=>values.imageFile=e.target.files[0]}
                                                />
                                                </Button> 
                                                
                                                 {errors.imageFile && touched.imageFile && (
                                                    <div className='alert alert-danger'>
                                                        {errors.imageFile}
                                                    </div>
                                                )} 


                                            <Button type='reset' variant='outlined' color='error' onClick={handleReset} disabled={isSubmitting} fullWidth>Reset</Button>
                                            <Button type='submit' variant='outlined' disabled={isSubmitting} fullWidth>Create</Button>                   
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