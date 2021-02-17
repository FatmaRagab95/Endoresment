<template>
    <div class="viewPatients">
        <div class="container">

            <h1 class='pt-3 pb-3  border-bottom mainColor'>
                <i class='ml-3 mr-3 fa fa-hospital-o'></i>
                Patients List</h1>

            <div class='row'>
                <div class="col-md-4" v-for='patient in patients' :key='patient.id'>
                    <div class='details'>
                        <div class="inner">
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
                                <a href='#' class='btn'>View Details</a>
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
    name:'viewPatients',
    data() {
        return {
            patients: [],
            apiUrl: "http://localhost:52861/endoresment/dist/",
        }
    },
    created() {
        let that = this;
        //get Patients
        $.ajax({
            type: "POST",
            url: that.apiUrl + "endoresment/viewPatients.aspx/getPatientsData",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                that.patients = JSON.parse(data.d);
            },
        });
    }
}
</script>

<style scoped>
.viewPatients {
    min-height:100vh;
    background:#fff url('../../assets/layout/img/patients1.png') right no-repeat;
    background-size:contain;
}
.viewPatients .details {
    max-width: 300px;
    border: 5px solid rgba(125, 145, 125, 0.07);
    margin: 15px auto;
    border-radius: 10px;
    box-shadow: 0 0 1px 1px #ccc;
}
.viewPatients .details .inner {
    border-radius:10px;
    background-color:#fff;
    padding:20px;
    position:relative;
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
    color: #fff;
    background-color: #046132;
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
</style>