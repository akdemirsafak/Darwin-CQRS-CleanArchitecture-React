import Yup from './validation'

export const LoginSchema=Yup.object().shape({

    email:Yup.string()
        .email()
        .required()
        .min(6),
    password:Yup.string().required().min(6).max(32)
})