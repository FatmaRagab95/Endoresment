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

public partial class _roomDetails : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
    }

    // get units
    [WebMethod]
    public static string getUnitsData(branches branch)
    {
        string config =
            Convert.ToString(ConfigurationManager.ConnectionStrings["dbcon"]);
        List<Units> Units = new List<Units>();

        SqlConnection con = new SqlConnection(config);

        con.Open();

        using (SqlCommand cmd = new SqlCommand("select * from Units where Branch_id = @Branch_id and Show = 1", con))
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
    public static string getRoomsData(Rooms Room)
    {
        string config =
            Convert.ToString(ConfigurationManager.ConnectionStrings["dbcon"]);
        List<Rooms> Rooms = new List<Rooms>();

        SqlConnection con = new SqlConnection(config);

        con.Open();

        using (
            SqlCommand cmd =
                new SqlCommand("select * from Endorsement_Rooms where unit_id = @unit_id", con)
        )
        {
            cmd.Parameters.Add("@unit_id", SqlDbType.VarChar).Value =
                Room.unit_id;
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
                    Room_type_id =
                        idr["Room_type_id"] != DBNull.Value
                            ? Convert.ToInt32(idr["Room_type_id"])
                            : 0,
                    Unit = Convert.ToString(idr["Unit"]),
                    Room_name = Convert.ToString(idr["Room_name"]),
                    Room_type = Convert.ToString(idr["Room_type"])
                });
        }

        return RoomsI;
    }

    public class Rooms
    {
        public int? id { get; set; }

        public int? unit_id { get; set; }

        public int? Room_type_id { get; set; }

        public string Unit { get; set; }

        public string Room_name { get; set; }

        public string Room_type { get; set; }
    }

    // get rooms dashboard
    [WebMethod]
    public static string getRoomsDashboardData(Rooms Room)
    {
        string config =
            Convert.ToString(ConfigurationManager.ConnectionStrings["dbcon"]);
        List<RoomsDashboard> RoomsDashboard = new List<RoomsDashboard>();
        List<Rooms> Rooms = new List<Rooms>();

        SqlConnection con = new SqlConnection(config);

        con.Open();

        using (
            SqlCommand cmd =
                new SqlCommand("select * from Endorsement_RoomsDashboard where Room_id in (select id from Endorsement_Rooms where unit_id = @unit_id)", con)
        )
        {
            cmd.Parameters.Add("@unit_id", SqlDbType.VarChar).Value =
                Room.unit_id;
            SqlDataReader idr = cmd.ExecuteReader();

            if (idr.HasRows)
            {
                RoomsDashboard = populateRoomsDashboardLisst(idr, con);
            }
        }

        con.Close();

        return JsonConvert.SerializeObject(RoomsDashboard);
    }

    public static List<RoomsDashboard>
    populateRoomsDashboardLisst(SqlDataReader idr, SqlConnection con)
    {
        List<RoomsDashboard> RoomsDashboardI = new List<RoomsDashboard>();

        while (idr.Read())
        {
            RoomsDashboardI
                .Add(new RoomsDashboard {
                    id =
                        idr["id"] != DBNull.Value
                            ? Convert.ToInt32(idr["id"])
                            : 0,
                    Room_id =
                        idr["Room_id"] != DBNull.Value
                            ? Convert.ToInt32(idr["Room_id"])
                            : 0,
                    Bed_name = Convert.ToString(idr["Bed_name"]),
                    Status_id =
                        idr["Status_id"] != DBNull.Value
                            ? Convert.ToInt32(idr["Status_id"])
                            : 0,
                    Status_name = Convert.ToString(idr["Status_name"]),
                    Shift = Convert.ToString(idr["Shift"]),
                    Entry_User =
                        idr["Entry_User"] != DBNull.Value
                            ? Convert.ToInt32(idr["Entry_User_id"])
                            : 0,
                    Entry_User_name = Convert.ToString(idr["Entry_User_name"])
                });
        }

        return RoomsDashboardI;
    }

    public class RoomsDashboard
    {
        public int? id { get; set; }

        public int? Room_id { get; set; }

        public string Bed_name { get; set; }

        public int? Status_id { get; set; }

        public string Status_name { get; set; }

        public string Shift { get; set; }

        public int? Entry_User { get; set; }

        public string Entry_User_name { get; set; }
    }

    // get patients data
    [WebMethod]
    public static string getPatientsData(Endorsement_PatientData info)
    {
        string config =
            Convert.ToString(ConfigurationManager.ConnectionStrings["dbcon"]);
        List<Endorsement_PatientData> Endorsement_PatientData =
            new List<Endorsement_PatientData>();

        SqlConnection con = new SqlConnection(config);

        con.Open();

        using (
            SqlCommand cmd =
                new SqlCommand("select * from Endorsement_PatientData WHERE Patient_Status = 1 and Branch_id = @Branch_id and Unit = @Unit",con)
        )
        {
            cmd.Parameters.Add("@Unit", SqlDbType.VarChar).Value =
                info.Unit;
            cmd.Parameters.Add("@Branch_id", SqlDbType.VarChar).Value =
                info.Branch_id;
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
        public int? Branch_id { get; set; }
        public string Unit { get; set; }

        public string Room { get; set; }

        public string Gender { get; set; }

        public Int64? Medical_Number { get; set; }

        public string Age { get; set; }

        public string Specialty { get; set; }
    }

    // insert new patient data
    [WebMethod]
    public static string newPatient(PatientData data)
    {
        string config =
            Convert.ToString(ConfigurationManager.ConnectionStrings["dbcon"]);

        List<PatientData> PatientData = new List<PatientData>();

        SqlConnection con = new SqlConnection(config);

        con.Open();
        using (
            SqlCommand cmd1 =
                new SqlCommand("insert into Endorsement_PatientData (Patient_FullName, Branch_id, Branch_name,Gender,Unit,Room,Bed_id,Addmission_date,Age,Specialty,Consultant_id,Consultant_Name,Patient_Status,Entry_user,Medical_Number) values (@Patient_FullName,@Branch_id, (select Branch_EName from Branches where id = @Branch_id), @Gender, @Unit, @Room, @Bed_id, @Addmission_date, @Age, @Specialty,@Consultant_id,@Consultant_Name, @Patient_Status ,@Entry_user, @Medical_Number)",
                    con)
        )
        {
            cmd1.Parameters.Add("@Patient_FullName", SqlDbType.VarChar).Value =
                data.Patient_FullName;
            cmd1.Parameters.Add("@Medical_Number", SqlDbType.Int).Value =
                data.Medical_Number;
            cmd1.Parameters.Add("@Branch_id", SqlDbType.Int).Value =
                data.Branch_id;
            cmd1.Parameters.Add("@Gender", SqlDbType.VarChar).Value =
                data.Gender;
            cmd1.Parameters.Add("@Unit", SqlDbType.VarChar).Value = data.Unit;
            cmd1.Parameters.Add("@Room", SqlDbType.VarChar).Value = data.Room;
            cmd1.Parameters.Add("@Bed_id", SqlDbType.Int).Value = data.Bed_id;
            cmd1.Parameters.Add("@Addmission_date", SqlDbType.VarChar).Value =
                data.Addmission_date;
            cmd1.Parameters.Add("@Age", SqlDbType.VarChar).Value = data.Age;
            cmd1.Parameters.Add("@Specialty", SqlDbType.VarChar).Value =
                data.Specialty;
            cmd1.Parameters.Add("@Consultant_id", SqlDbType.Int).Value = data.Consultant_id;
            cmd1.Parameters.Add("@Consultant_Name", SqlDbType.VarChar).Value =
                data.Consultant_Name;
            cmd1.Parameters.Add("@Patient_Status", SqlDbType.Int).Value = 1;
            cmd1.Parameters.Add("@Entry_user", SqlDbType.Int).Value =
                data.Entry_user;

            cmd1.ExecuteNonQuery();
        }
        con.Close();

        // update units dashboard
        con.Open();
        using (
            SqlCommand cmd2 =
                new SqlCommand("update Endorsement_UnitsDashboard SET Total_Census = Total_Census + 1, Admission = Admission + 1 where Unit_name in (select TOP(1) Unit_name from Endorsement_UnitsDashboard where  Unit_name = @Unit_name)",
                    con)
        )
        {
            cmd2.Parameters.Add("@Unit_name", SqlDbType.VarChar).Value =
                data.Unit;
            cmd2.ExecuteNonQuery();
        }

        con.Close();

        // update Rooms Dashboard
        con.Open();
        using (
            SqlCommand cmd3 =
                new SqlCommand("update Endorsement_RoomsDashboard SET Status_id = 2, Status_name = 'Full' where id = @id",
                    con)
        )
        {
            cmd3.Parameters.Add("@id", SqlDbType.Int).Value = data.Bed_id;
            cmd3.ExecuteNonQuery();
        }

        con.Close();

        return JsonConvert.SerializeObject(PatientData);
    }

    public class PatientData
    {
        public string Patient_FullName { get; set; }

        public int? Branch_id { get; set; }

        public int? Medical_Number { get; set; }

        public string Branch_name { get; set; }

        public string Gender { get; set; }

        public int? Bed_id { get; set; }

        public string Unit { get; set; }

        public string Room { get; set; }

        public string Addmission_date { get; set; }

        public string Age { get; set; }

        public string Specialty { get; set; }

        public int? Consultant_id { get; set; }

        public string Consultant_Name { get; set; }

        public int? Patient_Status { get; set; }

        public int? Entry_user { get; set; }
    }

    // get branches
    [WebMethod]
    public static string getBranchesData()
    {
        string config =
            Convert.ToString(ConfigurationManager.ConnectionStrings["dbcon"]);
        List<branches> branches = new List<branches>();

        SqlConnection con = new SqlConnection(config);

        con.Open();

        using (SqlCommand cmd = new SqlCommand("select * from Branches", con))
        {
            SqlDataReader idr = cmd.ExecuteReader();

            if (idr.HasRows)
            {
                branches = populatebranchesLisst(idr, con);
            }
        }

        con.Close();

        return JsonConvert.SerializeObject(branches);
    }

    public static List<branches>
    populatebranchesLisst(SqlDataReader idr, SqlConnection con)
    {
        List<branches> branchesI = new List<branches>();

        while (idr.Read())
        {
            branchesI
                .Add(new branches {
                    id =
                        idr["id"] != DBNull.Value
                            ? Convert.ToInt32(idr["id"])
                            : 0,
                    Branch_EName = Convert.ToString(idr["Branch_EName"])
                });
        }

        return branchesI;
    }

    public class branches
    {
        public int? id { get; set; }

        public string Branch_EName { get; set; }
    }

    // get Specialities
    [WebMethod]
    public static string getSpecialitiesData()
    {
        string config =
            Convert.ToString(ConfigurationManager.ConnectionStrings["dbcon"]);
        List<Specialities> Specialities = new List<Specialities>();

        SqlConnection con = new SqlConnection(config);

        con.Open();

        using (
            SqlCommand cmd = new SqlCommand("select * from Specialities", con)
        )
        {
            SqlDataReader idr = cmd.ExecuteReader();

            if (idr.HasRows)
            {
                Specialities = populateSpecialitiesLisst(idr, con);
            }
        }

        con.Close();

        return JsonConvert.SerializeObject(Specialities);
    }

    public static List<Specialities>
    populateSpecialitiesLisst(SqlDataReader idr, SqlConnection con)
    {
        List<Specialities> SpecialitiesI = new List<Specialities>();

        while (idr.Read())
        {
            SpecialitiesI
                .Add(new Specialities {
                    Spcy_id =
                        idr["Spcy_id"] != DBNull.Value
                            ? Convert.ToInt32(idr["Spcy_id"])
                            : 0,
                    Spcy_name_En = Convert.ToString(idr["Spcy_name_En"])
                });
        }

        return SpecialitiesI;
    }

    public class Specialities
    {
        public int? Spcy_id { get; set; }

        public string Spcy_name_En { get; set; }
    }

    // get Consultants
    [WebMethod]
    public static string getConsultantsData(Consultants info)
    {
        string config = Convert.ToString(ConfigurationManager.ConnectionStrings["dbcon"]);
        List<Consultants> Consultants = new List<Consultants>();

        SqlConnection con = new SqlConnection(config);

        con.Open();

        using (
            SqlCommand cmd = new SqlCommand("select * from Doctors_Data where Type = 'Consultant' and Spcy_id = @Spcy_id and Branch = @Branch_ID", con)
        )
        {
            cmd.Parameters.Add("@Spcy_id", SqlDbType.Int).Value = info.Spcy_id;
            cmd.Parameters.Add("@Branch_ID", SqlDbType.Int).Value = info.Branch_ID;
            SqlDataReader idr = cmd.ExecuteReader();

            if (idr.HasRows)
            {
                Consultants = populateConsultantsLisst(idr, con);
            }
        }

        con.Close();

        return JsonConvert.SerializeObject(Consultants);
    }

    public static List<Consultants>
    populateConsultantsLisst(SqlDataReader idr, SqlConnection con)
    {
        List<Consultants> ConsultantsI = new List<Consultants>();

        while (idr.Read())
        {
            ConsultantsI
                .Add(new Consultants {
                    Dr_Code = idr["Dr_Code"] != DBNull.Value ? Convert.ToInt32(idr["Dr_Code"]) : 0,
                    DR_Name = Convert.ToString(idr["DR_Name"]),
                    Spcy_id = idr["Spcy_id"] != DBNull.Value ? Convert.ToInt32(idr["Spcy_id"]) : 0,
                });
        }

        return ConsultantsI;
    }

    public class Consultants
    {
        public int? Dr_Code { get; set; }
        public string DR_Name { get; set; }
        public int? Spcy_id { get; set; }
        public int? Branch_ID { get; set; }
    }
}
