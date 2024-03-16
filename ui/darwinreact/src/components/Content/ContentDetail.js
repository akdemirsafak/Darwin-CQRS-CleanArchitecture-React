import { useEffect, useState } from "react";
import { getContentById } from "../../services/content";
// import {Card, 
//     Typography, 
//     CardMedia, 
//     CardContent, 
//     CardActions, 
//     Button,
//     CardActionArea } from '@mui/material'
// import DeleteIcon from '@mui/icons-material/Delete';
// import UpdateIcon from '@mui/icons-material/Update';

import 'bootstrap/dist/css/bootstrap.min.css';

export default function ContentDetail(id){

const [content,setContent] = useState({})
 
useEffect(()=>{
    getContentById(id)
        .then((result) => {
            if(result.ok && result.status === 200){
                return result.json()
            }
        })
        .then((data)=>{
                setContent(data.data)
        })
        .catch((err)=>console.error('Error:', err))
    },[])
     console.log(content.moods)
    return (
        <>
            <h2>Content Details</h2>
               <div className="card">
                <img className="card-img-top" src={content.imageUrl}/>
                <div className="card-body">
                    <h3 className="card-title">{content.name}</h3>
                    <p className="card-text">{content.lyrics}</p>
                    <hr/>
                    <strong className="card-text d-block">{content.isUsable ? "Kullanılabilir":"Kullanılamaz"}</strong>
                    <p>
                        Modlar : {
                            content.moods && content.moods.map((mood) => mood && mood.name).join(', ')
                            }
                    </p>
                        <p>
                        Kategoriler : {
                            content.categories && content.categories.map((category) => category && category.name).join(', ')
                            }
                    </p>

                    <div className="card-footer">
                        <a href="#" className="btn btn-warning btn-sm me-2">Update</a>
                        <a href="#" className="btn btn-danger btn-sm">Delete</a>
                    </div>
                </div>
            </div> 
            
       
        </>
    )
}