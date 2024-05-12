import Yup from './validation';

export const ContentSchema=Yup.object().shape({
    name:Yup.string()
     .required()
     .min(3)
     .max(64),
    //  selectedCategories:Yup.array().of(Yup.string()),
    //  selectedMoods:Yup.array().of(Yup.string()), //NotNull number
     
    imageFile: Yup.mixed().required('required')
      .test('fileFormat', 'Sadece jpg, jpeg, png dosyaları yükleyebilirsiniz.', value => {
        if (value) {
          const supportedFormats = ['jpg','jpeg','png'];
          return supportedFormats.includes(value.name.split('.').pop());
        }
        return true;
      })
      .test('fileSize', 'Görsel boyutu 3mb dan küçük olmalıdır.', value => {
        if (value) {
          return value.size <= 3145728; //Byte cinsinden 3mb
        }
        return true;
      }),
})