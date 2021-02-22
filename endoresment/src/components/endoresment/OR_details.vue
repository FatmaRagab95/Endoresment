<template>
    <div class="OR_details">
        <div class="container">
            <div class='content card mt-5'>
                <h3 class="title p-3 cu-flex align-items-center">
                    <span>Heart Transplant</span>
                    <a class='btn btn-sm btn-success text-white'><i class='fa fa-edit'></i> Edit</a>
                </h3>
            </div>

            <div class="card content mt-5 mb-5 p-3">
                <div class="container-fluid">
                    <div class='row'>
                        <div class="col-md-6">
                            <div class="info">
                                <h5><span> <i class='fa fa-user-md'></i> Consultant Doctor:</span> <small>Mohamed Ahmed</small></h5>
                            </div>
                            <div class="info">
                                <h5><span> <i class='fa fa-user'></i> Patient Full Name:</span> <small>Adel Shereef</small></h5>
                            </div>
                            <div class="info">
                                <h5><span> <i class='fa fa-heartbeat'></i> Patient Medical Number:</span> <small>1425</small></h5>
                            </div>
                            <div class="info">
                                <h5><span> <i class='fa fa-plus-square'></i> Speciality:</span> <small>Lorem Ipsum</small></h5>
                            </div>
                            <div class="info">
                                <h5><span> <i class='fa fa-stethoscope '></i> Assistant Doctor:</span> <small>Ahmed Saied</small></h5>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="info">
                                <h5><span> <i class='fa fa-calendar-o'></i> Date:</span> <small>19-2-2021</small></h5>
                            </div>
                            <div class="info">
                                <h5><span> <i class='fa fa-clock-o'></i> From:</span> <small>02:00 PM</small></h5>
                            </div>
                            <div class="info">
                                <h5><span> <i class='fa fa-clock-o'></i> To:</span> <small>03:00 PM</small></h5>
                            </div>
                            <div class="info">
                                <h5><span> <i class='fa fa-hospital-o'></i> OR Room:</span> <small>3</small></h5>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div class='content mb-5 bg-white shadow table-responsive'>
                <table class="table table-hover">
                    <thead>
                        <th scope="col">#</th>
                        <th scope="col">Participating Doctors</th>
                        <th scope="col">Spicaility</th>
                    </thead>
                    <tbody>
                        <tr>
                            <th scope="row">1</th>
                            <td>Mohamed Ahmed</td>
                            <td>Chardiology</td>
                        </tr>
                        <tr>
                            <th scope="row">2</th>
                            <td>Ahmed Saied</td>
                            <td>Anesthesia </td>
                        </tr>
                    </tbody>
                </table>
            </div>
        </div>
    </div>
</template>

<script>
export default {
    name:'OR_details',
    data() {
        name: {
            
        }
    }
}
</script>

<style scoped>
.OR_details {
    background-color:#f6f8fb;
    position: relative;
    overflow: hidden;
    min-height:100vh;
}
.OR_details::before {
    content:'';
    position: absolute;
    left:0%;
    top: 0px;
    display: block;
    width: 200px;
    height: 100%;
    background:url('../../assets/layout/img/left.png') left no-repeat ;
    background-size:100% 100%;
}
.OR_details::after {
    content:'';
    position: absolute;
    right:0%;
    top: 0px;
    display: block;
    width: 200px;
    height: 100%;
    background:url('../../assets/layout/img/right.png') right no-repeat;
    background-size:100% 100%;
}
.content {
    position:relative;
    z-index:1;
}
.content .col-md-6:first-of-type {
    border-right:1px solid #c8cbd0;
}
.content .info {
    border-bottom:1px solid #eee;
}
.content .info:last-of-type {
    border-bottom:none;
}
.content .info h5 {
    display:flex;
    justify-content: space-between;
    flex-wrap: wrap;
    justify-items: center;
    font-size:18px;
    flex-wrap: wrap;
    padding-top:15px;
    align-items:center
}
.content .info h5 i {
    color:#ff7979;
    margin-right:5px;
}
.content .info .span {
    flex:1;
}
.content .info h5 small {
    margin:15px;
    font-size:16px;
    background-color:#f6f8fb;
    padding:10px 15px 10px 15px;
    border-radius:5px;
}

@media (max-width:767px) {
    .content .col-md-6:first-of-type {
        border-right:none;
    }
}
</style>