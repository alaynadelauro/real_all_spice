<template>
    <div class="modal fade modal-lg" id="editRecipeModal" tabindex="-1" aria-labelledby="editRecipeModalLabel" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="editRecipeModalLabel">Modal title</h5>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="container-fluid modal-body">
                    <form class="row justify-content-center">
                        <div class="col-12">
                            <label for="title" class="form-label">{{ activeRecipe.title }}:</label>
                            <input type="text" maxlength="255" class="form-control" :placeholder="activeRecipe.title">
                        </div>
                        <div class=" col-12 my-2 dropdown">
                            <button class="btn form-control text-start dropdown-toggle" type="button" id="dropdownMenuButton1" data-bs-toggle="dropdown" aria-expanded="false">
                                {{ activeRecipe.category }}
                            </button>
                            <ul class="dropdown-menu" aria-labelledby="dropdownMenuButton1">
                                <li>
                                    <p class="dropdown-item" type="button">Cheese</p>
                                </li>
                                <li>
                                    <p class="dropdown-item" type="button">Italian</p>
                                </li>
                                <li>
                                    <p class="dropdown-item" type="button">Soup</p>
                                </li>
                                <li>
                                    <p class="dropdown-item" type="button">Mexican</p>
                                </li>
                                <li>
                                    <p class="dropdown-item" type="button">Specialty Coffee</p>
                                </li>
                                <li>
                                    <p class="dropdown-item" type="button">Other</p>
                                </li>
                            </ul>
                        </div>
                        <div>
                            <label for="img" class=" col-12 form-label">Link to Image:</label>
                            <input type="text" class="form-control" :placeholder="activeRecipe.img">
                        </div>
                        <div>
                            <label for="instructions" class="col-12 form-label">Instructions:</label>
                            <textarea class="form-control" :placeholder="activeRecipe.instructions">{{ activeRecipe.instructions }}</textarea>
                        </div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-danger" @click="deleteRecipe(activeRecipe.id)" title="delete recipe"><i class="mdi mdi-delete"></i></button>
                            <button type="button" class="btn btn-success text-light" title="View Recipe" @click="changeModal()">Back</button>
                            <button type="button" class="btn btn-primary">Save changes</button>
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
import { computed, reactive, onMounted } from 'vue';
import Pop from '../utils/Pop';
import { logger } from '../utils/Logger';
import { recipesService } from '../services/RecipesService';
export default {
    setup() {
        return {
            account: computed(() => AppState.account),
            activeRecipe: computed(() => AppState.activeRecipe),
            async deleteRecipe(recipeId) {
                try {
                    const yes = await Pop.confirm("Are you sure you want to delete your Recipe?")
                    if (!yes) { return }
                    await recipesService.deleteRecipe(recipeId)
                    Modal.getInstance('#activeRecipeModal').hide()
                    AppState.activeRecipe = {}
                } catch (error) {
                    Pop.error(error)
                    logger.error(error)
                }
            },
            changeModal() {
                Modal.getOrCreateInstance('#editRecipeModal').hide()
                Modal.getOrCreateInstance('#activeRecipeModal').show()
            }
        };
    },
    components: { Dropdown }
};
</script>


<style lang="scss" scoped></style>