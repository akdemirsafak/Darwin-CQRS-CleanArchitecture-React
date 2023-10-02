<template>

<v-card color=""  class="w-25 mx-auto mt-5">
    <v-card-title class="text-center mb-5">Create Category</v-card-title>
    <!-- <v-card-subtitle><hr></v-card-subtitle> -->
    <v-card-item>
        <v-form>
            <v-text-field variant="underlined" label="Kategori adı"></v-text-field>
            <v-file-input label="Görsel seçmek için tıkla." variant="outlined" prepend-icon="mdi-camera"></v-file-input>
            <v-checkbox label=" IsUsable" 
            v-model="isUsable"></v-checkbox>
        </v-form>
    </v-card-item>
    <v-card-actions class="d-flex justify-end">
        <v-btn end variant="outlined" class="bg-blue-lighten-5 px-10">Ekle</v-btn>
    </v-card-actions>
</v-card>
</template>


<script>
export default{
data(){
    return{
        isUsable:false,

    }
}
}
</script>

  <!-- <v-sheet class="mx-auto mt-10" width="500" >
    
<h3>Yeni kategori ekle</h3>
    <v-form ref="form">
 
        <v-text-field
            v-model="name"    
            label="Name"
            required
            variant="outlined"
        ></v-text-field>

        <v-file-input
            label="File input"
            prepend-icon="mdi-camera"
            variant="outlined"
        ></v-file-input>
        <v-checkbox
            v-model="isUsable"
            label="IsUsable?"
        ></v-checkbox>
        <v-btn>Create</v-btn>
</v-form>
</v-sheet>
</template>

<script>
export default {
        data(){
            return{
                name:null,
                imageUrl:null,
                isUsable:false,
                ex4: ['success'],
            }
        },        
}
</script> -->