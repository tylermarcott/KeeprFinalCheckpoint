import { AppState } from "../AppState.js";
import { Keep } from "../models/Keep.js";
import { logger } from "../utils/Logger.js";
import { api } from "./AxiosService.js";


class KeepsService{

  async getKeeps(){
    const res = await api.get('api/keeps')
    AppState.keeps = res.data.map(keep => new Keep(keep))
  }

  async getKeepById(keepId){
    const res = await api.get(`api/keeps/${keepId}`)
    return res.data
  }

  async setActiveKeep(keepId){
    const foundKeep = await this.getKeepById(keepId)
    AppState.activeKeep = foundKeep
  }

}

export const keepsService = new KeepsService;