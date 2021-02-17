<template>
  <div class="editShiftDetials">
    <div class="container card mt-3 shadow">
      <h1 class="mt-5 text-secondary" style="text-decoration: underline">Edit Shift</h1>
      <p class="text-success">Update date in {{ UnitDash[0].Entry_date }}</p>

      <div class="detials" v-if="UnitDash.length > 0">
        <div class="row">
          <div class="col-lg-6">
            <ul class="list-unstyled card bg-white shadow">
              <li class="text-left">
                <h5 class="text-info" style="text-decoration: underline">Shift date</h5>
                <div class="shadow-sm bg-light card">
                  <input
                    class="text-secondary pl-3 rounded"
                    type="date"
                    v-model="UnitDash[0].Shift_date"
                  />
                </div>
              </li>
              <li class="text-left">
                <h5 class="text-info" style="text-decoration: underline">Unit Name</h5>
                <div class="shadow-sm bg-light card">
                  <input
                    class="text-secondary pl-3 rounded"
                    type="text"
                    v-model="UnitDash[0].Unit_name"
                  />
                </div>
              </li>
              <li class="text-left row">
                <div class="col-md-6">
                  <h5 class="text-info" style="text-decoration: underline">
                    Total Census
                  </h5>
                  <div class="shadow-sm bg-light card">
                    <input
                      class="text-secondary pl-3 rounded"
                      type="number"
                      v-model="UnitDash[0].Total_Census"
                    />
                  </div>
                </div>
                <div class="col-md-6">
                  <h5 class="text-info" style="text-decoration: underline">Received</h5>
                  <div class="shadow-sm bg-light card">
                    <input
                      class="text-secondary pl-3 rounded"
                      type="number"
                      v-model="UnitDash[0].Received"
                    />
                  </div>
                </div>
              </li>

              <li class="text-left">
                <h5 class="text-info" style="text-decoration: underline">Shift</h5>
                <div class="shadow-sm bg-light card">
                  <input
                    class="text-secondary pl-3 rounded"
                    type="text"
                    v-model="UnitDash[0].Shift"
                  />
                </div>
              </li>
            </ul>
          </div>

          <div class="col-lg-6">
            <ul class="list-unstyled card bg-white shadow">
              <li class="text-left row">
                <div class="col-md-6">
                  <h5 class="text-info" style="text-decoration: underline">
                    Transfer In
                  </h5>
                  <div class="shadow-sm bg-light card">
                    <input
                      class="text-secondary pl-3 rounded"
                      type="number"
                      v-model="UnitDash[0].Transfer_In"
                    />
                  </div>
                </div>
                <div class="col-md-6">
                  <h5 class="text-info" style="text-decoration: underline">
                    Transfer Out
                  </h5>
                  <div class="shadow-sm bg-light card">
                    <input
                      class="text-secondary pl-3 rounded"
                      type="number"
                      v-model="UnitDash[0].Transfer_Out"
                    />
                  </div>
                </div>
              </li>
              <li class="text-left">
                <h5 class="text-info" style="text-decoration: underline">Admission</h5>
                <div class="shadow-sm bg-light card">
                  <input
                    class="text-secondary pl-3 rounded"
                    type="number"
                    v-model="UnitDash[0].Admission"
                  />
                </div>
              </li>
              <li class="text-left">
                <h5 class="text-info" style="text-decoration: underline">
                  Endorsing Charge Nurse
                </h5>
                <div class="shadow-sm bg-light card">
                  <input
                    class="text-secondary pl-3 rounded"
                    type="text"
                    v-model="UnitDash[0].Endorsing_ChargeNurse"
                  />
                </div>
              </li>
              <li class="text-left">
                <h5 class="text-info" style="text-decoration: underline">
                  Receive Charge Nurse
                </h5>
                <div class="shadow-sm bg-light card">
                  <input
                    class="text-secondary pl-3 rounded"
                    type="text"
                    v-model="UnitDash[0].Receive_ChargeNurse"
                  />
                </div>
              </li>
            </ul>
          </div>
        </div>
      </div>
      <button
        class="btn btn-success ml-auto mr-auto mt-0 mb-5"
        style="width: 100px"
        v-on:click.prevent="editShift(UnitDash[0])"
      >
        Edit
      </button>
    </div>
  </div>
</template>

<script>
export default {
  name: "editShiftDetials",
  data() {
    return {
      UnitDash: [],
      shiftId: this.$route.params.id,
      apiUrl: "http://localhost:49638/endoresment/dist/",
    };
  },
  methods: {
    editShift: function (shift) {
      let that = this,
        updatedShift = {
          id: shift.id,
          Unit_name: shift.Unit_name,
          Shift: shift.Shift,
          Total_Census: shift.Total_Census,
          Received: shift.Received,
          Shift_date: shift.Shift_date,
          Transfer_In: shift.Transfer_In,
          Transfer_Out: shift.Transfer_Out,
          Admission: shift.Admission,
          Endorsing_ChargeNurse: shift.Endorsing_ChargeNurse,
          Receive_ChargeNurse: shift.Receive_ChargeNurse,
        };

      if (updatedShift.Unit_name === "") {
        swal({
          title: "Error!",
          text: "Sorry, you should type the Unit name ! ",
          icon: "warning",
          dangerMode: true,
        });
      } else {
        swal({
          title: "Are you sure?",
          buttons: true,
        }).then((confirm) => {
          if (confirm) {
            $.ajax({
              type: "POST",
              url: that.apiUrl + "endoresment/editShiftDetials.aspx/updatedShift",
              data: JSON.stringify({ detail: updatedShift }),
              contentType: "application/json; charset=utf-8",
              dataType: "json",
              success: function () {
                swal({
                  title: "Sweet!",
                  text: "You successfully updated the shift ...",
                  icon: "success",
                  dangerMode: true,
                });
              },
            });
          }
        });
      }
    },
  },
  created() {
    let that = this;

    //get Units dahsboard
    $.ajax({
      type: "POST",
      url: that.apiUrl + "endoresment/editShiftDetials.aspx/getUnitsDashData",
      data: JSON.stringify({ id: { id: that.shiftId } }),
      contentType: "application/json; charset=utf-8",
      dataType: "json",
      success: function (data) {
        that.UnitDash = JSON.parse(data.d);
      },
    });
  },
};
</script>

<style scoped>
.editShiftDetials {
  background-color: #f6f8fb;
  text-align: center;
  position: relative;
  overflow: hidden;
  min-height: 100vh;
}
.editShiftDetials::before {
  content: "";
  position: absolute;
  left: -60%;
  top: 350px;
  display: block;
  width: 160%;
  height: 140vw;
  background-color: #fefefe;
  border-radius: 100% 0 0 0;
  border: none;
}
.editShiftDetials .detials {
  margin: 50px auto 20px auto;
}
.editShiftDetials .detials ul li {
  padding: 25px 30px;
}
.editShiftDetials .detials li:nth-of-type(even) {
  background-color: #f7f7f7;
}
.editShiftDetials .detials li .badge {
  width: 50px;
  line-height: 1.5;
}
</style>
