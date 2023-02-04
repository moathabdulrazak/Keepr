import { AppState } from '../AppState'
import { Account } from "../models/Account.js"
import { logger } from '../utils/Logger'
import { api } from './AxiosService'

class AccountService {
  async getAccount() {
    try {
      const res = await api.get('/account')
      AppState.account = res.data
    } catch (err) {
      logger.error('HAVE YOU STARTED YOUR SERVER YET???', err)
    }
  }

  async getMyKeeps() {
    const res = await api.get('/account/keeps')
    console.log(res.data);
    AppState.myAccountKeeps = res.data
  }
  async getMyVaults() {
    const res = await api.get('/account/vaults')
    AppState.myAccountVaults = res.data
  }
  async editAccount(accountData) {
    const res = await api.put('/account', accountData)
    AppState.account = res.data
  }
}

export const accountService = new AccountService()
