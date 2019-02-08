using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleRSS.Models
{
    public class RSSNews
    {
        public int Id { get; set; }
        public string NewsName { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public string NewsURL { get; set; }
        public int RSSSourceId { get; set; }
        public RSSSource RSSSource { get; set; }

    }
}
