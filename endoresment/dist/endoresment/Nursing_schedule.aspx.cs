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

public partial class Nursing_schedule : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
    }

    //insert Nursing schedule
    [WebMethod]
    public static string newSchdule(NurseSchedule data)
    {
        string config =
            Convert.ToString(ConfigurationManager.ConnectionStrings["dbcon"]);

        List<NurseSchedule> NurseSchedule = new List<NurseSchedule>();

        SqlConnection con = new SqlConnection(config);

        con.Open();
        using (
            SqlCommand cmd1 =
                new SqlCommand("insert into Endorsement_Nursing_schedule (Shift_date,Shift,Unit_id,Unit_name,Nurse_id,Nurse_role,Nurse_name) values (@Date,@Shift,@Unit_id,@Unit_name,@Nurse_id,@Nurse_role,@Nurse_name)",
                    con)
        )
        {
            cmd1.Parameters.Add("@Date", SqlDbType.VarChar).Value = data.Date;
            cmd1.Parameters.Add("@Shift", SqlDbType.VarChar).Value = data.Shift;

            cmd1.Parameters.Add("@Unit_id", SqlDbType.Int).Value = data.Unit_id;
            cmd1.Parameters.Add("@Unit_name", SqlDbType.VarChar).Value =
                data.Unit_name;
            cmd1.Parameters.Add("@Nurse_id", SqlDbType.Int).Value =
                data.Nurse_id;
            cmd1.Parameters.Add("@Nurse_role", SqlDbType.Int).Value =
                data.Nurse_role;
            cmd1.Parameters.Add("@Nurse_name", SqlDbType.VarChar).Value =
                data.Nurse_name;

            cmd1.ExecuteNonQuery();
        }
        con.Close();

        return JsonConvert.SerializeObject(NurseSchedule);
    }

    public class NurseSchedule
    {
        public string Date { get; set; }

        public string Shift { get; set; }

        public int? Unit_id { get; set; }

        public string Unit_name { get; set; }

        public int? Nurse_id { get; set; }

        public int? Nurse_role { get; set; }

        public string Nurse_name { get; set; }
    }

    public class branches
    {
        public int? id { get; set; }

        public string Branch_EName { get; set; }
    }

    // get admin users data
    [WebMethod]
    public static string getadminusersData()
    {
        string config =
            Convert.ToString(ConfigurationManager.ConnectionStrings["dbcon"]);
        List<adminusers> adminusers = new List<adminusers>();

        SqlConnection con = new SqlConnection(config);

        con.Open();

        using (
            SqlCommand cmd =
                new SqlCommand("select * from adminusers where Role_id = 12 or Role_id = 17",
                    con)
        )
        {
            SqlDataReader idr = cmd.ExecuteReader();

            if (idr.HasRows)
            {
                adminusers = populateadminusersLisst(idr, con);
            }
        }

        con.Close();

        return JsonConvert.SerializeObject(adminusers);
    }

    public static List<adminusers>
    populateadminusersLisst(SqlDataReader idr, SqlConnection con)
    {
        List<adminusers> adminusersI = new List<adminusers>();

        while (idr.Read())
        {
            adminusersI
                .Add(new adminusers {
                    Emp_id =
                        idr["Emp_id"] != DBNull.Value
                            ? Convert.ToInt32(idr["Emp_id"])
                            : 0,
                    FullName =
                        idr["FullName"] != DBNull.Value
                            ? Convert.ToString(idr["FullName"])
                            : String.Empty,
                    Branch_ID =
                        idr["Branch_ID"] != DBNull.Value
                            ? Convert.ToInt32(idr["Branch_ID"])
                            : 0,
                    Role_id =
                        idr["Role_id"] != DBNull.Value
                            ? Convert.ToInt32(idr["Role_id"])
                            : 0
                });
        }

        return adminusersI;
    }

    public class adminusers
    {
        public int? Emp_id { get; set; }

        public string FullName { get; set; }

        public int? Branch_ID { get; set; }

        public int? Role_id { get; set; }
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
                new SqlCommand("select * from Units where Branch_id = @Branch_id",
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
}
