using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleRSS.Interfaces
{
   public interface IRepository<T>: IDisposable where T:class 
    {
        IEnumerable<T> GetAll();
        T Get(int? id);
        void Create(T item);
        void Save();
    }
}
