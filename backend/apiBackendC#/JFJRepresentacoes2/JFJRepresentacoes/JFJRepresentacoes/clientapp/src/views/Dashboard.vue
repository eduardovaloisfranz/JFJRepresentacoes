<template>
  <div>
    <b-container fluid v-if="token !== null">
      <b-row>
        <b-col xs="12" sm="12" md="12" lg="12" class="mt-3">
          <h1 class="h1 lead text-info text-center">JFJ Representações</h1>
          <p
            class="p lead text-success text-center"
          >Bem vindo Jaime Franz, segue a lista de suas Representações</p>
          <div class="d-flex justify-content-between">
            <b-button
              variant="info"
              @click.prevent="exibirModalEditUser = true"
            >Alterar suas informações</b-button>
            <b-button
              variant="success"
              @click.prevent="exibirModalCadastroRepresentacao = true"
            >Cadastrar Nova Representação</b-button>
          </div>
        </b-col>
      </b-row>
      <b-row>
        <b-col xs="12" sm="12" md="6" lg="6" class="my-1">
          <b-form-group
            label="Filtro"
            label-cols-sm="3"
            label-align-sm="right"
            label-size="sm"
            label-for="filterInput"
            class="mb-0"
          >
            <b-input-group size="sm">
              <b-form-input
                v-model="filtro"
                type="search"
                id="filterInput"
                placeholder="Digite o nome da Empresa que deseja buscar"
              ></b-form-input>
            </b-input-group>
          </b-form-group>
        </b-col>

        <b-col xs="12" sm="12" md="6" lg="6" class="my-1">
          <b-form-group
            label="Por página"
            label-cols-sm="6"
            label-cols-md="4"
            label-cols-lg="3"
            label-align-sm="right"
            label-size="sm"
            label-for="perPageSelect"
            class="mb-0"
          >
            <b-form-select v-model="porPagina" id="perPageSelect" size="sm" :options="pageOptions"></b-form-select>
          </b-form-group>
        </b-col>

        <b-col xs="12" sm="12" md="12" lg="12" class="my-1">
          <b-pagination
            v-model="paginaAtual"
            :total-rows="totalRegistros"
            :per-page="porPagina"
            align="fill"
            size="sm"
            class="my-0"
          ></b-pagination>
        </b-col>
      </b-row>
      <b-row>
        <b-col xs="12" sm="12" md="12" lg="12" class="mt-3">
          <b-table
            striped
            hover
            show-empty
            small
            :current-page="paginaAtual"
            stacked="md"
            :busy="isBusy"
            :items="empresasRepresentadas"
            :per-page="porPagina"
            :filter="filtro"
            @row-clicked="handleClick($event)"
          >
            <template v-slot:table-busy>
              <div class="text-center text-danger my-2">
                <b-spinner class="align-middle"></b-spinner>
                <strong>Carregando...</strong>
              </div>
            </template>
          </b-table>
        </b-col>
      </b-row>
      <b-modal v-model="exibirModal" centered hide-footer title="Modificar Representação">
        <h3 class="h3 text-center text-info text-monospace">Essa é a empresa que você selecionou</h3>
        <div class="d-flex flex-column align-items-center">
          <b-card
            :title="empresaSelecionada.nome"
            :img-src="empresaSelecionada.urlImagem"
            img-alt="Image"
            img-top
            tag="article"
            style="max-width: 20rem;"
            class="m-2"
          >
            <b-card-text>{{empresaSelecionada.descricao}}</b-card-text>
            <b-button
              :href="empresaSelecionada.urlSite"
              variant="outline-success"
            >Visitar o Site da Empresa</b-button>
          </b-card>
          <div class="d-flex justify-content-between">
            <b-button
              class="mr-5"
              @click="exibirModalEditEmpresa = true"
              variant="success"
            >Modificar</b-button>
            <b-button
              class="ml-5"
              variant="danger"
              @click.prevent="handleApagarRepresentacao"
            >Apagar</b-button>
          </div>
        </div>
      </b-modal>
      <b-modal
        v-model="exibirModalEditEmpresa"
        centered
        hide-footer
        title="Modificar Representação"
      >
        <h3
          class="h3 text-center text-info text-monospace"
        >Pré-visualização do card da empresa atual</h3>
        <div class="d-flex flex-column align-items-center">
          <b-card
            :title="empresaSelecionada.nome"
            :img-src="empresaSelecionada.urlImagem"
            img-alt="Image"
            img-top
            tag="article"
            style="max-width: 20rem;"
            class="m-2"
          >
            <b-card-text>{{empresaSelecionada.descricao}}</b-card-text>
            <b-button
              :href="empresaSelecionada.urlSite"
              variant="outline-success"
            >Visitar o Site da Empresa</b-button>
          </b-card>

          <label for="input-nomeEmpresa">Informe o novo nome:</label>
          <b-form-input
            id="input-nomeEmpresa"
            v-model="empresaSelecionada.nome"
            :state="nomeIsValido"
            aria-describedby="input-live-help input-live-feedback"
            placeholder="Informe o novo nome da Empresa"
          ></b-form-input>

          <label for="input-descricao">Informe a nova Descrição da Empresa:</label>
          <b-form-textarea
            id="input-descricaoEmpresa"
            v-model="empresaSelecionada.descricao"
            :state="descricaoIsValida"
            aria-describedby="input-live-help input-live-feedback"
            placeholder="Informe a nova descrição da Empresa"
            rows="3"
            max-rows="6"
          ></b-form-textarea>

          <label for="input-nomeEmpresa">Informe a nova url da Imagem</label>
          <b-form-input
            id="input-urlImagem"
            v-model="empresaSelecionada.urlImagem"
            :state="urlImagemIsValido"
            aria-describedby="input-live-help input-live-feedback"
            placeholder="Informe a nova url da imagem da Empresa"
          ></b-form-input>

          <label for="input-nomeEmpresa">Informe o novo url do Site da Empresa:</label>
          <b-form-input
            id="input-urlEmpresa"
            v-model="empresaSelecionada.urlSite"
            :state="urlIsValido"
            aria-describedby="input-live-help input-live-feedback"
            placeholder="Informe o novo url da Empresa"
          ></b-form-input>
          <b-button
            class="mt-3 mr-5"
            variant="success"
            @click.prevent="handleEditarEmpresa"
            :disabled="empresaIsValida"
          >Modificar</b-button>
        </div>
      </b-modal>
      <b-modal
        v-model="exibirModalCadastroRepresentacao"
        hide-footer
        centered
        title="Cadastro de Representação"
      >
        <h3 class="h3 text-center text-info text-monospace">Pré-visualização do card da nova Empresa</h3>
        <div class="d-flex flex-column align-items-center">
          <b-card
            :title="novaEmpresa.nome"
            :img-src="novaEmpresa.urlImagem"
            img-alt="Image"
            img-top
            tag="article"
            style="max-width: 20rem;"
            class="m-2"
          >
            <b-card-text>{{novaEmpresa.descricao}}</b-card-text>
            <b-button
              :href="novaEmpresa.urlSite"
              variant="outline-success"
            >Visitar o Site da Empresa</b-button>
          </b-card>

          <label for="input-nomeEmpresa">Informe o nome da empresa Representada:</label>
          <b-form-input
            id="input-nomeEmpresaRepresentada"
            v-model="novaEmpresa.nome"
            :state="nomeIsValidoNovaEmpresa"
            aria-describedby="input-live-help input-live-feedback"
            placeholder="Informe o nome da Empresa a ser Representada"
          ></b-form-input>

          <label for="input-descricao">Informe a descrição da Empresa:</label>
          <b-form-textarea
            id="input-descricaoEmpresaRepresentada"
            v-model="novaEmpresa.descricao"
            :state="descricaoIsValidaNovaEmpresa"
            aria-describedby="input-live-help input-live-feedback"
            placeholder="Informe a descrição da Empresa"
            rows="3"
            max-rows="6"
          ></b-form-textarea>

          <label for="input-nomeEmpresaRepresentada">Informe a url da Imagem da Empresa</label>
          <b-form-input
            id="input-urlImagem"
            v-model="novaEmpresa.urlImagem"
            :state="urlImagemIsValidoNovaEmpresa"
            aria-describedby="input-live-help input-live-feedback"
            placeholder="Informe a nova url da imagem da Empresa"
          ></b-form-input>

          <label for="input-nomeEmpresaRepresentada">Informe o novo url do Site da Empresa:</label>
          <b-form-input
            id="input-urlEmpresaRepresentada"
            v-model="novaEmpresa.urlSite"
            :state="urlIsValidoNovaEmpresa"
            aria-describedby="input-live-help input-live-feedback"
            placeholder="Informe o url da Empresa"
          ></b-form-input>
          <b-button
            class="mt-3"
            variant="success"
            @click.prevent="handleCadastrarNovaRepresentacao"
            :disabled="empresaIsValidaNovaEmpresa"
          >Adicionar Representação</b-button>
        </div>
      </b-modal>
      <b-modal v-model="exibirModalEditUser" centered title="Alterar suas informações" hide-footer>
        <section>
          <label for="input-nomeEmpresa">Informe Seu novo Nome:</label>
          <b-form-input
            id="input-novoNome"
            v-model="user.nome"
            :state="nomeUserIsValido"
            type="text"
            aria-describedby="input-live-help input-live-feedback"
            placeholder="Informe o seu novo Nome"
          ></b-form-input>

          <label for="input-email">Informe Seu novo Email:</label>
          <b-form-input
            id="input-novoEmail"
            v-model="user.email"
            :state="emailIsValido"
            type="email"
            aria-describedby="input-live-help input-live-feedback"
            placeholder="Informe o seu novo Email"
          ></b-form-input>
          <label for="input-email">Informe Sua nova Senha:</label>
          <b-form-input
            id="input-novaSenha"
            v-model="user.senha"
            type="password"
            :state="senhaIsValida"
            aria-describedby="input-live-help input-live-feedback"
            placeholder="Informe sua nova Senha"
          ></b-form-input>
          <div class="d-flex justify-content-center mt-3">
            <b-button
              variant="success"
              :disabled="usuarioIsValido"
              @click.prevent="handleAlterarInformacoes"
            >Alterar Informações</b-button>
          </div>
        </section>
      </b-modal>
    </b-container>
    <b-container v-else>
      <h1
        class="h1 text-center lead text-danger mt-4"
      >Você não possui autorização necessária para visualizar esta página, por favor efetue o login</h1>
    </b-container>
  </div>
