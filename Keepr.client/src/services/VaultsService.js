import { AppState } from "../AppState.js";
import { logger } from "../utils/Logger.js";
import { api } from "./AxiosService.js";




class VaultsService {



  async createVault(body) {
    debugger
    const res = await api.post('api/vaults', body)
    AppState.vaults = [res.data, ...AppState.vaults]
    AppState.myAccountVaults = [res.data, ...AppState.myAccountVaults]
    AppState.myAccountVaults.push(res.data)
    AppState.profileVaults.push(res.data)
  }

  async deleteVault(vaultId) {
    const res = await api.delete(`api/vaults/${vaultId}`);
    let vault = AppState.vaults.findIndex((v) => v.id == vaultId);
    AppState.vaults.splice(vault, 1);
    let accVault = AppState.myAccountVaults.findIndex((v) => v.id == vaultId);
    AppState.myAccountVaults.splice(accVault, 1);
    AppState.vaults = res.data
    let profileVaults = AppState.profileVaults.findIndex((v) => v.id == vaultId);
    AppState.profileVaults.splice(profileVaults, 1);
    AppState.profileVaults = res.data
  }
  async getVaultById(id) {
    const res = await api.get(`api/vaults/${id}`)
    AppState.activeVault = res.data
    logger.log("vault", res.data)
  }

  async getKeepsByVaultId(id) {
    const res = await api.get(`api/vaults/${id}/keeps`)
    AppState.profilesKeeps = res.data
    logger.log(res.data)
  }
}




export const vaultsService = new VaultsService();