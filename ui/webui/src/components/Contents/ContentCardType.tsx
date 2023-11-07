import { Box, Card, CardMedia,CardContent } from "@mui/material"

export const ContentCardType=()=>{
    return(
       <Box width='16rem'>
            <Card>
                <CardMedia component='img' image="https://picsum.photos/200"  />
                {/* image={props.content.imgUrl} */}
                <CardContent>Title</CardContent>
            </Card>
       </Box>
    )
}