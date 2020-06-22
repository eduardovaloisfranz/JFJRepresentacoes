<template>
  <b-container>
    <b-row>
      <b-col cols="12" sm="12" md="12" lg="12">
        <h1 class="h1 lead text-info text-center pt-3">JFJ Representações</h1>
        <h2 class="h2 lead text-success text-center pt-3">Empresas Representadas:</h2>
      </b-col>
      <b-col cols="12" sm="12" md="12" lg="12">
        <section class="d-flex flex-wrap justify-content-center">
          <b-card
            :title="empresa.nome"
            :img-src="empresa.urlImagem"
            img-alt="Image"
            img-top
            tag="article"
            style="max-width: 20rem;"
            class="m-2"
            v-for="(empresa, idx) in empresasRepresentadas"
            :key="empresa + idx"
          >
            <b-card-text>{{empresa.descricao}}</b-card-text>
            <b-button :href="empresa.urlSite" variant="outline-success">Visitar o Site da Empresa</b-button>
          </b-card>
        </section>
      </b-col>
    </b-row>
    <b-row>
      <b-col cols="12" sm="12" md="12" lg="12">
        <h3 class="h3 text-center text-dark text-monospace">{{anoAtual}} - JFJ Representações</h3>
        <p class="p text-center text-dark lead">
          Telefone Para Contato:
          <a href="tel:47 98825-5153">47 98825-5153</a>
        </p>
      </b-col>
      <b-col cols="12" sm="12" md="12" lg="12">
        <section v-if="mensagemBiblia">
          <p class="text-info">Mensagem Biblia de hoje:</p>
          {{mensagemBibliaFormatada}}
        </section>
        <section class="d-flex justify-content-center">
          <div>
            <h3
              class="h3 text-success text-capitalize lead m-3"
            >Quer saber mais? entre em contato mandando email</h3>
            <b-form-input v-model="nome" placeholder="Informe seu nome" class="m-3"></b-form-input>
            <b-form-input v-model="email" type="email" placeholder="Informe seu Email" class="m-3"></b-form-input>
            <b-form-textarea
              id="textarea"
              class="m-3"
              v-model="mensagem"
              placeholder="Informe a mensagem..."
              rows="3"
              max-rows="6"
            ></b-form-textarea>
            <div>
              <b-button
                class="m-3"
                variant="outline-success"
                @click.prevent="handleEnviarEmail"
                :disabled="contatoIsValido"
              >Entrar em Contato</b-button>
            </div>
          </div>
        </section>
      </b-col>
    </b-row>
  </b-container>
</template>

<script>
import { mapState } from "vuex";
import emailjs from "emailjs-com";
import axios from "axios";

export default {
  name: "Index",
  data() {
    return {
      nome: "",
      email: "",
      mensagem: "",
      mensagemBiblia: null
    };
  },
  computed: {
    anoAtual() {
      return new Date().getFullYear();
    },
    contatoIsValido() {
      return this.nome.length < 4 ||
        this.email.length < 4 ||
        this.mensagem.length < 4
        ? true
        : false;
    },
    mensagemBibliaFormatada() {
      if (this.mensagemBiblia !== null) {
        return `${this.mensagemBiblia.book.name} ${this.mensagemBiblia.chapter}:${this.mensagemBiblia.number} ${this.mensagemBiblia.text}`;
      } else {
        return "";
      }
    },
    ...mapState(["empresasRepresentadas"])
  },

  methods: {
    handleEnviarEmail() {
      if (!this.contatoIsValido) {
        let templateParams = {
          from_name: this.nome,
          email_owner: this.email,
          message_html: this.mensagem
        };
        emailjs
          .send(
            "default_service",
            "template_L2H5EVut",
            templateParams,
            "user_kQatHorOJHaJ1DyEJkUuv"
          )
          .then(() => {
            this.$bvModal
              .msgBoxOk("Email Enviado Com Sucesso", {
                title: "Aviso",
                size: "sm",
                buttonSize: "sm",
                okVariant: "success",
                headerClass: "p-2 border-bottom-0",
                footerClass: "p-2 border-top-0",
                centered: true
              })
              .then(() => {
                this.limparCampos();
              });
          })
          .catch(err => console.log("err" + err));
      }
    },
    limparCampos() {
      (this.nome = ""), (this.email = ""), (this.mensagem = "");
    },
    getMensagemBiblia() {
      let config = {
        headers: {
          Authorization:
            "Bearer " +
            "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpZCI6Ik1vbiBKdW4gMjIgMjAyMCAxNzo0ODo1NSBHTVQrMDAwMC5kdWR1ZnJhbnoxM0BnbWFpbC5jb20iLCJpYXQiOjE1OTI4NDgxMzV9.emZ0ijWak2gNwoMcOG0OIhN3YEF61wdEh15Rj6VCNug"
        }
      };
      axios
        .get("https://bibleapi.co/api/verses/nvi/random", config)
        .then(res => {
          this.mensagemBiblia = res.data;
        });
    }
  },
  created() {
    this.getMensagemBiblia();
  }
};
</script>

<style>
</style>