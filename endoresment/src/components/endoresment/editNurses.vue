<template>
  <div class="editNurses p-3 text-capitalize">
    <div class="container-fluid mt-3 card">
      <div class="container-wave">
        <div class="wave"></div>
      </div>
      <div class="text-secondary card bg-light select-form p-4 text-left shadow">
        <h1>
          {{ Units.filter((x) => x.U_id == path)[0].U_name }}
        </h1>
        <hr />

        <div class="row">
          <div class="col-md-7">
            <h3 class="text-info" style="text-decoration: underline">
              nurses list in {{ Units.filter((x) => x.U_id == path)[0].U_name }}
            </h3>
            <div class="card overflow-auto p-3 shadow rounded" style="height: 500px">
              <div
                class="pt-2 pb-2 border-bottom"
                v-for="nurse in users"
                :key="nurse.Emp_id"
              >
                <span
                  ><input
                    style="width: 20px; height: 20px"
                    class="rounded bg-dark"
                    :id="nurse.Emp_id"
                    type="checkbox"
                    name="role"
                    :value="{ id: nurse.Emp_id, name: nurse.FullName }"
                    v-model="selectedNurse"
                /></span>
                <span class="ml-3"
                  ><label :for="nurse.Emp_id">{{ nurse.FullName }}</label></span
                >
                <span
                  class="pull-right"
                  v-if="
                    Endoresment_Nurses_Units.filter((x) => x.Nurse_id == nurse.Emp_id)
                      .length > 0
                  "
                  >registered in
                  {{
                    Units.filter(
                      (z) =>
                        z.U_id ==
                        Endoresment_Nurses_Units.filter(
                          (x) => x.Nurse_id == nurse.Emp_id
                        )[0].Unit_id
                    )[0].U_name
                  }}</span
                >
                <span class="pull-right" v-else>not registered in any unit</span>
              </div>
            </div>
          </div>
          <div class="col-md-5">
            <h3 class="text-info" style="text-decoration: underline">
              selected nurses in {{ Units.filter((x) => x.U_id == path)[0].U_name }}
            </h3>
            <div class="card overflow-auto p-3 shadow rounded" style="height: 500px">
              <div
                class="pt-2 pb-2 border-bottom"
                v-for="(nurse, i) in selectedNurse"
                :key="nurse.id"
              >
                <span>{{ i + 1 }}</span>
                <span class="ml-3">{{ nurse.name }}</span>
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
  name: "editNurses",
  props: ["link"],
  data() {
    return {
      Units: [],
      users: [],
      Endoresment_Nurses_Units: [],
      Nurses: [],
      path: "",
      selectedNurse: [],

      newUnitNurse: {
        Nurse_id: 0,
        Unit_id: 0,
        Entry_user: 0,
      },

      apiUrl: `http://localhost:${this.link}/endoresment/dist/`,
    };
  },
  methods: {
    OnSubmit: function () {
      let that = this;
      var ObjectD = Object.assign({}, this.newUnitNurse);

      swal({
        title: "Are you sure ...",
        icon: "warning",
        buttons: true,
        dangerMode: true,
      }).then((result) => {
        if (!result) {
          swal({
            title: "Canceled",
            text: "Sorry, the selection has been cancelled!",
          });
        } else {
          $.ajax({
            type: "POST",
            url: that.apiUrl + "endoresment/editNurses.aspx/getUnitsData",
            data: JSON.stringify({ course: ObjectD }),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function () {
              swal({
                title: "Sweet!",
                text: "You successfully added anew course ...",
                icon: "success",
                dangerMode: true,
              });
              window.location = "admin.aspx";
            },
          });
        }
      });
    },
  },

  created() {
    let that = this;
    this.path = this.$router.history.current.path.split("/")[2];

    //get units
    $.ajax({
      type: "POST",
      url: that.apiUrl + "endoresment/editNurses.aspx/getUnitsData",
      contentType: "application/json; charset=utf-8",
      dataType: "json",
      success: function (data) {
        that.Units = JSON.parse(data.d);
      },
    });

    //get admin users
    $.ajax({
      type: "POST",
      url: that.apiUrl + "endoresment/editNurses.aspx/getadminusersData",
      contentType: "application/json; charset=utf-8",
      dataType: "json",
      success: function (data) {
        that.users = JSON.parse(data.d);
        //that.users = that.users.filter((z) => z.Branch_ID == 1);
      },
    });

    //get Endoresment Nurses Units
    $.ajax({
      type: "POST",
      url: that.apiUrl + "endoresment/Nurses.aspx/getEndoresment_Nurses_UnitsData",
      contentType: "application/json; charset=utf-8",
      dataType: "json",
      success: function (data) {
        that.Endoresment_Nurses_Units = JSON.parse(data.d);
        //that.nurses_units = that.Endoresment_Nurses_Units.fiter((x) => x.Unit_id == this.path);
      },
    });
  },
};
</script>

<style scoped>
.container-wave {
  position: absolute;
  background: #fff;
  height: 50vh;
  width: 100%;
  left: 0;
  top: 0;
}

.wave {
  position: absolute;
  height: 250px;
  width: 100%;
  background: #fff;
  bottom: 0;
}

.wave::before,
.wave::after {
  content: "";
  display: block;
  position: absolute;
  border-radius: 100% 50%;
}

.wave::before {
  width: 55%;
  height: 109%;
  background-color: #f0f0f0;
  right: -1.5%;
  top: 80%;
}
.wave::after {
  width: 55%;
  height: 100%;
  background-color: #fff;
  left: 0;
  top: 60%;
}
.select-form {
  left: 50%;
  margin-top: 1%;
  margin-bottom: 1%;
  width: 90%;
  transform: translateX(-50%);
  max-height: 90vh;
  overflow: auto;
}
</style>
