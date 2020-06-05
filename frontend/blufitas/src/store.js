import Vue from "vue";
import Vuex from "vuex";
import { api } from "@/services";

Vue.use(Vuex);
export default new Vuex.Store({
  state: {
    empresasRepresentadas: [],
  },
  mutations: {
    SETAR_EMPRESAS_REPRESENTADAS(state, payload) {
      state.empresasRepresentadas = payload;
    },
  },
  actions: {
    getEmpresasRepresentadas(context) {
      api
        .get("empresas")
        .then((r) => {
          context.commit("SETAR_EMPRESAS_REPRESENTADAS", r.data);
        })
        .catch((err) =>
          console.log("Erro ao tentar setar as empresas: " + err)
        );
    },
  },
});
