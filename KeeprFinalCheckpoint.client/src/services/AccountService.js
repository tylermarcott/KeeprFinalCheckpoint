import { AppState } from '../AppState'
import { Account } from '../models/Account.js'
import { Vault } from "../models/Vault.js"
import { logger } from '../utils/Logger'
import { api } from './AxiosService'

class AccountService {
  async getAccount() {
    try {
      const res = await api.get('/account')
      AppState.account = new Account(res.data)
    } catch (err) {
      logger.error('HAVE YOU STARTED YOUR SERVER YET???', err)
    }
  }

  async getMyVaults(){
    const res = await api.get('account/vaults')
    AppState.activeVaults = res.data.map(vault => new Vault(vault))
    logger.log('here are myVaults:', AppState.activeVaults)
  }

  // FIXME: this does not work because there is no backend supporting it. Make sure you add the backend or do what you need to do in order to be able to do the edit.
  async editAccount(accountData){
    logger.log('Editing account:', accountData)
    const res = await api.put('http://localhost:3000/account', accountData)
    AppState.account = new Account(res.data)
  }
}

export const accountService = new AccountService()
