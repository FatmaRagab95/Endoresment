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

public partial class _Nurses : System.Web.UI.Page
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
                new SqlCommand("select * from Endoresment_Nurses_Units", con)
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
                    Entry_user =
                        idr["Entry_user"] != DBNull.Value
                            ? Convert.ToInt32(idr["Entry_user"])
                            : 0,
                    Entry_date =
                        idr["Entry_date"] != DBNull.Value
                            ? Convert.ToString(idr["Entry_date"])
                            : String.Empty,
                    Last_Update =
                        idr["Last_Update"] != DBNull.Value
                            ? Convert.ToString(idr["Last_Update"])
                            : String.Empty,
                    Update_From =
                        idr["Update_From"] != DBNull.Value
                            ? Convert.ToString(idr["Update_From"])
                            : String.Empty,
                    Update_FromId =
                        idr["Update_FromId"] != DBNull.Value
                            ? Convert.ToInt32(idr["Update_FromId"])
                            : 0
                });
        }

        return Endoresment_Nurses_UnitsI;
    }

    public class Endoresment_Nurses_Units
    {
        public int? Id { get; set; }

        public int? Nurse_id { get; set; }

        public int? Unit_id { get; set; }

        public int? Entry_user { get; set; }

        public string Entry_date { get; set; }

        public string Last_Update { get; set; }

        public string Update_From { get; set; }

        public int? Update_FromId { get; set; }
    }
}
