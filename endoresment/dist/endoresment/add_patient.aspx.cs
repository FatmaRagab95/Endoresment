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
	public static string newPatient(Endorsement_PatientData data)

	{
		string config = Convert.ToString(ConfigurationManager.ConnectionStrings["dbcon"]);

		List<Endorsement_PatientData> Endorsement_PatientData = new List<Endorsement_PatientData>();

		SqlConnection con = new SqlConnection(config);

		con.Open();
        using (SqlCommand cmd1 = new SqlCommand("insert into Endorsement_PatientData (Patient_FullName, Branch_id, Branch_name,Gender,Medical_Number,Unit,Room,Addmission_date,Age,Date_Birth,Consultant_Name,Specialty,Patient_Status) values (@Patient_FullName,@Branch_id, @Branch_name, @Gender, @Medical_Number, @Unit, @Room, @Addmission_date, @Age, @Date_Birth, @Consultant_Name, @Specialty, @Patient_Status)", con))

        {
            cmd1.Parameters.Add("@Patient_FullName", SqlDbType.VarChar).Value = data.Patient_FullName;
            cmd1.Parameters.Add("@Branch_id", SqlDbType.Int).Value  = data.Branch_id;
            cmd1.Parameters.Add("@Branch_name", SqlDbType.VarChar).Value = data.Branch_name;
            cmd1.Parameters.Add("@Gender", SqlDbType.VarChar).Value = data.Gender;
            cmd1.Parameters.Add("@Medical_Number", SqlDbType.BigInt).Value   = data.Medical_Number;
            cmd1.Parameters.Add("@Unit", SqlDbType.VarChar).Value   = data.Unit;
            cmd1.Parameters.Add("@Room", SqlDbType.VarChar).Value   = data.Room;
            cmd1.Parameters.Add("@Addmission_date", SqlDbType.VarChar).Value   = data.Addmission_date;
            cmd1.Parameters.Add("@Age", SqlDbType.Int).Value  = data.Age;
            cmd1.Parameters.Add("@Date_Birth", SqlDbType.VarChar).Value   = data.Date_Birth;
            cmd1.Parameters.Add("@Consultant_Name", SqlDbType.VarChar).Value   = data.Consultant_Name;
            cmd1.Parameters.Add("@Specialty", SqlDbType.VarChar).Value   = data.Specialty;
            cmd1.Parameters.Add("@Patient_Status", SqlDbType.Int).Value  = 1;

            cmd1.ExecuteNonQuery();

        }
		con.Close();
        

        // update units dashboard
        using (SqlCommand cmd2 = new SqlCommand("select * from Endorsement_UnitsDashboard where Unit_name = @Unit_name", con))
        {
            
            con.Open();
            cmd2.Parameters.Add("@Unit_name", SqlDbType.VarChar).Value   = data.Unit;
            
            SqlDataReader idr = cmd2.ExecuteReader();

            if (idr.HasRows)
            {
                con.Close();
                con.Open();
                using (SqlCommand cmd1 = new SqlCommand("update Endorsement_UnitsDashboard Total_Census = Total_Census + 1, Admission = Admission + 1", con))

                {
                    cmd1.ExecuteNonQuery();
                }
                con.Close();
            }
        }

        con.Close();

        return JsonConvert.SerializeObject(Endorsement_PatientData);

	}
    
    public class Endorsement_PatientData
    {
        public string Patient_FullName { get; set; }
        public int? Branch_id { get; set; }
        public string Branch_name { get; set; }
        public string Gender { get; set; }
        public Int64 Medical_Number { get; set; }
        public int? Unit_id { get; set; }
        public string Unit { get; set; }
        public string Room { get; set; }
        public string Addmission_date { get; set; }
        public int? Age { get; set; }
        public string Consultant_Name { get; set; }
        public string Specialty { get; set; }
        public string Date_Birth { get; set; }
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

						Branch_EName = Convert.ToString(idr["Branch_EName"]),
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

        using (SqlCommand cmd = new SqlCommand("select * from Specialities", con))
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

						Spcy_name_En = Convert.ToString(idr["Spcy_name_En"]),
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

        using (SqlCommand cmd = new SqlCommand("select * from Units where Branch_id = @Branch_id", con))
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

        using (SqlCommand cmd = new SqlCommand("select * from Units_Rooms where U_id = @U_id", con))
        {
            cmd.Parameters.Add("@U_id", SqlDbType.Int).Value = unit.U_id;
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
                    R_id =
                        idr["R_id"] != DBNull.Value
                            ? Convert.ToInt32(idr["R_id"])
                            : 0,

						R_name = Convert.ToString(idr["R_name"]),
                });
        }

        return RoomsI;
    }

    public class Rooms
    {
        public int? R_id { get; set; }

        public string R_name { get; set; }
    }
}
