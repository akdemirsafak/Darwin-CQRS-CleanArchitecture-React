import { useState, useEffect } from "react";
import { getContents } from "../../services/content";
import { Helmet } from "react-helmet";
import {  Grid  } from '@mui/material'
import ContentCardItem from "../../components/Contents/ContentCardItem";
import { useNavigate, useLocation } from "react-router-dom";

export default function Contents(){

    const navigate= useNavigate();
    const location = useLocation();

    const[contents,setContents]=useState(false)

    useEffect(()=>{
        getContents()
            .then((res)=>{
                if(res.ok && res.status === 200){
                    return res.json()
                }else if(res.status === 401){
                    console.log("missing.token")
                    localStorage.removeItem("token")
                    navigate(location?.state?.return_url || '/',{ replace: true })

                }
            }).then(data=>{ 
                setContents(data.data)
            })
            .catch((err)=>console.log(err))
    },[])

    return (
    <>
        <Helmet>
            <title> İçerikler </title>
        </Helmet>
            <div className="container mt-5">
            <Grid direction='row' container spacing={3}>
        {
            contents && contents.map((content, index) => (
                <ContentCardItem key={index} content={content} />
            ))
        }
            </Grid>
        </div>

    </>
    );
}