<template>
<div class='addUnit'>
    <div class="container-wave">
        <div class="wave"></div>
    </div>

    <div class='internal-page' id='auth'>
        <form @submit.prevent='submitUnit'>
            <div class="custom-form  pt-3">
                <div class="cu-container">
                    <div class="cu-form-group special">
                        <div class="title">
                            <span> <i class='fa fa-hospital-o mainColor mr-1'></i> Add shift details</span>
                        </div>

                        <div class="cu-field">
                            <h3 class="cu-label">
                                <label>Unit:</label>
                            </h3>
                            <div class="f-select">
                                <select class="form-control form-control-sm" v-model='newUnit.Unit_id' required>
                                    <option v-for='unit in  Units' :value='unit.U_id'
                                    :key='unit.U_name'>{{unit.U_name}}</option>
                                </select>
                            </div>
                        </div>
                        
                        <div class="cu-field">
                            <h3 class="cu-label">
                                <label>Endorsing Charge Nurse :</label>
                            </h3>
                            <div class="f-select">
                                <select class="form-control form-control-sm" v-model='newUnit.Endorsing_ChargeNurse_id' required>
                                    <option v-for='Nurse in  Nurses' :value='Nurse.Emp_ID'
                                    :key='Nurse.Emp_ID'>{{Nurse.FullName}}</option>
                                </select>
                            </div>
                        </div>
                        
                        <div class="cu-field">
                            <h3 class="cu-label">
                                <label>Receive Charge Nurse :</label>
                            </h3>
                            <div class="f-select">
                                <select class="form-control form-control-sm" v-model='newUnit.Receive_ChargeNurse_id' required>
                                    <option v-for='Nurse in  Nurses' :value='Nurse.Emp_ID'
                                    :key='Nurse.Emp_ID'>{{Nurse.FullName}}</option>
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
                                            <input type="text" v-model="newUnit.Received" required>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="cu-field">
                                        <h3 class="cu-label">
                                            <label>Admission :</label>
                                        </h3>
                                        <div class="cu-input text-box">
                                            <span class="fa fa-edit"></span>
                                            <input type="text" v-model="newUnit.Admission" required>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="cu-field">
                                        <h3 class="cu-label">
                                            <label>Transfer In :</label>
                                        </h3>
                                        <div class="cu-input text-box">
                                            <span class="fa fa-edit"></span>
                                            <input type="text" v-model="newUnit.Transfer_In" required>
                                        </div>
                                    </div>
                                </div>
                                
                                <div class="col-md-6">
                                    <div class="cu-field">
                                        <h3 class="cu-label">
                                            <label>Transfer Out :</label>
                                        </h3>
                                        <div class="cu-input text-box">
                                            <span class="fa fa-edit"></span>
                                            <input type="text" v-model="newUnit.Transfer_Out" required>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-12">
                                    <div class="cu-field">
                                        <h3 class="cu-label">
                                            <label>Total Census :</label>
                                        </h3>
                                        <div class="cu-input text-box">
                                            <span class="fa fa-edit"></span>
                                            <input type="text" v-model="newUnit.Total_Census" required>
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
                                        <input id="Day" type="radio" name="Shift" value="Day"
                                        @click='newUnit.Shift = "Day"' required>
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
                                        <input id="Night" type="radio" name="Shift" value="Night"
                                        @click='newUnit.Shift = "Night"' required>
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
                                <input :max='date' type="date" v-model="newUnit.Shift_date" required>
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
    name:'addUnit',
    props: ['link'],
    data() {
        return {
            apiUrl: `http://localhost:${this.link}/endoresment/dist/`,
            date: new Date(new Date().getTime() + (10000 * 60 * 6)).toISOString().substr(0,10),
            Nurses:[],
            Units:[],
            newUnit: {
                Unit_id:0,
                Unit_name:'',
                Shift:'',
                Shift_date:'',
                Total_Census:0,
                Received:0,
                Admission:0,
                Transfer_In:'',
                Transfer_Out:'',
                Endorsing_ChargeNurse_id:'',
                Endorsing_ChargeNurse:'',
                Receive_ChargeNurse_id:'',
                Receive_ChargeNurse:'',
            }
        }
    },
    methods: {
        submitUnit() {
             if ($("form").is(":valid")) {
                 let that = this;

                 that.newUnit.Endorsing_ChargeNurse = that.Nurses.filter(x => x.Emp_ID == that.newUnit.Endorsing_ChargeNurse_id)[0].FullName;

                 that.newUnit.Receive_ChargeNurse = that.Nurses.filter(x => x.Emp_ID == that.newUnit.Receive_ChargeNurse_id)[0].FullName;

                 that.newUnit.Unit_name = that.Units.filter(x => x.U_id == that.newUnit.Unit_id)[0].U_name;

                //insert Unit dashboard details
                $.ajax({
                    type: "POST",
                    url: that.apiUrl + "endoresment/add_unit.aspx/newUnit",
                    data:JSON.stringify({"data": that.newUnit}),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (data) {
                        that.$router.push({name:'Units'})
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
    created() {

        let that = this;

        //get units
        $.ajax({
        type: "POST",
        url: that.apiUrl + "endoresment/add_unit.aspx/getUnitsData",
        contentType: "application/json; charset=utf-8",
        data:JSON.stringify({"branch": {'id': 1}}),
        dataType: "json",
        success: function (data) {
            that.Units = JSON.parse(data.d);
        },
        });

        //get nurses
        $.ajax({
        type: "POST",
        url: that.apiUrl + "endoresment/add_unit.aspx/getNursesData",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            that.Nurses = JSON.parse(data.d);
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