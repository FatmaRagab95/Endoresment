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

public partial class display_schdule : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
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

    // get Endorsement_Nursing_schedule
    [WebMethod]
    public static string getEndorsement_Nursing_scheduleData()
    {
        string config =
            Convert.ToString(ConfigurationManager.ConnectionStrings["dbcon"]);
        List<Endorsement_Nursing_schedule> Endorsement_Nursing_schedule =
            new List<Endorsement_Nursing_schedule>();

        SqlConnection con = new SqlConnection(config);

        con.Open();

        using (
            SqlCommand cmd =
                new SqlCommand("select * from Endorsement_Nursing_schedule",
                    con)
        )
        {
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
                        idr["Date"] != DBNull.Value
                            ? Convert.ToString(idr["Date"])
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

        public string Unit_name { get; set; }

        public int? Nurse_role { get; set; }

        public string Nurse_name { get; set; }

        public int? Nurse_id { get; set; }
    }
}
