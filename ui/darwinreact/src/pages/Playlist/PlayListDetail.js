import { useState, useEffect } from "react";
import { useParams} from "react-router-dom";
import { getPlaylistById } from "../../services/playlist";
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
    console.log(playlist.contents)
    return(
        <div>
            <h1>{playlist.name}</h1>
            <p>Description: {playlist.description}</p>
            <p>{playlist.isUsable ? "Kullanılabilir" : "Kullanılamaz"}</p>
            <p>{playlist.isPublic ? "Herkese açık" : "Kullanıcıya özel."}</p>
            <p>
            İçerikler : {
                playlist.contents && playlist.contents.map((content) => content && content.name).join(', ')
                }
            </p>

        </div>
    )
}