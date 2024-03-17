import { useEffect,useState } from "react"
import { getMoods } from "../../services/mood";

export default function MoodList(){

    const [moods, setMoods] = useState(false)

    useEffect(() => {
           getMoods()
            .then(res =>{         
                if(res.ok && res.status === 200){ 
                    return res.json()
                }
                console.log("res",res)
            })
            .then(data => {
                setMoods(data.data)
                console.log(data)
            })
            .catch((error) => console.log("Error:" + error));
    }, [])  

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
