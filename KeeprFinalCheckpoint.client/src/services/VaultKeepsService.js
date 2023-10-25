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

  async deleteVaultKeep(){
    
  }
}

export const vaultKeepsService = new VaultKeepsService