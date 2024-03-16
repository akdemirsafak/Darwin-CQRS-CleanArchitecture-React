import { useEffect, useState } from "react"
import { getCategoryDetail } from "../../services/category"

export default function CategoryDetail(id){

const[category,setCategory]=useState({});

useEffect(()=>{
    getCategoryDetail(id).then((result) => {
        if(result.ok && result.status === 200){
            return result.json()
        }
        })
        .then(data=> setCategory(data.data))
        .catch((err) => {
            console.log(err)
        });
    },[])
    
    return(
        <>
        <div className='container'>
            <h1>Category Detail</h1>
        <div className='row'>
            <div className='col-md-12'>
                <div className='card'>
                    <div className='card-body'>
                        <img className='card-img-top' src={category.imageUrl} alt='Card image cap' />
                        <h5 className='card-title'>{category.name}</h5>
                        
                        <p>Bu kategori {category.isUsable? 'kullanılabilir':'kullanılamaz.'}</p>
                        <div className="card-footer">
                            <button className="btn btn-warning">Güncelle</button>
                            <button className="btn btn-danger">Sil</button>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        </div>
        </>
    )
  
}
