using Klinika123;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication1
{
    public class LjekarDB
    {

        public LjekarDB()
        {

        }

        string host = "80.65.65.66",
               serviceName = "etflab.db.lab.etf.unsa.ba",

        //*****POSTAVITE VAS USER I PASS******/
              userID = "AM16947",
               password = "prlNKflF";

        public OracleConnection GetConnection()
        {
            try
            {
                OracleConnection dbConnection = new OracleConnection();
                dbConnection.ConnectionString = string.Format(
                    @"Data Source=
                        (DESCRIPTION =
                                (ADDRESS = (PROTOCOL = TCP)(HOST = {0})(PORT = 1521))
                                (CONNECT_DATA =
                                    (SERVER = DEDICATED)
                                    (SERVICE_NAME = {1})
                                )
                        )
                    ;User Id= {2}; Password= {3}; Persist Security Info=True;",
                    host, serviceName, userID, password);

                return dbConnection;
            }
            catch (Exception ex)
            {
                //Log exception...
                return null;
            }
        }

        public bool CreateLjekarTable()
        {
            try
            {
                //kreiramo konekciju, s obzirom da koristimo using(), pa ne moramo se brinuti za Closing() konekcije
                //jer se to automatski desava kada se izvrsava Disposing(), konekcije
                //using samo mozemo koristiti sa objektima koji su disposable
                //IDisposable u DBConnenctionu
                using (OracleConnection oc = GetConnection())
                {
                    //kreiramo orakle komandu
                    using (OracleCommand cmd = oc.CreateCommand())
                    {
                        //otvorimo konekciju!!!!!
                        oc.Open();

                        cmd.CommandText = "CREATE TABLE Ljekar (ID int PRIMARY KEY, Ime varchar(50), Prezime varchar(50), Telefon varchar(20), Specijalizacija  varchar(50), Titula  varchar(20), DatumZaposlenja DATE, Spol varchar(2))";
                        int result = cmd.ExecuteNonQuery();

                        //ako zelimo auto increment da imamo na ID...
                        //Ali onda problem sa delete jer ne znamo koji employee ima koji ID....pa moramo na to paziti
                        //cmd.CommandText = "CREATE SEQUENCE SequenceTest_Sequence START WITH 1 INCREMENT BY 1";
                        //cmd.ExecuteNonQuery();

                        //cmd.CommandText = "CREATE OR REPLACE TRIGGER trigger_name BEFORE INSERT ON Employee FOR EACH ROW BEGIN :new.ID := SequenceTest_Sequence.nextval;END;";
                        //cmd.ExecuteNonQuery();

                        return true;
                    }
                }

            }
            catch (Exception ex)
            {
                //log exception
                return false;
            }
        }

        public bool DropLjekarTable()
        {
            try
            {
                using (OracleConnection oc = GetConnection())
                {
                    using (OracleCommand cmd = oc.CreateCommand())
                    {
                        oc.Open();
                        cmd.CommandText = "DROP TABLE Ljekar";
                        cmd.ExecuteNonQuery();

                        //ako smo koristili sequence
                        //cmd.CommandText = "DROP SEQUENCE SequenceTest_Sequence";
                        //cmd.ExecuteNonQuery();

                        return true;
                    }
                }
            }
            catch (OracleException oEx)
            {
                //ako ne postoji tabela ili seq oracle ce baciti OracleException, er brisemo tabelu koja ne postoji
                //sa poljem Number postavljeno na 942, sto u sustini i nije exception bar ne za nasu app
                //sto se nas tice obrisano je ako ne postoji ....
                if (oEx.Number == 942)
                    return true;
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool InsertLjekar(Ljekar employee)
        {
            try
            {
                using (OracleConnection oc = GetConnection())

                using (OracleCommand cmd = oc.CreateCommand())
                {

                    oc.Open();

                    //Imamo auto inkrement na employee tabeli
                    string sqlInsert = "insert into Ljekar (ID, Ime, Prezime, Telefon, Specijalizacija, Titula, DatumZaposlenja, Spol)";
                    sqlInsert += "values (:ID, :Ime, :Prezime, :Telefon, :Specijalizacija, :Titula, :DatumZaposlenja, :Spol)";
                    cmd.CommandText = sqlInsert;

                    //parametar se moze ovako praviti
                    //redoslijed parametara se mora podudariti sa redoslijedom u upitu
                    OracleParameter id = new OracleParameter();
                    id.Value = employee.ID;
                    id.ParameterName = "ID";
                    cmd.Parameters.Add(id);


                    cmd.Parameters.Add(new OracleParameter("Ime", employee.Ime));
                    cmd.Parameters.Add(new OracleParameter("Prezime", employee.Prezime));
                    cmd.Parameters.Add(new OracleParameter("Telefon", employee.Telefon));
                    cmd.Parameters.Add(new OracleParameter("Specijalizacija", employee.Specijalizacija));
                    cmd.Parameters.Add(new OracleParameter("Titula", employee.Titula));
                    cmd.Parameters.Add(new OracleParameter("DatumZaposlenja", employee.DatumZaposlenja));
                    cmd.Parameters.Add(new OracleParameter("Spol", employee.Spol));

                    cmd.ExecuteNonQuery();

                }

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool DeleteLjekar(Ljekar employee)
        {
            try
            {
                using (OracleConnection oc = GetConnection())
                using (OracleCommand cmd = oc.CreateCommand())
                {
                    oc.Open();

                    string sqlDelete = "Delete from Ljekar Where ID = :ID";
                    cmd.CommandText = sqlDelete;

                    cmd.Parameters.Add(new OracleParameter("ID", employee.ID));
                    cmd.ExecuteNonQuery();
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool UpdateLjekar(Ljekar employee)
        {
            try
            {
                using (OracleConnection oc = GetConnection())
                using (OracleCommand cmd = oc.CreateCommand())
                {
                    oc.Open();

                    string sqlUpdate = "Update Ljekar set Ime=:Ime, Prezime=:Prezime, Telefon=:Telefon, Specijalizacija=:Specijalizacija, Titula=:Titula, DatumZaposlenja=:DatumZaposlenja, Spol=:Spol"
                                     + " where ID=:ID ";
                    cmd.CommandText = sqlUpdate;

                    //redoslijed se mora podudariti sa redoslijedom u upitu
                    cmd.Parameters.Add(new OracleParameter("Ime", employee.Ime));
                    cmd.Parameters.Add(new OracleParameter("Prezime", employee.Prezime));
                    cmd.Parameters.Add(new OracleParameter("Telefon", employee.Telefon));
                    cmd.Parameters.Add(new OracleParameter("Specijalizacija", employee.Specijalizacija));
                    cmd.Parameters.Add(new OracleParameter("Titula", employee.Titula));
                    cmd.Parameters.Add(new OracleParameter("DatumZaposlenja", employee.DatumZaposlenja));
                    cmd.Parameters.Add(new OracleParameter("Spol", employee.Spol));

                    cmd.ExecuteNonQuery();
                }

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public BindingList<Ljekar> ReadAllLjekar()
        {
            BindingList<Ljekar> ljekari = new BindingList<Ljekar>();
            try
            {
                using (OracleConnection oc = GetConnection())
                using (OracleCommand cmd = oc.CreateCommand())
                {
                    oc.Open();

                    string sqlSelect = "SELECT * FROM Ljekar";
                    cmd.CommandText = sqlSelect;

                    OracleDataReader reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            ljekari.Add(new Ljekar()
                            {
                                ID = reader["ID"].ToString(),
                                Ime = reader["Ime"].ToString(),
                                Prezime = reader["Prezime"].ToString(),
                                Telefon = reader["Telefon"].ToString(),
                                Specijalizacija = reader["Specijalizacija"].ToString(),
                                Titula = reader["Titula"].ToString(),
                                DatumZaposlenja = Convert.ToDateTime(reader["DatumZaposlenja"]),
                                Spol = reader["Spol"].ToString()
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //log
            }
            return ljekari;
        }
    }
}
