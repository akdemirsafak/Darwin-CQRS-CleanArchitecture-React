import { useEffect,useState } from "react"
import { Helmet } from "react-helmet";
import { getCategories } from "../../services/category";
import CategoryListItem from "../../components/Category/CategoryListItem";

export default function Categories(){

    const [categories, setCategories] = useState(false)

    useEffect(() => {
           getCategories()
            .then(res =>{         
                if(res.ok && res.status === 200){ 
                    return res.json()
                }
            }).then(data => {
                setCategories(data.data)
            })
            .catch((error) => console.log("Error:" + error));
    }, [])  



    return(
        <>
            <Helmet>
                <title> Kategoriler</title>
            </Helmet>


            <div className="container">
                <h1>Categories</h1>
                <div className="row">
                    <div className="col-md-12">
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
