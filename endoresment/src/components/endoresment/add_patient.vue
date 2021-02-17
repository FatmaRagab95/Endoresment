<template>
<div class='addPatient'>
    <div class="container-wave">
        <div class="wave"></div>
    </div>

    <div class='internal-page' id='auth'>
        <form @submit.prevent='InsertPatient'>
            <div class="custom-form  pt-3">
                <div class="cu-container">
                    <div class="cu-form-group special">
                        <div class="title">
                            <span> <i class='fa fa-hospital-o mainColor mr-1'></i> admission Form</span>
                        </div>

                        <div class="cu-field">
                            <h3 class="cu-label">
                                <label>Patient Full Name :</label>
                            </h3>
                            <div class="cu-input text-box">
                                <span class="fa fa-edit"></span>
                                <input type="text" v-model="newPatient.Patient_FullName" required>
                            </div>
                        </div>

                        <div class="cu-field">
                            <h3 class="cu-label">
                                <label>Medical Number :</label>
                            </h3>
                            <div class="cu-input text-box">
                                <span class="fa fa-edit"></span>
                                <input type="number" v-model="newPatient.Medical_Number" required>
                            </div>
                        </div>

                        <div class="cu-field">
                            <h3 class="cu-label">
                                <label>Date Of Birth :</label>
                            </h3>
                            <div class="cu-input text-box">
                                <span class="fa fa-calendar-o"></span>
                                <input :max='new Date().toISOString().substr(0,10)' type="date" v-model="newPatient.Date_Birth" required>
                            </div>
                        </div>

                        <div class="cu-field">
                            <h3 class="cu-label">
                                <label>Age :</label>
                            </h3>
                            <div class="cu-input text-box">
                                <span class="fa fa-edit"></span>
                                <input type="number" v-model="newPatient.Age" required>
                            </div>
                        </div>
                        
                        <div class="cu-field radio-group">
                            <div class="row align-items-end">
                                <div class="col-md-6">
                                    <div class="cu-field">
                                        <h3 class="cu-label">
                                            <label>Gender</label>
                                        </h3>
                                    </div> 
                                    <div class="cu-field">
                                        <input id="gender" type="radio" name="gender" value="Male"
                                        @click='newPatient.Gender = "Male"' required>
                                        <label for="gender">
                                            <div class="cu-input cu-radio normal">
                                                <div class="contain">
                                                    <span class="fa fa-check"></span> 
                                                    <div>Male</div>
                                                </div>
                                            </div>
                                        </label>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="cu-field">
                                        <input id="gender1" type="radio" name="gender" value="Female"
                                        @click='newPatient.Gender = "Female"' required>
                                        <label for="gender1">
                                            <div class="cu-input cu-radio normal">
                                                <div class="contain">
                                                    <span class="fa fa-check"></span>
                                                    <div>Female</div>
                                                </div>
                                            </div>
                                        </label>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="cu-field">
                            <h3 class="cu-label">
                                <label>Branch :</label>
                            </h3>
                            <div class="f-select">
                                <select class="form-control form-control-sm" v-model='newPatient.Branch_id' required>
                                    <option v-for='branch in  Branches' :value='branch.id'
                                    :key='branch.Branch_EName'>{{branch.Branch_EName}}</option>
                                </select>
                            </div>
                        </div>

                        <div class="cu-field" v-if='Units.length > 0'>
                            <h3 class="cu-label">
                                <label>Unit :</label>
                            </h3>
                            <div class="f-select">
                                <select class="form-control form-control-sm" v-model='newPatient.Unit' required>
                                    <option v-for='unit in  Units' :value='unit.U_id'
                                    :key='unit.U_name'>{{unit.U_name}}</option>
                                </select>
                            </div>
                        </div>

                        <div class="cu-field" v-if='Rooms.length > 0'>
                            <h3 class="cu-label">
                                <label>Room :</label>
                            </h3>
                            <div class="f-select">
                                <select class="form-control form-control-sm" v-model='newPatient.Room' required>
                                    <option v-for='room in  Rooms' :value='room.R_name'
                                    :key='room.R_name'>{{room.R_name}}</option>
                                </select>
                            </div>
                        </div>
                        
                        <div class="cu-field">
                            <h3 class="cu-label">
                                <label>Speciality :</label>
                            </h3>
                            <div class="f-select">
                                <select class="form-control form-control-sm" v-model='newPatient.Specialty' required>
                                    <option v-for='speciality in  Specialities' :value='speciality.Spcy_name_En'
                                    :key='speciality.Spcy_name' v-if='speciality.Spcy_name_En != ""'>{{speciality.Spcy_name_En}}</option>
                                </select>
                            </div>
                        </div>

                        <div class="cu-field">
                            <h3 class="cu-label">
                                <label>Consultant Name :</label>
                            </h3>
                            <div class="cu-input text-box">
                                <span class="fa fa-edit"></span>
                                <input type="text" v-model="newPatient.Consultant_Name" required>
                            </div>
                        </div>

                        <div class="cu-field">
                            <h3 class="cu-label">
                                <label>Addmission Date :</label>
                            </h3>
                            <div class="cu-input text-box">
                                <span class="fa fa-calendar-o"></span>
                                <input :max='new Date().toISOString().substr(0,10)' type="date" v-model="newPatient.Addmission_date" required>
                            </div>
                        </div>

                        <div class="text-center">
                            <button class='special-btn'>Save</button>
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
    name:'addPatient',
    data() {
        return {
            apiUrl: "http://localhost:52861/endoresment/dist/",
            Branches:[],
            Units:[],
            Rooms:[],
            Specialities:[],
            newPatient: {
                Patient_FullName:'',
                Branch_id:0,
                Branch_name:'',
                Gender:'',
                Medical_Number:0,
                Unit:'',
                Room:'',
                Addmission_date:'',
                Age:0,
                Date_Birth:'',
                Consultant_Name:'',
                Specialty:'',
                Patient_Status:1,
            }
        }
    },
    methods: {
        InsertPatient() {
             if ($("form").is(":valid")) {
                 let that = this;
                 that.newPatient.Branch_name = that.Branches.filter(x => x.id == that.newPatient.Branch_id)[0].Branch_EName;

                 that.newPatient.Unit = that.Units.filter(x => x.U_id == that.newPatient.Unit)[0].U_name;

                //insert new patient
                $.ajax({
                    type: "POST",
                    url: that.apiUrl + "endoresment/add_patient.aspx/newPatient",
                    data:JSON.stringify({"data": that.newPatient}),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (data) {
                        that.$router.push({name:'viewPatients'})
                    },
                });
             } else {
                 swal({
                     icon:'warning',
                     dangerMode:true,
                     text:'Please Fill All Fields!'
                 })
             }
        }
    },
    watch: {
        'newPatient.Branch_id': function () {
            let branch = this.newPatient.Branch_id,
            that = this;

            //get Units
            $.ajax({
                type: "POST",
                url: that.apiUrl + "endoresment/add_patient.aspx/getUnitsData",
                data:JSON.stringify({"branch": {"id": branch}}),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    that.Units = JSON.parse(data.d);
                },
            });
        },
        'newPatient.Unit': function () {
            let Unit = this.newPatient.Unit,
            that = this;

            //get Rooms
            $.ajax({
                type: "POST",
                url: that.apiUrl + "endoresment/add_patient.aspx/getRoomsData",
                data:JSON.stringify({"unit": {"U_id": Unit}}),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    that.Rooms = JSON.parse(data.d);
                },
            });
        }
    },
    created() {

        let that = this;

        //get branches
        $.ajax({
        type: "POST",
        url: that.apiUrl + "endoresment/add_patient.aspx/getBranchesData",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            that.Branches = JSON.parse(data.d);
        },
        });

        //get Specialities
        $.ajax({
        type: "POST",
        url: that.apiUrl + "endoresment/add_patient.aspx/getSpecialitiesData",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            that.Specialities = JSON.parse(data.d);
        },
        });
    }
}
</script>

