const Base_Url= process.env.REACT_APP_API_URL;
export const getMoods=()=>{
    return fetch(`${Base_Url}/mood`,
    {
        method: 'GET',
        headers: {
            'Content-Type': 'application/json'
        }
    })
}
export const getMoodList=(paginationDatas)=>{
    fetch(`${Base_Url}/mood/list`,
    {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body:  paginationDatas
    })
}
export const newMood=(data,token)=>{
    return fetch(`${Base_Url}/mood`,
    {
        method:'post',
        headers: {
            //'Content-Type': 'application/json'
            Authorization:`Bearer ${token}`
        }, body:data
        
    })
}
export const updateMood=(id,data,token)=>{
    fetch(`${Base_Url}/mood/${id}`,{
        method: 'PUT',
        headers: {
            'Content-Type': 'application/json', //Şuan api'da görsel güncelleme olmadığı için content-type: application/json olarak belirlendi.
            Authorization:`Bearer ${token}`
        },
        body:data
    })
}