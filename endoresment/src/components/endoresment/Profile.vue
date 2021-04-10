<template>
    <div class="security">
		<div class="container-fluid">

			<section class='profile-box pt-3'>
				<div class='custom-form header-box'>
					<div class='cu-container'>
						<div class='row align-items-center'>
							<div class='col-lg-3 text-center'>
								<img class="img-responsive center-block rounded-circle img-thumbnail" 
                                src="../../assets/layout/img/user.jpg" alt="">
							</div>
							<div class='col-lg-9'>
								<ul class='list-unstyled cu-flex'>
									<li class="col-lg-6"><i class='fa fa-user'></i>Name:
										<span>{{user.FullName}}</span>
									</li>
									<li class="col-lg-6"><i class='fa fa-user'></i>User Name:
										<span>{{user.user_name}}</span>
									</li>
									<li class="col-lg-6"><i class='fa fa-briefcase'></i>Title: 
										<span>{{user.Title}}</span>
									</li>
									<li class="col-lg-6"><i class='fa fa-envelope'></i>Email:
										<span>{{user.Email}}</span>
									</li>
									<li class="col-lg-6"><i class='fa fa-usb'></i>Branch:
										<span>{{user.Branch_name}}</span>
									</li>
									<li class="col-lg-6"><i class='fa fa-usb'></i>Department:
										<span>{{user.Dept_name}}</span>
									</li>
									<li class="col-lg-6"><i class='fa fa-usb'></i>Section:
										<!-- <span><%=Session["Section"]%></span> -->
									</li>
									<li class="col-lg-6"><i class='fa fa-phone'></i>Mobile 1:
										<span>{{user.Mobile_1}}</span>
									</li>
									<li class="col-lg-6"><i class='fa fa-phone'></i>Mobile 2:
										<span>{{user.Mobile_2}}</span>
									</li>
									<li class="col-lg-6"><i class='fa fa-calendar-plus-o'></i>Hiring Date:
										<span>{{user.Hiring_Date}}</span>
									</li>
								</ul>
							</div>
						</div>
					</div>
				</div>
			</section>


			<div class="view-account custom-form" id='Profile'>

				<div class='custom-form header-box'>
					<div class='cu-container'>
						<ul class="nav list-unstyled cu-flex text-center">
							<li class='shadow'>
								<a href="#profile" data-toggle="tab" class="nav-item nav-link has-icon nav-link-faded active">
									<span class="fa fa-user"></span>  Profile Information
								</a>
							</li>
							<li class='shadow'>
								<a href="#security" data-toggle="tab" class="nav-item nav-link has-icon nav-link-faded">
									<span class="fa fa-cog"></span>  Change Password
								</a>
							</li>

						</ul>
					</div>
				</div>

				<section class="module">
					<div class="module-inner">	
						<div class="content-panel">
							<div id="profile" class="tab-pane active">
								<h3 class="fieldset-title"><i class='mr-1 fa fa-edit'></i>Personal Info</h3>
								<fieldset class="fieldset cu-form-group special shadow">
									<div class="form-group avatar hidden">
										<figure class="figure col-md-2 col-sm-3 col-xs-12">
											<img class="img-rounded img-responsive" 
                                            src="../../assets/layout/img/user.jpg" alt="">
										</figure>
										<div class="form-inline col-md-10 col-sm-9 col-xs-12">
											<input type="file" class="file-uploader pull-left">
											<button type="submit" class="btn btn-sm btn-default-alt pull-left">Update Image</button>
										</div>
									</div>
									<div class="form-group">
										<label class="control-label">Full Name</label>
										<div class="input-group mb-2">
											<div class="input-group-prepend">
												<div class="input-group-text"><i class='fa fa-user'></i></div>
											</div>
											<input id='FullName' type="text" class="form-control" 
												   placeholder="Full Name" :value="user.FullName">
										</div>
									</div>
									<div class="form-group">
										<label class="control-label">Email</label>
										<div class="input-group mb-2">
											<div class="input-group-prepend">
												<div class="input-group-text">@</div>
											</div>
											<input id='Email' type="email" class="form-control" 
												   placeholder="Email" :value="user.Email">
										</div>
									</div>
									<div class="form-group">
										<label class="control-label">Mobile 1</label>
										<div class="input-group mb-2">
											<div class="input-group-prepend">
												<div class="input-group-text"><i class='fa fa-phone'></i></div>
											</div>
											<input id='Mobile_1' type="number" class="form-control" 
												   placeholder="Mobile 1" :value="user.Mobile_1">
										</div>
									</div>
									<div class="form-group">
										<label class="control-label">Mobile 2</label>
										<div class="input-group mb-2">
											<div class="input-group-prepend">
												<div class="input-group-text"><i class='fa fa-phone'></i></div>
											</div>
											<input id='Mobile_2' type="number" class="form-control" 
												   placeholder="Mobile 2" :value="user.Mobile_2">
										</div>
									</div>

								</fieldset>
								<div class="form-group text-center">
									<button class='special-btn' @click.prevent='updateInfo()'>Update</button>
								</div>
							</div>

							<div id="security" class="tab-pane">
								<h3 class="fieldset-title"><i class='mr-1 fa fa-edit'></i>Change Password</h3>
								<fieldset class="fieldset cu-form-group special shadow">
									<div class="form-group">

										<label class="control-label">Your Current Password</label>
										<div class="input-group mb-2">
											<div class="input-group-prepend">
												<div class="input-group-text"><i class='fa fa-shield'></i></div>
											</div>
											<input class="form-control" ID="txt_cpassword" type="Password" v-model='oldPass'>
										</div>
									</div>
									<div class="form-group">
										<label class="control-label">Your New Password:</label>
										<div class="input-group mb-2">
											<div class="input-group-prepend">
												<div class="input-group-text"><i class='fa fa-shield'></i></div>
											</div>
											<input class="form-control" ID="txt_cpassword" type="Password" v-model='newPass'>
										</div>
									</div>
									<div class="form-group">
										<label class="control-label">Confirm Password:</label>
										<div class="input-group mb-2">
											<div class="input-group-prepend">
												<div class="input-group-text"><i class='fa fa-shield'></i></div>
											</div>
											<input class="form-control" ID="txt_cpassword" type="Password" v-model='confirm'>
										</div>
									</div>

								</fieldset>

								<div class="form-group text-center">
									<button class='special-btn' @click.prevent='changePass'>Update Passowrd</button>
								</div>
							</div>
						</div>
					</div>
				</section>
			</div>
		</div>

	</div>
