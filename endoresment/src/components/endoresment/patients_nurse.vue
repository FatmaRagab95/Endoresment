<template>
    <div class="viewPatients">

        <div v-if='(user.Role_id == 12) && scheduleShifts.length > 0' 
            class='endoresment-options mt-5 pt-3 text-center shadow rounded overflow-hidden'>
            <div class="row align-items-center border-bottom pt-3 pb-3" v-for='shift in scheduleShifts' :key='shift.Unit_id'>
                <div class="col-md-6">
                    <h3 class='text-dark font-weight-bold'> {{shift.Unit_name}} </h3>
                </div>
                <div class="col-md-6" v-if='Shifts'>
                    <div class="receive-shift" v-if='Shifts.filter(x => x.Unit_id == shift.Unit_id).length == 0'>

                        <button
                        type="button" class="btn btn-primary btn-block shadow" data-toggle="modal" data-target="#exampleModal2"
                       @click.prevent='currentShift = shift'>
                            Receive Shift
                        </button>
                    </div>

                    <button v-else-if='Shifts.filter(x => x.Unit_id == shift.Unit_id && x.Confirm == 1).length == 0'
                    type="button" class="btn btn-success btn-block shadow" data-toggle="modal" data-target="#exampleModal3"
                    @click.prevent='currentShift = shift; confirmShift = unConfirmedShifts.filter(x => x.Unit_id == shift.Unit_id)[0]'>
                        Confirm receiving shift
                    </button>

                    <p class='text-success' v-else-if='Shifts.filter(x => x.Unit_id == shift.Unit_id)[0].Completed'>
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
        
        <div class="container">

            <h1 class='pt-5 pb-3  border-bottom mainColor'>
                <i class='ml-3 mr-3 fa fa-hospital-o'></i>
                Your Patients list</h1>

            <div class='row' v-if='patients.length > 0 && Shifts.length'>
                <div class="col-md-4" v-for='(patient) in patients' :key='patient.id'>
                    <div class='details'>
                        <div class="inner shadow">
                            <h3 class='pb-2'>{{patient.Patient_FullName}} 
                                <span :class='patient.Gender.trim() == "Male" ? "text-primary" : "text-pink"'>
                                    <i :class='patient.Gender.trim() == "Male" ? "fa fa-male" : "fa fa-female"'></i>
                                </span>
                            </h3>
                            <span class='age badge badge-light border font-weight-normal'>
                                Age: <span class='text-danger'>{{patient.Age}} </span></span>
                            <span class='speciality badge font-weight-normal'>{{patient.Specialty}}</span>
                            <div class='row mt-3 p-2 border rounded place'>
                                <div class="col-6 border-right">
                                    <span>Unit:</span> {{patient.Unit}}
                                </div>
                                <div class="col-6">
                                    <span>Room:</span> {{patient.Room}}
                                </div>
                            </div>
                            <p class='medicalNum mt-3'>
                                <span>Medical Number:</span>
                                {{patient.Medical_Number}}
                            </p>

                            <div class='text-right'>
                                <router-link :to='{name:"Patient Data", params:{id:patient.id}}' class='btn'>View Details</router-link>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <v-alert v-else
            border="right"
            colored-border
            type="error"
            elevation="2"
            class='bg-white text-danger'
            style='max-width:500px;margin:60px auto'
            >
            You have no assigned patients yet!
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
                        <form
                        @submit.prevent='insertShift()'>
                            <ul class='shift-info list-unstyled'>
                                <li class='border-bottom p-3 cu-flex text-dark'>
                                    <v-select
                                    :items="currentShift.EndoresingNurse"
                                    item-text="FullName"
                                    :item-value="'Emp_ID'"
                                    label="Receiving From"
                                    placeholder="Select a nurse"
                                    name="nurse"
                                    v-model="newShift.Receive_from" required></v-select>
                                </li>
                                <li class='p-3 cu-flex'>
                                    <span class='keyWords'>Shift Date:</span>
                                    <span class='values text-secondary'>{{currentShift.Date}}</span>
                                    <span class='values text-secondary'>
                                        {{currentShift.Shift}}
                                        <span class='text-success'><i :class="currentShift.Shift.trim() == 'Day' ? 'fa fa-sun-o' : 'fa fa-moon-o'"></i></span>
                                    </span>
                                </li>
                            </ul>
                        </form>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-secondary shadow" data-dismiss="modal" @click='newShift.Total_Census = 0; newShift.Received = 0'>Close</button>
                        <button type="button" class="btn btn-primary shadow" @click='insertShift()'>Submit</button>
                    </div>
                    </div>
                </div>
            </div>

            <!-- handover shift -->
            <div class="modal fade" id="exampleModal" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
                <div class="modal-dialog modal-lg" role="document">
                    <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title" id="exampleModalLabel2">Handover Shift 
                            <span class='text-primary'>{{currentShift.Unit_name}}</span>
                        </h5>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                    <div class="modal-body">
                        <form
                        @submit.prevent='insertShift()'>
                            <ul class='shift-info list-unstyled'>
                                <li class='border-bottom p-3 cu-flex text-dark'>
                                    <v-select
                                    :items="currentShift.handNurse"
                                    item-text="FullName"
                                    :item-value="'Emp_ID'"
                                    label="Handover To"
                                    placeholder="Select a nurse"
                                    name="nurse"
                                    v-model="newShift.Nurse_id" required></v-select>
                                </li>
                                <li class='p-3 cu-flex'>
                                    <span class='keyWords'>Shift Date:</span>
                                    <span class='values text-secondary'>{{currentShift.handoverDate}}</span>
                                    <span class='values text-secondary'>
                                      {{currentShift.Shift.trim() == 'Day' ? 'Night' : 'Day'}}
                                      <span class='text-success'><i :class="currentShift.Shift.trim() == 'Day' ? 'fa fa-moon-o' : 'fa fa-sun-o'"></i></span>
                                    </span>
                                </li>
                            </ul>
                        </form>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-secondary shadow" data-dismiss="modal" @click='newShift.Total_Census = 0; newShift.Received = 0'>Close</button>
                        <button type="button" class="btn btn-primary shadow" @click='insertShift()'>Submit</button>
                    </div>
                    </div>
                </div>
            </div>

            <!-- confirm receiving shift -->
            <div v-if='confirmShift' class="modal fade" id="exampleModal3" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel3" aria-hidden="true">
                <div class="modal-dialog" role="document">
                    <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title" id="exampleModalLabel">Confirm receiving shift
                            <span class='text-primary'>{{currentShift.Unit_name}}</span>
                        </h5>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                    <div class="modal-body">
                        <ul class='shift-info list-unstyled'>
                            <li class='border-bottom p-3 cu-flex'>
                                <span class='keyWords'>Receiving From:</span>
                                <span class='values'>{{confirmShift.Receive_name}}</span>
                            </li>
                            <li class='p-3 cu-flex'>
                                <span class='keyWords'>Shift Date:</span>
                                <span class='values text-secondary'>{{confirmShift.Shift_date.substr(0,10)}}</span>
                                <span class='values text-secondary'>
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
            </div>
        </div>
        </div>
    </div>
