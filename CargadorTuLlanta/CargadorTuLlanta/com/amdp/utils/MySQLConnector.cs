using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;


namespace CargadorTuLlanta.com.amdp.utils
{
    class MySQLConnector
    {




        public void connect()
        {



            string connectionString;
            connectionString = "SERVER=" + Configuracion.Default.SERVER + ";Port="+Configuracion.Default.PORT+";" + "DATABASE=" +
             Configuracion.Default.DATABASE + ";" + "UID=" + Configuracion.Default.USERNAME + ";" + "PASSWORD=" + Configuracion.Default.PASSWORD + ";";

        
            MySqlConnection connection = new MySqlConnection(connectionString);

            connection.Open();
        }


    }
}
