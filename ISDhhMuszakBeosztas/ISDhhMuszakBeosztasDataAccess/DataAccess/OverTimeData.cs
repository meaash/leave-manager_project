using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using ISDhhMuszakBeosztasDataAccess.DBHelper;
using ISDhhMuszakBeosztasDataAccess.Model;
using Dapper;
using System.Data.SQLite;

namespace ISDhhMuszakBeosztasDataAccess
{
    public class OverTimeData
    {
        public List<OverTimeModel> GetOverTimeData()
        {
            using (IDbConnection connection = new SQLiteConnection(Helper.CnnVal("Default")))
            {

                var output = connection.Query<OverTimeModel>("SELECT * FROM OverTimeData", new DynamicParameters());
                return output.ToList();

            }
        }

        public void TuloraAdatokDataAccesSave(OverTimeModel tuloramodel)
        {


            using (IDbConnection connection = new SQLiteConnection(Helper.CnnVal("Default")))
            {
                //adatbazis feltöltése
                string Upload = "INSERT INTO OverTimeData(Name, sajatMuszak, tuloraMuszak, Munkakor, Datum)  VALUES ('" + tuloramodel.Name + "','"
                    + tuloramodel.sajatMuszak + "','"
                    + tuloramodel.tuloraMuszak + "','"
                    + tuloramodel.Munkakor + "','"
                    + tuloramodel.Datum + "')";
                connection.Execute(Upload);
            }
        }
        public void TuloraAdatokDataAccesDelete(OverTimeModel tuloramodel)
        {

            using (IDbConnection connection = new SQLiteConnection(Helper.CnnVal("Default")))
            {
                string Delete = "DELETE FROM OverTimeData WHERE Id='" + tuloramodel.ID + "'";
                connection.Execute(Delete);
            }
        }
    }
}
