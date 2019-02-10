using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleRSS.Interfaces;
using ConsoleRSS.Models;

namespace ConsoleRSS.Interfaces
{
  public  interface IRSSSource : IRepository<RSSSource>
    {
    }
}
