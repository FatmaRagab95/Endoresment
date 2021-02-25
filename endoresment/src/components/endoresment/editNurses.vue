<template>
  <div class="editNurses p-3 text-capitalize">
    <div class="container-fluid mt-3 card">
      <div class="container-wave">
        <div class="wave"></div>
      </div>
      <div class="text-secondary card bg-light select-form p-4 text-left shadow">
        <div class="row">
          <span class="col-md-6"
            ><h1>
              {{ Units.filter((x) => x.U_id == path)[0].U_name }}
            </h1>
          </span>
          <span class="col-md-6">
            <button
              class="btn btn-secondary shadow-sm pull-right"
              @click.prevent="OnSubmit"
            >
              submit selection
            </button>
          </span>
          <span class="col-md-6">
            <form class="form-inline">
              <input
                class="form-control mr-sm-2"
                style="width: 85%"
                type="text"
                placeholder="type a nurse name"
                v-model="selectedName"
                v-on:keyup="namesList"
                v-on:keydown.enter.prevent
              />
              <span
                class="bg-secondary text-white pt-1 pb-1 rounded mr-2 text-center"
                style="width: 10%"
                ><i class="fa fa-search" aria-hidden="true"></i
              ></span>
            </form>
          </span>
          <span class="col-md-6 row">
            <span class="col-md-3 text-right p-0 m-0" style="line-height: 2.1"
              >updated date</span
            >
            <span class="col-md-9"
              ><input
                class="form-control"
                type="date"
                v-model="updateDate"
                :min="new Date().toISOString().substring(0, 10)"
              />
            </span>
          </span>
        </div>

        <hr />

        <div class="row">
          <div class="col-md-7">
            <h3 class="text-info" style="text-decoration: underline">
              nurses list in {{ Units.filter((x) => x.U_id == path)[0].U_name }}
            </h3>
            <div class="card overflow-auto p-3 shadow rounded" style="height: 500px">
              <div
                class="pt-2 pb-2 border-bottom"
                v-for="nurse in filterNames(users, selectedName.toLowerCase())"
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
                    Endoresment_Nurses_Units.filter(
                      (x) =>
                        x.Nurse_id == nurse.Emp_id &&
                        x.Unit_id == path &&
                        x.Active == true
                    ).length > 0
                  "
                  >registered in ..
                  <span
                    v-for="unit in Endoresment_Nurses_Units.filter(
                      (x) =>
                        x.Nurse_id == nurse.Emp_id &&
                        x.Unit_id == path &&
                        x.Active == true
                    )"
                    :key="unit.Unit_id"
                    >{{ Units.filter((x) => x.U_id == unit.Unit_id)[0].U_name }}..</span
                  >
                  {{
                }}</span>
                <span class="pull-right text-danger" v-else
                  >not registered in any unit</span
                >
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
      oldNerses: [],
      filterNurses: [],

      oldArray: [1, 2, 3, 4, 5, 6],
      newArray: [1, 2, 3, 4],
      filterArray: [],

      selectedName: "",
      Names: [],

      updateDate: "",

      newUnitNurse: {
        Nurse_id: 0,
        Unit_id: 0,
        Entry_user: 0,
        Active: true,
      },

      apiUrl: this.link,
    };
  },
  methods: {
    filterNames(list, value) {
      return list.filter((x) => x.FullName.toLowerCase().indexOf(value) > -1);
    },
    namesList(event) {
      if (event.key == "Enter") {
        this.Names.push(this.selectedName);
      }
    },
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
            text: "Sorry, the selection has been canceled!",
          });
        } else {
          for (let i = 0; i < this.users.length; i++) {
            if (this.selectedNurse.map((x) => x.id).includes(this.users[i].Emp_id)) {
              ObjectD.Nurse_id = this.users[i].Emp_id;
              ObjectD.Unit_id = this.path;
              ObjectD.Entry_user = JSON.parse(localStorage.getItem("user")).Emp_id;
              ObjectD.Active = true;

              $.ajax({
                type: "POST",
                url: that.apiUrl + "endoresment/editNurses.aspx/insertNurseSelection",
                data: JSON.stringify({ data: ObjectD }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function () {
                  if (i == that.users.length - 1) {
                    swal({
                      title: "Sweet!",
                      text: "You successfully made your selections ...",
                      icon: "success",
                      dangerMode: true,
                    });
                    location.reload();
                  }
                },
              });
            } else {
              if (this.updateDate == "") {
                swal({
                  title: "Error!",
                  text: "Sorry, you should select the update date ! ",
                  icon: "warning",
                  dangerMode: true,
                });
              } else {
                let updateNurse = {
                  id: this.Endoresment_Nurses_Units.filter(
                    (x) => x.Nurse_id == this.users[i].Emp_id && x.Unit_id == this.path
                  )[0].Id,
                  Nurse_id: this.users[i].Emp_id,
                  Unit_id: this.path,
                  Entry_user: JSON.parse(localStorage.getItem("user")).Emp_id,
                  Last_Update: this.updateDate,
                  Active: false,
                };
                $.ajax({
                  type: "POST",
                  url: that.apiUrl + "endoresment/editNurses.aspx/updatedNurse",
                  data: JSON.stringify({ detail: updateNurse }),
                  contentType: "application/json; charset=utf-8",
                  dataType: "json",
                  success: function () {
                    if (i == that.users.length - 1) {
                      swal({
                        title: "Sweet!",
                        text: "You successfully updated the selection ...",
                        icon: "success",
                        dangerMode: true,
                      });
                      location.reload();
                    }
                  },
                });
              }
            }
          }
        }
      });
    },
    /**  OnSubmit: function () {
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
            text: "Sorry, the selection has been canceled!",
          });
        } else {
          if (
            this.Endoresment_Nurses_Units.filter(
              (x) => x.Nurse_id == this.selectedNurse[i].id && x.Unit_id == this.path
            ).length == 0
          ) {
            for (let i = 0; i < this.selectedNurse.length; i++) {
              ObjectD.Nurse_id = this.selectedNurse[i].id;
              ObjectD.Unit_id = this.path;
              ObjectD.Entry_user = JSON.parse(localStorage.getItem("user")).Emp_id;
              ObjectD.Active = true;

              $.ajax({
                type: "POST",
                url: that.apiUrl + "endoresment/editNurses.aspx/insertNurseSelection",
                data: JSON.stringify({ data: ObjectD }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function () {
                  if (i == that.selectedNurse.length - 1) {
                    swal({
                      title: "Sweet!",
                      text: "You successfully made your selections ...",
                      icon: "success",
                      dangerMode: true,
                    });
                    location.reload();
                  }
                },
              });
            }
          } else {
            that.filterNurses = that.oldNerses.filter(
              (x) => !that.selectedNurse.includes(x)
            );
            if (this.updateDate == "") {
              swal({
                title: "Error!",
                text: "Sorry, you should select the update date ! ",
                icon: "warning",
                dangerMode: true,
              });
            } else {
              for (let x = 0; x < this.filterNurses.length; x++) {
                let updateNurse = {
                  id: this.Endoresment_Nurses_Units.filter(
                    (x) => x.Nurse_id == this.filterNurses[x].id && x.Unit_id == this.path
                  )[0].Id,
                  Nurse_id: this.filterNurses[x].id,
                  Unit_id: this.path,
                  Entry_user: JSON.parse(localStorage.getItem("user")).Emp_id,
                  Last_Update: this.updateDate,
                  Active: false,
                };
                $.ajax({
                  type: "POST",
                  url: that.apiUrl + "endoresment/editNurses.aspx/updatedNurse",
                  data: JSON.stringify({ detail: updateNurse }),
                  contentType: "application/json; charset=utf-8",
                  dataType: "json",
                  success: function () {
                    if (i == that.filterNurses.length - 1) {
                      swal({
                        title: "Sweet!",
                        text: "You successfully updated the selection ...",
                        icon: "success",
                        dangerMode: true,
                      });
                      location.reload();
                    }
                  },
                });
              }
            }
          }
        }
      });
    },**/
    /**  OnSubmit: function () {
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
          for (let i = 0; i < this.selectedNurse.length; i++) {
            if (
              this.Endoresment_Nurses_Units.filter(
                (x) => x.Nurse_id == this.selectedNurse[i].id && x.Unit_id == this.path
              ).length == 0
            ) {
              ObjectD.Nurse_id = this.selectedNurse[i].id;
              ObjectD.Unit_id = this.path;
              ObjectD.Entry_user = JSON.parse(localStorage.getItem("user")).Emp_id;
              ObjectD.Active = true;

              $.ajax({
                type: "POST",
                url: that.apiUrl + "endoresment/editNurses.aspx/insertNurseSelection",
                data: JSON.stringify({ data: ObjectD }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function () {
                  if (i == that.selectedNurse.length - 1) {
                    swal({
                      title: "Sweet!",
                      text: "You successfully made your selections ...",
                      icon: "success",
                      dangerMode: true,
                    });
                    location.reload();
                  }
                },
              });
            } else {
              that.filterNurses = that.oldNerses.filter(
                (x) => !that.selectedNurse.includes(x)
              );
              if (this.updateDate == "") {
                swal({
                  title: "Error!",
                  text: "Sorry, you should select the update date ! ",
                  icon: "warning",
                  dangerMode: true,
                });
                console.log(that.filterNurses[0].name);
              } else {
                for (let x = 0; x < this.filterNurses.length; x++) {
                  let updateNurse = {
                    id: this.Endoresment_Nurses_Units.filter(
                      (x) =>
                        x.Nurse_id == this.filterNurses[x].id && x.Unit_id == this.path
                    )[0].Id,
                    Nurse_id: this.filterNurses[x].id,
                    Unit_id: this.path,
                    Entry_user: JSON.parse(localStorage.getItem("user")).Emp_id,
                    Last_Update: this.updateDate,
                    Active: false,
                  };
                  $.ajax({
                    type: "POST",
                    url: that.apiUrl + "endoresment/editNurses.aspx/updatedNurse",
                    data: JSON.stringify({ detail: updateNurse }),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function () {
                      if (i == that.filterNurses.length - 1) {
                        swal({
                          title: "Sweet!",
                          text: "You successfully updated the selection ...",
                          icon: "success",
                          dangerMode: true,
                        });
                        location.reload();
                      }
                    },
                  });
                }
              }
            }
          }
        }
      });
    },**/
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
        //get Endoresment Nurses Units
        $.ajax({
          type: "POST",
          url: that.apiUrl + "endoresment/Nurses.aspx/getEndoresment_Nurses_UnitsData",
          contentType: "application/json; charset=utf-8",
          dataType: "json",
          success: function (data) {
            that.Endoresment_Nurses_Units = JSON.parse(data.d);

            that.selectedNurse = that.Endoresment_Nurses_Units.filter(
              (x) => x.Unit_id == that.path
            ).map((z) => {
              let id = z.Nurse_id;
              z = {
                id: id,
                name: that.users.filter((i) => i.Emp_id == id)[0].FullName,
              };
              return z;
            });

            that.oldNerses = that.selectedNurse;
          },
        });
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
  max-height: 100vh;
}
</style>
