import { useState } from 'react';
import { newCategory } from '../../services/category';


export default function CreateCategory(){
    const addCategory = (data,token) => {
        const formData=new FormData();
 
        for(let [key,value] of Object.entries(data)){
            formData.append(key,value);
        }

      newCategory(formData,token).then(res =>{ 
        console.log(res)
        if(res.ok && res.status === 201){ 
            setName('');
            setImageFile(null);
            setIsUsable(false);
            console.log('oldu')
        }
       })
        .catch((err)=>{
            console.log(err.message)
        })
    
    }
    const[name,setName]= useState('');
    const [imageFile,setImageFile]= useState(null);
    const [isUsable,setIsUsable]= useState(false);



const submitHandle = e =>{
    console.log('submitHandle edildi');
    e.preventDefault();
    addCategory({
        name,
        imageFile,
        isUsable
    },"eyJhbGciOiJodHRwOi8vd3d3LnczLm9yZy8yMDAxLzA0L3htbGRzaWctbW9yZSNobWFjLXNoYTI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1laWRlbnRpZmllciI6IjY0N2Q1YzE2LThiODQtNDI2YS04ZWNmLTU1ZGZhZDIzZjdhZSIsImVtYWlsIjoiYWtkZW1pcnNhZmFrQGdtYWlsLmNvbSIsImp0aSI6IjViMzMzZGQxLWNiZTUtNDI5NC1iY2MzLTUyMjg2ZGVjODgwYiIsImF1ZCI6WyJodHRwczovL2xvY2FsaG9zdDo3MTE2IiwiaHR0cHM6Ly9sb2NhbGhvc3Q6NzExNyJdLCJodHRwOi8vc2NoZW1hcy5taWNyb3NvZnQuY29tL3dzLzIwMDgvMDYvaWRlbnRpdHkvY2xhaW1zL3JvbGUiOiJVc2VyIiwibmJmIjoxNzEwNDUyNDkxLCJleHAiOjE3MTA0NTQyOTEsImlzcyI6Imh0dHBzOi8vbG9jYWxob3N0OjcxMTYifQ._Khf5p53FyWsV0CgOAGfMaQyekq_KuwTs5FhS_rFTcw");
}
    return(
        <>
            <div className='container'>
                <form onSubmit={ submitHandle }>

                    <input type='text' 
                        className='form-control' 
                        value={name} 
                        name="name" 
                        onChange={e=>setName(e.target.value)} 
                        placeholder='Category Name'/><br/>
                    
                    <input className='form-check-input' 
                        type='checkbox' 
                        id='isUsable' 
                        name='isUsable' 
                        checked={isUsable} 
                        onChange={e=>setIsUsable(e.target.checked)}></input>
                    <label 
                        className='form-check-label' 
                        htmlFor='isUsable' 
                        name={isUsable}>IsUsable </label> <br/>
                    
                    <input 
                        type='file' 
                        className='form-control' 
                        name='imageFile' 
                        onChange={e=>setImageFile(e.target.files[0])}/><br/>
                    
                    <button type='submit' className='btn btn-primary' disabled={!name || !imageFile}> Kaydet </button>
                </form>
            </div>
        </>
    )
}
