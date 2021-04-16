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

public partial class _Nurses : System.Web.UI.Page
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

    // get Endoresment_Nurses_Units
    [WebMethod]
    public static string
    getEndoresment_Nurses_UnitsData(Endoresment_Nurses_Units nurse)
    {
        string config =
            Convert.ToString(ConfigurationManager.ConnectionStrings["dbcon"]);
        List<Endoresment_Nurses_Units> Endoresment_Nurses_Units =
            new List<Endoresment_Nurses_Units>();

        SqlConnection con = new SqlConnection(config);

        con.Open();

        using (
            SqlCommand cmd =
                new SqlCommand("select * from Endoresment_Nurses_Units where Active = 1 and Unit_id = @Unit_id",
                    con)
        )
        {
            cmd.Parameters.Add("@Unit_id", SqlDbType.Int).Value = nurse.Unit_id;
            SqlDataReader idr = cmd.ExecuteReader();

            if (idr.HasRows)
            {
                Endoresment_Nurses_Units =
                    populateEndoresment_Nurses_UnitsLisst(idr, con);
            }
        }

        con.Close();

        return JsonConvert.SerializeObject(Endoresment_Nurses_Units);
    }

    public static List<Endoresment_Nurses_Units>
    populateEndoresment_Nurses_UnitsLisst(SqlDataReader idr, SqlConnection con)
    {
        List<Endoresment_Nurses_Units> Endoresment_Nurses_UnitsI =
            new List<Endoresment_Nurses_Units>();

        while (idr.Read())
        {
            Endoresment_Nurses_UnitsI
                .Add(new Endoresment_Nurses_Units {
                    Id =
                        idr["Id"] != DBNull.Value
                            ? Convert.ToInt32(idr["Id"])
                            : 0,
                    Nurse_id =
                        idr["Nurse_id"] != DBNull.Value
                            ? Convert.ToInt32(idr["Nurse_id"])
                            : 0,
                    Unit_id =
                        idr["Unit_id"] != DBNull.Value
                            ? Convert.ToInt32(idr["Unit_id"])
                            : 0,
                    Entry_user =
                        idr["Entry_user"] != DBNull.Value
                            ? Convert.ToInt32(idr["Entry_user"])
                            : 0,
                    Active =
                        idr["Active"] != DBNull.Value
                            ? Convert.ToInt32(idr["Active"])
                            : 1
                });
        }

        return Endoresment_Nurses_UnitsI;
    }

    public class Endoresment_Nurses_Units
    {
        public int? Id { get; set; }

        public int? Nurse_id { get; set; }

        public int? Unit_id { get; set; }

        public int? Entry_user { get; set; }

        public int? Active { get; set; }
    }

    // get admin users data
    [WebMethod]
    public static string getadminusersData()
    {
        string config =
            Convert.ToString(ConfigurationManager.ConnectionStrings["dbcon"]);
        List<adminusers> adminusers = new List<adminusers>();

        SqlConnection con = new SqlConnection(config);

        con.Open();

        using (
            SqlCommand cmd =
                new SqlCommand("select * from adminusers where Role_id = 12 or Role_id = 17",
                    con)
        )
        {
            SqlDataReader idr = cmd.ExecuteReader();

            if (idr.HasRows)
            {
                adminusers = populateadminusersLisst(idr, con);
            }
        }

        con.Close();

        return JsonConvert.SerializeObject(adminusers);
    }

    public static List<adminusers>
    populateadminusersLisst(SqlDataReader idr, SqlConnection con)
    {
        List<adminusers> adminusersI = new List<adminusers>();

        while (idr.Read())
        {
            adminusersI
                .Add(new adminusers {
                    Emp_id =
                        idr["Emp_id"] != DBNull.Value
                            ? Convert.ToInt32(idr["Emp_id"])
                            : 0,
                    Role_id =
                        idr["Role_id"] != DBNull.Value
                            ? Convert.ToInt32(idr["Role_id"])
                            : 0,
                    FullName =
                        idr["FullName"] != DBNull.Value
                            ? Convert.ToString(idr["FullName"])
                            : String.Empty,
                    Branch_ID =
                        idr["Branch_ID"] != DBNull.Value
                            ? Convert.ToInt32(idr["Branch_ID"])
                            : 0
                });
        }

        return adminusersI;
    }

    public class adminusers
    {
        public int? Emp_id { get; set; }

        public int? Role_id { get; set; }

        public string FullName { get; set; }

        public int? Branch_ID { get; set; }
    }

    // get Endorsement Nurse patients
    [WebMethod]
    public static string Endorsement_Nurse_patients()
    {
        string config =
            Convert.ToString(ConfigurationManager.ConnectionStrings["dbcon"]);
        List<Nurse_patients> Nurse_patients = new List<Nurse_patients>();

        SqlConnection con = new SqlConnection(config);

        con.Open();

        using (
            SqlCommand cmd =
                new SqlCommand("select * from Endorsement_Nurse_patients", con)
        )
        {
            SqlDataReader idr = cmd.ExecuteReader();

            if (idr.HasRows)
            {
                Nurse_patients = populateNurse_patientsLisst(idr, con);
            }
        }

        con.Close();

        return JsonConvert.SerializeObject(Nurse_patients);
    }

    public static List<Nurse_patients>
    populateNurse_patientsLisst(SqlDataReader idr, SqlConnection con)
    {
        List<Nurse_patients> Nurse_patientsI = new List<Nurse_patients>();

        while (idr.Read())
        {
            Nurse_patientsI
                .Add(new Nurse_patients {
                    id =
                        idr["id"] != DBNull.Value
                            ? Convert.ToInt32(idr["id"])
                            : 0,
                    Nurse_id =
                        idr["Nurse_id"] != DBNull.Value
                            ? Convert.ToInt32(idr["Nurse_id"])
                            : 0,
                    Patient_id =
                        idr["Patient_id"] != DBNull.Value
                            ? Convert.ToInt32(idr["Patient_id"])
                            : 0,
                    Bed_id =
                        idr["Bed_id"] != DBNull.Value
                            ? Convert.ToInt32(idr["Bed_id"])
                            : 0,
                    Nurse_name =
                        idr["Nurse_name"] != DBNull.Value
                            ? Convert.ToString(idr["Nurse_name"])
                            : String.Empty,
                    Entry_user =
                        idr["Entry_user"] != DBNull.Value
                            ? Convert.ToInt32(idr["Entry_user"])
                            : 0
                });
        }

        return Nurse_patientsI;
    }

    public class Nurse_patients
    {
        public int? id { get; set; }

        public int? Nurse_id { get; set; }

        public int? Patient_id { get; set; }

        public int? Bed_id { get; set; }

        public string Nurse_name { get; set; }

        public int? Entry_user { get; set; }
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
                        idr["Patient_FullName"] != DBNull.Value
                            ? Convert.ToString(idr["Patient_FullName"])
                            : String.Empty,
                    Bed_id =
                        idr["Bed_id"] != DBNull.Value
                            ? Convert.ToInt32(idr["Bed_id"])
                            : 0,
                    Room = Convert.ToString(idr["Room"]),
                    Gender = Convert.ToString(idr["Gender"]),
                    Medical_Number =
                        idr["Medical_Number"] != DBNull.Value
                            ? Convert.ToInt64(idr["Medical_Number"])
                            : 0,
                    Age =
                        idr["Age"] != DBNull.Value
                            ? Convert.ToString(idr["Age"])
                            : String.Empty,
                    Specialty =
                        idr["Specialty"] != DBNull.Value
                            ? Convert.ToString(idr["Specialty"])
                            : String.Empty
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

        public Int64? Medical_Number { get; set; }

        public string Age { get; set; }

        public string Specialty { get; set; }
    }
}
