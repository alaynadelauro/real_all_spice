<template>
    <div class="modal fade modal-lg" id="createRecipeModal" tabindex="-1" aria-labelledby="createRecipeModalLabel" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h1 class="modal-title" id="editRecipeModalLabel">Add Your Very Own Recipe!</h1>
                    <button type="button" class="btn text-light fs-1" data-bs-dismiss="modal" aria-label="Close"><i class="mdi mdi-close"></i></button>
                </div>
                <div class="container-fluid modal-body">
                    <form class="row justify-content-center">
                        <div class="col-12">
                            <label for="title" class="form-label">Name:</label>
                            <input v-model="editable.title" type="text" maxlength="50" class="form-control">
                        </div>
                        <div class="col-12">
                            <label for="category" class="form-label">Category:</label>
                            <select v-model="editable.category" class="form-select" aria-label="Default select example">
                                <option v-for="category in categories" :key="category" :value="category">{{ category }}</option>
                            </select>
                        </div>
                        <div>
                            <label for="img" class="col-12 form-label">Link to Image:</label>
                            <input v-model="editable.img" type="text" maxlength="500" class="form-control">
                        </div>
                        <div>
                            <label for="instructions" class="col-12 form-label">Instructions:</label>
                            <textarea v-model="editable.instructions" class="form-control" maxlength="1000"></textarea>
                        </div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-primary" @click="createRecipe()">Create</button>
                            <button type="button" class="btn btn-secondary" @click="clearModal()">Cancel</button>
                        </div>
                    </form>
                </div>
            </div>
        </div>
    </div>
</template>


<script>
import { Modal } from 'bootstrap';
import { AppState } from '../AppState';
import { computed, reactive, onMounted, ref } from 'vue';
import Pop from '../utils/Pop';
import { logger } from '../utils/Logger';
import { recipesService } from '../services/RecipesService';
export default {
    setup() {
        const editable = ref({})
        return {
            editable,
            categories: ["All", "Cheese", "Italian", "Soup", "Mexican", "Specialty Coffee"],
            clearModal() {
                editable.value = {}
                Modal.getOrCreateInstance('#createRecipeModal').hide()
            },
            async createRecipe() {
                try {
                    // debugger
                    await recipesService.createRecipe(editable.value)
                    Modal.getOrCreateInstance('#createRecipeModal').hide()
                    Modal.getOrCreateInstance('#editRecipeModal').show()
                } catch (error) {
                    Pop.error(error)
                    logger.error(error)
                }
            }
        }
    }
};
</script>


<style lang="scss" scoped></style>