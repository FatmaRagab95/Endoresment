<template>
  <div class="roomDetails p-3 text-capitalize">
    <!-- start unit title -->
    <div class="container-fluid mt-2 bg-white pt-3 pb-3 card">
      <div class="container text-center">
        <h1
          class="mt-4 text-secondary"
          style="text-decoration: underline"
          v-if="selectedUnit"
        >
          {{ selectedUnit.U_name }} ......
          <span class="badge bg-primary text-white">{{ getRoomsData.length }} Rooms</span>
        </h1>

        <div v-if='user.Role_id != 12 && user.Role_id != 17'>
            <v-combobox 
            v-model="selectedUnit"
            :items="getUnitsData"
            item-text="U_name"
            :item-value="'U_name'"
            name="unit"
            label="Unit"
            outlined
            dense
            style="max-width: 200px; margin: 30px auto 0 auto"
          ></v-combobox>
        </div>
        
      </div>
    </div>
    <!-- end unit title -->
    <!-------------------------------------------------------------------------------------------------------------->
    <!-- start unit rooms -->
    <div class="container-fluid mt-2 bg-white pt-3 pb-3 card viewPatients">
      <div class="container" v-if="getRoomsData.length > 0">
        <!--- start double rooms --->
        <ul class="list-unstyled">
          <li class="cube-container">
            <span class="cube bg-success rounded">{{
              RoomsDashboard.filter((x) => x.Status_id == 1).length
            }}</span>
            Available Beds
          </li>
          <li class="cube-container">
            <span class="cube bg-danger rounded">{{
              RoomsDashboard.filter((x) => x.Status_id == 2).length
            }}</span>
            Occupied Beds
          </li>
        </ul>
        <h4
          v-if="getRoomsData.filter((x) => x.Room_type_id == 1).length > 0"
          class="text-secondary text-left border-top pt-5"
          style="text-decoration: underline"
        >
          double rooms ----
          <span class="badge badge-secondary">{{
            getRoomsData.filter((x) => x.Room_type_id == 1).length
          }}</span>
        </h4>
        <div
          class="row text-center"
          v-if="getRoomsData.filter((x) => x.Room_type_id == 1).length > 0"
        >
          <div
            class="col-md-4 p-2"
            v-for="room in getRoomsData.filter((x) => x.Room_type_id == 1)"
            :key="room.R_id"
          >
            <div class="bg-light shadow p-2 container card">
              <h4
                class="text-secondary font-weight-bold mb-3"
                style="text-decoration: underline"
              >
                room
                <span class="text-primary">{{ room.Room_name }}</span>
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
                      class="tooltip bg-white card p-4 text-secondary text-capitalize"
                      v-if="bed.Status_id == 2"
                    >
                      <h6>
                        <span class="text-primary">full name :</span
                        ><span class="ml-2">{{
                          PatientsData.filter((x) => x.Bed_id == bed.id)[0]
                            .Patient_FullName
                        }}</span>
                      </h6>
                      <h6>
                        <span class="text-primary">age : </span
                        ><span class="ml-2">
                          {{
                            PatientsData.filter((x) => x.Bed_id == bed.id)[0].Age
                          }}</span
                        >
                      </h6>
                      <h6>
                        <span class="text-primary">gender : </span
                        ><span class="ml-2">{{
                          PatientsData.filter((x) => x.Bed_id == bed.id)[0].Gender
                        }}</span>
                      </h6>
                      <h6>
                        <span class="text-primary">medical number : </span
                        ><span class="ml-2">{{
                          PatientsData.filter((x) => x.Bed_id == bed.id)[0].Medical_Number
                        }}</span>
                      </h6>
                      <h6>
                        <span class="text-primary">specialty : </span
                        ><span class="ml-2">{{
                          PatientsData.filter((x) => x.Bed_id == bed.id)[0].Specialty
                        }}</span>
                      </h6>
                      <router-link
                        :to="{
                          name: 'Patient Data',
                          params: {
                            id: PatientsData.filter((x) => x.Bed_id == bed.id)[0].id,
                          },
                        }"
                        target="_blank"
                        class="btn mt-3 btn-primary btn-sm shadow"
                        >Details</router-link
                      >
                    </div>
                    <div
                      class="tooltip bg-white card p-4 text-center"
                      v-else-if="bed.Status_id == 1"
                    >
                      <button
                        class="btn btn-primary btn-sm rounded open-pop shadow"
                        data-pop-name="patient-popup"
                        v-on:click.prevent="popUp(bed, 'patientPop')"
                      >
                        Patient Registration
                      </button>
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>
        <!--- end double rooms --->

        <!--- start single rooms --->
        <h4
          v-if="getRoomsData.filter((x) => x.Room_type_id == 2).length > 0"
          class="text-secondary text-left mt-5 border-top pt-5"
          style="text-decoration: underline"
        >
          single rooms ----
          <span class="badge badge-secondary">{{
            getRoomsData.filter((x) => x.Room_type_id == 2).length
          }}</span>
        </h4>
        <div
          class="row text-center"
          v-if="getRoomsData.filter((x) => x.Room_type_id == 2).length > 0"
        >
          <div
            class="col-md-4 p-2"
            v-for="room in getRoomsData.filter((x) => x.Room_type_id == 2)"
            :key="room.R_id"
          >
            <div class="bg-light shadow p-2 container card">
              <h4
                class="text-secondary font-weight-bold mb-3"
                style="text-decoration: underline"
              >
                room
                <span class="text-primary">{{ room.Room_name }}</span>
              </h4>

              <div class="shadow bg-white rounded row w-100 m-auto p-2">
                <div class="col-md-3 p-2"></div>
                <div
                  class="col-md-6 p-2"
                  v-for="bed in RoomsDashboard.filter((x) => x.Room_id == room.id)"
                  :key="bed.id"
                >
                  <div
                    class="card p-2 bg-light shadow icon"
                    style="
                      transition: 0.4s cubic-bezier(0.68, -0.55, 0.265, 1.55);
                      position: relative;
                      height: 200px;
                    "
                  >
                    <div class="h-25 bg-light mb-2 rounded pt-2 shadow">
                      <span
                        :class="
                          bed.Status_id == 1
                            ? 'badge-success pl-3 pr-3 rounded'
                            : bed.Status_id == 2
                            ? 'badge-danger pl-3 pr-3 rounded'
                            : bed.Status_id == 3
                            ? 'badge-warning pl-3 pr-3 rounded'
                            : ' '
                        "
                        >{{ bed.Bed_name.substring(0, 4) }}
                      </span>
                    </div>
                    <div
                      class="h-75 bg-light rounded pt-3 shadow card bed-background-single"
                    ></div>
                    <div
                      class="tooltip bg-white card p-4 text-secondary text-capitalize"
                      v-if="bed.Status_id == 2"
                    >
                      <h6>
                        <span class="text-primary">full name :</span
                        ><span class="ml-2">{{
                          PatientsData.filter((x) => x.Bed_id == bed.id)[0]
                            .Patient_FullName
                        }}</span>
                      </h6>
                      <h6>
                        <span class="text-primary">age : </span
                        ><span class="ml-2">{{
                          PatientsData.filter((x) => x.Bed_id == bed.id)[0].Age
                        }}</span>
                      </h6>
                      <h6>
                        <span class="text-primary">gender : </span
                        ><span class="ml-2">{{
                          PatientsData.filter((x) => x.Bed_id == bed.id)[0].Gender
                        }}</span>
                      </h6>
                      <h6>
                        <span class="text-primary">medical number : </span
                        ><span class="ml-2">{{
                          PatientsData.filter((x) => x.Bed_id == bed.id)[0].Medical_Number
                        }}</span>
                      </h6>
                      <h6>
                        <span class="text-primary">specialty : </span
                        ><span class="ml-2">{{
                          PatientsData.filter((x) => x.Bed_id == bed.id)[0].Specialty
                        }}</span>
                      </h6>
                      <button class="btn mt-3 btn-primary btn-sm rounded">Details</button>
                    </div>
                    <div
                      class="tooltip bg-white card p-4 text-center"
                      v-else-if="bed.Status_id == 1"
                    >
                      <button
                        class="btn btn-primary btn-sm rounded open-pop shadow"
                        data-pop-name="patient-popup"
                        v-on:click.prevent="popUp(bed, 'patientPop')"
                      >
                        Patient Registration
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

        <!--- start suits --->
        <h4
          v-if="getRoomsData.filter((x) => x.Room_type_id == 3).length > 0"
          class="text-secondary text-left mt-5 border-top pt-5"
          style="text-decoration: underline"
        >
          suits ----
          <span class="badge badge-secondary">{{
            getRoomsData.filter((x) => x.Room_type_id == 3).length
          }}</span>
        </h4>
        <div
          class="row text-center"
          v-if="getRoomsData.filter((x) => x.Room_type_id == 3).length > 0"
        >
          <div
            class="col-md-4 p-2"
            v-for="room in getRoomsData.filter((x) => x.Room_type_id == 3)"
            :key="room.R_id"
          >
            <div class="bg-light shadow p-2 container card">
              <h4
                class="text-secondary font-weight-bold mb-3"
                style="text-decoration: underline"
              >
                room
                <span class="text-primary">{{ room.Room_name }}</span>
              </h4>

              <div class="shadow bg-white rounded row w-100 m-auto p-2">
                <div class="col-md-3 p-2"></div>
                <div
                  class="col-md-6 p-2"
                  v-for="bed in RoomsDashboard.filter((x) => x.Room_id == room.id)"
                  :key="bed.id"
                >
                  <div
                    class="card p-2 bg-light shadow icon"
                    style="
                      transition: 0.4s cubic-bezier(0.68, -0.55, 0.265, 1.55);
                      position: relative;
                      height: 200px;
                    "
                  >
                    <div class="h-25 bg-light mb-2 rounded pt-2 shadow">
                      <span
                        :class="
                          bed.Status_id == 1
                            ? 'badge-success pl-3 pr-3 rounded'
                            : bed.Status_id == 2
                            ? 'badge-danger pl-3 pr-3 rounded'
                            : bed.Status_id == 3
                            ? 'badge-warning pl-3 pr-3 rounded'
                            : ' '
                        "
                        >{{ bed.Bed_name.substring(0, 4) }}
                      </span>
                    </div>
                    <div
                      class="h-75 bg-light rounded pt-3 shadow card bed-background-single"
                    ></div>
                    <div
                      class="tooltip bg-white card p-4 text-secondary text-capitalize"
                      v-if="bed.Status_id == 2"
                    >
                      <h6>
                        <span class="text-primary">full name :</span
                        ><span class="ml-2">{{
                          PatientsData.filter((x) => x.Bed_id == bed.id)[0]
                            .Patient_FullName
                        }}</span>
                      </h6>
                      <h6>
                        <span class="text-primary">age : </span
                        ><span class="ml-2">{{
                          PatientsData.filter((x) => x.Bed_id == bed.id)[0].Age
                        }}</span>
                      </h6>
                      <h6>
                        <span class="text-primary">gender : </span
                        ><span class="ml-2">{{
                          PatientsData.filter((x) => x.Bed_id == bed.id)[0].Gender
                        }}</span>
                      </h6>
                      <h6>
                        <span class="text-primary">medical number : </span
                        ><span class="ml-2">{{
                          PatientsData.filter((x) => x.Bed_id == bed.id)[0].Medical_Number
                        }}</span>
                      </h6>
                      <h6>
                        <span class="text-primary">specialty : </span
                        ><span class="ml-2">{{
                          PatientsData.filter((x) => x.Bed_id == bed.id)[0].Specialty
                        }}</span>
                      </h6>
                      <button class="btn mt-3 btn-primary btn-sm rounded">Details</button>
                    </div>
                    <div
                      class="tooltip bg-white card p-4 text-center"
                      v-else-if="bed.Status_id == 1"
                    >
                      <button
                        class="btn btn-primary btn-sm rounded open-pop shadow"
                        data-pop-name="patient-popup"
                        v-on:click.prevent="popUp(bed, 'patientPop')"
                      >
                        Patient Registration
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

        <!--- start daycase rooms --->
        <h4
          v-if="getRoomsData.filter((x) => x.Room_type_id == 4).length > 0"
          class="text-secondary text-left mt-5 border-top pt-5"
          style="text-decoration: underline"
        >
          daycase rooms ----
          <span class="badge badge-secondary">{{
            getRoomsData.filter((x) => x.Room_type_id == 4).length
          }}</span>
        </h4>
        <div
          class="row text-center"
          v-if="getRoomsData.filter((x) => x.Room_type_id == 4).length > 0"
        >
          <div
            class="col-md-4 p-2"
            v-for="room in getRoomsData.filter((x) => x.Room_type_id == 4)"
            :key="room.R_id"
          >
            <div class="bg-light shadow p-2 container card">
              <h4
                class="text-secondary font-weight-bold mb-3"
                style="text-decoration: underline"
              >
                room
                <span class="text-primary">{{ room.Room_name }}</span>
              </h4>

              <div class="shadow bg-white rounded row w-100 m-auto p-2">
                <div class="col-md-3 p-2"></div>
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
                      <span
                        :class="
                          bed.Status_id == 1
                            ? 'badge-success pl-3 pr-3 rounded'
                            : bed.Status_id == 2
                            ? 'badge-danger pl-3 pr-3 rounded'
                            : bed.Status_id == 3
                            ? 'badge-warning pl-3 pr-3 rounded'
                            : ' '
                        "
                        >{{ bed.Bed_name.substring(0, 4) }}
                      </span>
                    </div>
                    <div
                      class="h-75 bg-light rounded pt-3 shadow card bed-background-single"
                    ></div>
                    <div
                      class="tooltip bg-white card p-4 text-secondary text-capitalize"
                      v-if="bed.Status_id == 2"
                    >
                      <h6>
                        <span class="text-primary">full name :</span
                        ><span class="ml-2">{{
                          PatientsData.filter((x) => x.Bed_id == bed.id)[0]
                            .Patient_FullName
                        }}</span>
                      </h6>
                      <h6>
                        <span class="text-primary">age : </span
                        ><span class="ml-2">{{
                          PatientsData.filter((x) => x.Bed_id == bed.id)[0].Age
                        }}</span>
                      </h6>
                      <h6>
                        <span class="text-primary">gender : </span
                        ><span class="ml-2">{{
                          PatientsData.filter((x) => x.Bed_id == bed.id)[0].Gender
                        }}</span>
                      </h6>
                      <h6>
                        <span class="text-primary">medical number : </span
                        ><span class="ml-2">{{
                          PatientsData.filter((x) => x.Bed_id == bed.id)[0].Medical_Number
                        }}</span>
                      </h6>
                      <h6>
                        <span class="text-primary">specialty : </span
                        ><span class="ml-2">{{
                          PatientsData.filter((x) => x.Bed_id == bed.id)[0].Specialty
                        }}</span>
                      </h6>
                      <button class="btn mt-3 btn-primary btn-sm rounded">Details</button>
                    </div>
                    <div
                      class="tooltip bg-white card p-4 text-center"
                      v-else-if="bed.Status_id == 1"
                    >
                      <button
                        class="btn btn-danger btn-sm rounded open-pop shadow-sm"
                        data-pop-name="patient-popup"
                        v-on:click.prevent="popUp(bed, 'patientPop')"
                      >
                        Patient Registration
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

        <!--- start ccu rooms --->
        <h4
          v-if="getRoomsData.filter((x) => x.Room_type_id == 5).length > 0"
          class="text-secondary text-left mt-5 border-top pt-5"
          style="text-decoration: underline"
        >
          ccu rooms ----
          <span class="badge badge-secondary">{{
            getRoomsData.filter((x) => x.Room_type_id == 5).length
          }}</span>
        </h4>
        <div
          class="row text-center"
          v-if="getRoomsData.filter((x) => x.Room_type_id == 5).length > 0"
        >
          <div
            class="col-md-4 p-2"
            v-for="room in getRoomsData.filter((x) => x.Room_type_id == 5)"
            :key="room.R_id"
          >
            <div class="bg-light shadow p-2 container card">
              <h4
                class="text-secondary font-weight-bold mb-3"
                style="text-decoration: underline"
              >
                room
                <span class="text-primary">{{ room.Room_name }}</span>
              </h4>

              <div class="shadow bg-white rounded row w-100 m-auto p-2">
                <div class="col-md-3 p-2"></div>
                <div
                  class="col-md-6 p-2"
                  v-for="bed in RoomsDashboard.filter((x) => x.Room_id == room.id)"
                  :key="bed.id"
                >
                  <div
                    class="card p-2 bg-light shadow icon"
                    style="
                      transition: 0.4s cubic-bezier(0.68, -0.55, 0.265, 1.55);
                      position: relative;
                      height: 200px;
                    "
                  >
                    <div class="h-25 bg-light mb-2 rounded pt-2 shadow">
                      <span
                        :class="
                          bed.Status_id == 1
                            ? 'badge-success pl-3 pr-3 rounded'
                            : bed.Status_id == 2
                            ? 'badge-danger pl-3 pr-3 rounded'
                            : bed.Status_id == 3
                            ? 'badge-warning pl-3 pr-3 rounded'
                            : ' '
                        "
                        >{{ bed.Bed_name.substring(0, 4) }}
                      </span>
                    </div>
                    <div
                      class="h-75 bg-light rounded pt-3 shadow card bed-background-single"
                    ></div>
                    <div
                      class="tooltip bg-white card p-4 text-secondary text-capitalize"
                      v-if="bed.Status_id == 2"
                    >
                      <h6>
                        <span class="text-primary">full name :</span
                        ><span class="ml-2">{{
                          PatientsData.filter((x) => x.Bed_id == bed.id)[0]
                            .Patient_FullName
                        }}</span>
                      </h6>
                      <h6>
                        <span class="text-primary">age : </span
                        ><span class="ml-2">{{
                          PatientsData.filter((x) => x.Bed_id == bed.id)[0].Age
                        }}</span>
                      </h6>
                      <h6>
                        <span class="text-primary">gender : </span
                        ><span class="ml-2">{{
                          PatientsData.filter((x) => x.Bed_id == bed.id)[0].Gender
                        }}</span>
                      </h6>
                      <h6>
                        <span class="text-primary">medical number : </span
                        ><span class="ml-2">{{
                          PatientsData.filter((x) => x.Bed_id == bed.id)[0].Medical_Number
                        }}</span>
                      </h6>
                      <h6>
                        <span class="text-primary">specialty : </span
                        ><span class="ml-2">{{
                          PatientsData.filter((x) => x.Bed_id == bed.id)[0].Specialty
                        }}</span>
                      </h6>
                      <button class="btn mt-3 btn-primary btn-sm rounded">Details</button>
                    </div>
                    <div
                      class="tooltip bg-white card p-4 text-center"
                      v-else-if="bed.Status_id == 1"
                    >
                      <button
                        class="btn btn-primary btn-sm rounded open-pop shadow"
                        data-pop-name="patient-popup"
                        v-on:click.prevent="popUp(bed, 'patientPop')"
                      >
                        Patient Registration
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
        No avalible rooms in this unit!
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
        <div class="popup-body" style="max-width: 900px">
          <div class="custom-form m-auto pb-4">
            <span class="close-pop" v-on:click="close">Close</span>
            <div class="title text-center text-white">
              <span class="bg-secondary pt-2 pb-2 pl-3 pr-3">Patient Registration</span>
            </div>

            <!--- patient-primary -->
            <div class="social-timeline-card">
              <div class="card-body">
                <div
                  class="general primary tabcontent active overflow-auto text-center"
                  style="max-height: 400px"
                >
                  <h3 class="text-center text-success" style="text-decoration: underline">
                    {{ patientDetails.Bed_name }}
                  </h3>
                  <form @submit.prevent="InsertPatient(patientDetails)">
                    <div
                      class="cu-form-group special shadow-sm grouped rounded mb-2 bg-light pr-3 pl-3 ml-auto mr-auto text-left"
                    >
                      <div class="row">
                        <!-- patient name -->
                        <div class="cu-field col-md-12">
                          <h3 class="cu-label">
                            <label>Patient Full Name :</label>
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

                        <!-- medical number -->
                        <div class="cu-field col-md-6">
                          <h3 class="cu-label">
                            <label>Medical Number :</label>
                          </h3>
                          <div class="cu-input text-box" style="max-width: 100%">
                            <span class="fa fa-edit"></span>
                            <input
                              type="number"
                              v-model="newPatient.Medical_Number"
                              required
                            />
                          </div>
                        </div>

                        <!-- patient gender -->
                        <div class="cu-field col-md-6">
                          <h3 class="cu-label">
                            <label>Patient Gender :</label>
                          </h3>
                          <div class="f-select">
                            <select
                              class="form-control form-control-sm"
                              v-model="newPatient.Gender"
                              required
                            >
                              <option value="Male">Male</option>
                              <option value="Female">Female</option>
                            </select>
                          </div>
                        </div>

                        <!-- patient birth date -->
                        <div class="cu-field col-md-6">
                          <h3 class="cu-label">
                            <label>Patient Birth Date :</label>
                          </h3>
                          <div class="cu-input text-box">
                            <span class="fa fa-edit"></span>
                            <input
                              type="date"
                              :max="currentDate"
                              v-model="newPatient.Date_Birth"
                              required
                            />
                          </div>
                        </div>

                        <!-- patient age -->
                        <div class="cu-field col-md-6">
                          <h3 class="cu-label">
                            <label>Patient Age :</label>
                          </h3>
                          <div class="cu-input text-box">
                            <span class="fa fa-edit"></span>
                            <span class="w-100 text-left">{{ newPatient.Age }}</span>
                          </div>
                        </div>

                        <!-- specialty -->
                        <div class="cu-field col-md-6">
                          <h3 class="cu-label">
                            <label>specialty :</label>
                          </h3>
                          <div class="f-select">
                            <select
                              class="form-control form-control-sm"
                              v-model="newPatient.Specialty"
                              required
                            >
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
                            <label>Addmission Date :</label>
                          </h3>
                          <div class="cu-input text-box">
                            <span class="fa fa-edit"></span>
                            <input
                              type="date"
                              :max="currentDate"
                              v-model="newPatient.Addmission_date"
                              required
                            />
                          </div>
                        </div>

                        <div class="cu-field col-md-12" v-if="Consultants.length > 0">
                          <h3 class="cu-label">
                            <label>Consultant :</label>
                          </h3>
                          <div class="f-select">
                            <select
                              class="form-control form-control-sm"
                              v-model="newPatient.Consultant_id"
                              required
                            >
                              <option
                                v-for="dr in Consultants"
                                :value="dr.Dr_Code"
                                :key="dr.DR_Name"
                              >
                                {{ dr.DR_Name }}
                              </option>
                            </select>
                          </div>
                        </div>
                      </div>
                    </div>
                    <button class="btn btn-primary mt-4 shadow">Submit</button>
                  </form>
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
  props: ["link", "user"],
  data() {
    return {
      getUnitsData: [],
      getRoomsData: [],
      RoomsDashboard: [],
      Specialities: [],
      Consultants: [],
      PatientsData: [],
      Branches: [],
      getRoomsData: [],
      patientDetails: [],
      selectedUnit: null,
      Beds: [],

      currentDate: moment(new Date()).format("YYYY-MM-DD"),

      path: "",

      newPatient: {
        Patient_FullName: "",
        Medical_Number: 0,
        Branch_id: 0,
        Branch_name: "",
        Gender: "",
        Unit: "",
        Room: "",
        Bed_id: 0,
        Addmission_date: "",
        Age: "",
        Date_Birth: "",
        Specialty: "",
        Consultant_id: 0,
        Consultant_Name: "",
        Patient_Status: 1,
        Entry_user: 0,
      },

      apiUrl: this.link,
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
          (x) => x.U_id == that.selectedUnit.U_id
        )[0].U_name;

        that.newPatient.Consultant_Name = that.Consultants.filter(
          (x) => x.Dr_Code == that.newPatient.Consultant_id
        )[0].DR_Name;

        //insert new patient
        $.ajax({
          type: "POST",
          url: that.apiUrl + "endoresment/roomDetails.aspx/newPatient",
          data: JSON.stringify({ data: that.newPatient }),
          contentType: "application/json; charset=utf-8",
          dataType: "json",
          success: function (data) {
            swal({
              title: "Sweet!",
              text: "You successfully added a new patient ...",
              icon: "success",
            });
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
    //get rooms and beds
    RoomsInfo() {
      let that = this;
      //get rooms
      $.ajax({
        type: "POST",
        url: that.apiUrl + "endoresment/roomDetails.aspx/getRoomsData",
        data: JSON.stringify({ Room: { unit_id: that.selectedUnit.U_id } }),
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
        data: JSON.stringify({ Room: { unit_id: that.selectedUnit.U_id } }),
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
        data: JSON.stringify({
          info: { Branch_id: that.user.Branch_ID, Unit: that.selectedUnit.U_name },
        }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
          that.PatientsData = JSON.parse(data.d);
        },
      });
    },
  },
  watch: {
    "newPatient.Date_Birth": function () {
      if (moment().diff(this.newPatient.Date_Birth, "days") >= 365) {
        this.newPatient.Age =
          moment().diff(this.newPatient.Date_Birth, "years") + " years";
      } else if (
        moment().diff(this.newPatient.Date_Birth, "days") < 365 &&
        moment().diff(this.newPatient.Date_Birth, "days") >= 30
      ) {
        this.newPatient.Age =
          moment().diff(this.newPatient.Date_Birth, "months") + " months";
      } else {
        this.newPatient.Age = moment().diff(this.newPatient.Date_Birth, "days") + " days";
      }
    },
    selectedUnit: function () {
      this.RoomsInfo();
    },
    "newPatient.Specialty": function () {
      let Specialty = this.Specialities.filter(
          (x) => x.Spcy_name_En == this.newPatient.Specialty
        )[0].Spcy_id,
        that = this;

      //get Consultants
      $.ajax({
        type: "POST",
        url: that.apiUrl + "endoresment/roomDetails.aspx/getConsultantsData",
        data: JSON.stringify({
          info: { Spcy_id: Specialty, Branch_ID: that.user.Branch_ID },
        }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
          that.Consultants = JSON.parse(data.d);
        },
      });
    },
  },

  created() {
    let that = this;
    this.path = this.$router.history.current.path.split("/")[2];

    //get units
    $.ajax({
      type: "POST",
      url: that.apiUrl + "endoresment/roomDetails.aspx/getUnitsData",
      data: JSON.stringify({ branch: { id: that.user.Branch_ID } }),
      contentType: "application/json; charset=utf-8",
      dataType: "json",
      success: function (data) {
        that.getUnitsData = JSON.parse(data.d);
        if (that.user.Role_id == 17 || that.user.Role_id == 12) {
          that.selectedUnit = JSON.parse(data.d).length > 0 ? 
          that.getUnitsData.filter(x => x.U_id == that.user.Area_id)[0] : null;
        } else {
          that.selectedUnit = JSON.parse(data.d).length > 0 ? that.getUnitsData[0] : null;
        }
        
        that.RoomsInfo();
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
.viewPatients {
  min-height: 500px;
  background: #fff url("../../assets/layout/img/backgrounds/bg20.png") center no-repeat;
  background-size: contain;
  background-color: #f6f8fb;
  text-align: center;
  position: relative;
  overflow: hidden;
}
.cube-container {
  display: inline-flex;
  margin: 15px 30px;
  align-items: center;
}
.cube {
  min-width: 30px;
  height: 25px;
  line-height: 25px;
  display: inline-block;
  margin: 0 5px;
  color: #fff;
  font-size: 14px;
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
