<template>
  <div class="Nurses text-capitalize pt-5">
    <div class="container-fluid">
        <div class='card shadow m-auto'>
          <div class="row align-items-center p-3">
            <div class="col-md-6">
              <h1 style="text-decoration: underline">
                {{ Units.filter((x) => x.U_id == path)[0].U_name }}
              </h1>
            </div>
            <div class="col-md-6 p-4" v-if='editCharge'>
              <router-link
                class="btn btn-primary shadow pull-right mt-3 "
                style="width: 150px"
                :to="{ name: 'Edit Nurses', params: { id: path } }"
                ><i class="fa fa-pencil-square-o mr-3" aria-hidden="true"></i>Edit Nurses
                List</router-link
              >
            </div>
          </div>
        </div>
        <div class="card shadow ml-auto mr-auto mt-5 p-5">
            <div
              class="p-4 bg-light mb-3 shadow card"
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
                <span class="pull-right last-update" v-if="nurse.Last_Update"
                  ><i class="fa fa-pencil-square-o text-info mr-2" aria-hidden="true"></i
                  >Last update :
                  <span class="text-dark">{{ nurse.Last_Update }}</span></span
                >
                <span class="pull-right  last-update" v-else
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
                    View patients list
                  </a></span
                >
              </div>
            </div>
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
              <div class="title text-center text-white  mb-5">
                <span class="bg-secondary shadow pt-2 pb-2 pl-3 pr-3">patients list</span>
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
                  <div class="text-right mt-3" v-if='editCharge'>
                    <router-link
                      class="btn btn-primary shadow"
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
                    class="table table-striped table-bordered shadow  mt-3"
                    v-if=" Endorsement_Nurse_patients.filter(
                        (x) => x.Nurse_id == patientDetails.Nurse_id
                      ).length > 0
                    "
                  >
                    <thead class="thead-dark">
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
                  <div v-else>
                    <div class="text-center not-found">
                        <i class="fa fa-warning text-warning"></i>
                        No avalible data!
                    </div>
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
  props: ["link", 'user', 'edits', 'UnitDash'],
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
      editCharge:false
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
  watch: {
    UnitDash: function () {
        if (this.edits && this.UnitDash.filter(x => x.Unit_id == this.path).length > 0) {
          this.editCharge = true
        }
    }
  },

  created() {
    let that = this;

    this.path = this.$route.params.id;

    // if current user is charge nurse to this unit
    if (that.edits && that.UnitDash.filter(x => x.Unit_id == this.path).length > 0) {
      that.editCharge = true
    }

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

<style scoped>
.Nurses {
  min-height:100vh;
  padding-bottom:60px;
  background-color: #f9f9fb;
}
.Nurses .card {
  max-width:1200px;
}
.Nurses .card .last-update {
  font-size:16px;
}
</style>
