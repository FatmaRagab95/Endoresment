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

public partial class _global : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    // get name
        [WebMethod]
    public static string getName(Name Emp_ID)
    {
        string config = Convert.ToString(ConfigurationManager.ConnectionStrings["dbcon"]);
        List<Name> Name = new List<Name>();

        SqlConnection con = new SqlConnection(config);

        con.Open();

        using (SqlCommand cmd1 = new SqlCommand("select FullName from adminusers where Emp_ID = @Emp_ID", con))
        {
            cmd1.Parameters.Add("@Emp_ID", SqlDbType.Int).Value = Emp_ID.Emp_ID;
            SqlDataReader idr = cmd1.ExecuteReader();

            
            if (idr.HasRows)
            {
                Name = populateNameLisst(idr, con);

            }
        }

        con.Close();  

        return JsonConvert.SerializeObject(Name);
    }

    public static List<Name>
    populateNameLisst(SqlDataReader idr, SqlConnection con)
    {
        List<Name> NameI = new List<Name>();

        while (idr.Read())
        {
            NameI
                .Add(new Name {
                    FullName = Convert.ToString(idr["FullName"]),

                });
        }

        return NameI;
    }
        public class Name
    {
        public int Emp_ID { get; set; }
        public string FullName { get; set; }
    }
}