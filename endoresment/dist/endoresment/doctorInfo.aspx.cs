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

public partial class doctorInfo : System.Web.UI.Page
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

    // get Specialities
    [WebMethod]
    public static string getSpecialitiesData()
    {
        string config =
            Convert.ToString(ConfigurationManager.ConnectionStrings["dbcon"]);
        List<Specialities> Specialities = new List<Specialities>();

        SqlConnection con = new SqlConnection(config);

        con.Open();

        using (
            SqlCommand cmd = new SqlCommand("select * from Specialities", con)
        )
        {
            SqlDataReader idr = cmd.ExecuteReader();

            if (idr.HasRows)
            {
                Specialities = populateSpecialitiesLisst(idr, con);
            }
        }

        con.Close();

        return JsonConvert.SerializeObject(Specialities);
    }

    public static List<Specialities>
    populateSpecialitiesLisst(SqlDataReader idr, SqlConnection con)
    {
        List<Specialities> SpecialitiesI = new List<Specialities>();

        while (idr.Read())
        {
            SpecialitiesI
                .Add(new Specialities {
                    Spcy_id =
                        idr["Spcy_id"] != DBNull.Value
                            ? Convert.ToInt32(idr["Spcy_id"])
                            : 0,
                    Spcy_name_En = Convert.ToString(idr["Spcy_name_En"])
                });
        }

        return SpecialitiesI;
    }

    public class Specialities
    {
        public int? Spcy_id { get; set; }

        public string Spcy_name_En { get; set; }
    }

    // get doctors data
    [WebMethod]
    public static string getdoctorsData()
    {
        string config =
            Convert.ToString(ConfigurationManager.ConnectionStrings["dbcon"]);
        List<doctors> doctors = new List<doctors>();

        SqlConnection con = new SqlConnection(config);

        con.Open();

        using (
            SqlCommand cmd = new SqlCommand("select * from Doctors_Data", con)
        )
        {
            SqlDataReader idr = cmd.ExecuteReader();

            if (idr.HasRows)
            {
                doctors = populatedoctorsLisst(idr, con);
            }
        }

        con.Close();

        return JsonConvert.SerializeObject(doctors);
    }

    public static List<doctors>
    populatedoctorsLisst(SqlDataReader idr, SqlConnection con)
    {
        List<doctors> doctorsI = new List<doctors>();

        while (idr.Read())
        {
            doctorsI
                .Add(new doctors {
                    id =
                        idr["id"] != DBNull.Value
                            ? Convert.ToInt32(idr["id"])
                            : 0,
                    Dr_Code =
                        idr["Dr_Code"] != DBNull.Value
                            ? Convert.ToString(idr["Dr_Code"])
                            : String.Empty,
                    DR_Name =
                        idr["DR_Name"] != DBNull.Value
                            ? Convert.ToString(idr["DR_Name"])
                            : String.Empty,
                    Type =
                        idr["Type"] != DBNull.Value
                            ? Convert.ToString(idr["Type"])
                            : String.Empty,
                    Spcy_id =
                        idr["Spcy_id"] != DBNull.Value
                            ? Convert.ToInt32(idr["Spcy_id"])
                            : 0
                });
        }

        return doctorsI;
    }

    public class doctors
    {
        public int? id { get; set; }

        public string Dr_Code { get; set; }

        public string DR_Name { get; set; }

        public string Type { get; set; }

        public int? Spcy_id { get; set; }
    }

    // get Endoresment_Doctors_Shifts data
    [WebMethod]
    public static string getDoctors_ShiftsData()
    {
        string config =
            Convert.ToString(ConfigurationManager.ConnectionStrings["dbcon"]);
        List<Doctors_Shifts> Doctors_Shifts = new List<Doctors_Shifts>();

        SqlConnection con = new SqlConnection(config);

        con.Open();

        using (
            SqlCommand cmd =
                new SqlCommand("SELECT  d.Doctor_id,d.Doctor_name, Endoresment_Doctors_Shifts.Doctor_id,COUNT(DISTINCT Endoresment_Doctors_Shifts.Id) AS  ReceiveNum, SUM(Endoresment_Doctors_Shifts.Confirm) / COUNT(DISTINCT Endoresment_Doctors_Shifts.Id) AS  ConfirmNum, SUM(Endoresment_Doctors_Shifts.Completed) / COUNT(DISTINCT Endoresment_Doctors_Shifts.Id) AS  CompleteNum from Endoresment_Doctors_Shifts RIGHT JOIN Endoresment_Doctors_Shifts as d On Endoresment_Doctors_Shifts.Doctor_id = d.Doctor_id GROUP BY Endoresment_Doctors_Shifts.Doctor_id, d.Doctor_id,d.Doctor_name",
                    con)
        )
        {
            SqlDataReader idr = cmd.ExecuteReader();

            if (idr.HasRows)
            {
                Doctors_Shifts = populateDoctors_ShiftsLisst(idr, con);
            }
        }

        con.Close();

        return JsonConvert.SerializeObject(Doctors_Shifts);
    }

    public static List<Doctors_Shifts>
    populateDoctors_ShiftsLisst(SqlDataReader idr, SqlConnection con)
    {
        List<Doctors_Shifts> Doctors_ShiftsI = new List<Doctors_Shifts>();

        while (idr.Read())
        {
            Doctors_ShiftsI
                .Add(new Doctors_Shifts {
                    Doctor_name =
                        idr["Doctor_name"] != DBNull.Value
                            ? Convert.ToString(idr["Doctor_name"])
                            : String.Empty,
                    Doctor_id =
                        idr["Doctor_id"] != DBNull.Value
                            ? Convert.ToInt32(idr["Doctor_id"])
                            : 0,
                    ReceiveNum =
                        idr["ReceiveNum"] != DBNull.Value
                            ? Convert.ToInt32(idr["ReceiveNum"])
                            : 0,
                    ConfirmNum =
                        idr["ConfirmNum"] != DBNull.Value
                            ? Convert.ToInt32(idr["ConfirmNum"])
                            : 0,
                    CompleteNum =
                        idr["CompleteNum"] != DBNull.Value
                            ? Convert.ToInt32(idr["CompleteNum"])
                            : 0
                });
        }

        return Doctors_ShiftsI;
    }

    public class Doctors_Shifts
    {
        public string Doctor_name { get; set; }

        public int? Doctor_id { get; set; }

        public int? ReceiveNum { get; set; }

        public int? ConfirmNum { get; set; }

        public int? CompleteNum { get; set; }
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
                new SqlCommand("select * from Endorsement_PatientData", con)
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
                    Gender = Convert.ToString(idr["Gender"]),
                    Medical_Number = Convert.ToInt64(idr["Medical_Number"]),
                    Unit = Convert.ToString(idr["Unit"]),
                    Room = Convert.ToString(idr["Room"]),
                    Addmission_date = Convert.ToString(idr["Addmission_date"]),
                    Consultant_Name = Convert.ToString(idr["Consultant_Name"]),
                    Specialty = Convert.ToString(idr["Specialty"])
                });
        }

        return Endorsement_PatientDataI;
    }

    public class Endorsement_PatientData
    {
        public int? id { get; set; }

        public string Patient_FullName { get; set; }

        public string Gender { get; set; }

        public Int64 Medical_Number { get; set; }

        public string Unit { get; set; }

        public string Room { get; set; }

        public string Addmission_date { get; set; }

        public string Consultant_Name { get; set; }

        public string Specialty { get; set; }
    }
}
