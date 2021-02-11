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
    public class HolidayData
    {
        public List<UnnepnapModel> GetHolidayData()
        {
            using (IDbConnection connection = new SQLiteConnection(Helper.CnnVal("Default")))
            {
                var output = connection.Query<UnnepnapModel>("SELECT * FROM UnnepnapData", new DynamicParameters());
                return output.ToList();
            }
        }

        public void HolidaySave(DateTime Holiday)
        {

            using (IDbConnection connection = new SQLiteConnection(Helper.CnnVal("Default")))
            {
                //adatbazis feltöltése
                string Upload = "INSERT INTO UnnepnapData (UnnepNap)  VALUES ('" + Holiday + "')";
                connection.Execute(Upload);
            }


        }

        public void HolidayDelete(UnnepnapModel holidaymodel)
        {


            using (IDbConnection connection = new SQLiteConnection(Helper.CnnVal("Default")))
            {
                string Delete = "DELETE FROM UnnepnapData WHERE ID='" + holidaymodel.ID + "'";
                connection.Execute(Delete);
            }

        }

        public List<MunkanapModel> GetMunkaNapData()
        {
            using (IDbConnection connection = new SQLiteConnection(Helper.CnnVal("Default")))
            {
                var output = connection.Query<MunkanapModel>("SELECT * FROM MunkaNapData", new DynamicParameters());
                return output.ToList();
            }
        }
        public void MunkanapSave(DateTime Munkanap)
        {

            using (IDbConnection connection = new SQLiteConnection(Helper.CnnVal("Default")))
            {
                //adatbazis feltöltése
                string Upload = "INSERT INTO MunkaNapData (MunkaNap)  VALUES ('" + Munkanap + "')";
                connection.Execute(Upload);
            }


        }

        public void MunkanapDelete(MunkanapModel munkanapmodel)
        {


            using (IDbConnection connection = new SQLiteConnection(Helper.CnnVal("Default")))
            {
                string Delete = "DELETE FROM MunkaNapData WHERE ID='" + munkanapmodel.ID + "'";
                connection.Execute(Delete);
            }

        }

        public List<PihenonapModel> GetPihenoNapData()
        {
            using (IDbConnection connection = new SQLiteConnection(Helper.CnnVal("Default")))
            {
                var output = connection.Query<PihenonapModel>("SELECT * FROM PihenoNapData", new DynamicParameters());
                return output.ToList();
            }
        }
        public void PihenonapSave(DateTime Pihenonap)
        {

            using (IDbConnection connection = new SQLiteConnection(Helper.CnnVal("Default")))
            {
                //adatbazis feltöltése
                string Upload = "INSERT INTO PihenoNapData (PihenoNap)  VALUES ('" + Pihenonap + "')";
                connection.Execute(Upload);
            }


        }
        public void PihenonapDelete(PihenonapModel pihenonap)
        {


            using (IDbConnection connection = new SQLiteConnection(Helper.CnnVal("Default")))
            {
                string Delete = "DELETE FROM PihenoNapData WHERE ID='" + pihenonap.ID + "'";
                connection.Execute(Delete);
            }

        }


    }
}
