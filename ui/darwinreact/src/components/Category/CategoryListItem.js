import {
    Card,
    Grid,
    CardContent,
    Typography,
    CardMedia,
    CardActionArea
} from '@mui/material';
import { Link } from 'react-router-dom';

export default function CategoryListItem({category}){
    return(
              <Grid key={category.id} item xs={12} sm={6} md={4} lg={3}>
                <Card >
                    <CardActionArea to={`/categories/${category.id}`} component={Link}>
                    <CardMedia
                        sx={{ height: 192 }}
                        image={category.imageUrl}
                        title={category.name}
                    />

                    <CardContent>
                        <Typography gutterBottom variant="h5" color='black' component="div">
                            {category.name}
                        </Typography>
                        <Typography variant="body1" color="text.secondary">
                        {category.isUsable? ' Kullanılabilir ':' Kullanılamaz.'}
                        </Typography>
                    </CardContent>
                    </CardActionArea>
                </Card>
            </Grid>
    )
}
