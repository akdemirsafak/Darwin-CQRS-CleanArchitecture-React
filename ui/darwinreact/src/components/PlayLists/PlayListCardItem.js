import { 
    Grid,
    Card,
    CardActionArea,
    CardContent,
    Typography } from '@mui/material';
import { Link } from'react-router-dom'
import FavoriteIcon from '@mui/icons-material/Favorite';
import IconButton from '@mui/material/IconButton';

export default function PlayListCardItem({playlist}){

return(

    <Grid key={playlist.id} item  md={4} lg={4} sm={6} xs= {12}>
        <Card sx={{ margin:1}}>
            <CardActionArea to={`/playlists/${playlist.id}`} component={Link}>
                <CardContent>
                    <Typography gutterBottom variant="h5" color='black' component="div">
                        {playlist.name}  <small className='text-danger'>{playlist.isFavorite? 
                        <> <IconButton aria-label="add to favorites" >
                                <FavoriteIcon color='error' />
                            </IconButton>
                        </> : ''}</small>
                    </Typography>
                    <div>
                        {playlist.description}
                    </div>
                </CardContent>
            </CardActionArea>
        </Card>
    </Grid>
)}