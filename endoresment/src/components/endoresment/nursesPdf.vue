<template>
  <div class="nursesPdf text-capitalize pt-5">
    <div class="container-fluid">
      <div class="container card bg-white p-5" id="statistics">
        <div class="row card shadow mb-4">
          <div class="card-header" id="units">
            <h5 class="mb-0">
              <button class="btn btn-link">Units</button>
            </h5>
          </div>

          <div class="collapse show">
            <div class="card-body">
              <div
                v-if="
                  Endoresment_Nurses_Units.filter((x) => x.Nurse_id == path).length > 0
                "
              >
                <div
                  v-for="unit in Endoresment_Nurses_Units.filter(
                    (x) => x.Nurse_id == path
                  )"
                  :key="unit.Id"
                  class="border-bottom p-3"
                >
                  <span class="text-secondary">Unit Name :</span>
                  <span class="mr-2">{{
                    Units.filter((x) => x.U_id == unit.Unit_id)[0].U_name
                  }}</span>
                </div>
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

        <div class="row card shadow mb-4">
          <div class="card-header" id="patients">
            <h5 class="mb-0">
              <button class="btn btn-link">Patients</button>
            </h5>
          </div>

          <div class="collapse show">
            <div class="card-body">
              <div
                v-if="
                  Endorsement_Nurse_patients.filter((x) => x.Nurse_id == path).length > 0
                "
              >
                <div
                  v-for="patient in Endorsement_Nurse_patients.filter(
                    (x) => x.Nurse_id == path
                  )"
                  :key="patient.id"
                >
                  <span class="text-secondary">Patient Name :</span>
                  <span class="mr-2">{{
                    patients.filter((x) => x.id == patient.Patient_id)[0].Patient_FullName
                  }}</span>
                </div>
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

        <div class="row card shadow">
          <div class="card-header" id="shifts">
            <h3 class="text-info">Shifts ....</h3>
          </div>

          <div class="collapse show">
            <div class="card-body">
              <div v-if="ShiftsData.filter((x) => x.Nurse_id == path).length > 0">
                <table class="table shadow">
                  <thead>
                    <tr>
                      <th scope="col">unit</th>
                      <th scope="col">completed shift</th>
                      <th scope="col">Received shift</th>
                    </tr>
                  </thead>
                  <tbody class="text-secondary">
                    <tr
                      v-for="shift in ShiftsData.filter((x) => x.Nurse_id == path)"
                      :key="shift.id"
                    >
                      <td>
                        {{ Units.filter((x) => x.U_id == shift.Unit_id)[0].U_name }}
                      </td>
                      <td>{{ shift.Confirm }}</td>
                      <td>{{ shift.Completed }}</td>
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

      patientDetails: [],
      path: "",
      apiUrl: this.link,
    };
  },
  methods: {
    pdf: function () {
      const statistics = document.getElementById("statistics");
      var opt = {
        margin: 1,
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
