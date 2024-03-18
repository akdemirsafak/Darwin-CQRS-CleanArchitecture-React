import { useEffect,useState } from "react"
import { Helmet } from "react-helmet";
import { getMoods } from "../../services/mood";

export default function Moods(){

    const [moods, setMoods] = useState(false)

    useEffect(() => {
           getMoods()
            .then(res =>{         
                if(res.ok && res.status === 200){ 
                    return res.json()
                }
            })
            .then(data => {
                setMoods(data.data)
            })
            .catch((error) => console.log("Error:" + error));
    }, [])  
    return(
        <>
        <Helmet>
            <title> Ruh hali </title>
        </Helmet>
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
