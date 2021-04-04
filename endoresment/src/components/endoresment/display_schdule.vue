<template>
  <div class="display_schdule">
    <div class="internal-page">
      <form @submit.prevent="InsertPatient" v-if="!displayTable">
        <div class="custom-form">
          <div class="cu-container">
            <div class="cu-form-group special" style="max-width: 900px">
              <div class="title">
                <span>
                  <i class="fa fa-hospital-o mainColor mr-1"></i>Display Nursing
                  Schedule</span
                >
              </div>

              <div class="cu-field">
                <v-select
                  :items="Units"
                  item-text="U_name"
                  :item-value="'U_id'"
                  label="Select Unit"
                  name="unit"
                  v-model="unit"
                ></v-select>
              </div>

              <v-row justify="center" class="mt-3">
                <v-date-picker
                  class="bg-primary shadow m-auto"
                  v-model="month"
                  type="month"
                ></v-date-picker>
              </v-row>

              <div class="text-center">
                <button class="special-btn shadow" @click.prevent="monthDays">
                  Display Nursing Schedule
                </button>
              </div>
            </div>
          </div>
        </div>
      </form>

      <form v-if="displayTable">
        <div class="container-fluid pt-5">
          <div class="card shadow">
            <div class="row p-4 align-items-center">
              <div class="col-lg-4 col-md-6">
                <span class="text-dark mr-2 font-weight-bold">Month :</span>
                {{ date }}
              </div>
              <div class="col-lg-4 col-md-6">
                <span class="text-dark mr-2 font-weight-bold">Unit :</span>
                {{ Units.filter((x) => x.U_id == unit)[0].U_name }}
              </div>
              <div class="col-lg-4 col-md-6">
                <v-select
                  :items="items"
                  label="Filter"
                  v-model="filtered"
                  @change="filterTable()"
                ></v-select>
              </div>
            </div>

            <v-btn
              class="mx-2"
              fab
              bottom
              left
              absolute
              dark
              big
              color="green accent-3"
              @click="displayTable = !displayTable"
            >
              <v-icon dark> mdi-pencil </v-icon>
            </v-btn>
            <v-btn
              class="mx-2"
              fab
              bottom
              right
              absolute
              dark
              big
              color="red accent-3"
              data-toggle="modal"
              data-target="#exampleModalCenter"
            >
              <v-icon dark> mdi-calendar-edit </v-icon>
            </v-btn>
          </div>
        </div>

        <div class="container-fluid pt-5 pb-5">
          <div class="card shadow p-3">
            <v-simple-table fixed-header height="350px">
              <template v-slot:default>
                <thead>
                  <tr>
                    <th>#</th>
                    <th>Staff Name</th>
                    <th>ID</th>
                    <th
                      v-for="(day, i) in monthCalendar"
                      :key="day.day"
                      class="text-center pl-1 pr-1"
                      :class="day.day.substr(-3, 3) == 'Fri' ? 'bg-grey' : 'bg-white'"
                    >
                      <span class="d-block">{{ i + 1 }}</span>
                      <span class="d-block">{{ day.day.substr(-3, 3) }}</span>
                    </th>
                    <th class="bg-primary text-white">Total</th>
                  </tr>
                </thead>
                <tbody v-if="displayed.length > 0">
                  <tr v-for="(nurse, i) in displayed" :key="nurse.Nurse_id">
                    <td>{{ i + 1 }}</td>
                    <td style="min-width: 120px">{{ nurse.Nurse_name }}</td>
                    <td>{{ nurse.Nurse_id }}</td>
                    <td v-for="shift in nurse.shifts" :key="shift.fullDate">
                      <span
                        class="calendar-span"
                        :class="
                          shift.shift
                            ? 'bg-success'
                            : shift.name.substr(-3, 3) != 'Fri'
                            ? 'bg-light border'
                            : 'bg-grey'
                        "
                        >{{ shift.shift }}</span
                      >
                    </td>
                    <td class="bg-primary text-white text-center">{{ nurse.Total }}</td>
                  </tr>
                </tbody>
              </template>
            </v-simple-table>
          </div>
        </div>
      </form>
    </div>

    <!-- start edit Modal -->
    <div
      class="modal fade"
      id="exampleModalCenter"
      tabindex="-1"
      role="dialog"
      aria-labelledby="exampleModalCenterTitle"
      aria-hidden="true"
    >
      <div
        class="modal-dialog modal-dialog-centered"
        role="document"
        style="max-width: 1000px; max-height: 600px; margin-top: 100px"
      >
        <div class="modal-content">
          <div class="modal-header">
            <h5 class="modal-title" id="exampleModalLongTitle">Edit Schdule</h5>
            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
              <span aria-hidden="true">&times;</span>
            </button>
          </div>
          <div class="modal-body">
            <div class="container-fluid pt-5 pb-5">
              <div class="card shadow p-3">
                <v-simple-table fixed-header height="350px">
                  <template v-slot:default>
                    <thead>
                      <tr>
                        <th>#</th>
                        <th>Staff Name</th>
                        <th>ID</th>
                        <th
                          v-for="(day, i) in monthCalendar"
                          :key="day.day"
                          class="text-center pl-1 pr-1"
                          :class="day.day.substr(-3, 3) == 'Fri' ? 'bg-grey' : 'bg-white'"
                        >
                          <span class="d-block">{{ i + 1 }}</span>
                          <span class="d-block">{{ day.day.substr(-3, 3) }}</span>
                        </th>
                        <th class="bg-primary text-white">Total</th>
                      </tr>
                    </thead>
                    <tbody v-if="displayed.length > 0">
                      <tr v-for="(nurse, i) in displayed" :key="nurse.Nurse_id">
                        <td>{{ i + 1 }}</td>
                        <td style="min-width: 120px">{{ nurse.Nurse_name }}</td>
                        <td>{{ nurse.Nurse_id }}</td>
                        <td v-for="shift in nurse.shifts" :key="shift.fullDate">
                          <input
                            :class="
                              shift.shift
                                ? 'bg-success'
                                : shift.name.substr(-3, 3) != 'Fri'
                                ? 'bg-light border'
                                : 'bg-grey'
                            "
                            v-model="shift.shift"
                          />
                        </td>
                        <td class="bg-primary text-white text-center">
                          {{ nurse.Total }}
                        </td>
                      </tr>
                    </tbody>
                  </template>
                </v-simple-table>
                <div class="modal-footer">
                  <button type="button" class="btn btn-secondary" data-dismiss="modal">
                    Close
                  </button>
                  <button type="button" class="btn btn-primary">edit</button>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
    <!-- end edit Modal -->
  </div>
