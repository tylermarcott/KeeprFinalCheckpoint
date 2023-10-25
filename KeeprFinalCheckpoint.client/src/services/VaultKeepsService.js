import { logger } from "../utils/Logger.js"
import { api } from "./AxiosService.js"



class VaultKeepsService{
  async createVaultKeep(vaultKeepData){
    const res = await api.post('api/vaultKeeps', vaultKeepData)
    logger.log('created the following vault keep:', res.data)
  }
}

export const vaultKeepsService = new VaultKeepsService