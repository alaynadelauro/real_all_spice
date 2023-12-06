import { AppState } from '../AppState'
import { Account } from '../models/Account.js'
import { Favorite } from '../models/Favorite'
import { logger } from '../utils/Logger'
import { api } from './AxiosService'
import { favoritesService } from './FavoritesService'

class AccountService {
  async getAccount() {
    try {
      const res = await api.get('/account')
      AppState.account = new Account(res.data)
      // debugger
      // const favorites = await favoritesService.getFavorites()
      // logger.log(favorites)
      // AppState.favorites = favorites.data.map((favorite) => new Favorite(favorite))
    } catch (err) {
      logger.error('HAVE YOU STARTED YOUR SERVER YET???', err)
    }
  }
  async editAccount(accountId, accountData) {
    try {
      const res = await api.put(`account/${accountId}`, accountData)
      AppState.account = new Account(res.data)
    } catch (error) {
      logger.error(error)
    }
  }
}

export const accountService = new AccountService()
