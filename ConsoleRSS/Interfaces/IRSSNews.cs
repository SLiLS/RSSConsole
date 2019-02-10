using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleRSS.EF;
using ConsoleRSS.Models;
using ConsoleRSS.Interfaces;


namespace ConsoleRSS
{
 public   interface IRSSNews : IRepository<RSSNews>
    {


        int GetCount();
        bool Check(DateTime date,string name);
        int GetCount(string item);
        


    }
}
