import { logger } from "../utils/Logger.js"


class VaultsService{

  async setActiveVault(vaultId){
    logger.log('setting the active vault with the following id:', vaultId)
    // TODO: next, create getVaultById, call that here, then build out the vault page with the active vault. Or maybe it's not a vault page, it's a modal? have to double check.
  }
}

export const vaultsService = new VaultsService