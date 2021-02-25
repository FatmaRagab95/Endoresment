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

public partial class _editNurses : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
    }

    // get units
    [WebMethod]
    public static string getUnitsData()
    {
        string config =
            Convert.ToString(ConfigurationManager.ConnectionStrings["dbcon"]);
        List<Units> Units = new List<Units>();

        SqlConnection con = new SqlConnection(config);

        con.Open();

        using (SqlCommand cmd = new SqlCommand("select * from Units", con))
        {
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
                new SqlCommand("select * from adminusers where Role_id  = 12",
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
    }

    // get Endoresment_Nurses_Units
    [WebMethod]
    public static string getEndoresment_Nurses_UnitsData()
    {
        string config =
            Convert.ToString(ConfigurationManager.ConnectionStrings["dbcon"]);
        List<Endoresment_Nurses_Units> Endoresment_Nurses_Units =
            new List<Endoresment_Nurses_Units>();

        SqlConnection con = new SqlConnection(config);

        con.Open();

        using (
            SqlCommand cmd =
                new SqlCommand("select * from Endoresment_Nurses_Units where Active = 1",
                    con)
        )
        {
            SqlDataReader idr = cmd.ExecuteReader();

            if (idr.HasRows)
            {
                Endoresment_Nurses_Units =
                    populateEndoresment_Nurses_UnitsLisst(idr, con);
            }
        }

        con.Close();

        return JsonConvert.SerializeObject(Endoresment_Nurses_Units);
    }

    public static List<Endoresment_Nurses_Units>
    populateEndoresment_Nurses_UnitsLisst(SqlDataReader idr, SqlConnection con)
    {
        List<Endoresment_Nurses_Units> Endoresment_Nurses_UnitsI =
            new List<Endoresment_Nurses_Units>();

        while (idr.Read())
        {
            Endoresment_Nurses_UnitsI
                .Add(new Endoresment_Nurses_Units {
                    Id =
                        idr["Id"] != DBNull.Value
                            ? Convert.ToInt32(idr["Id"])
                            : 0,
                    Nurse_id =
                        idr["Nurse_id"] != DBNull.Value
                            ? Convert.ToInt32(idr["Nurse_id"])
                            : 0,
                    Unit_id =
                        idr["Unit_id"] != DBNull.Value
                            ? Convert.ToInt32(idr["Unit_id"])
                            : 0,
                    Active =
                        idr["Active"] != DBNull.Value
                            ? Convert.ToInt32(idr["Active"])
                            : 1
                });
        }

        return Endoresment_Nurses_UnitsI;
    }

    public class Endoresment_Nurses_Units
    {
        public int? Id { get; set; }

        public int? Nurse_id { get; set; }

        public int? Unit_id { get; set; }

        public int? Active { get; set; }
    }

    // insert nurse selection
    [WebMethod]
    public static string insertNurseSelection(selectedNurse data)
    {
        string config =
            Convert.ToString(ConfigurationManager.ConnectionStrings["dbcon"]);

        List<selectedNurse> selectedNurse = new List<selectedNurse>();

        SqlConnection con = new SqlConnection(config);

        con.Open();
        using (
            SqlCommand cmd1 =
                new SqlCommand("insert into Endoresment_Nurses_Units (Nurse_id, Unit_id, Entry_user, Active) values (@Nurse_id, @Unit_id, @Entry_user, @Active)",
                    con)
        )
        {
            cmd1.Parameters.Add("@Nurse_id", SqlDbType.Int).Value =
                data.Nurse_id;
            cmd1.Parameters.Add("@Unit_id", SqlDbType.Int).Value = data.Unit_id;
            cmd1.Parameters.Add("@Entry_user", SqlDbType.Int).Value =
                data.Entry_user;
            cmd1.Parameters.Add("@Active", SqlDbType.Int).Value = data.Active;

            cmd1.ExecuteNonQuery();
        }
        con.Close();

        return JsonConvert.SerializeObject(selectedNurse);
    }

    public class selectedNurse
    {
        public int? Nurse_id { get; set; }

        public int? Unit_id { get; set; }

        public int? Entry_user { get; set; }

        public int? Active { get; set; }
    }

    // update nurse selection
    [WebMethod]
    public static string updatedNurse(nursetDetails detail)
    {
        string config =
            Convert.ToString(ConfigurationManager.ConnectionStrings["dbcon"]);

        List<nursetDetails> nursetDetails = new List<nursetDetails>();

        SqlConnection con = new SqlConnection(config);

        con.Open();

        using (
            SqlCommand cmd =
                new SqlCommand("update Endoresment_Nurses_Units set Nurse_id=@Nurse_id, Unit_id=@Unit_id, Entry_user=@Entry_user, Last_Update=@Last_Update, Active=@Active where Id = @Id;",
                    con)
        )
        {
            cmd.Parameters.AddWithValue("@Id", detail.Id);
            cmd.Parameters.AddWithValue("@Nurse_id", detail.Nurse_id);
            cmd.Parameters.AddWithValue("@Unit_id", detail.Unit_id);
            cmd.Parameters.AddWithValue("@Entry_user", detail.Entry_user);
            cmd.Parameters.AddWithValue("@Last_Update", detail.Last_Update);
            cmd.Parameters.AddWithValue("@Active", detail.Active);

            cmd.ExecuteNonQuery();
        }

        con.Close();

        return JsonConvert.SerializeObject(nursetDetails);
    }

    public static List<nursetDetails>
    populatenursetDetailsLisst(SqlDataReader idr, SqlConnection con)
    {
        List<nursetDetails> nursetDetailsI = new List<nursetDetails>();

        while (idr.Read())
        {
            nursetDetailsI
                .Add(new nursetDetails {
                    Id = Convert.ToInt32(idr["Id"]),
                    Nurse_id = Convert.ToInt32(idr["Nurse_id"]),
                    Unit_id = Convert.ToInt32(idr["Unit_id"]),
                    Entry_user = Convert.ToInt32(idr["Entry_user"]),
                    Last_Update = Convert.ToString(idr["Last_Update"]),
                    Active = Convert.ToInt32(idr["Active"])
                });
        }
        return nursetDetailsI;
    }

    public class nursetDetails
    {
        public int? Id { get; set; }

        public int? Nurse_id { get; set; }

        public int? Unit_id { get; set; }

        public int? Entry_user { get; set; }

        public string Last_Update { get; set; }

        public int? Active { get; set; }
    }
}
