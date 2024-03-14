import { useEffect,useState } from "react"
import { getCategories } from "../../services/category";

export default function CategoryList(){

    const [categories, setCategories] = useState(false)

    useEffect(() => {
           getCategories()
            .then(res =>{         
                if(res.ok && res.status === 200){ 
                    return res.json()
                }
            }).then(data => setCategories(data.data))
            .catch((error) => console.log("Error:" + error));
        console.log("CategoryList")
    }, [])  



    return(
        <>
        <div className="container">
            <h1>Categories</h1>
            <div className="row">
                <div className="col-md-12">
                    <ul>
                        {categories && categories.map((category, index) => (
                            <li key={index}>{category.name} - {category.id}</li>
                        ))} 
                    </ul>
                </div>
            </div>
        </div>
        </>
    )
}
