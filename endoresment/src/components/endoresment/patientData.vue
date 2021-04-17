<template>
  <div class="patientData p-3">
    <div class="mt-2 bg-white pt-3 pb-3 card">
      <!-- start patient data -->
      <div class="container" v-if="patientData">
        <div class="row form-group head-info rounded text-center pt-1 pb-1 mb-0">
          <div class="col-md-4">
            <div class="shadow bg-white rounded pt-3 pb-3">
              <span class="text-info">Name :</span
              ><span class="ml-2">{{ patientData.Patient_FullName }}</span>
            </div>
          </div>
          <div class="col-md-4">
            <div class="shadow bg-white rounded pt-3 pb-3">
              <span class="text-info">Medical Number :</span
              ><span class="ml-2">{{ patientData.Medical_Number }}</span>
            </div>
          </div>
          <div class="col-md-4">
            <div class="shadow bg-white rounded pt-3 pb-3">
              <span class="text-info">Age:</span
              ><span class="ml-2">{{ patientData.Age }}</span>
            </div>
          </div>
        </div>

        <div class="row form-group text-center mt-2 mb-0" style="font-size: 20px">
          <div class="col-md-4">
            <span class="text-secondary">Unit :</span
            ><span
              class="ml-2 badge card badge-success text-uppercase pl-4 pr-4 shadow-sm"
              >{{ patientData.Unit }}</span
            >
          </div>
          <div class="col-md-4" style="text-decoration: underline; font-size: 25px">
            <span class="text-success">Date of admission :</span>
            <span class="ml-2 text-secondary">{{ patientData.Addmission_date }}</span>
          </div>
          <div class="col-md-4">
            <span class="text-secondary">Room:</span>
            <span class="ml-2 badge card badge-success pl-4 pr-4 shadow-sm">{{
              patientData.Room
            }}</span>
          </div>
        </div>

        <div class="row text-center mb-4 mt-0">
          <div class="col-md-6 mt-0">
            <span class="bg-white p-2 shadow-sm border rounded">
              <i class="fa fa-address-card-o text-secondary" aria-hidden="true"></i>
              <span class="ml-2 text-info">Specialty :</span
              ><span class="ml-2 text-danger">{{ patientData.Specialty }}</span></span
            >
          </div>
          <div class="col-md-6 mt-0">
            <span class="bg-white p-2 shadow-sm border rounded">
              <i class="fa fa-user-md text-secondary" aria-hidden="true"></i>
              <span class="ml-2 text-info">Consultant :</span
              ><span class="ml-2 text-danger">{{
                patientData.Consultant_Name
              }}</span></span
            >
          </div>
        </div>

        <div
          class="btn-options border-top text-center pt-3 m-0"
          v-if="edit && patientData.Patient_Status == 1"
        >
          <i v-if="stat > 1" class="close fa fa-close pull-right" @click="stat = 1"></i>
          <div class="row">
            <div class="col-sm-4 mb-1">
              <button
                class="btn btn-block btn-primary shadow-sm btn-sm"
                data-toggle="modal"
                data-target="#exampleModalCenter"
              >
                Transfer
              </button>
            </div>

            <div class="col-sm-4 mb-1">
              <button
                class="btn btn-block btn-danger shadow-sm btn-sm"
                @click.prevent="
                  stat = 2;
                  deathDate = '';
                "
              >
                Disharge
              </button>

              <div v-if="stat == 2" class="mt-3">
                <input
                  class="form-control"
                  type="date"
                  :max="today"
                  v-model="dischargeDate"
                />
                <button
                  class="btn btn-success mt-1 btn-sm"
                  @click.prevent="updateStat(2, patientData.Bed_id)"
                >
                  Submit
                </button>
              </div>
            </div>
            <div class="col-sm-4 mb-1">
              <button
                class="btn btn-block btn-dark shadow-sm btn-sm"
                @click.prevent="
                  stat = 3;
                  dischargeDate = '';
                "
              >
                Death
              </button>

              <div v-if="stat == 3" class="mt-3">
                <input
                  class="form-control"
                  type="date"
                  :max="today"
                  v-model="deathDate"
                />
                <button
                  class="btn btn-success mt-1 btn-sm"
                  @click.prevent="updateStat(3, patientData.Bed_id)"
                >
                  Submit
                </button>
              </div>
            </div>
          </div>
        </div>
        <div
          v-if="patientData.Patient_Status > 1"
          class="border-top text-center pt-5 pb-3 mt-3"
        >
          <p>
            Patient status:
            <span v-if="patientData.Patient_Status == 2" class="text-danger"
              >Discharged</span
            >
            <span v-if="patientData.Patient_Status == 3" class="text-danger">Dead</span>
          </p>
        </div>
      </div>
      <!-- end patient data -->
    </div>

    <div class="container-fluid mt-2 bg-white pt-3 pb-3 card">
      <div class="container" v-if="shiftData && viewedShift">
        <div class="row mt-4 text-left">
          <div class="col-md-8">
            <div class="form-group bg-white border p-3 shadow-sm">
              <label for="exampleFormControlSelect1">See previous shifts</label>
              <div class="row">
                <div class="col-md 6">
                  <select class="form-control form-control-sm" v-model="viewedShift">
                    <option v-for="shift in shiftData" :value="shift" :key="shift.id">
                      {{ shift.Entry_date.substr(0, 10) + " " + shift.Shift }}
                    </option>
                  </select>
                </div>
              </div>
            </div>
          </div>
          <div class="col-md-4" v-if="edit">
            <div class="form-group bg-white border text-center shadow-sm">
              <router-link
                :to="{ name: 'Insert Patient Data', params: { id: id } }"
                class="special-btn pl-3 pr-3"
              >
                Submit shift data
              </router-link>
            </div>
          </div>
        </div>

        <!-- start viewing follow up data -->

        <div class="infoData">
          <ul
            class="list-unstyled card bg-white shadow"
            :class="viewedShift.Shift.trim() == 'Day' ? 'Day-card' : 'Night-card'"
          >
            <li class="cu-flex detail">
              <span>Inserted From: </span>
              <span>
                {{
                  viewedShift.Insert_Doctor
                    ? "Dr." + viewedShift.Insert_Doctor_Name
                    : "Nurse: " + viewedShift.Insert_Nurse_Name
                }}
              </span>
            </li>
            <li v-if="viewedShift.Update_Doctor" class="cu-flex detail">
              <span>Updated From Doctor: </span>
              <span>
                {{ "Dr." + viewedShift.Update_Doctor_Name }}
                <i class="pl-2 pr-2 text-primary fa fa-clock-o"></i>
                {{ viewedShift.Update_Doctor_Time }}
              </span>
            </li>
            <li v-if="viewedShift.Update_Nurse" class="cu-flex detail">
              <span>Updated From Nurse: </span>
              <span>
                {{ viewedShift.Update_Nurse_Name }}
                <i class="pl-2 pr-2 text-primary fa fa-clock-o"></i>
                {{ viewedShift.Update_Nurse_Time }}
              </span>
            </li>
            <li class="cu-flex detail">
              <span>Shift</span>
              <span>
                <i
                  :class="
                    viewedShift.Shift.trim() == 'Day'
                      ? 'fa fa-sun-o text-primary'
                      : 'fa fa-moon-o  text-primary'
                  "
                ></i>
                {{ viewedShift.Shift }}
              </span>
            </li>
            <li class="cu-flex detail">
              <span>Entry Date</span>
              <span>{{ viewedShift.Entry_date.substr(0, 10) }}</span>
            </li>
          </ul>
        </div>

        <div class="row mt-4 text-left">
          <div class="col-xl-8 col-lg-7">
            <div class="card shadow overflow-auto" style="height: 250px">
              <div
                class="card-header py-3 d-flex flex-row align-items-center justify-content-between"
              >
                <h5 class="m-0">Diagnosis</h5>
              </div>
              <!-- Card Body -->
              <div class="card-body">
                <i
                  class="fa fa-circle text-danger small"
                  v-if="viewedShift.DR_Diagnosis"
                ></i>
                {{ viewedShift.DR_Diagnosis }}
              </div>
            </div>
          </div>

          <div class="col-xl-4 col-lg-5">
            <div class="card shadow overflow-auto" style="height: 250px">
              <div
                class="card-header py-3 d-flex flex-row align-items-center justify-content-between"
              >
                <h5 class="m-0">Progress Notes</h5>
              </div>
              <!-- Card Body -->
              <div class="card-body">
                <i
                  class="fa fa-circle text-danger small"
                  v-if="viewedShift.DR_ProgressNotes"
                ></i>
                {{ viewedShift.DR_ProgressNotes }}
              </div>
            </div>
          </div>
        </div>
        <div class="row mt-4 text-left">
          <div class="col-md-12">
            <div class="card shadow">
              <div
                class="card-header py-3 d-flex flex-row align-items-center justify-content-between"
              >
                <h5 class="m-0">Medical Details</h5>
              </div>
              <div class="card-body row">
                <div class="col-md-6">
                  <h4 class="font-weight-bold text-dark mt-3">
                    Pain Level
                    <span class="float-right text-success">{{ viewedShift.Pain }}</span>
                  </h4>
                  <div class="progress bg-light mb-4" style="height: 60px">
                    <input
                      class="progress-bar bg-danger w-100"
                      type="range"
                      min="0"
                      max="10"
                      step="1"
                      id="volume"
                      :value="viewedShift.Pain"
                      disabled
                    />
                  </div>
                </div>
                <div class="col-md-6">
                  <div class="bg-light pt-2 pb-2 mb-3 w-100 shadow-sm">
                    <span class="ml-2 font-weight-bold text-dark">Fall :</span>
                    <span class="font-weight-normal text-secondary ml-2">{{
                      viewedShift.Fall
                    }}</span>
                  </div>
                </div>
                <div class="col-md-6">
                  <div class="bg-light pt-2 pb-2 mb-3 w-100 shadow-sm">
                    <span class="ml-2 font-weight-bold text-dark">Diet :</span>
                    <span class="font-weight-normal text-secondary ml-2">{{
                      viewedShift.Diet_Name
                    }}</span>
                  </div>
                </div>

                <div class="col-md-6">
                  <div class="bg-light pt-2 pb-2 mb-3 w-100 shadow-sm">
                    <span class="ml-2 font-weight-bold text-dark">Issolation :</span>
                    <span class="font-weight-normal text-secondary ml-2">{{
                      viewedShift.P_Isolation
                    }}</span>
                  </div>
                </div>
                <div class="col-md-12">
                  <div class="bg-light p-3 mb-3 w-100 shadow-sm h-100">
                    <span class="font-weight-bold text-dark">Allergy :</span>
                    <span class="font-weight-normal text-secondary ml-2">{{
                      viewedShift.Allergy
                    }}</span>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>
        <div class="row mt-4 text-left">
          <div class="col-md-3">
            <div class="card shadow overflow-auto" style="height: 250px">
              <div
                class="card-header py-3 d-flex flex-row align-items-center justify-content-between"
              >
                <h5 class="m-0">To Do</h5>
              </div>
              <div class="card-body row m-0">
                <div class="col-md-12">
                  <i
                    class="fa fa-circle text-success small"
                    v-if="viewedShift.Investegation_ToDo"
                  ></i>
                  {{ viewedShift.Investegation_ToDo }}
                </div>
              </div>
            </div>
          </div>
          <div class="col-md-3">
            <div class="card shadow overflow-auto" style="height: 250px">
              <div
                class="card-header py-3 d-flex flex-row align-items-center justify-content-between"
              >
                <h5 class="m-0">Follow up</h5>
              </div>
              <div class="card-body row m-0">
                <div class="col-md-12">
                  <i
                    class="fa fa-circle text-success small"
                    v-if="viewedShift.Investegation_FollowUp"
                  ></i>
                  {{ viewedShift.Investegation_FollowUp }}
                </div>
              </div>
            </div>
          </div>
          <div class="col-md-3">
            <div class="card shadow overflow-auto" style="height: 250px">
              <div
                class="card-header py-3 d-flex flex-row align-items-center justify-content-between"
              >
                <h5 class="m-0">Contraptions &amp; Infusions</h5>
              </div>
              <div class="card-body row m-0">
                <div class="col-md-12">
                  <i
                    class="fa fa-circle text-success small"
                    v-if="viewedShift.Contraptions_Infusions"
                  ></i>
                  {{ viewedShift.Contraptions_Infusions }}
                </div>
              </div>
            </div>
          </div>
          <div class="col-md-3">
            <div class="card shadow overflow-auto" style="height: 250px">
              <div
                class="card-header py-3 d-flex flex-row align-items-center justify-content-between"
              >
                <h5 class="m-0">Routise Plan of Care</h5>
              </div>
              <div class="card-body row m-0">
                <div class="col-md-12">
                  <i
                    class="fa fa-circle text-success small"
                    v-if="viewedShift.Routise_PlanOfCare"
                  ></i>
                  {{ viewedShift.Routise_PlanOfCare }}
                </div>
              </div>
            </div>
          </div>
        </div>
        <div class="row mt-4 text-left">
          <div class="col-md-12">
            <div class="card shadow overflow-auto" style="height: 200px">
              <div
                class="card-header py-3 d-flex flex-row align-items-center justify-content-between"
              >
                <h5 class="m-0">Progress of consultations</h5>
              </div>
              <div class="card-body row m-0">
                <div class="col-md-12">
                  <i
                    class="fa fa-circle text-danger small"
                    v-if="viewedShift.DR_Consultaion_Progress"
                  ></i>
                  {{ viewedShift.DR_Consultaion_Progress }}
                </div>
              </div>
            </div>
          </div>
        </div>
        <!-- end viewing follow up data -->
      </div>

      <div class="text-center not-found" v-else>
        <p>
          <i class="fa fa-warning text-warning"></i> There is no follow up data yet for
          this patient!
        </p>
        <router-link
          v-if="edit"
          :to="{ name: 'Insert Patient Data', params: { id: id } }"
          class="special-btn pl-3 pr-3"
        >
          Submit shift data
        </router-link>
      </div>
    </div>
    <!-- start transfer Modal -->
    <div
      class="modal fade"
      id="exampleModalCenter"
      tabindex="-1"
      role="dialog"
      aria-labelledby="exampleModalCenterTitle"
      aria-hidden="true"
    >
      <div class="modal-dialog modal-dialog-centered" role="document">
        <div class="modal-content">
          <div class="modal-header">
            <h5 class="modal-title" id="exampleModalLongTitle">Transfer To</h5>
            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
              <span aria-hidden="true">&times;</span>
            </button>
          </div>
          <div class="modal-body overflow-auto" style="height: 450px">
            <form @submit.prevent="transform">
              <div class="custom-form pt-1">
                <div class="cu-container">
                  <div class="cu-form-group special" style="max-width: 900px">
                    <div class="cu-field" v-if="Units.length > 0">
                      <h3 class="cu-label">
                        <label>Unit :</label>
                      </h3>
                      <div class="f-select">
                        <select
                          class="form-control form-control-sm"
                          v-model="Unit"
                          required
                        >
                          <option
                            v-for="unit in Units"
                            :value="unit.U_id"
                            :key="unit.U_name"
                          >
                            {{ unit.U_name }}
                          </option>
                        </select>
                      </div>
                    </div>

                    <div class="cu-field" v-if="Rooms.length > 0">
                      <h3 class="cu-label">
                        <label>Room :</label>
                      </h3>
                      <div class="f-select">
                        <select
                          class="form-control form-control-sm"
                          v-model="Room"
                          required
                        >
                          <option
                            v-for="room in Rooms"
                            :value="room.id"
                            :key="room.Room_name"
                          >
                            {{ room.Room_name }}
                          </option>
                        </select>
                      </div>
                    </div>

                    <div class="cu-field" v-if="Beds.length > 0">
                      <h3 class="cu-label">
                        <label>Bed :</label>
                      </h3>
                      <div class="f-select">
                        <select
                          class="form-control form-control-sm"
                          v-model="Bed_id"
                          required
                        >
                          <option v-for="bed in Beds" :value="bed.id" :key="bed.id">
                            {{ bed.Bed_name }}
                          </option>
                        </select>
                      </div>
                    </div>
                    <div class="text-center">
                      <button class="special-btn btn-sm">Submit</button>
                    </div>
                  </div>
                </div>
              </div>
            </form>
          </div>
        </div>
      </div>
    </div>
    <!-- end transfer Modal -->
  </div>
