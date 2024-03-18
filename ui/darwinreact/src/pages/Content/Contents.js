import { useState, useEffect } from "react";
import { getContents } from "../../services/content";
import { Helmet } from "react-helmet";

export default function Contents(){

    const[contents,setContents]=useState(false)

    useEffect(()=>{
        getContents()
            .then((res)=>{
                if(res.ok && res.status === 200){
                    return res.json()
                }
            }).then(data=>{ 
                setContents(data.data)
            })
            .catch((err)=>console.log(err))
    },[])

    return (<>
    
        <Helmet>
            <title> İçerikler </title>
        </Helmet>
        <div className="container">
            <h1>Content index</h1>

            {contents && contents.map((content,index)=>(
                <div key={index}>
                    <span>{content.id}</span>
                    <img src={content.imageUrl} alt="görsel"/>
                    <h2>{content.name}</h2>
                    <p>{content.lyrics}</p>
                    <p>{content.isUsable.toString()}</p>
                    
                </div>
            ))}
        </div>
        </>
    );
}