import { AppState } from "../AppState.js"
import { logger } from "../utils/Logger.js"
import { api } from "./AxiosService.js"


class ProfilesService{

  getActiveProfile(){
    const activeProfile = AppState.activeProfile
    return  activeProfile
  }

  async getProfileById(profileId){
    const res = await api.get(`api/profiles/${profileId}`)
    const foundProfile = res.data
    AppState.activeProfile = foundProfile
    logger.log('we have the following active profile now in appstate:', AppState.activeProfile)
    return foundProfile
  }
  // async setActiveProfile(profileId){
  //   const foundProfile = await this.getProfileById(profileId)
  //   AppState.activeProfile = foundProfile
  //   logger.log('got the following active profile in the appstate:', AppState.activeProfile)
  //   return AppState.activeProfile
  // }

  async getProfileKeeps(profileId){
    const res = await api.get(`api/profiles/${profileId}/keeps`)
    AppState.activeKeeps = res.data
    logger.log('the following active keeps are in the appstate now:', AppState.activeKeeps)
  }

  async getProfileVaults(profileId) {
    const res = await api.get(`api/profiles/${profileId}/vaults`)
    AppState.activeVaults = res.data
    logger.log('the following active vaults are in the appstate now:', AppState.activeVaults)
  }


}

export const profilesService = new ProfilesService