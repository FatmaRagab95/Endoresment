<template>
  <v-container fluid>
    <v-data-iterator
      :items="Nurses"
      :items-per-page.sync="itemsPerPage"
      :page.sync="page"
      :search="search"
      hide-default-footer
    >
      <template v-slot:header>
        <v-toolbar
          color="bg-primary"
          class="mb-1"
        >
          <v-text-field
            v-model="search"
            clearable
            flat
            solo-inverted
            hide-details
            prepend-inner-icon="mdi-magnify"
            label="Search"
          ></v-text-field>
        </v-toolbar>
      </template>

      <template v-slot:default="props">
        <v-row>
          <v-col
            v-for="item in props.items"
            :key="item.Nurse_name"
            cols="12"
            sm="6"
            md="4"
            lg="3"
          >
            <v-card >
              <v-card-title class="subheading font-weight-bold">
                {{ item.Nurse_name }}
              </v-card-title>

              <v-divider></v-divider>

              <v-list dense>
                <v-list-item
                  v-for="(key, index) in item.shifts"
                  :key="index"
                >
                  <v-list-item-content :class="{ 'blue--text': sortBy === key }">
                    {{ key.name }}:
                    {{key.shift}}
                  </v-list-item-content>
                  <v-list-item-content
                    class="align-end"
                    :class="{ 'blue--text': sortBy === key.name }"
                  >
                    {{ item[key.name.toLowerCase()] }}
                  </v-list-item-content>
                </v-list-item>
              </v-list>
            </v-card>
          </v-col>
        </v-row>
      </template>

      <template v-slot:footer>
        <v-row
          class="mt-2"
          align="center"
          justify="center"
        >
          <span class="grey--text">Items per page</span>
          <v-menu offset-y>
            <template v-slot:activator="{ on, attrs }">
              <v-btn
                dark
                text
                color="primary"
                class="ml-2"
                v-bind="attrs"
                v-on="on"
              >
                {{ itemsPerPage }}
                <v-icon>mdi-chevron-down</v-icon>
              </v-btn>
            </template>
            <v-list>
              <v-list-item
                v-for="(number, index) in itemsPerPageArray"
                :key="index"
                @click="updateItemsPerPage(number)"
              >
                <v-list-item-title>{{ number }}</v-list-item-title>
              </v-list-item>
            </v-list>
          </v-menu>

          <v-spacer></v-spacer>

          <span
            class="mr-4
            blue--text">
            Page {{ page }} of {{ numberOfPages }}
          </span>
          <v-btn
            fab
            dark
            color="blue darken-3"
            class="mr-1"
            @click="formerPage"
          >
            <v-icon>mdi-chevron-left</v-icon>
          </v-btn>
          <v-btn
            fab
            dark
            color="blue darken-3"
            class="ml-1"
            @click="nextPage"
          >
            <v-icon>mdi-chevron-right</v-icon>
          </v-btn>
        </v-row>
      </template>
    </v-data-iterator>
  </v-container>
</template>

<script>
  export default {
    props: ["link", "user"],
    data () {
      return {

    apiUrl: this.link,
      Units: [],
      Nurses: [],      
      headers: [
        {
          text: 'Staff Name',
          align: 'start',
          sortable: false,
          value: 'Nurse_name',
        },
        { text: 'ID', value: 'Nurse_id' },
      ],
      Endorsement_Nursing_schedule: [],
      today: "",

      monthes: [
        { name: "January", id: "01" },
        { name: "February", id: "02" },
        { name: "March", id: "03" },
        { name: "April", id: "04" },
        { name: "May", id: "05" },
        { name: "June", id: "06" },
        { name: "July", id: "07" },
        { name: "August", id: "08" },
        { name: "September", id: "09" },
        { name: "October", id: "10" },
        { name: "November", id: "11" },
        { name: "December", id: "12" },
      ],

      month: "03",
      unit: 1,
      shift: "",

      monthCalendar: [],
      selectedMonth: [],
        
        itemsPerPageArray: [4, 8, 12],
        search: '',
        filter: {},
        sortDesc: false,
        page: 1,
        itemsPerPage: 4,
        sortBy: 'name',
        Nurses: [],
      }
    },
    computed: {
      numberOfPages () {
        return Math.ceil(this.Nurses.length / this.itemsPerPage)
      },
    },
    methods: {
        monthDays: function () {
      function getMonths(month, year) {
        var arr = [];
        var start = moment(year + "-" + month, "YYYY-MMM");
        for (
          var end = moment(start).add(1, "month");
          start.isBefore(end);
          start.add(1, "day")
        ) {
          arr.push(
            {
              fullDate: start.format("YYYY-MM-DD"),
              day: start.format("D-ddd")
            }
          );
        }
        return arr;
      }
      this.monthCalendar = getMonths(
        this.monthes.filter((x) => x.id == this.month)[0].name.substring(0, 3),
        new Date().getFullYear()
      );

      this.selectedMonth = this.Endorsement_Nursing_schedule.filter(
        (x) => x.Unit_id == this.unit && x.Date.split("-")[1] == this.month
      );

      /* edit Endorsement_Nursing_schedule to be presented as a table */

      // 1- get nurses names in Endorsement_Nursing_schedule without duplicate
      this.Nurses = this.Endorsement_Nursing_schedule.filter((thing, index, self) =>
        index === self.findIndex((t) => (
          t.Nurse_id === thing.Nurse_id
        ))
      );

      // 2- push all shift data for 30 days
      let that = this;

      this.Nurses.map(x => {
        x.shifts = [];
        that.monthCalendar.map(z => {
          let obj = {};
          obj.name = z.day;

          if (that.Endorsement_Nursing_schedule.filter(i => i.Nurse_name == x.Nurse_name && z.fullDate.trim() == i.Date.trim()).length > 0) {
            obj.shift = x.Shift;
            obj.work = true
          } else {
            obj.shift = '';
            obj.work = false;
          }

          x.shifts.push(obj);

          

          // push days as headers in table
          that.headers.push({ text: z.day, value: obj.shift });
        });
      });

    },
      nextPage () {
        if (this.page + 1 <= this.numberOfPages) this.page += 1
      },
      formerPage () {
        if (this.page - 1 >= 1) this.page -= 1
      },
      updateItemsPerPage (number) {
        this.itemsPerPage = number
      },
    },

  created() {
    let that = this;

    //get Units
    $.ajax({
      type: "POST",
      url: that.apiUrl + "endoresment/display_schdule.aspx/getUnitsData",
      data: JSON.stringify({ branch: { id: that.user.Branch_ID } }),
      contentType: "application/json; charset=utf-8",
      dataType: "json",
      success: function (data) {
        that.Units = JSON.parse(data.d);
      },
    });

    //get getEndorsement_Nursing_scheduleData
    $.ajax({
      type: "POST",
      url:
        that.apiUrl +
        "endoresment/display_schdule.aspx/getEndorsement_Nursing_scheduleData",
      contentType: "application/json; charset=utf-8",
      dataType: "json",
      success: function (data) {
        that.Endorsement_Nursing_schedule = JSON.parse(data.d);
        that.monthDays();

      },
    });
  },
  }
</script>