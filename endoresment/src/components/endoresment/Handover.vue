<template>
    <div class="Handover pb-5">
        <h1 class='text-center mb-5 bg-white p-3 pt-5'>Endoresment / Handover</h1>
        <div class="container-fluid">

            <div class='bg-white shadow table-responsive mb-5' v-for='(unit) in UnitDash' :key='unit.id'>
                <h4 class='cu-flex p-3 shadow'>
                    <span><i class='fa fa-hospital-o'></i> {{unit.Unit_name}}</span>
                    <router-link class='btn btn-primary' :to='{name:"Nurses", params:{id:unit.id}}'>Nurses List</router-link>
                </h4>
                <div class="scroll-box">
                    <table class="table table-bordered">
                        <thead class='seperated'>
                            <th scope="col"></th>
                            <th colspan="4" scope="col"><span>Date:</span> {{unit.Shift_date.substr(0,10)}}</th>
                            <th colspan="6" scope="col"><span>Shift:</span> {{unit.Shift}}</th>
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
                                    <td>To Do</td>
                                    <td>Follow Up</td>
                                </th>
                                <th><span>Contraptions & Infusions</span></th>
                                <th><span>Routise plan of care</span></th>
                                <th><span>Surgery, Procedures</span></th>
                                <th class='bg-warning'><span>Progress of consultations</span></th>
                            </tr>
                            <tr v-for='patient in patients.filter(x => unit.Unit_name.trim() == x.Unit.trim())' :key='patient.id'>
                                <td><span>{{patient.Room}}</span></td>
                                <td><span>{{patient.Patient_FullName}}</span></td>
                                <td><span>{{patient.Addmission_date}}</span>
                                    <br>
                                    <span></span>
                                </td>
                                <td><span>{{FollowData(patient.id, 'Diagnosis')}}</span></td>
                                <td><span>{{FollowData(patient.id, 'DR_ProgressNotes')}}</span></td>
                                <td><span>{{patient.Age}}</span></td>
                                <td><span>{{FollowData(patient.id, 'Diet_Name')}}</span></td>
                                <td><span>{{patient.Consultant_Name}}</span></td>
                                <td><span>{{FollowData(patient.id, 'Pain')}}</span></td>
                                <td><span>{{FollowData(patient.id, 'Allergy')}}</span></td>
                                <td><span>{{FollowData(patient.id, 'P_Isolation')}}</span></td>
                                <td><span>{{FollowData(patient.id, 'Fall')}}</span></td>
                                <td><span>{{FollowData(patient.id, 'Investegation_ToDo')}}</span></td>
                                <td><span>{{FollowData(patient.id, 'Investegation_FollowUp')}}</span></td>
                                <td><span>{{FollowData(patient.id, 'Contraptions_Infusions')}}</span></td>
                                <td><span>{{FollowData(patient.id, 'Routise_PlanOfCare')}}</span></td>
                                <td><span>{{FollowData(patient.id, 'Surgury_Procedures')}}</span></td>
                                <td><span>{{FollowData(patient.id, 'DR_Consultaion_Progress')}}</span></td>
                                <td><router-link :to='{name:"Insert Patient Data", params:{id: patient.id}}'><i class='fa fa-edit btn btn-success btn-sm'>Edit</i></router-link></td>
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
export default {
    name:'Handover',
    props: ['link'],
    data() {
        return {
            UnitDash: [],
            apiUrl: `http://localhost:${this.link}/endoresment/dist/`,
            patients:[],
            patientsFollow:[]
        }
    },
    methods: {
        FollowData(id, column) {
            if (this.patientsFollow.length > 0) {

                let checkData =  this.patientsFollow.filter(x => x.Patient_id == id);
                return checkData.length > 0 ? checkData.column : '';
            } else {
                return ''
            }
        }
    },
    created() {
        let that = this;

        //get Units dahsboard
        $.ajax({
            type: "POST",
            url: that.apiUrl + "endoresment/handover.aspx/getUnitDashData",
            data:JSON.stringify({"chargeNurse": JSON.parse(localStorage.getItem("user"))}),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                that.UnitDash = JSON.parse(data.d);
                if (new Date().getHours() < 19 && new Date().getHours() >= 8) {
                    that.Shift = 'Day';
                    that.UnitDash.map(x => {
                        if (x.Shift.trim() == 'Night') {
                            x.Received = 0;
                            x.Total_Census = 0;
                        }
                    });
                } else {
                    that.Shift = 'Night';
                    that.UnitDash.map(x => {
                        if (x.Shift.trim() == 'Day') {
                            x.Received = 0;
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
            data:JSON.stringify({"chargeNurse": JSON.parse(localStorage.getItem("user"))}),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                that.patients = JSON.parse(data.d);

                // get follow ups
                for (let i = 0; i < that.patients.length; i++) {

                    $.ajax({
                        type: "POST",
                        url: that.apiUrl + "endoresment/handover.aspx/getShiftData",
                        data:JSON.stringify({"data": {"Patient_id":  that.patients[i].id}}),
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (data) {
                            that.patientsFollow.push(JSON.parse(data.d)[0]);
                        },
                    });

                }
            },
        });
    }
}
</script>

<style scoped>
.Handover .scroll-box {
    max-height:500px;
}
.Handover table {
    font-size:14px;
    text-align:center;
    color:#444
}
.Handover table .seperated {
    border-top: 2px solid #ccc !important;
}
.Handover table th {
    font-weight: normal;
    display: table-cell;
    vertical-align: middle;
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
</style>