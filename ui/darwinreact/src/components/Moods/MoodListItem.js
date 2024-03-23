import {
    Card,
    Grid,
    CardContent,
    Typography,
    CardMedia,
    CardActionArea
} from '@mui/material';
export default function MoodListItem({mood}){
    return(
            <Grid key={mood.id} item xs={12} sm={6} md={4} lg={3}>
                <Card >
                    <CardActionArea>
                    <CardMedia
                        sx={{ height: 256 }}
                        image={mood.imageUrl}
                        title={mood.name}
                    />
                    <CardContent>
                        <Typography gutterBottom variant="h5" color='black' component="div">
                            {mood.name}
                        </Typography>
                        <Typography variant="body1" color="text.secondary">
                        {mood.isUsable? ' Kullanılabilir ':' Kullanılamaz.'}
                        </Typography>
                    </CardContent>
                    </CardActionArea>
                </Card>
            </Grid>
    )
}