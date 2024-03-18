import { getPlayLists } from "../../services/playlist";
import { useState, useEffect } from "react";

export default function PlayLists() {
    const [playlists, setPlaylists] = useState([]);
    useEffect(() => {
        getPlayLists()
         .then(res => {
                if (res.ok && res.status === 200) {
                    return res.json();
                }
            })
         .then(data => setPlaylists(data.data));
    }, []);
    return (
        <div>
            <h1>İçerik listeleri</h1>
            <ul style={{ listStyleType: 'none' }}>
                {playlists.map(playlist => (
                    <li key={playlist.id}>{playlist.name} - {playlist.description} - { playlist.isPublic ?  "Bu playlist'i herkes görebilir." : "Bu playlist'i kişiye özeldir."} </li>
                ))}
            </ul>
        </div>
    )
}