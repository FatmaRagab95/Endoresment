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

    // insert new shift for nurses
    public static int NewId;

    [WebMethod]
    public static string newDoctorShift(shifts shift)
    {
        string config =
            Convert.ToString(ConfigurationManager.ConnectionStrings["dbcon"]);
        List<shifts> shifts = new List<shifts>();

        SqlConnection con = new SqlConnection(config);

        con.Open();

        using (
            SqlCommand cmd =
                new SqlCommand("insert into Endoresment_Doctors_Shifts  (Doctor_id,Doctor_name,Receive_from,Receive_name, Shift, Shift_date, Confirm,Spcy_id, Entry_user) values(@Doctor_id, @Doctor_name,@Receive_from,@Receive_name,@Shift,@Shift_date,@Confirm,@Spcy_id, @Entry_user) SELECT CAST(scope_identity() AS int)",
                    con)
        )
        {
            cmd.Parameters.Add("@Doctor_id", SqlDbType.Int).Value =
                shift.Doctor_id;
            cmd.Parameters.Add("@Doctor_name", SqlDbType.VarChar).Value =
                shift.Doctor_name;
            cmd.Parameters.Add("@Receive_from", SqlDbType.Int).Value =
                shift.Receive_from;
            cmd.Parameters.Add("@Receive_name", SqlDbType.VarChar).Value =
                shift.Receive_name;
            cmd.Parameters.Add("@Shift", SqlDbType.VarChar).Value = shift.Shift;
            cmd.Parameters.Add("@Shift_date", SqlDbType.VarChar).Value =
                shift.Shift_date;
            cmd.Parameters.Add("@Confirm", SqlDbType.Int).Value = shift.Confirm;
            cmd.Parameters.Add("@Spcy_id", SqlDbType.Int).Value = shift.Spcy_id;
            cmd.Parameters.Add("@Entry_user", SqlDbType.Int).Value =
                shift.Entry_user;

            NewId = (Int32) cmd.ExecuteScalar();
        }

        con.Close();

        return JsonConvert.SerializeObject(NewId);
    }

    // if it's handover shift update the handed shift as completed
    [WebMethod]
    public static string completeDoctorShift(shifts shift)
    {
        string config =
            Convert.ToString(ConfigurationManager.ConnectionStrings["dbcon"]);
        List<shifts> shifts = new List<shifts>();

        SqlConnection con = new SqlConnection(config);

        con.Open();

        using (
            SqlCommand cmd =
                new SqlCommand("update Endoresment_Doctors_Shifts set Completed = 1 where id = @id",
                    con)
        )
        {
            cmd.Parameters.Add("@id", SqlDbType.Int).Value = shift.id;
            SqlDataReader idr = cmd.ExecuteReader();
        }

        con.Close();

        return JsonConvert.SerializeObject(shifts);
    }

    // if nurse confirming receiving shift
    [WebMethod]
    public static string confirmShift(shifts shift)
    {
        string config =
            Convert.ToString(ConfigurationManager.ConnectionStrings["dbcon"]);
        List<shifts> shifts = new List<shifts>();

        SqlConnection con = new SqlConnection(config);

        con.Open();

        using (
            SqlCommand cmd =
                new SqlCommand("update Endoresment_Doctors_Shifts set Confirm = 1 where id = @id",
                    con)
        )
        {
            cmd.Parameters.Add("@id", SqlDbType.Int).Value = shift.id;
            SqlDataReader idr = cmd.ExecuteReader();
        }

        con.Close();

        return JsonConvert.SerializeObject(shifts);
    }

    // get shifts
    [WebMethod]
    public static string getShiftsData(DoctorData data)
    {
        string config =
            Convert.ToString(ConfigurationManager.ConnectionStrings["dbcon"]);
        List<DoctorData> DoctorData = new List<DoctorData>();
        List<shifts> shifts = new List<shifts>();

        SqlConnection con = new SqlConnection(config);

        con.Open();

        using (
            SqlCommand cmd =
                new SqlCommand("select top(1) * from Endoresment_Doctors_Shifts where Doctor_id = @Doctor_id and (Shift_date >= DATEADD(day,-2, GETDATE())) order by id ASC",
                    con)
        )
        {
            cmd.Parameters.Add("@Doctor_id", SqlDbType.Int).Value =
                data.Dr_Code;
            SqlDataReader idr = cmd.ExecuteReader();

            if (idr.HasRows)
            {
                shifts = populateshiftsLisst(idr, con);
            }
        }

        con.Close();

        return JsonConvert.SerializeObject(shifts);
    }

    public static List<shifts>
    populateshiftsLisst(SqlDataReader idr, SqlConnection con)
    {
        List<shifts> shiftsI = new List<shifts>();

        while (idr.Read())
        {
            shiftsI
                .Add(new shifts {
                    id = Convert.ToInt32(idr["id"]),
                    Shift = Convert.ToString(idr["Shift"]),
                    Shift_date = Convert.ToString(idr["Shift_date"]),
                    Receive_name = Convert.ToString(idr["Receive_name"]),
                    Confirm =
                        idr["Confirm"] != DBNull.Value
                            ? Convert.ToInt32(idr["Confirm"])
                            : 0,
                    Completed =
                        idr["Completed"] != DBNull.Value
                            ? Convert.ToInt32(idr["Completed"])
                            : 0
                });
        }

        return shiftsI;
    }

    public class shifts
    {
        public int? id { get; set; }

        public int? Doctor_id { get; set; }

        public string Doctor_name { get; set; }

        public int? Receive_from { get; set; }

        public string Receive_name { get; set; }

        public string Shift { get; set; }

        public string Shift_date { get; set; }

        public int? Confirm { get; set; }

        public int? Completed { get; set; }

        public int? Spcy_id { get; set; }

        public int? Entry_user { get; set; }
    }

    // get doctor data
    [WebMethod]
    public static string getDoctorData(DoctorData data)
    {
        string config =
            Convert.ToString(ConfigurationManager.ConnectionStrings["dbcon"]);
        List<DoctorData> DoctorData = new List<DoctorData>();

        SqlConnection con = new SqlConnection(config);

        con.Open();

        using (
            SqlCommand cmd =
                new SqlCommand("select * from Doctors_Data where Dr_Code = @Dr_Code",
                    con)
        )
        {
            cmd.Parameters.Add("@Dr_Code", SqlDbType.Int).Value = data.Dr_Code;
            SqlDataReader idr = cmd.ExecuteReader();

            if (idr.HasRows)
            {
                DoctorData = populateDoctorDataLisst(idr, con);
            }
        }

        con.Close();

        return JsonConvert.SerializeObject(DoctorData);
    }

    // get Doctors
    [WebMethod]
    public static string getDoctorsData(DoctorData data)
    {
        string config =
            Convert.ToString(ConfigurationManager.ConnectionStrings["dbcon"]);
        List<DoctorData> DoctorData = new List<DoctorData>();

        SqlConnection con = new SqlConnection(config);

        con.Open();

        using (
            SqlCommand cmd =
                new SqlCommand("select * from Doctors_Data where Spcy_Description = @Spcy_Description and Type = 'Resident'",
                    con)
        )
        {
            cmd.Parameters.Add("@Spcy_Description", SqlDbType.VarChar).Value =
                data.Spcy_Description;
            SqlDataReader idr = cmd.ExecuteReader();

            if (idr.HasRows)
            {
                DoctorData = populateDoctorDataLisst(idr, con);
            }
        }

        con.Close();

        return JsonConvert.SerializeObject(DoctorData);
    }

    public static List<DoctorData>
    populateDoctorDataLisst(SqlDataReader idr, SqlConnection con)
    {
        List<DoctorData> DoctorDataI = new List<DoctorData>();

        while (idr.Read())
        {
            DoctorDataI
                .Add(new DoctorData {
                    Spcy_Description =
                        idr["Spcy_Description"] != DBNull.Value
                            ? Convert.ToString(idr["Spcy_Description"])
                            : String.Empty,
                    Type =
                        idr["Type"] != DBNull.Value
                            ? Convert.ToString(idr["Type"])
                            : String.Empty,
                    Dr_Code =
                        idr["Dr_Code"] != DBNull.Value
                            ? Convert.ToInt32(idr["Dr_Code"])
                            : 0,
                    Spcy_id =
                        idr["Spcy_id"] != DBNull.Value
                            ? Convert.ToInt32(idr["Spcy_id"])
                            : 0,
                    DR_Name =
                        idr["DR_Name"] != DBNull.Value
                            ? Convert.ToString(idr["DR_Name"])
                            : String.Empty
                });
        }

        return DoctorDataI;
    }

    public class DoctorData
    {
        public int? Dr_Code { get; set; }

        public int? Spcy_id { get; set; }

        public string Spcy_Description { get; set; }

        public string Type { get; set; }

        public string DR_Name { get; set; }
    }

    // get patients data if concultant
    [WebMethod]
    public static string getPatientsData(DoctorData data)
    {
        string config =
            Convert.ToString(ConfigurationManager.ConnectionStrings["dbcon"]);
        List<DoctorData> DoctorData = new List<DoctorData>();
        List<Endorsement_PatientData> Endorsement_PatientData =
            new List<Endorsement_PatientData>();

        SqlConnection con = new SqlConnection(config);

        con.Open();

        using (
            SqlCommand cmd =
                new SqlCommand("select * from Endorsement_PatientData where Consultant_id = @Consultant_id",
                    con)
        )
        {
            cmd.Parameters.Add("@Consultant_id", SqlDbType.Int).Value =
                data.Dr_Code;
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

    // get patients data if resident
    [WebMethod]
    public static string getPatientsData2(DoctorData data)
    {
        string config =
            Convert.ToString(ConfigurationManager.ConnectionStrings["dbcon"]);
        List<DoctorData> DoctorData = new List<DoctorData>();
        List<Endorsement_PatientData> Endorsement_PatientData =
            new List<Endorsement_PatientData>();

        SqlConnection con = new SqlConnection(config);

        con.Open();

        using (
            SqlCommand cmd =
                new SqlCommand("select * from Endorsement_PatientData where Specialty = @Spcy_Description",
                    con)
        )
        {
            cmd.Parameters.Add("@Spcy_Description", SqlDbType.VarChar).Value =
                data.Spcy_Description;
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

    // get Consultation cases data
    [WebMethod]
    public static string getConsultationsData(DoctorData data)
    {
        string config =
            Convert.ToString(ConfigurationManager.ConnectionStrings["dbcon"]);
        List<DoctorData> DoctorData = new List<DoctorData>();
        List<Endorsement_PatientData> Endorsement_PatientData =
            new List<Endorsement_PatientData>();

        SqlConnection con = new SqlConnection(config);

        con.Open();

        using (
            SqlCommand cmd =
                new SqlCommand("select * from Endorsement_PatientData where id in (select top(1) Patient_id from Endorsement_PatientFollow where Consultaion = @Consultant_id order by id ASC)",
                    con)
        )
        {
            cmd.Parameters.Add("@Consultant_id", SqlDbType.Int).Value =
                data.Dr_Code;
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
                    Branch_id =
                        idr["Branch_id"] != DBNull.Value
                            ? Convert.ToInt32(idr["Branch_id"])
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
                    Consultant_id =
                        idr["Consultant_id"] != DBNull.Value
                            ? Convert.ToInt32(idr["Consultant_id"])
                            : 0,
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
                            : String.Empty
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

        public int Consultant_id { get; set; }

        public string Consultant_Name { get; set; }

        public string Specialty { get; set; }

        public string Date_Birth { get; set; }
    }
}
