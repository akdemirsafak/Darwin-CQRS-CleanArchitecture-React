import { useState } from "react";
import {createPlaylist } from "../../services/playlist";
import 'bootstrap/dist/css/bootstrap.min.css';


export default function CreatePlayList() {
 const [name,setName]=useState('')
 const [description,setDescription]=useState('')
 const [isPublic,setIsPublic]=useState(true)
 const [isUsable,setIsUsable]=useState(true)


const createNewPlaylist=(data)=>{
        createPlaylist(data).then(res =>{
            if(res.ok && res.status === 201)
            { 
                return res.json()
            }
        }).then(data=>console.log(data.data))
        .catch((err)=>console.log(err))
    }


function handleSubmit(e){
    e.preventDefault();
    createNewPlaylist({name, description, isPublic, isUsable})
}

 return (
    <>
        <h3>Oynatma Listesi Oluştur</h3>
        <form onSubmit={handleSubmit}>
            <div className="mb-3">
                <label className="form-label">Oynatma listesi adı</label>
                <input type="text" className="form-control" onChange={e=>setName(e.target.value)} value={name} placeholder="Oynatma listesinin adını buraya yazınız.."/>

            </div>
            <div className="mb-3">
                <label className="form-label">Açıklama</label>
                <textarea type="text" className="form-control" value={description} onChange={e=>setDescription(e.target.value)} placeholder="Açıklama" ></textarea>
            </div>
            <div className="mb-3 form-check align-items-center d-block">
                <label className="form-check-label" >Herkese açık mı</label>
                <input type="checkbox" className="form-check-input"checked={isPublic} onChange={e=>setIsPublic(e.target.value)}/>
            </div>
            <button type="submit" className="btn btn-primary">Oluştur</button>
        </form>
    </>
 )
    
}