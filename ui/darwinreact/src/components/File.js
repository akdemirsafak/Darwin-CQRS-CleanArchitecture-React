import { FiCheck } from'react-icons/fi';
import { useField } from 'formik';


export default function File({label,...props}){
    const [field, meta, helpers]=useField(props);

    const changeHandle=(e)=>{
        helpers.setValue(e.target.files[0]);
    }
    
    async function fileOpen(e){
        
        try {
            const [fileHandle] = await window.showOpenFilePicker();
            const file=await fileHandle.getFile();
            helpers.setValue(file);
            console.log("File : ",file)
        } catch (error) {
            helpers.setValue('')
        }

    }


    return (
        <div className="block">
            <div className="block-header">{label} </div>
            
            <button type='button' onClick={fileOpen} className='btn btn-primary text-white' >
                {field.value && <>Dosya seçildi <FiCheck size={15}/><br/>
                </>}
                {!field.value && <>Dosya Seç </>}
            </button>
            {field.value && (
            <>
                <button onClick={()=> helpers.setValue('')} className='btn btn-sm btn-outline-danger'>Dosyayı kaldır</button>
                   <img width={64} height={64} src={URL.createObjectURL(field.value)} alt='selected file'/>
            </>
            )}
        </div>
    )
}