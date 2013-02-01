using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MyPartnerKinoBL.Extension;
using MainEntity.Models.File;
using MainEntity;

namespace MainBL
{
    public class FileService : BaseBO, MainEntity.Interfaces.IFileService
    {
        public FileService(string connection_name)
            : base(connection_name) { }

        #region Select

        public FileEntity[] SelectFilesByIDs(long[] file_ids, List<MainEntity.Models.File.FileEntity> fileList)
        {
            return this.Exec(System.Data.IsolationLevel.Snapshot,
            (FileDbManager context) =>
            {
                return context.SelectFilesByIDs(file_ids, fileList).ToArray();
            });
        }

        public FileEntity[] SelectFilesByClassesIDs(long[] classes_ids)
        {
            return this.Exec(System.Data.IsolationLevel.Snapshot,
           (FileDbManager context) =>
           {
               return context.SelectFilesByClassesIDs(classes_ids).ToArray();
           });
        }

        public FileEntity GetFile(long file_id, long? portal_id)
        {
            return this.Exec(System.Data.IsolationLevel.Snapshot,
                (FileDbManager context) =>
                {
                    var rval = portal_id.HasValue ? context.GetFile(file_id, portal_id.Value, true, true) : context.GetFile(file_id, true, true);

                    return rval.SingleOrDefault();
                });
        }


        #endregion

        #region Update

        public int UpdateFilePath(long file_id, string file_path, string alternate_file_path)
        {
            return this.Exec(System.Data.IsolationLevel.ReadCommitted,
                (FileDbManager context) =>
                {
                    return context.UpdateFilePath(file_id, file_path, alternate_file_path);
                });
        }

        #endregion
    }
}
