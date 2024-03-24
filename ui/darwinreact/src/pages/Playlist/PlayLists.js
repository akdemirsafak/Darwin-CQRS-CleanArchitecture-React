import { getPlayLists } from "../../services/playlist";
import { useState, useEffect } from "react";
import { Helmet } from "react-helmet";
import {  Grid, Typography, Stack  } from '@mui/material'
import PlayListCardItem from "../../components/PlayLists/PlayListCardItem";

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
                <Grid>
                    <Typography variant="h4" color="initial" sx={{
                        marginX:4,
                        fontWeight:500
                    }}>
                        İçerik Listeleri
                    </Typography>
                </Grid>
                    <Grid direction='row' container >
                        {playlists.map(playlist => (
                            <PlayListCardItem key={playlist.id} playlist={playlist}/>
                        ))}
                    </Grid>
            </Stack>
        </>
    )
}