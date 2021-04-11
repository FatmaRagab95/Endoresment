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

public partial class _patientData : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
    }

    // update patients data
    [WebMethod]
    public static string updatePatientData(Endorsement_PatientData patient)
    {
        string config =
            Convert.ToString(ConfigurationManager.ConnectionStrings["dbcon"]);
        List<Endorsement_PatientData> Endorsement_PatientData =
            new List<Endorsement_PatientData>();

        SqlConnection con = new SqlConnection(config);

        con.Open();

        using (
            SqlCommand cmd =
                new SqlCommand("update Endorsement_PatientData set Patient_Status = @Patient_Status, Death_date = @Death_date, Discharged_date = @Discharged_date where id = @id ",
                    con)
        )
        {
            cmd.Parameters.Add("@id", SqlDbType.Int).Value = patient.id;
            cmd.Parameters.Add("@Patient_Status", SqlDbType.Int).Value =
                patient.Patient_Status;
            cmd.Parameters.Add("@Death_date", SqlDbType.VarChar).Value =
                patient.Death_date;
            cmd.Parameters.Add("@Discharged_date", SqlDbType.VarChar).Value =
                patient.Discharged_date;
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

    // update unitsDashboard data
    [WebMethod]
    public static string updateUnitsData(UnitsDash unit)
    {
        string config =
            Convert.ToString(ConfigurationManager.ConnectionStrings["dbcon"]);
        List<UnitsDash> UnitsDash =
            new List<UnitsDash>();

        SqlConnection con = new SqlConnection(config);

        con.Open();

        using (
            SqlCommand cmd =
                new SqlCommand("update Endorsement_UnitsDashboard set Total_Census = Total_Census - 1, Discharge = Discharge + @Discharge, Death = Death + @Death where Unit_name = @Unit_name and Shift = @Shift and Shift_date >= DATEADD(day,-2, GETDATE()) ",
                    con)
        )
        {
            cmd.Parameters.Add("@Unit_name", SqlDbType.VarChar).Value = unit.Unit_name;
            cmd.Parameters.Add("@Shift", SqlDbType.VarChar).Value = unit.Shift;
            cmd.Parameters.Add("@Shift_date", SqlDbType.VarChar).Value = unit.Shift_date;
            cmd.Parameters.Add("@Death", SqlDbType.Int).Value =
                unit.Death;
            cmd.Parameters.Add("@Discharge", SqlDbType.Int).Value =
                unit.Discharge;
            SqlDataReader idr = cmd.ExecuteReader();
        }

        con.Close();

        return JsonConvert.SerializeObject(UnitsDash);
    }

        public class UnitsDash
    {
        public string Unit_name { get; set; }

        public string Shift { get; set; }
        public string Shift_date { get; set; }

        public int? Death { get; set; }

        public int? Discharge { get; set; }
    }

    // update room dashboard
    [WebMethod]
    public static string updateRoomData(RoomDetails room)
    {
        string config =
            Convert.ToString(ConfigurationManager.ConnectionStrings["dbcon"]);

        List<RoomDetails> RoomDetails = new List<RoomDetails>();

        SqlConnection con = new SqlConnection(config);

        con.Open();

        using (
            SqlCommand cmd =
                new SqlCommand("update Endorsement_RoomsDashboard set Status_id=@Status_id, Status_name=@Status_name where id = @id;",
                    con)
        )
        {
            cmd.Parameters.AddWithValue("@id", room.id);
            cmd.Parameters.AddWithValue("@Status_id", room.Status_id);
            cmd.Parameters.AddWithValue("@Status_name", room.Status_name);

            cmd.ExecuteNonQuery();
        }

        con.Close();

        return JsonConvert.SerializeObject(RoomDetails);
    }

    public static List<RoomDetails>
    populateRoomDetailsLisst(SqlDataReader idr, SqlConnection con)
    {
        List<RoomDetails> RoomDetailsI = new List<RoomDetails>();

        while (idr.Read())
        {
            RoomDetailsI
                .Add(new RoomDetails {
                    id = Convert.ToInt32(idr["id"]),
                    Status_id = Convert.ToInt32(idr["Status_id"]),
                    Status_name = Convert.ToString(idr["Status_name"])
                });
        }
        return RoomDetailsI;
    }

    public class RoomDetails
    {
        public int? id { get; set; }

        public int? Status_id { get; set; }

        public string Status_name { get; set; }
    }

    // get patients data
    [WebMethod]
    public static string getPatientData(Endorsement_PatientData id)
    {
        string config =
            Convert.ToString(ConfigurationManager.ConnectionStrings["dbcon"]);
        List<Endorsement_PatientData> Endorsement_PatientData =
            new List<Endorsement_PatientData>();

        SqlConnection con = new SqlConnection(config);

        con.Open();

        using (
            SqlCommand cmd =
                new SqlCommand("select * from Endorsement_PatientData where id = @id",
                    con)
        )
        {
            cmd.Parameters.Add("@id", SqlDbType.Int).Value = id.id;
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
                    Bed_id =
                        idr["Bed_id"] != DBNull.Value
                            ? Convert.ToInt32(idr["Bed_id"])
                            : 0,
                    Patient_FullName =
                        idr["Patient_FullName"] != DBNull.Value
                            ? Convert.ToString(idr["Patient_FullName"])
                            : String.Empty,
                    Branch_id =
                        idr["Branch_id"] != DBNull.Value
                            ? Convert.ToInt32(idr["Branch_id"])
                            : 0,
                    Patient_Status =
                        idr["Patient_Status"] != DBNull.Value
                            ? Convert.ToInt32(idr["Patient_Status"])
                            : 0,
                    Branch_name =
                        idr["Branch_name"] != DBNull.Value
                            ? Convert.ToString(idr["Branch_name"])
                            : String.Empty,
                    Gender =
                        idr["Gender"] != DBNull.Value
                            ? Convert.ToString(idr["Gender"])
                            : String.Empty,
                    Medical_Number =
                        idr["Medical_Number"] != DBNull.Value
                            ? Convert.ToInt64(idr["Medical_Number"])
                            : 0,
                    Unit =
                        idr["Unit"] != DBNull.Value
                            ? Convert.ToString(idr["Unit"])
                            : String.Empty,
                    Room =
                        idr["Room"] != DBNull.Value
                            ? Convert.ToString(idr["Room"])
                            : String.Empty,
                    Addmission_date =
                        idr["Addmission_date"] != DBNull.Value
                            ? Convert.ToString(idr["Addmission_date"])
                            : String.Empty,
                    Age =
                        idr["Age"] != DBNull.Value
                            ? Convert.ToString(idr["Age"])
                            : String.Empty,
                    Consultant_Name =
                        idr["Consultant_Name"] != DBNull.Value
                            ? Convert.ToString(idr["Consultant_Name"])
                            : String.Empty,
                    Specialty =
                        idr["Specialty"] != DBNull.Value
                            ? Convert.ToString(idr["Specialty"])
                            : String.Empty,
                    Date_Birth =
                        idr["Date_Birth"] != DBNull.Value
                            ? Convert.ToString(idr["Date_Birth"])
                            : String.Empty,
                    Death_date =
                        idr["Death_date"] != DBNull.Value
                            ? Convert.ToString(idr["Death_date"])
                            : String.Empty,
                    Discharged_date =
                        idr["Discharged_date"] != DBNull.Value
                            ? Convert.ToString(idr["Discharged_date"])
                            : String.Empty
                });
        }

        return Endorsement_PatientDataI;
    }

    public class Endorsement_PatientData
    {
        public int? id { get; set; }

        public int? Bed_id { get; set; }

        public string Patient_FullName { get; set; }

        public int? Branch_id { get; set; }

        public int? Patient_Status { get; set; }

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

        public string Death_date { get; set; }

        public string Discharged_date { get; set; }
    }

    /***********************************************************/
    // insert follow up data
    [WebMethod]
    public static string insertFollowUpData(Endorsement_shiftData patient)
    {
        string config =
            Convert.ToString(ConfigurationManager.ConnectionStrings["dbcon"]);
        List<Endorsement_shiftData> Endorsement_shiftData =
            new List<Endorsement_shiftData>();

        SqlConnection con = new SqlConnection(config);

        con.Open();

        using (
            SqlCommand cmd =
                new SqlCommand("insert into Endorsement_PatientFollow  (Patient_id,Insert_Nurse,Insert_Nurse_Name,Insert_Nurse_Time,Insert_Doctor,Insert_Doctor_Name,Insert_Doctor_Time, Transfer_From, Transfer_To, Shift) values(@Patient_id, @Insert_Nurse,@Insert_Nurse_Name, @Insert_Nurse_Time,@Insert_Doctor,@Insert_Doctor_Name, @Insert_Doctor_Time, @Transfer_From, @Transfer_To, @Shift ) ",
                    con)
        )
        {
            cmd.Parameters.Add("@Patient_id", SqlDbType.Int).Value =
                patient.Patient_id;

            cmd.Parameters.Add("@Insert_Nurse", SqlDbType.Int).Value =
                patient.Insert_Nurse;
            cmd.Parameters.Add("@Insert_Nurse_Name", SqlDbType.VarChar).Value =
                patient.Insert_Nurse_Name;
            cmd.Parameters.Add("@Insert_Nurse_Time", SqlDbType.VarChar).Value =
                patient.Insert_Nurse_Time;

            cmd.Parameters.Add("@Insert_Doctor", SqlDbType.Int).Value =
                patient.Insert_Doctor;
            cmd.Parameters.Add("@Insert_Doctor_Name", SqlDbType.VarChar).Value =
                patient.Insert_Doctor_Name;
            cmd.Parameters.Add("@Insert_Doctor_Time", SqlDbType.VarChar).Value =
                patient.Insert_Doctor_Time;
            cmd.Parameters.Add("@Transfer_From", SqlDbType.VarChar).Value =
                patient.Transfer_From;
            cmd.Parameters.Add("@Transfer_To", SqlDbType.VarChar).Value =
                patient.Transfer_To;
            cmd.Parameters.Add("@Shift", SqlDbType.VarChar).Value =
                patient.Shift;
            SqlDataReader idr = cmd.ExecuteReader();

            if (idr.HasRows)
            {
                Endorsement_shiftData =
                    populateEndorsement_shiftDataLisst(idr, con);
            }
        }

        con.Close();

        return JsonConvert.SerializeObject(Endorsement_shiftData);
    }

    // update follow up data
    [WebMethod]
    public static string updateFollowUpData(Endorsement_shiftData patient)
    {
        string config =
            Convert.ToString(ConfigurationManager.ConnectionStrings["dbcon"]);
        List<Endorsement_shiftData> Endorsement_shiftData =
            new List<Endorsement_shiftData>();

        SqlConnection con = new SqlConnection(config);

        con.Open();

        using (
            SqlCommand cmd =
                new SqlCommand("update Endorsement_PatientFollow set Update_Nurse = @Update_Nurse, Update_Nurse_Name = @Update_Nurse_Name, Update_Nurse_Time = @Update_Nurse_Time, Update_Doctor = @Update_Doctor,Update_Doctor_Name = @Update_Doctor_Name,Update_Doctor_Time = @Update_Doctor_Time, Transfer_From = @Transfer_From, Transfer_To = @Transfer_To  where id = @id ",
                    con)
        )
        {
            cmd.Parameters.Add("@id", SqlDbType.Int).Value = patient.id;
            cmd.Parameters.Add("@Update_Nurse", SqlDbType.Int).Value =
                patient.Update_Nurse;
            cmd.Parameters.Add("@Update_Nurse_Name", SqlDbType.VarChar).Value =
                patient.Update_Nurse_Name;
            cmd.Parameters.Add("@Update_Nurse_Time", SqlDbType.DateTime).Value =
                patient.Update_Nurse_Time;

            cmd.Parameters.Add("@Update_Doctor", SqlDbType.Int).Value =
                patient.Update_Doctor;
            cmd.Parameters.Add("@Update_Doctor_Name", SqlDbType.VarChar).Value =
                patient.Update_Doctor_Name;
            cmd
                .Parameters
                .Add("@Update_Doctor_Time", SqlDbType.DateTime)
                .Value = patient.Update_Doctor_Time;
            cmd.Parameters.Add("@Transfer_From", SqlDbType.VarChar).Value =
                patient.Transfer_From;
            cmd.Parameters.Add("@Transfer_To", SqlDbType.VarChar).Value =
                patient.Transfer_To;
            SqlDataReader idr = cmd.ExecuteReader();

            if (idr.HasRows)
            {
                Endorsement_shiftData =
                    populateEndorsement_shiftDataLisst(idr, con);
            }
        }

        con.Close();

        return JsonConvert.SerializeObject(Endorsement_shiftData);
    }

    // get follow up data
    [WebMethod]
    public static string getShiftData(Endorsement_shiftData data)
    {
        string config =
            Convert.ToString(ConfigurationManager.ConnectionStrings["dbcon"]);
        List<Endorsement_shiftData> Endorsement_shiftData =
            new List<Endorsement_shiftData>();

        SqlConnection con = new SqlConnection(config);

        con.Open();

        using (
            SqlCommand cmd =
                new SqlCommand("select id,Shift,Patient_id,Diet_id,Diet_Name,Pain,P_Isolation,Fall, Allergy,Investegation_ToDo,Investegation_FollowUp,Contraptions_Infusions,Routise_PlanOfCare,Surgury_Procedures,DR_Diagnosis,DR_ProgressNotes,DR_Consultaion_Progress,Insert_Nurse,Insert_Nurse_Name,Insert_Nurse_Time,Update_Nurse,Update_Nurse_Name,Update_Nurse_Time, Update_Doctor,Update_Doctor_Name,Update_Doctor_Time,Insert_Doctor,Insert_Doctor_Name,Insert_Doctor_Time,Consultaion,Entry_date from Endorsement_PatientFollow where Patient_id = @Patient_id order by id asc",
                    con)
        )
        {
            cmd.Parameters.Add("@Patient_id", SqlDbType.Int).Value =
                data.Patient_id;
            SqlDataReader idr = cmd.ExecuteReader();

            if (idr.HasRows)
            {
                Endorsement_shiftData =
                    populateEndorsement_shiftDataLisst(idr, con);
            }
        }

        con.Close();

        return JsonConvert.SerializeObject(Endorsement_shiftData);
    }

    public static List<Endorsement_shiftData>
    populateEndorsement_shiftDataLisst(SqlDataReader idr, SqlConnection con)
    {
        List<Endorsement_shiftData> Endorsement_shiftDataI =
            new List<Endorsement_shiftData>();

        while (idr.Read())
        {
            Endorsement_shiftDataI
                .Add(new Endorsement_shiftData {
                    id =
                        idr["id"] != DBNull.Value
                            ? Convert.ToInt32(idr["id"])
                            : 0,
                    Shift = Convert.ToString(idr["Shift"]),
                    Patient_id =
                        idr["Patient_id"] != DBNull.Value
                            ? Convert.ToInt32(idr["Patient_id"])
                            : 0,
                    Diet_id =
                        idr["Diet_id"] != DBNull.Value
                            ? Convert.ToInt32(idr["Diet_id"])
                            : 0,
                    Diet_Name = Convert.ToString(idr["Diet_Name"]),
                    Pain =
                        idr["Pain"] != DBNull.Value
                            ? Convert.ToInt32(idr["Pain"])
                            : 0,
                    P_Isolation = Convert.ToString(idr["P_Isolation"]),
                    Fall = Convert.ToString(idr["Fall"]),
                    Allergy = Convert.ToString(idr["Allergy"]),
                    Investegation_ToDo =
                        Convert.ToString(idr["Investegation_ToDo"]),
                    Investegation_FollowUp =
                        Convert.ToString(idr["Investegation_FollowUp"]),
                    Contraptions_Infusions =
                        Convert.ToString(idr["Contraptions_Infusions"]),
                    Routise_PlanOfCare =
                        Convert.ToString(idr["Routise_PlanOfCare"]),
                    Surgury_Procedures =
                        Convert.ToString(idr["Surgury_Procedures"]),
                    DR_Diagnosis = Convert.ToString(idr["DR_Diagnosis"]),
                    DR_ProgressNotes =
                        Convert.ToString(idr["DR_ProgressNotes"]),
                    DR_Consultaion_Progress =
                        Convert.ToString(idr["DR_Consultaion_Progress"]),
                    Insert_Nurse =
                        idr["Insert_Nurse"] != DBNull.Value
                            ? Convert.ToInt32(idr["Insert_Nurse"])
                            : 0,
                    Insert_Nurse_Name =
                        Convert.ToString(idr["Insert_Nurse_Name"]),
                    Insert_Nurse_Time =
                        Convert.ToString(idr["Insert_Nurse_Time"]),
                    Update_Nurse =
                        idr["Update_Nurse"] != DBNull.Value
                            ? Convert.ToInt32(idr["Update_Nurse"])
                            : 0,
                    Update_Nurse_Name =
                        Convert.ToString(idr["Update_Nurse_Name"]),
                    Update_Nurse_Time =
                        Convert.ToString(idr["Update_Nurse_Time"]),
                    Insert_Doctor =
                        idr["Insert_Doctor"] != DBNull.Value
                            ? Convert.ToInt32(idr["Insert_Doctor"])
                            : 0,
                    Insert_Doctor_Name =
                        Convert.ToString(idr["Insert_Doctor_Name"]),
                    Insert_Doctor_Time =
                        Convert.ToString(idr["Insert_Doctor_Time"]),
                    Update_Doctor =
                        idr["Update_Doctor"] != DBNull.Value
                            ? Convert.ToInt32(idr["Update_Doctor"])
                            : 0,
                    Update_Doctor_Name =
                        Convert.ToString(idr["Update_Doctor_Name"]),
                    Update_Doctor_Time =
                        Convert.ToString(idr["Update_Doctor_Time"]),
                    Consultaion =
                        idr["Consultaion"] != DBNull.Value
                            ? Convert.ToInt32(idr["Consultaion"])
                            : 0,
                    Entry_date = Convert.ToDateTime(idr["Entry_date"])
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

    public class branches
    {
        public int? id { get; set; }

        public string Branch_EName { get; set; }
    }

    // get units
    [WebMethod]
    public static string getUnitsData(branches branch)
    {
        string config =
            Convert.ToString(ConfigurationManager.ConnectionStrings["dbcon"]);
        List<branches> branches = new List<branches>();
        List<Units> Units = new List<Units>();

        SqlConnection con = new SqlConnection(config);

        con.Open();

        using (
            SqlCommand cmd =
                new SqlCommand("select * from Units where Branch_id = @Branch_id and Show = 1",
                    con)
        )
        {
            cmd.Parameters.Add("@Branch_id", SqlDbType.Int).Value = branch.id;
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

    // get rooms
    [WebMethod]
    public static string getRoomsData(Units unit)
    {
        string config =
            Convert.ToString(ConfigurationManager.ConnectionStrings["dbcon"]);
        List<Units> Units = new List<Units>();
        List<Rooms> Rooms = new List<Rooms>();

        SqlConnection con = new SqlConnection(config);

        con.Open();

        using (
            SqlCommand cmd =
                new SqlCommand("select * from Endorsement_Rooms where Unit_id = @Unit_id and id in (select Room_id from Endorsement_RoomsDashboard where Status_id = 1)",
                    con)
        )
        {
            cmd.Parameters.Add("@Unit_id", SqlDbType.Int).Value = unit.U_id;
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
                    Room_name = Convert.ToString(idr["Room_name"])
                });
        }

        return RoomsI;
    }

    public class Rooms
    {
        public int? id { get; set; }

        public string Room_name { get; set; }
    }

    // get beds
    [WebMethod]
    public static string getBedsData(Rooms room)
    {
        string config =
            Convert.ToString(ConfigurationManager.ConnectionStrings["dbcon"]);
        List<Rooms> Rooms = new List<Rooms>();
        List<Beds> Beds = new List<Beds>();

        SqlConnection con = new SqlConnection(config);

        con.Open();

        using (
            SqlCommand cmd =
                new SqlCommand("select * from Endorsement_RoomsDashboard where Room_id = @Room_id and Status_id = 1",
                    con)
        )
        {
            cmd.Parameters.Add("@Room_id", SqlDbType.Int).Value = room.id;
            SqlDataReader idr = cmd.ExecuteReader();

            if (idr.HasRows)
            {
                Beds = populateBedsLisst(idr, con);
            }
        }

        con.Close();

        return JsonConvert.SerializeObject(Beds);
    }

    public static List<Beds>
    populateBedsLisst(SqlDataReader idr, SqlConnection con)
    {
        List<Beds> BedsI = new List<Beds>();

        while (idr.Read())
        {
            BedsI
                .Add(new Beds {
                    id =
                        idr["id"] != DBNull.Value
                            ? Convert.ToInt32(idr["id"])
                            : 0,
                    Bed_name = Convert.ToString(idr["Bed_name"])
                });
        }

        return BedsI;
    }

    public class Beds
    {
        public int? id { get; set; }

        public string Bed_name { get; set; }
    }

    // update transform patient data table
    [WebMethod]
    public static string updateTransform(TranstDetails detail)
    {
        string config =
            Convert.ToString(ConfigurationManager.ConnectionStrings["dbcon"]);

        List<TranstDetails> TranstDetails = new List<TranstDetails>();

        SqlConnection con = new SqlConnection(config);

        con.Open();

        using (
            SqlCommand cmd =
                new SqlCommand("update Endorsement_PatientData set Unit=@Unit, Room=@Room, Bed_id=@Bed_id where id = @id;",
                    con)
        )
        {
            cmd.Parameters.AddWithValue("@id", detail.id);
            cmd.Parameters.AddWithValue("@Unit", detail.Unit);
            cmd.Parameters.AddWithValue("@Room", detail.Room);
            cmd.Parameters.AddWithValue("@Bed_id", detail.Bed_id);

            cmd.ExecuteNonQuery();
        }

        con.Close();

        return JsonConvert.SerializeObject(TranstDetails);
    }

    public static List<TranstDetails>
    populateTranstDetailsLisst(SqlDataReader idr, SqlConnection con)
    {
        List<TranstDetails> TranstDetailsI = new List<TranstDetails>();

        while (idr.Read())
        {
            TranstDetailsI
                .Add(new TranstDetails {
                    id = Convert.ToInt32(idr["id"]),
                    Unit = Convert.ToString(idr["Unit"]),
                    Room = Convert.ToString(idr["Room"]),
                    Bed_id = Convert.ToInt32(idr["Bed_id"])
                });
        }
        return TranstDetailsI;
    }

    public class TranstDetails
    {
        public int? id { get; set; }

        public string Unit { get; set; }

        public string Room { get; set; }

        public int? Bed_id { get; set; }
    }
}
