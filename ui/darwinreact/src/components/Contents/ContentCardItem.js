import { 
    Grid,
    Card,
    CardMedia,
    CardContent,
    CardActionArea,
    Typography} from '@mui/material';
    import {Link} from 'react-router-dom'

export default function ContentCardItem({content}){
    return (
          <Grid key={content.id} item xs={12} sm={6} md={4} lg={3}>
                <Card>
                    <CardActionArea to={`/contents/${content.id}`} component={Link}>
                        <CardMedia
                            sx={{ height: 256 }}
                            image={content.imageUrl}
                            title={content.name}
                        />

                        <CardContent>
                            <Typography gutterBottom variant="h5" color='black' component="div">
                                {content.name}
                            </Typography>
                            <Typography variant="body1" color="text.secondary">
                                {content.lyrics}
                            </Typography>
                        </CardContent>
                    </CardActionArea>
                </Card>
            </Grid>
    )
}