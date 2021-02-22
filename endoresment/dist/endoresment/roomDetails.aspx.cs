using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;

public partial class _roomDetails : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
    }

    // get units
    [WebMethod]
    public static string getUnitsData()
    {
        string config =
            Convert.ToString(ConfigurationManager.ConnectionStrings["dbcon"]);
        List<Units> Units = new List<Units>();

        SqlConnection con = new SqlConnection(config);

        con.Open();

        using (SqlCommand cmd = new SqlCommand("select * from Units", con))
        {
            SqlDataReader idr = cmd.ExecuteReader();

            if (idr.HasRows)
            {
                Units = populateUnitsLisst(idr, con);
            }
        }

        con.Close();

        return JsonConvert.SerializeObject(Units);
    }

    public static List<Units>
    populateUnitsLisst(SqlDataReader idr, SqlConnection con)
    {
        List<Units> UnitsI = new List<Units>();

        while (idr.Read())
        {
            UnitsI
                .Add(new Units {
                    U_id =
                        idr["U_id"] != DBNull.Value
                            ? Convert.ToInt32(idr["U_id"])
                            : 0,
                    U_name = Convert.ToString(idr["U_name"])
                });
        }

        return UnitsI;
    }

    public class Units
    {
        public int? U_id { get; set; }

        public string U_name { get; set; }
    }

    // get rooms
    [WebMethod]
    public static string getRoomsData()
    {
        string config =
            Convert.ToString(ConfigurationManager.ConnectionStrings["dbcon"]);
        List<Rooms> Rooms = new List<Rooms>();

        SqlConnection con = new SqlConnection(config);

        con.Open();

        using (
            SqlCommand cmd =
                new SqlCommand("select * from Endorsement_Rooms", con)
        )
        {
            SqlDataReader idr = cmd.ExecuteReader();

            if (idr.HasRows)
            {
                Rooms = populateRoomsLisst(idr, con);
            }
        }

        con.Close();

        return JsonConvert.SerializeObject(Rooms);
    }

    public static List<Rooms>
    populateRoomsLisst(SqlDataReader idr, SqlConnection con)
    {
        List<Rooms> RoomsI = new List<Rooms>();

        while (idr.Read())
        {
            RoomsI
                .Add(new Rooms {
                    id =
                        idr["id"] != DBNull.Value
                            ? Convert.ToInt32(idr["id"])
                            : 0,
                    unit_id =
                        idr["unit_id"] != DBNull.Value
                            ? Convert.ToInt32(idr["unit_id"])
                            : 0,
                    Room_type_id =
                        idr["Room_type_id"] != DBNull.Value
                            ? Convert.ToInt32(idr["Room_type_id"])
                            : 0,
                    Unit = Convert.ToString(idr["Unit"]),
                    Room_name = Convert.ToString(idr["Room_name"]),
                    Room_type = Convert.ToString(idr["Room_type"])
                });
        }

        return RoomsI;
    }

    public class Rooms
    {
        public int? id { get; set; }

        public int? unit_id { get; set; }

        public int? Room_type_id { get; set; }

        public string Unit { get; set; }

        public string Room_name { get; set; }

        public string Room_type { get; set; }
    }

    // get rooms dashboard
    [WebMethod]
    public static string getRoomsDashboardData()
    {
        string config =
            Convert.ToString(ConfigurationManager.ConnectionStrings["dbcon"]);
        List<RoomsDashboard> RoomsDashboard = new List<RoomsDashboard>();

        SqlConnection con = new SqlConnection(config);

        con.Open();

        using (
            SqlCommand cmd =
                new SqlCommand("select * from Endorsement_RoomsDashboard", con)
        )
        {
            SqlDataReader idr = cmd.ExecuteReader();

            if (idr.HasRows)
            {
                RoomsDashboard = populateRoomsDashboardLisst(idr, con);
            }
        }

        con.Close();

        return JsonConvert.SerializeObject(RoomsDashboard);
    }

    public static List<RoomsDashboard>
    populateRoomsDashboardLisst(SqlDataReader idr, SqlConnection con)
    {
        List<RoomsDashboard> RoomsDashboardI = new List<RoomsDashboard>();

        while (idr.Read())
        {
            RoomsDashboardI
                .Add(new RoomsDashboard {
                    id =
                        idr["id"] != DBNull.Value
                            ? Convert.ToInt32(idr["id"])
                            : 0,
                    Room_id =
                        idr["Room_id"] != DBNull.Value
                            ? Convert.ToInt32(idr["Room_id"])
                            : 0,
                    Bed_name = Convert.ToString(idr["Bed_name"]),
                    Status_id =
                        idr["Status_id"] != DBNull.Value
                            ? Convert.ToInt32(idr["Status_id"])
                            : 0,
                    Status_name = Convert.ToString(idr["Status_name"]),
                    Shift = Convert.ToString(idr["Shift"]),
                    Entry_User =
                        idr["Entry_User"] != DBNull.Value
                            ? Convert.ToInt32(idr["Entry_User_id"])
                            : 0,
                    Entry_User_name = Convert.ToString(idr["Entry_User_name"])
                });
        }

        return RoomsDashboardI;
    }

    public class RoomsDashboard
    {
        public int? id { get; set; }

        public int? Room_id { get; set; }

        public string Bed_name { get; set; }

        public int? Status_id { get; set; }

        public string Status_name { get; set; }

        public string Shift { get; set; }

        public int? Entry_User { get; set; }

        public string Entry_User_name { get; set; }
    }

    // get patients data
    [WebMethod]
    public static string getPatientsData()
    {
        string config =
            Convert.ToString(ConfigurationManager.ConnectionStrings["dbcon"]);
        List<Endorsement_PatientData> Endorsement_PatientData =
            new List<Endorsement_PatientData>();

        SqlConnection con = new SqlConnection(config);

        con.Open();

        using (
            SqlCommand cmd =
                new SqlCommand("select * from Endorsement_PatientData WHERE Patient_Status = 1",
                    con)
        )
        {
            SqlDataReader idr = cmd.ExecuteReader();

            if (idr.HasRows)
            {
                Endorsement_PatientData =
                    populateEndorsement_PatientDataLisst(idr, con);
            }
        }

        con.Close();

        return JsonConvert.SerializeObject(Endorsement_PatientData);
    }

    public static List<Endorsement_PatientData>
    populateEndorsement_PatientDataLisst(SqlDataReader idr, SqlConnection con)
    {
        List<Endorsement_PatientData> Endorsement_PatientDataI =
            new List<Endorsement_PatientData>();

        while (idr.Read())
        {
            Endorsement_PatientDataI
                .Add(new Endorsement_PatientData {
                    id =
                        idr["id"] != DBNull.Value
                            ? Convert.ToInt32(idr["id"])
                            : 0,
                    Patient_FullName =
                        Convert.ToString(idr["Patient_FullName"]),
                    Bed_id =
                        idr["Bed_id"] != DBNull.Value
                            ? Convert.ToInt32(idr["Bed_id"])
                            : 0,
                    Room = Convert.ToString(idr["Room"]),
                    Gender = Convert.ToString(idr["Gender"]),
                    Medical_Number = Convert.ToInt64(idr["Medical_Number"]),
                    Age =
                        idr["Age"] != DBNull.Value
                            ? Convert.ToInt32(idr["Age"])
                            : 0,
                    Specialty = Convert.ToString(idr["Specialty"])
                });
        }

        return Endorsement_PatientDataI;
    }

    public class Endorsement_PatientData
    {
        public int? id { get; set; }

        public string Patient_FullName { get; set; }

        public int? Bed_id { get; set; }

        public string Room { get; set; }

        public string Gender { get; set; }

        public Int64 Medical_Number { get; set; }

        public int? Age { get; set; }

        public string Specialty { get; set; }
    }
}
