import { useEffect, useState } from "react";
import { useParams } from "react-router-dom";
import { getContentById } from "../../services/content";
import 'bootstrap/dist/css/bootstrap.min.css';
import {
    Card,
    CardContent,
    Typography,
    CardMedia,
    CardActions,
    Button
} from "@mui/material";

export default function ContentDetail(){

const {id} = useParams()


const [content,setContent] = useState({
    id:'',
    name:'',
    imageUrl:'',
    moods:[],
    categories:[],
    lyrics:''

})
 
useEffect(()=>{
    getContentById(id)
        .then((result) => {
            if(result.ok && result.status === 200){
                return result.json()
            }
        })
        .then((data)=>{
                setContent(data.data)
        })
        .catch((err)=>console.error('Error:', err))
    },[])
    return (
        <div className='contentCenter'>

            <Card>
                <CardMedia
                    sx={{ maxHeight: 512,width:512 }}
                    image={content.imageUrl}
                    title={content.name}
                    component='img'
                />

                <CardContent>
                    <Typography gutterBottom variant="h4" color='black' component="div">
                        {content.name}
                    </Typography>
                    <Typography>
                        Modlar : {
                            content.moods && content.moods.map((mood) => mood && mood.name).join(', ')
                            }
                    </Typography>
                    <Typography >
                        Kategoriler : {
                            content.categories && content.categories.map((category) => category && category.name).join(', ')
                            }
                    </Typography>
                </CardContent>

                <CardActions>
                    <Button variant="contained" color="error">
                            Sil
                        </Button>
                        <Button variant="contained" color="warning" >
                            Güncelle
                        </Button>
                </CardActions>

            </Card>
        </div>
    )
}