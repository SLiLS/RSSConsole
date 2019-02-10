using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleRSS.Interfaces;
using ConsoleRSS.Models;
using ConsoleRSS.EF;

namespace ConsoleRSS.Repositories
{
   public class RSSSourcesRepository : IRSSSource
    {
      private  Context db;

        public RSSSourcesRepository(Context context)
        {
            db = context;
        }
        public IEnumerable<RSSSource> GetAll()
        {
            return db.RSSSources;
        }

        public RSSSource Get(int? id)
        {
            return db.RSSSources.Where(s => s.Id == id).FirstOrDefault();
        }

        public void Create(RSSSource item)
        {
            db.RSSSources.Add(item);
           
        }
        public void Save()
        {
            db.SaveChanges();
        }
        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

    }
}
