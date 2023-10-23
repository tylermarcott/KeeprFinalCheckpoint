import { AppState } from "../AppState.js"
import { logger } from "../utils/Logger.js"
import { api } from "./AxiosService.js"


class ProfilesService{

  async getProfileById(profileId){
    const res = await api.get(`api/profiles/${profileId}`)
    const foundProfile = res.data
    return foundProfile
  }
  async setActiveProfile(profileId){
    const foundProfile = await this.getProfileById(profileId)
    AppState.activeProfile = foundProfile
  }

  async getProfileKeeps(profileId){
    // logger.log('here is the profile id we get:', profileId)
  }

  async getProfileVaults(profileId) {
    // logger.log('here is the profile id we get:', profileId)
  }


}

export const profilesService = new ProfilesService