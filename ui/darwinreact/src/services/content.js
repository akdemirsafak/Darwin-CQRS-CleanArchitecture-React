const BASE_URL=process.env.REACT_APP_API_URL;

export const getContents=()=>{
    return fetch(`${BASE_URL}/content`,
    {
        method: 'GET',
        headers: {
            'Content-Type': 'application/json'
        }
    })
}

export const getContentById=(id)=>{
    return fetch(`${BASE_URL}/content/${id}`,
    {
        method: 'GET',
        headers: {
            'Content-Type': 'application/json'
        }
    })
}

export const getContentList = (paginationData)=>{
    return fetch(`${BASE_URL}/content/list`,
    {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body:  paginationData
    })
}

export const searchContents = (searchText) => {
    return fetch(`${BASE_URL}/content/search/${searchText}`,
    {
        method: 'GET',
        headers: {
            'Content-Type': 'application/json'
        }
    })
}

export const fullTextSearch = (searchText) => {
    return fetch(`${BASE_URL}/content/fulltextsearch/${searchText}`,
    {
        method: 'GET',
        headers: {
            'Content-Type': 'application/json'
        }
    })
}

export const createContent = (content) => {
    return fetch(`${BASE_URL}/content`,
    {
        method:'POST', 
        headers: {
            // 'Content-Type': 'application/json',
            Authorization: `Bearer ${localStorage.getItem('token')}`
        },
        body: content
    })
}

export const updateContent = (id,content) => {
    return fetch(`${BASE_URL}/content/${id}`,{
        method:"PUT",
        headers:{
            Authorization:`Bearer ${localStorage.getItem("token")}`,
            "Content-type":"application/json"
        },
        body:JSON.stringify(content)
    })
}

export const deleteContent = (id) => {
    return fetch(`${BASE_URL}/content/${id}`,
    {
        method:"DELETE",
        headers:{
            'Content-Type':'application/json',
            Authorization:`Bearer ${localStorage.getItem("token")}`
        }
    })
}