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

public partial class _addUnit : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
    }

    // insert new Unit dashboard data
	[WebMethod]
	public static string newUnit(Endorsement_UnitsDashboard data)

	{
		string config = Convert.ToString(ConfigurationManager.ConnectionStrings["dbcon"]);

		List<Endorsement_UnitsDashboard> Endorsement_UnitsDashboard = new List<Endorsement_UnitsDashboard>();

		SqlConnection con = new SqlConnection(config);

		con.Open();
        using (SqlCommand cmd1 = new SqlCommand("insert into Endorsement_UnitsDashboard (Unit_id,Branch_id, Unit_name, Shift,Shift_date,Total_Census,Received,Admission,Transfer_In,Transfer_Out,Endorsing_ChargeNurse,Endorsing_ChargeNurse_id,Receive_ChargeNurse,Receive_ChargeNurse_id, Confirm) values (@Unit_id,@Branch_id, @Unit_name, @Shift,@Shift_date,@Received,@Received,@Admission,@Transfer_In,@Transfer_Out,@Endorsing_ChargeNurse,@Endorsing_ChargeNurse_id,@Receive_ChargeNurse,@Receive_ChargeNurse_id, 'True')", con))

        {
            cmd1.Parameters.Add("@Unit_id", SqlDbType.Int).Value  = data.Unit_id;
            cmd1.Parameters.Add("@Branch_id", SqlDbType.Int).Value  = data.Branch_id;
            cmd1.Parameters.Add("@Unit_name", SqlDbType.VarChar).Value = data.Unit_name;
            cmd1.Parameters.Add("@Shift", SqlDbType.VarChar).Value = data.Shift;
            cmd1.Parameters.Add("@Shift_date", SqlDbType.DateTime).Value   = data.Shift_date;
            cmd1.Parameters.Add("@Total_Census", SqlDbType.Int).Value  = data.Total_Census;
            cmd1.Parameters.Add("@Received", SqlDbType.Int).Value  = data.Received;
            cmd1.Parameters.Add("@Admission", SqlDbType.Int).Value  = data.Admission;
            cmd1.Parameters.Add("@Transfer_In", SqlDbType.Int).Value  = data.Transfer_In;
            cmd1.Parameters.Add("@Transfer_Out", SqlDbType.Int).Value  = data.Transfer_Out;
            cmd1.Parameters.Add("@Endorsing_ChargeNurse", SqlDbType.VarChar).Value   = data.Endorsing_ChargeNurse;
            cmd1.Parameters.Add("@Endorsing_ChargeNurse_id", SqlDbType.Int).Value  = data.Endorsing_ChargeNurse_id;
            cmd1.Parameters.Add("@Receive_ChargeNurse", SqlDbType.VarChar).Value   = data.Receive_ChargeNurse;
            cmd1.Parameters.Add("@Receive_ChargeNurse_id", SqlDbType.Int).Value  = data.Receive_ChargeNurse_id;

            cmd1.ExecuteNonQuery();

        }
		con.Close();

        return JsonConvert.SerializeObject(Endorsement_UnitsDashboard);

	}

    // if it's handover shift update the handed shift as completed
    [WebMethod]
    public static string completeNurseShift(Endorsement_UnitsDashboard shift)
    {
        string config =  Convert.ToString(ConfigurationManager.ConnectionStrings["dbcon"]);
        List<Endorsement_UnitsDashboard> Endorsement_UnitsDashboard = new List<Endorsement_UnitsDashboard>();

        SqlConnection con = new SqlConnection(config);

        con.Open();

        
        using (SqlCommand cmd = new SqlCommand("update Endorsement_UnitsDashboard set Completed = 1 where Id = @Id", con))
        {
            cmd.Parameters.Add("@Id", SqlDbType.Int).Value = shift.Id;
            SqlDataReader idr = cmd.ExecuteReader();
        }

        con.Close();

        return JsonConvert.SerializeObject(Endorsement_UnitsDashboard);
    }
    
    public class Endorsement_UnitsDashboard
    {
        public int? Id { get; set; }
        public int? Unit_id { get; set; }
        public int? Branch_id { get; set; }
        public string Unit_name { get; set; }
        public string Shift { get; set; }
        public DateTime Shift_date { get; set; }
        public int? Total_Census { get; set; }
        public int? Received { get; set; }
        public int? Admission { get; set; }
        public int? Transfer_In { get; set; }
        public int? Transfer_Out { get; set; }
        public string Endorsing_ChargeNurse { get; set; }
        public int? Endorsing_ChargeNurse_id { get; set; }
        public string Receive_ChargeNurse { get; set; }
        public int? Receive_ChargeNurse_id { get; set; }
    }

            // get Endorsement_Nursing_schedule
    [WebMethod]
    public static string getScheduleData(Endorsement_Nursing_schedule data)
    {
        string config =
            Convert.ToString(ConfigurationManager.ConnectionStrings["dbcon"]);
        List<Endorsement_Nursing_schedule> Endorsement_Nursing_schedule =
            new List<Endorsement_Nursing_schedule>();

        SqlConnection con = new SqlConnection(config);

        con.Open();

        using (
            SqlCommand cmd =
                new SqlCommand("select * from Endorsement_Nursing_schedule where Nurse_id = @Nurse_id and Shift_date >=  DATEADD(day,-2, GETDATE()) and Shift_date <  DATEADD(day,0, GETDATE())",
                    con)
        )
        {
            cmd.Parameters.Add("@Nurse_id", SqlDbType.Int).Value = data.Nurse_id;
            SqlDataReader idr = cmd.ExecuteReader();

            if (idr.HasRows)
            {
                Endorsement_Nursing_schedule =
                    populateEndorsement_Nursing_scheduleLisst(idr, con);
            }
        }

        con.Close();

        return JsonConvert.SerializeObject(Endorsement_Nursing_schedule);
    }

    public static List<Endorsement_Nursing_schedule>
    populateEndorsement_Nursing_scheduleLisst(
        SqlDataReader idr,
        SqlConnection con
    )
    {
        List<Endorsement_Nursing_schedule> Endorsement_Nursing_scheduleI =
            new List<Endorsement_Nursing_schedule>();

        while (idr.Read())
        {
            Endorsement_Nursing_scheduleI
                .Add(new Endorsement_Nursing_schedule {
                    Id =
                        idr["Id"] != DBNull.Value
                            ? Convert.ToInt32(idr["Id"])
                            : 0,
                    Date =
                        idr["Shift_date"] != DBNull.Value
                            ? Convert.ToString(idr["Shift_date"])
                            : String.Empty,
                    Shift =
                        idr["Shift"] != DBNull.Value
                            ? Convert.ToString(idr["Shift"])
                            : String.Empty,
                    Unit_id =
                        idr["Unit_id"] != DBNull.Value
                            ? Convert.ToInt32(idr["Unit_id"])
                            : 0,
                    Unit_name =
                        idr["Unit_name"] != DBNull.Value
                            ? Convert.ToString(idr["Unit_name"])
                            : String.Empty,
                    Nurse_role =
                        idr["Nurse_role"] != DBNull.Value
                            ? Convert.ToInt32(idr["Nurse_role"])
                            : 0,
                    Nurse_name =
                        idr["Nurse_name"] != DBNull.Value
                            ? Convert.ToString(idr["Nurse_name"])
                            : String.Empty,
                    Nurse_id =
                        idr["Nurse_id"] != DBNull.Value
                            ? Convert.ToInt32(idr["Nurse_id"])
                            : 0
                });
        }

        return Endorsement_Nursing_scheduleI;
    }

    public class Endorsement_Nursing_schedule
    {
        public int? Id { get; set; }

        public string Date { get; set; }

        public string Shift { get; set; }

        public int? Unit_id { get; set; }
        public int? Role_id { get; set; }

        public string Unit_name { get; set; }

        public int? Nurse_role { get; set; }

        public string Nurse_name { get; set; }

        public int? Nurse_id { get; set; }
    }

        // get Charge Nurses
    [WebMethod]
    public static string getNursesData(Endorsement_Nursing_schedule data)
    {
        string config =
            Convert.ToString(ConfigurationManager.ConnectionStrings["dbcon"]);
        List<Endorsement_Nursing_schedule> user = new List<Endorsement_Nursing_schedule>();
        List<Nurses> Nurses = new List<Nurses>();

        SqlConnection con = new SqlConnection(config);

        con.Open();

        using (SqlCommand cmd = new SqlCommand("select * from adminusers where Role_id = @Role_id and Emp_ID in (select Nurse_id from Endorsement_Nursing_schedule where Unit_id = @Unit_id and Shift_date = @Date and (Shift = @Shift))", con))
        {
            cmd.Parameters.Add("@Unit_id", SqlDbType.Int).Value = data.Unit_id;
            cmd.Parameters.Add("@Role_id", SqlDbType.Int).Value = data.Role_id;
            cmd.Parameters.Add("@Shift", SqlDbType.VarChar).Value = data.Shift;
            cmd.Parameters.Add("@Date", SqlDbType.VarChar).Value = data.Date;
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


    // get Nurses
    /*
    [WebMethod]
    public static string getNursesData(branches branch)
    {
        string config =
            Convert.ToString(ConfigurationManager.ConnectionStrings["dbcon"]);
        List<branches> branches = new List<branches>();
        List<Nurses> Nurses = new List<Nurses>();

        SqlConnection con = new SqlConnection(config);

        con.Open();

        using (SqlCommand cmd = new SqlCommand("select * from adminusers where Role_id = 17 and Branch_ID = @Branch_id", con))
        {
            cmd.Parameters.Add("@Branch_id", SqlDbType.Int).Value = branch.id;
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
    }*/


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

    public class branches
    {
        public int? id { get; set; }
    }
}
