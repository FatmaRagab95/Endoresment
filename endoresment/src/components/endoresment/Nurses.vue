<template>
  <div class="Nurses p-3 text-capitalize">
    <div class="container-fluid mt-3 card" style="min-height: 100vh">
      <div class="container text-secondary mt-5 mb-5 bg-light shadow p-5">
        <div class="row bg-white shadow">
          <div class="col-md-6 p-4">
            <h1 style="text-decoration: underline">
              {{ Units.filter((x) => x.U_id == path)[0].U_name }}
            </h1>
          </div>
          <div class="col-md-6 p-4">
            <router-link
              class="btn btn-danger pull-right mt-3"
              style="width: 150px"
              :to="{ name: 'Edit Nurses', params: { id: path } }"
              ><i class="fa fa-pencil-square-o mr-3" aria-hidden="true"></i>Edit Nurses
              List</router-link
            >
          </div>
        </div>
        <div class="row bg-white shadow mt-4 p-4 card">
          <div
            class="col-md-12 p-4 bg-light mb-3 shadow card"
            v-for="nurse in Endoresment_Nurses_Units.filter((x) => x.Unit_id == path)"
            :key="nurse.Id"
          >
            <h4 class="pb-3">
              <i class="fa fa-user-md mr-2 text-info" aria-hidden="true"></i>nurse name :
              <span class="text-dark">{{
                users.filter((x) => x.Emp_id == nurse.Nurse_id)[0].FullName
              }}</span>
            </h4>
            <div class="border-bottom pb-3">
              <span class="pull-left"
                ><i class="fa fa-id-badge mr-3 text-info" aria-hidden="true"></i>emp id :
                <span class="badge-secondary badge pt-1 pb-1 pl-3 pr-3 ml-3">{{
                  nurse.Nurse_id
                }}</span></span
              >
              <span class="pull-right" v-if="nurse.Last_Update"
                ><i class="fa fa-pencil-square-o text-info mr-2" aria-hidden="true"></i
                >last update :
                <span class="text-dark">{{ nurse.Last_Update }}</span></span
              >
              <span class="pull-right" v-else
                ><i class="fa fa-pencil-square-o text-info mr-2" aria-hidden="true"></i
                >last update : <span class="text-dark">no update date found</span></span
              >
            </div>

            <div class="pt-3">
              <span
                class="pull-left"
                v-if="users.filter((x) => x.Emp_id == nurse.Entry_user).length > 0"
                >update from :
                <span class="text-success">{{
                  users.filter((x) => x.Emp_id == nurse.Entry_user)[0].FullName
                }}</span>
              </span>
              <span class="pull-left" v-else
                >update from : <span class="text-success">no avalible data</span></span
              >
              <span class="pull-right text-info"
                ><a
                  class="open-pop"
                  style="text-decoration: underline; cursor: pointer"
                  data-pop-name="patient-popup"
                  v-on:click.prevent="popUp(nurse, 'patientPop')"
                >
                  view patients list
                </a></span
              >
            </div>
          </div>
        </div>

        <!--  <table class="table table-striped table-light shadow text-secondary mt-3">
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
        </table>-->
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
          <div class="container-fluid custom-form m-auto pb-4" style="max-width: 1000px">
            <div class="cu-container">
              <span class="close-pop" v-on:click="close">Close</span>
              <div class="title text-center text-white">
                <span class="bg-secondary pt-2 pb-2 pl-3 pr-3">patients list</span>
              </div>

              <!--- start patient-info -->
              <div class="social-timeline-card shadow">
                <div
                  class="card bg-light general info tabcontent active overflow-auto p-3"
                  style="max-height: 380px"
                >
                  <h4
                    class="text-secondary text-center"
                    style="text-decoration: underline"
                  >
                    {{
                      users.filter((x) => x.Emp_id == patientDetails.Nurse_id)[0].FullName
                    }}
                  </h4>
                  <div class="text-right mt-3">
                    <router-link
                      class="btn btn-secondary"
                      style="width: 200px"
                      :to="{
                        name: 'Edit Patients',
                        params: { id: patientDetails.Nurse_id },
                      }"
                      ><i class="fa fa-pencil-square-o mr-3" aria-hidden="true"></i>Edit
                      patients List</router-link
                    >
                  </div>

                  <table
                    class="table table-light shadow text-secondary mt-3"
                    v-if="
                      Endoresment_Nurses_Units.filter(
                        (x) => x.Nurse_id == patientDetails.Nurse_id
                      ).length > 0
                    "
                  >
                    <thead class="font-wight-bold bg-secondary text-white">
                      <tr>
                        <th scope="col">#</th>
                        <th scope="col">patient name</th>
                        <th scope="col">Medical Number</th>
                        <th scope="col">age</th>
                        <th scope="col">Specialty</th>
                        <th scope="col">room</th>
                      </tr>
                    </thead>
                    <tbody>
                      <tr
                        v-for="(patient, i) in Endorsement_Nurse_patients.filter(
                          (x) => x.Nurse_id == patientDetails.Nurse_id
                        )"
                        :key="patient.id"
                      >
                        <td>{{ i + 1 }}</td>
                        <td>
                          {{
                            patients.filter((x) => x.id == patient.Patient_id)[0]
                              .Patient_FullName
                          }}
                        </td>
                        <td>
                          {{
                            patients.filter((x) => x.id == patient.Patient_id)[0]
                              .Medical_Number
                          }}
                        </td>
                        <td>
                          {{ patients.filter((x) => x.id == patient.Patient_id)[0].Age }}
                        </td>
                        <td>
                          {{
                            patients.filter((x) => x.id == patient.Patient_id)[0]
                              .Specialty
                          }}
                        </td>
                        <td>
                          {{ patients.filter((x) => x.id == patient.Patient_id)[0].Room }}
                        </td>
                      </tr>
                    </tbody>
                  </table>
                  <div v-else>no avalible data</div>
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
      Endorsement_Nurse_patients: [],
      Endoresment_Nurses_Units: [],
      patients: [],

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
      url: that.apiUrl + "endoresment/Nurses.aspx/Endorsement_Nurse_patients",
      contentType: "application/json; charset=utf-8",
      dataType: "json",
      success: function (data) {
        that.Endorsement_Nurse_patients = JSON.parse(data.d);
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

    //get patients data
    $.ajax({
      type: "POST",
      url: that.apiUrl + "endoresment/Nurses.aspx/getPatientsData",
      contentType: "application/json; charset=utf-8",
      dataType: "json",
      success: function (data) {
        that.patients = JSON.parse(data.d);
      },
    });
  },
};
</script>

<style scoped></style>
