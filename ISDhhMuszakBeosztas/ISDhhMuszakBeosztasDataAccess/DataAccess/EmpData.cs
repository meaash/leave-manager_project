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
    public class EmpData
    {

        //Azért érdemes a using kifejezést használni, mert ez megvéd attól, hogy nyitva maradjon az adatbáziskapcsolat
        public List<EmployeeModel> GetEmpData()
        {
            using (IDbConnection connection = new SQLiteConnection(Helper.CnnVal("Default")))
            {
                //adatbazis lekérdezése
                // dynamic paramter list kell, kapunk egy blank listat ha üres
                var output = connection.Query<EmployeeModel>("SELECT * FROM EmpData", new DynamicParameters());
                return output.ToList();      //mert amugy ienumerablet ad vissza nem listat    
            }
        }


        public void EmpAdatokDataAccesSave(EmployeeModel empAdatokModel)
        {

            string myBday = empAdatokModel.SzuletesiDatum.ToString("yyyy.MM.dd");
            string myName = empAdatokModel.FullName;

            using (IDbConnection connection = new SQLiteConnection(Helper.CnnVal("Default")))
            {
                //adatbazis feltöltése
                string Upload = "INSERT INTO EmpData (FirstName, LastName, Muszak, SzuletesiDatum, Email, Tel, Munkakor, ExtraSzabad)  VALUES ('" + empAdatokModel.FirstName + "','"
                    + empAdatokModel.LastName + "','"
                    + empAdatokModel.Muszak + "','"
                    + myBday + "','"
                    + empAdatokModel.EMail + "','"
                    + empAdatokModel.Tel + "','"
                    + empAdatokModel.Munkakor + "','"
                    + empAdatokModel.ExtraSzabad + "')";
                connection.Execute(Upload);
            }



            using (IDbConnection connection = new SQLiteConnection(Helper.CnnVal("Default")))
            {
                //adatbazis feltöltése
                //itt hozzáadjuk a szabdságoláshoz is egyből, ezzel kiszűrjük 
                //hogy az azonos nevűeket ne találja meg
                string Upload = " INSERT INTO LeaveData (Leave_Id, Name) SELECT MAX(Id), '"
                    + myName + "' AS Name FROM EmpData";
                connection.Execute(Upload);
            }

        }

        public void EmpAdatokDataAccessUpdate(EmployeeModel empAdatokModel)
        {

            string myBday = empAdatokModel.SzuletesiDatum.ToString("yyyy.MM.dd");

            using (IDbConnection connection = new SQLiteConnection(Helper.CnnVal("Default")))
            {
                //adatbazis feltöltése módosítással
                string Update = "UPDATE EmpData SET FirstName = '" + empAdatokModel.FirstName + "', " +
                "LastName = '" + empAdatokModel.LastName + "', " +
                "Muszak = '" + empAdatokModel.Muszak + "', " +
                "SzuletesiDatum = '" + myBday + "', " +
                "Email = '" + empAdatokModel.EMail + "', " +
                "Tel = '" + empAdatokModel.Tel + "', " +
                "ExtraSzabad = '" + empAdatokModel.ExtraSzabad + "', " +
                "Munkakor = '" + empAdatokModel.Munkakor +
                "' WHERE Id='" + empAdatokModel.ID + "'";

                connection.Execute(Update);
            }

            using (IDbConnection connection = new SQLiteConnection(Helper.CnnVal("Default")))
            {
                //adatbazis feltöltése módosítással
                string Update = "UPDATE LeaveData SET Name = '" + empAdatokModel.FullName +
                "' WHERE Leave_Id='" + empAdatokModel.ID + "'";
                connection.Execute(Update);
            }
        }
        public void EmpAdatokDataAccesDelete(EmployeeModel empAdatokModel)
        {

            int myID = empAdatokModel.ID;

            using (IDbConnection connection = new SQLiteConnection(Helper.CnnVal("Default")))
            {
                string Delete = "DELETE FROM EmpData WHERE Id='" + myID + "'";
                connection.Execute(Delete);
            }

            //itt kitöröljük a tavolletböl is ha egy usert töröl
            //nyilván akkor már szabadságra se lesz szüksége
            using (IDbConnection connection = new SQLiteConnection(Helper.CnnVal("Default")))
            {
                string Delete = "DELETE FROM LeaveData WHERE Leave_ID='" + myID + "'";
                connection.Execute(Delete);
            }

        }

        public List<CNapokModel> GetCNapokData()
        {
            using (IDbConnection connection = new SQLiteConnection(Helper.CnnVal("Default")))
            {
                var output = connection.Query<CNapokModel>("SELECT * FROM CNapokData", new DynamicParameters());
                return output.ToList();
            }
        }

        public void UpdateCnapok(List<int> mycnapoklist)
        {
            int ID = 1;
            foreach (var item in mycnapoklist)
            {
                using (IDbConnection connection = new SQLiteConnection(Helper.CnnVal("Default")))
                {
                    string Update = "UPDATE CNapokData SET CNapokSzama='" + item +
                     "' WHERE ID='" + ID + "'";
                    connection.Execute(Update);
                }
                ID++;
            }
        }

    }


}

