<template>
  <div class="display_schdule">
    <div class="container-wave">
      <div class="wave"></div>
    </div>

    <div class="internal-page" id="auth">
      <form @submit.prevent="InsertPatient" v-if="selectedMonth.length == 0">
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
                    <option v-for="month in monthes" :value="month.id" :key="month.id">
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
                    <option v-for="unit in Units" :value="unit.U_id" :key="unit.U_id">
                      {{ unit.U_name }}
                    </option>
                  </select>
                </div>
              </div>

              <!-- <div class="cu-field radio-group">
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
              </div>-->

              <div class="text-center">
                <button class="special-btn" @click.prevent="monthDays">
                  Display Nursing Schedule
                </button>
              </div>
            </div>
          </div>
        </div>
      </form>
      <div>{{ selectedMonth }}</div>
      <form v-if="selectedMonth.length > 0">
        <div class="custom-form pt-3">
          <div class="cu-container">
            <div class="cu-form-group special" style="max-width: 100%">
              <div class="row p-4">
                <span class="col-md-6"
                  ><span class="text-info mr-2">Month :</span
                  >{{ monthes.filter((x) => x.id == month)[0].name }}</span
                >
                <span class="col-md-6"
                  ><span class="text-info mr-2">Unit :</span>
                  {{ Units.filter((x) => x.U_id == unit)[0].U_name }}</span
                >
              </div>
              <!-- start table-->
              <div class="scroll-box" v-dragscroll>
                <table class="table table-bordered text-center">
                  <thead>
                    <tr>
                      <th class="p-0 m-0 bg-secondary text-white">
                        <div class="mb-1">No</div>
                      </th>
                      <th class="p-0 m-0 bg-warning text-white">
                        <div class="mb-1">Nurse Name</div>
                      </th>
                      <th class="p-0 m-0 bg-secondary text-white">
                        <div class="mb-1">ID</div>
                      </th>
                      <th
                        style="font-size: 12px"
                        v-for="(day, i) in monthCalendar"
                        :key="i"
                        class="p-0 m-0"
                      >
                        <div class="bg-warning text-white w-100 m-0 p-0 border-bottom">
                          {{ day.split("-")[0] }}
                        </div>

                        <div class="bg-warning text-white w-100 m-0 p-0">
                          {{ day.split("-")[1] }}
                        </div>
                      </th>
                      <th class="p-0 m-0 bg-secondary text-white">
                        <div class="mb-1">Total</div>
                      </th>
                    </tr>
                  </thead>
                  <tbody>
                    <tr v-for="(nurse, i) in selectedMonth" :key="nurse.Id">
                      <td class="bg-secondary text-white p-0 m-0">{{ i + 1 }}</td>
                      <td class="p-0 m-0 text-left pl-3">{{ nurse.Nurse_name }}</td>
                      <td class="bg-secondary text-white p-0 m-0">
                        {{ nurse.Nurse_id }}
                      </td>
                      <td v-for="(day, i) in monthCalendar" :key="i" class="p-0 m-0">
                        <div
                          v-if="day.split('-')[1] == 'Fri'"
                          class="bg-warning text-warning w-100 h-100 m-0 p-0"
                        >
                          {{ day.split("-")[0] }}
                        </div>
                        <div v-else class="bg-white text-dark w-100 m-0 p-0">1</div>
                      </td>
                      <td class="bg-secondary text-white p-0 m-0">total</td>
                    </tr>
                  </tbody>
                </table>
              </div>

              <!-- end table-->
            </div>
          </div>
        </div>
      </form>
    </div>
  </div>
</template>

<script>
import { dragscroll } from "vue-dragscroll";

export default {
  directives: {
    dragscroll,
  },
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
        { name: "January", id: "01" },
        { name: "February", id: "02" },
        { name: "March", id: "03" },
        { name: "April", id: "04" },
        { name: "May", id: "05" },
        { name: "June", id: "06" },
        { name: "July", id: "07" },
        { name: "August", id: "08" },
        { name: "September", id: "09" },
        { name: "October", id: "10" },
        { name: "November", id: "11" },
        { name: "December", id: "12" },
      ],

      month: "01",
      unit: 1,
      shift: "",

      monthCalendar: [],
      selectedMonth: [],
    };
  },
  methods: {
    monthDays: function () {
      function getMonths(month, year) {
        var arr = [];
        var start = moment(year + "-" + month, "YYYY-MMM");
        for (
          var end = moment(start).add(1, "month");
          start.isBefore(end);
          start.add(1, "day")
        ) {
          arr.push(start.format("D-ddd"));
        }
        return arr;
      }
      this.monthCalendar = getMonths(
        this.monthes.filter((x) => x.id == this.month)[0].name.substring(0, 3),
        new Date().getFullYear()
      );

      this.selectedMonth = this.Endorsement_Nursing_schedule.filter(
        (x) => x.Unit_id == this.unit && x.Date.split("-")[1] == this.month
      );
    },
  },

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
  margin-top: 100px;
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

.calendar {
  display: grid;
  width: 100%;
  grid-template-columns: repeat(7, minmax(60px, 1fr));
  grid-template-rows: 60px;
  grid-auto-rows: 60px;
  overflow: auto;
}
.calendar-container {
  width: 90%;
  margin: auto;
  overflow: hidden;
  box-shadow: 0 2px 20px rgba(0, 0, 0, 0.1);
  border-radius: 10px;
  background: #fff;
  max-width: 500px;
}
.day {
  border-bottom: 1px solid rgba(166, 168, 179, 0.5);
  border-right: 1px solid rgba(166, 168, 179, 0.5);
  text-align: right;
  padding: 14px 20px;
  letter-spacing: 1px;
  font-size: 14px;
  box-sizing: border-box;
  position: relative;
  pointer-events: none;
  z-index: 1;
}
.day.today {
  background-color: #0092ff;
  color: #fff;
}
.day-name {
  font-size: 16px;
  text-transform: uppercase;
  color: #0092ff;
  text-align: center;
  border-bottom: 1px solid rgba(166, 168, 179, 0.5);
  line-height: 50px;
  font-weight: 500;
}
</style>
