<template>
  <div class="editNurses pt-5">
    <div class="container-fluid">
      <div class="card bg-light select-form p-4 text-left shadow">
        <div class="row">
          <div class="col-md-6">
            <h1>{{ Units.filter((x) => x.U_id == path)[0].U_name }}</h1>
          </div>
          <span class="col-md-6">
            <button
              class="btn btn-primary pull-right shadow"
              @click.prevent="OnSubmit"
            >
              Submit selection
            </button>
          </span>
          <span class="col-lg-6">
            <form class="form-inline">
              <input
                class="form-control mr-sm-2"
                style="width: 85%"
                type="text"
                placeholder="Type a nurse name"
                v-model="selectedName"
                v-on:keyup="namesList"
                v-on:keydown.enter.prevent
              />
              <span
                class="btn btn-primary shadow pt-1 pb-1 rounded mr-2 text-center"
                style="width: 10%"
                ><i class="fa fa-search" aria-hidden="true"></i
              ></span>
            </form>
          </span>
          <span class="col-md-6 row">
            <span class="col-md-3 text-right p-0 m-0" style="line-height: 2.1"
              >Updated Date</span
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
          <div class="col-lg-7">
            <h3 class="text-info" style="text-decoration: underline">
              Nurses list in {{ Units.filter((x) => x.U_id == path)[0].U_name }}
            </h3>
            <div class="card overflow-auto p-3 shadow rounded" style="height: 500px">
              <div
                class="pt-2 pb-2 border-bottom"
                v-for="(nurse, i) in filterNames(users, selectedName.toLowerCase())"
                :key="i + 'f'"
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
                  class="pull-right text-success"
                  v-if="
                    Endoresment_Nurses_Units.filter(
                      (x) => x.Nurse_id == nurse.Emp_id && x.Active == 1
                    ).length > 0
                  "
                  >Registered in ..
                  <span
                    v-for="(unit, i) in Endoresment_Nurses_Units.filter(
                      (x) => x.Nurse_id == nurse.Emp_id && x.Active == 1
                    )"
                    :key="i"
                    >{{ Units.filter((x) => x.U_id == unit.Unit_id)[0].U_name }}..</span
                  >
                  {{
                }}</span>
                <span class="pull-right text-danger" v-else
                  >Not registered in any unit</span
                >
              </div>
            </div>
          </div>
          <div class="col-md-5">
            <h3 class="text-info" style="text-decoration: underline">
              Selected nurses in {{ Units.filter((x) => x.U_id == path)[0].U_name }}
            </h3>
            <div class="card overflow-auto p-3 shadow rounded" style="height: 500px">
              <div
                class="pt-2 pb-2 border-bottom"
                v-for="(nurse, i) in selectedNurse"
                :key="i + 'b'"
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
  props: ["link", 'user', 'edits', 'UnitDash'],
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

      selectedName: "",
      Names: [],

      updateDate: '',

      newUnitNurse: {
        Nurse_id: 0,
        Unit_id: 0,
        Entry_user: 0,
        Active: 1,
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
          for (let i = 0; i < this.selectedNurse.length; i++) {
            if (
              this.Endoresment_Nurses_Units.filter(
                (x) => x.Nurse_id == this.selectedNurse[i].id && x.Unit_id == this.path
              ).length == 0
            ) {
              ObjectD.Nurse_id = this.selectedNurse[i].id;
              ObjectD.Unit_id = this.path;
              ObjectD.Entry_user = JSON.parse(localStorage.getItem("user")).Emp_id;
              ObjectD.Active = 1;

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
                      dangerMode: 1,
                    });
                    location.reload();
                  }
                },
              });
            }
          }

          // get uncheked nurses
          let old = that.oldNerses.map((x) => x.id);
          let newNurses = that.selectedNurse.map((x) => x.id);

          if (old.filter((x) => !newNurses.includes(x)).length > 0) {
            that.filterNurses = old.filter((x) => !newNurses.includes(x));

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
                    (z) => z.Nurse_id == this.filterNurses[x] && z.Unit_id == this.path
                  )[0].Id,
                  Nurse_id: this.filterNurses[x],
                  Unit_id: this.path,
                  Entry_user: JSON.parse(localStorage.getItem("user")).Emp_id,
                  Last_Update: this.updateDate,
                  Active: 0,
                };

                resend();

                function resend () {
                    return $.ajax({
                      type: "POST",
                      url: that.apiUrl + "endoresment/editNurses.aspx/updatedNurse",
                      data: JSON.stringify({ detail: updateNurse }),
                      contentType: "application/json; charset=utf-8",
                      dataType: "json",
                      beforeSend: function () {
                          swal({
                            title: "Sending!",
                            text: "Please wait!",
                            buttons: false,
                            closeOnClickOutside: false
                          });
                      },
                      success: function () {
                        if (x == that.filterNurses.length - 1) {
                          swal({
                            title: "Sweet!",
                            text: "You successfully updated the selection ...",
                            icon: "success",
                            dangerMode: true,
                          }).then(() => {
                            location.reload();
                          });
                        }
                      },
                      error : function () {
                        resend();
                      }
                    });
                } 
              }
            }

          }
        }
      });
    },
  },

  created() {
    let that = this;

    this.path = this.$route.params.id;
    this.updateDate = moment(new Date()).format('YYYY-MM-DD');

    // if current user is charge nurse to this unit
    if ((this.edits && this.UnitDash.filter(x => x.Unit_id == this.path).length == 0) || 
    (!this.edits)) {
      this.$router.go(-1)
    }

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
          url:
            that.apiUrl + "endoresment/editNurses.aspx/getEndoresment_Nurses_UnitsData",
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
.select-form {
  min-height: 100vh;
  padding-bottom:120px;
  margin-bottom:120px;
}
</style>
