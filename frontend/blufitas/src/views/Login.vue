<template>
  <b-container>
    <b-row class="pt-3 justify-content-center align-items-center">
      <b-col cols="12" sm="8" md="4" lg="4">
        <h2 class="h2 text-center text-monospace">Por favor, faça login</h2>
        <b-form @submit="efetuarLogin">
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
            <b-button variant="success" @click.prevent type="submit">Login</b-button>
          </div>
        </b-form>
      </b-col>
    </b-row>
  </b-container>
</template>

<script>
export default {
  name: "Login",
  data() {
    return {
      user: {
        email: "",
        senha: ""
      }
    };
  },
  methods: {
    efetuarLogin() {
      console.log("OOOopa eae vei blz??? ");
    }
  }
};
</script>