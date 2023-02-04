import { reactive } from 'vue'

// NOTE AppState is a reactive object to contain app level data
export const AppState = reactive({
  user: {},
  /** @type {import('./models/Account.js').Account} */
  account: {},

  keeps: [],

  activeKeep: null,

  vaults: [],

  vaultKeeps: [],

  myAccountKeeps: [],

  myAccountVaults: [],
  profilesKeeps: [],
  activeVault: null,
  activeProfile: null,
  profileVaults: [],
  Profile: [],
})
