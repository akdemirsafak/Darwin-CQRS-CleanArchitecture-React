import Button from '@mui/material/Button';
import DeleteIcon from '@mui/icons-material/Delete';
import EditIcon from '@mui/icons-material/Edit';
export default function CategoryListItem(props){
    return(
            <tr>
                <td>{props.categoryItem.id}</td>
                <td>{props.categoryItem.name}</td>
                <td>{props.categoryItem.isUsable.toString()}</td>
                <td><img src={props.categoryItem.imageUrl} className="" width={64} height={48}/></td>
                <td><Button variant='outlined'  size='small' color='inherit' startIcon={<EditIcon/>}>Update</Button></td>
                <td><Button variant='outlined' color='error' size='small' startIcon={<DeleteIcon />}>Delete</Button></td>
            </tr>
    )
}
