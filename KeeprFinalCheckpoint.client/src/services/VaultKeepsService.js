import { AppState } from "../AppState.js"
import { VaultKeep } from "../models/Keep.js"
import { logger } from "../utils/Logger.js"
import { api } from "./AxiosService.js"



class VaultKeepsService{
  async createVaultKeep(vaultKeepData){
    const res = await api.post('api/vaultKeeps', vaultKeepData)
    logger.log('created the following vault keep:', res.data)
    AppState.vaultKeeps.push(new VaultKeep(res.data))
    logger.log('here are our list of vaultKeeps:', AppState.vaultKeeps)
  }

  async deleteVaultKeep(vaultKeepId){
    logger.log('here is our id of our vaultKeep to delete in our service:', vaultKeepId)
    const res = await api.delete(`api/vaultKeeps/${vaultKeepId}`)
    logger.log('deleted the following vaultKeep:', res.data)
    const indexToRemove = AppState.vaultKeeps.findIndex(vaultKeep => vaultKeep.id == vaultKeepId)
    AppState.vaultKeeps.splice(indexToRemove, 1)
  }
}

export const vaultKeepsService = new VaultKeepsService