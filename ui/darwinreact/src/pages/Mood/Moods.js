import { useEffect,useState } from "react"
import { getMoods } from "../../services/mood";

export default function Moods(){

    const [moods, setMoods] = useState(false)

    useEffect(() => {
           getMoods()
            .then(res =>{         
                console.log("Res : ",res)
                if(res.ok && res.status === 200){ 
                    return res.json()
                }
            })
            .then(data => {
                console.log("Data:" ,data)
                setMoods(data.data)
            })
            .catch((error) => console.log("Error:" + error));
    }, [])  
    console.log(moods)
    return(
        <>
        <div className="container">
            <h1>Moods</h1>
            <div className="row">
                <div className="col-md-12">
                    <ul>
                        {moods && moods.map((mood, index) => (
                            <li key={index}>{mood.name} - {mood.id}- {mood.isUsable.toString()}</li>
                        ))} 
                    </ul>
                   
                </div>
            </div>
        </div>
        </>
    )
}
