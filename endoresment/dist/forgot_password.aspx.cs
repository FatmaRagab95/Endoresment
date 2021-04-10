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


public partial class _forgot_password: basePageSessionExpire
{	
	protected void Page_Load(object sender, EventArgs e)
	{
		
	}
	// update informationa
	[WebMethod]
    public static string ChangeInfo(newInfo info)

    {
		List<newInfo> newInfo = new List<newInfo>();

		string config = Convert.ToString(ConfigurationManager.ConnectionStrings["dbcon"]);
		SqlConnection con = new SqlConnection(config);
		con.Open();

			using (SqlCommand cmd = new SqlCommand("update adminusers set FullName=@FullName,Email=@Email,Mobile_1=@Mobile_1,Mobile_2=@Mobile_2 where Emp_ID= @Emp_id", con))

			{
				cmd.Parameters.Add("@Emp_ID", SqlDbType.Int).Value = info.Emp_ID;
				cmd.Parameters.Add("@FullName", SqlDbType.VarChar).Value = info.FullName;
				cmd.Parameters.Add("@Email", SqlDbType.VarChar).Value = info.Email;
				cmd.Parameters.Add("@Mobile_1", SqlDbType.VarChar).Value = info.Mobile_1;
				cmd.Parameters.Add("@Mobile_2", SqlDbType.VarChar).Value = info.Mobile_2;
				cmd.ExecuteNonQuery();

			}

            con.Close();
		
		   
			return JsonConvert.SerializeObject(newInfo);

    }
	
		public class newInfo
    {
		 public int Emp_ID;
		 public string FullName;
         public string Email;
         public string Mobile_1;
         public string Mobile_2;
    }
	
	// update newPass
	[WebMethod]

    public static string ChangePass(newPass pass)

    {
		List<newPass> newPass = new List<newPass>();
		
		byte up = 0;
		string config = Convert.ToString(ConfigurationManager.ConnectionStrings["dbcon"]);
		SqlConnection con = new SqlConnection(config);
		con.Open();
		String str = "select pass_word from adminusers where Emp_ID = @Emp_id";		

        using (SqlCommand cmd = new SqlCommand(str, con))
			{
				cmd.Parameters.Add("@Emp_ID", SqlDbType.Int).Value = pass.Emp_ID;
				SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    if (pass.oldPass == reader["pass_word"].ToString())
                    {
                        up = 1;
                    }
                }
                reader.Close();
                con.Close();

                if (up == 1)
                {
                    con.Open();
                    str = "update adminusers set pass_word=@pass_word where Emp_ID = @Emp_id";

                    using (SqlCommand cmd1 = new SqlCommand(str, con))

                    {

                        cmd1.Parameters.Add("@Emp_ID", SqlDbType.Int).Value = pass.Emp_ID;
                        cmd1.Parameters.Add("@pass_word", SqlDbType.VarChar).Value = pass.pass_word;
                        cmd1.ExecuteNonQuery();

                    }

                    con.Close();
                    return JsonConvert.SerializeObject(newPass);
                }
                else
                {
                    return "error";
                }
			}
    }

   
	public class newPass
    {
		 public int Emp_ID;
		 public string oldPass;
         public string pass_word;
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
}