<template>
  <div class="roomDetails p-3 text-capitalize">
    <div class="container-fluid mt-2 bg-white pt-3 pb-3 card" style="min-height: 100vh">
      <div class="mt-4 text-secondary card w-50 h-50 bg-light select-form">
        <lable class="text-success mt-3" style="text-decoration: underline"
          >Update date in 05-2-2020
        </lable>
      </div>
    </div>
    <div class="container-fluid mt-2 bg-white pt-3 pb-3 card d-none">
      <div class="container text-center">
        <h1 class="mt-4 text-secondary" style="text-decoration: underline">unit 11</h1>
        <p class="text-success mt-3">Update date in 05-2-2020</p>
      </div>
    </div>
    <div class="container-fluid mt-2 bg-white pt-3 pb-3 card viewPatients d-none">
      <div class="container">
        <div class="row text-center">
          <div class="col-md-4 p-2">
            <div class="bg-light shadow p-2 container card">
              <h4
                class="text-secondary font-weight-bold mb-3"
                style="text-decoration: underline"
              >
                room
                <span class="text-info">1101</span>
              </h4>

              <div class="shadow bg-white rounded row w-100 m-auto p-2">
                <div class="col-md-6 p-2">
                  <div
                    class="card p-2 bg-light shadow icon"
                    style="
                      height: 200px;
                      transition: 0.4s cubic-bezier(0.68, -0.55, 0.265, 1.55);
                      position: relative;
                    "
                  >
                    <div class="h-25 bg-light mb-2 rounded pt-2 shadow">
                      <span class="text-dark">1101</span>
                      <span class="text-white ml-2 bg-danger badge pl-2 pr-2">A</span>
                    </div>
                    <div
                      class="h-75 bg-light rounded pt-3 bed-background shadow card"
                    ></div>
                    <div class="tooltip bg-light card p-4 text-secondary text-capitalize">
                      <h6>
                        <span class="text-info">full name :</span
                        ><span class="ml-2">rabaa ali massoud</span>
                      </h6>
                      <h6>
                        <span class="text-info">age : </span
                        ><span class="ml-2">35 years</span>
                      </h6>
                      <h6>
                        <span class="text-info">gender : </span
                        ><span class="ml-2">female</span>
                      </h6>
                      <h6>
                        <span class="text-info">medical </span
                        ><span class="ml-2">number : 3425</span>
                      </h6>
                      <h6>
                        <span class="text-info">specialty : </span
                        ><span class="ml-2">--------</span>
                      </h6>
                      <button class="btn mt-3 btn-info btn-sm rounded">Details</button>
                    </div>
                    <h6 class="btn text-secondary mt-2 mb-0 p-0 name">
                      rabaa ali massoud
                    </h6>
                  </div>
                </div>
                <div class="col-md-6 p-2">
                  <div class="card p-2 bg-light shadow" style="height: 200px">
                    <div class="h-25 bg-light mb-2 rounded pt-2 shadow">
                      <span class="text-dark">1101</span>
                      <span class="text-white ml-2 bg-danger badge pl-2 pr-2">B</span>
                    </div>
                    <div
                      class="h-75 bg-light rounded pt-3 bed-background shadow card"
                    ></div>
                    <div class="tooltip bg-light card p-4 text-secondary text-capitalize">
                      <h6>
                        <span class="text-info text-capitalize">full name :</span
                        ><span class="ml-2 text-capitalize">rabaa ali massoud</span>
                      </h6>
                      <h6>
                        <span class="text-info text-capitalize">age : </span
                        ><span class="ml-2 text-capitalize">35 years</span>
                      </h6>
                      <h6>
                        <span class="text-info text-capitalize">gender : </span
                        ><span class="ml-2 text-capitalize">female</span>
                      </h6>
                      <h6>
                        <span class="text-info text-capitalize">medical </span
                        ><span class="ml-2 text-capitalize">number : 3425</span>
                      </h6>
                      <h6>
                        <span class="text-info text-capitalize">specialty : </span
                        ><span class="ml- text-capitalize2">--------</span>
                      </h6>
                      <button class="btn mt-3 btn-info btn-sm rounded text-capitalize">
                        details
                      </button>
                    </div>
                    <h6 class="text-secondary mt-2 mb-0 p-0">rabaa ali massoud</h6>
                  </div>
                </div>
              </div>
            </div>
          </div>
          <div class="col-md-4 p-2">
            <div class="bg-light shadow p-2 container card">
              <h4
                class="text-secondary font-weight-bold mb-3"
                style="text-decoration: underline"
              >
                room
                <span class="text-info">1103</span>
              </h4>

              <div class="shadow bg-white rounded row w-100 m-auto p-2">
                <div class="col-md-6 p-2">
                  <div class="card p-2 bg-light shadow" style="height: 200px">
                    <div class="h-25 bg-light mb-2 rounded pt-2 shadow">
                      <span class="text-dark">1103</span>
                      <span class="text-white ml-2 bg-danger badge pl-2 pr-2">A</span>
                    </div>
                    <div
                      class="h-75 bg-light rounded pt-3 bed-background shadow card"
                    ></div>
                    <div class="tooltip bg-light card p-4 text-secondary">
                      <h6>
                        <span class="text-info">full name :</span
                        ><span class="ml-2">rabaa ali massoud</span>
                      </h6>
                      <h6>
                        <span class="text-info">age : </span
                        ><span class="ml-2">35 years</span>
                      </h6>
                      <h6>
                        <span class="text-info">gender : </span
                        ><span class="ml-2">female</span>
                      </h6>
                      <h6>
                        <span class="text-info">medical </span
                        ><span class="ml-2">number : 3425</span>
                      </h6>
                      <h6>
                        <span class="text-info">specialty : </span
                        ><span class="ml-2">--------</span>
                      </h6>
                      <button class="btn mt-3 btn-info btn-sm rounded">details</button>
                    </div>
                    <h6 class="text-secondary mt-2 mb-0 p-0">rabaa ali massoud</h6>
                  </div>
                </div>
                <div class="col-md-6 p-2">
                  <div class="card p-2 bg-light shadow" style="height: 200px">
                    <div class="h-25 bg-light mb-2 rounded pt-2 shadow">
                      <span class="text-dark">1103</span>
                      <span class="text-white ml-2 bg-warning badge pl-2 pr-2">B</span>
                    </div>
                    <div
                      class="h-75 bg-light rounded pt-3 bed-background shadow card"
                    ></div>
                    <h6 class="text-warning mt-2 mb-0 p-0">maintenance</h6>
                  </div>
                </div>
              </div>
            </div>
          </div>
          <div class="col-md-4 p-2">
            <div class="bg-light shadow p-2 container card">
              <h4
                class="text-secondary font-weight-bold mb-3"
                style="text-decoration: underline"
              >
                room
                <span class="text-info">1105</span>
              </h4>

              <div class="shadow bg-white rounded row w-100 m-auto p-2">
                <div class="col-md-6 p-2">
                  <div class="card p-2 bg-light shadow" style="height: 200px">
                    <div class="h-25 bg-light mb-2 rounded pt-2 shadow">
                      <span class="text-dark">1105</span>
                      <span class="text-white ml-2 bg-danger badge pl-2 pr-2">A</span>
                    </div>
                    <div
                      class="h-75 bg-light rounded pt-3 bed-background shadow card"
                    ></div>
                    <div class="tooltip bg-light card p-4 text-secondary">
                      <h6>
                        <span class="text-info">full name :</span
                        ><span class="ml-2">rabaa ali massoud</span>
                      </h6>
                      <h6>
                        <span class="text-info">age : </span
                        ><span class="ml-2">35 years</span>
                      </h6>
                      <h6>
                        <span class="text-info">gender : </span
                        ><span class="ml-2">female</span>
                      </h6>
                      <h6>
                        <span class="text-info">medical </span
                        ><span class="ml-2">number : 3425</span>
                      </h6>
                      <h6>
                        <span class="text-info">specialty : </span
                        ><span class="ml-2">--------</span>
                      </h6>
                      <button class="btn mt-3 btn-info btn-sm rounded">details</button>
                    </div>
                    <h6 class="text-secondary mt-2 mb-0 p-0">rabaa ali massoud</h6>
                  </div>
                </div>
                <div class="col-md-6 p-2">
                  <div class="card p-2 bg-light shadow" style="height: 200px">
                    <div class="h-25 bg-light mb-2 rounded pt-2 shadow">
                      <span class="text-dark">1105</span>
                      <span class="text-white ml-2 bg-success badge pl-2 pr-2">B</span>
                    </div>
                    <div
                      class="h-75 bg-light rounded pt-3 bed-background shadow card"
                    ></div>
                    <h6 class="text-secondary mt-2 mb-0 p-0">----------------</h6>
                  </div>
                </div>
              </div>
            </div>
          </div>
          <div class="col-md-4 p-2">
            <div class="bg-light shadow p-2 container card">
              <h4
                class="text-secondary font-weight-bold mb-3"
                style="text-decoration: underline"
              >
                room
                <span class="text-info">1107</span>
              </h4>

              <div class="shadow bg-white rounded row w-100 m-auto p-2">
                <div class="col-md-6 p-2">
                  <div class="card p-2 bg-light shadow" style="height: 200px">
                    <div class="h-25 bg-light mb-2 rounded pt-2 shadow">
                      <span class="text-dark">1107</span>
                      <span class="text-white ml-2 bg-danger badge pl-2 pr-2">A</span>
                    </div>
                    <div
                      class="h-75 bg-light rounded pt-3 bed-background shadow card"
                    ></div>
                    <div class="tooltip bg-light card p-4 text-secondary">
                      <h6>
                        <span class="text-info">full name :</span
                        ><span class="ml-2">rabaa ali massoud</span>
                      </h6>
                      <h6>
                        <span class="text-info">age : </span
                        ><span class="ml-2">35 years</span>
                      </h6>
                      <h6>
                        <span class="text-info">gender : </span
                        ><span class="ml-2">female</span>
                      </h6>
                      <h6>
                        <span class="text-info">medical </span
                        ><span class="ml-2">number : 3425</span>
                      </h6>
                      <h6>
                        <span class="text-info">specialty : </span
                        ><span class="ml-2">--------</span>
                      </h6>
                      <button class="btn mt-3 btn-info btn-sm rounded">details</button>
                    </div>
                    <h6 class="text-secondary mt-2 mb-0 p-0">rabaa ali massoud</h6>
                  </div>
                </div>
                <div class="col-md-6 p-2">
                  <div class="card p-2 bg-light shadow" style="height: 200px">
                    <div class="h-25 bg-light mb-2 rounded pt-2 shadow">
                      <span class="text-dark">1107</span>
                      <span class="text-white ml-2 bg-danger badge pl-2 pr-2">B</span>
                    </div>
                    <div
                      class="h-75 bg-light rounded pt-3 bed-background shadow card"
                    ></div>
                    <div class="tooltip bg-light card p-4 text-secondary">
                      <h6>
                        <span class="text-info">full name :</span
                        ><span class="ml-2">rabaa ali massoud</span>
                      </h6>
                      <h6>
                        <span class="text-info">age : </span
                        ><span class="ml-2">35 years</span>
                      </h6>
                      <h6>
                        <span class="text-info">gender : </span
                        ><span class="ml-2">female</span>
                      </h6>
                      <h6>
                        <span class="text-info">medical </span
                        ><span class="ml-2">number : 3425</span>
                      </h6>
                      <h6>
                        <span class="text-info">specialty : </span
                        ><span class="ml-2">--------</span>
                      </h6>
                      <button class="btn mt-3 btn-info btn-sm rounded">details</button>
                    </div>
                    <h6 class="text-secondary mt-2 mb-0 p-0">rabaa ali massoud</h6>
                  </div>
                </div>
              </div>
            </div>
          </div>
          <div class="col-md-4 p-2">
            <div class="bg-light shadow p-2 container card">
              <h4
                class="text-secondary font-weight-bold mb-3"
                style="text-decoration: underline"
              >
                room
                <span class="text-info">1109</span>
              </h4>

              <div class="shadow bg-white rounded row w-100 m-auto p-2">
                <div class="col-md-6 p-2">
                  <div class="card p-2 bg-light shadow" style="height: 200px">
                    <div class="h-25 bg-light mb-2 rounded pt-2 shadow">
                      <span class="text-dark">1109</span>
                      <span class="text-white ml-2 bg-danger badge pl-2 pr-2">A</span>
                    </div>
                    <div
                      class="h-75 bg-light rounded pt-3 bed-background shadow card"
                    ></div>
                    <div class="tooltip bg-light card p-4 text-secondary">
                      <h6>
                        <span class="text-info">full name :</span
                        ><span class="ml-2">rabaa ali massoud</span>
                      </h6>
                      <h6>
                        <span class="text-info">age : </span
                        ><span class="ml-2">35 years</span>
                      </h6>
                      <h6>
                        <span class="text-info">gender : </span
                        ><span class="ml-2">female</span>
                      </h6>
                      <h6>
                        <span class="text-info">medical </span
                        ><span class="ml-2">number : 3425</span>
                      </h6>
                      <h6>
                        <span class="text-info">specialty : </span
                        ><span class="ml-2">--------</span>
                      </h6>
                      <button class="btn mt-3 btn-info btn-sm rounded">details</button>
                    </div>
                    <h6 class="text-secondary mt-2 mb-0 p-0">rabaa ali massoud</h6>
                  </div>
                </div>
                <div class="col-md-6 p-2">
                  <div class="card p-2 bg-light shadow" style="height: 200px">
                    <div class="h-25 bg-light mb-2 rounded pt-2 shadow">
                      <span class="text-dark">1109</span>
                      <span class="text-white ml-2 bg-success badge pl-2 pr-2">B</span>
                    </div>
                    <div
                      class="h-75 bg-light rounded pt-3 bed-background shadow card"
                    ></div>
                    <h6 class="text-secondary mt-2 mb-0 p-0">----------------</h6>
                  </div>
                </div>
              </div>
            </div>
          </div>
          <div class="col-md-4 p-2">
            <div class="bg-light shadow p-2 container card">
              <h4
                class="text-secondary font-weight-bold mb-3"
                style="text-decoration: underline"
              >
                room
                <span class="text-info">1111</span>
              </h4>

              <div class="shadow bg-white rounded row w-100 m-auto p-2">
                <div class="col-md-6 p-2">
                  <div class="card p-2 bg-light shadow" style="height: 200px">
                    <div class="h-25 bg-light mb-2 rounded pt-2 shadow">
                      <span class="text-dark">1111</span>
                      <span class="text-white ml-2 bg-danger badge pl-2 pr-2">A</span>
                    </div>
                    <div
                      class="h-75 bg-light rounded pt-3 bed-background shadow card"
                    ></div>
                    <div class="tooltip bg-light card p-4 text-secondary">
                      <h6>
                        <span class="text-info">full name :</span
                        ><span class="ml-2">rabaa ali massoud</span>
                      </h6>
                      <h6>
                        <span class="text-info">age : </span
                        ><span class="ml-2">35 years</span>
                      </h6>
                      <h6>
                        <span class="text-info">gender : </span
                        ><span class="ml-2">female</span>
                      </h6>
                      <h6>
                        <span class="text-info">medical </span
                        ><span class="ml-2">number : 3425</span>
                      </h6>
                      <h6>
                        <span class="text-info">specialty : </span
                        ><span class="ml-2">--------</span>
                      </h6>
                      <button class="btn mt-3 btn-info btn-sm rounded">details</button>
                    </div>
                    <h6 class="text-secondary mt-2 mb-0 p-0">rabaa ali massoud</h6>
                  </div>
                </div>
                <div class="col-md-6 p-2">
                  <div class="card p-2 bg-light shadow" style="height: 200px">
                    <div class="h-25 bg-light mb-2 rounded pt-2 shadow">
                      <span class="text-dark">1111</span>
                      <span class="text-white ml-2 bg-warning badge pl-2 pr-2">B</span>
                    </div>
                    <div
                      class="h-75 bg-light rounded pt-3 bed-background shadow card"
                    ></div>
                    <h6 class="text-warning mt-2 mb-0 p-0">maintenance</h6>
                  </div>
                </div>
              </div>
            </div>
          </div>
          <div class="col-md-4 p-2">
            <div class="bg-light shadow p-2 container card">
              <h4
                class="text-secondary font-weight-bold mb-3"
                style="text-decoration: underline"
              >
                room
                <span class="text-info">1113</span>
              </h4>

              <div class="shadow bg-white rounded row w-100 m-auto p-2">
                <div class="col-md-3 p-2"></div>
                <div class="col-md-6 p-2">
                  <div class="card p-2 bg-light shadow" style="height: 200px">
                    <div class="h-25 bg-light mb-2 rounded pt-2 shadow">
                      <span class="text-dark">1113</span>
                      <span class="text-success ml-2 bg-success badge pl-2 pr-2">A</span>
                    </div>
                    <div
                      class="h-75 bg-light rounded pt-3 bed-background-single shadow card"
                    ></div>
                    <h6 class="text-secondary mt-2 mb-0 p-0">----------------</h6>
                  </div>
                </div>
                <div class="col-md-3 p-2"></div>
              </div>
            </div>
          </div>
          <div class="col-md-4 p-2">
            <div class="bg-light shadow p-2 container card">
              <h4
                class="text-secondary font-weight-bold mb-3"
                style="text-decoration: underline"
              >
                room
                <span class="text-info">1115</span>
              </h4>

              <div class="shadow bg-white rounded row w-100 m-auto p-2">
                <div class="col-md-3 p-2"></div>
                <div class="col-md-6 p-2">
                  <div class="card p-2 bg-light shadow" style="height: 200px">
                    <div class="h-25 bg-light mb-2 rounded pt-2 shadow">
                      <span class="text-dark">1115</span>
                      <span class="text-danger ml-2 bg-danger badge pl-2 pr-2">A</span>
                    </div>
                    <div
                      class="h-75 bg-light rounded pt-3 bed-background-single shadow card"
                    ></div>
                    <div class="tooltip bg-light card p-4 text-secondary">
                      <h6>
                        <span class="text-info">full name :</span
                        ><span class="ml-2">rabaa ali massoud</span>
                      </h6>
                      <h6>
                        <span class="text-info">age : </span
                        ><span class="ml-2">35 years</span>
                      </h6>
                      <h6>
                        <span class="text-info">gender : </span
                        ><span class="ml-2">female</span>
                      </h6>
                      <h6>
                        <span class="text-info">medical </span
                        ><span class="ml-2">number : 3425</span>
                      </h6>
                      <h6>
                        <span class="text-info">specialty : </span
                        ><span class="ml-2">--------</span>
                      </h6>
                      <button class="btn mt-3 btn-info btn-sm rounded">details</button>
                    </div>
                    <h6 class="text-secondary mt-2 mb-0 p-0">rabaa ali massoud</h6>
                  </div>
                </div>
                <div class="col-md-3 p-2"></div>
              </div>
            </div>
          </div>
          <div class="col-md-4 p-2">
            <div class="bg-light shadow p-2 container card">
              <h4
                class="text-secondary font-weight-bold mb-3"
                style="text-decoration: underline"
              >
                room
                <span class="text-info">1117</span>
              </h4>

              <div class="shadow bg-white rounded row w-100 m-auto p-2">
                <div class="col-md-3 p-2"></div>
                <div class="col-md-6 p-2">
                  <div class="card p-2 bg-light shadow" style="height: 200px">
                    <div class="h-25 bg-light mb-2 rounded pt-2 shadow">
                      <span class="text-dark">1117</span>
                      <span class="text-danger ml-2 bg-danger badge pl-2 pr-2">A</span>
                    </div>
                    <div
                      class="h-75 bg-light rounded pt-3 shadow card bed-background-single"
                    ></div>
                    <div class="tooltip bg-light card p-4 text-secondary">
                      <h6>
                        <span class="text-info">Full Name :</span
                        ><span class="ml-2">Rabaa Ali Massoud</span>
                      </h6>
                      <h6>
                        <span class="text-info">Age : </span
                        ><span class="ml-2">35 Years</span>
                      </h6>
                      <h6>
                        <span class="text-info">Gender : </span
                        ><span class="ml-2">Female</span>
                      </h6>
                      <h6>
                        <span class="text-info">Medical </span
                        ><span class="ml-2">Number : 3425</span>
                      </h6>
                      <h6>
                        <span class="text-info">Specialty : </span
                        ><span class="ml-2">--------</span>
                      </h6>
                      <button class="btn mt-3 btn-info btn-sm rounded">Details</button>
                    </div>
                    <h6 class="text-secondary mt-2 mb-0 p-0">Rabaa ali massoud</h6>
                  </div>
                </div>
                <div class="col-md-3 p-2"></div>
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
  name: "roomDetails",
  props: ["link"],
  data() {
    return {
      getUnitsData: [],
      apiUrl: `http://localhost:${this.link}/endoresment/dist/`,
    };
  },
  methods: {},
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
  },
};
</script>

<style scoped>
.select-form {
  position: absolute;
  left: 50%;
  top: 50%;
  transform: translateX(-50%);
  transform: translateY(-50%);
}
.viewPatients {
  min-height: 100vh;
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
