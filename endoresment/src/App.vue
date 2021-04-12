<style src="./assets/layout/css/bootstrap-material-datetimepicker.css"></style>
<style src="./assets/layout/css/bootstrap.min.css"></style>
<style src="./assets/layout/css/jQuery-UI.css"></style>
<style src="./assets/layout/css/jquery-ui.multidatespicker.css"></style>
<style src="./assets/layout/css/material-icons.css"></style>
<style src="./assets/layout/css/MaterialDesignIcons.css"></style>
<style src="./assets/layout/css/nice-select.css"></style>
<style src="./assets/layout/css/slick-theme.css"></style>
<style src="./assets/layout/css/slick.css"></style>
<style src="./assets/layout/css/main.css"></style>

<template>
  <v-app>
    <div :class="user ? 'page-container' : ''">
      <div :class="user ? 'main-contnet' : ''">
        <upper-nav v-if="user" :username="user.FullName"></upper-nav>

        <!-- show alerts for charge nurses to confirm endorsing -->
        <div v-if="Units.length > 0 && edits && user">
          <div v-if="user.Role_id == 17">
            <div v-for="(data, i) in Units" :key="data.id">
              <ul class="list-unstyled alert-charge" v-if="!data.Confirm">
                <li class="alert bg-danger text-white p-4">
                  <i class="text-warning fa fa-warning"></i>
                  <span
                    >You haven't confirmed endorsing for
                    <strong class="border-bottom">{{ data.Unit_name }}</strong> yet!</span
                  >
                  <button
                    @click.prevent="confirm(data.id, i)"
                    class="pull-right btn btn-warning text-dark"
                  >
                    Confirm
                  </button>
                </li>
              </ul>
            </div>
          </div>
        </div>

        <router-view
          :link="link"
          :user="user"
          :UnitDash="Units"
          :edits="edits"
          :NursesPatients="NursesPatients"
          :DoctorPatients="DoctorPatients"
          :DoctorData="DoctorData"
          :Doctors="Doctors"
        />
      </div>
    </div>
  </v-app>
</template>

<script>
import "./assets/layout/js/bootstrap-material-datetimepicker.js";
import "./assets/layout/js/jquery-nice-select.js";
import "./assets/layout/js/main.js";

import upperNav from "@/components/layout/upperNav";
import sideBar from "@/components/layout/sideBar";

export default {
  name: "App",
  components: { upperNav, sideBar },
  data() {
    return {
      user: JSON.parse(localStorage.getItem("user")),
      link: "",
      Units: [],
      NursesPatients: [], // if the user is a nurse
      DoctorPatients: [], // if the user is a doctor
      DoctorData: null, // if the user is a doctor,
      Doctors: [], // if the user is a doctor,
      edits: false,
    };
  },
  methods: {
    // if user is charge nurse
    getUnits() {
      let that = this;

      if (this.user.Role_id == 17) {
        $.ajax({
          type: "POST",
          url: that.link + "endoresment/handover.aspx/getUnitDashData",
          data: JSON.stringify({ chargeNurse: that.user }),
          contentType: "application/json; charset=utf-8",
          dataType: "json",
          success: function (data) {
            if (new Date().getHours() < 20 && new Date().getHours() >= 8) {
              that.Units = JSON.parse(data.d).filter((x) => x.Shift.trim() == "Day");
            } else {
              that.Units = JSON.parse(data.d).filter((x) => {
                let checkDate =
                  x.Shift_date.substr(3, 3) +
                  x.Shift_date.substr(0, 3) +
                  x.Shift_date.substr(6, 4);

                return moment(moment(checkDate).add(12) > moment(new Date()));
              });
            }
            if (that.Units.length > 0) {
              that.edits = true;
            }
          },
        });

        // if the user is a nurse
      } else if (this.user.Role_id == 12) {
        $.ajax({
          type: "POST",
          url: that.link + "endoresment/handover.aspx/getUnitDashData3",
          data: JSON.stringify({ id: that.user }),
          contentType: "application/json; charset=utf-8",
          dataType: "json",
          success: function (data) {
            if (data.d.length > 0) {
              that.edits = true;
              that.Units = JSON.parse(data.d);
              that.Units = that.Units.filter(
                (v, i, a) => a.findIndex((t) => t.Unit_id === v.Unit_id) === i
              );
            }
          },
        });

        // get the nurse patients
        $.ajax({
          type: "POST",
          url: that.link + "endoresment/patientsNurse.aspx/getPatientsData",
          contentType: "application/json; charset=utf-8",
          data: JSON.stringify({ id: that.user }),
          dataType: "json",
          success: function (data) {
            that.NursesPatients = JSON.parse(data.d);
          },
        });
      }
      // if the user is a doctor
      else if (this.user.Role_id == 10) {
        $.ajax({
          type: "POST",
          url: that.link + "endoresment/handover.aspx/getUnitDashData2",
          data: JSON.stringify({ id: that.user }),
          contentType: "application/json; charset=utf-8",
          dataType: "json",
          success: function (data) {
            if (data.d.length > 0) {
              that.edits = true;
              that.Units = JSON.parse(data.d);
            }
          },
        });

        // get doctor's data so we can get the patients depending on it
        $.ajax({
          type: "POST",
          url: that.link + "endoresment/viewPatients.aspx/getDoctorData",
          data: JSON.stringify({ data: { Dr_Code: that.user.Emp_id } }),
          contentType: "application/json; charset=utf-8",
          dataType: "json",
          success: function (data) {
            that.DoctorData = JSON.parse(data.d)[0];

            //get Patients
            if (that.DoctorData.Type == "Consultant") {
              $.ajax({
                type: "POST",
                url: that.link + "endoresment/viewPatients.aspx/getPatientsData",
                data: JSON.stringify({ data: { Dr_Code: that.user.Emp_id } }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                  that.DoctorPatients = JSON.parse(data.d);
                  if (that.DoctorPatients.length > 0) {
                    that.DoctorPatients.map((x) => that.$set(x, "edit", false));
                    for (let i = 0; i < that.DoctorPatients.length; i++) {
                      that.DoctorPatients[i].Room = that.DoctorPatients[i].Room.trim();
                    }
                  }
                },
              });
            } else {
              $.ajax({
                type: "POST",
                url: that.link + "endoresment/viewPatients.aspx/getPatientsData2",
                data: JSON.stringify({
                  data: {
                    Dr_Code: that.user.Emp_id,
                    Spcy_Description: that.DoctorData.Spcy_Description,
                  },
                }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                  that.DoctorPatients = JSON.parse(data.d);
                  if (that.DoctorPatients.length > 0) {
                    that.DoctorPatients.map((x) => that.$set(x, "edit", false));
                    for (let i = 0; i < that.DoctorPatients.length; i++) {
                      that.DoctorPatients[i].Room = that.DoctorPatients[i].Room.trim();
                    }
                  }
                },
              });

              // get doctors
              $.ajax({
                type: "POST",
                url: that.link + "endoresment/viewPatients.aspx/getDoctorsData",
                data: JSON.stringify({
                  data: { Spcy_Description: that.DoctorData.Spcy_Description },
                }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                  that.Doctors = JSON.parse(data.d);
                },
              });
            }
          },
        });
      }
      // if the user is anything else
      else {
        $.ajax({
          type: "POST",
          url: that.link + "endoresment/handover.aspx/getUnitDashData2",
          data: JSON.stringify({ id: that.user }),
          contentType: "application/json; charset=utf-8",
          dataType: "json",
          success: function (data) {
            if (data.d.length > 0) {
              that.edits = false;
              that.Units = JSON.parse(data.d);
            }
          },
        });
      }
    },

    // confirm endorsing
    confirm(id, index) {
      let that = this;
      $.ajax({
        type: "POST",
        url: that.link + "endoresment/handover.aspx/confirmEndorsing",
        data: JSON.stringify({ id: { id: id } }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
          swal({
            title: "Confirmed!",
            icon: "success",
            dangerMode: true,
          });

          that.Units[index].Confirm = true;
        },
      });
    },
  },
  watch: {
    $route: function (to, from) {
      if (JSON.stringify(this.user) != localStorage.getItem("user")) {
        this.user = JSON.parse(localStorage.getItem("user"));
        this.getUnits();
        let r = this.$router.options.routes;
        r.map((x) => {
          x.meta.authCheck = x.meta.auth == "all" || x.meta.auth == this.user.Role_id;
        });
        location.reload();
      }
    },
  },
  created() {
    this.getUnits();
  },
};
</script>

