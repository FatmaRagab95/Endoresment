<template>
  <div class="patientData p-3">
    <div class="container-fluid mt-2 bg-white pt-3 pb-3 card">
      <!-- start patient data -->
      <div class="container" v-if='patientData'>
        <div class="row form-group head-info rounded text-center pt-3 pb-3">
          <div class="col-md-4">
            <div class="shadow bg-white rounded pt-3 pb-3">
              <span class="text-info">Name :</span
              ><span class="ml-2">{{patientData.Patient_FullName}}</span>
            </div>
          </div>
          <div class="col-md-4">
            <div class="shadow bg-white rounded pt-3 pb-3">
              <span class="text-info">Medical Number :</span
              ><span class="ml-2">{{patientData.Medical_Number}}</span>
            </div>
          </div>
          <div class="col-md-4">
            <div class="shadow bg-white rounded pt-3 pb-3">
              <span class="text-info">Age:</span><span class="ml-2">{{patientData.Age}} years</span>
            </div>
          </div>
        </div>

        <div class="row form-group text-center mt-4" style="font-size: 25px">
          <div class="col-md-4">
            <span class="pull-left">
              <span class="text-secondary">Unit :</span
              ><span
                class="ml-2 badge card badge-success text-uppercase pl-4 pr-4 shadow-sm"
                >{{patientData.Unit}}</span
              ></span
            >
          </div>
          <div class="col-md-4" style="text-decoration: underline; font-size: 28px">
            <span class="text-success">Date of admission :</span>
            <span class="ml-2 text-secondary">{{patientData.Addmission_date}}</span>
          </div>
          <div class="col-md-4">
            <span class="pull-right">
              <span class="text-secondary">Room:</span>
              <span class="ml-2 badge card badge-success pl-4 pr-4 shadow-sm">{{patientData.Room}}</span>
            </span>
          </div>
        </div>
        <hr />
        <div class="cu-flex form-group">
          <div class="mt-3">
            <span class="pull-left bg-white p-3 shadow">
              <i class="fa fa-address-card-o text-secondary" aria-hidden="true"></i>
              <span class="ml-2 text-info">Specialty :</span
              ><span class="ml-2 text-danger">{{patientData.Specialty}}</span></span
            >
          </div>
          <div class="mt-3">
            <span class="pull-right bg-white p-3 shadow">
              <i class="fa fa-user-md text-secondary" aria-hidden="true"></i>
              <span class="ml-2 text-info">Consultant :</span
              ><span class="ml-2 text-danger">{{patientData.Consultant_Name}}</span></span
            >
          </div>
        </div>
      </div>
      <!-- end patient data -->
    </div>

    <div class="container-fluid mt-2 bg-white pt-3 pb-3 card">
      <div class="container" v-if='shiftData'>
        <div class="row mt-4 text-left">
          <div class="col-md-8">
            <div class="form-group bg-white border p-3 shadow-sm">
              <label for="exampleFormControlSelect1">See previous shifts</label>
              <div class="row">
                  <div class="col-md 6">
                    <select class="form-control form-control-sm" v-model='viewedShift'>
                      <option v-for='shift in shiftData' :value='shift' :key='shift.id'>{{shift.Entry_date.substr(0,10) + ' ' + shift.Shift}}</option>
                    </select>
                  </div>
              </div>
            </div>
          </div>
          <div class="col-md-4">
            <div class="form-group bg-white border text-center shadow-sm">
              <router-link :to='{name:"Insert Patient Data", params:{id:id}}' class="special-btn pl-3 pr-3">
                  Submit shift data
              </router-link>
            </div>
          </div>
        </div>

        <!-- start viewing follow up data -->
        
        <div class="infoData">
          <ul class="list-unstyled  card bg-white shadow" :class='viewedShift.Shift.trim() == "Day" ? "Day-card" : "Night-card"'>
            <li class="cu-flex detail"><span>Inserted From: </span> 
                <span>
                  {{viewedShift.Insert_Doctor ? 'Dr.' + viewedShift.Insert_Doctor_Name : 'Nurse: ' + viewedShift.Insert_Nurse_Name}}
                </span>
            </li>
            <li class="cu-flex detail"><span>Shift</span> <span>
              <i :class="viewedShift.Shift.trim() == 'Day' ? 'fa fa-sun-o' : 'fa fa-moon-o'"></i>
                {{viewedShift.Shift}}       
                </span>
            </li>
            <li class="cu-flex detail"><span>Entry Date</span> <span>{{viewedShift.Entry_date.substr(0,10)}}</span></li>
          </ul>
        </div>

        <div class="row mt-4 text-left">
          <div class="col-xl-8 col-lg-7">
            <div class="card shadow overflow-auto" style="height: 250px">
              <div
                class="card-header py-3 d-flex flex-row align-items-center justify-content-between"
              >
                <h5 class="m-0">Diagnosis</h5>
                <div class="dropdown no-arrow show">
                  <a
                    class="fa fa-pencil-square-o text-success"
                    href="#"
                    role="button"
                    id="dropdownMenuLink"
                    data-toggle="dropdown"
                    aria-haspopup="true"
                    aria-expanded="false"
                  >
                  </a>
                </div>
              </div>
              <!-- Card Body -->
              <div class="card-body">
                <i class="fa fa-circle text-danger small" v-if='viewedShift.DR_Diagnosis'></i>
                {{viewedShift.DR_Diagnosis}}
              </div>
            </div>
          </div>

          <div class="col-xl-4 col-lg-5">
            <div class="card shadow overflow-auto" style="height: 250px">
              <div
                class="card-header py-3 d-flex flex-row align-items-center justify-content-between"
              >
                <h5 class="m-0">Progress Notes</h5>
                <div class="dropdown no-arrow show">
                  <a
                    class="fa fa-pencil-square-o text-success"
                    href="#"
                    role="button"
                    id="dropdownMenuLink"
                    data-toggle="dropdown"
                    aria-haspopup="true"
                    aria-expanded="false"
                  >
                  </a>
                </div>
              </div>
              <!-- Card Body -->
              <div class="card-body">
                <i class="fa fa-circle text-danger small" v-if='viewedShift.DR_ProgressNotes'></i>
                {{viewedShift.DR_ProgressNotes}}
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
                <div class="dropdown no-arrow show">
                  <a
                    class="fa fa-pencil-square-o text-success"
                    href="#"
                    role="button"
                    id="dropdownMenuLink"
                    data-toggle="dropdown"
                    aria-haspopup="true"
                    aria-expanded="false"
                  >
                  </a>
                </div>
              </div>
              <div class="card-body row">
                <div class="col-md-6">
                  <h4 class="font-weight-bold text-dark mt-3">
                    Pain Level <span class="float-right text-success">{{viewedShift.Pain}}</span>
                  </h4>
                  <div class="progress bg-light mb-4" style='height:60px'>
                    <input
                      class="progress-bar bg-danger w-100"
                      type="range"
                      min="0"
                      max="10"
                      step="1"
                      id="volume"
                      :value='viewedShift.Pain' disabled/>
                  </div>
                </div>
                <div class="col-md-6">
                  <div class="bg-light pt-2 pb-2 mb-3 w-100 shadow-sm">
                    <span class="ml-2 font-weight-bold text-dark">Fall :</span>
                    <span class="font-weight-normal text-secondary ml-2">{{viewedShift.Fall}}</span>
                  </div>
                </div>
                <div class="col-md-6">
                  <div class="bg-light pt-2 pb-2 mb-3 w-100 shadow-sm">
                    <span class="ml-2 font-weight-bold text-dark">Diet :</span>
                    <span class="font-weight-normal text-secondary ml-2">{{viewedShift.Diet_Name}}</span>
                  </div>
                </div>

                <div class="col-md-6">
                  <div class="bg-light pt-2 pb-2 mb-3 w-100 shadow-sm">
                    <span class="ml-2 font-weight-bold text-dark">Issolation :</span>
                    <span class="font-weight-normal text-secondary ml-2"
                      >{{viewedShift.P_Isolation}}</span
                    >
                  </div>
                </div>
                <div class="col-md-12">
                  <div class="bg-light p-3 mb-3 w-100 shadow-sm h-100">
                    <span class="font-weight-bold text-dark">Allergy :</span>
                    <span class="font-weight-normal text-secondary ml-2"
                      >{{viewedShift.Allergy}}</span
                    >
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
                <div class="dropdown no-arrow show">
                  <a
                    class="fa fa-pencil-square-o text-success"
                    href="#"
                    role="button"
                    id="dropdownMenuLink"
                    data-toggle="dropdown"
                    aria-haspopup="true"
                    aria-expanded="false"
                  >
                  </a>
                </div>
              </div>
              <div class="card-body row m-0">
                <div class="col-md-12">
                  <i class="fa fa-circle text-success small"  v-if='viewedShift.Investegation_ToDo'></i>
                  {{viewedShift.Investegation_ToDo}}
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
                <div class="dropdown no-arrow show">
                  <a
                    class="fa fa-pencil-square-o text-success"
                    href="#"
                    role="button"
                    id="dropdownMenuLink"
                    data-toggle="dropdown"
                    aria-haspopup="true"
                    aria-expanded="false"
                  >
                  </a>
                </div>
              </div>
              <div class="card-body row m-0">
                <div class="col-md-12">
                  <i class="fa fa-circle text-success small" v-if='viewedShift.Investegation_FollowUp'></i>
                  {{viewedShift.Investegation_FollowUp}}
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
                <div class="dropdown no-arrow show">
                  <a
                    class="fa fa-pencil-square-o text-success"
                    href="#"
                    role="button"
                    id="dropdownMenuLink"
                    data-toggle="dropdown"
                    aria-haspopup="true"
                    aria-expanded="false"
                  >
                  </a>
                </div>
              </div>
              <div class="card-body row m-0">
                <div class="col-md-12">
                  <i class="fa fa-circle text-success small" v-if='viewedShift.Contraptions_Infusions'></i>
                  {{viewedShift.Contraptions_Infusions}}
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
                <div class="dropdown no-arrow show">
                  <a
                    class="fa fa-pencil-square-o text-success"
                    href="#"
                    role="button"
                    id="dropdownMenuLink"
                    data-toggle="dropdown"
                    aria-haspopup="true"
                    aria-expanded="false"
                  >
                  </a>
                </div>
              </div>
              <div class="card-body row m-0">
                <div class="col-md-12">
                  <i class="fa fa-circle text-success small"  v-if='viewedShift.Routise_PlanOfCare'></i>
                  {{viewedShift.Routise_PlanOfCare}}
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
                <div class="dropdown no-arrow show">
                  <a
                    class="fa fa-pencil-square-o text-success"
                    href="#"
                    role="button"
                    id="dropdownMenuLink"
                    data-toggle="dropdown"
                    aria-haspopup="true"
                    aria-expanded="false"
                  >
                  </a>
                </div>
              </div>
              <div class="card-body row m-0">
                <div class="col-md-12">
                  <i class="fa fa-circle text-danger small" v-if='viewedShift.DR_Consultaion_Progress'></i>
                  {{viewedShift.DR_Consultaion_Progress}}
                </div>
              </div>
            </div>
          </div>
        </div>
        <!-- end viewing follow up data -->
      </div>
  
      <div class="text-center not-found" v-else>
        <p><i class='fa fa-warning text-warning'></i>
        There is no follow up data yet for this patient!</p>
        <router-link :to='{name:"Insert Patient Data", params:{id:id}}' class="special-btn pl-3 pr-3">
            Submit shift data
        </router-link>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  name: "patientData",
  props: ["link"],
  data() {
    return {
      apiUrl: this.link,
      patientData:null,
      shiftData:null,
      id: this.$route.params.id,
      viewedShift: null
    };
  },
  methods: {
  },
  created() {
    let that = this;

    // get patient data
    $.ajax({
      type: "POST",
      url: that.apiUrl + "endoresment/patientData.aspx/getPatientData",
      data: JSON.stringify({ "id": { "id": that.id } }),
      contentType: "application/json; charset=utf-8",
      dataType: "json",
      success: function (data) {
        that.patientData = JSON.parse(data.d)[0];
      },
    });

    // get latest shift data
    $.ajax({
      type: "POST",
      url: that.apiUrl + "endoresment/patientData.aspx/getShiftData",
      data: JSON.stringify({ "data": { "Patient_id": that.id } }),
      contentType: "application/json; charset=utf-8",
      dataType: "json",
      success: function (data) {
        that.shiftData = JSON.parse(data.d);
        that.viewedShift = that.shiftData.slice(-1)[0];
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
  font-size:16px;
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
</style>
