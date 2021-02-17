<template>
    <div class="OR">
        <h1 class='mt-5 mb-5 bg-white p-3 pl-5 pr-5  cu-flex align-items-center'>
            <span><i class='fa fa-heartbeat text-danger'></i> OR</span>
            <router-link :to='{name:"Schedule Procedure"}' target='_blank' class='btn btn-primary text-white'>Schedule a procedure</router-link>
        </h1>
        <div class="container-fluid">
            <div class="content pt-3 pb-5">
                <h3 class='pl-3 pb-3 pt-3 mb-4 bg-white card'>Today's Procedure</h3>
                <div class='bg-white shadow table-responsive'>
                    <table class="table table-bordered">
                        <thead>
                            <tr>
                            <th scope="col">#</th>
                            <th scope="col">Precedure</th>
                            <th scope="col">Room Number</th>
                            <th scope="col">Doctors</th>
                            <th scope="col">Patient Name</th>
                            <th scope="col">From</th>
                            <th scope="col">To</th>
                            <th scope="col">Date</th>
                            </tr>
                        </thead>
                        <tbody>
                            <tr>
                                <th scope="row">1</th>
                                <td>Heart Transplant</td>
                                <td>2</td>
                                <td>
                                    <table class="table table-hover">
                                        <tbody>
                                            <tr>
                                                <th scope="row">1</th>
                                                <td>Mark</td>
                                                <td>Chardiology</td>
                                            </tr>
                                            <tr>
                                                <th scope="row">2</th>
                                                <td>Jack</td>
                                                <td>Anesthesia </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </td>
                                <td>John Doe</td>
                                <td>2:00 AM</td>
                                <td>3:00 AM</td>
                                <td>14/2/2021</td>
                                <td class='text-center'><a href='#' class='btn btn-primary btn-sm text-white'>Details</a></td>
                                <td class='text-center'><a class='btn btn-success btn-sm text-white'><i class='fa fa-edit'></i></a></td>
                            </tr>
                            <tr>
                                <th scope="row">1</th>
                                <td>Heart Transplant</td>
                                <td>2</td>
                                <td>
                                    <table class="table table-hover">
                                        <tbody>
                                            <tr>
                                                <th scope="row">1</th>
                                                <td>Mark</td>
                                                <td>Chardiology</td>
                                            </tr>
                                            <tr>
                                                <th scope="row">2</th>
                                                <td>Jack</td>
                                                <td>Anesthesia </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </td>
                                <td>John Doe</td>
                                <td>11:00 AM</td>
                                <td>12:00 PM</td>
                                <td>14/2/2021</td>
                                <td class='text-center'><a href='#' class='btn btn-primary btn-sm text-white'>Details</a></td>
                                <td class='text-center'><a class='btn btn-success btn-sm text-white'><i class='fa fa-edit'></i></a></td>
                            </tr>
                        </tbody>
                    </table>
                </div>
            </div>
            <div class="content pt-3 pb-5">
                <h3 class='pl-3 pb-3 pt-3 mb-4 bg-primary text-white card'>Scheduled Procedure</h3>
                <div class='bg-white shadow table-responsive'>
                    <table class="table table-bordered">
                        <thead>
                            <tr>
                            <th scope="col">#</th>
                            <th scope="col">Precedure</th>
                            <th scope="col">Room Number</th>
                            <th scope="col">Doctors</th>
                            <th scope="col">Patient Name</th>
                            <th scope="col">From</th>
                            <th scope="col">To</th>
                            <th scope="col">Date</th>
                            </tr>
                        </thead>
                        <tbody>
                            <tr>
                                <th scope="row">1</th>
                                <td>Heart Transplant</td>
                                <td>2</td>
                                <td>
                                    <table class="table table-hover">
                                        <tbody>
                                            <tr>
                                                <th scope="row">1</th>
                                                <td>Mark</td>
                                                <td>Chardiology</td>
                                            </tr>
                                            <tr>
                                                <th scope="row">2</th>
                                                <td>Jack</td>
                                                <td>Anesthesia </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </td>
                                <td>John Doe</td>
                                <td>2:00 AM</td>
                                <td>3:00 AM</td>
                                <td>14/2/2021</td>
                                <td class='text-center'><a href='#' class='btn btn-primary btn-sm text-white'>Details</a></td>
                                <td class='text-center'><a class='btn btn-success btn-sm text-white'><i class='fa fa-edit'></i></a></td>
                            </tr>
                            <tr>
                                <th scope="row">1</th>
                                <td>Heart Transplant</td>
                                <td>2</td>
                                <td>
                                    <table class="table table-hover">
                                        <tbody>
                                            <tr>
                                                <th scope="row">1</th>
                                                <td>Mark</td>
                                                <td>Chardiology</td>
                                            </tr>
                                            <tr>
                                                <th scope="row">2</th>
                                                <td>Jack</td>
                                                <td>Anesthesia </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </td>
                                <td>John Doe</td>
                                <td>11:00 AM</td>
                                <td>12:00 PM</td>
                                <td>14/2/2021</td>
                                <td class='text-center'><a href='#' class='btn btn-primary btn-sm text-white'>Details</a></td>
                                <td class='text-center'><a class='btn btn-success btn-sm text-white'><i class='fa fa-edit'></i></a></td>
                            </tr>
                        </tbody>
                    </table>
                </div>
            </div>
            
            <div class="content pt-3 pb-5">
                <h3 class='pl-3 pr-3 pt-1 pb-1 mb-4 bg-success text-white rounded cu-flex align-items-center'>
                    <span>Done Procedure</span>
                    <div class='cu-flex filterDone'>
                        <label>Filter :</label>
                        <select class="form-control form-control-sm">
                            <option value='1'>This Week</option>
                            <option value='2'>This Month</option>
                            <option value='2'>This Year</option>
                        </select>
                    </div>
                </h3>
                <div class='bg-white shadow table-responsive'>
                    <table class="table table-bordered">
                        <thead>
                            <tr>
                            <th scope="col">#</th>
                            <th scope="col">Precedure</th>
                            <th scope="col">Room Number</th>
                            <th scope="col">Doctors</th>
                            <th scope="col">Patient Name</th>
                            <th scope="col">From</th>
                            <th scope="col">To</th>
                            <th scope="col">Date</th>
                            </tr>
                        </thead>
                        <tbody>
                            <tr>
                                <th scope="row">1</th>
                                <td>Heart Transplant</td>
                                <td>2</td>
                                <td>
                                    <table class="table table-hover">
                                        <tbody>
                                            <tr>
                                                <th scope="row">1</th>
                                                <td>Mark</td>
                                                <td>Chardiology</td>
                                            </tr>
                                            <tr>
                                                <th scope="row">2</th>
                                                <td>Jack</td>
                                                <td>Anesthesia </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </td>
                                <td>John Doe</td>
                                <td>2:00 AM</td>
                                <td>3:00 AM</td>
                                <td>14/2/2021</td>
                                <td class='text-center'><a href='#' class='btn btn-primary btn-sm text-white'>Details</a></td>
                            </tr>
                            <tr>
                                <th scope="row">1</th>
                                <td>Heart Transplant</td>
                                <td>2</td>
                                <td>
                                    <table class="table table-hover">
                                        <tbody>
                                            <tr>
                                                <th scope="row">1</th>
                                                <td>Mark</td>
                                                <td>Chardiology</td>
                                            </tr>
                                            <tr>
                                                <th scope="row">2</th>
                                                <td>Jack</td>
                                                <td>Anesthesia </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </td>
                                <td>John Doe</td>
                                <td>11:00 AM</td>
                                <td>12:00 PM</td>
                                <td>14/2/2021</td>
                                <td class='text-center'><a href='#' class='btn btn-primary btn-sm text-white'>Details</a></td>
                            </tr>
                        </tbody>
                    </table>
                </div>
            </div>
        </div>
    </div>
</template>

<script>
export default {
    name:'OR',
    props: ['link'],
    data() {
        return {
            apiUrl: `http://localhost:${this.link}/endoresment/dist/`,
        }
    }
}
</script>

<style scoped>
.OR {
    background-color:#f6f8fb;
    background-attachment: fixed;
    background-repeat: no-repeat;
    background-size:cover;
    background-position: center;
    position: relative;
    overflow: hidden;
    min-height:100vh;
}
.OR .content {
    max-width:1200px;
    margin:60px auto;
}
.OR .content .table-responsive {
    overflow:auto;
    max-height:380px;
}
.OR .content .filterDone {
    font-size:16px;
    margin:10px 0;
}
</style>