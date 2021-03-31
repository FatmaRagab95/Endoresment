<template>
  <div class="addUnit">
    
    <div class="heading text-center mb-5 bg-white shadow-sm p-3 pt-5"> 
      <h2>
        <span class='text-primary'>Endoresment</span> / <span class='text-grey'>Handover</span>
      </h2>
      <p><span class='text-success'>Charge Nurse:</span> {{user.FullName}}</p>
    </div>

    <v-container>

      <div v-if='(user.Role_id == 17) && edits && scheduleShifts.length > 0' 
        class='endoresment-options mt-5 pt-3 text-center shadow rounded overflow-hidden'>
        <div class="row align-items-center border-bottom pt-3 pb-3" v-for='shift in scheduleShifts' :key='shift.Unit_id'>
            <div class="col-md-6">
                <h3 class='text-dark font-weight-bold'> {{shift.Unit_name}} </h3>
            </div>
            <div class="col-md-6">
                <div class="receive-shift" v-if='UnitDash.filter(x => x.Unit_id == shift.Unit_id).length == 0'>
                    <button
                    type="button" class="btn btn-primary btn-block shadow" data-toggle="modal" data-target="#exampleModal2"
                    @click.prevent='currentShift = shift'>
                        Receive Shift
                    </button>
                </div>                        
                <div class="receive-shift" v-else-if='UnitDash.filter(x => x.Unit_id == shift.Unit_id && x.Confirm).length == 0'>

                    <button
                    type="button" class="btn btn-warning btn-block shadow" data-toggle="modal" data-target="#exampleModal3"
                    @click.prevent='confirm(UnitDash.filter(x => x.Unit_id == shift.Unit_id)[0].id)'>
                        Confirm receiving shift
                    </button>
                </div>

                <p class='text-success' v-else-if='UnitDash.filter(x => x.Unit_id == shift.Unit_id && x.Completed).length > 0'>
                    <i class='fa fa-check-circle pr-1'></i> You handoverd current shift!
                </p>
                
                <button v-else
                    type="button" class="btn btn-danger btn-block shadow" data-toggle="modal" data-target="#exampleModal"
                @click.prevent='currentShift = shift'>
                    Handover Shift
                </button>
            </div>
        </div>
      </div>

      <v-alert v-else-if='(user.Role_id == 17) && !edits && scheduleShifts.length == 0'
      border="right"
      colored-border
      type="error"
      elevation="2"
      class='bg-white text-danger'
      style='max-width:500px;margin:60px auto'
      >
      You have no scheduled shifts yet!
      </v-alert>

        <!-- popup endorsing-nurse-->
      <div class="endorsing-nurse" v-if='currentShift '>

          <!-- receive shift -->
          <div class="modal fade" id="exampleModal2" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel2" aria-hidden="true">
              <div class="modal-dialog modal-lg" role="document">
                  <div class="modal-content">
                  <div class="modal-header">
                      <h5 class="modal-title" id="exampleModalLabel2">Receive Shift 
                          <span class='text-primary'>{{currentShift.Unit_name}}</span>
                      </h5>
                      <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                          <span aria-hidden="true">&times;</span>
                      </button>
                  </div>
                  <div class="modal-body">
                      <form @submit.prevent='submitUnit()'>
                          <ul class='shift-info list-unstyled'>
                              <li class='border-bottom p-3 cu-flex'>
                                  <span class='keyWords'>Receiving From:</span>
                                  <span class='values text-secondary'>{{currentShift.EndoresingNurse.FullName}}</span>
                              </li>
                              <li class='border-bottom p-3 cu-flex'>
                                  <span class='keyWords'>Shift Date:</span>
                                  <span class='values text-secondary'>{{currentShift.Date}}</span>
                              </li>
                              <li class='p-3 cu-flex'>
                                  <span class='keyWords'>Shift:</span>
                                  <span class='values text-secondary'>
                                      {{currentShift.Shift}}
                                      <span class='text-success'><i :class="currentShift.Shift.trim() == 'Day' ? 'fa fa-sun-o' : 'fa fa-moon-o'"></i></span>
                                  </span>
                              </li>
                          </ul>
                          <div class="custom-form">

                            <div class="cu-field">
                              <div class="row align-items-end">
                                <div class="col-md-6">
                                  <div class="cu-field">
                                    <h3 class="cu-label">
                                      <label>Received :</label>
                                    </h3>
                                    <div class="cu-input text-box">
                                      <span class="fa fa-edit"></span>
                                      <input type="number" v-model="newUnit.Received" required />
                                    </div>
                                  </div>
                                </div>
                                <div class="col-md-6">
                                  <div class="cu-field">
                                    <h3 class="cu-label">
                                      <label>Total Census :</label>
                                    </h3>
                                    <div class="cu-input text-box">
                                      <span class="fa fa-edit"></span>
                                      <input type="number" v-model="newUnit.Total_Census" required />
                                    </div>
                                  </div>
                                </div>
                              </div>
                            </div>
                          </div>
                      </form>
                  </div>
                  <div class="modal-footer">
                      <button type="button" class="btn btn-secondary shadow" data-dismiss="modal" @click='newUnit.Total_Census = 0; newUnit.Received = 0'>Close</button>
                      <button type="button" class="btn btn-primary shadow" @click='submitUnit()'>Submit</button>
                  </div>
                  </div>
              </div>
          </div>

          <!-- handover shift -->
          <div class="modal fade" id="exampleModal" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
              <div class="modal-dialog modal-lg" role="document">
                  <div class="modal-content">
                  <div class="modal-header">
                      <h5 class="modal-title" id="exampleModalLabel">Handover Shift
                          <span class='text-primary'>{{currentShift.Unit_name}}</span>
                      </h5>
                      <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                          <span aria-hidden="true">&times;</span>
                      </button>
                  </div>
                  <div class="modal-body">
                      <form @submit.prevent='submitUnit()'>
                          <ul class='shift-info list-unstyled'>
                              <li class='border-bottom p-3 cu-flex'>
                                  <span class='keyWords'>Handover To:</span>
                                  <span class='values text-secondary'>{{currentShift.handNurse.FullName}}</span>
                              </li>
                              <li class='border-bottom p-3 cu-flex'>
                                  <span class='keyWords'>Shift Date:</span>
                                  <span class='values text-secondary'>{{currentShift.handoverDate}}</span>
                              </li>
                              <li class='p-3 cu-flex'>
                                  <span class='keyWords'>Shift:</span>
                                  <span class='values text-secondary'>
                                      {{currentShift.Shift.trim() == 'Day' ? 'Night' : 'Day'}}
                                      <span class='text-success'><i :class="currentShift.Shift.trim() == 'Day' ? 'fa fa-moon-o' : 'fa fa-sun-o'"></i></span>
                                  </span>
                              </li>
                          </ul>
                          <div class="custom-form">

                            <div class="cu-field">
                              <div class="row align-items-end">
                                <div class="col-md-6">
                                  <div class="cu-field">
                                    <h3 class="cu-label">
                                      <label>Received :</label>
                                    </h3>
                                    <div class="cu-input text-box">
                                      <span class="fa fa-edit"></span>
                                      <input type="number" v-model="newUnit.Received" required />
                                    </div>
                                  </div>
                                </div>
                                <div class="col-md-6">
                                  <div class="cu-field">
                                    <h3 class="cu-label">
                                      <label>Total Census :</label>
                                    </h3>
                                    <div class="cu-input text-box">
                                      <span class="fa fa-edit"></span>
                                      <input type="number" v-model="newUnit.Total_Census" required />
                                    </div>
                                  </div>
                                </div>
                              </div>
                            </div>
                          </div>
                      </form>
                  </div>
                  <div class="modal-footer">
                      <button type="button" class="btn btn-secondary shadow" data-dismiss="modal">Close</button>
                      <button type="button" class="btn btn-primary shadow" @click='handover()'>Submit</button>
                  </div>
                  </div>
              </div>
          </div>

          <!-- confirm receiving shift
          <div v-if='confirmShift' class="modal fade" id="exampleModal3" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel3" aria-hidden="true">
              <div class="modal-dialog" role="document">
                  <div class="modal-content">
                  <div class="modal-header">
                      <h5 class="modal-title" id="exampleModalLabel">Confirm receiving shift
                          <span class='text-primary'>{{clickedUnit.Unit_name}}</span>
                      </h5>
                      <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                          <span aria-hidden="true">&times;</span>
                      </button>
                  </div>
                  <div class="modal-body">
                      <ul class='shift-info list-unstyled'>
                          <li class='border-bottom p-3 cu-flex'>
                              <span class='keyWords text-primary'>Receiving From:</span>
                              <span class='values'>{{confirmShift.Receive_name}}</span>
                          </li>
                          <li class='border-bottom p-3 cu-flex'>
                              <span class='keyWords text-primary'>Shift Date:</span>
                              <span class='values'>{{confirmShift.Shift_date.substr(0,10)}}</span>
                          </li>
                          <li class='p-3 cu-flex'>
                              <span class='keyWords text-primary'>Shift:</span>
                              <span class='values'>
                                  {{confirmShift.Shift}}
                                  <span class='text-success'><i :class="confirmShift.Shift.trim() == 'Day' ? 'fa fa-sun-o' : 'fa fa-moon-o'"></i></span>
                              </span>
                          </li>
                      </ul>
                  </div>
                  <div class="modal-footer">
                      <button type="button" class="btn btn-secondary shadow" data-dismiss="modal">Close</button>
                      <button type="button" class="btn btn-success shadow" @click='confirm()'>Confirm</button>
                  </div>
                  </div>
              </div>
          </div> -->
      </div>
    </v-container>


    <!-- <div class="internal-page" id="auth">
      <form @submit.prevent="submitUnit">
        <div class="custom-form pt-3">
          <div class="cu-container">
            <div class="cu-form-group special" style="max-width: 900px">
              <div class="title">
                <span>
                  <i class="fa fa-hospital-o mainColor mr-1"></i> Add shift details</span
                >
              </div>

              <div class="cu-field">
                <h3 class="cu-label">
                  <label>Unit:</label>
                </h3>
                <div class="f-select">
                  <select
                    class="form-control form-control-sm"
                    v-model="newUnit.Unit_id"
                    required
                  >
                    <option v-for="unit in Units" :value="unit.U_id" :key="unit.U_name">
                      {{ unit.U_name }}
                    </option>
                  </select>
                </div>
              </div>

              <div class="cu-field">
                <h3 class="cu-label">
                  <label>Endorsing Charge Nurse :</label>
                </h3>
                <div class="f-select">
                  <select
                    class="form-control form-control-sm"
                    v-model="newUnit.Endorsing_EndoresingNurse_id"
                    required
                  >
                    <option
                      v-for="Nurse in Nurses"
                      :value="Nurse.Emp_ID"
                      :key="Nurse.Emp_ID"
                    >
                      {{ Nurse.FullName }}
                    </option>
                  </select>
                </div>
              </div>

              <div class="cu-field">
                <h3 class="cu-label">
                  <label>Receive Charge Nurse :</label>
                </h3>
                <div class="f-select">
                  <select
                    class="form-control form-control-sm"
                    v-model="newUnit.Receive_EndoresingNurse_id"
                    required
                  >
                    <option
                      v-for="Nurse in Nurses"
                      :value="Nurse.Emp_ID"
                      :key="Nurse.Emp_ID"
                    >
                      {{ Nurse.FullName }}
                    </option>
                  </select>
                </div>
              </div>

              <div class="cu-field">
                <div class="row align-items-end">
                  <div class="col-md-6">
                    <div class="cu-field">
                      <h3 class="cu-label">
                        <label>Received :</label>
                      </h3>
                      <div class="cu-input text-box">
                        <span class="fa fa-edit"></span>
                        <input type="text" v-model="newUnit.Received" required />
                      </div>
                    </div>
                  </div>
                  <div class="col-md-6">
                    <div class="cu-field">
                      <h3 class="cu-label">
                        <label>Total Census :</label>
                      </h3>
                      <div class="cu-input text-box">
                        <span class="fa fa-edit"></span>
                        <input type="text" v-model="newUnit.Total_Census" required />
                      </div>
                    </div>
                  </div>
                </div>
              </div>

              <div class="cu-field radio-group">
                <div class="row align-items-end">
                  <div class="col-md-6">
                    <div class="cu-field">
                      <h3 class="cu-label">
                        <label>Current Shift: </label>
                      </h3>
                    </div>
                    <div class="cu-field">
                      <input
                        id="Day"
                        type="radio"
                        name="Shift"
                        value="Day"
                        @click="newUnit.Shift = 'Day'"
                        required
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
                        name="Shift"
                        value="Night"
                        @click="newUnit.Shift = 'Night'"
                        required
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

              <div class="cu-field">
                <h3 class="cu-label">
                  <label>Shift Date :</label>
                </h3>
                <div class="cu-input text-box">
                  <span class="fa fa-calendar-o"></span>
                  <input :max="date" type="date" v-model="newUnit.Shift_date" required />
                </div>
              </div>

              <div class="text-center">
                <button class="special-btn">Save</button>
              </div>
            </div>
          </div>
        </div>
      </form>
    </div> -->
  </div>
