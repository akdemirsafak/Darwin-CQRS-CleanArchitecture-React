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
    });
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
