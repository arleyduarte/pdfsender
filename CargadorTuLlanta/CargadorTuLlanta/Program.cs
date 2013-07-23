using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CargadorTuLlanta.com.amdp.tullanta;
using CargadorTuLlanta.com.amdp.utils;

namespace CargadorTuLlanta
{
    class Program
    {
        static void Main(string[] args)
        {

            MySQLConnector connector = new MySQLConnector();
            connector.connect();
            
            //TuLLantaAdapter tuLLantaAdapter = new TuLLantaAdapter();
            //tuLLantaAdapter.updateData();
        }
    }
}
