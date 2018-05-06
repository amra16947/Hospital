using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Klinika123
{
    public class IzuzetciDB
    {

        public IzuzetciDB()
        {

        }

        string host = "80.65.65.66",
               serviceName = "etflab.db.lab.etf.unsa.ba",

        
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

        public bool CreateIzuzetakTable()
        {
            try
            {
                
                using (OracleConnection oc = GetConnection())
                {
                    using (OracleCommand cmd = oc.CreateCommand())
                    {
                      
                        oc.Open();

                        cmd.CommandText = "CREATE TABLE Izuzetak (SIFRA int PRIMARY KEY, Text varchar(300), Datum DATE)";
                        int result = cmd.ExecuteNonQuery();


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

        
        public bool InsertIzuzetak(Izuzetak employee)
        {
            try
            {
                using (OracleConnection oc = GetConnection())

                using (OracleCommand cmd = oc.CreateCommand())
                {

                    oc.Open();

                    
                    string sqlInsert = "insert into Izuzetak (Sifra, Text, Datum)";
                    sqlInsert += "values (:Sifra, :Text, :Datum)";
                    cmd.CommandText = sqlInsert;

                    OracleParameter id = new OracleParameter();
                    id.Value = employee.Sifra;
                    id.ParameterName = "Sifra";
                    cmd.Parameters.Add(id);


                    cmd.Parameters.Add(new OracleParameter("Text", employee.Text));
                    cmd.Parameters.Add(new OracleParameter("Datum", employee.Datum));
                   

                    cmd.ExecuteNonQuery();

                }

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public BindingList<Izuzetak> ReadAllIzuzetak()
        {
            BindingList<Izuzetak> greske = new BindingList<Izuzetak>();
            try
            {
                using (OracleConnection oc = GetConnection())
                using (OracleCommand cmd = oc.CreateCommand())
                {
                    oc.Open();

                    string sqlSelect = "SELECT * FROM Izuzetak";
                    cmd.CommandText = sqlSelect;

                    OracleDataReader reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            greske.Add(new Izuzetak()
                            {
                                Sifra =Convert.ToInt32(reader["Sifra"]),
                                Text = reader["Text"].ToString(),
                                Datum= Convert.ToDateTime(reader["Datum"])
                                
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //log
            }
            return greske;
        }

        public int PrebrojIzuzetke()
        {
            try
            {
                int broj;
                using (OracleConnection oc = GetConnection())

                using (OracleCommand cmd = oc.CreateCommand())
                {

                    oc.Open();

                    //Imamo auto inkrement na employee tabeli
                    string sqlSelect = "select count(*) from Izuzetak";

                    cmd.CommandText = sqlSelect;

                    OracleDataReader reader = cmd.ExecuteReader();

                     broj = Convert.ToInt32(reader.Read());

                }

                return broj;
            }
            catch (Exception ex)
            {
                return -1;
            }

        }

    }

    


    }

