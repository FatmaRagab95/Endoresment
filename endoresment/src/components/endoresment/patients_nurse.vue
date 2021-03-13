<template>
    <div class="viewPatients">
        <div class="container">

            <div class='endoresment-options pt-5 pb-3 text-center'>
                <div class="row">
                    <div class="col-md-6">
                        <button type="button" class="btn btn-primary btn-block shadow" data-toggle="modal" data-target="#exampleModal">
                            Handover Shift
                        </button>
                    </div>
                    <div class="col-md-6">
                        <button type="button" class="btn btn-primary btn-block shadow" data-toggle="modal" data-target="#exampleModal2">
                            Receive Shift
                        </button>
                    </div>
                </div>
            </div>

            <h1 class='pt-5 pb-3  border-bottom mainColor'>
                <i class='ml-3 mr-3 fa fa-hospital-o'></i>
                Your Patients list</h1>

            <div class='row' v-if='patients.length > 0'>
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
            <div class="card text-center" v-else>
                <p class='mt-3 alert'><i class='fa fa-warning text-warning'></i> You have no assigned patients yet!</p>
            </div>


            <!-- popup endorsing-nurse-->
            <div class="endorsing-nurse">

                <!-- handover shift -->
                <div class="modal fade" id="exampleModal" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
                    <div class="modal-dialog" role="document">
                        <div class="modal-content">
                        <div class="modal-header">
                            <h5 class="modal-title" id="exampleModalLabel">Handover Shift</h5>
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                <span aria-hidden="true">&times;</span>
                            </button>
                        </div>
                        <div class="modal-body">
                            <form>
                                <div class="custom-form">
                                    <div class="cu-field">
                                        <h3 class="cu-label">
                                            <label>Handover To :</label>
                                        </h3>
                                        <div class="f-select">
                                            <select class="form-control form-control-sm" required>
                                                <option value=''>...</option>
                                                <option v-for='nurse in Nurses' :value='nurse.Emp_id' :key='nurse.Emp_id'>{{nurse.FullName}}</option>
                                            </select>
                                        </div>
                                    </div>
                                    <div class="cu-field">
                                        <h3 class="cu-label">
                                            <label>Shift Date :</label>
                                        </h3>
                                        <div class="cu-input text-box">
                                            <span class="fa fa-calendar-o"></span>
                                            <input :max='today' type="date"  required>
                                        </div>
                                    </div>
                                    <div class="cu-field radio-group">
                                        <div class="row align-items-end">
                                            <div class="col-md-6">
                                                <div class="cu-field">
                                                    
                                                <h3 class="cu-label">
                                                    <label>Current Shift:</label>
                                                </h3>
                                                    <input id="select1" type="radio" name="selectPatient" value="Day"
                                                    v-model='selectPatient' required>
                                                    <label for="select1">
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
                                                    <input id="select2" type="radio" name="selectPatient" value="Night"
                                                    v-model='selectPatient' required>
                                                    <label for="select2">
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
                                </div>
                            </form>
                        </div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-secondary shadow" data-dismiss="modal">Close</button>
                            <button type="button" class="btn btn-primary shadow">Submit</button>
                        </div>
                        </div>
                    </div>
                </div>

                <!-- receive shift -->
                <div class="modal fade" id="exampleModal2" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel2" aria-hidden="true">
                    <div class="modal-dialog" role="document">
                        <div class="modal-content">
                        <div class="modal-header">
                            <h5 class="modal-title" id="exampleModalLabel2">Receive Shift</h5>
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                <span aria-hidden="true">&times;</span>
                            </button>
                        </div>
                        <div class="modal-body">
                            <form>
                                <div class="custom-form">
                                    <div class="cu-field">
                                        <h3 class="cu-label">
                                            <label>Receive From :</label>
                                        </h3>
                                        <div class="f-select">
                                            <select class="form-control form-control-sm" required>
                                                <option value=''>...</option>
                                                <option v-for='nurse in Nurses' :value='nurse.Emp_id' :key='nurse.Emp_id + "f"'>{{nurse.FullName}}</option>
                                            </select>
                                        </div>
                                    </div>
                                    <div class="cu-field">
                                        <h3 class="cu-label">
                                            <label>Shift Date :</label>
                                        </h3>
                                        <div class="cu-input text-box">
                                            <span class="fa fa-calendar-o"></span>
                                            <input :max='today' type="date"  required>
                                        </div>
                                    </div>
                                    <div class="cu-field radio-group">
                                        <div class="row align-items-end">
                                            <div class="col-md-6">
                                                <div class="cu-field">
                                                    
                                                <h3 class="cu-label">
                                                    <label>Current Shift:</label>
                                                </h3>
                                                    <input id="select1" type="radio" name="selectPatient" value="Day"
                                                    v-model='selectPatient' required>
                                                    <label for="select1">
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
                                                    <input id="select2" type="radio" name="selectPatient" value="Night"
                                                    v-model='selectPatient' required>
                                                    <label for="select2">
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
                                </div>
                            </form>
                        </div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-secondary shadow" data-dismiss="modal">Close</button>
                            <button type="button" class="btn btn-primary shadow">Submit</button>
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
            today:'',
            apiUrl: this.link,
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

        //get nurses
        $.ajax({
        type: "POST",
        url: that.apiUrl + "endoresment/patientsNurse.aspx/getNursesData",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ info: that.user}),
        dataType: "json",
        success: function (data) {
            that.Nurses = JSON.parse(data.d);
        },
        });
    }
}
</script>

<style scoped>
.viewPatients {
    min-height:100vh;
    background-color:#f6f8fb;
    background:#f6f8fb url('../../assets/layout/img/bg-content-layer-02.png') top no-repeat;
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