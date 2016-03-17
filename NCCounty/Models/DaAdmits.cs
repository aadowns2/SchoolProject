using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using Newtonsoft.Json;

namespace NCCounty.Models
{
	public class DaAdmits
    {
		public List<ToAdmits> GetAdmittedStudents(string filename)
		{
            List<ToAdmits> list = new List<ToAdmits>();
		    try
		    {
		        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["SchoolDatabase"].ConnectionString))
		        {
		            con.Open();
                    using (SqlCommand cmd = new SqlCommand("usp_R_AdmitStatus", con))
		            {
		                cmd.CommandType = CommandType.StoredProcedure;
                        using (SqlDataReader reader = cmd.ExecuteReaderExtended())
		                    while (reader.Read())
		                        list.Add(new ToAdmits
		                        {
		                            CountyName = reader["ResidenceCounty"].ToNullSafeString(),
		                            Admitted = reader["Admitted"].ChangeType<int>(),
		                            NotAdmitted = reader["Not Admitted"].ChangeType<int>(),
		                            Waitlist = reader["Waiting List"].ChangeType<int>(),
		                            NoAction = reader["No Action"].ChangeType<int>(),
		                            Rejected = reader["Rejected"].ChangeType<int>()
		                        });
		            }
		        }
		        using (FileStream fs = new FileStream(filename, FileMode.OpenOrCreate))
                using (StreamWriter sw = new StreamWriter(fs))
                using (JsonWriter jw = new JsonTextWriter(sw))
                {
                    jw.Formatting = Formatting.Indented;
                    JsonSerializer serializer = new JsonSerializer();
                    serializer.Serialize(jw, new List<ToAdmits>());
                }
            }

		    catch
		        (Exception ex)
		    {
		        string fileName = ConfigurationManager.AppSettings["SqlError"];
		        using (FileStream fs = new FileStream(fileName, FileMode.OpenOrCreate))
		        using (StreamWriter sw = new StreamWriter(fs))
		        {
		            sw.WriteLine(DateTime.Now);
		            sw.WriteLine("DM::Method {0}", ex.TargetSite);
		            sw.WriteLine("DM::Source {0}", ex.Source);
		            sw.WriteLine("DM::Logon {0}", ex.Message);
		        }
		    }
		    return list;
		}
	}
}