<template>
  <div class="display_schdule">
    <div class="internal-page">
      <form>
        <div class="container-fluid pt-5">
          <div class="card shadow">
            <div class="row p-4 align-items-center">
              <!-- calender for select month -->
              <div class="col-lg-4 col-md-6">
                <v-dialog ref="dialog" v-model="modal" width="290px">
                  <template v-slot:activator="{ on, attrs }">
                    <v-text-field
                      v-model="month"
                      label="Pick a month"
                      readonly
                      v-bind="attrs"
                      v-on="on"
                    ></v-text-field>
                  </template>
                  <v-date-picker
                    v-model="month"
                    type="month"
                    scrollable
                    color="green lighten-1"
                  >
                  </v-date-picker>
                </v-dialog>
              </div>
              <!-- end calender -->
              <!-- start select unit -->
              <div class="col-lg-4 col-md-6" v-if='user.Role_id != 17'>
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
              </div>
              <!-- end select unit -->
              <div class="col-lg-4 col-md-6">
                <v-select
                  :items="items"
                  label="Filter"
                  v-model="filtered"
                  @change="filterTable()"
                ></v-select>
              </div>
            </div>

            <div class="text-center mt-0">
              <button class="special-btn shadow mt-0" @click.prevent="monthDays">
                Display Nursing Schedule
              </button>
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
              data-toggle="modal"
              data-target="#exampleModalCenter"
              v-if="user.Role_id == 19 || user.Role_id == 18 || user.Role_id == 17"
            >
              <v-icon dark> mdi-pencil </v-icon>
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
                  <tr
                    v-for="(nurse, i) in displayed"
                    :key="nurse.Nurse_id"
                  >
                    <td v-if="nurse.Role_id == 17" style="background-color: #fff7c1">
                      {{ i + 1 }}
                    </td>
                    <td v-else>{{ i + 1 }}</td>
                    <td
                      style="min-width: 120px; background-color: #fff7c1"
                      v-if="nurse.Role_id == 17"
                    >
                      {{ nurse.Nurse_name }}
                    </td>
                    <td style="min-width: 120px" v-else>
                      {{ nurse.Nurse_name }}
                    </td>
                    <td v-if="nurse.Role_id == 17" style="background-color: #fff7c1">
                      {{ nurse.Nurse_id }}
                    </td>
                    <td v-else>{{ nurse.Nurse_id }}</td>
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
                        >{{ shift.shift.substr(0, 1) }}</span
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
        class="modal-dialog modal-lg modal-dialog-centered"
        role="document"
        style="max-width: 1300px; margin-top: 120px"
      >
        <div class="modal-content">
          <div class="modal-header">
            <h5 class="modal-title" id="exampleModalLongTitle">
              <i class="fa fa-edit text-success"></i>
              Edit Schedule
            </h5>
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
                          :key="'i' + day.day"
                          class="text-center pl-1 pr-1"
                          :class="day.day.substr(-3, 3) == 'Fri' ? 'bg-grey' : 'bg-white'"
                        >
                          <span class="d-block">{{ i + 1 }}</span>
                          <span class="d-block">{{ day.day.substr(-3, 3) }}</span>
                        </th>
                        <th class="bg-primary text-white">Total</th>
                      </tr>
                    </thead>
                    <tbody v-show="displayed.length > 0">

                      <!-- edit by manager -->
                      <tr v-if='user.Role_id == 19 || user.Role_id == 18' v-for="(nurse, i) in displayed" :key="'i' + nurse.Nurse_id">
                        <td>{{ i + 1 }}</td>
                        <td style="min-width: 120px">{{ nurse.Nurse_name }}</td>
                        <td>{{ nurse.Nurse_id }}</td>
                        <td v-for="shift in nurse.shifts" :key="'i' + shift.fullDate">
                          <v-select
                            :class="shift.shift == '' ? 'text-secondary' : 'text-success'"
                            :items="[
                              { name: 'Day', val: 'Day' },
                              { name: 'Night', val: 'Night' },
                              { name: 'None', val: '' },
                            ]"
                            item-text="name"
                            :item-value="'val'"
                            v-model="shift.shift"
                          ></v-select>
                        </td>
                        <td class="bg-primary text-white text-center">
                          {{ nurse.Total }}
                        </td>
                      </tr>

                      <!-- edit by a charge nurse -->
                      <tr v-if='user.Role_id == 17' v-for="(nurse, i) in Nurses.filter((x) => x.Nurse_role == 12)" :key="'i' + nurse.Nurse_id">
                        <td>{{ i + 1 }}</td>
                        <td style="min-width: 120px">{{ nurse.Nurse_name }}</td>
                        <td>{{ nurse.Nurse_id }}</td>
                        <td v-for="shift in nurse.shifts" :key="'i' + shift.fullDate">
                          <v-select
                            :class="shift.shift == '' ? 'text-secondary' : 'text-success'"
                            :items="[
                              { name: 'Day', val: 'Day' },
                              { name: 'Night', val: 'Night' },
                              { name: 'None', val: '' },
                            ]"
                            item-text="name"
                            :item-value="'val'"
                            v-model="shift.shift"
                          ></v-select>
                        </td>
                        <td class="bg-primary text-white text-center">
                          {{ nurse.Total }}
                        </td>
                      </tr>
                    </tbody>
                  </template>
                </v-simple-table>
                <div class="modal-footer">
                  <button
                    type="button"
                    class="btn btn-secondary shadow"
                    data-dismiss="modal"
                  >
                    Close
                  </button>
                  <button type="button" class="btn btn-primary shadow" @click="edit()">
                    Edit
                  </button>
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
      adminusers: [],
      Nurses: [],
      displayed: [],
      Endorsement_Nursing_schedule: [],

      displayTable: false,
      date: "",

      month: "",
      unit: 0,
      shift: "",

      monthCalendar: [],
      selectedMonth: [],
      dialog: false,
      modal:false
    };
  },
  methods: {
    monthDays: function () {
      swal({
        title: "Preparing table!",
        text: "Please wait!",
        buttons: false,
        closeOnClickOutside: false,
      });

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
          that.displayTable = true;
          //get getadminusersData
          $.ajax({
            type: "POST",
            url: that.apiUrl + "endoresment/display_schdule.aspx/getadminusersData",
            data: JSON.stringify({ data: { Area_id: that.unit } }),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
              that.adminusers = JSON.parse(data.d);
              that.tableData();
            },
          });
        },
      });
    },
    tableData() {
      // 1- get nurses names
      this.Nurses = this.adminusers;

      // 2- push all shift data for 30 days for each nurse
      let that = this;

      that.Nurses.map((x, n) => {
        x.shifts = [];
        x.Total = 0;
        x.Nurse_name = x.FullName;
        x.Nurse_role = x.Role_id;
        x.Nurse_id = x.Emp_id;
        x.Unit_id = that.unit;
        x.Unit_name = that.Units.filter((u) => u.U_id == that.unit)[0].U_name;

        that.monthCalendar.map((z) => {
          let obj = {};
          obj.name = z.day;

          if (
            that.Endorsement_Nursing_schedule.filter(
              (i) => i.Nurse_name == x.FullName && z.fullDate.trim() == i.Date.trim()
            ).length > 0
          ) {
            obj.shift = that.Endorsement_Nursing_schedule.filter(
              (i) => i.Nurse_name == x.FullName && z.fullDate.trim() == i.Date.trim()
            )[0].Shift.trim();
            obj.work = true;
            x.Total++;
          } else {
            obj.shift = "";
            obj.work = false;
          }
          obj.fullDate = z.fullDate;
          x.shifts.push(obj);
        });

        if (n == that.Nurses.length - 1) {
          that.displayed = that.Nurses.sort((a, b) => a.Role_id < b.Role_id);
          that.filterTable()
        }
      });

      swal.close();
    },
    filterTable() {
      if (this.filtered == "All") {
        this.displayed = this.Nurses.sort((a, b) => a.Role_id < b.Role_id);
      } else if (this.filtered == "Nurses") {
        this.displayed = this.Nurses.filter((x) => x.Nurse_role == 12);
      } else {
        this.displayed = this.Nurses.filter((x) => x.Nurse_role == 17);
      }
    },
    edit() {
      swal({
        title: "Updating!",
        text: "Please wait!",
        buttons: false,
        closeOnClickOutside: false,
      });

      let that = this;
      let f = 0,
        num = that.displayed.length * that.displayed[0].shifts.length;

      for (let i = 0; i < that.displayed.length; i++) {
        for (let z = 0; z < that.displayed[i].shifts.length; z++) {
          f++;
          console.log(f);
          let currentShift = that.displayed[i].shifts[z];
          let obj = {
            Date: currentShift.fullDate,
            Shift: currentShift.shift,
            Unit_id: that.displayed[i].Unit_id,
            Unit_name: that.displayed[i].Unit_name,
            Nurse_role: that.displayed[i].Nurse_role,
            Nurse_name: that.displayed[i].Nurse_name,
            Nurse_id: that.displayed[i].Nurse_id,
          };
          if (currentShift.shift.length > 0 && currentShift.work) {
            // update shift
            $.ajax({
              type: "POST",
              url: that.apiUrl + "endoresment/Nursing_schedule.aspx/updateSchdule",
              data: JSON.stringify({ data: obj }),
              contentType: "application/json; charset=utf-8",
              dataType: "json",
              success: function (data) {
                if (f == num) {
                  swal({
                    title: "Done!",
                    text: `You successfully updated scheduled shifts!`,
                    icon: "success",
                  }).then((x) => {
                    $(".modal").modal("hide");
                    location.reload();
                  });
                }
              },
            });
          } else if (currentShift.shift.length > 0 && currentShift.work == false) {
            // insert shift
            $.ajax({
              type: "POST",
              url: that.apiUrl + "endoresment/Nursing_schedule.aspx/newSchdule",
              data: JSON.stringify({ data: obj }),
              contentType: "application/json; charset=utf-8",
              dataType: "json",
              success: function (data) {
                if (f == num) {
                  swal({
                    title: "Done!",
                    text: `You successfully updated scheduled shifts!`,
                    icon: "success",
                  }).then((x) => {
                    $(".modal").modal("hide");
                    location.reload();
                  });
                }
              },
            });
          } else {
            if (f == num) {
              swal({
                title: "Done!",
                text: `You successfully updated scheduled shifts!`,
                icon: "success",
              }).then((x) => {
                $(".modal").modal("hide");
                location.reload();
              });
            }
          }
        }
      }
    },
  },

  created() {
    let that = this;
    that.month = new Date().toISOString().substr(0, 10);

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

    //get Units
    $.ajax({
      type: "POST",
      url: that.apiUrl + "endoresment/display_schdule.aspx/getUnitsData",
      data: JSON.stringify({ branch: { id: that.user.Branch_ID } }),
      contentType: "application/json; charset=utf-8",
      dataType: "json",
      success: function (data) {
        that.Units = JSON.parse(data.d);
        that.unit = that.user.Role_id != 17 ? that.Units[0].U_id : that.user.Area_id;

        //get getEndorsement_Nursing_scheduleData
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
            that.displayTable = true;
            //get getadminusersData
            $.ajax({
              type: "POST",
              url: that.apiUrl + "endoresment/display_schdule.aspx/getadminusersData",
              data: JSON.stringify({ data: { Area_id: that.unit } }),
              contentType: "application/json; charset=utf-8",
              dataType: "json",
              success: function (data) {
                that.adminusers = JSON.parse(data.d);
                that.tableData();
              },
            });
          },
        });

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