</template>

<script>
export default {
    name:'patientsNurse',
    props: ['link', 'user'],
    data() {
        return {
            patients: [],
            Nurses:[],
            NursesUnits:[],
            Units: [],
            Shifts: null,
            unConfirmedShifts: [],
            scheduleShifts:[],
            today:'',
            newShift: {
                Nurse_id:0,
                Nurse_name: '',
                Receive_from:0,
                Receive_name:'',
                Unit_id:0,
                Shift:'',
                Shift_date:'',
                Confirm: 1,
                Entry_user: 0
            },
            currentShift:null,
            confirmShift: null,
            apiUrl: this.link,
        }
    },
    methods: {
        insertShift() {
            if ($(".modal.show form").is(":valid")) {
                let that = this,
                handover = false;
                that.newShift.Unit_id = that.currentShift.Unit_id;
                that.newShift.Entry_user = that.user.Emp_id;

                // if it's receive shift
                if (!that.newShift.Nurse_id) {
                    that.newShift.Nurse_id = that.user.Emp_id;
                    that.newShift.Nurse_name = that.user.FullName;
                    that.newShift.Receive_name = that.currentShift.EndoresingNurse.filter(x => x.Emp_ID == that.newShift.Receive_from)[0].FullName;
                    that.newShift.Shift = that.currentShift.Shift;
                    that.newShift.Shift_date = that.currentShift.Date;
                    that.newShift.Confirm = 1
                } 
                // if it's handover
                else {
                    handover = true;
                    that.newShift.Receive_from = that.user.Emp_id;
                    that.newShift.Receive_name = that.user.FullName;
                    that.newShift.Nurse_name = that.currentShift.handNurse.filter(x => x.Emp_ID == that.newShift.Nurse_id)[0].FullName;
                    that.newShift.Shift = that.currentShift.Shift.trim() == 'Day' ? 'Night' : 'Day';
                    that.newShift.Shift_date = that.currentShift.handoverDate;
                    that.newShift.Confirm = 0
                }
                let stored = Object.assign({}, that.newShift);

                // insert into database new shift
                $.ajax({
                    type: "POST",
                    url: that.apiUrl + "endoresment/patientsNurse.aspx/newNurseShift",
                    contentType: "application/json; charset=utf-8",
                    data: JSON.stringify({ shift: that.newShift }),
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
                        if (!handover) {
                            stored.id = JSON.parse(data.d);
                            that.Shifts.push(stored);
                        } else {
                            let handedShift = that.Shifts.filter(x => x.Unit_id == that.newShift.Unit_id)[0].id;
                            resend();

                            function resend () {
                                return $.ajax({
                                    type: "POST",
                                    url: that.apiUrl + "endoresment/patientsNurse.aspx/completeNurseShift",
                                    data: JSON.stringify({ shift: {id: handedShift} }),
                                    contentType: "application/json; charset=utf-8",
                                    dataType: "json",
                                    error : function () {
                                        resend();
                                    }
                                });
                            }
                            that.Shifts.filter(x => x.id == handedShift)[0].Completed = 1; 
                        }
                    },
                    complete: function () {
                        swal({
                            title: "Done!",
                            text: `You successfully ${handover ? 'handed' : 'received'} a shift in ${that.currentShift.Unit_name}`,
                            icon: "success",
                        }).then(x => {
                            $('.modal').modal('hide');
                            Object.keys(that.newShift).map(x => that.newShift[x] = null);
                        });
                    }
                });
            } else {
                swal({
                    icon:'warning',
                    dangerMode:true,
                    text:'Please Fill All Fields!'
                })
            }
        },
        confirm() {
            let that = this;
            if (that.confirmShift.Shift.trim() == 'Day') {

                if (new Date().getHours() < 20 && new Date().getHours() >= 8) {
                    $.ajax({
                        type: "POST",
                        url: that.apiUrl + "endoresment/patientsNurse.aspx/confirmShift",
                        data: JSON.stringify({ shift: {id: that.confirmShift.id} }),
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (data) {
                            
                            that.unConfirmedShifts = that.unConfirmedShifts.filter(x => x.id != that.confirmShift.id);
                            that.confirmShift.Confirm = 1;
                            that.Shifts.push(that.confirmShift);

                            swal({
                                title: "Done!",
                                text: `You confirmed receiving a shift in ${that.currentShift.Unit_name}`,
                                icon: "success",
                            }).then(x => {
                                $('.modal').modal('hide');
                            });
                        }
                    });
                } else {
                    swal({
                        title: "Oops!",
                        text: `You can not receive this shift unless it's past 8:00 AM`,
                        icon: "warning",
                        dangerMode: true
                    })
                }
            } else {
                if (new Date().getHours() >= 20 || new Date().getHours() < 8) {
                    $.ajax({
                        type: "POST",
                        url: that.apiUrl + "endoresment/patientsNurse.aspx/confirmShift",
                        data: JSON.stringify({ shift: {id: that.confirmShift.id} }),
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (data) {
                            
                            that.unConfirmedShifts = that.unConfirmedShifts.filter(x => x.id != that.confirmShift.id);
                            that.confirmShift.Confirm = 1;
                            that.Shifts.push(that.confirmShift);

                            swal({
                                title: "Done!",
                                text: `You confirmed receiving a shift in ${that.currentShift.Unit_name}`,
                                icon: "success",
                            }).then(x => {
                                $('.modal').modal('hide');
                            });
                        }
                    });
                } else {
                    swal({
                        title: "Oops!",
                        text: `You can not receive this shift unless it's past 8:00 PM`,
                        icon: "warning",
                        dangerMode: true
                    })
                }
            }
        }
    },
    created() {
        let that = this;
        that.today = moment(new Date()).format('YYYY-MM-DD');
        //get Patients
        $.ajax({
            type: "POST",
            url: that.apiUrl + "endoresment/patientsNurse.aspx/getPatientsData",
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify({ id: { Emp_id: that.user.Emp_id } }),
            dataType: "json",
            success: function (data) {
                that.patients = JSON.parse(data.d);
            },
        });

        $.ajax({
            type: "POST",
            url: that.apiUrl + "endoresment/add_unit.aspx/getScheduleData",
            data:JSON.stringify({"data": {"Nurse_id":  that.user.Emp_id}}),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                that.scheduleShifts = JSON.parse(data.d);
                if (new Date().getHours() < 20 && new Date().getHours() >= 8) {
                    that.scheduleShifts = that.scheduleShifts.filter(x => x.Date.trim() == moment(new Date()).format('YYYY-MM-DD') && x.Shift.trim() == 'Day');
                } else {
                    that.scheduleShifts = that.scheduleShifts.filter(x => {
                    let checkDate = moment(moment(new Date()).format('YYYY-MM-DD') + ' 20:00');
                    let check = new Date().getHours() < 8;
                    return x.Shift.trim() == 'Night' && (check ? moment(x.Date.trim() + ' 19:59').add(12,'Hour') < checkDate : moment(x.Date.trim() + ' 20:01').add(12,'Hour') > checkDate)
                    });
                }


                // if there is scheduled shift get endorsing and receiving nurses at the same unit
                for (let i = 0; i < that.scheduleShifts.length; i++) {

                    let shift = that.scheduleShifts[i].Shift.trim() == 'Day' ? 'Night' : 'Day';
                    let scheduleDate = that.scheduleShifts[i].Shift == 'Night' ? that.scheduleShifts[i].Date : moment(that.scheduleShifts[i].Date.trim()).add(-1, "day").format('YYYY-MM-DD');
                    
                    that.scheduleShifts[i].EndoresingNurse = [];
                    that.scheduleShifts[i].handNurse = [];
                    getNurse(shift,scheduleDate, i);

                }

                for (let i = 0; i < that.scheduleShifts.length; i++) {

                    let shift = that.scheduleShifts[i].Shift.trim() == 'Day' ? 'Night' : 'Day';
                    let scheduleDate = that.scheduleShifts[i].Shift == 'Day' ? that.scheduleShifts[i].Date : moment(that.scheduleShifts[i].Date.trim()).add(1, "day").format('YYYY-MM-DD');
                    getNurse(shift,scheduleDate, i);

                }

            }
        });

        function getNurse (shift,scheduleDate, i) {
            $.ajax({
                type: "POST",
                url: that.apiUrl + "endoresment/add_Unit.aspx/getNursesData",
                data:JSON.stringify({"data":
                    {"Unit_id":  that.scheduleShifts[i].Unit_id,"Role_id":that.user.Role_id,"Shift": shift, "Date": scheduleDate}
                }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    let endCurrentShift = shift == 'Night' ? ' 7:59' : ' 19:59';
                    let endOtherShift = shift == 'Night' ? ' 19:59' : ' 7:59';

                    if (moment(that.scheduleShifts[i].Date.trim() + endCurrentShift).add(12,'Hour') < moment(scheduleDate + endOtherShift).add(12,'Hour')) {

                        if (JSON.parse(data.d).length > 0) {
                          that.scheduleShifts[i].handNurse.push(JSON.parse(data.d));
                          that.scheduleShifts[i].handNurse = that.scheduleShifts[i].handNurse.flat();
                        } else {
                          that.scheduleShifts[i].handNurse.push({FullName:'Unknown', Emp_ID: 0})
                        }

                    } else {

                        if (JSON.parse(data.d).length > 0) {
                          that.scheduleShifts[i].EndoresingNurse.push(JSON.parse(data.d));
                          that.scheduleShifts[i].EndoresingNurse = that.scheduleShifts[i].EndoresingNurse.flat();
                        } else {
                          that.scheduleShifts[i].EndoresingNurse = {FullName:'Unknown', Emp_ID: 0};
                        }

                    }

                    that.scheduleShifts[i].handoverDate = that.scheduleShifts[i].Shift == 'Day' ? that.scheduleShifts[i].Date : moment(that.scheduleShifts[i].Date.trim()).add(1, "day").format('YYYY-MM-DD');
                    
                }
            });
        }

        // getUnitsData
        $.ajax({
            type: "POST",
            url: that.apiUrl + "endoresment/patientsNurse.aspx/getUnitsData",
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify({ info: that.user}),
            dataType: "json",
            success: function (data) {
                that.Units = JSON.parse(data.d);
            },
        });
        
        // getShiftsData
        $.ajax({
            type: "POST",
            url: that.apiUrl + "endoresment/patientsNurse.aspx/getShiftsData",
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify({ info: that.user}),
            dataType: "json",
            success: function (data) {

                that.Shifts = JSON.parse(data.d);
                that.unConfirmedShifts = that.Shifts.filter(x => x.Confirm == 0);

                if (new Date().getHours() < 20 && new Date().getHours() >= 8) {
                    that.Shifts = that.Shifts.filter(x => {
                        
                        x.Shift_date = moment(x.Shift_date.substr(0,10)).format('DD/MM/YYYY');
                        return x.Shift_date == moment(new Date()).format('DD/MM/YYYY') && x.Shift.trim() == 'Day'});
                } else {
                    that.Shifts = that.Shifts.filter(x => x.Shift.trim() == 'Night' 
                    && moment(x.Shift_date.trim() + ' 20:00').add(12,'Hour') >= moment(new Date()));
                }

            },
        });
        
    }
}
</script>

