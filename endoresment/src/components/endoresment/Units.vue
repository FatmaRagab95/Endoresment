<template>
    <div class="Units">
        <div class="container">

            <div class='statisics'>
                    
                <h4 class="text-center"><span class='text-secondary'>latest statistics data for day</span> 
                        {{new Date().toDateString()}}.</h4>
                <p class='mb-5 text-center text-secondary'>
                    Current shift 
                    <span class='text-info'><i :class="Shift == 'Day' ? 'fa fa-sun-o' : 'fa fa-moon-o'"></i> {{Shift}}</span>
                </p>

                <div class='cu-flex'>
                    <div class="">
                        <div class='outer'>
                            <div class='inner bg-white shadow text-center'>

                                <span class='num'>{{sumDash('Total_Census')}}</span>
                                Total Census

                            </div>
                        </div>
                    </div>
                    <div class="">
                        <div class='outer'>
                            <div class='inner bg-white shadow text-center'>

                                <span class='num'>{{sumDash('Admission')}}</span>
                                Admission  

                            </div>
                        </div>
                    </div>
                    <div class="">
                        <div class='outer'>
                            <div class='inner bg-white shadow text-center'>

                                <span class='num'>{{sumDash('Admission')}}</span>
                                Discharge 

                            </div>
                        </div>
                    </div>
                    <div class="">
                        <div class='outer'>
                            <div class='inner bg-white shadow text-center'>

                                <span class='num'>{{sumDash('Admission')}}</span>
                                ER

                            </div>
                        </div>
                    </div>
                    <div class="">
                        <div class='outer'>
                            <div class='inner bg-white shadow text-center'>

                                <span class='num'>{{sumDash('Transfer_In')}}</span>
                                OR

                            </div>
                        </div>
                    </div>
                    <div class="">
                        <div class='outer'>
                            <div class='inner bg-white shadow text-center'>

                                <span class='num'>{{sumDash('Transfer_Out')}}</span>
                                Death

                            </div>
                        </div>
                    </div>
                    </div>
                </div>
            </div>

            <div class='row align-items-baseline'>
                <div class='col-md-8'>
                    <h1 class='pt-3 pb-3  border-bottom mainColor'>
                        <i class='ml-3 mr-3 fa fa-hospital-o'></i>
                        Units List
                    </h1>
                </div>

                <div class="col-md-4">
                    <div class="input-group">
                        <div class="input-group-prepend">
                            <span class="input-group-text fa fa-search" id="basic-addon1"></span>
                        </div>
                        <input type="text" class="form-control" v-model='search' placeholder="Search for unit" aria-label="Search for unit" aria-describedby="basic-addon1">
                    </div>
                </div>
            </div>
            <div class='row'>
                <div class="col-xl-4 col-lg-6" v-for='unit in filtered' :key='unit.U_id'>
                    <div class='details'>
                        <div class="inner shadow">
                            <h3 class='pb-2'>{{unit.U_name}}</h3>

                            <span class='Census badge badge-light border font-weight-normal'>
                                Total Census: <span class='text-danger'>{{filterDash('Total_Census',unit.U_id)}} </span>
                            </span>

                            <span class='Admission badge font-weight-normal'>
                                Admission: {{filterDash('Admission',unit.U_id)}}
                            </span>

                            <div class='row mt-3 p-2 border rounded Transfer'>
                                <div class="col-6 border-right text-success">
                                    <span>Transfer In:</span> {{filterDash('Transfer_In',unit.U_id)}}
                                </div>
                                <div class="col-6 text-danger">
                                    <span>Transfer Out:</span> {{filterDash('Transfer_Out',unit.U_id)}}
                                </div>
                            </div>
                            
                            <p class='RoomsNum mt-3'>
                                <i class='fa fa-hospital-o mainColor'></i>
                                <span>Rooms Number:</span>
                                {{unit.RoomsNum}}
                            </p>

                            <div class='text-right'>
                                <router-link :to='{name:"Unit Details", params:{id:unit.U_id}}' class='btn'>View Details</router-link >
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
</template>

<script>
export default {
    name:'Units',
    data() {
        return {
            patients: [],
            Units:[],
            UnitsDash: [],
            search:'',
            filtered: [],
            Shift:'',
            apiUrl: "http://localhost:49638/endoresment/dist/",
        }
    },
    methods: {
        sumDash(column) {
            return this.UnitsDash.reduce((a,b) => a + b[column], 0)
        },

        filterDash(column,id) {
             let data = this.UnitsDash.filter(x => x.Unit_id == id).length > 0 ?
                                this.UnitsDash.filter(x => x.Unit_id == id) : 0;
            return data != 0 ? data[data.length - 1][column] : 0
        }
    },
    watch: {
        search: function () {
            this.filtered = this.Units.filter(x => x.U_name.startsWith(this.search))
        }
    },
    created() {
        let that = this;

        //get Units
        $.ajax({
            type: "POST",
            url: that.apiUrl + "endoresment/Units.aspx/getUnitsData",
            data:JSON.stringify({"branch": {"id": 1}}),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                that.Units = JSON.parse(data.d);
                that.filtered = that.Units;
            },
        });

        //get Units dahsboard
        $.ajax({
            type: "POST",
            url: that.apiUrl + "endoresment/Units.aspx/getUnitsDashData",
            data:JSON.stringify({"branch": {"id": 1}}),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                that.UnitsDash = JSON.parse(data.d);
                if (new Date().getHours() < 19 && new Date().getHours() >= 8) {
                    that.Shift = 'Day';
                    that.UnitsDash.map(x => {
                        if (x.Shift.trim() == 'Night') {
                            x.Received = 0;
                            x.Total_Census = 0;
                        }
                    });
                } else {
                    that.Shift = 'Night';
                    that.UnitsDash.map(x => {
                        if (x.Shift.trim() == 'Day') {
                            x.Received = 0;
                            x.Total_Census = 0;
                        }
                    });
                }
            },
        });
    }
}
</script>

<style scoped>
.Units {
    min-height:100vh;
    background-color:#f6f8fb;
}

.statisics {
    padding:60px 15px;
}
.statisics .outer{
    background-color: #ddeeda;
    max-width: 135px;
    margin: auto;
}
.statisics .inner {
    padding: 29px;
    width: 135px;
    margin: auto;
    font-size: 15px;
    color: #046132;
    border-radius:5px;
}
.statisics .inner .num {
    display:block;
    font-size:50px;
    color:#000
}

.Units .details {
    max-width: 300px;
    margin: 20px auto;
    background-color: #baf2b1;
}
.Units .details .inner {
    background-color:#fff;
    padding:20px;
    position:relative;
    padding: 29px;
    margin: auto;
    border-radius:5px;
}
.Units .details .inner h3 {
    color: #046132;
    font-size:19px;
}
.Units .details .inner .Admission {
    position: absolute;
    top: 13px;
    right: 5px;
    font-size: 16px;
    color: #046132;
    background-color:#e2fbde
}
.Units .details .inner .Census {
    font-size:16px;
}
.Units .details .inner .Transfer {font-size:14px;}
.Units .details .inner .Transfer span {
    margin-right:10px;
}
.Units .details .inner .btn {
    box-shadow: 2px 2px 2px #ccc, 5px 5px 15px inset #ccc;
}
</style>