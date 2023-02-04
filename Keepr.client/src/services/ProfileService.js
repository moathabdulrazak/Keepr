
import { AppState } from "../AppState.js"
import { api } from "./AxiosService.js"

class ProfileService {
  async getProfileById(id) {
    const res = await api.get(`api/profiles/${id}`)
    AppState.activeProfile = res.data
  }

  async getProfilesKeeps(id) {
    const res = await api.get(`api/profiles/${id}/keeps`);
    AppState.profilesKeeps = res.data;
    AppState.myAccountKeeps = res.data
  }

  async getProfilesVaults(id) {
    debugger
    const res = await api.get(`api/profiles/${id}/vaults`)
    AppState.profileVaults = res.data
  }
}



export const profileService = new ProfileService();