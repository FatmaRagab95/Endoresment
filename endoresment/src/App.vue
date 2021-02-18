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
        <router-view :link="link" />
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
      link: 59677,
    };
  },
  watch: {
    $route: function (to, from) {
      this.user = JSON.parse(localStorage.getItem("user"));
    },
  },
  created() {},
};
</script>

<style>
.main-contnet {
  margin-top: 60px;
}
.ui-datepicker-clear-month {
  position: absolute;
  top: 9px;
  right: 32px;
  height: 100%;
  line-height: 100%;
  display: inline;
  cursor: pointer;
  color: red !important;
}
.ui-state-active,
.ui-widget-content .ui-state-active {
  background: #666 !important;
  color: #fff !important;
}
.ui-datepicker {
  width: 35em !important;
  padding: 0.2em 0.2em 0;
  display: none;
}
.ui-datepicker th {
  padding: 0.4em 0.2em !important;
}
.ui-datepicker td a {
  text-align: center !important;
}
.ui-widget {
  font-family: inherit !important;
  font-size: 1em !important;
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
