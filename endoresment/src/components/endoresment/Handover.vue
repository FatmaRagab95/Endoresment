<template>
    <div class="Handover pb-5">
        <h1 class='text-center mb-5 bg-white p-3 pt-5'>Endoresment / Handover</h1>
        <div class="container-fluid">

            <div class='bg-white shadow table-responsive mb-5' v-for='(unit) in UnitDash' :key='unit.id'>
                <h4 class='cu-flex p-3 shadow'>
                    <span><i class='fa fa-hospital-o'></i> {{unit.Unit_name}}</span>
                    <router-link class='btn btn-primary' :to='{name:"Nurses", params:{id:unit.Unit_id}}'>Nurses List</router-link>
                </h4>
                <div class="scroll-box" v-dragscroll>
                    <table class="table table-bordered" v-if='patientsFollow.length == patients.length'>
                        <thead class='seperated'>
                            <th scope="col"></th>
                            <th colspan="4" scope="col"><span>Date:</span> {{unit.Shift_date.substr(0,10)}}</th>
                            <th colspan="6" scope="col"><span>Shift:</span> <span class='text-primary'><i :class="Shift == 'Day' ? 'fa fa-sun-o' : 'fa fa-moon-o'"></i> {{Shift}}</span></th>
                            <th colspan="2" scope="col"><span>Total Census:</span> {{unit.Total_Census}}</th>
                            <th colspan="4" scope="col">
                                <p><span>Endorsing Charge Nurse:</span> {{unit.Endorsing_ChargeNurse}}</p>
                                <p><span>Recieving Charge Nurse:</span> {{unit.Receive_ChargeNurse}}</p>
                            </th>
                            <th colspan='2'></th>
                        </thead>
                        <tbody>
                            <tr>
                                <th></th>
                                <td colspan="4"><span>Recieved:</span> {{unit.Received}}</td>
                                <td colspan="6"><span>Admission:</span> {{unit.Admission}}</td>
                                <td><span>Transfer In:</span> {{unit.Transfer_In}}</td>
                                <td><span>Transfer Out:</span> {{unit.Transfer_Out}}</td>
                                <td colspan="4"><span>Discharged:</span> 0</td>
                            </tr>
                            <tr class='seperated'>
                                <th><span>Room</span></th>
                                <th><span>Patient's Name &amp; MR</span></th>
                                <th><span>DOA &amp; LOS</span></th>
                                <th class='bg-warning'><span>Diagnosis</span></th>
                                <th class='bg-warning'><span>Progress Notes</span></th>
                                <th><span>Age</span></th>
                                <th><span>Diet</span></th>
                                <th><span>Consultant Name</span></th>
                                <th><span>Pain</span></th>
                                <th><span>Allergy</span></th>
                                <th><span>Isolation</span></th>
                                <th><span>Fall</span></th>
                                <th colspan='2'>
                                    <span>Investigations</span>
                                    <div class='cu-flex'>
                                        <div>To Do</div>
                                        <div>Follow Up</div>
                                    </div>
                                </th>
                                <th><span>Contraptions & Infusions</span></th>
                                <th><span>Routise plan of care</span></th>
                                <th><span>Surgery, Procedures</span></th>
                                <th class='bg-warning'><span>Progress of consultations</span></th>
                            </tr>
                            <tr v-for='(patient, i) in patients.filter(x => unit.Unit_name.trim() == x.Unit.trim())' :key='patient.id'>
                                <td>
                                    <span>{{patient.Room}}</span>
                                </td>
                                <td>
                                    <input v-if='patient.edit' type='text' v-model='patient.Patient_FullName'>
                                    <span v-else>{{patient.Patient_FullName}}</span>
                                </td>
                                <td>
                                    
                                    <input v-if='patient.edit' type='date' v-model='patient.Addmission_date'>
                                    <span v-else>{{patient.Addmission_date}}</span>
                                    <br>
                                    <span v-if='!patient.edit'>{{LOS(patient.Addmission_date)}}</span>
                                </td>
                                <td class='bigText'>
                                    <textarea :disabled="user.Role_id == 12 || user.Role_id == 17 ? 'disabled' : ''" v-if='patient.edit' v-model="FollowData(patient.id).DR_Diagnosis"></textarea>
                                    <span v-else>{{FollowData(patient.id).DR_Diagnosis}}</span>
                                </td>
                                <td class='bigText'>
                                    <textarea  :disabled="user.Role_id == 12 || user.Role_id == 17 ? 'disabled' : ''" v-if='patient.edit' v-model="FollowData(patient.id).DR_ProgressNotes"></textarea>
                                    <span v-else>{{FollowData(patient.id).DR_ProgressNotes}}</span>
                                </td>
                                <td>
                                    <input v-if='patient.edit' type='text' v-model='patient.Age'>
                                    <span v-else>{{patient.Age}}</span>
                                </td>
                                <td>
                                    <select v-if='patient.edit' v-model="FollowData(patient.id).Diet_Name">
                                        <option v-for='diet in Diets' :key='diet.id' 
                                        :value="diet.Diet_Name"  @click='FollowData(patient.id).Diet_id = diet.id '>{{diet.Diet_Name}}</option>
                                    </select>
                                    <span v-else>{{FollowData(patient.id).Diet_Name}}</span>
                                </td>
                                <td>
                                    <input v-if='patient.edit' type='text' v-model='patient.Consultant_Name'>
                                    <span v-else>{{patient.Consultant_Name}}</span>
                                </td>
                                <td>
                                    <select v-if='patient.edit' v-model="FollowData(patient.id).Pain">
                                        <option value='0'>0</option>
                                        <option value='1'>1</option>
                                        <option value='2'>2</option>
                                        <option value='3'>3</option>
                                        <option value='4'>4</option>
                                        <option value='5'>5</option>
                                        <option value='6'>6</option>
                                        <option value='7'>7</option>
                                        <option value='8'>8</option>
                                        <option value='9'>9</option>
                                        <option value='10'>10</option>
                                    </select>
                                    <span v-else>{{FollowData(patient.id).Pain}}</span>
                                </td>
                                <td class='bigText'>
                                    <textarea v-if='patient.edit' v-model="FollowData(patient.id).Allergy"></textarea>
                                    <span v-else>{{FollowData(patient.id).Allergy}}</span>
                                </td>
                                <td>
                                    <select v-if='patient.edit' v-model="FollowData(patient.id).P_Isolation">
                                        <option value='None'>None</option>
                                        <option value='Contact'>Contact</option>
                                        <option value='Droplet'>Droplet</option>
                                        <option value='Airborn'>Airborn</option>
                                        <option value='Contact Droplet'>Contact Droplet</option>
                                        <option value='Contact Airborn'>Contact Airborn</option>
                                    </select>
                                    <span v-else>{{FollowData(patient.id).P_Isolation}}</span>
                                </td>
                                <td>
                                    <select v-if='patient.edit' v-model="FollowData(patient.id).Fall">
                                        <option value='None'>None</option>
                                        <option value='F'>F</option>
                                    </select>
                                    <span v-else>{{FollowData(patient.id).Fall}}</span>
                                </td>
                                <td class='bigText'>
                                    <textarea v-if='patient.edit' v-model="FollowData(patient.id).Investegation_ToDo"></textarea>
                                    <span v-else>{{FollowData(patient.id).Investegation_ToDo}}</span>
                                </td>
                                <td class='bigText'>
                                    <textarea v-if='patient.edit' v-model="FollowData(patient.id).Investegation_FollowUp"></textarea>
                                    <span v-else>{{FollowData(patient.id).Investegation_FollowUp}}</span>
                                </td>
                                <td class='bigText'>
                                    <textarea v-if='patient.edit' v-model="FollowData(patient.id).Contraptions_Infusions"></textarea>
                                    <span v-else>{{FollowData(patient.id).Contraptions_Infusions}}</span>
                                </td>
                                <td class='bigText'>
                                    <textarea v-if='patient.edit' v-model="FollowData(patient.id).Routise_PlanOfCare"></textarea>
                                    <span v-else>{{FollowData(patient.id).Routise_PlanOfCare}}</span>
                                </td>
                                <td>
                                    <input v-if='patient.edit' type='text' v-model="FollowData(patient.id).Surgury_Procedures">
                                    <span v-else>{{FollowData(patient.id).Surgury_Procedures}}</span>
                                </td>
                                <td class='bigText'>
                                    <textarea  :disabled="user.Role_id == 12 || user.Role_id == 17 ? 'disabled' : ''" v-if='patient.edit' type='text' v-model="FollowData(patient.id).DR_Consultaion_Progress"></textarea>
                                    <span v-else>{{FollowData(patient.id).DR_Consultaion_Progress}}</span>
                                </td>
                                <td v-if='user.Role_id == 12 || user.Role_id == 17'>
                                    <button v-if='patient.edit' class='btn btn-primary btn-sm'
                                     @click.prevent='SaveEdits(i, patient.id)'>Save</button>
                                    <button v-else class='btn btn-success btn-sm' @click.prevent='edit(i)'>Edit</button>
                                </td>
                            </tr>
                        </tbody>
                        <caption class='text-center' v-if='patients.filter(x => unit.Unit_name.trim() == x.Unit.trim()).length == 0'>
                            <p class='mt-5 alert'><i class='fa fa-warning text-warning'></i> There is no patients at this unit!</p>
                            <router-link class='special-btn' :to='{name:"Admission Form"}'>Admission Form</router-link>
                        </caption>
                    </table>
                </div>
            </div>
        </div>
    </div>
