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

public partial class _UnitDetials : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
    }

    // get unit
    [WebMethod]
    public static string getUnitData(Unit id)
    {
        string config = Convert.ToString(ConfigurationManager.ConnectionStrings["dbcon"]);
        List<Unit> Unit = new List<Unit>();

        SqlConnection con = new SqlConnection(config);

        con.Open();

        using (SqlCommand cmd = new SqlCommand("SELECT u.U_id,u.U_name, Units_Rooms.U_id, COUNT(Units_Rooms.R_id) AS  RoomsNum from Units_Rooms RIGHT JOIN Units as u On u.U_id = @U_id and Units_Rooms.U_id = u.U_id GROUP BY Units_Rooms.U_id, u.U_id,u.U_name", con))
        {
            cmd.Parameters.Add("@U_id", SqlDbType.Int).Value = id.U_id;
            SqlDataReader idr = cmd.ExecuteReader();

            if (idr.HasRows)
            {
                Unit = populateUnitsLisst(idr, con);
            }
        }

        con.Close();

        return JsonConvert.SerializeObject(Unit);
    }

    public static List<Unit>
    populateUnitsLisst(SqlDataReader idr, SqlConnection con)
    {
        List<Unit> UnitI = new List<Unit>();

        while (idr.Read())
        {
            UnitI
                .Add(new Unit {
                    U_id = idr["U_id"] != DBNull.Value ? Convert.ToInt32(idr["U_id"]) : 0,
                    U_name = Convert.ToString(idr["U_name"]),
                    RoomsNum = idr["RoomsNum"] != DBNull.Value ? Convert.ToInt32(idr["RoomsNum"]) : 0,

                });
        }

        return UnitI;
    }

    public class Unit
    {
        public int? U_id { get; set; }
        public string U_name { get; set; }
        public int? RoomsNum { get; set; }
    }
    
    // get unit dashboard
    [WebMethod]
    public static string getUnitsDashData(Unit id)
    {
        string config =
            Convert.ToString(ConfigurationManager.ConnectionStrings["dbcon"]);
        List<UnitsDash> UnitsDash = new List<UnitsDash>();

        SqlConnection con = new SqlConnection(config);

        con.Open();

        using (SqlCommand cmd = new SqlCommand("SELECT TOP(5) id,Unit_name,Shift,Shift_date,Total_Census,Received,Admission,Transfer_In,Transfer_Out from Endorsement_UnitsDashboard where Unit_id = @Unit_id order by id desc", con))
        {
            cmd.Parameters.Add("@Unit_id", SqlDbType.Int).Value = id.U_id;
            SqlDataReader idr = cmd.ExecuteReader();

            if (idr.HasRows)
            {
                UnitsDash = populateUnitsDashLisst(idr, con);
            }
        }

        con.Close();

        return JsonConvert.SerializeObject(UnitsDash);
    }

    public static List<UnitsDash>
    populateUnitsDashLisst(SqlDataReader idr, SqlConnection con)
    {
        List<UnitsDash> UnitsDashI = new List<UnitsDash>();

        while (idr.Read())
        {
            UnitsDashI
                .Add(new UnitsDash {
                    id = idr["id"] != DBNull.Value ? Convert.ToInt32(idr["id"]) : 0,
                    Unit_name = Convert.ToString(idr["Unit_name"]),
                    Shift = Convert.ToString(idr["Shift"]),
                    Shift_date = Convert.ToDateTime(idr["Shift_date"]),
                    Total_Census = idr["Total_Census"] != DBNull.Value ? Convert.ToInt32(idr["Total_Census"]) : 0,
                    Received = idr["Received"] != DBNull.Value ? Convert.ToInt32(idr["Received"]) : 0,
                    Admission = idr["Admission"] != DBNull.Value ? Convert.ToInt32(idr["Admission"]) : 0,
                    Transfer_In = idr["Transfer_In"] != DBNull.Value ? Convert.ToInt32(idr["Transfer_In"]) : 0,
                    Transfer_Out = idr["Transfer_Out"] != DBNull.Value ? Convert.ToInt32(idr["Transfer_Out"]) : 0,

                });
        }

        return UnitsDashI;
    }

    public class UnitsDash
    {
        public int id { get; set; }
        public int? Unit_id { get; set; }
        public string Unit_name { get; set; }
        public string Shift { get; set; }
        public DateTime Shift_date { get; set; }
        public int? Total_Census { get; set; }
        public int? Received { get; set; }
        public int? Admission { get; set; }
        public int? Transfer_In { get; set; }
        public int? Transfer_Out { get; set; }
    }

    // get patients data
    [WebMethod]
    public static string getPatientsData()
    {
        string config =  Convert.ToString(ConfigurationManager.ConnectionStrings["dbcon"]);
        List<Endorsement_PatientData> Endorsement_PatientData = new List<Endorsement_PatientData>();

        SqlConnection con = new SqlConnection(config);

        con.Open();

        using (SqlCommand cmd = new SqlCommand("select * from Endorsement_PatientData", con))
        {
            SqlDataReader idr = cmd.ExecuteReader();

            if (idr.HasRows)
            {
                Endorsement_PatientData = populateEndorsement_PatientDataLisst(idr, con);
            }
        }

        con.Close();

        return JsonConvert.SerializeObject(Endorsement_PatientData);
    }

    public static List<Endorsement_PatientData>
    populateEndorsement_PatientDataLisst(SqlDataReader idr, SqlConnection con)
    {
        List<Endorsement_PatientData> Endorsement_PatientDataI = new List<Endorsement_PatientData>();

        while (idr.Read())
        {
            Endorsement_PatientDataI
                .Add(new Endorsement_PatientData {
                    id = idr["id"] != DBNull.Value ? Convert.ToInt32(idr["id"]) : 0,
					Patient_FullName = Convert.ToString(idr["Patient_FullName"]),
                    Branch_id = idr["Branch_id"] != DBNull.Value ? Convert.ToInt32(idr["Branch_id"]) : 0,
					Branch_name = Convert.ToString(idr["Branch_name"]),
					Gender = Convert.ToString(idr["Gender"]),
					Medical_Number = Convert.ToInt64(idr["Medical_Number"]),
					Unit = Convert.ToString(idr["Unit"]),
					Room = Convert.ToString(idr["Room"]),
					Addmission_date = Convert.ToString(idr["Addmission_date"]),
                    Age = idr["Age"] != DBNull.Value ? Convert.ToInt32(idr["Age"]) : 0,
					Consultant_Name = Convert.ToString(idr["Consultant_Name"]),
					Specialty = Convert.ToString(idr["Specialty"]),
					Date_Birth = Convert.ToString(idr["Date_Birth"]),
                });
        }

        return Endorsement_PatientDataI;
    }

    public class Endorsement_PatientData
    {
        public int? id { get; set; }
        public string Patient_FullName { get; set; }
        public int? Branch_id { get; set; }
        public string Branch_name { get; set; }
        public string Gender { get; set; }
        public Int64 Medical_Number { get; set; }
        public string Unit { get; set; }
        public string Room { get; set; }
        public string Addmission_date { get; set; }
        public int? Age { get; set; }
        public string Consultant_Name { get; set; }
        public string Specialty { get; set; }
        public string Date_Birth { get; set; }
    }
}