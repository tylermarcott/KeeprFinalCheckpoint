import { AppState } from "../AppState.js"
import { logger } from "../utils/Logger.js"
import { api } from "./AxiosService.js"


class VaultsService{

  async setActiveVault(vaultId){
    this.getVaultById(vaultId)
  }

  async getVaultById(vaultId){
    const res = await api.get(`api/vaults/${vaultId}`)
    AppState.activeVault = res.data
    logger.log('we have the following vault as our active vault now:', AppState.activeVault)
  }

  async getKeepsInVault(vaultId){
    const res = await api.get(`api/vaults/${vaultId}/keeps`)
    logger.log('here are our keeps we got in our vault:', res.data)
  }
}

export const vaultsService = new VaultsService