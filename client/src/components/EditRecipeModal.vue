<template>
    <div class="modal fade modal-lg" id="editRecipeModal" tabindex="-1" aria-labelledby="editRecipeModalLabel" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div v-if="activeRecipe" class="active-recipe-img modal-header" :style="{ backgroundImage: `url(${activeRecipe.img})` }">
                    <h1 class="modal-title modal-title-personalized rounded-pill px-3" id="editRecipeModalLabel">Edit {{ activeRecipe.title }}</h1>
                    <button type="button" class="btn text-light fs-1" data-bs-dismiss="modal" aria-label="Close"><i class="mdi mdi-close"></i></button>
                </div>
                <div class="container-fluid modal-body">
                    <form class="row justify-content-center">
                        <div class="col-12">
                            <label for="title" class="form-label">Name:</label>
                            <input v-model="editable.title" type="text" maxlength="50" class="form-control" :placeholder="activeRecipe.title">
                        </div>
                        <div class="col-12">
                            <label for="category" class="form-label">Category:</label>
                            <select v-model="editable.category" class="form-select" aria-label="Default select example">
                                <option v-for="category in categories" :key="category" :value="category">{{ category }}</option>
                            </select>
                        </div>
                        <div>
                            <label for="img" class=" col-12 form-label">Link to Image:</label>
                            <input v-model="editable.img" type="text" maxlength="500" class="form-control" :placeholder="activeRecipe.img">
                        </div>
                        <div>
                            <label for="instructions" class="col-12 form-label">Instructions:</label>
                            <textarea v-model="editable.instructions" class="form-control" maxlength="1000" :placeholder="activeRecipe.instructions">{{ activeRecipe.instructions }}</textarea>
                        </div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-danger" @click="deleteRecipe(activeRecipe.id)" title="delete recipe"><i class="mdi mdi-delete"></i></button>
                            <button type="button" class="btn btn-success text-light" title="View Recipe" @click="changeModal()">Back</button>
                            <button type="button" class="btn btn-primary" @click="updateRecipe(activeRecipe.id)">Save changes</button>
                            <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Close</button>
                        </div>
                    </form>
                </div>
            </div>
        </div>
    </div>
</template>


<script>
import { Dropdown, Modal } from 'bootstrap';
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
            account: computed(() => AppState.account),
            activeRecipe: computed(() => AppState.activeRecipe),
            async deleteRecipe(recipeId) {
                try {
                    const yes = await Pop.confirm("Are you sure you want to delete your Recipe?")
                    if (!yes) { return }
                    await recipesService.deleteRecipe(recipeId)
                    Modal.getOrCreateInstance('#editRecipeModal').hide()
                    AppState.activeRecipe = {}
                } catch (error) {
                    Pop.error(error)
                    logger.error(error)
                }
            },
            changeModal() {
                Modal.getOrCreateInstance('#editRecipeModal').hide()
                Modal.getOrCreateInstance('#activeRecipeModal').show()
            },
            async updateRecipe(recipeId) {
                try {
                    const yes = await Pop.confirm("Save Changes and Update Recipe?")
                    if (!yes) { return }
                    const recipeData = editable.value
                    await recipesService.updateRecipe(recipeId, recipeData)
                    editable.value = {}
                    this.changeModal()
                } catch (error) {
                    Pop.error(error)
                    logger.error(error)
                }
            }
        };
    },
    components: { Dropdown }
};
</script>


<style lang="scss" scoped>
.active-recipe-img {
    background-position: center;
    background-size: cover;
    box-shadow: 0px 5px 4px blue;
}

.modal-title-personalized {
    background-color: rgba(255, 255, 255, 0.656);
    box-shadow: 0px 5px 4px blue;
}
</style>