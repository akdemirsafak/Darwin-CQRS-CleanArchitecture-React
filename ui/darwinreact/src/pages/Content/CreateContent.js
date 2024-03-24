import { useState, useEffect } from "react";
import { createContent } from "../../services/content";
import { getCategories } from "../../services/category";
import { getMoods } from "../../services/mood";
import {Button,
    FormControl,
    MenuItem,
    Select,
    InputLabel,
    FormControlLabel,
    TextField,
    Box,
    Checkbox} from '@mui/material'
import SaveIcon from '@mui/icons-material/Save';

export default function CreateContent() {
    
    const[name, setName]=useState('')
    const[lyrics, setLyrics]=useState('')
    const[imageUrl, setImageUrl]=useState('')
    const[isUsable, setIsUsable]=useState(false)
    const[categories, setCategories]=useState([])
    const[moods, setMoods]=useState([])

    const[selectedCategories,setSelectedCategories]=useState([])
    const[selectedMoods,setSelectedMoods]=useState([])



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

    const createNewContent=(data)=>{
        createContent(data).then(res =>{
            if(res.ok && res.status === 201)
            { 
                return res.json()
            }
        }).then(data=>console.log(data.data))
        .catch((err)=>console.log(err))
    }

    function handleSubmit(e){
         e.preventDefault();
        createNewContent({name, lyrics, imageUrl, isUsable, categoryIds:selectedCategories, moodIds:selectedMoods})

    }



    return (
        <div>
            <h1>İçerik ekle</h1>
            <form onSubmit={handleSubmit}>
                 <Box sx={{
                    width: '50%',
                    display: 'block',
                    justifyContent: 'center',
                    m: 'auto',
                    textAlign: 'center'
                }}>
                <div className="form-group row" style={{margin:25}}>
                    <TextField id="name-field" label="Ad" variant="standard" onChange={e=>setName(e.target.value)} />   
                </div>
    
                <div className="form-group row">
                    <TextField id="lyric-field" label="İçerik sözleri" variant="standard" onChange={e=>setLyrics(e.target.value)} />   
                </div>
                <div className="form-group row">
                   <TextField id="imageUrl-field" label="Görsel adı" variant="standard" onChange={e=>setImageUrl(e.target.value)} />   
                </div>
                

                <div className="form-group">
                <FormControl sx={{ m: 1, minWidth: 120 }}>
                    <InputLabel id="demo-multiple-select-label">kategoriler</InputLabel>
                    <Select
                    multiple
                    value={selectedCategories}
                    onChange={e=>setSelectedCategories(e.target.value)}
                    renderValue={(selected)=>selected.join(',')}>
                        <MenuItem value="">
                            <em>Kategori Seçiniz</em>
                        </MenuItem>
                        {
                            categories && categories.map((category)=>(
                                <MenuItem key={category.id} value={category.id}>{category.name}</MenuItem>
                            ))
                        }
                    </Select>
                </FormControl>                   
                </div>
                <div className="form-group">
                <FormControl sx={{ m: 1, minWidth: 120 }}>
                    <InputLabel id="demo-multiple-select-label">modlar</InputLabel>
                    <Select
                    width="100%"
                    multiple
                    value={selectedMoods}
                    onChange={e=>setSelectedMoods(e.target.value)}
                    renderValue={(selected)=>selected.join(',')}>
                        <MenuItem value="">
                            <em>Mod Seçiniz</em>
                        </MenuItem>
                        {
                            moods && moods.map((mood)=>(
                                <MenuItem key={mood.id} value={mood.id}>{mood.name}</MenuItem>
                            ))
                        }
                    </Select>
                </FormControl>                   
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
                <Button type="submit" variant="contained" startIcon={<SaveIcon />}>Kaydet</Button>
            </Box>
            </form>
        </div>
    );
}