</template>

<script>
export default {
  name: "patientData",
  props: ["link", "user", "UnitDash", "DoctorPatients", "NursesPatients"],
  data() {
    return {
      apiUrl: this.link,
      Units: [],
      Rooms: [],
      Beds: [],
      patientData: null,
      shiftData: null,
      id: this.$route.params.id,
      viewedShift: null,
      edit: false,
      stat: 0,
      today: "",
      dischargeDate: "",
      deathDate: "",

      Unit: "",
      Room: "",
      Bed_id: 0,
    };
  },

  watch: {
    Unit: function () {
      let Unit = this.Unit,
        that = this;

      //get Rooms
      $.ajax({
        type: "POST",
        url: that.apiUrl + "endoresment/patientData.aspx/getRoomsData",
        data: JSON.stringify({ unit: { U_id: Unit } }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
          that.Rooms = JSON.parse(data.d);
        },
      });
    },
    Room: function () {
      let Room = this.Room,
        that = this;

      //get Beds
      $.ajax({
        type: "POST",
        url: that.apiUrl + "endoresment/patientData.aspx/getBedsData",
        data: JSON.stringify({ room: { id: Room } }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
          that.Beds = JSON.parse(data.d);
        },
      });
    },
  },
  methods: {
    LOS(date) {
      let length = Math.round(
        (new Date() - new Date(date.trim())) / (1000 * 60 * 60 * 24)
      );
      return length == 0 ? "Today" : length == 1 ? length + " Day" : length + " Days";
    },
    transform: function () {
      let dataObj = null,
        that = this;

      if ($("form").is(":valid")) {
        let updatedTransform = {
          id: this.id,
          Unit: this.Units.filter((x) => x.U_id == this.Unit)[0].U_name,
          Room: this.Rooms.filter((x) => x.id == this.Room)[0].Room_name,
          Bed_id: this.Bed_id,
        };

        swal({
          title: "Are you sure?",
          buttons: true,
        }).then((confirm) => {
          if (confirm) {
            // 1- update patientData table
            $.ajax({
              type: "POST",
              url: that.apiUrl + "endoresment/patientData.aspx/updateTransform",
              data: JSON.stringify({ detail: updatedTransform }),
              contentType: "application/json; charset=utf-8",
              dataType: "json",
              success: function () {
                // 2- update followUp table
                if (that.shiftData.length > 0) {
                  dataObj = that.shiftData.filter(
                    (x) => x.id == Math.max(...that.shiftData.map((z) => z.id))
                  )[0];
                } else {
                  dataObj = {
                    Patient_id: that.patientData.id,
                    Insert_Nurse: 0,
                    Insert_Nurse_Name: "",
                    Insert_Nurse_Time: 0,
                    Insert_Doctor: 0,
                    Insert_Doctor_Name: "",
                    Insert_Doctor_Time: 0,
                  };
                }
                console.log(dataObj);
                let currentShift = new Date().getHours() >= 20 ? "Night" : "Day";
                dataObj.Transfer_From = that.patientData.Unit;
                dataObj.Transfer_To = that.Units.filter(
                  (x) => x.U_id == that.Unit
                )[0].U_name;
                dataObj.Shift = currentShift;

                if (
                  dataObj.id &&
                  that.LOS(dataObj.Entry_date) == "Today" &&
                  dataObj.Shift.trim() == currentShift
                ) {
                  // update follow up by nurse
                  if (that.user.Role_id == 12 || that.user.Role_id == 17) {
                    dataObj.Update_Nurse = that.user.Emp_id;
                    dataObj.Update_Nurse_Name = that.user.FullName;
                    dataObj.Update_Nurse_Time = moment(new Date()).format(
                      "DD-MM-YYYY A HH-mm"
                    );

                    UpdateData();
                  } else if (that.user.Role_id == 10) {
                    // update follow up by doctor

                    dataObj.Update_Doctor = that.user.Emp_id;
                    dataObj.Update_Doctor_Name = that.user.FullName;
                    dataObj.Update_Doctor_Time = moment(new Date()).format(
                      "DD-MM-YYYY A HH-mm"
                    );

                    UpdateData();
                  }
                } else {
                  // insert into follow up by nurse
                  if (that.user.Role_id == 12 || that.user.Role_id == 17) {
                    dataObj.Insert_Nurse = that.user.Emp_id;
                    dataObj.Insert_Nurse_Name = that.user.FullName;
                    dataObj.Insert_Nurse_Time = moment(new Date()).format(
                      "DD-MM-YYYY A HH-mm"
                    );
                    dataObj.Insert_Doctor = 0;

                    InsertData();
                  } else if (that.user.Role_id == 10) {
                    // insert into follow up by doctor
                    dataObj.Insert_Doctor = that.user.Emp_id;
                    dataObj.Insert_Doctor_Name = that.user.FullName;
                    dataObj.Insert_Doctor_Time = moment(new Date()).format(
                      "DD-MM-YYYY A HH-mm"
                    );
                    dataObj.Insert_Nurse = 0;

                    InsertData();
                  }
                }

                // update units dashboard
                if (that.patientData.Unit != updatedTransform.Unit) {
                  let currentShift = new Date().getHours() >= 20 ? "Night" : "Day";
                  let currentDate = new Date();
                  let updatedUnit = [
                    {
                      Unit_name: that.patientData.Unit,
                      Transfer_In: 0,
                      Transfer_Out: 1,
                      Shift:currentShift,
                      Shift_date:currentDate,
                      Total_Census: -1
                    },
                    {
                      Unit_name: updatedTransform.Unit,
                      Transfer_In: 1,
                      Transfer_Out: 0,
                      Shift:currentShift,
                      Shift_date:currentDate,
                      Total_Census:1
                    },
                  ];
                  $.ajax({
                    type: "POST",
                    url: that.apiUrl + "endoresment/patientData.aspx/updateUnitsTransferOut",
                    data: JSON.stringify({ unit: updatedUnit[0] }),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                  });
                  $.ajax({
                    type: "POST",
                    url: that.apiUrl + "endoresment/patientData.aspx/updateUnitsTransferIn",
                    data: JSON.stringify({ unit: updatedUnit[1] }),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                  });
                }

                //4- update roomsDashboard table
                let updatedBeds = [
                  {
                    id: that.patientData.Bed_id,
                    Status_id: 1,
                    Status_name: "Empty",
                  },
                  {
                    id: that.Bed_id,
                    Status_id: 2,
                    Status_name: "Full",
                  },
                ];
                for (let i = 0; i < updatedBeds.length; i++) {
                  $.ajax({
                    type: "POST",
                    url: that.apiUrl + "endoresment/patientData.aspx/updateRoomData",
                    data: JSON.stringify({
                      room: {
                        id: updatedBeds[i].id,
                        Status_id: updatedBeds[i].Status_id,
                        Status_name: updatedBeds[i].Status_name,
                      },
                    }),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                  });
                }

                swal({
                  title: "Sweet!",
                  text: "You successfully submit transfer ...",
                  icon: "success",
                  dangerMode: true,
                }).then(x => {
                  location.reload();
                });
              },
            });
          }
        });
      } else {
        swal({
          icon: "warning",
          dangerMode: true,
          text: "Please Fill All Fields!",
        });
      }

      function InsertData() {
        $.ajax({
          type: "POST",
          url: that.apiUrl + "endoresment/patientData.aspx/insertFollowUpData",
          data: JSON.stringify({ patient: dataObj }),
          contentType: "application/json; charset=utf-8",
          dataType: "json",
        });
      }

      function UpdateData() {
        $.ajax({
          type: "POST",
          url: that.apiUrl + "endoresment/patientData.aspx/updateFollowUpData",
          data: JSON.stringify({ patient: dataObj }),
          contentType: "application/json; charset=utf-8",
          dataType: "json",
        });
      }
    },
    updateStat(stat, Id) {
      if (this.dischargeDate || this.deathDate) {
        let that = this;
        swal({
          title: "Are you sure?",
          icon: "warning",
          buttons: true,
          dangerMode: true,
        }).then((confirm) => {
          if (confirm) {
            let obj = {
              id: that.id,
              Patient_Status: stat,
              Discharged_date: that.dischargeDate,
              Death_date: that.deathDate,
            };
            $.ajax({
              type: "POST",
              url: that.apiUrl + "endoresment/patientData.aspx/updatePatientData",
              data: JSON.stringify({ patient: obj }),
              contentType: "application/json; charset=utf-8",
              dataType: "json",
              success: function (data) {
                let bed = {
                  id: Id,
                  Status_id: 1,
                  Status_name: "Empty",
                };
                $.ajax({
                  type: "POST",
                  url: that.apiUrl + "endoresment/patientData.aspx/updateRoomData",
                  data: JSON.stringify({ room: bed }),
                  contentType: "application/json; charset=utf-8",
                  dataType: "json",
                });

                // update unitsdashboard
                let currentShift = new Date().getHours() >= 20 ? "Night" : "Day";
                let currentDate = that.dischargeDate
                  ? that.dischargeDate
                  : that.deathDate;

                let Unitobj = {
                  Unit_name: that.patientData.Unit,
                  Shift: currentShift,
                  Shift_date: currentDate,
                  Death: stat == 3 ? 1 : 0,
                  Discharge: stat == 2 ? 1 : 0,
                };

                $.ajax({
                  type: "POST",
                  url: that.apiUrl + "endoresment/patientData.aspx/updateUnitsData",
                  data: JSON.stringify({ unit: Unitobj }),
                  contentType: "application/json; charset=utf-8",
                  dataType: "json",
                });

                swal({
                  title: "Updated!",
                  icon: "success",
                });

                that.patientData.Patient_Status = stat;
              },
            });
          }
        });
      } else {
        swal({
          title: "Enter Date!",
          icon: "warning",
        });
      }
    },
  },
  created() {
    let that = this;
    that.today = moment(new Date()).format("YYYY-MM-DD");

    //get Units
    $.ajax({
      type: "POST",
      url: that.apiUrl + "endoresment/patientData.aspx/getUnitsData",
      data: JSON.stringify({ branch: { id: that.user.Branch_ID } }),
      contentType: "application/json; charset=utf-8",
      dataType: "json",
      success: function (data) {
        that.Units = JSON.parse(data.d);
      },
    });

    // get patient data
    $.ajax({
      type: "POST",
      url: that.apiUrl + "endoresment/patientData.aspx/getPatientData",
      data: JSON.stringify({ id: { id: that.id } }),
      contentType: "application/json; charset=utf-8",
      dataType: "json",
      success: function (data) {
        that.patientData = JSON.parse(data.d)[0];
        that.stat = that.patientData.Patient_Status;

        // if user is a nurse check if the patient is the nurses patient or not
        if (that.user.Role_id == 12) {
          that.edit = that.NursesPatients.filter((x) => x.id == that.id).length > 0;
        }
        // if the user is a charge nurse check if the patient located in the same unit as current charge nurse
        else if (that.user.Role_id == 17) {
          that.edit =
            that.UnitDash.filter(
              (x) => x.Unit_name.trim() == that.patientData.Unit.trim()
            ).length > 0
              ? true
              : false;
        }

        // if the user is a doctor check if the patient is his/her patient
        else if (that.user.Role_id == 10) {
          that.edit = that.DoctorPatients.filter((x) => x.id == that.id).length > 0;
        }
      },
    });

    // get latest shift data
    $.ajax({
      type: "POST",
      url: that.apiUrl + "endoresment/patientData.aspx/getShiftData",
      data: JSON.stringify({ data: { Patient_id: that.id } }),
      contentType: "application/json; charset=utf-8",
      dataType: "json",
      success: function (data) {
        that.shiftData = JSON.parse(data.d);
        that.viewedShift = that.shiftData ? that.shiftData.slice(-1)[0] : null;
      },
    });
  },
};
</script>

<style scoped>
.viewPatients {
  min-height: 100vh;
  background-color: #f6f8fb;
  position: relative;
  overflow: hidden;
  font-size: 16px;
}
.head-info {
  background-color: #f6f8fb;
}
.infoData ul li {
  padding: 15px 30px;
}
.infoData ul li:nth-of-type(2n) {
  background-color: #f7f7f7;
}
.btn-options button {
  max-width: 120px;
  margin: auto;
}
</style>
