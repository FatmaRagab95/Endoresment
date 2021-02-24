<template>
  <div class="sideBar collapsed" id="app_general">
    <i class="fa fa-times close-btn hidden"></i>
    <div class="app-logo text-center">
      <img class="img-fluid" alt="logo" src="../../assets/layout/img/logo.png" />
    </div>
    <div class="profile">
      <div class="container">
        <div class="row">
          <div class="col-4">
            <div class="img">
              <img class="rounded-circle" src="../../assets/layout/img/user.jpg" />
            </div>
          </div>
          <div class="col-8">
            <div class="info">
              <span>Welcome,</span>
              <h4>{{ username }}</h4>
            </div>
          </div>
        </div>
      </div>
    </div>

    <ul class="list-unstyled">
      <li>
        <router-link :to="{ name: 'home' }" class="sid-links">
          <i class="fa fa-home"></i>
          <span class="link-text">Dashboard</span>
        </router-link>
      </li>

      <li class="main-menu">
        <a class="sid-links" @click.prevent   :class='currentPage == "Endoresment" ? "for-a-active" : ""'>
          <span class="link-text">Endoresment</span>
          <span class="icon fa fa-chevron-down pull-right"></span>
        </a>
        <ul class="list-unstyled sub-menu">
          <li v-for="route in routes.filter(x=> x.meta.cat == 'Endoresment')" :key="route.path">
            <router-link :to="route.path">
              <span class="link-text">{{route.name}}</span>
            </router-link>
          </li>
        </ul>
      </li>
    </ul>
  </div>
</template>

<script>
$(".sideBar ul li.main-menu.open")
  .find("> ul")
  .slideToggle()
  .parent()
  .siblings()
  .find("ul")
  .slideUp();

$("body").on("click", ".sideBar ul li.main-menu", function () {
  $(this).siblings().removeClass("open");
  $(this).toggleClass("open");
  $(this).find("> ul").slideToggle().parent().siblings().find("ul").slideUp();
});

export default {
  name: "sideBar",
  props: ["username"],
  data() {
    return {
      routes: this.$router.options.routes,
      currentPage: this.$route.meta.cat
    }
  },
};
</script>

<style></style>