<style scoped>
.viewPatients {
    min-height:100vh;
    background-color:#f6f8fb;
    background:#fff url('../../assets/layout/img/backgrounds/bg20.svg') top no-repeat;
    background-size:contain
}
.endoresment-options {
    background-color:rgba(255,255,255,.8);
}
.endoresment-options .row:last-of-type {
    border-bottom:none !important;
}
.endoresment-options .btn {
    padding:15px 0;
    max-width:300px;
    margin:15px auto;
    font-size:18px;
}
.viewPatients .details {
    max-width: 300px;
    margin: 20px auto;
    background-color: #baf2b1;
}
.viewPatients .details .inner {
    background-color:#fff;
    padding:20px;
    position:relative;
    padding: 29px;
    margin: auto;
    border-radius:5px;
}
.viewPatients .details .inner h3 {
    color: #046132;
    font-size:19px;
}
.viewPatients .details .inner h3 {
    color: #046132;
    font-size:19px;
}
.viewPatients .details .inner h3 span {
    font-size:14px;
    color:#aaa
}
.viewPatients .details .inner h3 span.text-pink {color:#ff4646}
.viewPatients .details .inner .speciality {
    position: absolute;
    top: 13px;
    right: 5px;
    font-size: 16px;
    color: #046132;
    background-color:#e2fbde
}
.viewPatients .details .inner .age {
    font-size:16px;
}
.viewPatients .details .inner .place {font-size:14px;}
.viewPatients .details .inner .place span {
    margin-right:10px;
}
.viewPatients .details .inner .btn {
    box-shadow: 2px 2px 2px #ccc, 5px 5px 15px inset #ccc;
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