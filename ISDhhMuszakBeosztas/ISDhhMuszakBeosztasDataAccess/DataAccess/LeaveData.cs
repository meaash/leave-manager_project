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
    public class LeaveData
    {
        public List<LeaveModel> GetLeaveData()
        {
            using (IDbConnection connection = new SQLiteConnection(Helper.CnnVal("Default")))
            {

                // dynamic paramter list kell, kapunk egy blank listat ha amugy üres
                var output = connection.Query<LeaveModel>("SELECT * FROM LeaveData", new DynamicParameters());
                return output.ToList();      //mert amugy ienumerablet ad vissza nem listat    
            }
        }


        // mindenképpen létezik, már adatbázis sor az alkalmazottnak úgyhogy csak módosítgatni kell
        public void UpdateLeaveDataSzabadnap(LeaveModel tavolletmodel)
        {

            using (IDbConnection connection = new SQLiteConnection(Helper.CnnVal("Default")))
            {
                //adatbazis feltöltése
                string Update = "UPDATE LeaveData SET Szabadnap = '" + tavolletmodel.Szabadnap +
                "' WHERE Leave_Id='" + tavolletmodel.Leave_ID + "'";
                connection.Execute(Update);
            }

        }

        public void UpdateLeaveDataCnap(LeaveModel tavolletmodel)
        {

            using (IDbConnection connection = new SQLiteConnection(Helper.CnnVal("Default")))
            {
                //adatbazis feltöltése
                string Update = "UPDATE LeaveData SET Cnap = '" + tavolletmodel.Cnap +
                "' WHERE Leave_Id='" + tavolletmodel.Leave_ID + "'";

                connection.Execute(Update);
            }

        }
        public void UpdateLeaveDataBetegszabadsag(LeaveModel tavolletmodel)
        {

            using (IDbConnection connection = new SQLiteConnection(Helper.CnnVal("Default")))
            {
                //adatbazis feltöltése
                string Update = "UPDATE LeaveData SET Betegszabadsag = '" + tavolletmodel.BetegSzabadsag +
                "' WHERE Leave_Id='" + tavolletmodel.Leave_ID + "'";

                connection.Execute(Update);
            }

        }

        public void UpdateLeaveDataIgazolatlan(LeaveModel tavolletmodel)
        {

            using (IDbConnection connection = new SQLiteConnection(Helper.CnnVal("Default")))
            {
                //adatbazis feltöltése
                string Update = "UPDATE LeaveData SET Igazolatlan = '" + tavolletmodel.Igazolatlan +
                "' WHERE Leave_Id='" + tavolletmodel.Leave_ID + "'";

                connection.Execute(Update);
            }

        }
    }
}
