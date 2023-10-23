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
    return AppState.activeProfile
  }

  async getProfileKeeps(profileId){
    const res = await api.get(`api/profiles/${profileId}/keeps`)
    AppState.activeKeeps = res.data
  }

  async getProfileVaults(profileId) {
    const res = await api.get(`api/profiles/${profileId}/vaults`)
    AppState.activeVaults = res.data
  }


}

export const profilesService = new ProfilesService