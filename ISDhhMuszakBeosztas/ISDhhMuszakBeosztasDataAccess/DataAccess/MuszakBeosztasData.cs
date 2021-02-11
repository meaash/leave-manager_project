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
    public class MuszakBeosztasData
    {
        public List<MuszakBeosztasModel> GetMuszakBeosztasData()
        {
            using (IDbConnection connection = new SQLiteConnection(Helper.CnnVal("Default")))
            {
                //adatbazis lekérdezése
                // dynamic paramter list kell, kapunk egy blank listat ha amugy üres
                var output = connection.Query<MuszakBeosztasModel>("SELECT * FROM MuszakBeosztasData", new DynamicParameters());
                return output.ToList();      //mert amugy ienumerablet ad vissza nem listat    
            }
        }
        public void MuszakBeosztasDataAccesSave(MuszakBeosztasModel muszakBeosztasmodel)
        {
            using (IDbConnection connection = new SQLiteConnection(Helper.CnnVal("Default")))
            {
                //adatbazis feltöltése
                string Upload = "INSERT INTO MuszakBeosztasData (Ev, Honap, Nap, HetNap, Delelott, Delutan, Ejszaka, Szabad)  VALUES ('" + muszakBeosztasmodel.Ev + "','"
                 + muszakBeosztasmodel.Honap + "','"
                 + muszakBeosztasmodel.Nap + "','"
                 + muszakBeosztasmodel.HetNap + "','"
                 + muszakBeosztasmodel.Delelott + "','"
                 + muszakBeosztasmodel.Delutan + "','"
                 + muszakBeosztasmodel.Ejszaka + "','"
                 + muszakBeosztasmodel.Szabad + "')";
                connection.Execute(Upload);
            }
        }

        public void MuszakBeosztasDataAccesDelete()
        {
            using (IDbConnection connection = new SQLiteConnection(Helper.CnnVal("Default")))
            {
                string Delete = "DELETE FROM MuszakBeosztasData";
                connection.Execute(Delete);
            }
        }
    }
}
