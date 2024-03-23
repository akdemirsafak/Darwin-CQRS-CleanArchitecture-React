import { useEffect, useState } from "react";
import { useParams } from "react-router-dom";
import { getContentById } from "../../services/content";
import 'bootstrap/dist/css/bootstrap.min.css';
import {
    Grid,
    Card,
    CardContent,
    Typography,
    CardMedia,
    CardActions,
    Button
} from "@mui/material";

export default function ContentDetail(){

const {id} = useParams()


const [content,setContent] = useState({})
 
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
        <>
             <Grid key={content.id} item sx={{marginTop:10}}  >
                    <Card className="col-4 offset-4" >
                        <CardMedia
                            sx={{ height: 512 }}
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
                                {/* <NavLink to={`/categories/${category.id}`}>Sil</NavLink> */} 
                                    Sil
                                </Button>
                                <Button variant="contained" color="warning" >
                                {/* <NavLink to={`/categories/${category.id}`}>Güncelle</NavLink> */}
                                    Güncelle
                                </Button>
                        </CardActions>

                    </Card>
            
            </Grid>                   

            
        </>
    )
}