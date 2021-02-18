<template>
  <div class="insertPatientData p-3">
    <div class="container-fluid mt-2 bg-white pt-3 pb-3 card">
    <div class="container-fluid mt-2 bg-white pt-3 pb-3">
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
            <span class="text-success">Date of admission :</span
            ><span class="ml-2 text-secondary">{{patientData.Addmission_date}}</span>
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
      <div class="text-center not-found" v-else>
          <p><i class='fa fa-warning text-warning'></i>
          This patient is not found!</p>
          <p>There is no data found for this IP</p>
      </div>
      <!-- end patient data -->
    </div>
    </div>
    <div class="container-fluid mt-2 bg-white pt-3 pb-3 card" v-if='patientData'>
      <div class="container">
        <div class="row mt-4 text-left">
          <div class="col-xl-8 col-lg-7">
            <div class="card shadow overflow-auto" style="height: 250px">
              <div
                class="card-header py-3 d-flex flex-row align-items-center justify-content-between">
                <h5 class="m-0">Diagnosis</h5>
              </div>
              <!-- Card Body -->
              <div class="card-body">
                <textarea
                  class="form-control rounded shadow-sm"
                  id="exampleFormControlTextarea1"
                  rows="5"
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
                ></textarea>
              </div>
            </div>
          </div>
        </div>
        <div class="row mt-4 text-left">
          <div class="col-md-12">
            <div class="card shadow">
              <div
                class="card-header py-3 d-flex flex-row align-items-center justify-content-between">
                <h5 class="m-0">Medical Details</h5>
              </div>
              <div class="card-body row">
                <div class="col-md-6">
                  <h4 class="font-weight-bold text-dark mt-3">
                    Pain Level <span class="float-right text-success">2</span>
                  </h4>
                  <div class="progress bg-light mb-4" style='height:60px'>
                    <input
                      class="progress-bar bg-danger w-100"
                      type="range"
                      min="0"
                      max="10"
                      step="1"
                      id="volume"/>
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
                        />
                        <label class="custom-control-label" for="customRadio1">f</label>
                      </div>
                    </span>
                    <span class="col-md-6">
                      <div class="custom-control custom-radio bg-light p-2 text-center">
                        <input
                          type="radio"
                          id="customRadio2"
                          name="customRadio"
                          class="custom-control-input"/>
                        <label class="custom-control-label" for="customRadio2"
                          >none</label
                        >
                      </div>
                    </span>
                  </div>
                </div>

                <div class="col-md-6">
                  <div class="bg-light p-3 w-100 shadow-sm">
                    <span class="ml-2 font-weight-bold text-dark">Diet :</span>
                    <select
                      class="form-control font-weight-normal text-secondary shadow-sm rounded"
                    >
                      <option>....................</option>
                    </select>
                  </div>
                </div>
                <div class="col-md-6">
                  <div class="bg-light p-3 mb-3 w-100 shadow-sm">
                    <span class="ml-2 font-weight-bold text-dark">Issolation :</span>
                    <select
                      class="form-control font-weight-normal text-secondary shadow-sm rounded"
                    >
                      <option>....................</option>
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
                  ></textarea>
                </div>
              </div>
            </div>
          </div>
        </div>
        <div class="text-center">
          <button class="special-btn">Submit</button>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  name: "insertPatientData",
  props: ["link"],
  data() {
    return {
      apiUrl: `http://localhost:${this.link}/endoresment/dist/`,
      patientData: null,
      id: this.$route.params.id,
    };
  },
  methods: {},
  created() {
    let that = this;
    // get patient data
    $.ajax({
      type: "POST",
      url: that.apiUrl + "endoresment/insertPatientData.aspx/getPatientData",
      data: JSON.stringify({ "id": { "id": that.id } }),
      contentType: "application/json; charset=utf-8",
      dataType: "json",
      success: function (data) {
        that.patientData = JSON.parse(data.d)[0];
      },
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
  font-size:16px;
}
.head-info {
  background-color: #f6f8fb;
}
</style>
