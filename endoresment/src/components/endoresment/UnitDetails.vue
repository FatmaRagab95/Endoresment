<template>
<<<<<<< HEAD
    <div class="UnitDetails pt-5">
        <div class="container">
            <h2 class="main-heading text-center mb-5">{{UnitsDash[0].Unit_name}}</h2>
            <div class="dash">
                <div class="row">
                    <div class="col-lg-7">
                        <div class="card shadow bg-white mb-5">
                            <a class='btn btn-bg btn-primary text-white' target='_blank'>View Rooms</a>
                        </div>
                        <div class="card bg-white shadow">
                            <h4> <i class='fa fa-table'></i> Latest Shifts</h4>

                            <div class="table-card  overflow-auto">
                                <table class="table text-center">
                                    <thead>
                                        <tr>
                                        <th scope="col">#</th>
                                        <th scope="col">Date</th>
                                        <th scope="col">Shift</th>
                                        <th scope="col">Details</th>
                                        <th scope="col">Controls</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr v-for='(shift,i) in UnitsDash' :key='shift.id'>
                                            <th scope="row">{{i + 1}}</th>
                                            <td>{{shift.Shift_date.substr(0,10)}}</td>
                                            <td>{{shift.Shift}}</td>
                                            <td><router-link :to='{name:"Shift Details", params: {id:shift.id}}' class='btn btn-sm btn-primary'>Details</router-link></td>
                                            <td><router-link :to='{name:"Edit Shift Details", params: {id:shift.id}}'><i class='fa fa-edit bg-success text-white btn-sm btn'></i></router-link>
                                                <i class='fa fa-trash bg-danger text-white btn-sm btn'></i>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </div>
=======
  <div class="UnitDetails pt-5">
    <div class="container">
      <h2 class="main-heading text-center mb-5">{{ UnitsDash[0].Unit_name }}</h2>
      <div class="dash">
        <div class="row">
          <div class="col-lg-7">
            <div class="card shadow bg-white mb-5">
              <a class="btn btn-bg btn-primary text-white" target="_blank">View Rooms</a>
            </div>
            <div class="card bg-white shadow">
              <h4><i class="fa fa-table"></i> Latest Shifts</h4>
