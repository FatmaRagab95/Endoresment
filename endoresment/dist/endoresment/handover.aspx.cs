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
        public int Branch_ID { get; set; }
    }

    // current branch
        public class branches
    {
        public int? id { get; set; }
        public string Branch_EName { get; set; }
    }

    // get unit dashboard
    // if user is a charge nurse and has units at this shift
    [WebMethod]
    public static string getUnitDashData(user chargeNurse)
    {
        string config = Convert.ToString(ConfigurationManager.ConnectionStrings["dbcon"]);
        List<UnitsDash> UnitsDash = new List<UnitsDash>();
        List<user> user = new List<user>();

        SqlConnection con = new SqlConnection(config);

        con.Open();

        using (SqlCommand cmd = new SqlCommand("SELECT * from Endorsement_UnitsDashboard where Shift_date >= DATEADD(day,-2, GETDATE()) and Unit_id in (select TOP(20) Unit_id from Endorsement_UnitsDashboard where Receive_ChargeNurse_id = @Receive_ChargeNurse_id and Shift_date >=   DATEADD(day,-2, GETDATE()) order by id desc) order by id desc", con))
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

    // get unit dashboard
    // if user is a charge nurse but has no units at this shift
    // or if the user is not a charge nurse
    [WebMethod]
    public static string getUnitDashData2(user id)
    {
        string config = Convert.ToString(ConfigurationManager.ConnectionStrings["dbcon"]);
        List<UnitsDash> UnitsDash = new List<UnitsDash>();
        List<user> user = new List<user>();

        SqlConnection con = new SqlConnection(config);

        con.Open();

        using (SqlCommand cmd1 = new SqlCommand("select Endorsement_UnitsDashboard.* from Endorsement_UnitsDashboard where Branch_ID = @Branch_ID and  Endorsement_UnitsDashboard.Entry_date = (select max(t2.Entry_date) from Endorsement_UnitsDashboard t2 where t2.Unit_id = Endorsement_UnitsDashboard.Unit_id) order by id desc", con))
        {
           // cmd1.Parameters.Add("@Branch_ID", SqlDbType.Int).Value = id.Branch_ID;
           cmd1.Parameters.Add("@Branch_ID", SqlDbType.Int).Value = 1;
            SqlDataReader idr = cmd1.ExecuteReader();

            
            if (idr.HasRows)
            {
                UnitsDash = populateUnitsDashLisst(idr, con);

            }
        }

        con.Close();  

        return JsonConvert.SerializeObject(UnitsDash);
    }

    // get unit dashboard
    // if user is a nurse
    [WebMethod]
    public static string getUnitDashData3(user id)
    {
        string config = Convert.ToString(ConfigurationManager.ConnectionStrings["dbcon"]);
        List<UnitsDash> UnitsDash = new List<UnitsDash>();
        List<user> user = new List<user>();

        SqlConnection con = new SqlConnection(config);

        con.Open();

        using (SqlCommand cmd1 = new SqlCommand("SELECT * from Endorsement_UnitsDashboard where Shift_date >= DATEADD(day,-2, GETDATE()) and Unit_id in (select TOP(20) Unit_id from Endoresment_Nurses_Units where Nurse_id = @Nurse_id and Shift_date >=   DATEADD(day,-2, GETDATE()) order by id desc) order by id desc", con))
        {
            cmd1.Parameters.Add("@Nurse_id", SqlDbType.Int).Value = id.Emp_id;
            SqlDataReader idr = cmd1.ExecuteReader();

            
            if (idr.HasRows)
            {
                UnitsDash = populateUnitsDashLisst(idr, con);

            } else {
                con.Close(); 
                con.Open();

                using (SqlCommand cmd2 = new SqlCommand("select Endorsement_UnitsDashboard.* from Endorsement_UnitsDashboard where Unit_id in (select TOP(20) Unit_id from Endoresment_Nurses_Units where Nurse_id = @Nurse_id) and Branch_ID = @Branch_ID and  Endorsement_UnitsDashboard.Entry_date = (select max(t2.Entry_date) from Endorsement_UnitsDashboard t2 where t2.Unit_id = Endorsement_UnitsDashboard.Unit_id) order by id desc", con))
                {
                cmd2.Parameters.Add("@Nurse_id", SqlDbType.Int).Value = id.Emp_id;
                cmd2.Parameters.Add("@Branch_ID", SqlDbType.Int).Value = 1;
                    SqlDataReader idr1 = cmd2.ExecuteReader();

                    
                    if (idr1.HasRows)
                    {
                        UnitsDash = populateUnitsDashLisst(idr1, con);

                    }
                }

                con.Close(); 
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
                    Unit_id = idr["Unit_id"] != DBNull.Value ? Convert.ToInt32(idr["Unit_id"]) : 0,
                    Shift = Convert.ToString(idr["Shift"]),
                    Shift_date = Convert.ToString(idr["Shift_date"]),
                    Endorsing_ChargeNurse = Convert.ToString(idr["Endorsing_ChargeNurse"]),
                    Receive_ChargeNurse = Convert.ToString(idr["Receive_ChargeNurse"]),
                    Total_Census = idr["Total_Census"] != DBNull.Value ? Convert.ToInt32(idr["Total_Census"]) : 0,
                    Received = idr["Received"] != DBNull.Value ? Convert.ToInt32(idr["Received"]) : 0,
                    Admission = idr["Admission"] != DBNull.Value ? Convert.ToInt32(idr["Admission"]) : 0,
                    Transfer_In = idr["Transfer_In"] != DBNull.Value ? Convert.ToInt32(idr["Transfer_In"]) : 0,
                    Confirm = idr["Confirm"] != DBNull.Value ? Convert.ToBoolean(idr["Confirm"]) : false,

                });
        }

        return UnitsDashI;
    }

    // update confirmation
    [WebMethod]
    public static string confirmEndorsing(UnitsDash id)
    {
        string config = Convert.ToString(ConfigurationManager.ConnectionStrings["dbcon"]);
        List<UnitsDash> UnitsDash = new List<UnitsDash>();

        SqlConnection con = new SqlConnection(config);

        con.Open();

        using (SqlCommand cmd1 = new SqlCommand("update Endorsement_UnitsDashboard set Confirm = 'True' where id = @id", con))
        {
            cmd1.Parameters.Add("@id", SqlDbType.Int).Value = id.id;
            SqlDataReader idr = cmd1.ExecuteReader();
            
        }

        con.Close();  

        return JsonConvert.SerializeObject(UnitsDash);
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
        public Boolean Confirm { get; set; }
    }

    // update patients data
    [WebMethod]
    public static string updatePatientData(Endorsement_PatientData patient)
    {
        string config =  Convert.ToString(ConfigurationManager.ConnectionStrings["dbcon"]);
        List<Endorsement_PatientData> Endorsement_PatientData = new List<Endorsement_PatientData>();

        SqlConnection con = new SqlConnection(config);

        con.Open();

        using (SqlCommand cmd = new SqlCommand("update Endorsement_PatientData set Patient_FullName = @Patient_FullName,Age = @Age, Addmission_date = @Addmission_date, Consultant_Name = @Consultant_Name where id = @id ", con))
        {
            cmd.Parameters.Add("@id", SqlDbType.Int).Value = patient.id;
            cmd.Parameters.Add("@Patient_FullName", SqlDbType.VarChar).Value = patient.Patient_FullName;
            cmd.Parameters.Add("@Age", SqlDbType.VarChar).Value = patient.Age;
            cmd.Parameters.Add("@Addmission_date", SqlDbType.VarChar).Value = patient.Addmission_date;
            cmd.Parameters.Add("@Consultant_Name", SqlDbType.VarChar).Value = patient.Consultant_Name;
            SqlDataReader idr = cmd.ExecuteReader();

            if (idr.HasRows)
            {
                Endorsement_PatientData = populateEndorsement_PatientDataLisst(idr, con);
            }
        }

        con.Close();

        return JsonConvert.SerializeObject(Endorsement_PatientData);
    }

    // get patients data
    [WebMethod]
    public static string getPatientsData(user chargeNurse)
    {
        string config =  Convert.ToString(ConfigurationManager.ConnectionStrings["dbcon"]);
        List<Endorsement_PatientData> Endorsement_PatientData = new List<Endorsement_PatientData>();

        SqlConnection con = new SqlConnection(config);

        con.Open();

        using (SqlCommand cmd = new SqlCommand("select * from Endorsement_PatientData where Patient_Status = 1 and  Unit in (select TOP(20) Unit_name from Endorsement_UnitsDashboard where Receive_ChargeNurse_id = @Receive_ChargeNurse_id order by id desc) and Branch_id = @Branch_ID", con))
        {
            cmd.Parameters.Add("@Receive_ChargeNurse_id", SqlDbType.Int).Value = chargeNurse.Emp_id;
            cmd.Parameters.Add("@Branch_ID", SqlDbType.Int).Value = chargeNurse.Branch_ID;
            SqlDataReader idr = cmd.ExecuteReader();

            if (idr.HasRows)
            {
                Endorsement_PatientData = populateEndorsement_PatientDataLisst(idr, con);
            }
        }

        con.Close();

        return JsonConvert.SerializeObject(Endorsement_PatientData);
    }

    [WebMethod]
    public static string getPatientsData2(user chargeNurse)
    {
        string config =  Convert.ToString(ConfigurationManager.ConnectionStrings["dbcon"]);
        List<Endorsement_PatientData> Endorsement_PatientData = new List<Endorsement_PatientData>();

        SqlConnection con = new SqlConnection(config);

        con.Open();

        using (SqlCommand cmd = new SqlCommand("select * from Endorsement_PatientData where Patient_Status = 1 and Branch_id = @Branch_ID", con))
        {
            cmd.Parameters.Add("@Branch_ID", SqlDbType.Int).Value = chargeNurse.Branch_ID;
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

    // insert follow up data
    [WebMethod]
    public static string insertFollowUpData(Endorsement_shiftData patient)
    {
        string config =  Convert.ToString(ConfigurationManager.ConnectionStrings["dbcon"]);
        List<Endorsement_shiftData> Endorsement_shiftData = new List<Endorsement_shiftData>();

        SqlConnection con = new SqlConnection(config);

        con.Open();

        using (SqlCommand cmd = new SqlCommand("insert into Endorsement_PatientFollow  (Patient_id,Shift,Diet_id, Diet_Name, Pain, P_Isolation, Fall, Allergy,Investegation_ToDo, Investegation_FollowUp,Contraptions_Infusions, Routise_PlanOfCare,Surgury_Procedures,DR_Diagnosis,DR_ProgressNotes, DR_Consultaion_Progress,Insert_Nurse,Insert_Nurse_Name,Insert_Nurse_Time,Insert_Doctor,Insert_Doctor_Name,Insert_Doctor_Time) values(@Patient_id, @Shift,@Diet_id,@Diet_Name,@Pain,@P_Isolation,@Fall, @Allergy,@Investegation_ToDo, @Investegation_FollowUp, @Contraptions_Infusions, @Routise_PlanOfCare, @Surgury_Procedures, @DR_Diagnosis, @DR_ProgressNotes, @DR_Consultaion_Progress, @Insert_Nurse,@Insert_Nurse_Name,@Insert_Nurse_Time,@Insert_Doctor,@Insert_Doctor_Name, @Insert_Doctor_Time ) ", con))
        {
            cmd.Parameters.Add("@Patient_id", SqlDbType.Int).Value = patient.Patient_id;
            cmd.Parameters.Add("@Shift", SqlDbType.VarChar).Value = patient.Shift;
            cmd.Parameters.Add("@Diet_id", SqlDbType.Int).Value = patient.Diet_id;
            cmd.Parameters.Add("@Diet_Name", SqlDbType.VarChar).Value = patient.Diet_Name;
            cmd.Parameters.Add("@Pain", SqlDbType.Int).Value = patient.Pain;
            cmd.Parameters.Add("@P_Isolation", SqlDbType.VarChar).Value = patient.P_Isolation;
            cmd.Parameters.Add("@Fall", SqlDbType.VarChar).Value = patient.Fall;
            cmd.Parameters.Add("@Allergy", SqlDbType.VarChar).Value = patient.Allergy;
            cmd.Parameters.Add("@Investegation_ToDo", SqlDbType.VarChar).Value = patient.Investegation_ToDo;
            cmd.Parameters.Add("@Investegation_FollowUp", SqlDbType.VarChar).Value = patient.Investegation_FollowUp;
            cmd.Parameters.Add("@Contraptions_Infusions", SqlDbType.VarChar).Value = patient.Contraptions_Infusions;
            cmd.Parameters.Add("@Routise_PlanOfCare", SqlDbType.VarChar).Value = patient.Routise_PlanOfCare;
            cmd.Parameters.Add("@Surgury_Procedures", SqlDbType.VarChar).Value = patient.Surgury_Procedures;
            cmd.Parameters.Add("@DR_Diagnosis", SqlDbType.VarChar).Value = patient.DR_Diagnosis;
            cmd.Parameters.Add("@DR_ProgressNotes", SqlDbType.VarChar).Value = patient.DR_ProgressNotes;
            cmd.Parameters.Add("@DR_Consultaion_Progress", SqlDbType.VarChar).Value = patient.DR_Consultaion_Progress;
            cmd.Parameters.Add("@Insert_Nurse", SqlDbType.Int).Value = patient.Insert_Nurse;
            cmd.Parameters.Add("@Insert_Nurse_Name", SqlDbType.VarChar).Value = patient.Insert_Nurse_Name;
            cmd.Parameters.Add("@Insert_Nurse_Time", SqlDbType.VarChar).Value = patient.Insert_Nurse_Time;
            cmd.Parameters.Add("@Insert_Doctor", SqlDbType.Int).Value = patient.Insert_Doctor;
            cmd.Parameters.Add("@Insert_Doctor_Name", SqlDbType.VarChar).Value = patient.Insert_Doctor_Name;
            cmd.Parameters.Add("@Insert_Doctor_Time", SqlDbType.VarChar).Value = patient.Insert_Doctor_Time;
            SqlDataReader idr = cmd.ExecuteReader();

            if (idr.HasRows)
            {
                Endorsement_shiftData = populateEndorsement_shiftDataLisst(idr, con);
            }
        }

        con.Close();

        return JsonConvert.SerializeObject(Endorsement_shiftData);
    }

    // update follow up data
    [WebMethod]
    public static string updateFollowUpData(Endorsement_shiftData patient)
    {
        string config =  Convert.ToString(ConfigurationManager.ConnectionStrings["dbcon"]);
        List<Endorsement_shiftData> Endorsement_shiftData = new List<Endorsement_shiftData>();

        SqlConnection con = new SqlConnection(config);

        con.Open();

        using (SqlCommand cmd = new SqlCommand("update Endorsement_PatientFollow set Shift = @Shift,Diet_id = @Diet_id, Diet_Name = @Diet_Name, Pain = @Pain, P_Isolation=@P_Isolation, Fall = @Fall, Allergy= @Allergy,Investegation_ToDo = @Investegation_ToDo, Investegation_FollowUp = @Investegation_FollowUp,Contraptions_Infusions = @Contraptions_Infusions, Routise_PlanOfCare=@Routise_PlanOfCare,Surgury_Procedures = @Surgury_Procedures,DR_Diagnosis = @DR_Diagnosis,DR_ProgressNotes = @DR_ProgressNotes, DR_Consultaion_Progress = @DR_Consultaion_Progress, Update_Nurse = @Update_Nurse, Update_Nurse_Name = @Update_Nurse_Name, Update_Nurse_Time = @Update_Nurse_Time, Update_Doctor = @Update_Doctor, Update_Doctor_Name=@Update_Doctor_Name,Update_Doctor_Time = @Update_Doctor_Time where id = @id ", con))
        {
            cmd.Parameters.Add("@id", SqlDbType.Int).Value = patient.id;
            cmd.Parameters.Add("@Shift", SqlDbType.VarChar).Value = patient.Shift;
            cmd.Parameters.Add("@Diet_id", SqlDbType.Int).Value = patient.Diet_id;
            cmd.Parameters.Add("@Diet_Name", SqlDbType.VarChar).Value = patient.Diet_Name;
            cmd.Parameters.Add("@Pain", SqlDbType.Int).Value = patient.Pain;
            cmd.Parameters.Add("@P_Isolation", SqlDbType.VarChar).Value = patient.P_Isolation;
            cmd.Parameters.Add("@Fall", SqlDbType.VarChar).Value = patient.Fall;
            cmd.Parameters.Add("@Allergy", SqlDbType.VarChar).Value = patient.Allergy;
            cmd.Parameters.Add("@Investegation_ToDo", SqlDbType.VarChar).Value = patient.Investegation_ToDo;
            cmd.Parameters.Add("@Investegation_FollowUp", SqlDbType.VarChar).Value = patient.Investegation_FollowUp;
            cmd.Parameters.Add("@Contraptions_Infusions", SqlDbType.VarChar).Value = patient.Contraptions_Infusions;
            cmd.Parameters.Add("@Routise_PlanOfCare", SqlDbType.VarChar).Value = patient.Routise_PlanOfCare;
            cmd.Parameters.Add("@Surgury_Procedures", SqlDbType.VarChar).Value = patient.Surgury_Procedures;
            cmd.Parameters.Add("@DR_Diagnosis", SqlDbType.VarChar).Value = patient.DR_Diagnosis;
            cmd.Parameters.Add("@DR_ProgressNotes", SqlDbType.VarChar).Value = patient.DR_ProgressNotes;
            cmd.Parameters.Add("@DR_Consultaion_Progress", SqlDbType.VarChar).Value = patient.DR_Consultaion_Progress;
            cmd.Parameters.Add("@Update_Nurse", SqlDbType.VarChar).Value = patient.Update_Nurse;
            cmd.Parameters.Add("@Update_Nurse_Name", SqlDbType.VarChar).Value = patient.Update_Nurse_Name;
            cmd.Parameters.Add("@Update_Nurse_Time", SqlDbType.VarChar).Value = patient.Update_Nurse_Time;
            cmd.Parameters.Add("@Update_Doctor", SqlDbType.Int).Value = patient.Update_Doctor;
            cmd.Parameters.Add("@Update_Doctor_Name", SqlDbType.VarChar).Value = patient.Update_Doctor_Name;
            cmd.Parameters.Add("@Update_Doctor_Time", SqlDbType.VarChar).Value = patient.Update_Doctor_Time;
            SqlDataReader idr = cmd.ExecuteReader();

            if (idr.HasRows)
            {
                Endorsement_shiftData = populateEndorsement_shiftDataLisst(idr, con);
            }
        }

        con.Close();

        return JsonConvert.SerializeObject(Endorsement_shiftData);
    }

    // get follow up data
    [WebMethod]
    public static string getShiftData(Endorsement_shiftData data)
    {
        string config =  Convert.ToString(ConfigurationManager.ConnectionStrings["dbcon"]);
        List<Endorsement_shiftData> Endorsement_shiftData = new List<Endorsement_shiftData>();
        List<user> user = new List<user>();

        SqlConnection con = new SqlConnection(config);

        con.Open();

        using (SqlCommand cmd = new SqlCommand("select top(1) * from Endorsement_PatientFollow where Patient_id = @Patient_id order by id desc", con))
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
                    
                    
                    Insert_Nurse       = idr["Insert_Nurse"] != DBNull.Value ? Convert.ToInt32(idr["Insert_Nurse"]) : 0,
					Insert_Nurse_Name  = Convert.ToString(idr["Insert_Nurse_Name"]),
					Insert_Nurse_Time  = Convert.ToString(idr["Insert_Nurse_Time"]),

                    Update_Nurse       = idr["Update_Nurse"] != DBNull.Value ? Convert.ToInt32(idr["Update_Nurse"]) : 0,
					Update_Nurse_Name  = Convert.ToString(idr["Update_Nurse_Name"]),
					Update_Nurse_Time  = Convert.ToString(idr["Update_Nurse_Time"]),

                    Insert_Doctor      = idr["Insert_Doctor"] != DBNull.Value ? Convert.ToInt32(idr["Insert_Doctor"]) : 0,
                    Insert_Doctor_Name = Convert.ToString(idr["Insert_Doctor_Name"]),
                    Insert_Doctor_Time = Convert.ToString(idr["Insert_Doctor_Time"]),

                    Update_Doctor      = idr["Update_Doctor"] != DBNull.Value ? Convert.ToInt32(idr["Update_Doctor"]) : 0,
                    Update_Doctor_Name = Convert.ToString(idr["Update_Doctor_Name"]),
                    Update_Doctor_Time = Convert.ToString(idr["Update_Doctor_Time"]),

                    Consultaion        = idr["Consultaion"] != DBNull.Value ? Convert.ToInt32(idr["Consultaion"]) : 0,

                    Entry_date = Convert.ToDateTime(idr["Entry_date"]),
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
        public string Update_Nurse_Name { get; set; }
        public string Update_Nurse_Time { get; set; }
        public int? Update_Doctor { get; set; }
        public string Update_Doctor_Name { get; set; }
        public string Update_Doctor_Time { get; set; }
        public int? Insert_Nurse { get; set; }
        public string Insert_Nurse_Name { get; set; }
        public string Insert_Nurse_Time { get; set; }
        public int? Insert_Doctor { get; set; }
        public string Insert_Doctor_Name { get; set; }
        public string Insert_Doctor_Time { get; set; }
        public int? Consultaion { get; set; }
        public string Transfer_From { get; set; }
        public string Transfer_To { get; set; }
        public DateTime Entry_date { get; set; }
    }

    // get diets
    [WebMethod]
    public static string getDietsData()
    {
        string config = Convert.ToString(ConfigurationManager.ConnectionStrings["dbcon"]);
        List<Diets> Diets = new List<Diets>();

        SqlConnection con = new SqlConnection(config);

        con.Open();

        using (SqlCommand cmd = new SqlCommand("SELECT * from Endorsement_Diet", con))
        {
            SqlDataReader idr = cmd.ExecuteReader();

            if (idr.HasRows)
            {
                Diets = populateDietsLisst(idr, con);
            }
        }

        con.Close();

        return JsonConvert.SerializeObject(Diets);
    }

    public static List<Diets>
    populateDietsLisst(SqlDataReader idr, SqlConnection con)
    {
        List<Diets> DietsI = new List<Diets>();

        while (idr.Read())
        {
            DietsI
                .Add(new Diets {
                    id = idr["id"] != DBNull.Value ? Convert.ToInt32(idr["id"]) : 0,
                    Diet_Name = Convert.ToString(idr["Diet_Name"]),

                });
        }

        return DietsI;
    }

    public class Diets
    {
        public int id { get; set; }
        public string Diet_Name { get; set; }
    }

    // get rooms
    [WebMethod]
    public static string getRoomsData(branches branch)
    {
        string config =
            Convert.ToString(ConfigurationManager.ConnectionStrings["dbcon"]);
        List<Rooms> Rooms = new List<Rooms>();
        List<branches> branches = new List<branches>();

        SqlConnection con = new SqlConnection(config);

        con.Open();

        using (
            SqlCommand cmd = new SqlCommand("select * from Endorsement_Rooms where Branch_id = @branch and id in (select Room_id from Endorsement_RoomsDashboard where Status_id = 1)", con)
        )
        {
            cmd.Parameters.Add("@branch", SqlDbType.Int).Value = branch.id;
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
                    Room_name = Convert.ToString(idr["Room_name"]),
                });
        }

        return RoomsI;
    }

    public class Rooms
    {
        public int? id { get; set; }
        public int? unit_id { get; set; }
        public string Room_name { get; set; }
    }
}