import { useState, useEffect } from "react";
import { createContent } from "../../services/content";
import { getCategories } from "../../services/category";
import { getMoods } from "../../services/mood";
import {
    Button,
    Typography,
    Stack,
    TextField,
    FormControl,
    InputLabel,
    Select,
    MenuItem
    
} from '@mui/material'
import CloudUploadIcon from '@mui/icons-material/CloudUpload';
import { Form,Formik,ErrorMessage } from "formik";
import { ContentSchema } from "../../validations/ContentSchema";
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


export default function CreateContent() {
    
    const[categories, setCategories]=useState([])
    const[moods, setMoods]=useState([])


       const initialValues= {
        name: '',
        lyrics:'',
        imageFile:'',
        selectedCategories:[],
        selectedMoods:[]
    };


    useEffect(() => {
        getCategories().then((res)=>{
            if(res.ok && res.status === 200){
                return res.json()
            }})
            .then(data=> setCategories(data.data))
            .catch((err)=>console.log(err))

        getMoods().then((res)=>{
            if(res.ok && res.status === 200){
                return res.json()
            }})
            .then(data=> setMoods(data.data))
            .catch((err)=>console.log(err))
    }, []);


return (
        <Stack maxWidth='md' spacing={3}>                  
            <Typography variant="h3" color="initial" className='text-center my-2'>İçerik oluştur</Typography>
            <img src={require("../../assets/logo.png")}   className='align-self-center' width={256} alt="logo" />
            
            <Formik
                initialValues={initialValues}
                validationSchema={ContentSchema}
                onSubmit={(values,actions)=>
                    {
                        const formData=new FormData();
                        formData.append('name',values.name)
                        formData.append('lyrics',values.lyrics)
                        formData.append('imageFile',values.imageFile)
                        
                        values.selectedMoods.forEach((mood, index) => {
                            formData.append(`selectedMoods[${index}]`, mood);
                            }); // bu kısmı foreach ile dönmezsek direkt string formatla yanyana ekleyip yolluyor ve api 400-500 dönüyor.

                                            values.selectedCategories.forEach((category, index) => {
                                            formData.append(`selectedCategories[${index}]`, category);
                                            });

                                        createContent(formData).then((res)=>{
                                             if(res.ok && res.status === 201){ 
                                                 actions.resetForm()
                                                 actions.setSubmitting(false);
                                             }else{
                                                console.log(res.body)
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
                                                label="name"
                                                name="name"
                                                onChange={handleChange}
                                                value={values.name}
                                                error={touched.name && Boolean(errors.name)}
                                                helperText={touched.name && errors.name}
                                            />


                                            <TextField fullWidth
                                                multiline
                                                rows={4}
                                                id="lyrics"
                                                label="lyrics"
                                                name="lyrics"
                                                onChange={handleChange}
                                                value={values.lyrics}
                                                error={touched.lyrics && Boolean(errors.lyrics)}
                                                helperText={touched.lyrics && errors.lyrics}
                                            />


                                    <FormControl fullWidth>
                                        <InputLabel id="multiple-select-label">Kategoriler</InputLabel>
                                            <Select
                                                labelId="multiple-select-label"
                                                id="multiple-select"
                                                name="selectedCategories"
                                                multiple
                                                value={values.selectedCategories}
                                                label="Kategoriler"
                                                onChange={handleChange}
                                            >
                                                {categories.map((category) => (
                                                    <MenuItem key={category.id} value={category.id}>
                                                        {category.name}
                                                    </MenuItem>
                                                ))}
                                            </Select>
                                            <ErrorMessage name="selectedCategories" component="div" />
                                    </FormControl>



                                    <FormControl fullWidth>
                                        <InputLabel id="multiple-select-label">Moods</InputLabel>
                                        <Select
                                            labelId="multiple-select-label"
                                            id="multiple-select"
                                            name="selectedMoods"
                                            multiple
                                            value={values.selectedMoods}
                                            label="Moods"
                                            onChange={handleChange}
                                        >
                                            {moods.map((mood) => (
                                                <MenuItem key={mood.id} value={mood.id}>
                                                    {mood.name}
                                                </MenuItem>
                                            ))}
                                        </Select>
                                        <ErrorMessage name="selectedMoods" component="div" />
                                    </FormControl>



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
                        </Stack>
    );
}