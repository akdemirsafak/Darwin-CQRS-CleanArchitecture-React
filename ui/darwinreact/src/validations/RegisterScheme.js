import Yup from './validation'

export const RegisterScheme=Yup.object().shape({

    email:Yup.string()
        .email()
        .required(),
    password:Yup.string()
        .required()
        .min(6).max(32),
        confirmPassword:Yup.string()
        .required()
        .oneOf([Yup.ref('password'), null], 'Şifreler uyuşmuyor.'),
})