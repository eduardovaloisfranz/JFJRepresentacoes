<template>
  <div>
    <router-view></router-view>
    <p class="text-center text-info text-capitalize lead">
      Site criado por Eduardo Valois Franz
      <a
        href="https://github.com/eduardovaloisfranz/"
        class="text-danger"
      >Github</a>
      -
      <a
        href="https://br.linkedin.com/in/eduardo-valois-franz-b82008170"
        class="text-dark"
      >Linkedin</a>
    </p>
  </div>
</template>

<script>
import { mapActions } from "vuex";
export default {
  name: "App",
  components: {},
  methods: {
    ...mapActions(["getEmpresasRepresentadas"])
  },
  created() {
    this.getEmpresasRepresentadas();
  }
};
</script>

<style>
</style>
