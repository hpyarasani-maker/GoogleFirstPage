using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;

namespace GoogleFirstPage
{
    public partial class login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void btn1_Click(object sender, EventArgs e)
        {
            if (txtname.Text == "pisoftware" && txtpswd.Text == "Pi*Soft5301")
            {
                Response.Redirect("~/UnitTestReport/CoverageReport/index.html");
            }
            else
            {
                lbl1.Text = "User Name or Password is Invalid";
            }

            //if (FormsAuthentication.Authenticate(txtname.Text, txtpswd.Text))
            //{
            //    FormsAuthentication.RedirectFromLoginPage(txtname.Text, false);
            //}
            //else
            //{
            //    lbl1.Text = "Invalid user";
            //}

            //if (FormsAuthentication.Authenticate(txtname.Text, txtpswd.Text))
            //{
            //    FormsAuthentication.RedirectFromLoginPage(txtname.Text, false);
            //    Response.Redirect("~/UnitTestReport/CoverageReport/index.html");
            //}
            //else
            //{
            //    lbl1.Text = "Invalid UserName and/or password";
            //}

        }
    }
}