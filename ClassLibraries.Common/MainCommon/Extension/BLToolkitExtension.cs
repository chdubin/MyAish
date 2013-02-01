using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using BLToolkit.Data;

namespace MyPartnerKinoBL
{
    public static class BLToolkitExtension
    {
        public static Tr Exec<Td,Tr>(this Td db_manager, Func<Td,Tr> func, IsolationLevel transaction_level)
            where Td : DbManager
        {
            Tr rval = default(Tr);
            try
            {
                db_manager.BeginTransaction(transaction_level);
                rval = func(db_manager);
                db_manager.CommitTransaction();
            }
            catch
            {
                db_manager.RollbackTransaction();
                throw;
            }
            return rval;
        }

        public static void Exec<Td>(this Td db_manager, Action<Td> func, IsolationLevel transaction_level)
            where Td : DbManager
        {
            try
            {
                db_manager.BeginTransaction(transaction_level);
                func(db_manager);
                db_manager.CommitTransaction();
            }
            catch
            {
                db_manager.RollbackTransaction();
                throw;
            }
        }

    }
}
