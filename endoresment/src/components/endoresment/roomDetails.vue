<template>
  <div class="roomDetails p-3 text-capitalize">
    <!-------------------------------------------------------------------------------------------------------------->
    <!-- start select unit -->
    <div class="container-fluid mt-2 card" id="unit">
      <div class="container-wave">
        <div class="wave"></div>
      </div>

      <div
        class="text-secondary card bg-light select-form p-4 text-center shadow overflow-auto"
      >
        <label class="text-secondary">
          <span>select unit :</span>
        </label>
        <div class="bg-white w-75 m-auto form-group">
          <select class="w-100 h-100 card form-control" v-model="selectedUnit">
            <option v-for="unit in getUnitsData" :key="unit.U_id" :value="unit.U_id">
              {{ unit.U_name }}
            </option>
          </select>
        </div>
        <button class="btn btn-secondary w-25 m-auto" @click.prevent="selectUnit">
          show unit details
        </button>
      </div>
    </div>
    <!-- end select unit -->
    <!-------------------------------------------------------------------------------------------------------------->
    <!-- start unit title -->
    <div class="container-fluid mt-2 bg-white pt-3 pb-3 card d-none" id="title">
      <button
        class="btn btn-secondary btn-sm mr-0"
        style="width: 70px; position: absolute; right: 5%"
        @click.prevent="back"
      >
        <i class="fa fa-arrow-left mr-2" aria-hidden="true"></i>back
      </button>
      <div class="container text-center">
        <h1
          v-if="selectedUnit != 0"
          class="mt-4 text-secondary"
          style="text-decoration: underline"
        >
          {{ getUnitsData.filter((x) => x.U_id == selectedUnit)[0].U_name }} ......
          <span class="badge badge-secondary"
            >{{
              getRoomsData.filter((x) => x.unit_id == selectedUnit).length
            }}
            rooms</span
          >
        </h1>
      </div>
    </div>
    <!-- end unit title -->
    <!-------------------------------------------------------------------------------------------------------------->
    <!-- start unit rooms -->
    <div
      class="container-fluid mt-2 bg-white pt-3 pb-3 card viewPatients d-none"
      id="rooms"
    >
      <div class="container" v-if="filterUnits.length > 0">
        <!--- start double rooms --->

        <h4
          v-if="filterUnits.filter((x) => x.Room_type_id == 1).length > 0"
          class="text-secondary text-left"
          style="text-decoration: underline"
        >
          double rooms ----
          <span class="badge badge-secondary">{{
            filterUnits.filter((x) => x.Room_type_id == 1).length
          }}</span>
        </h4>
        <div
          class="row text-center"
          v-if="filterUnits.filter((x) => x.Room_type_id == 1).length > 0"
        >
          <div
            class="col-md-4 p-2"
            v-for="room in filterUnits.filter((x) => x.Room_type_id == 1)"
            :key="room.R_id"
          >
            <div class="bg-light shadow p-2 container card">
              <h4
                class="text-secondary font-weight-bold mb-3"
                style="text-decoration: underline"
              >
                room
                <span class="text-info">{{ room.Room_name }}</span>
              </h4>

              <div class="shadow bg-white rounded row w-100 m-auto p-2">
                <div
                  class="col-md-6 p-2"
                  v-for="bed in RoomsDashboard.filter((x) => x.Room_id == room.id)"
                  :key="bed.id"
                >
                  <div
                    class="card p-2 bg-light shadow icon"
                    style="
                      height: 200px;
                      transition: 0.4s cubic-bezier(0.68, -0.55, 0.265, 1.55);
                      position: relative;
                    "
                  >
                    <div class="h-25 bg-light mb-2 rounded pt-2 shadow">
                      <span class="text-dark">{{ bed.Bed_name.substring(0, 4) }}</span>
                      <span
                        :class="
                          bed.Status_id == 1
                            ? 'ml-2 badge-success pl-2 pr-2 rounded'
                            : bed.Status_id == 2
                            ? 'ml-2 badge-danger pl-2 pr-2 rounded'
                            : bed.Status_id == 3
                            ? 'ml-2 badge-warning pl-2 pr-2 rounded'
                            : ' '
                        "
                        >{{ bed.Bed_name[4] }}</span
                      >
                    </div>
                    <div
                      class="h-75 bg-light rounded pt-3 bed-background shadow card"
                    ></div>
                    <div
                      class="tooltip bg-light card p-4 text-secondary text-capitalize"
                      v-if="bed.Status_id == 2"
                    >
                      <h6>
                        <span class="text-info">full name :</span
                        ><span class="ml-2">{{
                          PatientsData.filter((x) => x.Bed_id == bed.id)[0]
                            .Patient_FullName
                        }}</span>
                      </h6>
                      <h6>
                        <span class="text-info">age : </span
                        ><span class="ml-2">{{
                          PatientsData.filter((x) => x.Bed_id == bed.id)[0].Age
                        }}</span>
                      </h6>
                      <h6>
                        <span class="text-info">gender : </span
                        ><span class="ml-2">{{
                          PatientsData.filter((x) => x.Bed_id == bed.id)[0].Gender
                        }}</span>
                      </h6>
                      <h6>
                        <span class="text-info">medical number : </span
                        ><span class="ml-2">{{
                          PatientsData.filter((x) => x.Bed_id == bed.id)[0].Medical_Number
                        }}</span>
                      </h6>
                      <h6>
                        <span class="text-info">specialty : </span
                        ><span class="ml-2">{{
                          PatientsData.filter((x) => x.Bed_id == bed.id)[0].Specialty
                        }}</span>
                      </h6>
                      <button class="btn mt-3 btn-info btn-sm rounded">Details</button>
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>
        <!--- end double rooms --->
        <hr />
        <!--- start single rooms --->
        <h4
          v-if="filterUnits.filter((x) => x.Room_type_id == 2).length > 0"
          class="text-secondary text-left mt-5"
          style="text-decoration: underline"
        >
          single rooms ----
          <span class="badge badge-secondary">{{
            filterUnits.filter((x) => x.Room_type_id == 2).length
          }}</span>
        </h4>
        <div
          class="row text-center"
          v-if="filterUnits.filter((x) => x.Room_type_id == 2).length > 0"
        >
          <div
            class="col-md-4 p-2"
            v-for="room in filterUnits.filter((x) => x.Room_type_id == 2)"
            :key="room.R_id"
          >
            <div class="bg-light shadow p-2 container card">
              <h4
                class="text-secondary font-weight-bold mb-3"
                style="text-decoration: underline"
              >
                room
                <span class="text-info">{{ room.Room_name }}</span>
              </h4>

              <div class="shadow bg-white rounded row w-100 m-auto p-2">
                <div class="col-md-3 p-2"></div>
                <div class="col-md-6 p-2">
                  <div class="card p-2 bg-light shadow" style="height: 200px">
                    <div class="h-25 bg-light mb-2 rounded pt-2 shadow">
                      <span
                        :class="
                          RoomsDashboard.filter((x) => x.Room_id == room.id)[0]
                            .Status_id == 1
                            ? 'ml-2 badge-success pl-3 pr-3 rounded'
                            : RoomsDashboard.filter((x) => x.Room_id == room.id)[0]
                                .Status_id == 2
                            ? 'ml-2 badge-danger pl-3 pr-3 rounded'
                            : RoomsDashboard.filter((x) => x.Room_id == room.id)[0]
                                .Status_id == 3
                            ? 'ml-2 badge-warning pl-3 pr-3 rounded'
                            : ' '
                        "
                      ></span>
                    </div>
                    <div
                      class="h-75 bg-light rounded pt-3 shadow card bed-background-single"
                    ></div>
                    <div
                      class="tooltip bg-light card p-4 text-secondary"
                      v-if="
                        RoomsDashboard.filter((x) => x.Room_id == room.id)[0].Status_id ==
                        2
                      "
                    >
                      <h6>
                        <span class="text-info">Full Name :</span
                        ><span class="ml-2">{{
                          PatientsData.filter((x) => x.Room == room.Room_name)[0]
                            .Patient_FullName
                        }}</span>
                      </h6>
                      <h6>
                        <span class="text-info">Age : </span
                        ><span class="ml-2">{{
                          PatientsData.filter((x) => x.Room == room.Room_name)[0].Age
                        }}</span>
                      </h6>
                      <h6>
                        <span class="text-info">Gender : </span
                        ><span class="ml-2">{{
                          PatientsData.filter((x) => x.Room == room.Room_name)[0].Gender
                        }}</span>
                      </h6>
                      <h6>
                        <span class="text-info">Medical Number :</span
                        ><span class="ml-2">{{
                          PatientsData.filter((x) => x.Room == room.Room_name)[0]
                            .Medical_Number
                        }}</span>
                      </h6>
                      <h6>
                        <span class="text-info">Specialty : </span
                        ><span class="ml-2">{{
                          PatientsData.filter((x) => x.Room == room.Room_name)[0]
                            .Specialty
                        }}</span>
                      </h6>
                      <button class="btn mt-3 btn-info btn-sm rounded">Details</button>
                    </div>
                  </div>
                </div>
                <div class="col-md-3 p-2"></div>
              </div>
            </div>
          </div>
        </div>
        <!--- end single rooms --->
        <hr />
        <!--- start suits --->
        <h4
          v-if="filterUnits.filter((x) => x.Room_type_id == 3).length > 0"
          class="text-secondary text-left mt-5"
          style="text-decoration: underline"
        >
          suits ----
          <span class="badge badge-secondary">{{
            filterUnits.filter((x) => x.Room_type_id == 3).length
          }}</span>
        </h4>
        <div
          class="row text-center"
          v-if="filterUnits.filter((x) => x.Room_type_id == 3).length > 0"
        >
          <div
            class="col-md-4 p-2"
            v-for="room in filterUnits.filter((x) => x.Room_type_id == 3)"
            :key="room.R_id"
          >
            <div class="bg-light shadow p-2 container card">
              <h4
                class="text-secondary font-weight-bold mb-3"
                style="text-decoration: underline"
              >
                room
                <span class="text-info">{{ room.Room_name }}</span>
              </h4>

              <div class="shadow bg-white rounded row w-100 m-auto p-2">
                <div class="col-md-3 p-2"></div>
                <div class="col-md-6 p-2">
                  <div class="card p-2 bg-light shadow" style="height: 200px">
                    <div class="h-25 bg-light mb-2 rounded pt-2 shadow">
                      <span
                        :class="
                          RoomsDashboard.filter((x) => x.Room_id == room.id)[0]
                            .Status_id == 1
                            ? 'ml-2 badge-success pl-3 pr-3 rounded'
                            : RoomsDashboard.filter((x) => x.Room_id == room.id)[0]
                                .Status_id == 2
                            ? 'ml-2 badge-danger pl-3 pr-3 rounded'
                            : RoomsDashboard.filter((x) => x.Room_id == room.id)[0]
                                .Status_id == 3
                            ? 'ml-2 badge-warning pl-3 pr-3 rounded'
                            : ' '
                        "
                      ></span>
                    </div>
                    <div
                      class="h-75 bg-light rounded pt-3 shadow card bed-background-single"
                    ></div>
                    <div
                      class="tooltip bg-light card p-4 text-secondary"
                      v-if="
                        RoomsDashboard.filter((x) => x.Room_id == room.id)[0].Status_id ==
                        2
                      "
                    >
                      <h6>
                        <span class="text-info">Full Name :</span
                        ><span class="ml-2">{{
                          PatientsData.filter((x) => x.Room == room.Room_name)[0]
                            .Patient_FullName
                        }}</span>
                      </h6>
                      <h6>
                        <span class="text-info">Age : </span
                        ><span class="ml-2">{{
                          PatientsData.filter((x) => x.Room == room.Room_name)[0].Age
                        }}</span>
                      </h6>
                      <h6>
                        <span class="text-info">Gender : </span
                        ><span class="ml-2">{{
                          PatientsData.filter((x) => x.Room == room.Room_name)[0].Gender
                        }}</span>
                      </h6>
                      <h6>
                        <span class="text-info">Medical Number :</span
                        ><span class="ml-2">{{
                          PatientsData.filter((x) => x.Room == room.Room_name)[0]
                            .Medical_Number
                        }}</span>
                      </h6>
                      <h6>
                        <span class="text-info">Specialty : </span
                        ><span class="ml-2">{{
                          PatientsData.filter((x) => x.Room == room.Room_name)[0]
                            .Specialty
                        }}</span>
                      </h6>
                      <button class="btn mt-3 btn-info btn-sm rounded">Details</button>
                    </div>
                  </div>
                </div>
                <div class="col-md-3 p-2"></div>
              </div>
            </div>
          </div>
        </div>
        <!--- end suits --->
        <hr />
        <!--- start daycase rooms --->
        <h4
          v-if="filterUnits.filter((x) => x.Room_type_id == 4).length > 0"
          class="text-secondary text-left mt-5"
          style="text-decoration: underline"
        >
          daycase rooms ----
          <span class="badge badge-secondary">{{
            filterUnits.filter((x) => x.Room_type_id == 4).length
          }}</span>
        </h4>
        <div
          class="row text-center"
          v-if="filterUnits.filter((x) => x.Room_type_id == 4).length > 0"
        >
          <div
            class="col-md-4 p-2"
            v-for="room in filterUnits.filter((x) => x.Room_type_id == 4)"
            :key="room.R_id"
          >
            <div class="bg-light shadow p-2 container card">
              <h4
                class="text-secondary font-weight-bold mb-3"
                style="text-decoration: underline"
              >
                room
                <span class="text-info">{{ room.Room_name }}</span>
              </h4>

              <div class="shadow bg-white rounded row w-100 m-auto p-2">
                <div class="col-md-3 p-2"></div>
                <div class="col-md-6 p-2">
                  <div class="card p-2 bg-light shadow" style="height: 200px">
                    <div class="h-25 bg-light mb-2 rounded pt-2 shadow">
                      <span
                        :class="
                          RoomsDashboard.filter((x) => x.Room_id == room.id)[0]
                            .Status_id == 1
                            ? 'ml-2 badge-success pl-3 pr-3 rounded'
                            : RoomsDashboard.filter((x) => x.Room_id == room.id)[0]
                                .Status_id == 2
                            ? 'ml-2 badge-danger pl-3 pr-3 rounded'
                            : RoomsDashboard.filter((x) => x.Room_id == room.id)[0]
                                .Status_id == 3
                            ? 'ml-2 badge-warning pl-3 pr-3 rounded'
                            : ' '
                        "
                      ></span>
                    </div>
                    <div
                      class="h-75 bg-light rounded pt-3 shadow card bed-background-single"
                    ></div>
                    <div
                      class="tooltip bg-light card p-4 text-secondary"
                      v-if="
                        RoomsDashboard.filter((x) => x.Room_id == room.id)[0].Status_id ==
                        2
                      "
                    >
                      <h6>
                        <span class="text-info">Full Name :</span
                        ><span class="ml-2">{{
                          PatientsData.filter((x) => x.Room == room.Room_name)[0]
                            .Patient_FullName
                        }}</span>
                      </h6>
                      <h6>
                        <span class="text-info">Age : </span
                        ><span class="ml-2">{{
                          PatientsData.filter((x) => x.Room == room.Room_name)[0].Age
                        }}</span>
                      </h6>
                      <h6>
                        <span class="text-info">Gender : </span
                        ><span class="ml-2">{{
                          PatientsData.filter((x) => x.Room == room.Room_name)[0].Gender
                        }}</span>
                      </h6>
                      <h6>
                        <span class="text-info">Medical Number :</span
                        ><span class="ml-2">{{
                          PatientsData.filter((x) => x.Room == room.Room_name)[0]
                            .Medical_Number
                        }}</span>
                      </h6>
                      <h6>
                        <span class="text-info">Specialty : </span
                        ><span class="ml-2">{{
                          PatientsData.filter((x) => x.Room == room.Room_name)[0]
                            .Specialty
                        }}</span>
                      </h6>
                      <button class="btn mt-3 btn-info btn-sm rounded">Details</button>
                    </div>
                  </div>
                </div>
                <div class="col-md-3 p-2"></div>
              </div>
            </div>
          </div>
        </div>
        <!--- end daycase rooms --->
        <hr />
        <!--- start ccu rooms --->
        <h4
          v-if="filterUnits.filter((x) => x.Room_type_id == 5).length > 0"
          class="text-secondary text-left mt-5"
          style="text-decoration: underline"
        >
          ccu rooms ----
          <span class="badge badge-secondary">{{
            filterUnits.filter((x) => x.Room_type_id == 5).length
          }}</span>
        </h4>
        <div
          class="row text-center"
          v-if="filterUnits.filter((x) => x.Room_type_id == 5).length > 0"
        >
          <div
            class="col-md-4 p-2"
            v-for="room in filterUnits.filter((x) => x.Room_type_id == 5)"
            :key="room.R_id"
          >
            <div class="bg-light shadow p-2 container card">
              <h4
                class="text-secondary font-weight-bold mb-3"
                style="text-decoration: underline"
              >
                room
                <span class="text-info">{{ room.Room_name }}</span>
              </h4>

              <div class="shadow bg-white rounded row w-100 m-auto p-2">
                <div class="col-md-3 p-2"></div>
                <div class="col-md-6 p-2">
                  <div class="card p-2 bg-light shadow" style="height: 200px">
                    <div class="h-25 bg-light mb-2 rounded pt-2 shadow">
                      <span
                        :class="
                          RoomsDashboard.filter((x) => x.Room_id == room.id)[0]
                            .Status_id == 1
                            ? 'ml-2 badge-success pl-3 pr-3 rounded'
                            : RoomsDashboard.filter((x) => x.Room_id == room.id)[0]
                                .Status_id == 2
                            ? 'ml-2 badge-danger pl-3 pr-3 rounded'
                            : RoomsDashboard.filter((x) => x.Room_id == room.id)[0]
                                .Status_id == 3
                            ? 'ml-2 badge-warning pl-3 pr-3 rounded'
                            : ' '
                        "
                      ></span>
                    </div>
                    <div
                      class="h-75 bg-light rounded pt-3 shadow card bed-background-single"
                    ></div>
                    <div
                      class="tooltip bg-light card p-4 text-secondary"
                      v-if="
                        RoomsDashboard.filter((x) => x.Room_id == room.id)[0].Status_id ==
                        2
                      "
                    >
                      <h6>
                        <span class="text-info">Full Name :</span
                        ><span class="ml-2">{{
                          PatientsData.filter((x) => x.Room == room.Room_name)[0]
                            .Patient_FullName
                        }}</span>
                      </h6>
                      <h6>
                        <span class="text-info">Age : </span
                        ><span class="ml-2">{{
                          PatientsData.filter((x) => x.Room == room.Room_name)[0].Age
                        }}</span>
                      </h6>
                      <h6>
                        <span class="text-info">Gender : </span
                        ><span class="ml-2">{{
                          PatientsData.filter((x) => x.Room == room.Room_name)[0].Gender
                        }}</span>
                      </h6>
                      <h6>
                        <span class="text-info">Medical Number :</span
                        ><span class="ml-2">{{
                          PatientsData.filter((x) => x.Room == room.Room_name)[0]
                            .Medical_Number
                        }}</span>
                      </h6>
                      <h6>
                        <span class="text-info">Specialty : </span
                        ><span class="ml-2">{{
                          PatientsData.filter((x) => x.Room == room.Room_name)[0]
                            .Specialty
                        }}</span>
                      </h6>
                      <button class="btn mt-3 btn-info btn-sm rounded">Details</button>
                    </div>
                  </div>
                </div>
                <div class="col-md-3 p-2"></div>
              </div>
            </div>
          </div>
        </div>
        <!--- end ccu rooms --->
      </div>
      <div v-else class="bg-light p-5 card shadow text-secondary w-75 m-auto">
        no avalible rooms in this unit
      </div>
    </div>
    <!-- end unit rooms -->
    <!-------------------------------------------------------------------------------------------------------------------->
  </div>
