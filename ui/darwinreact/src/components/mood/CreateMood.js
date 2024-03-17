import { newMood } from "../../services/mood";
import {useState} from "react";
import {Typography, 
    Button,
    TextField, 
    FormControlLabel,
    Checkbox, 
    Box,
    Icon } from "@mui/material";
export default function CreateMood() {

    const addMood = (data,token) => {
        const formData=new FormData();
        for(let [key,value] of Object.entries(data)){
            formData.append(key,value);
        };
        newMood(formData,token).then(res =>{
            if(res.ok && res.status === 201){ 
                alert("Mood created successfully");
                setName('');
                setImageFile(null);
                setIsUsable(false);
            }
         })
         .catch((error)=>alert("Error: "+ error));
    }

    function submitHandle(e) {
        e.preventDefault();
        addMood({name,imageFile,isUsable},"eyJhbGciOiJodHRwOi8vd3d3LnczLm9yZy8yMDAxLzA0L3htbGRzaWctbW9yZSNobWFjLXNoYTI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1laWRlbnRpZmllciI6IjY0N2Q1YzE2LThiODQtNDI2YS04ZWNmLTU1ZGZhZDIzZjdhZSIsImVtYWlsIjoiYWtkZW1pcnNhZmFrQGdtYWlsLmNvbSIsImp0aSI6ImM1ZmUwMGViLWY0ZjMtNGYxYy04OWIwLWE5ZjA2MWM3YTVlMyIsImF1ZCI6WyJodHRwczovL2xvY2FsaG9zdDo3MTE2IiwiaHR0cHM6Ly9sb2NhbGhvc3Q6NzExNyJdLCJodHRwOi8vc2NoZW1hcy5taWNyb3NvZnQuY29tL3dzLzIwMDgvMDYvaWRlbnRpdHkvY2xhaW1zL3JvbGUiOiJVc2VyIiwibmJmIjoxNzEwNTA5NjcxLCJleHAiOjE3MTA1MTE0NzEsImlzcyI6Imh0dHBzOi8vbG9jYWxob3N0OjcxMTYifQ.y7WUB75cGrNvquiEP1WgnSI5DGEDsBDYLm31i1OmFgY")        
    }

    const[name, setName]=useState('');
    const[imageFile, setImageFile]=useState(false);
    const[isUsable, setIsUsable]=useState(false);

    return(
        <>
        <h1>Create Mood</h1>
            <form onSubmit={submitHandle}>

                <div className="form-group row" style={{margin:25}}>
                    <TextField id="standard-basic" label="Name" variant="standard" onChange={e=>setName(e.target.value)} />   
                </div>
                <div className="form-group row">
                   <Box>
                        <input
                            type="file"
                            accept=".jpg, .png, .jpeg"
                            onChange={event=>setImageFile(event.target.files[0])}
                            style={{ display: 'none' }}
                            id="file-upload"
                        />
                        <label htmlFor="file-upload">
                            <Button variant="outlined" component="span" startIcon={<Icon variant="file_upload"/>}>
                            Görseli seçin:
                            </Button>
                        </label>
                        {imageFile && (
                            <Typography variant="body1" component="p">
                            Selected File: {imageFile.name}
                            </Typography>
                        )}
                    </Box>
                </div>


                <div className="form-group row">
                    <FormControlLabel
                      label="IsUsable"

                      control={
                        <Checkbox
                          value={isUsable}
                          checked={isUsable}
                          onChange={e=>setIsUsable(e.target.checked)}
                          color="primary"
                        />
                      }
                    />
                </div>
                <Button variant="contained" type="submit">Create Mood</Button>
            </form>
        </>
    )
}