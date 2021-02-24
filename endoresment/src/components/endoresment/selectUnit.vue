<template>
  <div class="selectUnit p-3 text-capitalize">
    <!-- start select unit -->
    <div class="container-fluid mt-3 card">
      <div class="container-wave">
        <div class="wave"></div>
      </div>

      <div class="text-secondary card bg-light select-form p-4 text-center shadow">
        <label class="text-secondary">
          <h3 class="font-weight-bold">select unit :</h3>
        </label>
        <div class="bg-white w-75 m-auto form-group">
          <select class="w-100 h-100 card form-control" v-model="selectedUnit">
            <option
              v-for="unit in getUnitsData"
              :key="unit.U_id"
              :value="{ id: unit.U_id, name: unit.U_name }"
            >
              {{ unit.U_name }}
            </option>
          </select>
        </div>

        <router-link
          v-if="selectedUnit"
          :to="'/roomDetails/' + selectedUnit.id"
          class="btn btn-secondary w-25 m-auto"
          >show unit details</router-link
        >
      </div>
    </div>
    <!-- end select unit -->
  </div>
</template>

<script>
export default {
  name: "selectUnit",
  props: ["link"],
  data() {
    return {
      getUnitsData: [],
      selectedUnit: null,

      apiUrl: this.link,
    };
  },
  methods: {
    selectUnit: function () {
      if (this.selectedUnit == null) {
        swal({
          title: "Error!",
          text: "Sorry, you should select the Unit ! ",
          icon: "warning",
          dangerMode: true,
        });
      } else {
        this.$router.push(this.selectedUnit);
      }
    },
  },

  created() {
    let that = this;
    //get units
    $.ajax({
      type: "POST",
      url: that.apiUrl + "endoresment/selectUnit.aspx/getUnitsData",
      contentType: "application/json; charset=utf-8",
      dataType: "json",
      success: function (data) {
        that.getUnitsData = JSON.parse(data.d);
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
  top: 70%;
}
.wave::after {
  width: 55%;
  height: 100%;
  background-color: #fff;
  left: 0;
  top: 50%;
}
.select-form {
  left: 50%;
  margin-top: 8%;
  width: 50%;
  transform: translateX(-50%);
  height: 40vh;
}
</style>