<style scoped>
    .container-wave {
        position: absolute;
        background: #fff;
        height: 50vh;
        width:100%;
        left:0;
        top:0;
    }

    .wave {
        position: absolute;
        height: 250px;
        width: 100%;
        background: #fff;
        bottom: 0;
    }

    .wave::before, .wave::after {
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
        position:relative;
        z-index:2;
    }
    .custom-form .cu-form-group.special {
        margin-top:60px;
    }
    .custom-form .cu-form-group .title {
        text-align:initial;
        top: -60px;
    }
    .custom-form .cu-form-group .title span {
        height: 45px;
        line-height: 43px;
        font-size: 20px;
        box-shadow: 0 1px 10px 0 rgba(206, 206, 206, 0.3),0 1px 3px 1px rgba(225, 225, 225, 0.15);
        border-radius: 30px;
        background-color: #fff;
        color: #212529;
        border:1px solid #e0e0e0;
    }
    .internal-page .custom-form .cu-field {
        max-width:600px;
        margin:15px auto;
    }
    .custom-form .cu-field .cu-input.text-box {
        margin-inline-end: 0px;
    }
    .internal-page .f-select::before {
        top: 50%;
    }
    .special-btn {
        border-radius:65px;
        padding: 10px 30px;
        margin: 30px 10px;
        text-transform: capitalize;
    }
    .special-btn:hover {opacity:.7}
    .special-btn.blue-btn {background-color: #007cff;}
    .special-btn.yellow-btn {
        background-color: #f8f2a4;
        color: #000;
    }
    .special-btn i {margin-right:15px;}
</style>