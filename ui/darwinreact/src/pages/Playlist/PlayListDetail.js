import { useState, useEffect } from "react";
import { useParams} from "react-router-dom";
import { getPlaylistById } from "../../services/playlist";
import {Grid,Card,CardContent,CardActions,Button,Typography} from '@mui/material';
import {Link} from "react-router-dom";
export default function PlayListDetail() 
{
    const {id} = useParams();
    const [playlist, setPlaylist] = useState({});
    
    useEffect(()=>{
        getPlaylistById(id)
        .then((res)=>{
            if(res.ok && res.status === 200){
                return res.json()
            }
        })
        .then(data=>{
            setPlaylist(data.data)
        })
        .catch((err)=>console.log(err))
    },[]);
    return(
        <>
            <Grid key={playlist.id} item sx={{marginTop:10}}  >
                    <Card className="col-4 offset-4" >
                        <CardContent>
                            <Typography gutterBottom variant="h4" color='black' component="div">
                                {playlist.name}
                            </Typography>
                            <Typography  gutterBottom variant="body1" marginY={3} color='black' component="div">
                                {playlist.description}
                            </Typography>
                          <Typography>
                                İçerikler : {
                                    playlist.contents && playlist.contents.map((content) => <div className="btn btn-sm text-primary mx-2">{content.name}</div>) 
                                    }
                            </Typography>
                        </CardContent>

                        <CardActions>
                            <Button variant="contained" color="error" >
                                    Sil
                                </Button>
                                <Button variant="contained" color="warning" to={`/playlist/update/${playlist.id}`} component={Link}>
                                    Güncelle
                                </Button>
                        </CardActions>
                    </Card>
            </Grid>
        </>
    )
}