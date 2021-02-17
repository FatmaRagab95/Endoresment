<template>
  <div class="roomDetails p-3 text-capitalize">
    <div class="container-fluid mt-2 bg-white pt-3 pb-3 card">
      <div class="container text-center">
        <h1 class="mt-4 text-secondary" style="text-decoration: underline">ICU unit</h1>
        <p class="text-success mt-3">Update date in 05-2-2020</p>
      </div>
    </div>
    <div class="container-fluid mt-2 bg-white pt-3 pb-3 card viewPatients">
      <div class="container"></div>
    </div>
  </div>
</template>

<script>
export default {
  name: "roomDetails",
  props: ["link"],
  data() {
    return {
      apiUrl: `http://localhost:${this.link}/endoresment/dist/`,
    };
  },
  methods: {},
  created() {
    let that = this;
  },
};
</script>

<style scoped>
.viewPatients {
  min-height: 100vh;
  background: #fff url("../../assets/layout/img/humaaans-doctors.png") right no-repeat;
  background-size: contain;
  background-color: #f6f8fb;
  text-align: center;
  position: relative;
  overflow: hidden;
}
</style>
