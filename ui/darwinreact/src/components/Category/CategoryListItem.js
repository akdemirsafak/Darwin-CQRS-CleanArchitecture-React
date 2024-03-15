
export default function CategoryListItem(props){
    return(

            <tr>
                <td>{props.categoryItem.id}</td>
                <td>{props.categoryItem.name}</td>
                <td>{props.categoryItem.isUsable}</td>
                <td><img src={props.categoryItem.imageUrl} className="" width={128}/></td>
                <td><button className="btn btn-outline-warning">Update</button></td>
                <td><button className="btn btn-outline-danger">Delete</button></td>
            </tr>
       /* { <div className='container'>
            <p>{props.categoryItem.id}</p>
            <h5>Adı {props.categoryItem.name}</h5>
            <p>{props.categoryItem.isUsable}</p>
            <img src={props.categoryItem.imageUrl} className="img img-thumbnail" width={128} height={128}/>
        </div> }*/
    )
}
