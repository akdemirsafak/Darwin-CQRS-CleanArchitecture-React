const BASE_URL= process.env.REACT_APP_API_URL;

export const login=(data)=>{
    return fetch(`${BASE_URL}/auth/login`,{
        method:'POST',
        headers:{
            "Content-Type": "application/json"
        },
        body:JSON.stringify(data)
    })}
export const register=(data)=>{
    return fetch(`${BASE_URL}/auth/register`,{
        method:'POST',
        headers:{
            "Content-Type": "application/json"
        },
        body:JSON.stringify(data)
    })
}