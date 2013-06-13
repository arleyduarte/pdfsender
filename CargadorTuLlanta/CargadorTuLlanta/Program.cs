using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CargadorTuLlanta.com.amdp.tullanta;

namespace CargadorTuLlanta
{
    class Program
    {
        static void Main(string[] args)
        {
            TuLLantaAdapter tuLLantaAdapter = new TuLLantaAdapter();
            tuLLantaAdapter.updateData();
        }
    }
}
