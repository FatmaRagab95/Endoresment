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

    public static int NewId;

    // insert new shift for nurses

    [WebMethod]
    public static string newNurseShift(shifts shift)
    {
        string config =  Convert.ToString(ConfigurationManager.ConnectionStrings["dbcon"]);
        List<shifts> shifts = new List<shifts>();

        SqlConnection con = new SqlConnection(config);

        con.Open();

        using (SqlCommand cmd = new SqlCommand("insert into Endoresment_Nurses_Shifts  (Nurse_id,Nurse_name,Receive_from,Receive_name, Unit_id, Shift, Shift_date, Confirm, Entry_user) values(@Nurse_id, @Nurse_name,@Receive_from,@Receive_name,@Unit_id,@Shift,@Shift_date,@Confirm, @Entry_user) SELECT CAST(scope_identity() AS int)", con))
        {
            cmd.Parameters.Add("@Nurse_id", SqlDbType.Int).Value = shift.Nurse_id;
            cmd.Parameters.Add("@Nurse_name", SqlDbType.VarChar).Value = shift.Nurse_name;
            cmd.Parameters.Add("@Receive_from", SqlDbType.Int).Value = shift.Receive_from;
            cmd.Parameters.Add("@Receive_name", SqlDbType.VarChar).Value = shift.Receive_name;
            cmd.Parameters.Add("@Unit_id", SqlDbType.Int).Value = shift.Unit_id;
            cmd.Parameters.Add("@Shift", SqlDbType.VarChar).Value = shift.Shift;
            cmd.Parameters.Add("@Shift_date", SqlDbType.VarChar).Value = shift.Shift_date;
            cmd.Parameters.Add("@Confirm", SqlDbType.Int).Value = shift.Confirm;
            cmd.Parameters.Add("@Entry_user", SqlDbType.Int).Value = shift.Entry_user;
            
            NewId = (Int32) cmd.ExecuteScalar();
        }

        con.Close();

        return JsonConvert.SerializeObject(NewId);
    }

    // if it's handover shift update the handed shift as completed
    [WebMethod]
    public static string completeNurseShift(shifts shift)
    {
        string config =  Convert.ToString(ConfigurationManager.ConnectionStrings["dbcon"]);
        List<shifts> shifts = new List<shifts>();

        SqlConnection con = new SqlConnection(config);

        con.Open();

        
        using (SqlCommand cmd = new SqlCommand("update Endoresment_Nurses_Shifts set Completed = 1 where id = @id", con))
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
        string config =  Convert.ToString(ConfigurationManager.ConnectionStrings["dbcon"]);
        List<shifts> shifts = new List<shifts>();

        SqlConnection con = new SqlConnection(config);

        con.Open();

        
        using (SqlCommand cmd = new SqlCommand("update Endoresment_Nurses_Shifts set Confirm = 1 where id = @id", con))
        {
            cmd.Parameters.Add("@id", SqlDbType.Int).Value = shift.id;
            SqlDataReader idr = cmd.ExecuteReader();
        }

        con.Close();

        return JsonConvert.SerializeObject(shifts);
    }

    // get shifts
    [WebMethod]
    public static string getShiftsData(user info)
    {
        string config =
            Convert.ToString(ConfigurationManager.ConnectionStrings["dbcon"]);
        List<user> user = new List<user>();
        List<shifts> shifts = new List<shifts>();

        SqlConnection con = new SqlConnection(config);

        con.Open();

        using (SqlCommand cmd = new SqlCommand("select * from Endoresment_Nurses_Shifts where Nurse_id = @Nurse_id and (Shift_date >= DATEADD(day,-2, GETDATE()))", con))
        {
            cmd.Parameters.Add("@Nurse_id", SqlDbType.Int).Value = info.Emp_id;
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
                    Unit_id = idr["Unit_id"] != DBNull.Value ? Convert.ToInt32(idr["Unit_id"]) : 0,
                    Confirm = idr["Confirm"] != DBNull.Value ? Convert.ToInt32(idr["Confirm"]) : 0,
                    Completed = idr["Completed"] != DBNull.Value ? Convert.ToInt32(idr["Completed"]) : 0,
                });
        }

        return shiftsI;
    }

    public class shifts
    {
        public int? id { get; set; }
        public int? Nurse_id { get; set; }
        public string Nurse_name { get; set; }
        public int? Receive_from { get; set; }
        public string Receive_name { get; set; }
        public int? Unit_id { get; set; }
        public string Shift { get; set; }
        public string Shift_date { get; set; }
        public int? Confirm { get; set; }
        public int? Completed { get; set; }
        public int? Entry_user { get; set; }
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
    public static string getNursesData(user info)
    {
        string config =
            Convert.ToString(ConfigurationManager.ConnectionStrings["dbcon"]);
        List<user> user = new List<user>();
        List<Nurses> Nurses = new List<Nurses>();

        SqlConnection con = new SqlConnection(config);

        con.Open();

        using (SqlCommand cmd = new SqlCommand("select * from adminusers where Role_id = 12 and Branch_ID = @Branch_id and Emp_id in (select Nurse_id from Endoresment_Nurses_Units where Active = 1 and Unit_id in (select Unit_id from Endoresment_Nurses_Units where Nurse_id = @Nurse_id and Active = 1))", con))
        {
            cmd.Parameters.Add("@Branch_id", SqlDbType.Int).Value = info.Branch_ID;
            cmd.Parameters.Add("@Nurse_id", SqlDbType.Int).Value = info.Emp_id;
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

    // get Nurses units
    [WebMethod]
    public static string getNursesUnitsData(user info)
    {
        string config =
            Convert.ToString(ConfigurationManager.ConnectionStrings["dbcon"]);
        List<user> user = new List<user>();
        List<NursesUnits> NursesUnits = new List<NursesUnits>();

        SqlConnection con = new SqlConnection(config);

        con.Open();

        using (SqlCommand cmd = new SqlCommand("select * from Endoresment_Nurses_Units where Active = 1 and Unit_id in (select Unit_id from Endoresment_Nurses_Units where Nurse_id = @Nurse_id and Active = 1)", con))
        {
            cmd.Parameters.Add("@Nurse_id", SqlDbType.Int).Value = info.Emp_id;
            SqlDataReader idr = cmd.ExecuteReader();

            if (idr.HasRows)
            {
                NursesUnits = populateNursesUnitsLisst(idr, con);
            }
        }

        con.Close();

        return JsonConvert.SerializeObject(NursesUnits);
    }

    public static List<NursesUnits>
    populateNursesUnitsLisst(SqlDataReader idr, SqlConnection con)
    {
        List<NursesUnits> NursesUnitsI = new List<NursesUnits>();

        while (idr.Read())
        {
            NursesUnitsI
                .Add(new NursesUnits {
                    Nurse_id = idr["Nurse_id"] != DBNull.Value ? Convert.ToInt32(idr["Nurse_id"]) : 0,
                    Unit_id = idr["Unit_id"] != DBNull.Value ? Convert.ToInt32(idr["Unit_id"]) : 0,
                });
        }

        return NursesUnitsI;
    }

    public class NursesUnits
    {
        public int? Nurse_id { get; set; }
        public int? Unit_id { get; set; }
    }


    // get units
    [WebMethod]
    public static string getUnitsData(user info)
    {
        string config =
            Convert.ToString(ConfigurationManager.ConnectionStrings["dbcon"]);
        List<user> user = new List<user>();
        List<Units> Units = new List<Units>();

        SqlConnection con = new SqlConnection(config);

        con.Open();

        using (SqlCommand cmd = new SqlCommand("SELECT * from Units where U_id in (select Unit_id from Endoresment_Nurses_Units where Nurse_id = @Nurse_id)", con))
        {
            cmd.Parameters.Add("@Nurse_id", SqlDbType.Int).Value = info.Emp_id;
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
                    U_id = idr["U_id"] != DBNull.Value ? Convert.ToInt32(idr["U_id"]) : 0,
                    U_name = Convert.ToString(idr["U_name"]),

                });
        }

        return UnitsI;
    }

    public class Units
    {
        public int? U_id { get; set; }
        public string U_name { get; set; }
    }
}