<style>
/* edit bootstrap classes */
.badge-primary,
.btn-primary,
.bg-primary,
.v-btn--active {
  background-color: #29b770 !important;
  border-color: #29b770 !important;
}
.btn-primary:hover {
  background-color: #009245 !important;
  border-color: #009245 !important;
}
.text-primary {
  color: #29b770 !important;
}
.badge-success,
.btn-success,
.bg-success {
  background-color: #29b770 !important;
}
.bg-grey {
  background-color: #d6ddda !important;
}
.text-grey {
  color: #d6ddda !important;
}
/* end edit bootstrap classes */

/* start global rules */
.main-contnet {
  margin-top: 60px;
}
.loader-container {
  background-color: #7da4d0;
  padding: 10px 25px;
  border-radius: 5px;
  box-shadow: 0 2px 5px 1px #c8c8c8;
  color: #fff;
  display: inline-block;
  word-spacing: 15px;
}
.loader-f {
  animation: loader 800ms step-end infinite;
  width: 40px;
  height: 40px;
  transform: rotate(0deg);
}
.loader-f path {
  fill: #4468d6;
}
.not-found {
  max-width: 350px;
  margin: 60px auto;
  padding: 15px;
  color: #666;
}
.not-found i {
  font-size: 3em;
  display: block;
  margin: 20px auto;
}
.not-found button {
  max-width: 150px;
  margin: 30px auto;
}
.alert-charge {
  margin-top: 120px;
}
.modal-content {
  border: none;
  border-radius: 5px;
  box-shadow: 0 0 20px #68696c;
}

/* reset vuetify theme */
.theme--light.v-application {
  background: inherit;
  color: inherit;
}
.v-application {
  display: inherit;
  display: inherit;
  display: inherit;
}
.v-application {
  font-family: inherit;
  line-height: inherit;
}
.v-application,
.v-application .headline,
.v-application .title {
  line-height: inherit;
  font-family: "PTSansNarrow" !important;
}
.v-application .pt-5 {
  padding-top: 60px !important;
}
.v-application .pb-5 {
  padding-bottom: 60px !important;
}
.theme--light.v-label,
.theme--light.v-select .v-select__selections {
  color: inherit;
}
.v-application ol,
.v-application ul {
  padding-left: 0 !important;
}
button,
input,
select,
textarea {
  background-color: #fff;
}
.v-application--is-ltr .v-text-field .v-label {
  z-index: 1;
}
.v-label--active {
  font-size: 22px;
}
/* end reset vuetify theme */

@keyframes loader {
  12.5% {
    transform: rotate(45deg);
  }
  25% {
    transform: rotate(90deg);
  }
  37.5% {
    transform: rotate(135deg);
  }
  50% {
    transform: rotate(180deg);
  }
  62.5% {
    transform: rotate(225deg);
  }
  75% {
    transform: rotate(270deg);
  }
  87.5% {
    transform: rotate(315deg);
  }
  100% {
    transform: rotate(360deg);
  }
}
</style>
