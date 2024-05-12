import { useEffect,useState } from "react"
import { Helmet } from "react-helmet";
import { getMoods } from "../../services/mood";
import MoodListItem from "../../components/Moods/MoodListItem";
import {Grid, Typography,Button} from "@mui/material";
import { NavLink } from "react-router-dom";
export default function Moods(){

    const [moods, setMoods] = useState(false)

    useEffect(() => {
           getMoods()
            .then(res =>{         
                if(res.ok && res.status === 200){ 
                    return res.json()
                }
            })
            .then(data => {
                setMoods(data.data)
            })
            .catch((error) => console.log("Error:" + error));
    }, [])  
    return(
        <>
        <Helmet>
            <title> Ruh Hali </title>
        </Helmet>
             
            <Typography variant="h3" marginY={3} color="initial">Keşfet</Typography>
            <Grid container justifyContent='end' className="mb-5">
                <Button variant="contained" color="primary" component={NavLink} to={`/moods/create`}>Ekle</Button>
            </Grid>    

            <Grid direction='row' container spacing={3}>
                {moods && moods.map((mood, index) => (
                    <MoodListItem key={index} mood={mood}/>
                ))} 
            </Grid>
        </>
    )
}