</template>

<script>
export default {
  name: "addUnit",
  props: ["link", 'user', 'UnitDash', 'edits'],
  data() {
    return {
      apiUrl: this.link,
      date: new Date(new Date().getTime() + 10000 * 60 * 6).toISOString().substr(0, 10),
      Nurses: [],
      Units: [],
      newUnit: {
        Unit_id: 0,
        Branch_id:0,
        Unit_name: "",
        Shift: "",
        Shift_date: "",
        Total_Census: 0,
        Received: 0,
        Admission: 0,
        Transfer_In: 0,
        Transfer_Out: 0,
        Endorsing_EndoresingNurse_id: "",
        Endorsing_EndoresingNurse: "",
        Receive_EndoresingNurse_id: "",
        Receive_EndoresingNurse: "",
      },
      scheduleShifts:[],
      currentShift:null
    };
  },
  methods: {
    submitUnit() {
      if ($(".modal.show form").is(":valid")) {
        let that = this;

        that.newUnit.Shift = that.currentShift.Shift;
        that.newUnit.Shift_date = that.currentShift.Date;
        that.newUnit.Branch_id = that.user.Branch_ID;
        that.newUnit.Endorsing_ChargeNurse_id = that.currentShift.EndoresingNurse.Emp_ID;
        that.newUnit.Endorsing_ChargeNurse = that.currentShift.EndoresingNurse.FullName;
        that.newUnit.Receive_ChargeNurse = that.user.FullName;
        that.newUnit.Receive_ChargeNurse_id = that.user.Emp_id;
        that.newUnit.Unit_id = that.currentShift.Unit_id;
        that.newUnit.Unit_name = that.currentShift.Unit_name;

        //insert Unit dashboard details
        $.ajax({
          type: "POST",
          url: that.apiUrl + "endoresment/add_unit.aspx/newUnit",
          data: JSON.stringify({ data: that.newUnit }),
          contentType: "application/json; charset=utf-8",
          dataType: "json",
          success: function (data) {
              swal({
                  title: "Done!",
                  text: `You successfully received a shift in ${that.currentShift.Unit_name}`,
                  icon: "success",
              }).then(x => {
                  $('.modal').modal('hide');
                  location.reload();
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
    handover() {
      if ($(".modal.show form").is(":valid")) {
        let that = this;

        that.newUnit.Shift = that.currentShift.Shift.trim() == 'Day' ? 'Night' : 'Day';
        that.newUnit.Shift_date = that.currentShift.handoverDate;
        that.newUnit.Branch_id = that.user.Branch_ID;
        that.newUnit.Endorsing_ChargeNurse_id = that.user.Emp_id;
        that.newUnit.Endorsing_ChargeNurse = that.user.FullName;
        that.newUnit.Receive_ChargeNurse = that.currentShift.handNurse.FullName;
        that.newUnit.Receive_ChargeNurse_id = that.currentShift.handNurse.Emp_ID;
        that.newUnit.Unit_id = that.currentShift.Unit_id;
        that.newUnit.Unit_name = that.currentShift.Unit_name;

        //insert Unit dashboard details
        $.ajax({
            type: "POST",
            url: that.apiUrl + "endoresment/add_unit.aspx/newUnit",
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify({  data: that.newUnit }),
            dataType: "json",
            beforeSend: function () {
                swal({
                    title: "Sending!",
                    text: "Please wait!",
                    buttons: false,
                    closeOnClickOutside: false
                });
            },
            success: function (data) {
                let handedShift = that.UnitDash.filter(x => x.Unit_id == that.newUnit.Unit_id)[0].id;
                resend();

                function resend () {
                    return $.ajax({
                        type: "POST",
                        url: that.apiUrl + "endoresment/add_unit.aspx/completeNurseShift",
                        data: JSON.stringify({ shift: {id: handedShift} }),
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        error : function () {
                            resend();
                        }
                    });
                }
                that.UnitDash.filter(x => x.id == handedShift)[0].Completed = 1;
            },
            complete: function () {
                swal({
                    title: "Done!",
                    text: `You successfully handed a shift in ${that.currentShift.Unit_name}`,
                    icon: "success",
                }).then(x => {
                    $('.modal').modal('hide');
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
    // confirm endorsing
    confirm(id) {
      let that = this;
        $.ajax({
            type: "POST",
            url: that.link + "endoresment/handover.aspx/confirmEndorsing",
            data:JSON.stringify({"id": {"id": id}}),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                swal({
                  title: "Confirmed!",
                  icon: "success",
                  dangerMode: true,
                });

                that.UnitDash.filter(x => x.id == id)[0].Confirm = true;
            }
        });
    }
  },
  created() {
    let that = this;

    //get units
    /*
    $.ajax({
      type: "POST",
      url: that.apiUrl + "endoresment/add_unit.aspx/getUnitsData",
      contentType: "application/json; charset=utf-8",
      data: JSON.stringify({ branch: { id:  that.user.Branch_ID} }),
      dataType: "json",
      success: function (data) {
        that.Units = JSON.parse(data.d);
      },
    });
    */

    //get nurses
    /*
    $.ajax({
      type: "POST",
      url: that.apiUrl + "endoresment/add_unit.aspx/getNursesData",
      contentType: "application/json; charset=utf-8",
      data: JSON.stringify({ branch: { id:  that.user.Branch_ID} }),
      dataType: "json",
      success: function (data) {
        that.Nurses = JSON.parse(data.d);
      },
    });
    */

    // check scheduled shifts
    $.ajax({
        type: "POST",
        url: that.apiUrl + "endoresment/handover.aspx/getScheduleData",
        data:JSON.stringify({"data": {"Nurse_id":  that.user.Emp_id}}),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            that.scheduleShifts = JSON.parse(data.d);

            if (new Date().getHours() < 20 && new Date().getHours() >= 8) {
                that.scheduleShifts = that.scheduleShifts.filter(x => x.Date.trim() == moment(new Date()).format('YYYY-MM-DD') && x.Shift.trim() == 'Day');
            } else {
                that.scheduleShifts = that.scheduleShifts.filter(x => x.Shift.trim() == 'Night')
            }

            // if there is scheduled shift get endorsing and receiving charge nurse at the same unit
            for (let i = 0; i < that.scheduleShifts.length; i++) {

                let shift = that.scheduleShifts[i].Shift.trim() == 'Day' ? 'Night' : 'Day';
                let scheduleDate = that.scheduleShifts[i].Shift == 'Night' ? that.scheduleShifts[i].Date : moment(that.scheduleShifts[i].Date.trim()).add(-1, "day").format('YYYY-MM-DD');
                getNurse(shift,scheduleDate, i);

            }

            for (let i = 0; i < that.scheduleShifts.length; i++) {

                let shift = that.scheduleShifts[i].Shift.trim() == 'Day' ? 'Night' : 'Day';
                let scheduleDate = that.scheduleShifts[i].Shift == 'Day' ? that.scheduleShifts[i].Date : moment(that.scheduleShifts[i].Date.trim()).add(1, "day").format('YYYY-MM-DD');
                getNurse(shift,scheduleDate, i);

            }

            function getNurse (shift,scheduleDate, i) {
              $.ajax({
                  type: "POST",
                  url: that.apiUrl + "endoresment/handover.aspx/getChargeNursesData",
                  data:JSON.stringify({"data": 
                      {"Unit_id":  that.scheduleShifts[i].Unit_id,"Shift": shift, "Date": scheduleDate}
                  }),
                  contentType: "application/json; charset=utf-8",
                  dataType: "json",
                  success: function (data) {
                      if (moment(that.scheduleShifts[i].Date) >= moment(scheduleDate)) {
                        if (JSON.parse(data.d).length > 0) {
                          that.scheduleShifts[i].EndoresingNurse = JSON.parse(data.d)[0];
                        } else {
                          that.scheduleShifts[i].EndoresingNurse = {FullName:'Unknown', Emp_ID: 0}
                        }
                      } else {
                        if (JSON.parse(data.d).length > 0) {
                          that.scheduleShifts[i].handNurse = JSON.parse(data.d)[0];
                        } else {
                          that.scheduleShifts[i].handNurse = {FullName:'Unknown', Emp_ID: 0}
                        }
                      }


                      that.scheduleShifts[i].handoverDate = that.scheduleShifts[i].Shift == 'Day' ? that.scheduleShifts[i].Date : moment(that.scheduleShifts[i].Date.trim()).add(1, "day").format('YYYY-MM-DD');
                      
                  }
              });
            }
        }
    });
  },
};
</script>

<style scoped>
.addUnit {
  background:#fff url('../../assets/layout/img/backgrounds/bg16.svg') center no-repeat;
  background-size:contain;
  min-height:100vh;
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
.endoresment-options {
    background-color:rgba(255,255,255,.9);
}
.endoresment-options .row:last-of-type {
    border-bottom:none !important;
}
.endoresment-options .btn {
    padding:15px 0;
    max-width:300px;
    margin:15px auto;
}
.endorsing-nurse .modal{
    padding-top:120px;
}
.endorsing-nurse .modal .custom-form {
    border:none;
}
.endorsing-nurse .modal .custom-form .cu-label {
    font-size:16px;
}
.internal-page .f-select::before {
    top: calc(50% + 0px);
}
.custom-form .cu-form-group .cu-field {
    padding:5px 0;
}
.custom-form .cu-form-group .cu-field .cu-input,
.custom-form .cu-form-group .cu-field .f-select{
    max-width:500px;
}
.custom-form .cu-field .cu-input.text-box {
    margin-inline-end: 0px;
}
</style>