>>>>>>> 4576a0ad5b1c8a8504d7cc3c90a8c950d8804d96

              <div class="table-card overflow-auto">
                <table class="table text-center">
                  <thead>
                    <tr>
                      <th scope="col">#</th>
                      <th scope="col">Date</th>
                      <th scope="col">Shift</th>
                      <th scope="col">Details</th>
                      <th scope="col">Controls</th>
                    </tr>
                  </thead>
                  <tbody>
                    <tr v-for="(shift, i) in UnitsDash" :key="shift.id">
                      <th scope="row">{{ i + 1 }}</th>
                      <td>{{ shift.Shift_date.substr(0, 10) }}</td>
                      <td>{{ shift.Shift }}</td>
                      <td>
                        <router-link
                          :to="{ name: 'Shift Details', params: { id: shift.id } }"
                          class="btn btn-sm btn-primary"
                          >Details</router-link
                        >
                      </td>
                      <td>
                        <router-link
                          :to="'/editShiftDetials/' + shift.id"
                          class="fa fa-edit bg-success text-white btn-sm btn"
                        ></router-link>
                        <i class="fa fa-trash bg-danger text-white btn-sm btn"></i>
                      </td>
                    </tr>
                  </tbody>
                </table>
              </div>
            </div>
          </div>

          <div class="col-lg-5">
            <div class="card bg-white shadow">
              <h4><i class="fa fa-bar-chart"></i> Statistics data for today.</h4>
              <div class="my-chart"><canvas id="myData"></canvas></div>
            </div>

            <div class="card bg-white shadow mt-5 p-3">
              <ul class="list-unstyled">
                <li class="cu-flex">
                  <span>Received</span>
                  <span class="badge badge-info">{{ todayData.Received }}</span>
                </li>
                <li class="cu-flex">
                  <span>Admission</span>
                  <span class="badge badge-warning">{{ todayData.Admission }}</span>
                </li>
                <li class="cu-flex">
                  <span>Transfer In</span>
                  <span class="badge badge-success">{{ todayData.Transfer_In }}</span>
                </li>
                <li class="cu-flex">
                  <span>Transfer Out</span>
                  <span class="badge badge-danger">{{ todayData.Transfer_Out }}</span>
                </li>
                <li class="cu-flex">
                  <span>Total Census</span>
                  <span class="badge badge-primary">{{ todayData.Total_Census }}</span>
                </li>
              </ul>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  name: "UnitDetails",
  data() {
    return {
      UnitsDash: [],
      unitId: this.$route.params.id,
      todayData: {
        Received: 0,
        Admission: 0,
        Transfer_In: 0,
        Transfer_Out: 0,
        Total_Census: 0,
      },
      apiUrl: "http://localhost:52861/endoresment/dist/",
    };
  },
  created() {
    let that = this;

<<<<<<< HEAD
        //get Units dahsboard
        $.ajax({
            type: "POST",
            url: that.apiUrl + "endoresment/UnitDetails.aspx/getUnitsDashData",
            data:JSON.stringify({"id": {"U_id": that.unitId}}),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                that.UnitsDash = JSON.parse(data.d);
                that.UnitsDash.map(x => {
                    let checkDate = x.Shift_date;
                    if (checkDate == that.UnitsDash[0].Shift_date) {
                       that.todayData = x;
                    } 
                });
=======
    //get Units dahsboard
    $.ajax({
      type: "POST",
      url: that.apiUrl + "endoresment/UnitDetails.aspx/getUnitsDashData",
      data: JSON.stringify({ id: { U_id: that.unitId } }),
      contentType: "application/json; charset=utf-8",
      dataType: "json",
      success: function (data) {
        that.UnitsDash = JSON.parse(data.d);
        that.UnitsDash.map((x) => {
          let checkDate =
            x.Shift_date.substr(3, 3) +
            x.Shift_date.substr(0, 3) +
            x.Shift_date.substr(6, 4);
          if (checkDate == moment(new Date()).format("MM/DD/YYYY")) {
            that.todayData = x;
          }
        });
      },
      complete: function () {
        $(document).ready(function () {
          var ctx = document.getElementById("myData").getContext("2d");
          var myChart = new Chart(ctx, {
            type: "pie",
            data: {
              labels: ["Admission", "Transfer In", "Transfer Out"],
              datasets: [
                {
                  data: [
                    that.todayData.Admission,
                    that.todayData.Transfer_In,
                    that.todayData.Transfer_Out,
                  ],
                  backgroundColor: ["#E62531", "#552DEA", "#FCEC34"],
                },
              ],
>>>>>>> 4576a0ad5b1c8a8504d7cc3c90a8c950d8804d96
            },
            options: {
              scales: {
                yAxes: [
                  {
                    ticks: {
                      beginAtZero: true,
                    },
                  },
                ],
              },
            },
          });
        });
      },
    });
  },
};
</script>

<style scoped>
.UnitDetails {
  min-height: 100vh;
  background-color: #f6f8fb;
  padding-bottom: 60px;
}
.UnitDetails .main-heading {
  background: url("../../assets/layout/img/border2.png") bottom center no-repeat;
  background-size: contain;
  padding-bottom: 20px;
  max-width: 300px;
  margin: auto;
  font-size: 24px;
}
.UnitDetails .dash .card .table-card {
  height: 300px;
  font-size: 16px;
}
thead {
  box-shadow: 0 0 10px 10px #e1e1e1;
}
.UnitDetails .dash .card h4 {
  font-size: 22px;
  padding: 15px;
}
.UnitDetails .dash .card h4 i {
  margin-right: 5px;
}
.UnitDetails .dash .card li {
  padding: 10px;
}
.UnitDetails .dash .card li:nth-of-type(odd) {
  background-color: #f7f7f7;
  border-radius: 5px;
}
.UnitDetails .dash .card li .badge {
  width: 50px;
  line-height: 1.5;
}
</style>
