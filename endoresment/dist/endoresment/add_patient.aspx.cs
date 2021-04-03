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

public partial class _addPatient : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
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
            cmd1.Parameters.Add("@Consultant_id", SqlDbType.Int).Value =
                data.Consultant_id;
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
}
