import { getPlayLists } from "../../services/playlist";
import { useState, useEffect } from "react";
import { Helmet } from "react-helmet";
import { Grid, Typography, Stack, Button } from '@mui/material'
import PlayListCardItem from "../../components/PlayLists/PlayListCardItem";
import { NavLink } from "react-router-dom";

export default function PlayLists() {

    const [playlists, setPlaylists] = useState([]);
    useEffect(() => {
        getPlayLists()
         .then(res => {
                if (res.ok && res.status === 200) {
                    return res.json();
                }
            })
         .then(data =>{
            setPlaylists(data.data)
        });
    }, []);
    return (
        <>
            <Helmet>
                <title>Playlists</title>
            </Helmet>
            <Stack sx={{
                marginTop:5
            }} >
                <Typography variant="h3" marginY={3} color="initial">Oynatma listeleri</Typography>
                <Grid container justifyContent='end' className="mb-5">
                    <Button variant="contained" color="primary" component={NavLink} to={`/playlists/create`}> Ekle</Button>
                </Grid>    
                <Grid direction='row' container >
                    {playlists.map((playlist) => (
                        <PlayListCardItem key={playlist.id} playlist={playlist}/>
                    ))}
                </Grid>
            </Stack>
        </>
    )
}