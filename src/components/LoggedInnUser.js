import { createStore } from 'vuex'

export default createStore({
  state: {
    loggedInn: {
      id: '',
      name: '',
      role: '',
      lifePoints: '',
      weedstones: ''
    }
  },
  mutations: {
    setLoggedInn(state, payload) {
      state.loggedInn = payload
    },
    updateLoggedInnField(state, { key, value }) {
      state.loggedInn[key] = value
    },
    setLoggedOut(state) {
      for (let x in state.loggedInn) {
        state.loggedInn[x] = '';
      }
    }
  },
  actions: {
    setLoggedInn({ commit }, payload) {
      commit('setLoggedInn', payload)
    },
    updateLoggedInnField({ commit }, payload) {
      commit('updateLoggedInnField', payload)
    },
    setLoggedOut({ commit }) {
      commit('setLoggedOut')
    }
  },
  getters: {
    loggedInn: (state) => state.loggedInn
  }
})