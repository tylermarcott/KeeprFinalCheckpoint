import { AppState } from "../AppState.js"
import { Keep } from "../models/Keep.js"
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
    AppState.activeKeeps = res.data.map(keep => new Keep(keep))
    logger.log('we have the following keeps in our vault now:', AppState.activeKeeps)
  }
}

export const vaultsService = new VaultsService