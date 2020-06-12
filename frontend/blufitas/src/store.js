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
    APAGAR_EMPRESA_REPRESENTADA(state, payload) {
      state.empresasRepresentadas.splice(payload.idx, 1);
    },
    EDITAR_EMPRESA_REPRESENTADA(state, payload) {
      Vue.set(state.empresasRepresentadas, payload.idx, payload.obj);
    },
    CADASTRAR_EMPRESA_REPRESENTADA(state, payload) {
      state.empresasRepresentadas.push(payload);
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
    async apagarEmpresaRepresentada(context, payload) {
      let idx = context.state.empresasRepresentadas.findIndex(
        (el) => el.id === payload.obj.id
      );
      payload.idx = idx;
      await api
        .delete(`empresas/${payload.obj.id}`)
        .then(() => {
          this.commit("APAGAR_EMPRESA_REPRESENTADA", payload);
        })
        .catch((err) => console.log("Erro ao apagar" + err));
    },
    async editarEmpresaRepresentada(context, payload) {
      let idx = context.state.empresasRepresentadas.findIndex(
        (el) => el.id === payload.obj.id
      );
      payload.idx = idx;
      await api
        .put(`empresas/${payload.obj.id}`, payload.obj)
        .then(() => {
          context.commit("EDITAR_EMPRESA_REPRESENTADA", payload);
        })
        .catch((res) =>
          console.log("Erro ao tentar modificar a empresa" + res)
        );
    },
    async addEmpresaRepresentada(context, payload) {
      await api
        .post("empresas", payload)
        .then((res) => {
          context.commit("CADASTRAR_EMPRESA_REPRESENTADA", res.data);
        })
        .catch((err) => console.log("Erro ao tentar adicionar Empresa" + err));
    },
  },
});
