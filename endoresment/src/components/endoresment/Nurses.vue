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
          <thead class="font-wight-bold bg-secondary text-white">
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
            <tr
              v-for="(nurse, i) in Endoresment_Nurses_Units.filter(
                (x) => x.Unit_id == path
              )"
              :key="nurse.Id"
            >
              <td>{{ i + 1 }}</td>
              <td>{{ users.filter((x) => x.Emp_id == nurse.Nurse_id)[0].FullName }}</td>
              <td>{{ nurse.Nurse_id }}</td>
              <td v-if="nurse.Last_Update">{{ nurse.Last_Update }}</td>
              <td v-else>no update date found</td>
              <td v-if="users.filter((x) => x.Emp_id == nurse.Entry_user).length > 0">
                {{ users.filter((x) => x.Emp_id == nurse.Entry_user)[0].FullName }}
              </td>
              <td v-else>no avalible data</td>
              <td class="text-info">
                <a
                  class="open-pop"
                  style="text-decoration: underline; cursor: pointer"
                  data-pop-name="patient-popup"
                  v-on:click.prevent="popUp(nurse, 'patientPop')"
                >
                  view patients list
                </a>
              </td>
            </tr>
          </tbody>
        </table>
      </div>
    </div>
    <!-- start patients list popup-->
    <div v-if="patientDetails">
      <div
        v-bind:class="
          patientDetails['patientPop']
            ? 'patient-popup popup active'
            : 'patient-popup popup'
        "
        v-if="patientDetails['patientPop'] == true"
      >
        <div class="popup-body" style="max-width: 1000px">
          <div class="container-fluid custom-form m-auto pb-4" style="max-width: 900px">
            <div class="cu-container">
              <span class="close-pop" v-on:click="close">Close</span>
              <div class="title text-center text-white">
                <span class="bg-secondary pt-2 pb-2 pl-3 pr-3">patients list</span>
              </div>

              <!--- start patient-info -->
              <div class="social-timeline-card shadow">
                <div
                  class="card bg-light general info tabcontent active overflow-auto text-center"
                  style="max-height: 380px"
                >
                  <div
                    class="text-secondary card bg-light select-form p-4 text-center shadow"
                  >
                    <h1 style="text-decoration: underline">
                      {{ Units.filter((x) => x.U_id == path)[0].U_name }}
                    </h1>
                    <div class="text-right mt-3">
                      <router-link
                        class="btn btn-secondary"
                        style="width: 150px"
                        :to="{ name: 'Edit Patients', params: { id: path } }"
                        ><i class="fa fa-pencil-square-o mr-3" aria-hidden="true"></i>Edit
                        patients List</router-link
                      >
                    </div>

                    <table
                      class="table table-striped table-light shadow text-secondary mt-3"
                    >
                      <thead class="font-wight-bold bg-secondary text-white">
                        <tr>
                          <th scope="col">#</th>
                          <th scope="col">nurse name</th>
                          <th scope="col">emp id</th>
                          <th scope="col">last update</th>
                          <th scope="col">update from</th>
                          <th scope="col">patients list</th>
                        </tr>
                      </thead>
                      <tbody></tbody>
                    </table>
                  </div>
                </div>
              </div>
              <!--- end patient-info -->
            </div>
          </div>
        </div>
      </div>
    </div>

    <!-- end patients list popup -->
  </div>
</template>

<script>
export default {
  name: "Nurses",
  props: ["link"],
  data() {
    return {
      Units: [],
      users: [],
      Endoresment_Nurses_Units: [],
      patientDetails: [],
      path: "",
      apiUrl: this.link,
    };
  },
  methods: {
    popUp: function (nurse, popName) {
      nurse["patientPop"] = false;
      nurse[popName] = true;
      this.patientDetails = Object.assign({}, nurse);

      setTimeout(function () {
        $("." + popName).addClass("active");
      }, 500);
    },

    close: function () {
      this.patientDetails = false;
    },
  },

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
      },
    });

    //get admin users
    $.ajax({
      type: "POST",
      url: that.apiUrl + "endoresment/Nurses.aspx/getadminusersData",
      contentType: "application/json; charset=utf-8",
      dataType: "json",
      success: function (data) {
        that.users = JSON.parse(data.d);
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
