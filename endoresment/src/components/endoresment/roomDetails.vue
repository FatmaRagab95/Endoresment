<template>
  <div class="roomDetails p-3 text-capitalize">
    <!-- start unit title -->
    <div class="container-fluid mt-2 bg-white pt-3 pb-3 card">
      <div class="container text-center">
        <h1 class="mt-4 text-secondary" style="text-decoration: underline">
          {{ getUnitsData.filter((x) => x.U_id == path)[0].U_name }} ......
          <span class="badge badge-secondary"
            >{{ getRoomsData.filter((x) => x.unit_id == path).length }} rooms</span
          >
        </h1>
      </div>
    </div>
    <!-- end unit title -->
    <!-------------------------------------------------------------------------------------------------------------->
    <!-- start unit rooms -->
    <div class="container-fluid mt-2 bg-white pt-3 pb-3 card viewPatients">
      <div
        class="container"
        v-if="getRoomsData.filter((x) => x.unit_id == path).length > 0"
      >
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
                    <div
                      class="tooltip bg-light card p-4 text-center"
                      v-else-if="bed.Status_id == 1"
                    >
                      <button
                        class="btn btn-danger btn-sm rounded open-pop shadow-sm"
                        data-pop-name="patient-popup"
                        v-on:click.prevent="popUp(bed, 'patientPop')"
                      >
                        patient registration
                      </button>
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
            <div
              class="bg-light shadow p-2 container card icon"
              style="
                transition: 0.4s cubic-bezier(0.68, -0.55, 0.265, 1.55);
                position: relative;
              "
            >
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
                    <div
                      class="tooltip bg-light card p-4 text-center"
                      v-else-if="
                        RoomsDashboard.filter((x) => x.Room_id == room.id)[0].Status_id ==
                        1
                      "
                    >
                      <button
                        class="btn align-middle btn-danger btn-sm rounded open-pop shadow-sm m-auto"
                        data-pop-name="patient-popup"
                        v-on:click.prevent="
                          popUp(
                            RoomsDashboard.filter((x) => x.Room_id == room.id),
                            'patientPop'
                          )
                        "
                      >
                        patient registration
                      </button>
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
            <div
              class="bg-light shadow p-2 container card icon"
              style="
                transition: 0.4s cubic-bezier(0.68, -0.55, 0.265, 1.55);
                position: relative;
              "
            >
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
                    <div
                      class="tooltip bg-light card p-4 text-center"
                      v-else-if="
                        RoomsDashboard.filter((x) => x.Room_id == room.id)[0].Status_id ==
                        1
                      "
                    >
                      <button
                        class="btn align-middle btn-danger btn-sm rounded open-pop shadow-sm"
                        data-pop-name="patient-popup"
                        v-on:click.prevent="
                          popUp(
                            RoomsDashboard.filter((x) => x.Room_id == room.id),
                            'patientPop'
                          )
                        "
                      >
                        patient registration
                      </button>
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
            <div
              class="bg-light shadow p-2 container card icon"
              style="
                transition: 0.4s cubic-bezier(0.68, -0.55, 0.265, 1.55);
                position: relative;
              "
            >
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
                    <div
                      class="tooltip bg-light card p-4 text-center"
                      v-else-if="
                        RoomsDashboard.filter((x) => x.Room_id == room.id)[0].Status_id ==
                        1
                      "
                    >
                      <button
                        class="btn align-middle btn-danger btn-sm rounded open-pop shadow-sm"
                        data-pop-name="patient-popup"
                        v-on:click.prevent="
                          popUp(
                            RoomsDashboard.filter((x) => x.Room_id == room.id),
                            'patientPop'
                          )
                        "
                      >
                        patient registration
                      </button>
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
            <div
              class="bg-light shadow p-2 container card icon"
              style="
                transition: 0.4s cubic-bezier(0.68, -0.55, 0.265, 1.55);
                position: relative;
              "
            >
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
                    <div
                      class="tooltip bg-light card p-4 text-center"
                      v-else-if="
                        RoomsDashboard.filter((x) => x.Room_id == room.id)[0].Status_id ==
                        1
                      "
                    >
                      <button
                        class="btn align-middle btn-danger btn-sm rounded open-pop shadow-sm"
                        data-pop-name="patient-popup"
                        v-on:click.prevent="
                          popUp(
                            RoomsDashboard.filter((x) => x.Room_id == room.id),
                            'patientPop'
                          )
                        "
                      >
                        patient registration
                      </button>
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
    <!-- start patient registration popup-->
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
                <span class="bg-secondary pt-2 pb-2 pl-3 pr-3">patient registration</span>
              </div>

              <!--- patient-info -->
              <div class="col-md-12">
                <div>
                  <div class="social-timeline-card shadow">
                    <div class="card-body">
                      <div
                        class="general info tabcontent active overflow-auto text-center"
                        style="max-height: 380px"
                      >
                        <form @submit.prevent="InsertPatient(patientDetails)">
                          <div
                            class="cu-form-group special shadow-sm grouped rounded mb-2 bg-light pr-3 pl-3 ml-auto mr-auto text-left"
                          >
                            <div class="row">
                              <!-- patient name -->
                              <div class="cu-field col-md-12">
                                <h3 class="cu-label">
                                  <label>patient name :</label>
                                </h3>
                                <div class="cu-input text-box" style="max-width: 100%">
                                  <span class="fa fa-edit"></span>
                                  <input
                                    type="text"
                                    v-model="newPatient.Patient_FullName"
                                    required
                                  />
                                </div>
                              </div>

                              <!-- patient age -->
                              <div class="cu-field col-md-6">
                                <h3 class="cu-label">
                                  <label>patient age :</label>
                                </h3>
                                <div class="cu-input text-box">
                                  <span class="fa fa-edit"></span>
                                  <input type="text" v-model="newPatient.Age" required />
                                </div>
                              </div>

                              <!-- patient gender -->
                              <div class="cu-field col-md-6">
                                <h3 class="cu-label">
                                  <label>patient gender :</label>
                                </h3>
                                <div class="cu-input text-box">
                                  <span class="fa fa-edit"></span>
                                  <input
                                    type="text"
                                    v-model="newPatient.Gender"
                                    required
                                  />
                                </div>
                              </div>

                              <!-- specialty -->
                              <div class="cu-field col-md-6">
                                <h3 class="cu-label">
                                  <label>specialty :</label>
                                </h3>
                                <div class="cu-input text-box">
                                  <span class="fa fa-edit"></span>
                                  <select
                                    class="w-100 form-control"
                                    v-model="newPatient.Specialty"
                                    required
                                  >
                                    <option>........</option>
                                    <option
                                      v-for="spicial in Specialities"
                                      :key="spicial.Spcy_id"
                                      :value="spicial.Spcy_name_En"
                                    >
                                      {{ spicial.Spcy_name_En }}
                                    </option>
                                  </select>
                                </div>
                              </div>

                              <!-- addmission date -->
                              <div class="cu-field col-md-6">
                                <h3 class="cu-label">
                                  <label>addmission date :</label>
                                </h3>
                                <div class="cu-input text-box">
                                  <span class="fa fa-edit"></span>
                                  <input
                                    type="date"
                                    :min="new Date().toISOString().substring(0, 10)"
                                    v-model="newPatient.Addmission_date"
                                    required
                                  />
                                </div>
                              </div>
                            </div>
                          </div>
                          <button class="btn btn-secondary mt-4">submit</button>
                        </form>
                      </div>
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>

    <!-- end patient rigistration popup -->
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
      Specialities: [],
      PatientsData: [],
      Branches: [],
      filterUnits: [],
      patientDetails: [],
      selectedUnit: null,

      path: "",

      newPatient: {
        Patient_FullName: "",
        Branch_id: 0,
        Branch_name: "",
        Gender: "",
        Unit: "",
        Room: "",
        Bed_id: 0,
        Addmission_date: "",
        Age: "",
        Specialty: "",
        Patient_Status: 1,
        Entry_user: 0,
      },

      apiUrl: `http://localhost:${this.link}/endoresment/dist/`,
    };
  },
  methods: {
    popUp: function (room, popName) {
      room["patientPop"] = false;
      room[popName] = true;
      this.patientDetails = Object.assign({}, room);

      setTimeout(function () {
        $("." + popName).addClass("active");
      }, 500);
    },

    close: function () {
      this.patientDetails = false;
    },
    InsertPatient(bed) {
      if ($("form").is(":valid")) {
        let that = this;

        that.newPatient.Branch_id = JSON.parse(localStorage.getItem("user")).Branch_ID;
        that.newPatient.Branch_name = JSON.parse(
          localStorage.getItem("user")
        ).Branch_name;
        that.newPatient.Entry_user = JSON.parse(localStorage.getItem("user")).Emp_id;

        that.newPatient.Room = that.getRoomsData.filter(
          (x) => x.id == bed.Room_id
        )[0].Room_name;

        that.newPatient.Bed_id = bed.id;
        that.newPatient.Unit = that.getUnitsData.filter(
          (x) => x.U_id == that.path
        )[0].U_name;

        //insert new patient
        $.ajax({
          type: "POST",
          url: that.apiUrl + "endoresment/roomDetails.aspx/newPatient",
          data: JSON.stringify({ data: that.newPatient }),
          contentType: "application/json; charset=utf-8",
          dataType: "json",
          success: function (data) {
            //that.$router.push({ name: "viewPatients" });
            location.reload();
          },
        });
      } else {
        swal({
          icon: "warning",
          dangerMode: true,
          text: "Please Fill All Fields!",
        });
      }
    },
  },

  created() {
    let that = this;
    this.path = this.$router.history.current.path.split("/")[2];
    //this.filterUnits = this.getRoomsData.filter((x) => x.unit_id == this.path);

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
        that.filterUnits = that.getRoomsData.filter((x) => x.unit_id == that.path);
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

    //get branches
    $.ajax({
      type: "POST",
      url: that.apiUrl + "endoresment/roomDetails.aspx/getBranchesData",
      contentType: "application/json; charset=utf-8",
      dataType: "json",
      success: function (data) {
        that.Branches = JSON.parse(data.d);
      },
    });

    //get Specialities
    $.ajax({
      type: "POST",
      url: that.apiUrl + "endoresment/roomDetails.aspx/getSpecialitiesData",
      contentType: "application/json; charset=utf-8",
      dataType: "json",
      success: function (data) {
        that.Specialities = JSON.parse(data.d);
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
