import { newCategory } from '../../services/category';
import { Formik, Form } from 'formik';
import { CategorySchema } from '../../validations/CategorySchema';
import CloudUploadIcon from '@mui/icons-material/CloudUpload';
import { styled } from '@mui/material/styles';
import {
        Typography, 
        Button,
        TextField,
        Stack
} from '@mui/material'



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

export default function CreateCategory(){

  const initialValues= {
        name: '',
        imageFile:''
    };


    return(
            <Stack maxWidth='md' spacing={3}>
                <Typography variant="h3" color="initial" className='text-center my-2'>Kategori oluştur</Typography>
                <img src={require("../../assets/logo.png")} width={256} className='align-self-center' alt="logo"/>

                <Formik
                    initialValues={initialValues}
                    validationSchema={CategorySchema}
                    onSubmit={(values,actions)=>
                        {
                            const formData=new FormData();
                            for(let [key,value] of Object.entries(values)){
                                formData.append(key,value);
                            }
                            newCategory(formData).then((res)=>{
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
                                                disabled={isSubmitting}
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
                                                <VisuallyHiddenInput 
                                                
                                                type="file" name='imageFile'
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
            </Stack>
    )
}
