using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Infrastructure
{
    public interface IRepository
    {
        T Add<T>(T tt);

        void Delete<T>(T tt);

        List<T> GetAll<T>();

        T Get<T>(int Id);

        T Update<T>(T tt);
    }
}
