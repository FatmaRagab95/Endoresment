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
  <div id="app">
    <div :class="user ? 'page-container' : ''">
      <div :class="user ? 'main-contnet' : ''">
        <upper-nav v-if="user" :username="user.FullName"></upper-nav>

        <!-- show alerts for charge nurses to confirm endorsing -->
        <div v-if='chargeNurseUnits.length > 0 && edits  && user'>
          <div v-for='(data, i) in chargeNurseUnits' :key='data.id'>
            <ul class='list-unstyled alert-charge'>
              <li v-if='!data.Confirm' class='alert bg-danger text-white p-4'>
                <i class='text-warning fa fa-warning'></i> <span>You haven't confirmed endorsing for 
                  <strong class='border-bottom'>{{data.Unit_name}}</strong> yet!</span>
                <button @click.prevent='confirm(data.id,i)' class='pull-right btn btn-warning text-dark'>
                  Confirm</button>
              </li>
            </ul>
          </div>
        </div>

        <router-view :link="link" :user="user" :UnitDash='chargeNurseUnits' :edits='edits' />
      </div>
    </div>
  </div>
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
      link: `http://localhost:${49638}/endoresment/dist/`,
      chargeNurseUnits: [],
      edits:false
    };
  },
  methods: {
    // if user is charge nurse
    getchargeNurseUnits() {
      if (this.user.Role_id == 17) {
        let that = this;
          $.ajax({
            type: "POST",
            url: that.link + "endoresment/handover.aspx/getUnitDashData",
            data:JSON.stringify({"chargeNurse": that.user}),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
              if (new Date().getHours() < 20 && new Date().getHours() >= 8) {
                that.chargeNurseUnits = JSON.parse(data.d).filter(x => x.Shift.trim() == 'Day');
              } else {
                that.chargeNurseUnits = JSON.parse(data.d).filter(x => x.Shift.trim() == 'Night');
              }
              if (that.chargeNurseUnits.length > 0) {
                that.edits = true;
              } else {
                  $.ajax({
                      type: "POST",
                      url: that.link + "endoresment/handover.aspx/getUnitDashData2",
                      data:JSON.stringify({"id": that.user}),
                      contentType: "application/json; charset=utf-8",
                      dataType: "json",
                      success: function (data) {
                          if (data.d.length > 0) {
                              that.edits = false;
                              that.chargeNurseUnits = JSON.parse(data.d);
                          }
                      },
                  });
              }

            },
        });

        // if the user is not a charge nurse
      } else {
          $.ajax({
              type: "POST",
              url: that.link + "endoresment/handover.aspx/getUnitDashData2",
              data:JSON.stringify({"id": that.user}),
              contentType: "application/json; charset=utf-8",
              dataType: "json",
              success: function (data) {
                  if (data.d.length > 0) {
                      that.edits = false;
                      that.chargeNurseUnits = JSON.parse(data.d);
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
            data:JSON.stringify({"id": {"id": id}}),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                swal({
                  title: "Confirmed!",
                  icon: "success",
                  dangerMode: true,
                });

                that.chargeNurseUnits[index].Confirm = true;
            }
        });
    }
  },
  watch: {
    $route: function (to, from) {
      this.user = JSON.parse(localStorage.getItem("user"));
    },
    user: function () {
      this.getchargeNurseUnits()
    }
  },
  created() {
    this.getchargeNurseUnits()
  },
};
</script>

<style>
[v-cloak] {
  display: none;
}
.main-contnet {
  margin-top: 60px;
}
.badge-success {
  background-color: #29b770;
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
  margin-top:120px;
}
.modal-content {
    border: none;
    border-radius: 5px;
    box-shadow: 0 0 20px #68696c;
}
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
