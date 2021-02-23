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

public partial class _insertPatientData : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
    }

    // get patients data
    [WebMethod]
    public static string getPatientData(Endorsement_PatientData id)
    {
        string config =  Convert.ToString(ConfigurationManager.ConnectionStrings["dbcon"]);
        List<Endorsement_PatientData> Endorsement_PatientData = new List<Endorsement_PatientData>();

        SqlConnection con = new SqlConnection(config);

        con.Open();

        using (SqlCommand cmd = new SqlCommand("select * from Endorsement_PatientData where id = @id", con))
        {
            cmd.Parameters.Add("@id", SqlDbType.Int).Value = id.id;
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

    /***********************************************************/

    // get shift data
    [WebMethod]
    public static string getShiftData(Endorsement_shiftData data)
    {
        string config =  Convert.ToString(ConfigurationManager.ConnectionStrings["dbcon"]);
		List<Endorsement_shiftData> Endorsement_shiftData = new List<Endorsement_shiftData>();

		SqlConnection con = new SqlConnection(config);

		con.Open();
        using (SqlCommand cmd1 = new SqlCommand("insert into Endorsement_PatientFollow (Shift, Patient_id, Diet_id, Diet_Name,Pain,P_Isolation,Allergy,Investegation_ToDo,Investegation_FollowUp,Contraptions_Infusions,Routise_PlanOfCare,DR_Diagnosis,DR_ProgressNotes,DR_Consultaion_Progress,Insert_Nurse,Insert_Nurse_Time,Insert_Doctor,Insert_Doctor_Time,Consultaion,Transfer_From,Transfer_To) values (@Shift, @Patient_id,@Diet_id, @Diet_Name, @Pain, @P_Isolation, @Allergy, @Investegation_ToDo, @Investegation_FollowUp, @Contraptions_Infusions, @Routise_PlanOfCare, @DR_Diagnosis, @DR_ProgressNotes, @DR_Consultaion_Progress, @Insert_Nurse, @Insert_Nurse_Time, @Insert_Doctor, @Insert_Doctor_Time, @Consultaion, @Transfer_From, @Transfer_To)", con))

        {
            cmd1.Parameters.Add("@Shift", SqlDbType.VarChar).Value = data.Shift;
            cmd1.Parameters.Add("@Patient_id", SqlDbType.Int).Value  = data.Patient_id;
            cmd1.Parameters.Add("@Diet_id", SqlDbType.Int).Value = data.Diet_id;
            cmd1.Parameters.Add("@Diet_Name", SqlDbType.VarChar).Value = data.Diet_Name;
            cmd1.Parameters.Add("@Pain", SqlDbType.Int).Value   = data.Pain;
            cmd1.Parameters.Add("@P_Isolation", SqlDbType.VarChar).Value   = data.P_Isolation;
            cmd1.Parameters.Add("@Allergy", SqlDbType.VarChar).Value   = data.Allergy;
            cmd1.Parameters.Add("@Investegation_ToDo", SqlDbType.VarChar).Value   = data.Investegation_ToDo;
            cmd1.Parameters.Add("@Investegation_FollowUp", SqlDbType.Int).Value  = data.Investegation_FollowUp;
            cmd1.Parameters.Add("@Contraptions_Infusions", SqlDbType.VarChar).Value   = data.Contraptions_Infusions;
            cmd1.Parameters.Add("@Routise_PlanOfCare", SqlDbType.VarChar).Value   = data.Routise_PlanOfCare;
            cmd1.Parameters.Add("@DR_Diagnosis", SqlDbType.VarChar).Value   = data.DR_Diagnosis;
            cmd1.Parameters.Add("@DR_ProgressNotes", SqlDbType.VarChar).Value  = data.DR_ProgressNotes;
            cmd1.Parameters.Add("@DR_Consultaion_Progress", SqlDbType.VarChar).Value  = data.DR_Consultaion_Progress;
            cmd1.Parameters.Add("@Insert_Nurse", SqlDbType.VarChar).Value  = data.Insert_Nurse;
            cmd1.Parameters.Add("@Insert_Nurse_Time", SqlDbType.VarChar).Value  = data.Insert_Nurse_Time;
            cmd1.Parameters.Add("@Insert_Doctor", SqlDbType.VarChar).Value  = data.Insert_Doctor;
            cmd1.Parameters.Add("@Insert_Doctor_Time", SqlDbType.VarChar).Value  = data.Insert_Doctor_Time;
            cmd1.Parameters.Add("@Consultaion", SqlDbType.VarChar).Value  = data.Consultaion;
            cmd1.Parameters.Add("@Transfer_From", SqlDbType.VarChar).Value  = data.Transfer_From;
            cmd1.Parameters.Add("@Transfer_To", SqlDbType.VarChar).Value  = data.Transfer_To;

            cmd1.ExecuteNonQuery();

        }

        con.Close();

        return JsonConvert.SerializeObject(Endorsement_shiftData);
    }

    public class Endorsement_shiftData
    {
        public int? id { get; set; }
        public string Shift { get; set; }
        public int? Patient_id { get; set; }
        public int? Diet_id { get; set; }
        public string Diet_Name { get; set; }
        public int? Pain { get; set; }
        public string P_Isolation { get; set; }
        public string Allergy { get; set; }
        public string Investegation_ToDo { get; set; }
        public string Investegation_FollowUp { get; set; }
        public string Contraptions_Infusions { get; set; }
        public string Routise_PlanOfCare { get; set; }
        public string DR_Diagnosis { get; set; }
        public string DR_ProgressNotes { get; set; }
        public string DR_Consultaion_Progress { get; set; }
        public int? Insert_Nurse { get; set; }
        public string Insert_Nurse_Time { get; set; }
        public int? Insert_Doctor { get; set; }
        public string Insert_Doctor_Time { get; set; }
        public int? Consultaion { get; set; }
        public string Transfer_From { get; set; }
        public string Transfer_To { get; set; }
    }
}