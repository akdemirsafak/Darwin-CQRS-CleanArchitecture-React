import { useEffect, useState } from "react"
import { getCategoryDetail } from "../../services/category"
import { useParams } from "react-router-dom"
import {
    Grid,
    Card,
    CardContent,
    CardActions,
    Button,
    Typography,
    CardMedia} from '@mui/material';

export default function CategoryDetail(){

const[category,setCategory]=useState({});
const { id }= useParams();

useEffect(()=>{
    getCategoryDetail(id).then((result) => {
        if(result.ok && result.status === 200){
            return result.json()
        }
        })
        .then(data=> setCategory(data.data))
        .catch((err) => {
            console.log(err)
        });
    },[])
    
    return(
        <>
            <Grid key={category.id} item sx={{marginTop:10}}  >
                    <Card className="col-4 offset-4" >
                        <CardMedia
                            sx={{ height: 512 }}
                            image={category.imageUrl}
                            title={category.name}
                            component='img'
                        />

                        <CardContent>
                            <Typography gutterBottom variant="h4" color='black' component="div">
                                {category.name}
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
