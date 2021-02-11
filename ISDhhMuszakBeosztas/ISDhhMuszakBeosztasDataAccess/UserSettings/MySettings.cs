using System.Data;
using ISDhhMuszakBeosztasDataAccess.DBHelper;
using Dapper;
using System.Data.SQLite;


namespace ISDhhMuszakBeosztasDataAccess.UserSettings
{
    public class MySettings
    {
		private string myMuszak;

		public string MyMuszak
		{
			get
			{
				myMuszak = GetmyMuszak();
				return myMuszak;
			}
			set
			{
				myMuszak = value;				
			}
		}

		private string GetmyMuszak()
		{
			using (IDbConnection connection = new SQLiteConnection(Helper.CnnVal("Default")))
			{
				var output = connection.QueryFirstOrDefault<string>("SELECT MyMuszak FROM UserSettingsData WHERE ID = 1", new DynamicParameters());
				return output;
			}
		}

		public void UpdatemyMuszak(string myMuszak)
		{
			using (IDbConnection connection = new SQLiteConnection(Helper.CnnVal("Default")))
			{
				string Update = "UPDATE UserSettingsData SET myMuszak= '" + myMuszak +
				 "' WHERE ID='1'";
				connection.Execute(Update);
			}
		}
	}
}
