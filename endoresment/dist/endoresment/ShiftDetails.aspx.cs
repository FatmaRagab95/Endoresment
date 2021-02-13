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

public partial class _ShiftDetials : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
    }
    
    // get unit dashboard
    [WebMethod]
    public static string getUnitsDashData(UnitsDash id)
    {
        string config =
            Convert.ToString(ConfigurationManager.ConnectionStrings["dbcon"]);
        List<UnitsDash> UnitsDash = new List<UnitsDash>();

        SqlConnection con = new SqlConnection(config);

        con.Open();

        using (SqlCommand cmd = new SqlCommand("SELECT * from Endorsement_UnitsDashboard where id = @id", con))
        {
            cmd.Parameters.Add("@id", SqlDbType.Int).Value = id.id;
            SqlDataReader idr = cmd.ExecuteReader();

            if (idr.HasRows)
            {
                UnitsDash = populateUnitsDashLisst(idr, con);
            }
        }

        con.Close();

        return JsonConvert.SerializeObject(UnitsDash);
    }

    public static List<UnitsDash>
    populateUnitsDashLisst(SqlDataReader idr, SqlConnection con)
    {
        List<UnitsDash> UnitsDashI = new List<UnitsDash>();

        while (idr.Read())
        {
            UnitsDashI
                .Add(new UnitsDash {
                    id = idr["id"] != DBNull.Value ? Convert.ToInt32(idr["id"]) : 0,
                    Unit_name = Convert.ToString(idr["Unit_name"]),
                    Shift = Convert.ToString(idr["Shift"]),
                    Shift_date = Convert.ToString(idr["Shift_date"]),
                    Total_Census = idr["Total_Census"] != DBNull.Value ? Convert.ToInt32(idr["Total_Census"]) : 0,
                    Received = idr["Received"] != DBNull.Value ? Convert.ToInt32(idr["Received"]) : 0,
                    Admission = idr["Admission"] != DBNull.Value ? Convert.ToInt32(idr["Admission"]) : 0,
                    Transfer_In = idr["Transfer_In"] != DBNull.Value ? Convert.ToInt32(idr["Transfer_In"]) : 0,
                    Transfer_Out = idr["Transfer_Out"] != DBNull.Value ? Convert.ToInt32(idr["Transfer_Out"]) : 0,
                    Endorsing_ChargeNurse_id = idr["Endorsing_ChargeNurse_id"] != DBNull.Value ? Convert.ToInt32(idr["Endorsing_ChargeNurse_id"]) : 0,
                    Endorsing_ChargeNurse = Convert.ToString(idr["Endorsing_ChargeNurse"]),
                    Receive_ChargeNurse_id = idr["Receive_ChargeNurse_id"] != DBNull.Value ? Convert.ToInt32(idr["Receive_ChargeNurse_id"]) : 0,
                    Receive_ChargeNurse = Convert.ToString(idr["Receive_ChargeNurse"]),
                    Entry_date = Convert.ToString(idr["Entry_date"]),

                });
        }

        return UnitsDashI;
    }

    public class UnitsDash
    {
        public int id { get; set; }
        public int? Unit_id { get; set; }
        public string Unit_name { get; set; }
        public string Shift { get; set; }
        public string Shift_date { get; set; }
        public int? Total_Census { get; set; }
        public int? Received { get; set; }
        public int? Admission { get; set; }
        public int? Transfer_In { get; set; }
        public int? Transfer_Out { get; set; }
        public int? Endorsing_ChargeNurse_id { get; set; }
        public string Endorsing_ChargeNurse { get; set; }
        public int? Receive_ChargeNurse_id { get; set; }
        public string Receive_ChargeNurse { get; set; }
        public string Entry_date { get; set; }
    }
}