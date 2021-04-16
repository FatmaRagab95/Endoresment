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

public partial class _editPatients : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
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
                    Date_to =
                        idr["Date_to"] != DBNull.Value
                            ? Convert.ToString(idr["Date_to"])
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

        public string Date_to { get; set; }

        public int? Entry_user { get; set; }
    }

    // get patients data
    [WebMethod]
    public static string getPatientsData(Endorsement_PatientData data)
    {
        string config =
            Convert.ToString(ConfigurationManager.ConnectionStrings["dbcon"]);
        List<Endorsement_PatientData> Endorsement_PatientData =
            new List<Endorsement_PatientData>();

        SqlConnection con = new SqlConnection(config);

        con.Open();

        using (
            SqlCommand cmd =
                new SqlCommand("select * from Endorsement_PatientData WHERE Patient_Status = 1 and Branch_id = @Branch_id and Unit in (select U_name from Units where U_id in (select Area_id from adminusers where Emp_ID = @Unit))",
                    con)
        )
        {
            cmd.Parameters.Add("@Unit", SqlDbType.Int).Value = data.Unit;
            cmd.Parameters.Add("@Branch_id", SqlDbType.Int).Value = data.Branch_id;
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
                    Branch_id =
                        idr["Branch_id"] != DBNull.Value
                            ? Convert.ToInt32(idr["Branch_id"])
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
        public int? Branch_id { get; set; }

        public string Patient_FullName { get; set; }

        public int? Bed_id { get; set; }

        public int? Unit { get; set; }
        public string Room { get; set; }

        public string Gender { get; set; }

        public Int64? Medical_Number { get; set; }

        public string Age { get; set; }

        public string Specialty { get; set; }
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
                    FullName =
                        idr["FullName"] != DBNull.Value
                            ? Convert.ToString(idr["FullName"])
                            : String.Empty
                });
        }

        return adminusersI;
    }

    public class adminusers
    {
        public int? Emp_id { get; set; }

        public string FullName { get; set; }
    }

    // insert nurse selection
    [WebMethod]
    public static string insertPatientsSelection(selectedPatient data)
    {
        string config =
            Convert.ToString(ConfigurationManager.ConnectionStrings["dbcon"]);

        List<selectedPatient> selectedPatient = new List<selectedPatient>();

        SqlConnection con = new SqlConnection(config);

        con.Open();
        using (
            SqlCommand cmd1 =
                new SqlCommand("insert into Endorsement_Nurse_patients (Patient_id, Nurse_id, Entry_user, Bed_id, Nurse_name, Date_from) values (@Patient_id, @Nurse_id, @Entry_user, @Bed_id, @Nurse_name, @Date_from)",
                    con)
        )
        {
            cmd1.Parameters.Add("@Patient_id", SqlDbType.Int).Value =
                data.Patient_id;
            cmd1.Parameters.Add("@Nurse_id", SqlDbType.Int).Value =
                data.Nurse_id;
            cmd1.Parameters.Add("@Entry_user", SqlDbType.Int).Value =
                data.Entry_user;
            cmd1.Parameters.Add("@Bed_id", SqlDbType.Int).Value = data.Bed_id;
            cmd1.Parameters.Add("@Nurse_name", SqlDbType.VarChar).Value =
                data.Nurse_name;
            cmd1.Parameters.Add("@Date_from", SqlDbType.VarChar).Value =
                data.Date_from;

            cmd1.ExecuteNonQuery();
        }
        con.Close();

        return JsonConvert.SerializeObject(selectedPatient);
    }

    public class selectedPatient
    {
        public int? Patient_id { get; set; }

        public int? Nurse_id { get; set; }

        public int? Entry_user { get; set; }

        public int? Bed_id { get; set; }

        public string Nurse_name { get; set; }

        public string Date_from { get; set; }
    }

    // update nurse selection
    [WebMethod]
    public static string updatedPatient(patientDetails detail)
    {
        string config =
            Convert.ToString(ConfigurationManager.ConnectionStrings["dbcon"]);

        List<patientDetails> patientDetails = new List<patientDetails>();

        SqlConnection con = new SqlConnection(config);

        con.Open();

        using (
            SqlCommand cmd =
                new SqlCommand("update Endorsement_Nurse_patients set Nurse_id=@Nurse_id, Patient_id=@Patient_id, Entry_user=@Entry_user, Date_to=@Date_to, Bed_id=@Bed_id, Nurse_name=@Nurse_name where id = @id;",
                    con)
        )
        {
            cmd.Parameters.AddWithValue("@id", detail.id);
            cmd.Parameters.AddWithValue("@Nurse_id", detail.Nurse_id);
            cmd.Parameters.AddWithValue("@Patient_id", detail.Patient_id);
            cmd.Parameters.AddWithValue("@Entry_user", detail.Entry_user);
            cmd.Parameters.AddWithValue("@Date_to", detail.Date_to);
            cmd.Parameters.AddWithValue("@Bed_id", detail.Bed_id);
            cmd.Parameters.AddWithValue("@Nurse_name", detail.Nurse_name);

            cmd.ExecuteNonQuery();
        }

        con.Close();

        return JsonConvert.SerializeObject(patientDetails);
    }

    public static List<patientDetails>
    populatepatientDetailsLisst(SqlDataReader idr, SqlConnection con)
    {
        List<patientDetails> patientDetailsI = new List<patientDetails>();

        while (idr.Read())
        {
            patientDetailsI
                .Add(new patientDetails {
                    id = Convert.ToInt32(idr["id"]),
                    Nurse_id = Convert.ToInt32(idr["Nurse_id"]),
                    Patient_id = Convert.ToInt32(idr["Patient_id"]),
                    Entry_user = Convert.ToInt32(idr["Entry_user"]),
                    Date_to = Convert.ToString(idr["Date_to"]),
                    Bed_id = Convert.ToInt32(idr["Bed_id"]),
                    Nurse_name = Convert.ToString(idr["Nurse_name"])
                });
        }
        return patientDetailsI;
    }

    public class patientDetails
    {
        public int? id { get; set; }

        public int? Nurse_id { get; set; }

        public int? Patient_id { get; set; }

        public int? Entry_user { get; set; }

        public string Date_to { get; set; }

        public int? Bed_id { get; set; }

        public string Nurse_name { get; set; }
    }
}