</template>

<script>
export default {
  name: "roomDetails",
  props: ["link"],
  data() {
    return {
      getUnitsData: [],
      getRoomsData: [],
      RoomsDashboard: [],
      PatientsData: [],
      filterUnits: [],
      selectedUnit: 0,
      apiUrl: `http://localhost:${this.link}/endoresment/dist/`,
    };
  },
  methods: {
    selectUnit: function () {
      if (this.selectedUnit == 0) {
        swal({
          title: "Error!",
          text: "Sorry, you should select the Unit ! ",
          icon: "warning",
          dangerMode: true,
        });
      } else {
        let unit = document.getElementById("unit"),
          title = document.getElementById("title"),
          rooms = document.getElementById("rooms");

        unit.classList.add("d-none");
        title.classList.remove("d-none");
        rooms.classList.remove("d-none");

        this.filterUnits = this.getRoomsData.filter(
          (x) => x.unit_id == this.selectedUnit
        );
      }
    },
    back: function () {
      let unit = document.getElementById("unit"),
        title = document.getElementById("title"),
        rooms = document.getElementById("rooms");

      unit.classList.remove("d-none");
      title.classList.add("d-none");
      rooms.classList.add("d-none");
    },
  },
  created() {
    let that = this;
    //get units
    $.ajax({
      type: "POST",
      url: that.apiUrl + "endoresment/roomDetails.aspx/getUnitsData",
      contentType: "application/json; charset=utf-8",
      dataType: "json",
      success: function (data) {
        that.getUnitsData = JSON.parse(data.d);
      },
    });

    //get rooms
    $.ajax({
      type: "POST",
      url: that.apiUrl + "endoresment/roomDetails.aspx/getRoomsData",
      contentType: "application/json; charset=utf-8",
      dataType: "json",
      success: function (data) {
        that.getRoomsData = JSON.parse(data.d);
      },
    });

    //get rooms dashboards
    $.ajax({
      type: "POST",
      url: that.apiUrl + "endoresment/roomDetails.aspx/getRoomsDashboardData",
      contentType: "application/json; charset=utf-8",
      dataType: "json",
      success: function (data) {
        that.RoomsDashboard = JSON.parse(data.d);
      },
    });

    // get patients data
    $.ajax({
      type: "POST",
      url: that.apiUrl + "endoresment/roomDetails.aspx/getPatientsData",
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
.viewPatients {
  min-height: 500px;
  background: #fff url("../../assets/layout/img/humaaans-doctors.png") center no-repeat;
  background-size: contain;
  background-color: #f6f8fb;
  text-align: center;
  position: relative;
  overflow: hidden;
}
.bed-background {
  background: #fff url("../../assets/layout/img/bed5.png") center no-repeat;
  background-size: cover;
  overflow: hidden;
  width: 100%;
}
.bed-background-single {
  background: #fff url("../../assets/layout/img/bed6.png") center no-repeat;
  background-size: cover;
  overflow: hidden;
  width: 100%;
}
.tooltip {
  position: absolute;
  top: 0;
  left: -100px;
  width: 300px;
  z-index: 1;
  font-size: 15px;
  font-weight: 500;
  border-radius: 25px;
  opacity: 0;
  pointer-events: none;
  box-shadow: 0px 10px 10px rgba(0, 0, 0, 0.1);
  transition: 0.4s cubic-bezier(0.68, -0.55, 0.265, 1.55);
}

.icon:hover .tooltip {
  top: -70px;
  opacity: 1;
  pointer-events: auto;
}

.icon .tooltip:before {
  position: absolute;
  content: "";
  height: 15px;
  width: 15px;
  background: #fff;
  left: 50%;
  bottom: -6px;
  transform: translateX(-50%) rotate(45deg);
  transition: 0.4s cubic-bezier(0.68, -0.55, 0.265, 1.55);
}

.icon:hover .name {
  color: #fff;
}

.icon:hover .name,
.icon:hover .tooltip {
  text-shadow: 0px -1px 0px rgba(0, 0, 0, 0.4);
}
</style>
