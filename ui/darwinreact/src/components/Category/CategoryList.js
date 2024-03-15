import { useEffect,useState } from "react"
import { getCategories } from "../../services/category";
import CategoryListItem from "./CategoryListItem";

export default function CategoryList(){

    const [categories, setCategories] = useState(false)

    useEffect(() => {
           getCategories()
            .then(res =>{         
                if(res.ok && res.status === 200){ 
                    return res.json()
                }
            }).then(data => {
                setCategories(data.data)
                console.log(data)
            })
            .catch((error) => console.log("Error:" + error));
    }, [])  



    return(
        <>
        <div className="container">
            <h1>Categories</h1>
            <div className="row">
                <div className="col-md-12">
                    {/* <ul>
                        {categories && categories.map((category, index) => (
                            <li key={index}>{category.name} - {category.id}</li>
                        ))} 
                    </ul> */}
                    <table>
                        <thead>
                            <tr>
                                <th>id</th>
                                <th>name</th>
                                <th>isUsable</th>
                                <th>image</th>
                            </tr>

                        </thead>
                        <tbody>
                        {categories && categories.map((category, index) =>  (
                            
                                    <CategoryListItem key={index} categoryItem={{id:category.id, name:category.name, isUsable:category.isUsable, imageUrl:category.imageUrl}} > </CategoryListItem>
                            
                            ))}
                        </tbody>
                    </table>  
                </div>
            </div>
        </div>
        </>
    )
}
