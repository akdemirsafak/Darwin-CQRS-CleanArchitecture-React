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
    fetch(`${BASE_URL}/category/list`,
    {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body:  paginationData
    })

}

export const getCategoryDetail= (id) =>{
    fetch(`${BASE_URL}/category/${id}`,
    {
        method: 'GET',
        headers: {
            'Content-Type': 'application/json'
        }
    })
}
export const newCategory =(data,token) =>{
    return fetch(`${BASE_URL}/category`,
     {
        method: 'POST',
        headers: {
            //"Content-Type": "application/json",
            //'Content-Type': 'multipart/form-data',
             Authorization : `Bearer ${token}`
         },
         body:data
     })
}
export const updateCategory=(id,data,token) =>{
   fetch(`${BASE_URL}/category/${id}`,{
    method: 'PUT',
    headers: {
        "Content-Type": "application/json",
        Authorization:"Bearer "+ token 
    },
    body:data
   })

}

export const deleteCategory=(id,token) =>{
    return fetch(`${BASE_URL}/category/${id}`,{
        method:'DELETE',
        headers:{Authorization:`Bearer ${token}`}
    })
}