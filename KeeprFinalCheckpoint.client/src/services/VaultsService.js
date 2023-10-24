import { AppState } from "../AppState.js"
import { Keep } from "../models/Keep.js"
import { Vault } from "../models/Vault.js"
import { logger } from "../utils/Logger.js"
import { api } from "./AxiosService.js"


class VaultsService{

  async createVault(vaultData){
    logger.log('here is our vault data for create:', vaultData)
    const res = await api.post('api/vaults', vaultData)
    AppState.activeVaults.push(new Vault(res.data))
    logger.log('created a new vault with the following data:', res.data)
  }

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

  async deleteVault(vaultId) {
    const res = await api.delete(`api/vaults/${vaultId}`)
    logger.log('deleted the following vault:', res.data)
    const indexToRemove = AppState.activeVaults.findIndex(vault => vault.id == vaultId)
    AppState.activeVaults.splice(indexToRemove, 1)
  }
}

export const vaultsService = new VaultsService