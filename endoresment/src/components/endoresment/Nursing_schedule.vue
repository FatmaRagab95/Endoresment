<template>
  <div class="Nursing_schedule">
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
                  <i class="fa fa-hospital-o mainColor mr-1"></i>Nursing_schedule</span
                >
              </div>

              <div class="cu-field">
                <h3 class="cu-label">
                  <label>Select Date :</label>
                </h3>
                <div class="cu-input text-box">
                  <span class="fa fa-calendar-o"></span>
                  <input :min="today" type="date" v-model="newSchdule.Date" required />
                </div>
              </div>

              <div class="cu-field">
                <h3 class="cu-label">
                  <label>Select Shift :</label>
                </h3>
                <div class="f-select">
                  <select
                    class="form-control form-control-sm"
                    v-model="newSchdule.Shift"
                    required
                  >
                    <option value='Day'>Day</option>
                    <option value='Night'>Night</option>
                  </select>
                </div>
              </div>

              <div class="cu-field">
                <h3 class="cu-label">
                  <label>Select Unit :</label>
                </h3>
                <div class="f-select">
                  <select
                    class="form-control form-control-sm"
                    v-model="newSchdule.Unit_id"
                    required
                  >
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
                        <label>Nurse Type</label>
                      </h3>
                    </div>
                    <div class="cu-field">
                      <input
                        id="Charge"
                        type="radio"
                        name="nurse"
                        value="17"
                        required
                        v-model="newSchdule.Nurse_role"
                      />
                      <label for="Charge">
                        <div class="cu-input cu-radio normal">
                          <div class="contain">
                            <span class="fa fa-check"></span>
                            <div>Charge Nurse</div>
                          </div>
                        </div>
                      </label>
                    </div>
                  </div>
                  <div class="col-md-6">
                    <div class="cu-field">
                      <input
                        id="Staff"
                        type="radio"
                        name="nurse"
                        value="12"
                        required
                        v-model="newSchdule.Nurse_role"
                      />
                      <label for="Staff">
                        <div class="cu-input cu-radio normal">
                          <div class="contain">
                            <span class="fa fa-check"></span>
                            <div>Staff Nurse</div>
                          </div>
                        </div>
                      </label>
                    </div>
                  </div>
                </div>
              </div>

              <div class="cu-field" v-if="newSchdule.Nurse_role == 12">
                <h3 class="cu-label">
                  <label>Select Staff Nurse :</label>
                </h3>
                <div class="f-select">
                  <select
                    class="form-control form-control-sm"
                    v-model="newSchdule.Nurse_id"
                    required
                  >
                    <option
                      v-for="nurse in displayNurses.filter((x) => x.Role_id == 12)"
                      :value="nurse.Emp_id"
                      :key="nurse.Emp_id"
                    >
                      {{ nurse.FullName }}
                    </option>
                  </select>
                </div>
              </div>

              <div class="cu-field" v-if="newSchdule.Nurse_role == 17">
                <h3 class="cu-label">
                  <label>Select Charge Nurse :</label>
                </h3>
                <div class="f-select">
                  <select
                    class="form-control form-control-sm"
                    v-model="newSchdule.Nurse_id"
                    required
                  >
                    <option
                      v-for="nurse in displayNurses.filter((x) => x.Role_id == 17)"
                      :value="nurse.Emp_id"
                      :key="nurse.Emp_id"
                    >
                      {{ nurse.FullName }}
                    </option>
                  </select>
                </div>
              </div>

              <div class="text-center">
                <button class="special-btn">Save</button>
              </div>
            </div>
          </div>
        </div>
      </form>
    </div>
  </div>
</template>

<script>
export default {
  name: "Nursing_schedule",
  props: ["link", "user"],
  data() {
    return {
      apiUrl: this.link,
      Units: [],
      adminusers: [],
      displayNurses: [],
      today: "",

      newSchdule: {
        Date: "",
        Shift: "",
        Unit_id: 0,
        Unit_name: "",
        Nurse_id: 0,
        Nurse_role: 0,
        Nurse_name: "",
      },
    };
  },
  watch: {
    'newSchdule.Unit_id': function () {
      this.displayNurses = this.adminusers.filter(x => x.Area_id == this.newSchdule.Unit_id);
    }
  },
  methods: {
    InsertPatient() {
      if ($("form").is(":valid")) {
        let that = this;

        that.newSchdule.Unit_name = that.Units.filter(
          (x) => x.U_id == that.newSchdule.Unit_id
        )[0].U_name;
        that.newSchdule.Nurse_name = that.adminusers.filter(
          (x) => x.Emp_id == that.newSchdule.Nurse_id
        )[0].FullName;

        //insert Nursing schedule
        $.ajax({
          type: "POST",
          url: that.apiUrl + "endoresment/Nursing_schedule.aspx/newSchdule",
          data: JSON.stringify({ data: that.newSchdule }),
          contentType: "application/json; charset=utf-8",
          dataType: "json",
          success: function (data) {
            swal({
              title: "Done!",
              text: "You successfully added a new schedule ...",
              icon: "success",
            });
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
    that.today = moment(new Date()).format("YYYY-MM-DD");

    //get Units
    $.ajax({
      type: "POST",
      url: that.apiUrl + "endoresment/Nursing_schedule.aspx/getUnitsData",
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
      url: that.apiUrl + "endoresment/Nursing_schedule.aspx/getadminusersData",
      contentType: "application/json; charset=utf-8",
      dataType: "json",
      success: function (data) {
        that.adminusers = JSON.parse(data.d);
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
  margin: 25px auto;
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
