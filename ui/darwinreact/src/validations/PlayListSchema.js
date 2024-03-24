import Yup from './validation';


export const PlayListSchema=Yup.object().shape({
    name:Yup.string()
        .required()
        .min(3)
        .max(64),
     
    description:Yup.string()
        .max(128),

    isPublic:Yup.bool()
        .required()
})