</template>

<script>
export default {
    name:'OR',
    props: ['link', "user"],
    data() {
        return {
            apiUrl: this.link,
            oldPass: "",
			newPass: "",
			confirm: ""
        }
    },
    methods: {
        updateInfo: function () {
            let that = this;

            let info = {
                Emp_id: that.user.Emp_id,
                FullName: document.getElementById("FullName").value,
                Email:    document.getElementById("Email").value,
                Mobile_1: document.getElementById("Mobile_1").value,
                Mobile_2: document.getElementById("Mobile_2").value
            };
            
            if (info.FullName.length < 5 && !parseInt(info.FullName)) {
                swal({
                    title: "Error!",
                    text: "Sorry you have to fill the Full Name field with at least 5 characters!", icon: "warning", dangerMode: !0 
                });
            } else {
                $.ajax({
                    type: "POST",
                    url: that.apiUrl + "/forgot_password.aspx/ChangeInfo",
                    data: JSON.stringify({ info: info }),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    beforSend: function () {
                        swal("Please Wait!");
                    },
                    success: function () {
                        swal("You have successfully updated your information!");
                        that.user.FullName = info.FullName;
                        that.user.Email = info.Email;
                        that.user.Mobile_1 = info.Mobile_1;
                        that.user.Mobile_2 = info.Mobile_2;
                    },
                    error: function () {
                        swal({
                            title: "Error!",
                            text: "Something went wrong please try again later!", icon: "warning", dangerMode: !0 
                        });
                    }
                });
            }
        },
        changePass: function () {
            let a = this;
            a.newPass == a.confirm
                ? a.newPass.length >= 6
                ? $.ajax({
                type: "POST",
                url: a.apiUrl + "/forgot_password.aspx/ChangePass",
                data: JSON.stringify({ pass: { pass_word: a.newPass, oldPass: a.oldPass, Emp_id: a.user.Emp_id } }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (a) {
                    "error" != a.d ? swal("Your password has been changed!") : swal({ title: "Error!", text: "Sorry the current password is incorrect!", icon: "warning", dangerMode: !0 });
                },
            })
            : swal({ title: "Error!", text: "Sorry the password have to contain at least 6 digits or characters!", icon: "warning", dangerMode: !0 })
            : swal({ title: "Error!", text: "Sorry the password doesn't match!", icon: "warning", dangerMode: !0 });
        },
    },
    created: function () {
    },
}
</script>

<style scoped>
    a:hover, a:focus{
        text-decoration: none !important;
        outline: none !important;
    }
    .header-box {
        background: linear-gradient(45deg,#fff 50%,#f8f9fa 50%);
        position:relative;
        overflow:hidden;
    }
    .header-box::after {
        content: "";
        position: absolute;
        width: 500px;
        height: 500px;
        border-radius: 50%;
        top: 0;
        left: 0;
        background-color: #f8f9fa;
        z-index: 1;
    }
    .header-box .cu-container {
        position:relative;
        z-index:2;
    }
    .header-box img {
        max-width:140px;
    }
    .header-box ul li {
        background: linear-gradient(rgba(255,255,255,.5),rgba(255,255,255,.5));
        padding: 2px;
        border-radius: 3px;
        border: 1px solid #ddd;
        margin: 2px 0;
        max-width: 350px;
        font-size: 16px;
        color: #046132;
    }
    .header-box ul li i {
        color: #aaa;
        padding: 5px;
    }
    .header-box ul li span {
        background-color: #fff;
        display: inline-block;
        padding: 2px 5px;
        margin: 0 5px;
        color: #000;
    }
    #Profile.view-account .content-panel {
        margin-left:0;
    }

    .panel-group .panel{
        background-color: #fff;
        border:none;
        box-shadow:none;
        border-radius: 10px;
        margin-bottom:11px;
    }
    .panel .panel-heading{
        padding: 0;
        border-radius:10px;
        border: none;
    }
    .panel-heading a{
        color:#816d6d !important;
        display: block;
        border:none;
        padding:20px 35px 20px;
        font-size: 20px;
        background-color:#fff;
        font-weight:600;
        position: relative;
        color:#fff;
        box-shadow:none;
        transition:all 0.1s ease 0;
    }
    .panel-heading a:after, .panel-heading a.collapsed:after{
        content: "\f068";
        font-family: fontawesome;
        text-align: center;
        position: absolute;
        left:-20px;
        top: 10px;
        color:#816d6d !important;
        background-color:#fff;
        border: 5px solid #fff;
        font-size: 15px;
        width: 40px;
        height:40px;
        line-height: 30px;
        border-radius: 50%;
        transition:all 0.3s ease 0s;
    }
    .panel-heading:hover a:after,
    .panel-heading:hover a.collapsed:after{
        transform:rotate(360deg);
    }
    .panel-heading a.collapsed:after{
        content: "\f067";
    }
    #accordion .panel-body{
        background-color:#fbf8f8;
        color:#8C8C8C;
        line-height: 25px;
        padding: 10px 25px 20px 35px ;
        border-top:none;
        font-size:14px;
        position: relative;
    }
    .text-white{
        color:white;
        text-transform: uppercase;
    }

    .loader {
        display: inline-block;
        width: 30px;
        height: 30px;
        position: relative;
        border: 4px solid #Fff;
        top: 50%;
        animation: loader 2s infinite ease;
    }

    .loader-inner {
        vertical-align: top;
        display: inline-block;
        width: 100%;
        background-color: #fff;
        animation: loader-inner 2s infinite ease-in;
    }

    @keyframes loader {
        0% {
            transform: rotate(0deg);
        }

        25% {
            transform: rotate(180deg);
        }

        50% {
            transform: rotate(180deg);
        }

        75% {
            transform: rotate(360deg);
        }

        100% {
            transform: rotate(360deg);
        }
    }

    @keyframes loader-inner {
        0% {
            height: 0%;
        }

        25% {
            height: 0%;
        }

        50% {
            height: 100%;
        }

        75% {
            height: 100%;
        }

        100% {
            height: 0%;
        }
    }
</style>