</template>

<script>

import { dragscroll } from 'vue-dragscroll'

export default {
    directives: {
        dragscroll
    },
    name:'Handover',
    props: ['link', 'user'],
    data() {
        return {
            UnitDash: [],
            apiUrl: this.link,
            patients:[],
            patientsFollow:[],
            Diets: [],
            Rooms: [],
            Shift: ''
        }
    },
    methods: {
        FollowData(id) {
            let checkData =  this.patientsFollow.filter(x => x.Patient_id == id);
            return checkData[0];
        },
        LOS(date) {
            let length = Math.round((new Date() - new Date(date.trim())) / (1000 * 60 * 60 * 24));
            return length == 0 ? 'Today' : length == 1 ? length + ' Day' : length + ' Days'
        },
        edit(rowIndex) {
            this.$set(this.patients[rowIndex], 'edit', true);
        },
        SaveEdits(rowIndex, patientId) { //updateFollowUpData
            let that = this;
            swal({
                title: "Are you sure?",
                icon: "warning",
                buttons: true,
                dangerMode: true,
                })
                .then((confirm) => {
                if (confirm) {
                    $.ajax({
                        type: "POST",
                        url: that.apiUrl + "endoresment/handover.aspx/updatePatientData",
                        data:JSON.stringify({"patient": that.patients[rowIndex]}),
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (data) {
                            let dataObj = that.patientsFollow.filter(x => patientId == x.Patient_id)[0];

                            if (!dataObj.id) {
                                // insert into follow up by nurse
                                if (that.user.Role_id == 12 || that.user.Role_id == 17) {
                                    dataObj.Insert_Nurse = that.user.Emp_id;
                                    dataObj.Insert_Nurse_Time = moment(new Date).format();

                                    InsertData();
                                    
                                } else {
                                    
                                    // insert into follow up by doctor
                                }
                                    
                            } else {
                                // update follow up by nurse
                                if (that.user.Role_id == 12 || that.user.Role_id == 17) {

                                    dataObj.Insert_Nurse = dataObj.Insert_Nurse != '' ? that.user.Emp_id : dataObj.Insert_Nurse;
                                    dataObj.Insert_Nurse_Time = dataObj.Insert_Nurse_Time != '' ? moment(new Date).format() : dataObj.Insert_Nurse_Time;

                                    dataObj.Update_Nurse = dataObj.Insert_Nurse != '' ? that.user.Emp_id : '';
                                    dataObj.Update_Nurse_Time = dataObj.Insert_Nurse_Time != '' ? moment(new Date).format() : '';

                                    UpdateData();
                                    
                                } else {
                                    
                                    // update follow up by doctor
                                }
                            }

                            function InsertData () {
                                $.ajax({
                                    type: "POST",
                                    url: that.apiUrl + "endoresment/handover.aspx/insertFollowUpData",
                                    data:JSON.stringify({"patient": dataObj}),
                                    contentType: "application/json; charset=utf-8",
                                    dataType: "json",
                                    success: function (data) {
                                        swal({
                                            title: "Updated!",
                                            icon: "success",
                                        });
                                        that.$set(that.patients[rowIndex], 'edit', false);
                                        location.reload(); 
                                    }
                                });
                            }

                            function UpdateData () {
                                $.ajax({
                                    type: "POST",
                                    url: that.apiUrl + "endoresment/handover.aspx/updateFollowUpData",
                                    data:JSON.stringify({"patient": dataObj}),
                                    contentType: "application/json; charset=utf-8",
                                    dataType: "json",
                                    success: function (data) {
                                        swal({
                                            title: "Updated!",
                                            icon: "success",
                                        });
                                        that.$set(that.patients[rowIndex], 'edit', false);
                                    }
                                });
                            }
                        }
                    });
                } else {
                    swal("Canceled!");
                    this.$set(that.patients[rowIndex], 'edit', false);
                }
            });
        }
    },
    created() {
        let that = this;

        //get Units dahsboard
        $.ajax({
            type: "POST",
            url: that.apiUrl + "endoresment/handover.aspx/getUnitDashData",
            data:JSON.stringify({"chargeNurse": that.user}),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                that.UnitDash = JSON.parse(data.d);
                if (new Date().getHours() < 20 && new Date().getHours() >= 8) {
                    that.Shift = 'Day';
                    that.UnitDash.map(x => {
                        if (x.Shift.trim() == 'Night') {
                            x.Received = 0;
                            x.Admission = 0;
                            x.Total_Census = 0;
                        }
                    });
                } else {
                    that.UnitDash.map(x => {
                    that.Shift = 'Night';
                        if (x.Shift.trim() == 'Day') {
                            x.Received = 0;
                            x.Admission = 0;
                            x.Total_Census = 0;
                        }
                    });
                }
            },
        });

        // get patients data
        $.ajax({
            type: "POST",
            url: that.apiUrl + "endoresment/handover.aspx/getPatientsData",
            data:JSON.stringify({"chargeNurse": that.user}),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                that.patients = JSON.parse(data.d);
                if (that.patients.length > 0) {
                    that.patients.map(x => that.$set(x, 'edit', false))
                }

                // get follow ups
                for (let i = 0; i < that.patients.length; i++) {

                    that.patients[i].Room = that.patients[i].Room.trim();

                    $.ajax({
                        type: "POST",
                        url: that.apiUrl + "endoresment/handover.aspx/getShiftData",
                        data:JSON.stringify({"data": {"Patient_id":  that.patients[i].id}}),
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (data) {
                            if (JSON.parse(data.d).length > 0) {
                                that.patientsFollow.push(JSON.parse(data.d)[0]);
                            }
                            else {
                                let shift = null;
                                if (new Date().getHours() <= 20 && new Date().getHours() >= 8) {
                                    shift = 'Day';
                                } else {
                                    shift = 'Night';
                                }

                                that.patientsFollow.push(
                                    {
                                        Shift: shift,
                                        Patient_id: that.patients[i].id,
                                        Diet_id: 0,
                                        Diet_Name: '',
                                        Pain: 0,
                                        P_Isolation: '',
                                        Fall:'',
                                        Allergy: '',
                                        Investegation_ToDo: '',
                                        Investegation_FollowUp: '',
                                        Contraptions_Infusions: '',
                                        Routise_PlanOfCare: '',
                                        Surgury_Procedures :'',
                                        DR_Diagnosis: '',
                                        DR_ProgressNotes: '',
                                        DR_Consultaion_Progress: '',
                                        Update_Nurse: 0,
                                        Update_Nurse_Time: '',
                                        Update_Doctor: 0,
                                        Update_Doctor_Time: '',
                                        Insert_Nurse: '',
                                        Insert_Nurse_Time: '',
                                        Insert_Doctor: 0,
                                        Insert_Doctor_Time: '',
                                        Consultaion: '',
                                        Transfer_From: '',
                                        Transfer_To: ''
                                    }
                                )
                            }
                        },
                    });

                }
            },
        });

        // get diets 
        $.ajax({
            type: "POST",
            url: that.apiUrl + "endoresment/handover.aspx/getDietsData",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                that.Diets = JSON.parse(data.d);
            }
        });

        // get rooms 
        $.ajax({
            type: "POST",
            url: that.apiUrl + "endoresment/handover.aspx/getRoomsData",
            data:JSON.stringify({"branch": {"id":  that.user.Branch_ID}}),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                that.Rooms = JSON.parse(data.d);
            }
        });
    }
}
</script>

<style scoped>
.Handover .scroll-box {
    max-height:600px;
}
.Handover table {
    font-size:14px;
    text-align:center;
    color:#444;
}
.Handover table:hover {
    cursor:-moz-grab
}
.Handover table .seperated {
    border-top: 2px solid #ccc !important;
    box-shadow: 0 1px 5px #ddd;
}
.Handover table th {
    font-weight: normal;
    display: table-cell;
    vertical-align: middle;
}
.Handover table td input,
.Handover table td textarea,
.Handover table td select {
    border-radius:5px;
    border:1px solid #ccc;
    padding:5px;
}
.Handover table td textarea {
    height:180px;
    font-size:16px;
    color:#333;
    letter-spacing: 1px;
    line-height: 1.5;
}
.Handover table td select option {
    font-family:Verdana, Geneva, Tahoma, sans-serif
}
.Handover table th span {
    color:#000;
}
.Handover table td {
    display: table-cell;
    vertical-align: middle;
}
.Handover table .alert {
    font-size:22px;
}
.Handover table .bigText {
    min-width: 150px;
    text-align: initial;
}
</style>