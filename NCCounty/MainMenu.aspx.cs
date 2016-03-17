using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web.UI;
using NCCounty.Models;
using Newtonsoft.Json;

namespace NCCounty
{
	public partial class MainMenu : Page
	{

	    public DaAdmits Admin { get; set; }
	    public DaGre Gre { get; set; }

	    public MainMenu()
	    {
	        Admin = new DaAdmits();
            Gre = new DaGre();
	    }


	    protected void Page_Load(object sender, EventArgs e)
		{
			if (Page.IsPostBack == false)
			{
				panelMain.Visible = true;
				panelDisplay.Visible = false;
			}
		}
		protected void MedianGre(object sender, EventArgs e)
		{
			panelMain.Visible = false;
			panelDisplay.Visible = true;

            List<ToGre> list = Gre.RetrieveMedianGreCounty(ConfigurationManager.AppSettings["MedianGre"]);

		    //Call javascript function to create json string
            ClientScript.RegisterStartupScript(GetType(), "", "medianGreCounty('" + JsonConvert.SerializeObject(list) + "')", true);
		}

    //   protected void AverageGre(object sender, EventArgs e)
		//{
		//	panelMain.Visible = false;
		//	panelDisplay.Visible = true;

		//    List<ToGre> list = Gre.RetrieveAvgGreCounty(ConfigurationManager.AppSettings["AverageGre"]);

		//    //Call javascript function to create json string
		//	ClientScript.RegisterStartupScript(GetType(), "", "avgGreCounty('" + JsonConvert.SerializeObject(list) + "')", true);
		//}
        
	    protected void AdmittedStudents(object sender, EventArgs e)
		{
			panelMain.Visible = false;
			panelDisplay.Visible = true;

	        List<ToAdmits> list = Admin.GetAdmittedStudents(ConfigurationManager.AppSettings["AdmittedStudents"]);
			
            //Call javascript function to create json string
			ClientScript.RegisterStartupScript(GetType(), "", "admittedStudents('"+JsonConvert.SerializeObject(list)+"')", true);
		}

        protected void AdmittedStudentsChoropleth(object sender, EventArgs e)
        {
            panelMain.Visible = false;
            panelDisplay.Visible = true;
            
            List<ToAdmits> list = Admin.GetAdmittedStudents(ConfigurationManager.AppSettings["AdmittedStudents"]);
            
            //Call javascript function to create json string
            ClientScript.RegisterStartupScript(GetType(), "", "choroplethAdmittedStudents('" + JsonConvert.SerializeObject(list) + "')", true);
        }

        protected void ReturnMainMenu(object sender, EventArgs e)
		{
			panelMain.Visible = true;
			panelDisplay.Visible = false;
		}
	}
}