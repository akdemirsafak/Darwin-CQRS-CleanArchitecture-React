const BASE_URL= process.env.REACT_APP_API_URL;
//const BASE_URL=window.REACT_APP_API_URL;
//const BASE_URL= 'https://localhost:7116'
export const getCategories=()=>{
    return fetch(`${BASE_URL}/category`,
    {
        method: 'GET',
        headers: {
            'Content-Type': 'application/json'
        }
    })

}
export const getCategoryList=(paginationData)=>{
    return fetch(`${BASE_URL}/category/list`,
    {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body:  paginationData
    })

}

export const getCategoryDetail= (id) =>{
    return fetch(`${BASE_URL}/category/${id}`,
    {
        method: 'GET',
        headers: {
            'Content-Type': 'application/json'
        }
    })
}
export const newCategory =(data) =>{
    return fetch(`${BASE_URL}/category`,
     {
        method: 'POST',
        headers: {
            //"Content-Type": "application/json",
            //'Content-Type': 'multipart/form-data',
             Authorization : `Bearer ${localStorage.getItem('token')}`
         },
         body:data
     })
}
export const updateCategory=(id,data) =>{
   return fetch(`${BASE_URL}/category/${id}`,{
    method: 'PUT',
    headers: {
        "Content-Type": "application/json",
        Authorization:`Bearer ${localStorage.getItem('token')}` 
    },
    body:data
   })
}

export const deleteCategory=(id) =>{
    return fetch(`${BASE_URL}/category/${id}`,{
        method:'DELETE',
        headers:{
            Authorization:`Bearer ${localStorage.getItem('token')}`
        }
    })
}