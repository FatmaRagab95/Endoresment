<template>
    <div class="shiftDetails">
        <div class="container">
            <h2 class='mt-5'>Shift Details</h2>
            <p>Update date in {{UnitDash[0].Entry_date}}</p>

            <div class="detials" v-if='UnitDash.length > 0'>
                <div class="row">
                    <div class="col-lg-6">

                        <ul class='list-unstyled  card bg-white shadow'>
                            <li class='cu-flex detail'>
                                <span>Unit Name</span>
                                <span>{{UnitDash[0].Unit_name}}</span>
                            </li>
                            <li class='cu-flex detail'>
                                <span>Shift</span>
                                <span>
                                    <i :class="UnitDash[0].Shift.trim() == 'Day' ? 'fa fa-sun-o' : 'fa fa-moon-o'"></i>
                                    {{UnitDash[0].Shift}}
                                </span>
                            </li>
                            <li class='cu-flex detail'>
                                <span>Shift date</span>
                                <span>{{UnitDash[0].Shift_date}}</span>
                            </li>
                            <li class='cu-flex detail'>
                                <span>Total Census</span>
                                <span class='badge badge-primary'>{{UnitDash[0].Total_Census}}</span>
                            </li>
                            <li class='cu-flex detail'>
                                <span>Received</span>
                                <span  class='badge badge-warning'>{{UnitDash[0].Received}}</span>
                            </li>
                        </ul>

                    </div>

                    <div class="col-lg-6">

                        <ul class='list-unstyled  card bg-white shadow'>
                            <li class='cu-flex detail'>
                                <span>Admission</span>
                                <span class='badge badge-info'>{{UnitDash[0].Admission}}</span>
                            </li>
                            <li class='cu-flex detail'>
                                <span>Transfer In</span>
                                <span  class='badge badge-success'>{{UnitDash[0].Transfer_In}}</span>
                            </li>
                            <li class='cu-flex detail'>
                                <span>Transfer Out</span>
                                <span  class='badge badge-danger'>{{UnitDash[0].Transfer_Out}}</span>
                            </li>
                            <li class='cu-flex detail'>
                                <span>Endorsing Charge Nurse</span>
                                <span>{{UnitDash[0].Endorsing_ChargeNurse}}</span>
                            </li>
                            <li class='cu-flex detail'>
                                <span>Receive Charge Nurse</span>
                                <span>{{UnitDash[0].Receive_ChargeNurse}}</span>
                            </li>
                        </ul>

                    </div>
                </div>
            </div>
        </div>
    </div>
</template>

<script>
export default {
    name:'shiftDetials',
    data() {
        return {
            UnitDash:[],
            shiftId:this.$route.params.id,
            apiUrl: "http://localhost:62477/endoresment/dist/",
        }
    },
    created () {
        let that = this;

        //get Units dahsboard
        $.ajax({
            type: "POST",
            url: that.apiUrl + "endoresment/ShiftDetails.aspx/getUnitsDashData",
            data:JSON.stringify({"id": {"id": that.shiftId}}),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                that.UnitDash = JSON.parse(data.d);
            },
        });
    }
}
</script>

<style scoped>
.shiftDetails {
    background-color:#f6f8fb;
    text-align: center;
    position: relative;
    overflow: hidden;
    min-height:100vh;
}
.shiftDetails::before {
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
.shiftDetails .detials {
    margin:120px auto;
}
.shiftDetails .detials ul li {
    padding:15px 30px;
}
.shiftDetails .detials li:nth-of-type(even) {
    background-color:#f7f7f7;
}
.shiftDetails .detials li .badge {
    width:50px;
    line-height: 1.5;
}
</style>