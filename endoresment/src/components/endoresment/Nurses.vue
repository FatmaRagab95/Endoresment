<template>
  <div class="Nurses text-capitalize pt-5">
    <div class="container">
      <div class="card shadow m-auto">
        <div class="row align-items-center p-3">
          <div class="col-lg-6">
            <h1 style="text-decoration: underline">
              {{ Units.filter((x) => x.U_id == path)[0].U_name }}
            </h1>
          </div>
          <div class="col-md-6 p-4" v-if="editCharge">
            <router-link
              class="btn btn-primary shadow pull-right mt-3"
              style="width: 150px"
              :to="{ name: 'Edit Nurses', params: { id: path } }"
              ><i class="fa fa-pencil-square-o mr-3" aria-hidden="true"></i>Edit Nurses
              List</router-link
            >
          </div>
        </div>
      </div>
      <div class="nurses-container border bg-white shadow rounded row">
        <div
          class="mb-3 card bg-light col-md-6 nurse-card"
          v-for="nurse in users"
          :key="nurse.Id"
        >
          <div class="row">
            <span class="col-md-8 text-secondary">
              <i class="fa fa-user-md mr-1 text-primary" aria-hidden="true"></i>
              Nurse Name:
              <span class="text-dark">{{
                nurse.FullName
              }}</span>
            </span>
            <span class="col-lg-4 text-right">
              <router-link
                v-if="user.Role_id != 12"
                class="text-danger text-right"
                style="text-decoration: underline"
                :to="{ name: 'Nurses Pdf', params: { id: nurse.Emp_id } }"
                >nurses details PDF</router-link
              >
            </span>
          </div>
          <div class="row pt-3">
            <span class="col-lg-12 text-right">
              <a
                class="open-pop btn btn-primary text-white shadow patient-view-btn"
                data-pop-name="patient-popup"
                v-on:click.prevent="popUp(nurse, 'patientPop')"
              >
                View patients
              </a>
            </span>
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
              <div class="title text-center text-white mb-5">
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
                      patientDetails.FullName
                    }}
                  </h4>
                  <div class="text-right mt-3" v-if="editCharge">
                    <router-link
                      class="btn btn-primary shadow"
                      style="width: 200px"
                      :to="{
                        name: 'Edit Patients',
                        params: { id: patientDetails.Emp_id },
                      }"
                      ><i class="fa fa-pencil-square-o mr-3" aria-hidden="true"></i>Edit
                      patients List</router-link
                    >
                  </div>

                  <table
                    class="table table-striped table-bordered shadow mt-3"
                    v-if="
                      Endorsement_Nurse_patients.filter(
                        (x) => x.Nurse_id == patientDetails.Emp_id
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
                          (x) => x.Nurse_id == patientDetails.Emp_id
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
  props: ["link", "user", "edits", "UnitDash"],
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
      editCharge: false,
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
      if (
        this.edits &&
        this.user.Role_id == 17 &&
        this.UnitDash.filter((x) => x.Unit_id == this.path).length > 0
      ) {
        this.editCharge = true;
      }
    },
  },

  created() {
    let that = this;

    this.path = this.$route.params.id;

    // if current user is charge nurse to this unit
    if (
      that.edits &&
      this.user.Role_id == 17 &&
      that.UnitDash.filter((x) => x.Unit_id == this.path).length > 0
    ) {
      that.editCharge = true;
    }

    //get units
    $.ajax({
      type: "POST",
      url: that.apiUrl + "endoresment/Nurses.aspx/getUnitsData",
      data: JSON.stringify({ user: { Branch_ID: that.user.Branch_ID } }),
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

    //get admin users
    $.ajax({
      type: "POST",
      url: that.apiUrl + "endoresment/Nurses.aspx/getadminusersData",
      contentType: "application/json; charset=utf-8",
      data: JSON.stringify({ user: { Area_id: that.path, Branch_ID: that.user.Branch_ID } }),
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
  padding-bottom: 60px;
  min-height: 100vh;
  background-color: #f6f8fb;
  background: #fff url("../../assets/layout/img/backgrounds/bg9.svg") center no-repeat;
}
.Nurses .nurses-container {
  margin-top: 60px;
  padding: 30px 15px;
}
.Nurses .nurses-container .nurse-card {
  padding: 30px;
}
.Nurses .card .patient-view-btn {
  font-size: 14px;
}
</style>
