<template>
  <div class="doctorInfo">
    <div class="container-wave">
      <div class="wave"></div>
    </div>

    <div class="internal-page" id="auth">
      <form class="mb-0">
        <div class="custom-form pt-3">
          <div class="cu-container">
            <div class="cu-form-group special" style="max-width: 900px">
              <div class="title">
                <span> <i class="fa fa-hospital-o mainColor mr-1"></i>Doctors Info</span>
              </div>

              <div class="cu-field">
                <h3 class="cu-label">
                  <label>Select Specialities :</label>
                </h3>
                <div class="f-select">
                  <select class="form-control form-control-sm" v-model="Special" required>
                    <option
                      v-for="Special in Specialities"
                      :value="Special.Spcy_id"
                      :key="Special.Spcy_id"
                    >
                      {{ Special.Spcy_name_En }}
                    </option>
                  </select>
                </div>
              </div>
            </div>
          </div>
        </div>
      </form>

      <!-------------------------------------------------------------------------------------------------------------------->
      <!--display doctors data-->
      <form class="mt-0">
        <div class="custom-form pt-3 mt-0">
          <div class="cu-container mt-0">
            <div class="cu-form-group special mt-0" style="max-width: 1000px">
              <div class="row">
                <div class="col-md-8">
                  <div class="cu-field">
                    <h3 class="cu-label">
                      <label>search for specific name:</label>
                    </h3>
                    <div class="cu-input text-box">
                      <span class="fa fa-search"></span>
                      <input
                        type="text"
                        v-model="selectedName"
                        v-on:keyup="namesList"
                        v-on:keydown.enter.prevent
                      />
                    </div>
                  </div>
                </div>
                <div class="col-md-4">
                  <a
                    class="open-pop btn btn-primary text-white mt-5 pull-right ml-2"
                    data-pop-name="patient-popup"
                  >
                    View Patients list
                  </a>
                  <a
                    class="btn btn-danger text-white mt-5 pull-right"
                    @click.prevent="pdf"
                  >
                    View Pdf
                  </a>
                </div>
              </div>
              <hr />
              <div id="statistics">
                <div
                  class="p-4 bg-light mb-3 shadow card"
                  v-for="doctor in filterNames(
                    Doctors_Data.filter((x) => x.Spcy_id == Special),
                    selectedName.toLowerCase()
                  )"
                  :key="doctor.Dr_Code"
                >
                  <div class="row">
                    <div class="col-md-6">
                      <div class="card bg-white h-100 p-2">
                        <div class="row pb-3">
                          <span class="text-dark col-md-12"
                            >Doctor Name :<span
                              class="ml-2 text-secondary font-weight-bold text-capitalize"
                              >{{ doctor.DR_Name }}</span
                            >
                          </span>
                        </div>
                        <div class="rowborder-bottom pb-3">
                          <span class="pull-left"
                            >Type :
                            <span class="text-success">{{ doctor.Type }}</span></span
                          >
                        </div>
                      </div>
                    </div>
                    <div class="col-md-6">
                      <div class="card bg-white h-100 p-2">
                        <span
                          class="col-md-6"
                          style="text-decoration: underline; font-size: 18px"
                        >
                          Patients Consulting
                        </span>
                      </div>
                    </div>
                  </div>

                  <hr />
                  <div class="row" style="font-size: 16px">
                    <div
                      class="col-md-4"
                      v-if="
                        Doctors_Shifts.filter((x) => x.Doctor_id == doctor.Dr_Code)
                          .length > 0
                      "
                    >
                      Received Shifts :
                      <span class="text-secondary"
                        >{{
                          Doctors_Shifts.filter((x) => x.Doctor_id == doctor.Dr_Code)[0]
                            .ReceiveNum
                        }}
                        shifts</span
                      >
                    </div>
                    <div class="col-md-4" v-else>
                      Received Shifts : <span class="text-danger">No Shifts</span>
                    </div>
                    <div
                      class="col-md-4"
                      v-if="
                        Doctors_Shifts.filter((x) => x.Doctor_id == doctor.Dr_Code)
                          .length > 0
                      "
                    >
                      Confirmed Shifts :
                      <span class="text-secondary"
                        >{{
                          Doctors_Shifts.filter((x) => x.Doctor_id == doctor.Dr_Code)[0]
                            .ConfirmNum
                        }}
                        shifts</span
                      >
                    </div>
                    <div class="col-md-4" v-else>
                      Confirmed Shifts : <span class="text-danger">No Shifts</span>
                    </div>
                    <div
                      class="col-md-4"
                      v-if="
                        Doctors_Shifts.filter((x) => x.Doctor_id == doctor.Dr_Code)
                          .length > 0
                      "
                    >
                      Completed Shifts :
                      <span class="text-secondary"
                        >{{
                          Doctors_Shifts.filter((x) => x.Doctor_id == doctor.Dr_Code)[0]
                            .CompleteNum
                        }}
                        shifts</span
                      >
                    </div>
                    <div class="col-md-4" v-else>
                      Completed Shifts : <span class="text-danger">No Shifts</span>
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>
      </form>
      <!--end display doctors data-->
    </div>
    <!-- start patients list popup-->
    <div class="patient-popup popup">
      <div class="popup-body" style="max-width: 1000px">
        <div class="container-fluid custom-form m-auto pb-4" style="max-width: 1000px">
          <div class="cu-container bg-white">
            <span class="close-pop">Close</span>
            <div class="title text-center text-white mb-5">
              <span class="bg-secondary shadow pt-2 pb-2 pl-3 pr-3">patients list</span>
              <table
                class="table table-striped mt-4 shadow"
                v-if="
                  PatientsData.filter(
                    (x) =>
                      x.Specialty ==
                      Specialities.filter((z) => z.Spcy_id == Special)[0].Spcy_name_En
                  ).length > 0
                "
              >
                <thead>
                  <tr>
                    <th scope="col">No</th>
                    <th scope="col">Name</th>
                    <th scope="col">Medicl No</th>
                    <th scope="col">Gender</th>
                    <th scope="col">Room</th>
                    <th scope="col">Unit</th>
                    <th scope="col">Addmission Date</th>
                    <th scope="col">Consultant Name</th>
                  </tr>
                </thead>
                <tbody>
                  <tr
                    v-for="(patient, i) in PatientsData.filter(
                      (x) =>
                        x.Specialty ==
                        Specialities.filter((z) => z.Spcy_id == Special)[0].Spcy_name_En
                    )"
                    :key="patient.id"
                  >
                    <th>{{ i + 1 }}</th>
                    <td>{{ patient.Patient_FullName }}</td>
                    <td class="text-success">{{ patient.Medical_Number }}</td>
                    <td>{{ patient.Gender }}</td>
                    <td>{{ patient.Room }}</td>
                    <td>{{ patient.Unit }}</td>
                    <td>{{ patient.Addmission_date }}</td>
                    <td>{{ patient.Consultant_Name }}</td>
                  </tr>
                </tbody>
              </table>
              <div v-else class="p-4 bg-light text-center text-secondary mt-4">
                No Avaliable Data
              </div>
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
  name: "doctorInfo",
  props: ["link", "user"],
  data() {
    return {
      apiUrl: this.link,
      Units: [],
      Specialities: [],
      Doctors_Data: [],
      Doctors_Shifts: [],
      PatientsData: [],

      Special: 40,
      filterData: [],

      selectedName: "",
      Names: [],
    };
  },
  methods: {
    filterNames(list, value) {
      return list.filter((x) => x.DR_Name.toLowerCase().indexOf(value) > -1);
    },
    namesList(event) {
      if (event.key == "Enter") {
        this.Names.push(this.selectedName);
      }
    },
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

  created() {
    let that = this;
    that.today = moment(new Date()).format("YYYY-MM-DD");

    //get Units
    $.ajax({
      type: "POST",
      url: that.apiUrl + "endoresment/doctorInfo.aspx/getUnitsData",
      data: JSON.stringify({ branch: { id: that.user.Branch_ID } }),
      contentType: "application/json; charset=utf-8",
      dataType: "json",
      success: function (data) {
        that.Units = JSON.parse(data.d);
      },
    });

    //get Specialities
    $.ajax({
      type: "POST",
      url: that.apiUrl + "endoresment/doctorInfo.aspx/getSpecialitiesData",
      contentType: "application/json; charset=utf-8",
      dataType: "json",
      success: function (data) {
        that.Specialities = JSON.parse(data.d);
      },
    });

    //get Doctors_Data
    $.ajax({
      type: "POST",
      url: that.apiUrl + "endoresment/doctorInfo.aspx/getdoctorsData",
      contentType: "application/json; charset=utf-8",
      dataType: "json",
      success: function (data) {
        that.Doctors_Data = JSON.parse(data.d);
      },
    });

    //get Endoresment_Doctors_Shifts
    $.ajax({
      type: "POST",
      url: that.apiUrl + "endoresment/doctorInfo.aspx/getDoctors_ShiftsData",
      contentType: "application/json; charset=utf-8",
      dataType: "json",
      success: function (data) {
        that.Doctors_Shifts = JSON.parse(data.d);
      },
    });

    // get patients data
    $.ajax({
      type: "POST",
      url: that.apiUrl + "endoresment/doctorInfo.aspx/getPatientsData",
      contentType: "application/json; charset=utf-8",
      dataType: "json",
      success: function (data) {
        that.PatientsData = JSON.parse(data.d);
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
  top: 60%;
}
.wave::after {
  width: 55%;
  height: 100%;
  background-color: #fff;
  left: -1.5%;
  top: 40%;
}
.custom-form {
  margin: 15px;
  background-color: transparent;
  border: none;
  text-align: initial;
  position: relative;
  z-index: 2;
}
.custom-form .cu-form-group.special {
  margin-top: 60px;
}
.custom-form .cu-form-group .title {
  text-align: initial;
  top: -60px;
}
.custom-form .cu-form-group .title span {
  height: 45px;
  line-height: 43px;
  font-size: 20px;
  box-shadow: 0 1px 10px 0 rgba(206, 206, 206, 0.3),
    0 1px 3px 1px rgba(225, 225, 225, 0.15);
  border-radius: 30px;
  background-color: #fff;
  color: #212529;
  border: 1px solid #e0e0e0;
}
.internal-page .custom-form .cu-field {
  max-width: 600px;
  margin: 25px auto;
}
.custom-form .cu-field .cu-input.text-box {
  margin-inline-end: 0px;
}
.internal-page .f-select::before {
  top: 50%;
}
.special-btn {
  border-radius: 65px;
  padding: 10px 30px;
  margin: 30px 10px;
  text-transform: capitalize;
}
.special-btn:hover {
  opacity: 0.7;
}
.special-btn.blue-btn {
  background-color: #007cff;
}
.special-btn.yellow-btn {
  background-color: #f8f2a4;
  color: #000;
}
.special-btn i {
  margin-right: 15px;
}
</style>