</template>

<script>
import { mapState, mapActions } from "vuex";
import axios from "axios";
export default {
  name: "Dashboard",
  data() {
    return {
      empresaSelecionada: {
        id: 0,
        nome: "",
        urlImagem: "",
        descricao: "",
        urlSite: ""
      },
      novaEmpresa: {
        nome: "",
        urlImagem: "",
        descricao: "",
        urlSite: ""
      },
      exibirModal: false,
      exibirModalEditEmpresa: false,
      exibirModalCadastroRepresentacao: false,
      filtro: null,
      porPagina: 5,
      pageOptions: [5, 10, 15],
      paginaAtual: 1,
      exibirModalEditUser: false,
      user: {
        nome: "",
        email: "",
        senha: ""
      }
    };
  },
  methods: {
    ...mapActions([
      "apagarEmpresaRepresentada",
      "editarEmpresaRepresentada",
      "addEmpresaRepresentada"
    ]),
    handleClick(empresaSelecionada) {
      this.empresaSelecionada.id = empresaSelecionada.id;
      this.empresaSelecionada.nome = empresaSelecionada.nome;
      this.empresaSelecionada.urlImagem = empresaSelecionada.urlImagem;
      this.empresaSelecionada.descricao = empresaSelecionada.descricao;
      this.empresaSelecionada.urlSite = empresaSelecionada.urlSite;
      console.log(empresaSelecionada);
      this.exibirModal = true;
    },
    estiloConfirm(title, variant) {
      return {
        title: title,
        size: "sm",
        buttonSize: "sm",
        okVariant: variant || "info",
        okTitle: "Sim",
        cancelTitle: "Não",
        footerClass: "p-2",
        hideHeaderClose: false,
        centered: true
      };
    },
    estiloMsgBox(title, variant) {
      return {
        title: title,
        size: "sm",
        buttonSize: "sm",
        okVariant: variant || "success",
        headerClass: "p-2 border-bottom-0",
        footerClass: "p-2 border-top-0",
        centered: true
      };
    },
    makeToast(variant = null, title, corpo) {
      this.$bvToast.toast(corpo, {
        title: `${title}`,
        variant: variant,
        solid: true
      });
    },

    async handleApagarRepresentacao() {
      let res = "";
      await this.$bvModal
        .msgBoxConfirm(
          "Você realmente tem certeza que deseja remover esta empresa?",
          this.estiloConfirm("Esta ação é irreversivel", "danger")
        )
        .then(r => (res = r));
      if (!res) {
        this.$bvModal
          .msgBoxOk(
            "Empresa não foi apagada",
            this.estiloMsgBox("Registro mantido", "success")
          )
          .then(() => {
            this.exibirModal = false;
          });
      } else {
        this.apagarEmpresaRepresentada({ obj: this.empresaSelecionada });
        this.makeToast(
          "success",
          "Ação concluída",
          "Registro apagado com Sucesso"
        );
        this.exibirModal = false;
        this.empresaSelecionada = {
          id: 0,
          nome: "",
          urlImagem: "",
          descricao: "",
          urlSite: ""
        };
      }
    },
    async handleEditarEmpresa() {
      let res = "";
      await this.$bvModal
        .msgBoxConfirm(
          "Você realmente deseja modificar essa empresa?",
          this.estiloConfirm("Confirmação", "warning")
        )
        .then(r => (res = r));
      if (!res) {
        this.$bvModal
          .msgBoxOk(
            "Registro mantido",
            this.estiloMsgBox("Registro Não modificado", "success")
          )
          .then(() => {
            this.exibirModalEditEmpresa = false;
          });
      } else {
        this.editarEmpresaRepresentada({ obj: this.empresaSelecionada });
        this.empresaSelecionada = {
          id: 0,
          nome: "",
          urlImagem: "",
          descricao: "",
          urlSite: ""
        };
        this.exibirModal = false;
        this.exibirModalEditEmpresa = false;
      }
    },
    async handleCadastrarNovaRepresentacao() {
      let res = "";
      await this.$bvModal
        .msgBoxConfirm(
          "Você realmente deseja adicionar esta empresa?",
          this.estiloConfirm("Confirmação", "warning")
        )
        .then(r => (res = r));
      if (!res) {
        this.$bvModal.msgBoxOk(
          "Empresa não foi cadastrada",
          this.estiloMsgBox("Registro não inserido", "info")
        );
      } else {
        this.addEmpresaRepresentada(this.novaEmpresa);
      }
      this.exibirModalCadastroRepresentacao = false;
    },
    async handleAlterarInformacoes() {
      console.log(this.user);
      let res = "";
      await this.$bvModal
        .msgBoxConfirm(
          "Você realmente deseja modificar suas informações?",
          this.estiloConfirm("Confirmação", "warning")
        )
        .then(r => (res = r));
      if (!res) {
        this.$bvModal
          .msgBoxOk(
            "Registro mantido",
            this.estiloMsgBox("Registro Não modificado", "success")
          )
          .then(() => {
            this.exibirModalEditUser = false;
          });
      } else {
        let config = {
          headers: {
            Authorization: "Bearer " + sessionStorage.getItem("token")
          }
        };
        console.log(config);
        axios
          .put("https://localhost:44348/api/conta", this.user, config)
          .then(() => {
            this.$bvModal.msgBoxOk(
              "Registro Alterado com sucesso",
              this.estiloMsgBox("Registro Modificado", "success")
            );
          })
          .catch(() => {
            this.$bvModal.msgBoxOk(
              "Erro ao alterar as suas informações, por favor tente mais tarde",
              this.estiloMsgBox(
                "Houve um problema ao alterar o registro",
                "danger"
              )
            );
          });
        //this.exibirModalEditUser = false;
      }
    }
  },
  computed: {
    token() {
      return sessionStorage.getItem("token");
    },
    totalRegistros() {
      return this.empresasRepresentadas.length;
    },
    isBusy() {
      return this.empresasRepresentadas.length === 0 ? true : false;
    },
    nomeIsValido() {
      if (this.empresaSelecionada.nome !== undefined) {
        return this.empresaSelecionada.nome.length > 3 ? true : false;
      } else {
        return false;
      }
    },
    descricaoIsValida() {
      if (this.empresaSelecionada.descricao !== undefined) {
        return this.empresaSelecionada.descricao.length > 3 ||
          this.empresaSelecionada.length < 400
          ? true
          : false;
      } else {
        return false;
      }
    },
    urlImagemIsValido() {
      if (this.empresaSelecionada.urlImagem !== undefined) {
        return this.empresaSelecionada.urlImagem.length > 5 ? true : false;
      } else {
        return false;
      }
    },
    urlIsValido() {
      if (this.empresaSelecionada.urlSite != undefined) {
        return this.empresaSelecionada.urlSite.length > 5 ||
          this.empresaSelecionada.urlSite.startsWith("https://")
          ? true
          : false;
      } else {
        return false;
      }
    },
    empresaIsValida() {
      if (
        this.nomeIsValido &&
        this.descricaoIsValida &&
        this.urlImagemIsValido &&
        this.urlIsValido
      ) {
        return false;
      } else {
        return true;
      }
    },
    nomeIsValidoNovaEmpresa() {
      if (this.novaEmpresa.nome !== undefined) {
        return this.novaEmpresa.nome.length > 3 ? true : false;
      } else {
        return false;
      }
    },
    descricaoIsValidaNovaEmpresa() {
      if (this.novaEmpresa.descricao !== undefined) {
        return this.novaEmpresa.descricao.length > 3 ||
          this.novaEmpresa.length < 400
          ? true
          : false;
      } else {
        return false;
      }
    },
    urlImagemIsValidoNovaEmpresa() {
      if (this.novaEmpresa.urlImagem !== undefined) {
        return this.novaEmpresa.urlImagem.length > 5 ? true : false;
      } else {
        return false;
      }
    },
    urlIsValidoNovaEmpresa() {
      if (this.novaEmpresa.urlSite != undefined) {
        return this.novaEmpresa.urlSite.length > 5 ||
          this.novaEmpresa.urlSite.startsWith("https://")
          ? true
          : false;
      } else {
        return false;
      }
    },
    empresaIsValidaNovaEmpresa() {
      if (
        this.nomeIsValidoNovaEmpresa &&
        this.descricaoIsValidaNovaEmpresa &&
        this.urlImagemIsValidoNovaEmpresa &&
        this.urlIsValidoNovaEmpresa
      ) {
        return false;
      } else {
        return true;
      }
    },
    emailIsValido() {
      if (this.user.email != undefined) {
        return this.user.email.includes("@") && this.user.email.includes(".com")
          ? true
          : false;
      }
      return false;
    },
    senhaIsValida() {
      if (this.user.senha != undefined) {
        return this.user.senha.length > 3 ? true : false;
      } else {
        return false;
      }
    },
    nomeUserIsValido() {
      if (this.user.nome != undefined) {
        return this.user.nome.length > 3 && this.user.nome.length < 20
          ? true
          : false;
      } else {
        return false;
      }
    },
    usuarioIsValido() {
      if (this.emailIsValido && this.senhaIsValida && this.nomeUserIsValido) {
        return false;
      } else {
        return true;
      }
    },

    ...mapState(["empresasRepresentadas"])
  },
  beforeRouteEnter(to, from, next) {
    next(vm => {
      if (vm.token === null) {
        alert(
          "Você não possui autorização necessária para visualizar esta parte do sistema"
        );
        vm.$router.push({ path: "login" });
      } else {
        next();
      }
    });
  }
};
</script>