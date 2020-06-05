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
        <form class="contact-form" @submit.prevent="sendEmail">
          <label>Name</label>
          <input type="text" name="user_name" />
          <label>Email</label>
          <input type="email" name="user_email" />
          <label>Message</label>
          <textarea name="message"></textarea>
          <input type="submit" value="Send" />
        </form>
      </b-col>
    </b-row>
  </b-container>
</template>

<script>
import { mapState, mapActions } from "vuex";
import emailjs from "emailjs";
export default {
  name: "Index",
  computed: {
    anoAtual() {
      return new Date().getFullYear();
    },
    ...mapState(["empresasRepresentadas"])
  },
  methods: {
    ...mapActions(["getEmpresasRepresentadas"]),
    sendEmail(e) {
      emailjs
        .sendForm(
          "2a5c752534b7a21fdd1134ac3e5f5e62",
          "template_L2H5EVut",
          e.target,
          "user_kQatHorOJHaJ1DyEJkUuv"
        )
        .then(
          result => {
            console.log("SUCCESS!", result);
          },
          error => {
            console.log("FAILED...", error);
          }
        );
    }
  },
  created() {
    this.getEmpresasRepresentadas();
  }
};
</script>

<style>
</style>