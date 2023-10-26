import { AppState } from "../AppState.js";
import { Keep } from "../models/Keep.js";
import { logger } from "../utils/Logger.js";
import { api } from "./AxiosService.js";


class KeepsService{

  async createKeep(data){
    const res = await api.post('api/keeps', data)
    logger.log('just created the following keep:', res.data)
    AppState.keeps.push(new Keep(res.data))
    // FIXME: throws a backdrop error for modal once the form is submitted
  }

  async getKeeps(){
    const res = await api.get('api/keeps')
    AppState.keeps = res.data.map(keep => new Keep(keep))
    logger.log('got the following keeps in the appstate:', AppState.keeps)
  }

  async getKeepById(keepId){
    const res = await api.get(`api/keeps/${keepId}`)
    return res.data
  }

  async setActiveKeep(keepId){
    const foundKeep = await this.getKeepById(keepId)
    AppState.activeKeep = foundKeep
    logger.log('set the following keep as the active keep:', AppState.activeKeep)
    return AppState.activeKeep
  }

  async deleteKeep(keepId){
    const res = await api.delete(`api/keeps/${keepId}`)
    logger.log('deleted the following keep:', res.data)
    const indexToRemove = AppState.activeKeeps.findIndex(keep => keep.id == keepId)
    AppState.activeKeeps.splice(indexToRemove, 1)
  }
}

export const keepsService = new KeepsService;