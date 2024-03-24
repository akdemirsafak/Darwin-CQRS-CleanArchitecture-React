import * as Yup from "yup";

Yup.setLocale({
    mixed:{
        required:'Bu alan boş olamaz'
        
    },
    string:{
        min: 'Bu alan en az ${min} karakter olmalıdır',
        max:'Bu alan en fazla ${max} karakter olmalıdır',
        email:'Geçerli bir e-posta adresi giriniz',
    }
})

export default Yup;