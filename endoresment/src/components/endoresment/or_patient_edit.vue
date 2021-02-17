<template>
    <div class="or_patient_edit">
        <div class='internal-page'>
                <form>
                    <div class="custom-form  pt-3">
                        <div class="cu-container">
                            <div class="cu-form-group special">
                                <div class="title">
                                    <span> <i class='fa fa-hospital-o mainColor mr-1'></i> Schedule Procedure</span>
                                </div>

                                <div class='row'>
                                    <div class="col-md-6">
                                        <div class="cu-field">
                                            <h3 class="cu-label">
                                                <label>Name of Procedure :</label>
                                            </h3>
                                            <div class="f-select">
                                                <select class="form-control form-control-sm" required>
                                                    <option value=''>...</option>
                                                </select>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="cu-field">
                                            <h3 class="cu-label">
                                                <label>OR Room :</label>
                                            </h3>
                                            <div class="f-select">
                                                <select class="form-control form-control-sm">
                                                    <option value=''>...</option>
                                                </select>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-12">
                                        <div class="cu-field">
                                            <h3 class="cu-label">
                                                <label>Procedure Date :</label>
                                            </h3>
                                            <div class="cu-input text-box">
                                                <span class="fa fa-calendar-o"></span>
                                                <input :min='new Date().toISOString().substr(0,10)' type="date"  required>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="cu-field">
                                            <h3 class="cu-label">
                                                <label>From :</label>
                                            </h3>
                                            <div class="cu-input text-box">
                                                <span class="fa fa-clock-o"></span>
                                                <input :min='new Date().toISOString().substr(0,10)' type="time"  required>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="cu-field">
                                            <h3 class="cu-label">
                                                <label>To :</label>
                                            </h3>
                                            <div class="cu-input text-box">
                                                <span class="fa fa-clock-o"></span>
                                                <input :min='new Date().toISOString().substr(0,10)' type="time"  required>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-12">
                                        <div class="cu-field radio-group">
                                            <div class="row align-items-end">
                                                <div class="col-md-4">
                                                    <div class="cu-field">
                                                        
                                                    <h3 class="cu-label">
                                                        <label>Select patient by:</label>
                                                    </h3>
                                                        <input id="select1" type="radio" name="selectPatient" value="1"
                                                        v-model='selectPatient' required>
                                                        <label for="select1">
                                                            <div class="cu-input cu-radio normal">
                                                                <div class="contain">
                                                                    <span class="fa fa-check"></span> 
                                                                    <div>Medical Number</div>
                                                                </div>
                                                            </div>
                                                        </label>
                                                    </div>
                                                </div>
                                                <div class="col-md-4">
                                                    <div class="cu-field">
                                                        <input id="select2" type="radio" name="selectPatient" value="2"
                                                        v-model='selectPatient' required>
                                                        <label for="select2">
                                                            <div class="cu-input cu-radio normal">
                                                                <div class="contain">
                                                                    <span class="fa fa-check"></span>
                                                                    <div>Bed Number</div>
                                                                </div>
                                                            </div>
                                                        </label>
                                                    </div>
                                                </div>
                                                <div class="col-md-4">
                                                    <div class="cu-field">
                                                        <input id="select3" type="radio" name="selectPatient" value="3"
                                                        v-model='selectPatient' required>
                                                        <label for="select3">
                                                            <div class="cu-input cu-radio normal">
                                                                <div class="contain">
                                                                    <span class="fa fa-check"></span>
                                                                    <div>Outside Hospital</div>
                                                                </div>
                                                            </div>
                                                        </label>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-6" v-if='selectPatient == "1"'>
                                        <div class="cu-field">
                                            <h3 class="cu-label">
                                                <label>Medical number of patient :</label>
                                            </h3>
                                            <div class="cu-input text-box">
                                                <span class="fa fa-edit"></span>
                                                <input type="number" required>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-6" v-if='selectPatient == "2"'>
                                        <div class="cu-field">
                                            <h3 class="cu-label">
                                                <label>Bed number of patient :</label>
                                            </h3>
                                            <div class="cu-input text-box">
                                                <span class="fa fa-edit"></span>
                                                <input type="number" required>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-12" v-if='selectPatient == "3"'>
                                        <div class="row">
                                            <div class="col-md-6">
                                                <div class="cu-field">
                                                    <h3 class="cu-label">
                                                        <label>Patient Full Name :</label>
                                                    </h3>
                                                    <div class="cu-input text-box">
                                                        <span class="fa fa-edit"></span>
                                                        <input type="text" required>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-md-6">
                                                <div class="cu-field">
                                                    <h3 class="cu-label">
                                                        <label>Patient Phone Number :</label>
                                                    </h3>
                                                    <div class="cu-input text-box">
                                                        <span class="fa fa-phone"></span>
                                                        <input type="text" required>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-md-6"> 
                                                <div class="cu-field">
                                                    <h3 class="cu-label">
                                                        <label>Medical number of patient :</label>
                                                    </h3>
                                                    <div class="cu-input text-box">
                                                        <span class="fa fa-edit"></span>
                                                        <input type="number" required>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    
                                    <div class="col-md-12 border-top">
                                        <div class="cu-field">
                                            <h3 class="cu-label">
                                                <label>Speciality :</label>
                                            </h3>
                                            <div class="f-select">
                                                <select class="form-control form-control-sm" v-model='Speciality' required>
                                                    <option value=''>...</option>
                                                    <option value='1'>Lorem Ipsum</option>
                                                </select>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-12" v-if='Speciality != ""'>
                                        <div class="cu-field checkbox-group">
                                            <div class="row align-items-end">
                                                <div class="col-lg-3 col-md-6">
                                                    <div class="cu-field">
                                                        
                                                    <h3 class="cu-label">
                                                        <label>Select doctors:</label>
                                                    </h3>
                                                        <input id="doctor1" type="checkbox" name="doctor" value="1"
                                                        v-model='selectPatient' required>
                                                        <label for="doctor1">
                                                            <div class="cu-input cu-checkbox normal">
                                                                <div class="contain">
                                                                    <span class="fa fa-check"></span> 
                                                                    <div>John Doe</div>
                                                                </div>
                                                            </div>
                                                        </label>
                                                    </div>
                                                </div>
                                                <div class="col-lg-3 col-md-6">
                                                    <div class="cu-field">
                                                        <input id="doctor1" type="checkbox" name="doctor" value="2"
                                                        v-model='selectPatient' required>
                                                        <label for="doctor3">
                                                            <div class="cu-input cu-checkbox normal">
                                                                <div class="contain">
                                                                    <span class="fa fa-check"></span>
                                                                    <div>John Doe</div>
                                                                </div>
                                                            </div>
                                                        </label>
                                                    </div>
                                                </div>
                                                <div class="col-lg-3 col-md-6">
                                                    <div class="cu-field">
                                                        <input id="doctor3" type="checkbox" name="doctor" value="3"
                                                        v-model='selectPatient' required>
                                                        <label for="doctor3">
                                                            <div class="cu-input cu-checkbox normal">
                                                                <div class="contain">
                                                                    <span class="fa fa-check"></span>
                                                                    <div>John Doe</div>
                                                                </div>
                                                            </div>
                                                        </label>
                                                    </div>
                                                </div>
                                                <div class="col-lg-3 col-md-6">
                                                    <div class="cu-field">
                                                        <input id="doctor3" type="checkbox" name="doctor" value="3"
                                                        v-model='selectPatient' required>
                                                        <label for="doctor3">
                                                            <div class="cu-input cu-checkbox normal">
                                                                <div class="contain">
                                                                    <span class="fa fa-check"></span>
                                                                    <div>John Doe</div>
                                                                </div>
                                                            </div>
                                                        </label>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-6"> 
                                        <div class="cu-field">
                                            <h3 class="cu-label">
                                                <label>Assistant Doctor : <small>Please enter the doctor code</small></label>
                                            </h3>
                                            <div class="cu-input text-box">
                                                <span class="fa fa-edit"></span>
                                                <input type="number" required>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <div class="text-center">
                                    <button class='special-btn'>Submit</button>
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
    name:'or_patient',
    props: ['link'],
    data() {
        return {
            apiUrl: `http://localhost:${this.link}/endoresment/dist/`,
            selectPatient: '',
            Speciality: ''
        }
    }
}
</script>

