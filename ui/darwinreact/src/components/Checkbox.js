import { useField } from "formik";
import { FiCheck } from "react-icons/fi";
import classNames from "classnames";

export default function Checkbox({ label,...props })
{
    const [field, meta, helpers] =useField(props);

    return(
        <label className="d-flex align-items-center align-content-between">
            <button onClick={
                ()=>
                {
                    helpers.setValue(!field.value)
                }
            }
            className={
                classNames(
                    {
                    "btn btn-sm border text-bold rounded-left bordered d-flex align-items-center justify-content-center m-2" : true,
                    'border-secondary':!field.value,
                    'text-white bg-primary ': field.value
                    }
                )
            }
            type="button"
            >
            <FiCheck size={14} className={classNames({
                'text-white': field.value,
                'opacity-0':!field.value
            })}/>
            </button>
             {label}
        </label>
    )
}