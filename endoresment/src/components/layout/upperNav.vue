<template>
  <header class="topbar">
    <nav class="navbar navbar-expand-lg navbar-light bg-light topbar bg-white">
      <router-link class="navbar-brand" :to="{ name: 'home' }">
        <img class="img-fluid" alt="logo" src="../../assets/layout/img/logo.png" />
      </router-link>
      <button
        class="navbar-toggler"
        type="button"
        data-toggle="collapse"
        data-target="#navbarSupportedContent"
        aria-controls="navbarSupportedContent"
        aria-expanded="false"
        aria-label="Toggle navigation"
      >
        <span class="navbar-toggler-icon"></span>
      </button>
      <div class="collapse navbar-collapse" id="navbarSupportedContent">
        <ul class="navbar-nav mr-auto">
          <!--<li class="nav-item dropdown">
                        <router-link class="nav-link" :to="{ name: 'home' }">
                            <i class="fa fa-home"></i>
                            <span class="link-text">Dashboard</span>
                        </router-link>
                    </li>-->
          <li class="nav-item dropdown">
            <a
              class="nav-link dropdown-toggle"
              @click.prevent
              :class="currentPage == 'Endoresment' ? '' : ''"
              id="navbarDropdown"
              role="button"
              data-toggle="dropdown"
              aria-haspopup="true"
              aria-expanded="false"
              href="#"
            >
              <i class="fa fa-hospital-o"></i>
              <span class="link-text">Endorsement</span>
            </a>
            <ul class="dropdown-menu shadow" aria-labelledby="navbarDropdown">
              <li
                class="border-bottom pt-1 pb-1 border-light"
                v-for="route in routes.filter(
                  (x) =>
                    x.meta.cat == 'Endoresment' && x.meta.viewMenu && x.meta.authCheck
                )"
                :key="route.path"
              >
                <router-link class="dropdown-item" :to="route.path">
                  <span class="link-text">{{ route.name }}</span>
                </router-link>
              </li>
            </ul>
          </li>
        </ul>
        <ul class="list-unstyled menu pull-right align-items-center">
          <!-- <li class="main-menu">
            <i class="fa fa-bell-o"></i><span class="num">4</span>
            <ul class="list-unstyled sub-menu">
                <li>
                <a class="notfi" href="#">
                  <div class="row align-items-center">
                    <div class="col-12">
                      <div class="info">
                        <span class="time">3 min ago</span>
                        <h3>Fatma Ragab</h3>
                        <p>
                          lorem Ipsum lorem Ipsum lorem Ipsum lorem Ipsum lorem Ipsum
                          lorem Ipsum lorem Ipsum lorem Ipsum
                        </p>
                      </div>
                    </div>
                  </div>
                </a>
              </li>
              <li>
                <a class="notfi" href="#">
                  <div class="row align-items-center">
                    <div class="col-12">
                      <div class="info">
                        <span class="time">3 min ago</span>
                        <h3>Fatma Ragab</h3>
                        <p>
                          lorem Ipsum lorem Ipsum lorem Ipsum lorem Ipsum lorem Ipsum
                          lorem Ipsum lorem Ipsum lorem Ipsum
                        </p>
                      </div>
                    </div>
                  </div>
                </a>
              </li>
              <li>
                <a class="notfi" href="#">
                  <div class="row align-items-center">
                    <div class="col-12">
                      <div class="info">
                        <span class="time">3 min ago</span>
                        <h3>Fatma Ragab</h3>
                        <p>
                          lorem Ipsum lorem Ipsum lorem Ipsum lorem Ipsum lorem Ipsum
                          lorem Ipsum lorem Ipsum lorem Ipsum
                        </p>
                      </div>
                    </div>
                  </div>
                </a>
              </li>
              <li class="text-center"><a class="msg" href="#">View All</a></li>
            </ul>
          </li>
          <li class="main-menu">
            <i class="fa fa-envelope-o"></i><span class="num">13</span>
            <ul class="list-unstyled sub-menu">
              <li>
                <a class="msg" href="#">
                  <div class="row align-items-center">
                    <div class="col-3">
                      <img
                        class="rounded-circle img-fluid"
                        src="../../assets/layout/img/user.jpg"
                      />
                    </div>
                    <div class="col-9">
                      <div class="info">
                        <span class="time">3 min ago</span>
                        <h3>Fatma Ragab</h3>
                        <p>
                          lorem Ipsum lorem Ipsum lorem Ipsum lorem Ipsum lorem Ipsum
                          lorem Ipsum lorem Ipsum lorem Ipsum
                        </p>
                      </div>
                    </div>
                  </div>
                </a>
              </li>
              <li>
                <a class="msg" href="#">
                  <div class="row align-items-center">
                    <div class="col-3">
                      <img
                        class="rounded-circle img-fluid"
                        src="../../assets/layout/img/user.jpg"
                      />
                    </div>
                    <div class="col-9">
                      <div class="info">
                        <span class="time">3 min ago</span>
                        <h3>Fatma Ragab</h3>
                        <p>
                          lorem Ipsum lorem Ipsum lorem Ipsum lorem Ipsum lorem Ipsum
                          lorem Ipsum lorem Ipsum lorem Ipsum
                        </p>
                      </div>
                    </div>
                  </div>
                </a>
              </li>
              <li>
                <a class="msg" href="#">
                  <div class="row align-items-center">
                    <div class="col-3">
                      <img
                        class="rounded-circle img-fluid"
                        src="../../assets/layout/img/user.jpg"
                      />
                    </div>
                    <div class="col-9">
                      <div class="info">
                        <span class="time">3 min ago</span>
                        <h3>Fatma Ragab</h3>
                        <p>
                          lorem Ipsum lorem Ipsum lorem Ipsum lorem Ipsum lorem Ipsum
                          lorem Ipsum lorem Ipsum lorem Ipsum
                        </p>
                      </div>
                    </div>
                  </div>
                </a>
              </li>
              <li class="text-center"><a class="msg" href="#">View All</a></li>
            </ul>
          </li>-->
          <li class="main-menu">
            <div class="row align-items-center">
              <div class="profile">
                <img
                  class="rounded-circle img-fluid"
                  src="../../assets/layout/img/user.jpg"
                />
              </div>
              <div class="p-0 userName">
                <span>{{ username }}</span>
              </div>
            </div>
            <ul class="list-unstyled sub-menu profile-menu">
              <li>
                <router-link :to="{ name: 'Profile' }">
                  <i class="fa fa-user-circle"></i><span>Profile</span>
                </router-link>
              </li>
              <!-- <li>
                <a href="#"><i class="fa fa-gear"></i><span>Settings</span></a>
              </li>-->
              <li>
                <input type="submit" @click.prevent="Button_LogOut" value="Log Out" />
              </li>
            </ul>
          </li>
        </ul>
      </div>
    </nav>
    <div class="clearfix"></div>
  </header>
</template>

<script>
export default {
  name: "upperNav",
  props: ["username"],
  data() {
    return {
      routes: this.$router.options.routes,
      currentPage: this.$route.meta.cat,
    };
  },
  methods: {
    Button_LogOut() {
      localStorage.removeItem("user");
      this.$router.push({ name: "login" });
    },
  },
};
</script>

<style>
.page-container {
  padding-left: 0px !important;
}
.topbar {
  left: 0px !important;
  width: 100% !important;
}
.topbar .navbar-brand img {
  max-width: 120px;
}
.topbar .menu .main-menu {
  max-width: 240px;
}
.userName {
  max-width: 120px;
  text-overflow: ellipsis;
  overflow: hidden;
  word-break: break-word;
}
</style>
