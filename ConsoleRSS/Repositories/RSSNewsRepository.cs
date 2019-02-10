using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleRSS.Interfaces;
using System.Data.Entity;
using ConsoleRSS.EF;
using ConsoleRSS.Models;

namespace ConsoleRSS.Repositories
{
  public  class RSSNewsRepository : IRepository<RSSNews>
    {
        private Context db;

        public RSSNewsRepository(Context context)
        {
            db = context;
        }
        public IEnumerable<RSSNews> GetAll()
        {
            return db.RSSNews.Include(cfg => cfg.RSSSource);
        }

        public RSSNews Get (int? id)
        {
            return db.RSSNews.Where(s => s.Id == id).Include(cfg => cfg.RSSSource).FirstOrDefault();
        }

        public void Create (RSSNews item)
        {
            db.RSSNews.Add(item);
           
        }
        public void Save()
        {
            db.SaveChanges();
        }
        public int GetCount()
        {
            int count = db.RSSNews.Count();
            return count;
        }
        public int GetCount(string item)
        {
            return db.RSSNews.Where(cfd => cfd.RSSSource.SourceName == item).Count();
        }
        public bool Check(DateTime date,string name)
        {
            bool cond = true;
            if (db.RSSNews.Where(s => s.Date == date.Date).FirstOrDefault() != null)
                cond = false;
            if (db.RSSNews.Where(s => s.NewsName == name).FirstOrDefault() != null)
                cond = false;

            return cond;
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