</template>

<script>
import { dragscroll } from "vue-dragscroll";

export default {
  name: "display_schdule",
  props: ["link", "user"],
  directives: {
    dragscroll,
  },
  data() {
    return {
      apiUrl: this.link,
      items: ["All", "Nurses", "Charge Nurses"],
      filtered: "All",
      Units: [],
      Nurses: [],
      displayed: [],
      Endorsement_Nursing_schedule: [],

      displayTable: false,
      date: "",

      month: "",
      unit: 1,
      shift: "",

      monthCalendar: [],
      selectedMonth: [],
    };
  },
  methods: {
    monthDays: function () {
      function getMonths(date) {
        var arr = [];
        var start = moment(date, "YYYY-MM");
        for (
          var end = moment(start).add(1, "month");
          start.isBefore(end);
          start.add(1, "day")
        ) {
          arr.push({
            fullDate: start.format("YYYY-MM-DD"),
            day: start.format("D-ddd"),
          });
        }
        return arr;
      }
      this.monthCalendar = getMonths(this.month);
      this.date = moment(this.month).format("MMMM-YYYY");

      //get getEndorsement_Nursing_scheduleData
      let that = this;
      $.ajax({
        type: "POST",
        url:
          that.apiUrl +
          "endoresment/display_schdule.aspx/getEndorsement_Nursing_scheduleData",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ unit: { U_id: that.unit } }),
        dataType: "json",
        success: function (data) {
          that.Endorsement_Nursing_schedule = JSON.parse(data.d);
          that.selectedMonth = that.Endorsement_Nursing_schedule;
          that.tableData();
          that.displayTable = true;
        },
      });
    },
    tableData() {
      // 1- get nurses names in Endorsement_Nursing_schedule without duplicate
      this.Nurses = this.Endorsement_Nursing_schedule.filter(
        (thing, index, self) =>
          index === self.findIndex((t) => t.Nurse_id === thing.Nurse_id)
      );

      // 2- push all shift data for 30 days
      let that = this;

      this.Nurses.map((x, n) => {
        x.shifts = [];
        x.Total = 0;
        that.monthCalendar.map((z) => {
          let obj = {};
          obj.name = z.day;

          if (
            that.Endorsement_Nursing_schedule.filter(
              (i) => i.Nurse_name == x.Nurse_name && z.fullDate.trim() == i.Date.trim()
            ).length > 0
          ) {
            obj.shift = that.Endorsement_Nursing_schedule.filter(
              (i) => i.Nurse_name == x.Nurse_name && z.fullDate.trim() == i.Date.trim()
            )[0].Shift.substr(0, 1);
            obj.work = true;
            x.Total++;
          } else {
            obj.shift = "";
            obj.work = false;
          }

          x.shifts.push(obj);
        });

        if (n == this.Nurses.length - 1) {
          this.displayed = this.Nurses;
        }
      });
    },
    filterTable() {
      if (this.filtered == "All") {
        this.displayed = this.Nurses;
      } else if (this.filtered == "Nurses") {
        this.displayed = this.Nurses.filter((x) => x.Nurse_role == 12);
      } else {
        this.displayed = this.Nurses.filter((x) => x.Nurse_role == 17);
      }
    },
  },

  created() {
    let that = this;
    that.month = new Date().toISOString().substr(0, 10);

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
  },
};
</script>

<style scoped>
.display_schdule {
  min-height: 100vh;
  background-color: #f6f8fb;
  background: #f6f8fb url("../../assets/layout/img/backgrounds/bg9.svg") top no-repeat;
  background-size: contain;
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
th {
  font-size: 16px;
}
.calendar-span {
  display: block;
  width: 20px;
  height: 20px;
  line-height: 20px;
  text-align: center;
  color: #fff;
  border-radius: 5px;
  font-size: 12px;
}
.v-data-table > .v-data-table__wrapper > table > thead > tr > th {
  font-size: 14px;
}
table th,
table td {
  display: table-cell;
  vertical-align: middle;
}
</style>
