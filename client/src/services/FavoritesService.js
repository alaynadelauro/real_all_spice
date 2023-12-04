import { AppState } from "../AppState"
import { Favorite } from "../models/Favorite"
import { logger } from "../utils/Logger"
import { api } from "./AxiosService"

class FavoritesService {
    async getFavorites() {
        try {
            // debugger
            const res = await api.get('account/favorites')
            // logger.log(res.data)
            AppState.favorites = res.data.map((favorite) => new Favorite(favorite))
            // logger.log(AppState.favorites)
        } catch (error) {
            logger.error(error)
        }
    }
    async unfavorite(favoriteId) {
        try {
            await api.delete(`api/favorites/${favoriteId}`)
            AppState.favorites = AppState.favorites.filter(favorite => favorite.favoriteId != favoriteId)
        } catch (error) {
            logger.error(error)
        }
    }
}

export const favoritesService = new FavoritesService()