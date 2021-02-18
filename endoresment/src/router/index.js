import Vue from "vue";
import Router from "vue-router";
import login from "@/components/login";
import home from "@/components/home";

import addUnit from "@/components/endoresment/addUnit";
import Units from "@/components/endoresment/Units";
import addPatient from "@/components/endoresment/add_patient";
import viewPatients from "@/components/endoresment/viewPatients";
import UnitDetails from "@/components/endoresment/UnitDetails";
import shiftDetails from "@/components/endoresment/shiftDetails";
import editShiftDetials from "@/components/endoresment/editShiftDetials";
import insertPatientData from "@/components/endoresment/insertPatientData";
import roomDetails from "@/components/endoresment/roomDetails";
import patientData from "@/components/endoresment/patientData";
import or_patient from "@/components/endoresment/or_patient";
import or_patient_edit from "@/components/endoresment/or_patient_edit";
import OR from "@/components/endoresment/OR";
import OR_details from "@/components/endoresment/OR_details";

Vue.use(Router);

const router = new Router({
  // mode:'history',
  routes: [
    {
      path: "/login",
      name: "login",
      component: login,
      meta: {},
    },
    {
      path: "/",
      name: "Dashboard",
      component: home,
      meta: {
        requiresAuth: true,
        cat: "Endoresment",
      },
    },
    {
      path: "/add_Unit",
      name: "Add shift details",
      component: addUnit,
      meta: {
        requiresAuth: true,
        cat: "Endoresment",
      },
    },
    {
      path: "/Units",
      name: "Units",
      component: Units,
      meta: {
        requiresAuth: true,
        cat: "Endoresment",
      },
    },
    {
      path: "/UnitDetails/:id",
      name: "Unit Details",
      component: UnitDetails,
      meta: {
        requiresAuth: true,
        cat: "Endoresment",
      },
    },
    {
      path: "/ShiftDetails/:id",
      name: "Shift Details",
      component: shiftDetails,
      meta: {
        requiresAuth: true,
        cat: "Endoresment",
      },
    },
     {
      path: "/editShiftDetials/:id",
      name: "Edit Shift Details",
      component: editShiftDetials,
      meta: {
        requiresAuth: true,
        cat: "Endoresment",
      },
    },
    {
      path: "/addmition",
      name: "Admission Form",
      component: addPatient,
      meta: {
        requiresAuth: true,
        cat: "Endoresment",
      },
    },
    {
      path: "/viewPatients",
      name: "Patients",
      component: viewPatients,
      meta: {
        requiresAuth: true,
        cat: "Endoresment",
      },
    },
   {
      path: "/patientData",
      name: "Patient Data",
      component: patientData,
      meta: {
        requiresAuth: true,
        cat: "Endoresment",
      },
    },
   {
      path: "/insertPatientData",
      name: "Insert Patient Data",
      component: insertPatientData,
      meta: {
        requiresAuth: true,
        cat: "Endoresment",
      },
    },
   {
      path: "/roomDetails",
      name: "Room Details",
      component: roomDetails,
      meta: {
        requiresAuth: true,
        cat: "Endoresment",
      },
    },
    {
       path: "/or_patient",
       name: "Schedule Procedure",
       component: or_patient,
       meta: {
         requiresAuth: true,
         cat: "Endoresment",
       },
     },
     {
        path: "/or_patient_edit:id",
        name: "Edit Procedure",
        component: or_patient_edit,
        meta: {
          requiresAuth: true,
          cat: "Endoresment",
        },
      },
     {
        path: "/OR",
        name: "OR",
        component: OR,
        meta: {
          requiresAuth: true,
          cat: "Endoresment",
        },
      },
      {
         path: "/OR_details:id",
         name: "OR Details",
         component: OR_details,
         meta: {
           requiresAuth: true,
           cat: "Endoresment",
         },
       },
   
  ],
});

router.beforeEach((to, from, next) => {
  // check to see if route requires auth
  if (to.matched.some((rec) => rec.meta.requiresAuth)) {
    // check auth state of user
    let user = localStorage.getItem("user");
    if (user) {
      // user signed in, proceed to route
      next();
    } else {
      // no user signed in, redirect to login
      next({ name: "login" });
    }
  } else {
    next();
  }
});

export default router;
