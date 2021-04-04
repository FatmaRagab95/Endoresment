<template>
  <div class="editShiftDetials">
      <h1 class="text-secondary card p-4">
        Edit Shift
        <br/>
        <span style='font-size:16px;'>Update date in {{ UnitDash[0].Entry_date }}</span>
      </h1>
    <div class="container mt-5 mb-5">
      <form @submit.prevent="editShift(UnitDash[0])">
        <div class="detials card shadow" v-if="UnitDash.length > 0">
          <div class="row">
            <div class="col-lg-6">
                <div class="text-left">
                  <h5 class="text-dark">Shift date</h5>
                    <v-dialog
                      ref="dialog"
                      v-model="modal"
                      width="290px"
                    >
                    <template v-slot:activator="{ on, attrs }">
                      <v-text-field
                        v-model="UnitDash[0].Shift_date"
                        class='card pl-3 pr-3'
                        readonly
                        v-bind="attrs"
                        v-on="on"
                      ></v-text-field>
                    </template>
                    <v-date-picker
                      v-model="UnitDash[0].Shift_date"
                      @input="modal = false"
                      color='green'
                    ></v-date-picker>
                  </v-dialog>
                </div>
            </div>
            <div class="col-lg-6">
                <div class="text-left">
                    <h5 class="text-dark">
                      Total Census
                    </h5> 
                    <v-text-field
                      v-model="UnitDash[0].Total_Census"
                      class='card pl-3 pr-3'
                      type='number'
                      required
                    ></v-text-field>
                </div>
            </div>
            <div class="col-lg-6">
                <div class="text-left">
                  <h5 class="text-dark">Received</h5>
                    <v-text-field
                      v-model="UnitDash[0].Received"
                      class='card pl-3 pr-3'
                      type='number'
                      required
                    ></v-text-field>
                </div>
            </div>
            <div class="col-lg-6">
                <div class="text-left">
                  <h5 class="text-dark">Shift</h5>
                  <v-select
                  :items="['Day', 'Night']"
                  class='card pl-3 pr-3'
                  v-model="UnitDash[0].Shift" required></v-select>
                </div>
            </div>

            <div class="col-lg-6">
                <div class="text-left row">
                  <div class="col-md-6">
                    <h5 class="text-dark">
                      Transfer In
                    </h5>
                    <v-text-field
                      v-model="UnitDash[0].Transfer_In"
                      class='card pl-3 pr-3'
                      type='number'
                      required
                    ></v-text-field>
                  </div>
                  <div class="col-md-6">
                    <h5 class="text-dark">
                      Transfer Out
                    </h5>
                    <v-text-field
                      v-model="UnitDash[0].Transfer_Out"
                      class='card pl-3 pr-3'
                      type='number'
                      required
                    ></v-text-field>
                  </div>
                </div>
            </div>
            <div class="col-lg-6">
                <div class="text-left">
                  <h5 class="text-dark">Admission</h5>
                    <v-text-field
                      v-model="UnitDash[0].Admission"
                      class='card pl-3 pr-3'
                      type='number'
                      required
                    ></v-text-field>
                </div>
            </div>
            <div class="col-lg-6">
                <div class="text-left">
                  <h5 class="text-dark">
                    Endorsing Charge Nurse
                  </h5>
                  <v-select
                  :items="Nurses"
                  class='card pl-3 pr-3'
                  item-text="FullName"
                  :item-value="'Emp_ID'"
                  placeholder="Select a nurse"
                  name="nurse"
                  v-model="UnitDash[0].Endorsing_ChargeNurse_id" required></v-select>
                </div>
            </div>
            <div class="col-lg-6">
                <div class="text-left">
                  <h5 class="text-dark">
                    Receive Charge Nurse
                  </h5>
                  <v-select
                  :items="Nurses"
                  class='card pl-3 pr-3'
                  item-text="FullName"
                  :item-value="'Emp_ID'"
                  placeholder="Select a nurse"
                  name="nurse"
                  v-model="UnitDash[0].Receive_ChargeNurse_id" required></v-select>
                </div>
            </div>
          </div>
        </div>
        <button class="special-btn">
          Edit
        </button>
      </form>
    </div>
  </div>
</template>

<script>
export default {
  name: "editShiftDetials",
  props: ["link"],
  data() {
    return {
      UnitDash: [],
      Nurses: [],
      shiftId: this.$route.params.id,
      apiUrl: this.link,
      modal: false,
    };
  },
  methods: {
    editShift: function (shift) {
      if ($("form").is(":valid")) {
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
            Endorsing_ChargeNurse_id: shift.Endorsing_ChargeNurse_id,
            Receive_ChargeNurse_id: shift.Receive_ChargeNurse_id,
          };

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
                location.reload();
              },
            });
          }
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

    //get Units dahsboard
    $.ajax({
      type: "POST",
      url: that.apiUrl + "endoresment/editShiftDetials.aspx/getUnitsDashData",
      data: JSON.stringify({ id: { id: that.shiftId } }),
      contentType: "application/json; charset=utf-8",
      dataType: "json",
      success: function (data) {
        that.UnitDash = JSON.parse(data.d);
        that.UnitDash.map((x) => {
          x.Shift = x.Shift.trim();
          x.Shift_date = moment(x.Shift_date.substr(3, 3) +
                    x.Shift_date.substr(0, 3) +
                    x.Shift_date.substr(6, 4)).format('YYYY-MM-DD');
        });

        //get nurses
        $.ajax({
          type: "POST",
          url: that.apiUrl + "endoresment/editShiftDetials.aspx/getNursesData",
          data: JSON.stringify({ unit: { Unit_id: that.UnitDash[0].Unit_id } }),
          contentType: "application/json; charset=utf-8",
          dataType: "json",
          success: function (data) {
            that.Nurses = JSON.parse(data.d);
          },
        });
      },
    });
  },
};
</script>

<style scoped>
.editShiftDetials {
  background:#f5f5fc  url('../../assets/layout/img/backgrounds/bg9.svg') bottom no-repeat;
  background-size: contain;
  text-align: center;
  position: relative;
  overflow: hidden;
  min-height: 100vh;
}
.editShiftDetials .detials {
  margin: 60px auto 10px auto;
  padding:60px 30px;
}
</style>
