<template>
  <div class="patientData p-3 text-capitalize">
    <div class="container-fluid mt-2 bg-white pt-3 pb-3 card">
      <div class="container">
        <div class="row form-group text-center bg-info pt-3 pb-3">
          <div class="col-md-4">
            <div class="shadow-sm bg-white pt-3 pb-3">
              <span class="text-info">name :</span
              ><span class="ml-2">Rabaa Ali Massoud</span>
            </div>
          </div>
          <div class="col-md-4">
            <div class="shadow-sm bg-white pt-3 pb-3">
              <span class="text-info">medical number :</span
              ><span class="ml-2">4567</span>
            </div>
          </div>
          <div class="col-md-4">
            <div class="shadow-sm bg-white pt-3 pb-3">
              <span class="text-info">age:</span><span class="ml-2">35 years</span>
            </div>
          </div>
        </div>

        <div class="row form-group text-center mt-4" style="font-size: 25px">
          <div class="col-md-4">
            <span class="pull-left">
              <span class="text-secondary">unit :</span
              ><span
                class="ml-2 badge card badge-success text-uppercase pl-4 pr-4 shadow-sm"
                >icu</span
              ></span
            >
          </div>
          <div class="col-md-4" style="text-decoration: underline; font-size: 28px">
            <span class="text-success">date of admission :</span
            ><span class="ml-2 text-secondary">25-2-2020</span>
          </div>
          <div class="col-md-4">
            <span class="pull-right">
              <span class="text-secondary">room:</span>
              <span class="ml-2 badge card badge-success pl-4 pr-4 shadow-sm">35</span>
            </span>
          </div>
        </div>
        <hr />
        <div class="row form-group">
          <div class="col-md-4">
            <span class="pull-left bg-light p-3 shadow-sm">
              <i class="fa fa-address-card-o text-secondary" aria-hidden="true"></i>
              <span class="ml-2 text-info">specialty :</span
              ><span class="ml-2 text-danger">neurosurgery</span></span
            >
          </div>
          <div class="col-md-4 text-center pt-4">
            ......................................
          </div>
          <div class="col-md-4">
            <span class="pull-right bg-light p-3 shadow-sm">
              <i class="fa fa-user-md text-secondary" aria-hidden="true"></i>
              <span class="ml-2 text-info">consultant :</span
              ><span class="ml-2 text-danger">ahmad mamdouh</span></span
            >
          </div>
        </div>
      </div>
    </div>
    <div class="container-fluid mt-2 bg-white pt-3 pb-3 card viewPatients">
      <div class="container">
        <div class="row mt-4 text-left">
          <div class="col-xl-8 col-lg-7">
            <div class="card shadow overflow-auto" style="height: 250px">
              <div
                class="card-header py-3 d-flex flex-row align-items-center justify-content-between"
              >
                <h6 class="m-0 font-weight-bold text-info">diagnosis</h6>
                <div class="dropdown no-arrow show">
                  <a
                    class="fa fa-pencil-square-o text-danger"
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
                <i class="fa fa-circle text-danger small"></i>
                Vast amounts of medical information are still recorded as unstructured
                text. The knowledge contained in this textual data has a great potential
                to
                <br />
                <i class="fa fa-circle text-success small"></i>
                Vast amounts of medical information are still recorded as unstructured
                text. The knowledge contained in this textual data has a great potential
                to
                <br />
                <i class="fa fa-circle text-info small"></i>
                Vast amounts of medical information are still recorded as unstructured
                text. The knowledge contained in this textual data has a great potential
                to
              </div>
            </div>
          </div>

          <div class="col-xl-4 col-lg-5">
            <div class="card shadow overflow-auto" style="height: 250px">
              <div
                class="card-header py-3 d-flex flex-row align-items-center justify-content-between"
              >
                <h6 class="m-0 font-weight-bold text-info">progress notes</h6>
                <div class="dropdown no-arrow show">
                  <a
                    class="fa fa-pencil-square-o text-danger"
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
                <i class="fa fa-circle text-danger small"></i>
                Vast amounts of medical information are still recorded as unstructured
                text. The knowledge contained in this textual data has a great potential
                to
                <br />
                <i class="fa fa-circle text-success small"></i>
                Vast amounts of medical information are still recorded as unstructured
                text. The knowledge contained in this textual data has a great potential
                to
                <br />
                <i class="fa fa-circle text-info small"></i>
                Vast amounts of medical information are still recorded as unstructured
                text. The knowledge contained in this textual data has a great potential
                to
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
                <h5 class="m-0 font-weight-bold text-info">medical details</h5>
                <div class="dropdown no-arrow show">
                  <a
                    class="fa fa-pencil-square-o text-danger"
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
                <div class="col-md-12">
                  <h4 class="font-weight-bold text-dark">
                    pain level <span class="float-right text-success">20%</span>
                  </h4>
                  <div class="progress mb-4">
                    <input
                      class="progress-bar bg-danger w-100"
                      type="range"
                      min="0"
                      max="100"
                      step="5"
                      id="volume"
                    />
                  </div>
                </div>

                <div class="col-md-6">
                  <div class="bg-light pt-2 pb-2 mb-3 w-100 shadow-sm">
                    <span class="ml-2 font-weight-bold text-dark">Diet :</span>
                    <span class="font-weight-normal text-secondary ml-2">Diet ...</span>
                  </div>
                </div>
                <div class="col-md-6">
                  <div class="bg-light pt-2 pb-2 mb-3 w-100 shadow-sm">
                    <span class="ml-2 font-weight-bold text-dark">Allergy :</span>
                    <span class="font-weight-normal text-secondary ml-2"
                      >Allergy ...</span
                    >
                  </div>
                </div>
                <hr />
                <div class="col-md-6">
                  <div class="bg-light pt-2 pb-2 mb-3 w-100 shadow-sm">
                    <span class="ml-2 font-weight-bold text-dark">Issolation :</span>
                    <span class="font-weight-normal text-secondary ml-2"
                      >Issolation ...</span
                    >
                  </div>
                </div>
                <div class="col-md-6">
                  <div class="bg-light pt-2 pb-2 mb-3 w-100 shadow-sm">
                    <span class="ml-2 font-weight-bold text-dark">Fall :</span>
                    <span class="font-weight-normal text-secondary ml-2">Fall ...</span>
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
                <h5 class="m-0 font-weight-bold text-info">To Do</h5>
                <div class="dropdown no-arrow show">
                  <a
                    class="fa fa-pencil-square-o text-danger"
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
                  <i class="fa fa-circle text-success small"></i>
                  Vast amounts of medical information are still recorded as unstructured
                  text. The knowledge contained in this textual data has a great potential
                  to
                </div>
              </div>
            </div>
          </div>
          <div class="col-md-3">
            <div class="card shadow overflow-auto" style="height: 250px">
              <div
                class="card-header py-3 d-flex flex-row align-items-center justify-content-between"
              >
                <h5 class="m-0 font-weight-bold text-info">Follow up</h5>
                <div class="dropdown no-arrow show">
                  <a
                    class="fa fa-pencil-square-o text-danger"
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
                  <i class="fa fa-circle text-success small"></i>
                  Vast amounts of medical information are still recorded as unstructured
                  text. The knowledge contained in this textual data has a great potential
                  to
                </div>
              </div>
            </div>
          </div>
          <div class="col-md-3">
            <div class="card shadow overflow-auto" style="height: 250px">
              <div
                class="card-header py-3 d-flex flex-row align-items-center justify-content-between"
              >
                <h5 class="m-0 font-weight-bold text-info">Complications</h5>
                <div class="dropdown no-arrow show">
                  <a
                    class="fa fa-pencil-square-o text-danger"
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
                  <i class="fa fa-circle text-success small"></i>
                  Vast amounts of medical information are still recorded as unstructured
                  text. The knowledge contained in this textual data has a great potential
                  to
                </div>
              </div>
            </div>
          </div>
          <div class="col-md-3">
            <div class="card shadow overflow-auto" style="height: 250px">
              <div
                class="card-header py-3 d-flex flex-row align-items-center justify-content-between"
              >
                <h5 class="m-0 font-weight-bold text-info">Routes Plan of Care</h5>
                <div class="dropdown no-arrow show">
                  <a
                    class="fa fa-pencil-square-o text-danger"
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
                  <i class="fa fa-circle text-success small"></i>
                  Vast amounts of medical information are still recorded as unstructured
                  text. The knowledge contained in this textual data has a great potential
                  to
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  name: "patientData",
  data() {
    return {
      apiUrl: "http://localhost:52861/endoresment/dist/",
    };
  },
  methods: {},
  created() {
    let that = this;
  },
};
</script>

<style scoped>
.viewPatients {
  min-height: 100vh;
  background: #fff url("../../assets/layout/img/humaaans-doctors.png") right no-repeat;
  background-size: contain;
  background-color: #f6f8fb;
  text-align: center;
  position: relative;
  overflow: hidden;
}
</style>
