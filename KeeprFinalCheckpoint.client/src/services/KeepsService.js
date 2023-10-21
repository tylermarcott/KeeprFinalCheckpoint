import { AppState } from "../AppState.js";
import { Keep } from "../models/Keep.js";
import { logger } from "../utils/Logger.js";
import { api } from "./AxiosService.js";


class KeepsService{

  async getKeeps(){
    const res = await api.get('api/keeps')
    AppState.activeKeeps = res.data.map(keep => new Keep(keep))
  }

}

export const keepsService = new KeepsService;