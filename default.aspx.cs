using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;

using System.Linq;

using System.Web;

using System.Web.UI;

using System.Web.UI.WebControls;

using System.Web.Services;

using Newtonsoft.Json;
using System.Configuration;

public partial class _default : basePageSessionExpire
{
	 //public static int tid= Request.QueryString["admin_id"]; '"+Session[admin_id].Tostring()+"'
	
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    // get pages
       [WebMethod(EnableSession = true)]
    public static string getPages()

    {
        string config = Convert.ToString(ConfigurationManager.ConnectionStrings["dbcon"]);

        List<pages> pages = new List<pages>();

        SqlConnection con = new SqlConnection(config);

        con.Open();

        using (SqlCommand cmd = new SqlCommand("SELECT Menu.* FROM User_Authority INNER JOIN Menu ON User_Authority.menu_id = Menu.id and User_Authority.admin_id=@admin_id" , con))

        {
cmd.Parameters.Add("@admin_id", SqlDbType.Int).Value = HttpContext.Current.Session["admin_id"];
             SqlDataReader idr = cmd.ExecuteReader();

            if (idr.HasRows)

            {

                pages = populatepagesLisst(idr, con);

            }

        }

        con.Close();

        return JsonConvert.SerializeObject(pages);

    }
    public static List<pages> populatepagesLisst(SqlDataReader idr, SqlConnection con)

    {

        List<pages> pagesI = new List<pages>();

        while (idr.Read())

        {

            pagesI.Add(new pages

            {

                id = Convert.ToInt32(idr["id"]),
				CategoryId = Convert.ToInt32(idr["CategoryId"]),
                Name = Convert.ToString(idr["Name"]),
                Path = Convert.ToString(idr["Path"])


            });

        }

        return pagesI;

    }

    public class pages

    {

        public int id { get; set; }
		public int CategoryId { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }


    }
	
		// get categories
	
	[WebMethod]
    public static string getcatsData()

    {
        string config = Convert.ToString(ConfigurationManager.ConnectionStrings["dbcon"]);

        List<cats> cats = new List<cats>();

        SqlConnection con = new SqlConnection(config);

        con.Open();

        using (SqlCommand cmd = new SqlCommand("Select * from Categories", con))

        {

            SqlDataReader idr = cmd.ExecuteReader();

            if (idr.HasRows)

            {

                cats = populatecatsLisst(idr, con);

            }

        }

        con.Close();

        return JsonConvert.SerializeObject(cats);

    }

    public static List<cats> populatecatsLisst(SqlDataReader idr, SqlConnection con)

    {

        List<cats> catsI = new List<cats>();

        while (idr.Read())

        {

            catsI.Add(new cats

            {

			    id     = idr["id"] != DBNull.Value ? Convert.ToInt32(idr["id"]) : 0,
                Name   = idr["Name"] != DBNull.Value ? Convert.ToString(idr["Name"]) : String.Empty,
				Icon   = idr["Icon"] != DBNull.Value ? Convert.ToString(idr["Icon"]) : String.Empty,

            });

        }

        return catsI;

    }

	public class cats
    {
        public int id { get; set; }
        public string Name { get; set; }
        public string Icon { get; set; }
    }
}