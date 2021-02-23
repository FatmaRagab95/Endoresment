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

public partial class _Handover : System.Web.UI.Page
{
    public int unitId = 0;
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    // current user
        public class user
    {
        public int Emp_id { get; set; }
    }

    // get unit dashboard
    [WebMethod]
    public static string getUnitDashData(user chargeNurse)
    {
        string config = Convert.ToString(ConfigurationManager.ConnectionStrings["dbcon"]);
        List<UnitsDash> UnitsDash = new List<UnitsDash>();
        List<user> user = new List<user>();

        SqlConnection con = new SqlConnection(config);

        con.Open();

        using (SqlCommand cmd = new SqlCommand("SELECT * from Endorsement_UnitsDashboard where Shift_date >= DATEADD(day,-1, GETDATE()) and Unit_id in (select TOP(20) Unit_id from Endorsement_UnitsDashboard where Receive_ChargeNurse_id = @Receive_ChargeNurse_id and Shift_date >=   DATEADD(day,-1, GETDATE()) order by id desc) order by id desc", con))
        {
            cmd.Parameters.Add("@Receive_ChargeNurse_id", SqlDbType.Int).Value = chargeNurse.Emp_id;
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
                    Shift_date = Convert.ToString(idr["Shift_date"]),
                    Endorsing_ChargeNurse = Convert.ToString(idr["Endorsing_ChargeNurse"]),
                    Receive_ChargeNurse = Convert.ToString(idr["Receive_ChargeNurse"]),
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
        public string Shift_date { get; set; }
        public string Endorsing_ChargeNurse { get; set; }
        public string Receive_ChargeNurse { get; set; }
        public int? Total_Census { get; set; }
        public int? Received { get; set; }
        public int? Admission { get; set; }
        public int? Transfer_In { get; set; }
        public int? Transfer_Out { get; set; }
    }

    // get patients data
    [WebMethod]
    public static string getPatientsData(user chargeNurse)
    {
        string config =  Convert.ToString(ConfigurationManager.ConnectionStrings["dbcon"]);
        List<Endorsement_PatientData> Endorsement_PatientData = new List<Endorsement_PatientData>();

        SqlConnection con = new SqlConnection(config);

        con.Open();

        using (SqlCommand cmd = new SqlCommand("select * from Endorsement_PatientData where Patient_Status = 1 and  Unit in (select TOP(20) Unit_name from Endorsement_UnitsDashboard where Receive_ChargeNurse_id = @Receive_ChargeNurse_id order by id desc)", con))
        {
            cmd.Parameters.Add("@Receive_ChargeNurse_id", SqlDbType.Int).Value = chargeNurse.Emp_id;
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
        List<user> user = new List<user>();

        SqlConnection con = new SqlConnection(config);

        con.Open();

        using (SqlCommand cmd = new SqlCommand("select * from Endorsement_PatientFollow where Patient_id = @Patient_id order by id", con))
        {
            cmd.Parameters.Add("@Patient_id", SqlDbType.Int).Value = data.Patient_id;
            SqlDataReader idr = cmd.ExecuteReader();

            if (idr.HasRows)
            {
                Endorsement_shiftData = populateEndorsement_shiftDataLisst(idr, con);
            }
        }

        con.Close();

        return JsonConvert.SerializeObject(Endorsement_shiftData);
    }

    public static List<Endorsement_shiftData>
    populateEndorsement_shiftDataLisst(SqlDataReader idr, SqlConnection con)
    {
        List<Endorsement_shiftData> Endorsement_shiftDataI = new List<Endorsement_shiftData>();

        while (idr.Read())
        {
            Endorsement_shiftDataI
                .Add(new Endorsement_shiftData {
                    id       = idr["id"] != DBNull.Value ? Convert.ToInt32(idr["id"]) : 0,
					Shift       = Convert.ToString(idr["Shift"]),
                    Patient_id  = idr["Patient_id"] != DBNull.Value ? Convert.ToInt32(idr["Patient_id"]) : 0,
                    Diet_id     = idr["Diet_id"] != DBNull.Value ? Convert.ToInt32(idr["Diet_id"]) : 0,
					Diet_Name   = Convert.ToString(idr["Diet_Name"]),
                    Pain        = idr["Pain"] != DBNull.Value ? Convert.ToInt32(idr["Pain"]) : 0,
					P_Isolation = Convert.ToString(idr["P_Isolation"]),
					Fall = Convert.ToString(idr["Fall"]),
					Allergy     = Convert.ToString(idr["Allergy"]),

					Investegation_ToDo      = Convert.ToString(idr["Investegation_ToDo"]),
					Investegation_FollowUp  = Convert.ToString(idr["Investegation_FollowUp"]),
					Contraptions_Infusions  = Convert.ToString(idr["Contraptions_Infusions"]),
					Routise_PlanOfCare      = Convert.ToString(idr["Routise_PlanOfCare"]),
					Surgury_Procedures      = Convert.ToString(idr["Surgury_Procedures"]),
					DR_Diagnosis            = Convert.ToString(idr["DR_Diagnosis"]),
					DR_ProgressNotes        = Convert.ToString(idr["DR_ProgressNotes"]),
					DR_Consultaion_Progress = Convert.ToString(idr["DR_Consultaion_Progress"]),

                    Update_Nurse       = idr["Update_Nurse"] != DBNull.Value ? Convert.ToInt32(idr["Update_Nurse"]) : 0,
					Update_Nurse_Time  = Convert.ToString(idr["Update_Nurse_Time"]),
					Specialty          = Convert.ToString(idr["Specialty"]),
                    Update_Doctor      = idr["Update_Doctor"] != DBNull.Value ? Convert.ToInt32(idr["Update_Doctor"]) : 0,
                    Insert_Doctor_Time = Convert.ToString(idr["Insert_Doctor_Time"]),
                    Consultaion        = idr["Consultaion"] != DBNull.Value ? Convert.ToInt32(idr["Consultaion"]) : 0,
					Transfer_From      = Convert.ToString(idr["Transfer_From"]),
					Transfer_To        = Convert.ToString(idr["Transfer_To"]),
					Entry_date         = Convert.ToString(idr["Entry_date"]),
                });
        }

        return Endorsement_shiftDataI;
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
        public string Fall { get; set; }
        public string Allergy { get; set; }
        public string Investegation_ToDo { get; set; }
        public string Investegation_FollowUp { get; set; }
        public string Contraptions_Infusions { get; set; }
        public string Routise_PlanOfCare { get; set; }
        public string Surgury_Procedures { get; set; }
        public string DR_Diagnosis { get; set; }
        public string DR_ProgressNotes { get; set; }
        public string DR_Consultaion_Progress { get; set; }
        public int? Update_Nurse { get; set; }
        public string Update_Nurse_Time { get; set; }
        public string Specialty { get; set; }
        public int? Update_Doctor { get; set; }
        public int? Update_Doctor_Time { get; set; }
        public int? Insert_Nurse { get; set; }
        public string Insert_Nurse_Time { get; set; }
        public int? Insert_Doctor { get; set; }
        public string Insert_Doctor_Time { get; set; }
        public int? Consultaion { get; set; }
        public string Transfer_From { get; set; }
        public string Transfer_To { get; set; }
        public string Entry_date { get; set; }
    }
}