using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using Newtonsoft.Json;

namespace NCCounty.Models
{
	public class DaGre
	{
		public List<ToGre> RetrieveMedianGreCounty(string filename)
		{
			List<ToGre> list = new List<ToGre>();
            try
		    {
		        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["SchoolDatabase"].ConnectionString))
		        {
		            con.Open();
		            using (SqlCommand cmd = new SqlCommand("usp_R_MedGreByCounty", con))
		            {
		                cmd.CommandType = CommandType.StoredProcedure;
                        using (SqlDataReader reader = cmd.ExecuteReaderExtended())
		                    while (reader.Read())
		                        list.Add(new ToGre
		                        {
		                            Name = reader["ResidenceCounty"].ToNullSafeString(),
		                            GreVerbal = (reader["GreVerbal"]).ChangeType<decimal>(),
		                            GreQuantitative = (reader["GreQuantitative"]).ChangeType<decimal>(),
		                            GreAnalyticalWriting = (reader["GreAnalyticalWriting"]).ChangeType<decimal>()
		                        });
		            }}

		        using (FileStream fs = new FileStream(filename, FileMode.OpenOrCreate))
		        using (StreamWriter sw = new StreamWriter(fs))
		        using (JsonWriter jw = new JsonTextWriter(sw))
		        {
		            jw.Formatting = Formatting.Indented;
		            JsonSerializer serializer = new JsonSerializer();
		            serializer.Serialize(jw, list);
		        }
		    }
		    
		    catch (Exception ex)
		    {
		        string log = ex.ToLogString("Database Error");
		        string fileName = ConfigurationManager.AppSettings["SqlError"];
		        using (FileStream fs = new FileStream(fileName, FileMode.OpenOrCreate))
		        using (StreamWriter sw = new StreamWriter(fs))
		            sw.WriteLine(log);
		    }

            foreach (ToGre to in list)
                {
                    Debug.WriteLine(to.Name + ":" + to.GreAnalyticalWriting);
                }
            return list;
		}

		//public List<ToGre> RetrieveAvgGreCounty(string filename)
		//{
		//	List<ToGre> list = new List<ToGre>();
  //          try
		//	{
		//		using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["SchoolDatabase"].ConnectionString))
		//		{
		//			con.Open();
		//			using (SqlCommand cmd = new SqlCommand("usp_R_AvgGreByCounty", con))
		//			{
		//				cmd.CommandType = CommandType.StoredProcedure;
		//				using (SqlDataReader reader = cmd.ExecuteReaderExtended())
		//			        while (reader.Read())
		//			            list.Add(new ToGre
		//			            {
		//			                name = reader["ResidenceCounty"].ToNullSafeString(),
		//			                GreVerbal = (reader["GREVerbal"]).ChangeType<decimal>(),
		//			                GreQuantitative = (reader["GREQuantitative"]).ChangeType<decimal>(),
		//			                GreAnalyticalWriting = (reader["GREAnalyticalWriting"]).ChangeType<decimal>()
		//			            });
		//			}}
		//	    using (FileStream fs = new FileStream(filename, FileMode.OpenOrCreate))
		//	    using (StreamWriter sw = new StreamWriter(fs))
		//	    using (JsonWriter jw = new JsonTextWriter(sw))
		//	    {
		//	        jw.Formatting = Formatting.Indented;
		//	        JsonSerializer serializer = new JsonSerializer();
		//	        serializer.Serialize(jw, list);
		//	    }
		//	}
		//	catch (Exception ex)
		//	{
		//		string log = ex.ToLogString("Database Error");
		//		string fileName = ConfigurationManager.AppSettings["SqlError"];
		//	    using (FileStream fs = new FileStream(fileName, FileMode.OpenOrCreate))
		//	    using (StreamWriter sw = new StreamWriter(fs))
		//	        sw.WriteLine(log);
		//	}
		//	return list;
		//}
	}
}