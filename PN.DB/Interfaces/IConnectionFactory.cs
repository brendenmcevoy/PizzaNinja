using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PN.DB.Interfaces
{
    public interface IConnectionFactory
    {
        IDbConnection GetConnection { get; }
    }
}
