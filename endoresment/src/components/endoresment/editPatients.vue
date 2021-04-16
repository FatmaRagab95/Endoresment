<template>
  <div class="editPatients">
    <div class="container-fluid">
      <div class="card bg-light select-form p-4 text-left shadow">
        <div class="row mt-5">
          <span class="col-md-10"
            ><h3 class="text-center" style="text-decoration: underline">
              <span class="text-success">Patient List For / </span>
              {{ users.filter((x) => x.Emp_id == path)[0].FullName }}
            </h3>
          </span>
          <span class="col-md-2">
            <button class="btn btn-primary pull-right shadow" @click.prevent="OnSubmit">
              Submit selection
            </button>
          </span>
          <span class="col-md-6 mt-0">
            <form class="form-inline">
              <input
                class="form-control mr-sm-2"
                style="width: 85%"
                type="text"
                placeholder="Type a patient name.."
                v-model="selectedName"
                v-on:keyup="namesList"
                v-on:keydown.enter.prevent
              />
              <span
                class="btn btn-primary shadow pt-1 pb-1 rounded mr-2 text-center"
                style="width: 10%"
              >
                <i class="fa fa-search" aria-hidden="true"></i>
              </span>
            </form>
          </span>
          <span class="col-md-6 row mt-0">
            <span class="col-md-3 text-right p-0 m-0" style="line-height: 2.1"
              >Updated Date:</span
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
            <h5 class="text-info" style="text-decoration: underline">
              Patients list for {{ users.filter((x) => x.Emp_id == path)[0].FullName }}
            </h5>
            <div class="card overflow-auto p-3 shadow rounded" style="height: 500px">
              <div
                class="pt-2 pb-2 border-bottom d-flex justify-content-between"
                v-for="(patient, i) in filterNames(patients, selectedName.toLowerCase())"
                :key="i + 'f'"
              >
                <span class="w-50">
                  <span
                    ><input
                      style="width: 20px; height: 20px"
                      class="rounded bg-dark"
                      :id="patient.id"
                      type="checkbox"
                      name="role"
                      :value="{ id: patient.id, name: patient.Patient_FullName }"
                      v-model="selectedPatients"
                  /></span>
                  <span class="ml-3"
                    ><label :for="patient.id">{{ patient.Patient_FullName }}</label></span
                  ></span
                >
                <span class="text-left w-25"
                  ><span class="text-secondary">Medicl Number : </span
                  >{{ patient.Medical_Number }}</span
                >

                <span class="text-right w-25"
                  ><span class="text-secondary">Room : </span
                  ><span class="text-success">{{ patient.Room }}</span></span
                >
              </div>
            </div>
          </div>
          <div class="col-md-5">
            <h5 class="text-info" style="text-decoration: underline">
              Selected patients with
              {{ users.filter((x) => x.Emp_id == path)[0].FullName }}
            </h5>
            <div class="card overflow-auto p-3 shadow rounded" style="height: 500px">
              <div
                class="pt-2 pb-2 border-bottom"
                v-for="(patient, i) in selectedPatients"
                :key="i + 'b'"
              >
                <span>{{ i + 1 }}</span>
                <span class="ml-3">{{ patient.name }}</span>
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
  name: "editPatients",
  props: ["link"],
  data() {
    return {
      Units: [],
      users: [],
      Nurse_patients: [],
      Nurses: [],
      patients: [],
      path: "",
      selectedPatients: [],
      oldPatients: [],
      filterPatients: [],

      selectedName: "",
      Names: [],

      updateDate: "",

      newPatients: {
        Patient_id: 0,
        Nurse_id: 0,
        Entry_user: 0,
        Bed_id: 0,
        Nurse_name: "",
        Date_to: "",
      },

      apiUrl: this.link,
    };
  },
  methods: {
    filterNames(list, value) {
      return list.filter((x) => x.Patient_FullName.toLowerCase().indexOf(value) > -1);
    },
    namesList(event) {
      if (event.key == "Enter") {
        this.Names.push(this.selectedName);
      }
    },

    OnSubmit: function () {
      let that = this;
      var ObjectD = Object.assign({}, this.newPatients);
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
          for (let i = 0; i < this.selectedPatients.length; i++) {
            if (
              this.Nurse_patients.filter(
                (x) =>
                  x.Patient_id == this.selectedPatients[i].id && x.Nurse_id == this.path
              ).length == 0
            ) {
              if (this.updateDate == "") {
                swal({
                  title: "Error!",
                  text: "Sorry, you should select the update date ! ",
                  icon: "warning",
                  dangerMode: true,
                });
              } else {
                ObjectD.Patient_id = this.selectedPatients[i].id;
                ObjectD.Nurse_id = this.path;
                ObjectD.Entry_user = JSON.parse(localStorage.getItem("user")).Emp_id;
                ObjectD.Date_from = this.updateDate;
                ObjectD.Bed_id = this.patients.filter(
                  (x) => x.id == this.selectedPatients[i].id
                )[0].Bed_id;
                ObjectD.Nurse_name = this.users.filter(
                  (x) => x.Emp_id == this.path
                )[0].FullName;

                $.ajax({
                  type: "POST",
                  url:
                    that.apiUrl + "endoresment/editPatients.aspx/insertPatientsSelection",
                  data: JSON.stringify({ data: ObjectD }),
                  contentType: "application/json; charset=utf-8",
                  dataType: "json",
                  success: function () {
                    if (i == that.selectedPatients.length - 1) {
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
          }

          // get uncheked nurses
          let old = that.oldPatients.map((x) => x.id);
          let newPatients = that.selectedPatients.map((x) => x.id);

          if (old.filter((x) => !newPatients.includes(x)).length > 0) {
            that.filterPatients = old.filter((x) => !newPatients.includes(x));

            if (this.updateDate == "") {
              swal({
                title: "Error!",
                text: "Sorry, you should select the update date ! ",
                icon: "warning",
                dangerMode: true,
              });
            } else {
              for (let x = 0; x < this.filterPatients.length; x++) {
                let updatePatients = {
                  id: this.Nurse_patients.filter(
                    (z) =>
                      z.Patient_id == this.filterPatients[x] && z.Nurse_id == this.path
                  )[0].id,
                  Patient_id: this.filterPatients[x],
                  Nurse_id: 0,
                  Entry_user: JSON.parse(localStorage.getItem("user")).Emp_id,
                  Date_to: this.updateDate,
                  Bed_id: this.patients.filter((z) => z.id == this.filterPatients[x])[0]
                    .Bed_id,
                  Nurse_name: "",
                };
                $.ajax({
                  type: "POST",
                  url: that.apiUrl + "endoresment/editPatients.aspx/updatedPatient",
                  data: JSON.stringify({ detail: updatePatients }),
                  contentType: "application/json; charset=utf-8",
                  dataType: "json",
                  success: function () {
                    if (x == that.filterPatients.length - 1) {
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
                });
              }
            }
          }
        }
      });
    },
  },

  created() {
    let that = this;
    this.path = this.$router.history.current.path.split("/")[2];
    this.updateDate = moment(new Date()).format("YYYY-MM-DD");

    //get patients data
    $.ajax({
      type: "POST",
      url: that.apiUrl + "endoresment/editPatients.aspx/getPatientsData",
      contentType: "application/json; charset=utf-8",
      dataType: "json",
      success: function (data) {
        that.patients = JSON.parse(data.d);

        // get Endorsement Nurse patients
        $.ajax({
          type: "POST",
          url: that.apiUrl + "endoresment/editPatients.aspx/Endorsement_Nurse_patients",
          contentType: "application/json; charset=utf-8",
          dataType: "json",
          success: function (data) {
            that.Nurse_patients = JSON.parse(data.d);

            that.selectedPatients = that.Nurse_patients.filter(
              (x) => x.Nurse_id == that.path
            ).map((z) => {
              let id = z.Patient_id;
              z = {
                id: id,
                name: that.patients.filter((i) => i.id == id)[0].Patient_FullName,
              };
              return z;
            });

            that.oldPatients = that.selectedPatients;
          },
        });
      },
    });

    //get admin users
    $.ajax({
      type: "POST",
      url: that.apiUrl + "endoresment/editPatients.aspx/getadminusersData",
      contentType: "application/json; charset=utf-8",
      dataType: "json",
      success: function (data) {
        that.users = JSON.parse(data.d);
      },
    });
  },
};
</script>

<style scoped>
.select-form {
  min-height: 100vh;
  padding-bottom: 120px;
  margin-bottom: 120px;
}
</style>
