using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BLToolkit.Data;
using System.Data;

namespace MyPartnerKinoBL.Extension
{
    public class BaseBO
    {
        private string _connectionName;
        private IDbConnection _externalConnection;

        public BaseBO(string connection_name)
        {
            _connectionName = connection_name;
        }

        public BaseBO(IDbConnection external_connection)
        {
            _externalConnection = external_connection;
        }

        protected T CreateDbManager<T>()
            where T:BLToolkit.Data.DbManager
        {
            T rval = default(T);

            if(_externalConnection==null)
                rval = (T)Activator.CreateInstance(typeof(T), _connectionName);
            else
                rval = (T)Activator.CreateInstance(typeof(T), _externalConnection);

            return rval;
        }

        protected Tr Exec<Td, Tr>(IsolationLevel transaction_level, Func<Td,Tr> func)
            where Td : DbManager
        {
            Tr rval = default(Tr);
            using (var context = CreateDbManager<Td>())
            {
                rval = context.Exec(func, transaction_level);
            }
            return rval;
        }

        protected void Exec<Td>(IsolationLevel transaction_level, Action<Td> action)
            where Td : DbManager
        {
            using (var context = CreateDbManager<Td>())
            {
                context.Exec(action, transaction_level);
            }
        }

    }
}
