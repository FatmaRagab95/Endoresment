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
                <v-select
                  :items="Specialities"
                  item-text="Spcy_name_En"
                  :item-value="'Spcy_id'"
                  label="Select Specialities :"
                  name="Special"
                  v-model="Special"
                ></v-select>
              </div>

              <v-row justify="center" class="mt-3 mb-3">
                <v-date-picker
                  class="bg-primary shadow m-auto"
                  v-model="month"
                  type="month"
                ></v-date-picker>
              </v-row>

              <!--  <div>
                {{
                  Doctors_Shifts.filter(
                    (x) =>
                      new Date(x.Shift_date).toISOString().slice(0, 10).split("-")[1] ==
                        month.split("-")[1] &&
                      x.Shift_date.split(" ")[0].split("/")[2] == month.split("-")[0]
                  )
                }}
              </div>-->
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
                <div class="col-md-9">
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
                <div class="col-md-3">
                  <div class="w-100 h-100 d-flex justify-content-center">
                    <a
                      class="btn btn-danger text-white"
                      style="height: 37px; margin-top: 55px"
                      @click.prevent="pdf"
                    >
                      View Pdf
                    </a>
                  </div>
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
                    <span class="text-dark col-md-6"
                      >Doctor Name :<span
                        class="ml-2 text-secondary font-weight-bold text-capitalize"
                        >{{ doctor.DR_Name }}</span
                      >
                    </span>
                    <span class="text-dark col-md-6"
                      >Type :<span class="text-success ml-2">{{ doctor.Type }}</span>
                    </span>
                  </div>
                  <hr />
                  <div class="row">
                    <div class="col-md-12">
                      <div class="card bg-white p-3">
                        <h5 style="text-decoration: underline" class="text-info">
                          Consultation Patients List
                        </h5>

                        <table
                          class="table mt-4 shadow"
                          v-if="
                            PatientsData.filter(
                              (x) =>
                                x.Consultant_id == doctor.Dr_Code &&
                                x.Addmission_date.split('-')[0] == month.split('-')[0] &&
                                x.Addmission_date.split('-')[1] == month.split('-')[1]
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
                            </tr>
                          </thead>
                          <tbody>
                            <tr
                              v-for="(pat, i) in PatientsData.filter(
                                (x) =>
                                  x.Consultant_id == doctor.Dr_Code &&
                                  x.Addmission_date.split('-')[0] ==
                                    month.split('-')[0] &&
                                  x.Addmission_date.split('-')[1] == month.split('-')[1]
                              )"
                              :key="pat.id"
                            >
                              <th>{{ i + 1 }}</th>
                              <td>{{ pat.Patient_FullName }}</td>
                              <td class="text-success">{{ pat.Medical_Number }}</td>
                              <td>{{ pat.Gender }}</td>
                              <td>{{ pat.Room }}</td>
                              <td>{{ pat.Unit }}</td>
                            </tr>
                          </tbody>
                        </table>
                        <div v-else class="p-4 bg-light text-center text-secondary mt-4">
                          No Avaliable Data For Consulting Patients
                        </div>

                        <!-- Primary patients ------------------------------------------>
                        <h5 style="text-decoration: underline" class="text-info">
                          Primary Patients List
                        </h5>
                        <table
                          class="table mt-4 shadow"
                          v-if="
                            PatientFollow.filter(
                              (x) =>
                                new Date(x.Entry_date)
                                  .toISOString()
                                  .slice(0, 10)
                                  .split('-')[1] == month.split('-')[1] &&
                                x.Entry_date.split(' ')[0].split('/')[2] ==
                                  month.split('-')[0] &&
                                x.Consultaion == doctor.Dr_Code
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
                            </tr>
                          </thead>
                          <tbody>
                            <tr
                              v-for="(pat, i) in PatientFollow.filter(
                                (x) =>
                                  new Date(x.Entry_date)
                                    .toISOString()
                                    .slice(0, 10)
                                    .split('-')[1] == month.split('-')[1] &&
                                  x.Entry_date.split(' ')[0].split('/')[2] ==
                                    month.split('-')[0] &&
                                  x.Consultaion == doctor.Dr_Code
                              )"
                              :key="pat.id"
                            >
                              <th>{{ i + 1 }}</th>
                              <td
                                v-if="
                                  PatientsData.filter((x) => x.id == pat.Patient_id)
                                    .length > 0
                                "
                              >
                                {{
                                  PatientsData.filter((x) => x.id == pat.Patient_id)[0]
                                    .Patient_FullName
                                }}
                              </td>
                              <td class="text-secondary" v-else>no data registered</td>
                              <td
                                v-if="
                                  PatientsData.filter((x) => x.id == pat.Patient_id)
                                    .length > 0
                                "
                                class="text-success"
                              >
                                {{
                                  PatientsData.filter((x) => x.id == pat.Patient_id)[0]
                                    .Medical_Number
                                }}
                              </td>
                              <td class="text-secondary" v-else>no data registered</td>
                              <td
                                v-if="
                                  PatientsData.filter((x) => x.id == pat.Patient_id)
                                    .length > 0
                                "
                              >
                                {{
                                  PatientsData.filter((x) => x.id == pat.Patient_id)[0]
                                    .Gender
                                }}
                              </td>
                              <td class="text-secondary" v-else>no data registered</td>
                              <td
                                v-if="
                                  PatientsData.filter((x) => x.id == pat.Patient_id)
                                    .length > 0
                                "
                              >
                                {{
                                  PatientsData.filter((x) => x.id == pat.Patient_id)[0]
                                    .Room
                                }}
                              </td>
                              <td class="text-secondary" v-else>no data registered</td>
                              <td
                                v-if="
                                  PatientsData.filter((x) => x.id == pat.Patient_id)
                                    .length > 0
                                "
                              >
                                {{
                                  PatientsData.filter((x) => x.id == pat.Patient_id)[0]
                                    .Unit
                                }}
                              </td>
                              <td class="text-secondary" v-else>no data registered</td>
                            </tr>
                          </tbody>
                        </table>
                        <div v-else class="p-4 bg-light text-center text-secondary mt-4">
                          No Avaliable Data For Primary Patients
                        </div>

                        <!-- Shifts patients in Resident doctors-->
                        <div v-if="doctor.Type == 'Resident'">
                          <h5 style="text-decoration: underline" class="text-info">
                            Primary Patients List
                          </h5>
                          <div
                            v-if="
                              Doctors_Shifts.filter((x) => x.Doctor_id == doctor.Dr_Code)
                                .length > 0
                            "
                          >
                            <div class="row">
                              <div
                                class="col-md-6"
                                v-for="(shift, i) in Doctors_Shifts.filter(
                                  (x) => x.Doctor_id == doctor.Dr_Code
                                )"
                                :key="i + 'r'"
                              >
                                <h6 class="ml-2 text-success">
                                  Shift date {{ shift.Shift_date.split(" ")[0] }}
                                </h6>

                                <ul>
                                  <li
                                    class="ml-5"
                                    style="list-style-type: none"
                                    v-for="(patient, i) in PatientsData"
                                    :key="patient.id"
                                    v-if="
                                      new Date(shift.Shift_date.trim()) <
                                        new Date(patient.Discharged_date.trim()) ||
                                      patient.Discharged_date == false
                                    "
                                  >
                                    <span>{{ i + 1 }} - </span
                                    >{{ patient.Patient_FullName }}
                                  </li>
                                </ul>
                              </div>
                            </div>
                          </div>
                          <div
                            v-else
                            class="p-4 bg-light text-center text-secondary mt-4"
                          >
                            No Avaliable Data For Shifts Patients
                          </div>
                        </div>
                      </div>
                    </div>
                  </div>
                </div>

                <hr />
              </div>
            </div>
          </div>
        </div>
      </form>
      <!--end display doctors data-->
    </div>
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
      PatientFollow: [],

      month: "2021-03",

      Special: 40,
      filterData: [],

      selectedName: "",
      Names: [],
    };
  },
  watch: {
    month: function () {
      this.shiftData();
    },
    Special: function () {
      this.shiftData();
    },
  },
  methods: {
    shiftData() {
      let that = this;
      $.ajax({
        type: "POST",
        url: that.apiUrl + "endoresment/doctorInfo.aspx/getDoctors_ShiftsData",
        data: JSON.stringify({
          shift: {
            Branch_id: that.user.Branch_ID,
            Spcy_id: that.Special,
            Shift_date: that.month + "-01",
            endDate: moment(that.month).add(1, "Month").format("YYYY-MM-DD"),
          },
        }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
          that.Doctors_Shifts = JSON.parse(data.d);
        },
      });
    },
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

    // get Endorsement_PatientFollow
    $.ajax({
      type: "POST",
      url: that.apiUrl + "endoresment/doctorInfo.aspx/getPatientFollowData",
      contentType: "application/json; charset=utf-8",
      dataType: "json",
      success: function (data) {
        that.PatientFollow = JSON.parse(data.d);
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
    that.shiftData();

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
  list-style-type: none;
}
</style>
