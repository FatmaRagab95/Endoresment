<template>
  <div class="nursesPdf pt-5">
    <div class="container">
      <!-- calender for select month -->
      <v-dialog ref="dialog" v-model="modal" width="290px">
        <template v-slot:activator="{ on, attrs }">
          <v-text-field
            v-model="month"
            label="Pick a month"
            readonly
            v-bind="attrs"
            v-on="on"
            class="card p-5 mb-3"
          ></v-text-field>
        </template>
        <v-date-picker v-model="month" type="month" scrollable color="green lighten-1">
        </v-date-picker>
      </v-dialog>
      <!-- end calender -->

      <!-- display nurse pdf -->
      <div class="card bg-white p-5" id="statistics">
        <h2 class="text-danger text-center">
          Endoresment Report
          <span class="text-dark"> {{ month }}</span>
        </h2>
        <h3 class="text-center text-dark">
          {{ users.filter((x) => x.Emp_id == path)[0].FullName }}
        </h3>
        <hr />
        <!-- start patients-->
        <div class="row card shadow mb-4 mt-2">
          <div class="card-header" id="patients">
            <h3 class="text-primary">Patients ....</h3>
          </div>

          <div class="collapse show">
            <div class="card-body">
              <table
                class="table bg-white shadow-sm table-bordered"
                v-if="
                  Endorsement_Nurse_patients.filter(
                    (x) =>
                      x.Nurse_id == path &&
                      new Date(x.Date_from).toISOString().slice(0,7) ==
                        month &&
                      new Date(x.Date_to).toISOString().slice(0, 10).split('-')[1] ==
                        month.split('-')[1] &&
                      new Date(x.Date_to).toISOString().slice(0, 10).split('-')[1] ==
                        month.split('-')[1]
                  ).length > 0
                "
                style="font-size: 16px"
              >
                <thead>
                  <tr>
                    <th scope="col">Patient</th>
                    <th scope="col">From</th>
                    <th scope="col">To</th>
                  </tr>
                </thead>
                <tbody class="text-secondary">
                  <tr
                    v-for="patient in Endorsement_Nurse_patients.filter(
                      (x) =>
                        x.Nurse_id == path &&
                        new Date(x.Date_from).toISOString().slice(0, 7) ==
                          month &&
                        new Date(x.Date_to).toISOString().slice(0, 10).split('-')[1] ==
                          month.split('-')[1] &&
                        new Date(x.Date_to).toISOString().slice(0, 10).split('-')[1] ==
                          month.split('-')[1]
                    )"
                    :key="patient.id"
                  >
                    <td>
                      {{
                        patients.filter((x) => x.id == patient.Patient_id)[0]
                          .Patient_FullName
                      }}
                    </td>
                    <td>{{ patient.Date_from.split(" ")[0] }}</td>
                    <td v-if="patient.Date_to.length > 0">{{ patient.Date_to }}</td>
                    <td v-else>Tell Now</td>
                  </tr>
                </tbody>
              </table>
              <table
                class="table bg-white shadow-sm table-bordered"
                v-else-if="
                  Endorsement_Nurse_patients.filter(
                    (x) =>
                      x.Nurse_id == path &&
                      new Date(x.Date_from).toISOString().slice(0, 7) ==
                        month &&
                      x.Date_to == ''
                  ).length > 0
                "
                style="font-size: 16px"
              >
                <thead>
                  <tr>
                    <th scope="col">Patient</th>
                    <th scope="col">From</th>
                    <th scope="col">To</th>
                  </tr>
                </thead>
                <tbody class="text-secondary">
                  <tr
                    v-for="patient in Endorsement_Nurse_patients.filter(
                      (x) =>
                        x.Nurse_id == path &&
                        new Date(x.Date_from).toISOString().slice(0, 7) ==
                          month &&
                        x.Date_to == ''
                    )"
                    :key="patient.id"
                  >
                    <td>
                      {{
                        patients.filter((x) => x.id == patient.Patient_id)[0]
                          .Patient_FullName
                      }}
                    </td>
                    <td>{{ patient.Date_from.split(" ")[0] }}</td>
                    <td v-if="patient.Date_to.length > 0">{{ patient.Date_to }}</td>
                    <td v-else>Tell Now</td>
                  </tr>
                </tbody>
              </table>
              <div class="text-secondary text-center" v-else>
                <i
                  class="fa fa-exclamation-triangle text-warning mr-2"
                  aria-hidden="true"
                ></i
                >no data avaliable
              </div>
            </div>
          </div>
        </div>

        <!-- end patients-->

        <!-- start shift -->

        <div class="row card shadow mb-4">
          <div class="card-header" id="shifts">
            <h3 class="text-primary">Shifts ....</h3>
          </div>

          <div class="collapse show">
            <div class="card-body">
              <div
                v-if="
                  ShiftsData.filter(
                    (x) =>
                      x.Nurse_id == path &&
                      new Date(x.Shift_date).toISOString().slice(0, 7) ==
                        month
                  ).length > 0
                "
              >
                <table
                  class="table bg-white shadow-sm table-bordered"
                  style="font-size: 16px"
                >
                  <thead>
                    <tr>
                      <th scope="col">Unit</th>
                      <th scope="col">Received From</th>
                      <th scope="col">Type</th>
                      <th scope="col">Shift Date</th>
                      <th scope="col">Confirmed</th>
                      <th scope="col">Completed</th>
                    </tr>
                  </thead>
                  <tbody class="text-secondary">
                    <tr
                      v-for="shift in ShiftsData.filter(
                        (x) =>
                          x.Nurse_id == path &&
                          new Date(x.Shift_date).toISOString().slice(0, 7) ==
                          month
                      )"
                      :key="shift.id"
                    >
                      <td>
                        {{ Units.filter((x) => x.U_id == shift.Unit_id)[0].U_name }}
                      </td>
                      <td>{{ shift.Receive_name }}</td>
                      <td>{{ shift.Shift }}</td>
                      <td>{{ shift.Shift_date }}</td>
                      <td v-if="shift.Confirm == 0" class="text-danger">Not Confirmed</td>
                      <td v-else>Confirmed</td>
                      <td v-if="shift.Completed == 0" class="text-danger">
                        Not Completed
                      </td>
                      <td v-else>Completed</td>
                    </tr>
                  </tbody>
                </table>
              </div>
              <div class="text-secondary text-center" v-else>
                <i
                  class="fa fa-exclamation-triangle text-warning mr-2"
                  aria-hidden="true"
                ></i
                >no data avaliable
              </div>
            </div>
          </div>
        </div>

        <!-- end shift -->

        <!-- start units-->
        <div class="row card shadow">
          <div class="card-header" id="units">
            <h3 class="text-primary">Units ....</h3>
          </div>

          <div class="collapse show">
            <div class="card-body">
              <table
                class="table bg-white shadow-sm table-bordered"
                style="font-size: 16px"
                v-if="
                  Endoresment_Nurses_Units.filter(
                    (x) =>
                      x.Nurse_id == path &&
                      new Date(x.Entry_date).toISOString().slice(0, 7) ==
                        month
                  ).length > 0
                "
              >
                <thead>
                  <tr>
                    <th scope="col">unit</th>
                    <th scope="col">Updated By</th>
                    <th scope="col">Updated Date</th>
                    <th scope="col">Status</th>
                  </tr>
                </thead>
                <tbody class="text-secondary">
                  <tr
                    v-for="unit in Endoresment_Nurses_Units.filter(
                      (x) =>
                        x.Nurse_id == path &&
                        new Date(x.Entry_date).toISOString().slice(0, 7) ==
                          month
                    )"
                    :key="unit.Id"
                  >
                    <td>
                      {{ Units.filter((x) => x.U_id == unit.Unit_id)[0].U_name }}
                    </td>
                    <td>
                      {{ users.filter((x) => x.Emp_id == unit.Entry_user)[0].FullName }}
                    </td>
                    <td class="text-danger" v-if="unit.Last_Update.length == 0">
                      No Date Registered
                    </td>
                    <td v-else-if="unit.Last_Update.length > 0">
                      {{ unit.Last_Update }}
                    </td>
                    <td v-if="unit.Active == 0">Not Registered In The Unit</td>
                    <td v-else-if="unit.Active == 1">still working in the unit</td>
                  </tr>
                </tbody>
              </table>
              <div class="text-secondary text-center" v-else>
                <i
                  class="fa fa-exclamation-triangle text-warning mr-2"
                  aria-hidden="true"
                ></i
                >no data avaliable
              </div>
            </div>
          </div>
        </div>
        <!-- end units-->
      </div>
      <div class="d-flex justify-content-center mt-4">
        <button @click.prevent="pdf" class="btn btn-danger" style="width: 100px">
          get PDF
        </button>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  name: "nursesPdf",
  props: ["link", "user", "edits", "UnitDash"],
  data() {
    return {
      Units: [],
      users: [],
      Endorsement_Nurse_patients: [],
      Endoresment_Nurses_Units: [],
      patients: [],
      ShiftsData: [],

      month: new Date().toISOString().slice(0, 10).substring(0, 7),
      modal: false,

      patientDetails: [],
      path: "",
      apiUrl: this.link,
    };
  },
  methods: {
    pdf: function () {
      const statistics = document.getElementById("statistics");
      var opt = {
        margin: 0.3,
        filename: "pdf-nurse-statistics",
        image: { type: "jpeg", quality: 0.98 },
        html2canvas: { dpi: 192, letterRendering: true },
        jsPDF: { unit: "in", format: "letter", orientation: "portrait" },
      };
      html2pdf().from(statistics).set(opt).save();
    },
  },
  watch: {},

  created() {
    let that = this;

    this.path = this.$route.params.id;

    //get units
    $.ajax({
      type: "POST",
      url: that.apiUrl + "endoresment/nursesPdf.aspx/getUnitsData",
      contentType: "application/json; charset=utf-8",
      dataType: "json",
      success: function (data) {
        that.Units = JSON.parse(data.d);
      },
    });

    //get Endoresment Nurses patients
    $.ajax({
      type: "POST",
      url: that.apiUrl + "endoresment/nursesPdf.aspx/Endorsement_Nurse_patients",
      contentType: "application/json; charset=utf-8",
      dataType: "json",
      success: function (data) {
        that.Endorsement_Nurse_patients = JSON.parse(data.d);
      },
    });

    //get Endoresment Nurses Units
    $.ajax({
      type: "POST",
      url: that.apiUrl + "endoresment/nursesPdf.aspx/getEndoresment_Nurses_UnitsData",
      contentType: "application/json; charset=utf-8",
      dataType: "json",
      success: function (data) {
        that.Endoresment_Nurses_Units = JSON.parse(data.d);
      },
    });

    //get admin users
    $.ajax({
      type: "POST",
      url: that.apiUrl + "endoresment/nursesPdf.aspx/getadminusersData",
      contentType: "application/json; charset=utf-8",
      dataType: "json",
      success: function (data) {
        that.users = JSON.parse(data.d);
      },
    });

    //get patients data
    $.ajax({
      type: "POST",
      url: that.apiUrl + "endoresment/nursesPdf.aspx/getPatientsData",
      contentType: "application/json; charset=utf-8",
      dataType: "json",
      success: function (data) {
        that.patients = JSON.parse(data.d);
      },
    });

    //get nurses shifts
    $.ajax({
      type: "POST",
      url: that.apiUrl + "endoresment/nursesPdf.aspx/getshiftsData",
      contentType: "application/json; charset=utf-8",
      dataType: "json",
      success: function (data) {
        that.ShiftsData = JSON.parse(data.d);
      },
    });
  },
};
</script>

<style scoped>
.nursesPdf {
  min-height: 100vh;
  padding-bottom: 60px;
  background-color: #f9f9fb;
}
.nursesPdf .card {
  max-width: 1200px;
}
.nursesPdf .card .last-update {
  font-size: 16px;
}
</style>
