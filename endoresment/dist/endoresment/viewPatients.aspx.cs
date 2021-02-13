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

public partial class _viewPatients : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
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