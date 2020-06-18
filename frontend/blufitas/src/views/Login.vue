<template>
  <b-container>
    <b-row class="pt-3 justify-content-center align-items-center">
      <b-col cols="12" sm="8" md="4" lg="4">
        <h2 class="h2 text-center text-monospace">Por favor, faça login</h2>
        <b-form>
          <b-form-group>
            <b-form-input
              id="input-1"
              v-model="user.email"
              type="email"
              required
              placeholder="Informe o email"
            ></b-form-input>
          </b-form-group>
          <b-form-group>
            <b-form-input
              id="input-2"
              v-model="user.senha"
              type="password"
              required
              placeholder="Informe sua senha"
            ></b-form-input>
          </b-form-group>
          <div class="d-flex justify-content-between">
            <b-button variant="success" @click.prevent="efetuarLogin" type="submit">Login</b-button>
            <b-button variant="danger" @click.prevent="handleEsquecimentoSenha">Esqueci minha Senha</b-button>
          </div>
        </b-form>
      </b-col>
    </b-row>
  </b-container>
</template>

<script>
import axios from "axios";
export default {
  name: "Login",
  data() {
    return {
      user: {
        email: "",
        senha: ""
      },
      modalRecuperacaoSenha: false
    };
  },
  methods: {
    efetuarLogin() {
      const URLTOFETCH = "https://localhost:44348/api/Representacao/login";
      axios
        .post(URLTOFETCH, this.user)
        .then(res => {
          sessionStorage.setItem("token", res.data);
          this.$router.push({ path: "dash" });
        })
        .catch(err => console.log("Erro ao efetuar o login" + err));
    },
    handleEsquecimentoSenha() {
      console.log("??");
    }
  }
};
</script>