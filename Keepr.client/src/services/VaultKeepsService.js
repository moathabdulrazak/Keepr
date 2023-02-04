import { api } from './AxiosService'
import { AppState } from '../AppState'
import { logger } from "../utils/Logger.js"
class VaultKeepsService {

  async createVaultKeep(body) {
    const res = await api.post('api/vaultKeeps', body)
    AppState.vaultKeeps.push(res.data)
    // AppState.vaultKeeps = [res.data, ...AppState.vaultKeeps]
    return res.data
  }

  async removeFromVault(id) {
    const res = await api.delete(`api/vaultKeeps/${id}`);
    AppState.profilesKeeps = AppState.profilesKeeps.filter(v => v.vaultKeepId != id);
    // AppState.vaultKeeps = AppState.vaultKeeps.filter(
    //   (v) => v.vaultKeepId != id
    // );
    AppState.vaultKeeps = res.data
    AppState.vaultKeeps = res.data
  }
}


export const vaultsKeepsService = new VaultKeepsService();