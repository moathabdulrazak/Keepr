import { api } from "./AxiosService.js";
import { AppState } from "../AppState.js";

class KeepsService {



  async getKeeps() {
    const res = await api.get('api/keeps')
    AppState.keeps = res.data
  }


  async createKeep(body) {
    const res = await api.post('api/keeps', body)
    AppState.keeps.push(res.data)
  }

  async deleteKeep(keepId) {
    await api.delete(`api/keeps/${keepId}`);
    let keep = AppState.keeps.findIndex((k) => k.id == keepId);
    AppState.keeps.splice(keep, 1);
  }

  async setActiveKeep(keepId) {
    const res = await api.get(`api/keeps/${keepId}`)
    AppState.activeKeep = res.data
    if (AppState.activeKeep.creatorId != AppState.account.id) {
      AppState.activeKeep.viewCount++
    }
  }
}


export const keepsService = new KeepsService();