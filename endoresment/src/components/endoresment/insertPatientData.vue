<template>
  <div class="insertPatientData p-3">
    <div class="container-fluid mt-2 bg-white pt-3 pb-3 card">
      <div class="container-fluid mt-2 bg-white pt-3 pb-3">
        <!-- start patient data -->
        <div class="container" v-if="patientData">
          <div class="row form-group head-info rounded text-center pt-3 pb-3">
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
                ><span class="ml-2">{{ patientData.Age }} years</span>
              </div>
            </div>
          </div>

          <div class="row form-group text-center mt-4" style="font-size: 25px">
            <div class="col-md-4">
              <span class="pull-left">
                <span class="text-secondary">Unit :</span
                ><span
                  class="ml-2 badge card badge-success text-uppercase pl-4 pr-4 shadow-sm"
                  >{{ patientData.Unit }}</span
                ></span
              >
            </div>
            <div class="col-md-4" style="text-decoration: underline; font-size: 28px">
              <span class="text-success">Date of admission :</span
              ><span class="ml-2 text-secondary">{{ patientData.Addmission_date }}</span>
            </div>
            <div class="col-md-4">
              <span class="pull-right">
                <span class="text-secondary">Room:</span>
                <span class="ml-2 badge card badge-success pl-4 pr-4 shadow-sm">{{
                  patientData.Room
                }}</span>
              </span>
            </div>
          </div>
          <hr />
          <div class="cu-flex form-group">
            <div class="mt-3">
              <span class="pull-left bg-white p-3 shadow">
                <i class="fa fa-address-card-o text-secondary" aria-hidden="true"></i>
                <span class="ml-2 text-info">Specialty :</span
                ><span class="ml-2 text-danger">{{ patientData.Specialty }}</span></span
              >
            </div>
            <div class="mt-3">
              <span class="pull-right bg-white p-3 shadow">
                <i class="fa fa-user-md text-secondary" aria-hidden="true"></i>
                <span class="ml-2 text-info">Consultant :</span
                ><span class="ml-2 text-danger">{{
                  patientData.Consultant_Name
                }}</span></span
              >
            </div>
          </div>
        </div>
        <div class="text-center not-found" v-else>
          <p><i class="fa fa-warning text-warning"></i> This patient is not found!</p>
          <p>There is no data found for this IP</p>
        </div>
        <!-- end patient data -->
      </div>
    </div>
    <div class="container-fluid mt-2 bg-white pt-3 pb-3 card" v-if="patientData && patientFollow">
      <div class="container">
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
                <textarea
                  class="form-control rounded shadow-sm"
                  id="exampleFormControlTextarea1"
                  rows="5"
                  v-model='patientFollow.DR_Diagnosis'
                  :disabled="user.Role_id == 12 || user.Role_id == 17 ? 'disabled' : ''"
                ></textarea>
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
                <textarea
                  class="form-control rounded shadow-sm"
                  id="exampleFormControlTextarea1"
                  rows="5"
                  v-model='patientFollow.DR_ProgressNotes'
                  :disabled="user.Role_id == 12 || user.Role_id == 17 ? 'disabled' : ''"
                ></textarea>
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
                    Pain Level <span class="float-right text-success">{{patientFollow.Pain}}</span>
                  </h4>
                  <div class="progress bg-light mb-4" style="height: 60px">
                    <input
                      class="progress-bar bg-danger w-100"
                      type="range"
                      min="0"
                      max="10"
                      step="1"
                      id="volume"
                      v-model='patientFollow.Pain'
                    />
                  </div>
                </div>

                <div class="col-md-6">
                  <h4 class="font-weight-bold text-dark">Fall</h4>
                  <div class="mb-4 row">
                    <span class="col-md-6"
                      ><div class="custom-control custom-radio bg-light p-2 text-center">
                        <input
                          type="radio"
                          id="customRadio1"
                          name="customRadio"
                          class="custom-control-input"
                          value='F'
                          v-model='patientFollow.Fall'
                        />
                        <label class="custom-control-label" for="customRadio1">F</label>
                      </div>
                    </span>
                    <span class="col-md-6">
                      <div class="custom-control custom-radio bg-light p-2 text-center">
                        <input
                          type="radio"
                          id="customRadio2"
                          name="customRadio"
                          class="custom-control-input"
                          value='None'
                          v-model='patientFollow.Fall'
                        />
                        <label class="custom-control-label" for="customRadio2"
                          >None</label
                        >
                      </div>
                    </span>
                  </div>
                </div>

                <div class="col-md-6">
                  <div class="bg-light p-3 w-100 shadow-sm">
                    <span class="ml-2 font-weight-bold text-dark">Diet :</span>
                    <select class="form-control font-weight-normal text-secondary shadow-sm rounded" v-model="patientFollow.Diet_Name">
                        <option v-for='diet in Diets' :key='diet.id' 
                        :value="diet.Diet_Name"  @click='patientFollow.Diet_id = diet.id '>{{diet.Diet_Name}}</option>
                    </select>
                  </div>
                </div>
                <div class="col-md-6">
                  <div class="bg-light p-3 mb-3 w-100 shadow-sm">
                    <span class="ml-2 font-weight-bold text-dark">Issolation :</span>
                    <select
                      class="form-control font-weight-normal text-secondary shadow-sm rounded"
                      v-model='patientFollow.P_Isolation'>
                      <option value='None'>None</option>
                      <option value='Contact'>Contact</option>
                      <option value='Droplet'>Droplet</option>
                      <option value='Airborn'>Airborn</option>
                      <option value='Contact Droplet'>Contact Droplet</option>
                      <option value='Contact Airborn'>Contact Airborn</option>
                    </select>
                  </div>
                </div>
                <div class="col-md-12">
                  <div class="bg-light p-3 mb-3 w-100 shadow-sm">
                    <span class="ml-2 font-weight-bold text-dark">Allergy :</span>
                    <textarea
                      class="form-control rounded shadow-sm"
                      id="exampleFormControlTextarea1"
                      rows="2"
                      v-model='patientFollow.Allergy'
                    ></textarea>
                  </div>
                </div>
                <hr />
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
                  <textarea
                    class="form-control rounded shadow-sm"
                    id="exampleFormControlTextarea1"
                    rows="5"
                    v-model='patientFollow.Investegation_ToDo'
                  ></textarea>
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
                  <textarea
                    class="form-control rounded shadow-sm"
                    id="exampleFormControlTextarea1"
                    rows="5"
                    v-model='patientFollow.Investegation_FollowUp'
                  ></textarea>
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
                  <textarea
                    class="form-control rounded shadow-sm"
                    id="exampleFormControlTextarea1"
                    rows="5"
                    v-model='patientFollow.Contraptions_Infusions'
                  ></textarea>
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
                <div class="dropdown no-arrow show"></div>
              </div>
              <div class="card-body row m-0">
                <div class="col-md-12">
                  <textarea
                    class="form-control rounded shadow-sm"
                    id="exampleFormControlTextarea1"
                    rows="5"
                    v-model='patientFollow.Routise_PlanOfCare'
                  ></textarea>
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
                  <textarea
                    class="form-control rounded shadow-sm"
                    id="exampleFormControlTextarea1"
                    rows="3"
                    v-model='patientFollow.DR_Consultaion_Progress'
                    :disabled="user.Role_id == 12 || user.Role_id == 17 ? 'disabled' : ''"
                  ></textarea>
                </div>
              </div>
            </div>
          </div>
        </div>
        <div class="text-center">
          <button class="special-btn" @click.prevent='SaveEdits()'>Submit</button>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  name: "insertPatientData",
  props: ['link', 'user'],
  data() {
    return {
      apiUrl: this.link,
      patientData: null,
      patientFollow: null,
      id: this.$route.params.id,
      Shift: '',
      Diets: []
    };
  },
  methods: {
    
    LOS(date) {
        let length = Math.round((new Date() - new Date(date.trim())) / (1000 * 60 * 60 * 24));
        return length == 0 ? 'Today' : length == 1 ? length + ' Day' : length + ' Days'
    },
    SaveEdits() { //updateFollowUpData
        let that = this;
        swal({
            title: "Are you sure?",
            icon: "warning",
            buttons: true,
            dangerMode: true,
            })
            .then((confirm) => {
            if (confirm) {
              let dataObj = that.patientFollow;

              if (dataObj.id && that.LOS(dataObj.Entry_date) == 'Today' && dataObj.Shift.trim() == that.Shift) {

                  // update follow up by nurse
                  if (that.user.Role_id == 12 || that.user.Role_id == 17) {

                      dataObj.Update_Nurse = that.user.Emp_id;
                      dataObj.Update_Nurse_Name = that.user.FullName;
                      dataObj.Update_Nurse_Time = moment(new Date).format('DD-MM-YYYY A HH-mm');

                      UpdateData();
                      
                  } else if (that.user.Role_id == 10) {
                      
                      // update follow up by doctor

                      dataObj.Update_Doctor = that.user.Emp_id;
                      dataObj.Update_Doctor_Name = that.user.FullName;
                      dataObj.Update_Doctor_Time = moment(new Date).format('DD-MM-YYYY A HH-mm');

                      UpdateData();
                  }

              } else {

                  // insert into follow up by nurse
                  if (that.user.Role_id == 12 || that.user.Role_id == 17) {

                      dataObj.Shift = that.Shift;
                      dataObj.Insert_Nurse = that.user.Emp_id;
                      dataObj.Insert_Nurse_Name = that.user.FullName;
                      dataObj.Insert_Nurse_Time = moment(new Date).format('DD-MM-YYYY A HH-mm');
                      dataObj.Insert_Doctor = 0;

                      InsertData();
                      
                  } else if (that.user.Role_id == 10) {
                      
                      // insert into follow up by doctor
                      dataObj.Shift = that.Shift;
                      dataObj.Insert_Doctor = that.user.Emp_id;
                      dataObj.Insert_Doctor_Name = that.user.FullName;
                      dataObj.Insert_Doctor_Time = moment(new Date).format('DD-MM-YYYY A HH-mm');
                      dataObj.Insert_Nurse = 0;

                      InsertData();
                  }
              }

              function InsertData () {
                $.ajax({
                    type: "POST",
                    url: that.apiUrl + "endoresment/handover.aspx/insertFollowUpData",
                    data:JSON.stringify({"patient": dataObj}),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (data) {
                        swal({
                            title: "Updated!",
                            icon: "success",
                        });
                        that.$router.push({ path:'/Endorsement'});
                    }
                });
            }

            function UpdateData () {
                $.ajax({
                  type: "POST",
                  url: that.apiUrl + "endoresment/handover.aspx/updateFollowUpData",
                  data:JSON.stringify({"patient": dataObj}),
                  contentType: "application/json; charset=utf-8",
                  dataType: "json",
                  success: function (data) {
                      swal({
                          title: "Updated!",
                          icon: "success",
                      });
                      that.$router.push({ path:'/Endorsement'});
                  }
                });
              }
            } else {
                swal("Canceled!");
            }
        });
    },
  },
  created() {
    let that = this;

    // current shift
    if (new Date().getHours() <= 20 && new Date().getHours() >= 8) {
        that.Shift = 'Day';
    } else {
        that.Shift = 'Night';
    }

    // get patient data
    $.ajax({
      type: "POST",
      url: that.apiUrl + "endoresment/insertPatientData.aspx/getPatientData",
      data: JSON.stringify({ id: { id: that.id } }),
      contentType: "application/json; charset=utf-8",
      dataType: "json",
      success: function (data) {
        that.patientData = JSON.parse(data.d)[0];
      },
    });

    // get latest shift data
    $.ajax({
        type: "POST",
        url: that.apiUrl + "endoresment/insertPatientData.aspx/getShiftData",
        data:JSON.stringify({"data": {"Patient_id":  that.id}}),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (JSON.parse(data.d).length > 0) {
                that.patientFollow =  JSON.parse(data.d)[0] ;
                that.patientFollow.Fall = that.patientFollow.Fall.trim();
            }
            else {

                that.patientFollow = 
                    {
                        Shift: that.Shift,
                        Patient_id: that.id,
                        Diet_id: 0,
                        Diet_Name: '',
                        Pain: 0,
                        P_Isolation: '',
                        Fall:'',
                        Allergy: '',
                        Investegation_ToDo: '',
                        Investegation_FollowUp: '',
                        Contraptions_Infusions: '',
                        Routise_PlanOfCare: '',
                        Surgury_Procedures :'',
                        DR_Diagnosis: '',
                        DR_ProgressNotes: '',
                        DR_Consultaion_Progress: '',
                        Update_Nurse: 0,
                        Update_Nurse_Name: '',
                        Update_Nurse_Time: '',
                        Update_Doctor: 0,
                        Update_Doctor_Name: '',
                        Update_Doctor_Time: '',
                        Insert_Nurse: 0,
                        Insert_Nurse_Name: '',
                        Insert_Nurse_Time: '',
                        Insert_Doctor: 0,
                        Insert_Doctor_Name: '',
                        Insert_Doctor_Time: '',
                        Consultaion: '',
                        Transfer_From: '',
                        Transfer_To: '',
                        Entry_date:new Date()
                    };
            }
        },
    });

    // get diets 
    $.ajax({
        type: "POST",
        url: that.apiUrl + "endoresment/handover.aspx/getDietsData",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            that.Diets = JSON.parse(data.d);
        }
    });
  },
};
</script>

<style scoped>
.insertPatientData {
  min-height: 100vh;
  background-color: #f6f8fb;
  position: relative;
  overflow: hidden;
  font-size: 16px;
}
.head-info {
  background-color: #f6f8fb;
}
</style>
