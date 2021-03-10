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

public partial class _PatientsNurse : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
    }

    public class user
    {
        public int Emp_id { get; set; }
        public int Branch_ID { get; set; }
    }

    // get patients data
    [WebMethod]
    public static string getPatientsData(user id)
    {
        string config =  Convert.ToString(ConfigurationManager.ConnectionStrings["dbcon"]);
        List<Endorsement_PatientData> Endorsement_PatientData = new List<Endorsement_PatientData>();
        List<user> user = new List<user>();

        SqlConnection con = new SqlConnection(config);

        con.Open();

        using (SqlCommand cmd = new SqlCommand("select * from Endorsement_PatientData where id in (select Patient_id from Endorsement_Nurse_patients where Date_to IS NULL and Nurse_id = @Nurse_id)", con))
        {
            cmd.Parameters.Add("@Nurse_id", SqlDbType.Int).Value = id.Emp_id;
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
					Patient_FullName = idr["Patient_FullName"] != DBNull.Value ? Convert.ToString(idr["Patient_FullName"]) : String.Empty,
                    Branch_id = idr["Branch_id"] != DBNull.Value ? Convert.ToInt32(idr["Branch_id"]) : 0,
					Branch_name =  idr["Branch_name"] != DBNull.Value ? Convert.ToString(idr["Branch_name"]) : String.Empty,
					Gender =  idr["Gender"] != DBNull.Value ? Convert.ToString(idr["Gender"]): String.Empty,
					Medical_Number =  idr["Medical_Number"] != DBNull.Value ? Convert.ToInt64(idr["Medical_Number"]): 0,
					Unit =  idr["Unit"] != DBNull.Value ? Convert.ToString(idr["Unit"]): String.Empty,
					Room =  idr["Room"] != DBNull.Value ? Convert.ToString(idr["Room"]): String.Empty,
					Addmission_date =  idr["Addmission_date"] != DBNull.Value ? Convert.ToString(idr["Addmission_date"]): String.Empty,
                    Age = idr["Age"] != DBNull.Value ? Convert.ToString(idr["Age"]) : String.Empty,
					Consultant_Name =  idr["Consultant_Name"] != DBNull.Value ? Convert.ToString(idr["Consultant_Name"]): String.Empty,
					Specialty =  idr["Specialty"] != DBNull.Value ? Convert.ToString(idr["Specialty"]): String.Empty,
					Date_Birth =  idr["Date_Birth"] != DBNull.Value ? Convert.ToString(idr["Date_Birth"]): String.Empty,
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
        public string Age { get; set; }
        public string Consultant_Name { get; set; }
        public string Specialty { get; set; }
        public string Date_Birth { get; set; }
    }

        // get Nurses
    [WebMethod]
    public static string getNursesData(user branch)
    {
        string config =
            Convert.ToString(ConfigurationManager.ConnectionStrings["dbcon"]);
        List<user> user = new List<user>();
        List<Nurses> Nurses = new List<Nurses>();

        SqlConnection con = new SqlConnection(config);

        con.Open();

        using (SqlCommand cmd = new SqlCommand("select * from adminusers where Role_id = 12 and Branch_ID = @Branch_id", con))
        {
            cmd.Parameters.Add("@Branch_id", SqlDbType.Int).Value = branch.Branch_ID;
            SqlDataReader idr = cmd.ExecuteReader();

            if (idr.HasRows)
            {
                Nurses = populateNursesLisst(idr, con);
            }
        }

        con.Close();

        return JsonConvert.SerializeObject(Nurses);
    }

    public static List<Nurses>
    populateNursesLisst(SqlDataReader idr, SqlConnection con)
    {
        List<Nurses> NursesI = new List<Nurses>();

        while (idr.Read())
        {
            NursesI
                .Add(new Nurses {
                    Emp_ID = idr["Emp_ID"] != DBNull.Value ? Convert.ToInt32(idr["Emp_ID"]) : 0,
                    FullName = Convert.ToString(idr["FullName"]),
                });
        }

        return NursesI;
    }

    public class Nurses
    {
        public int? Emp_ID { get; set; }
        public string FullName { get; set; }
    }
}