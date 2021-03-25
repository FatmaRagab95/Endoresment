<template>
  <div class="display_schdule">
    <div class="container-wave">
      <div class="wave"></div>
    </div>

    <div class="internal-page" id="auth">
      <form @submit.prevent="InsertPatient">
        <div class="custom-form pt-3">
          <div class="cu-container">
            <div class="cu-form-group special" style="max-width: 900px">
              <div class="title">
                <span>
                  <i class="fa fa-hospital-o mainColor mr-1"></i>Display Nursing
                  Schedule</span
                >
              </div>

              <div class="cu-field">
                <h3 class="cu-label">
                  <label>Select month :</label>
                </h3>
                <div class="f-select">
                  <select class="form-control form-control-sm" v-model="month" required>
                    <option v-for="month in monthes" :value="month.id" :key="month">
                      {{ month.name }}
                    </option>
                  </select>
                </div>
              </div>

              <div class="cu-field">
                <h3 class="cu-label">
                  <label>Select Unit :</label>
                </h3>
                <div class="f-select">
                  <select class="form-control form-control-sm" v-model="unit" required>
                    <option v-for="unit in Units" :value="unit.U_id" :key="unit.U_name">
                      {{ unit.U_name }}
                    </option>
                  </select>
                </div>
              </div>

              <div class="cu-field radio-group">
                <div class="row align-items-end">
                  <div class="col-md-6">
                    <div class="cu-field">
                      <h3 class="cu-label">
                        <label>Select shift</label>
                      </h3>
                    </div>
                    <div class="cu-field">
                      <input
                        id="Day"
                        type="radio"
                        name="shift"
                        value="Day"
                        required
                        v-model="shift"
                      />
                      <label for="Day">
                        <div class="cu-input cu-radio normal">
                          <div class="contain">
                            <span class="fa fa-check"></span>
                            <div>Day</div>
                          </div>
                        </div>
                      </label>
                    </div>
                  </div>
                  <div class="col-md-6">
                    <div class="cu-field">
                      <input
                        id="Night"
                        type="radio"
                        name="shift"
                        value="Night"
                        required
                        v-model="shift"
                      />
                      <label for="Night">
                        <div class="cu-input cu-radio normal">
                          <div class="contain">
                            <span class="fa fa-check"></span>
                            <div>Night</div>
                          </div>
                        </div>
                      </label>
                    </div>
                  </div>
                </div>
              </div>

              <div class="text-center">
                <button class="special-btn">Display Nursing Schedule</button>
              </div>
            </div>
          </div>
        </div>
      </form>
      <form>
        <div class="custom-form pt-3">
          <div class="cu-container">
            <div class="cu-form-group special" style="max-width: 1200px">
              <div class="row p-4">
                <span class="col-md-4"
                  ><span class="text-info mr-2">Month :</span
                  >{{ monthes.filter((x) => x.id == month)[0].name }}</span
                >
                <span class="col-md-4"
                  ><span class="text-info mr-2">Unit :</span>
                  {{ Units.filter((x) => x.U_id == unit)[0].U_name }}</span
                >
                <span class="col-md-4"
                  ><span class="text-info mr-2">Shift :</span>{{ shift }}</span
                >
              </div>
              <table class="table table-striped shadow-sm">
                <thead>
                  <tr>
                    <th scope="col">No</th>
                    <th scope="col">Nurse Name</th>
                    <th scope="col">ID</th>
                    <th scope="col">
                      <div>1</div>
                      <hr />
                      <div>sat</div>
                    </th>
                  </tr>
                </thead>
                <tbody>
                  <tr>
                    <th scope="row">1</th>
                    <td>Mark</td>
                    <td>Otto</td>
                    <td>@mdo</td>
                  </tr>
                  <tr>
                    <th scope="row">2</th>
                    <td>Jacob</td>
                    <td>Thornton</td>
                    <td>@fat</td>
                  </tr>
                  <tr>
                    <th scope="row">3</th>
                    <td>Larry</td>
                    <td>the Bird</td>
                    <td>@twitter</td>
                  </tr>
                </tbody>
              </table>
            </div>
          </div>
        </div>
      </form>
    </div>
  </div>
</template>

<script>
export default {
  name: "display_schdule",
  props: ["link", "user"],
  data() {
    return {
      apiUrl: this.link,
      Units: [],
      adminusers: [],
      Endorsement_Nursing_schedule: [],
      today: "",

      monthes: [
        { name: "january", id: "01" },
        { name: "february", id: "02" },
        { name: "march", id: "03" },
        { name: "april", id: "04" },
        { name: "may", id: "05" },
        { name: "june", id: "06" },
        { name: "july", id: "07" },
        { name: "august", id: "08" },
        { name: "september", id: "09" },
        { name: "october", id: "10" },
        { name: "november", id: "11" },
        { name: "december", id: "12" },
      ],

      month: "01",
      unit: 1,
      shift: "",
    };
  },
  methods: {},

  created() {
    let that = this;

    //get Units
    $.ajax({
      type: "POST",
      url: that.apiUrl + "endoresment/display_schdule.aspx/getUnitsData",
      data: JSON.stringify({ branch: { id: that.user.Branch_ID } }),
      contentType: "application/json; charset=utf-8",
      dataType: "json",
      success: function (data) {
        that.Units = JSON.parse(data.d);
      },
    });

    //get getadminusersData
    $.ajax({
      type: "POST",
      url: that.apiUrl + "endoresment/display_schdule.aspx/getadminusersData",
      contentType: "application/json; charset=utf-8",
      dataType: "json",
      success: function (data) {
        that.adminusers = JSON.parse(data.d);
      },
    });

    //get getEndorsement_Nursing_scheduleData
    $.ajax({
      type: "POST",
      url:
        that.apiUrl +
        "endoresment/display_schdule.aspx/getEndorsement_Nursing_scheduleData",
      contentType: "application/json; charset=utf-8",
      dataType: "json",
      success: function (data) {
        that.Endorsement_Nursing_schedule = JSON.parse(data.d);
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
  margin: 15px auto;
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
}
</style>
