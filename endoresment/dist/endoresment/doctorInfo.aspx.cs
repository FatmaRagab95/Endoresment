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

    // get branches
    [WebMethod]
    public static string getBranchesData()
    {
        string config =
            Convert.ToString(ConfigurationManager.ConnectionStrings["dbcon"]);
        List<Branches> Branches = new List<Branches>();

        SqlConnection con = new SqlConnection(config);

        con.Open();

        using (SqlCommand cmd = new SqlCommand("select * from Branches", con))
        {
            SqlDataReader idr = cmd.ExecuteReader();

            if (idr.HasRows)
            {
                Branches = populateBranchesLisst(idr, con);
            }
        }

        con.Close();

        return JsonConvert.SerializeObject(Branches);
    }

    public static List<Branches>
    populateBranchesLisst(SqlDataReader idr, SqlConnection con)
    {
        List<Branches> BranchesI = new List<Branches>();

        while (idr.Read())
        {
            BranchesI
                .Add(new Branches {
                    id =
                        idr["id"] != DBNull.Value
                            ? Convert.ToInt32(idr["id"])
                            : 0,
                    Branch_EName = Convert.ToString(idr["Branch_EName"])
                });
        }

        return BranchesI;
    }

    public class Branches
    {
        public int? id { get; set; }

        public string Branch_EName { get; set; }
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
                    Branch =
                        idr["Branch"] != DBNull.Value
                            ? Convert.ToInt32(idr["Branch"])
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

        public int? Branch { get; set; }

        public string Dr_Code { get; set; }

        public string DR_Name { get; set; }

        public string Type { get; set; }

        public int? Spcy_id { get; set; }
    }

    // get Endoresment_Doctors_Shifts data
    [WebMethod]
    public static string getDoctors_ShiftsData(Doctors_Shifts shift)
    {
        string config =
            Convert.ToString(ConfigurationManager.ConnectionStrings["dbcon"]);
        List<Doctors_Shifts> Doctors_Shifts = new List<Doctors_Shifts>();

        SqlConnection con = new SqlConnection(config);

        con.Open();

        using (
            SqlCommand cmd =
                new SqlCommand("SELECT * from Endoresment_Doctors_Shifts where Spcy_id = @Spcy_id and Shift_date >= @Shift_date and Shift_date < @endDate and  Doctor_id in (select Dr_Code from Doctors_Data where Branch = @Branch_id )",
                    con)
        )
        {
            cmd.Parameters.Add("@Spcy_id", SqlDbType.Int).Value = shift.Spcy_id;
            cmd.Parameters.Add("@Branch_id", SqlDbType.Int).Value =
                shift.Branch_id;
            cmd.Parameters.Add("@Shift_date", SqlDbType.VarChar).Value =
                shift.Shift_date;
            cmd.Parameters.Add("@endDate", SqlDbType.VarChar).Value =
                shift.endDate;
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
                    id =
                        idr["id"] != DBNull.Value
                            ? Convert.ToInt32(idr["id"])
                            : 0,
                    Doctor_name =
                        idr["Doctor_name"] != DBNull.Value
                            ? Convert.ToString(idr["Doctor_name"])
                            : String.Empty,
                    Shift_date =
                        idr["Shift_date"] != DBNull.Value
                            ? Convert.ToString(idr["Shift_date"])
                            : String.Empty,
                    Doctor_id =
                        idr["Doctor_id"] != DBNull.Value
                            ? Convert.ToInt32(idr["Doctor_id"])
                            : 0
                });
        }

        return Doctors_ShiftsI;
    }

    public class Doctors_Shifts
    {
        public int? id { get; set; }

        public string Doctor_name { get; set; }

        public int? Branch_id { get; set; }

        public int? Doctor_id { get; set; }

        public int? Spcy_id { get; set; }

        public string endDate { get; set; }

        public string Shift_date { get; set; }
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
                    Consultant_id =
                        idr["Consultant_id"] != DBNull.Value
                            ? Convert.ToInt32(idr["Consultant_id"])
                            : 0,
                    Age =
                        idr["Age"] != DBNull.Value
                            ? Convert.ToString(idr["Age"])
                            : String.Empty,
                    Patient_FullName =
                        Convert.ToString(idr["Patient_FullName"]),
                    Gender = Convert.ToString(idr["Gender"]),
                    Medical_Number = Convert.ToInt64(idr["Medical_Number"]),
                    Unit = Convert.ToString(idr["Unit"]),
                    Room = Convert.ToString(idr["Room"]),
                    Addmission_date = Convert.ToString(idr["Addmission_date"]),
                    Discharged_date = Convert.ToString(idr["Discharged_date"]),
                    Consultant_Name = Convert.ToString(idr["Consultant_Name"]),
                    Specialty = Convert.ToString(idr["Specialty"])
                });
        }

        return Endorsement_PatientDataI;
    }

    public class Endorsement_PatientData
    {
        public int? id { get; set; }

        public int? Consultant_id { get; set; }

        public string Age { get; set; }

        public string Patient_FullName { get; set; }

        public string Gender { get; set; }

        public Int64 Medical_Number { get; set; }

        public string Unit { get; set; }

        public string Room { get; set; }

        public string Addmission_date { get; set; }

        public string Discharged_date { get; set; }

        public string Consultant_Name { get; set; }

        public string Specialty { get; set; }
    }

    // get Endoresment_Doctors_Shifts data
    /*
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
                new SqlCommand("select * from Endoresment_Doctors_Shifts", con)
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
                    id =
                        idr["id"] != DBNull.Value
                            ? Convert.ToInt32(idr["id"])
                            : 0,
                    Doctor_name =
                        idr["Doctor_name"] != DBNull.Value
                            ? Convert.ToString(idr["Doctor_name"])
                            : String.Empty,
                    Shift_date =
                        idr["Shift_date"] != DBNull.Value
                            ? Convert.ToString(idr["Shift_date"])
                            : String.Empty,
                    Doctor_id =
                        idr["Doctor_id"] != DBNull.Value
                            ? Convert.ToInt32(idr["Doctor_id"])
                            : 0
                });
        }

        return Doctors_ShiftsI;
    }*/
    /*
    public class Doctors_Shifts
    {
        public int? id { get; set; }

        public string Doctor_name { get; set; }

        public int? Doctor_id { get; set; }

        public string Shift_date { get; set; }
    }*/
    // get Endorsement_PatientFollow
    [WebMethod]
    public static string getPatientFollowData()
    {
        string config =
            Convert.ToString(ConfigurationManager.ConnectionStrings["dbcon"]);
        List<PatientFollow> PatientFollow = new List<PatientFollow>();

        SqlConnection con = new SqlConnection(config);

        con.Open();

        using (
            SqlCommand cmd =
                new SqlCommand("select * from Endorsement_PatientFollow where Consultaion > 0", con)
        )
        {
            SqlDataReader idr = cmd.ExecuteReader();

            if (idr.HasRows)
            {
                PatientFollow = populatePatientFollowLisst(idr, con);
            }
        }

        con.Close();

        return JsonConvert.SerializeObject(PatientFollow);
    }

    public static List<PatientFollow>
    populatePatientFollowLisst(SqlDataReader idr, SqlConnection con)
    {
        List<PatientFollow> PatientFollowI = new List<PatientFollow>();

        while (idr.Read())
        {
            PatientFollowI
                .Add(new PatientFollow {
                    id =
                        idr["id"] != DBNull.Value
                            ? Convert.ToInt32(idr["id"])
                            : 0,
                    Consultaion =
                        idr["Consultaion"] != DBNull.Value
                            ? Convert.ToInt32(idr["Consultaion"])
                            : 0,
                    Patient_id =
                        idr["Patient_id"] != DBNull.Value
                            ? Convert.ToInt32(idr["Patient_id"])
                            : 0,
                    Insert_Nurse =
                        idr["Insert_Nurse"] != DBNull.Value
                            ? Convert.ToInt32(idr["Insert_Nurse"])
                            : 0,
                    Insert_Doctor =
                        idr["Insert_Doctor"] != DBNull.Value
                            ? Convert.ToInt32(idr["Insert_Doctor"])
                            : 0,
                    Shift = Convert.ToString(idr["Shift"]),
                    Insert_Nurse_Time =
                        idr["Insert_Nurse_Time"] != DBNull.Value
                            ? Convert.ToString(idr["Insert_Nurse_Time"])
                            : String.Empty,
                    Insert_Doctor_Time =
                        idr["Insert_Doctor_Time"] != DBNull.Value
                            ? Convert.ToString(idr["Insert_Doctor_Time"])
                            : String.Empty,
                    Entry_date = Convert.ToDateTime(idr["Entry_date"])
                });
        }

        return PatientFollowI;
    }

    public class PatientFollow
    {
        public int? id { get; set; }

        public int? Patient_id { get; set; }

        public int? Consultaion { get; set; }

        public int? Insert_Nurse { get; set; }

        public int? Insert_Doctor { get; set; }

        public string Shift { get; set; }

        public string Insert_Nurse_Time { get; set; }

        public string Insert_Doctor_Time { get; set; }

        public DateTime Entry_date { get; set; }
    }
}
