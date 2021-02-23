<template>
  <div class="Nurses p-3 text-capitalize">
    <div class="container-fluid mt-3 card">
      <div class="container-wave">
        <div class="wave"></div>
      </div>
      <div class="text-secondary card bg-light select-form p-4 text-center shadow">
        <h1 style="text-decoration: underline">
          {{ Units.filter((x) => x.U_id == path)[0].U_name }}
        </h1>
        <div class="text-right mt-3">
          <router-link
            class="btn btn-secondary"
            style="width: 150px"
            :to="{ name: 'Edit Nurses', params: { id: path } }"
            ><i class="fa fa-pencil-square-o mr-3" aria-hidden="true"></i>Edit Nurses
            List</router-link
          >
        </div>

        <table class="table table-striped table-light shadow text-secondary mt-3">
          <thead class="font-wight-bold">
            <tr>
              <th scope="col">#</th>
              <th scope="col">nurse name</th>
              <th scope="col">emp id</th>
              <th scope="col">last update</th>
              <th scope="col">update from</th>
              <th scope="col">patients list</th>
            </tr>
          </thead>
          <tbody>
            <tr v-for="(nurse, i) in Nurses" :key="nurse.Id">
              <td>{{ i + 1 }}</td>
              <td>{{ nurse.Nurse_id }}</td>
              <td>{{ nurse.Nurse_id }}</td>
              <td>{{ nurse.Last_Update }}</td>
              <td>{{ nurse.Update_From }}</td>
              <td><a>view patient list</a></td>
            </tr>
          </tbody>
        </table>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  name: "Nurses",
  props: ["link"],
  data() {
    return {
      Units: [],
      Endoresment_Nurses_Units: [],
      Nurses: [],
      path: "",

      apiUrl: `http://localhost:${this.link}/endoresment/dist/`,
    };
  },
  methods: {},

  created() {
    let that = this;
    this.path = this.$router.history.current.path.split("/")[2];

    //get units
    $.ajax({
      type: "POST",
      url: that.apiUrl + "endoresment/Nurses.aspx/getUnitsData",
      contentType: "application/json; charset=utf-8",
      dataType: "json",
      success: function (data) {
        that.Units = JSON.parse(data.d);
      },
    });

    //get Endoresment Nurses Units
    $.ajax({
      type: "POST",
      url: that.apiUrl + "endoresment/Nurses.aspx/getEndoresment_Nurses_UnitsData",
      contentType: "application/json; charset=utf-8",
      dataType: "json",
      success: function (data) {
        that.Endoresment_Nurses_Units = JSON.parse(data.d);
        that.Nurses = that.Endoresment_Nurses_Units.fiter((x) => x.Unit_id == this.path);
      },
    });
  },
};
</script>

<style scoped>
.container-wave {
  position: absolute;
  background: #fff;
  height: 50vh;
  width: 100%;
  left: 0;
  top: 0;
}

.wave {
  position: absolute;
  height: 250px;
  width: 100%;
  background: #fff;
  bottom: 0;
}

.wave::before,
.wave::after {
  content: "";
  display: block;
  position: absolute;
  border-radius: 100% 50%;
}

.wave::before {
  width: 55%;
  height: 109%;
  background-color: #f0f0f0;
  right: -1.5%;
  top: 80%;
}
.wave::after {
  width: 55%;
  height: 100%;
  background-color: #fff;
  left: 0;
  top: 60%;
}
.select-form {
  left: 50%;
  margin-top: 1%;
  margin-bottom: 1%;
  width: 80%;
  transform: translateX(-50%);
  max-height: 90vh;
  overflow: auto;
}
</style>
