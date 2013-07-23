using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using log4net;


namespace CargadorTuLlanta.com.amdp.utils
{
    class MySQLConnector
    {

        private static readonly ILog log = LogManager.GetLogger(typeof(MySQLConnector));

        public void connect()
        {



            string connectionString;
            connectionString = "SERVER=" + Configuracion.Default.SERVER + ";Port="+Configuracion.Default.PORT+";" + "DATABASE=" +
             Configuracion.Default.DATABASE + ";" + "UID=" + Configuracion.Default.USERNAME + ";" + "PASSWORD=" + Configuracion.Default.PASSWORD + ";";


            try
            {
                MySqlConnection connection = new MySqlConnection(connectionString);

                connection.Open();
            }catch(MySqlException ed){
                log.Error("No se puedo realizar la conexión a la base de datos:"+connectionString+" "+ed);
            }


        }


    }
}
