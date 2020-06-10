<template>
  <router-view></router-view>
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
