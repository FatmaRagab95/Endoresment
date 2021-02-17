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

public partial class _editShiftDetials : System.Web.UI.Page
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

        using (
            SqlCommand cmd =
                new SqlCommand("SELECT * from Endorsement_UnitsDashboard where id = @id",
                    con)
        )
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
                    id =
                        idr["id"] != DBNull.Value
                            ? Convert.ToInt32(idr["id"])
                            : 0,
                    Unit_name = Convert.ToString(idr["Unit_name"]),
                    Shift = Convert.ToString(idr["Shift"]),
                    Shift_date = Convert.ToString(idr["Shift_date"]),
                    Total_Census =
                        idr["Total_Census"] != DBNull.Value
                            ? Convert.ToInt32(idr["Total_Census"])
                            : 0,
                    Received =
                        idr["Received"] != DBNull.Value
                            ? Convert.ToInt32(idr["Received"])
                            : 0,
                    Admission =
                        idr["Admission"] != DBNull.Value
                            ? Convert.ToInt32(idr["Admission"])
                            : 0,
                    Transfer_In =
                        idr["Transfer_In"] != DBNull.Value
                            ? Convert.ToInt32(idr["Transfer_In"])
                            : 0,
                    Transfer_Out =
                        idr["Transfer_Out"] != DBNull.Value
                            ? Convert.ToInt32(idr["Transfer_Out"])
                            : 0,
                    Endorsing_ChargeNurse_id =
                        idr["Endorsing_ChargeNurse_id"] != DBNull.Value
                            ? Convert.ToInt32(idr["Endorsing_ChargeNurse_id"])
                            : 0,
                    Endorsing_ChargeNurse =
                        Convert.ToString(idr["Endorsing_ChargeNurse"]),
                    Receive_ChargeNurse_id =
                        idr["Receive_ChargeNurse_id"] != DBNull.Value
                            ? Convert.ToInt32(idr["Receive_ChargeNurse_id"])
                            : 0,
                    Receive_ChargeNurse =
                        Convert.ToString(idr["Receive_ChargeNurse"]),
                    Entry_date = Convert.ToString(idr["Entry_date"])
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

    // update shift
    [WebMethod]
    public static string updatedShift(ShiftDetails detail)
    {
        string config =
            Convert.ToString(ConfigurationManager.ConnectionStrings["dbcon"]);

        List<ShiftDetails> ShiftDetails = new List<ShiftDetails>();

        SqlConnection con = new SqlConnection(config);

        con.Open();

        using (
            SqlCommand cmd =
                new SqlCommand("update Endorsement_UnitsDashboard set Unit_name=@Unit_name, Shift=@Shift, Total_Census=@Total_Census, Received=@Received, Shift_date=@Shift_date, Transfer_In=@Transfer_In, Transfer_Out=@Transfer_Out, Admission=@Admission, Endorsing_ChargeNurse_id=@Endorsing_ChargeNurse_id, Receive_ChargeNurse_id=@Receive_ChargeNurse_id where ID = @ID;",
                    con)
        )
        {
            cmd.Parameters.AddWithValue("@id", detail.id);
            cmd.Parameters.AddWithValue("@Unit_name", detail.Unit_name);
            cmd.Parameters.AddWithValue("@Shift", detail.Shift);
            cmd.Parameters.AddWithValue("@Total_Census", detail.Total_Census);
            cmd.Parameters.AddWithValue("@Received", detail.Received);
            cmd.Parameters.AddWithValue("@Shift_date", detail.Shift_date);
            cmd.Parameters.AddWithValue("@Transfer_In", detail.Transfer_In);
            cmd.Parameters.AddWithValue("@Transfer_Out", detail.Transfer_Out);
            cmd.Parameters.AddWithValue("@Admission", detail.Admission);

            cmd
                .Parameters
                .AddWithValue("@Endorsing_ChargeNurse_id",
                detail.Endorsing_ChargeNurse_id);
            cmd
                .Parameters
                .AddWithValue("@Receive_ChargeNurse_id",
                detail.Receive_ChargeNurse_id);

            cmd.ExecuteNonQuery();
        }

        con.Close();

        return JsonConvert.SerializeObject(ShiftDetails);
    }

    public static List<ShiftDetails>
    populateShiftDetailsLisst(SqlDataReader idr, SqlConnection con)
    {
        List<ShiftDetails> ShiftDetailsI = new List<ShiftDetails>();

        while (idr.Read())
        {
            ShiftDetailsI
                .Add(new ShiftDetails {
                    id = Convert.ToInt32(idr["id"]),
                    Unit_name = Convert.ToString(idr["Unit_name"]),
                    Shift = Convert.ToString(idr["Shift"]),
                    Total_Census = Convert.ToInt32(idr["Total_Census"]),
                    Received = Convert.ToInt32(idr["Received"]),
                    Shift_date = Convert.ToString(idr["Shift_date"]),
                    Transfer_In = Convert.ToInt32(idr["Transfer_In"]),
                    Transfer_Out = Convert.ToInt32(idr["Transfer_Out"]),
                    Admission = Convert.ToInt32(idr["Admission"]),
                    Endorsing_ChargeNurse_id =
                        Convert.ToInt32(idr["Endorsing_ChargeNurse_id"]),
                    Receive_ChargeNurse_id =
                        Convert.ToInt32(idr["Receive_ChargeNurse_id"])
                });
        }
        return ShiftDetailsI;
    }

    public class ShiftDetails
    {
        public int? id { get; set; }

        public string Unit_name { get; set; }

        public string Shift { get; set; }

        public int? Total_Census { get; set; }

        public int? Received { get; set; }

        public string Shift_date { get; set; }

        public int? Transfer_In { get; set; }

        public int? Transfer_Out { get; set; }

        public int? Admission { get; set; }

        public int? Endorsing_ChargeNurse_id { get; set; }

        public int? Receive_ChargeNurse_id { get; set; }
    }

    // get Nurses
    [WebMethod]
    public static string getNursesData()
    {
        string config =
            Convert.ToString(ConfigurationManager.ConnectionStrings["dbcon"]);
        List<Nurses> Nurses = new List<Nurses>();

        SqlConnection con = new SqlConnection(config);

        con.Open();

        using (
            SqlCommand cmd =
                new SqlCommand("select * from adminusers where Role_id = 17",
                    con)
        )
        {
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
                    Emp_ID =
                        idr["Emp_ID"] != DBNull.Value
                            ? Convert.ToInt32(idr["Emp_ID"])
                            : 0,
                    FullName = Convert.ToString(idr["FullName"])
                });
        }

        return NursesI;
    }

    public class Nurses
    {
        public int? Emp_ID { get; set; }

        public string FullName { get; set; }
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
}
