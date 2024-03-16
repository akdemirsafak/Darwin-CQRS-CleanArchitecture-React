const Base_Url = process.env.REACT_APP_API_URL;

export const getPlayLists = () => {
    return fetch(`${Base_Url}/playlist`,{
        method: 'GET',
        headers: {
            'Content-Type': 'application/json',
            Authorization : `Bearer ${localStorage.getItem('token')}`
        }
    })
}
export const getPlaylistById = (id) => {
    return fetch(`${Base_Url}/playlist/${id}`,{
        method: 'GET',
        headers: {
            'Content-Type': 'application/json',
            Authorization : `Bearer ${localStorage.getItem('token')}`
        }
    })
}
export const getMyPlayLists = () => {
    return fetch(`${Base_Url}/playlist/GetMyPlayLists`,{
        method: 'GET',
        headers: {
            'Content-Type': 'application/json',
            Authorization : `Bearer ${localStorage.getItem('token')}`
        }
    })
}
export const createPlaylist = (playlist) => {
    return fetch(`${Base_Url}/playlist`,{
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
            Authorization : `Bearer ${localStorage.getItem('token')}`
        },
        body: JSON.stringify(playlist)
    })
}
export const updatePlaylist = (id,playlist) => {
    return fetch(`${Base_Url}/playlist/${id}`,{
        method:'PUT',
        headers:{
            Authorization : `Bearer ${localStorage.getItem("token")}`,
            "Content-Type" : "application/json"
        },
        body: JSON.stringify(playlist)
    })
}
export const deletePlaylist = (id) => {
    return fetch(`${Base_Url}/playlist/${id}`,{
        method:'DELETE',
        headers:{
            Authorization : `Bearer ${localStorage.getItem("token")}`,
            "Content-Type" : "application/json"
        }
    })
}
export const addContentToPlaylist = (data) => {
    return fetch(`${Base_Url}/playlist/AddContentToPlaylist`,{
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
            Authorization : `Bearer ${localStorage.getItem('token')}`
        },
        body: JSON.stringify(data)
    })
}
export const removeContentFromPlaylist = (data) => {
    return fetch(`${Base_Url}/playlist/RemoveContentFromPlaylist`,{
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
            Authorization : `Bearer ${localStorage.getItem('token')}`
        },
        body: JSON.stringify(data)
    })
}

