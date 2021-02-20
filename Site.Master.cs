using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Navbar_Demo
{
    public partial class SiteMaster : MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            DataTable dt = new DataTable();
            DataTableCollection dtc = GetTables("exec app_sp_get_user_data_demo 'edde029d-efe8-47ac-9752-140cc3b4f759'");


            dt = dtc[0];
            Session["UserMenu"] = Server.HtmlDecode(dt.Rows[0]["User_Menu"].ToString());

            //Session["UserMenu"] = "Hello world!";
        }

        public DataTableCollection GetTables(string SQL)
        {
            return GetDataSet(SQL).Tables;
        }

        public DataSet GetDataSet(string SQL)
        {

            string ConnectionString = "Data Source=apps.promasys.com, 4434;Initial Catalog=foundation;Persist Security Info=True;User ID=foundation_user;Password=foundation789!**";
            SqlConnection conn = new SqlConnection(ConnectionString);
            SqlDataAdapter cmd = new SqlDataAdapter(SQL, conn);

            DataSet ds = new DataSet();

            ds.EnforceConstraints = false;

            cmd.FillSchema(ds, SchemaType.Mapped);
            cmd.MissingSchemaAction = MissingSchemaAction.AddWithKey;
            cmd.Fill(ds);

            cmd = null;
            conn.Close();

            return ds;
        }
    }

}