<style scoped>
.or_patient_edit {
    background-color:#f6f8fb;
    background-attachment: fixed;
    background-repeat: no-repeat;
    background-size:cover;
    background-position: center;
    text-align: center;
    position: relative;
    overflow: hidden;
    min-height:100vh;
}
.or_patient_edit::before {
    content:'';
    position: absolute;
    left:0%;
    top: 0px;
    display: block;
    width: 200px;
    height: 100%;
    background:url('../../assets/layout/img/left.png') left no-repeat fixed;
    background-size:contain;
}
.or_patient_edit::after {
    content:'';
    position: absolute;
    right:0%;
    top: 0px;
    display: block;
    width: 200px;
    height: 100%;
    background:url('../../assets/layout/img/right.png') right no-repeat fixed;
    background-size:contain;
}
.custom-form {
    margin: 15px;
    background-color: transparent;
    border:none;
    text-align: initial;
}
.custom-form .cu-form-group.special {
    margin: 60px auto;
    max-width:1100px;
    box-shadow: 0 0 50px #eee;
    padding:30px 30px 60px 30px;
}
.custom-form .cu-form-group .title {
    top: -60px;
}
.custom-form .cu-form-group .title span {
    height: 40px;
    line-height: 38px;
    color: #000;
    background-color:#fff;
    box-shadow: 0 0 5px #eee;
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