using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MainEntity.Models.File;
using BLToolkit.Data;
using BLToolkit.Data.Linq;

namespace MainEntity
{
    public partial class FileDbManager
    {
        #region Select

        public IQueryable<FileEntity> SelectFilesByIDs(long[] file_ids, List<MainEntity.Models.File.FileEntity> fileList)
        {
            //var rval = from f in this.FileEntity
            //           where file_ids.Contains(f.fileID)
            //           select new FileEntity()
            //           {
            //               fileID = f.fileID,
            //               EntityItem = new EntityItem()
            //               {
            //                   entityID = f.fileID,
            //                   FileClassEntity = new ClassEntity()
            //                   {
            //                       classID = f.EntityItem.parentEntityID,
            //                       EntityItem = f.EntityItem.FileClassEntity.EntityItem,
            //                       SpeakerEntityItem = f.EntityItem.FileClassEntity.SpeakerEntityItem
            //                   }
            //               }
            //           };

            //return rval;

            List<long> newFileIds = new List<long>();
            foreach (long id_ in file_ids)
            {
                bool addId = fileList == null;
                if (!addId)
                {
                    FileEntity file = fileList.FirstOrDefault(x => x.fileID == id_);
                    addId = file == null;
                }
                if (addId)
                    newFileIds.Add(id_);
            }
            var rval = from f in this.FileEntity
                       join e in this.EntityItem on f.fileID equals e.entityID
                       join c in this.ClassEntity on e.parentEntityID equals c.classID
                       join ce in this.EntityItem on c.classID equals ce.entityID
                       join s in this.EntityItem on c.speakerID equals s.entityID
                       join ext in this.CatalogItemExtend on c.classID equals ext.entityID
                       where newFileIds.Contains(f.fileID) && !e.deleted && ce.active && f.fileTypeID == 2

                       select new FileEntity()
                       {
                           fileID = f.fileID,
                           filePath = f.filePath,
                           alternateFilePath = f.alternateFilePath,
                           EntityItem = new EntityItem()
                           {
                               entityID = e.entityID,
                               FileClassEntity = new ClassEntity()
                               {
                                   classID = c.classID,
                                   ClassEntityItem = ce,
                                   SpeakerEntityItem = s,
                                   CatalogItemExtend = ext
                               }
                           }
                       };

            return rval;
        }

        public IQueryable<FileEntity> SelectFilesByClassesIDs(long[] classes_ids) 
        {
            var rval = from f in this.FileEntity
                       where classes_ids.Contains(f.EntityItem.parentEntityID)
                       select new FileEntity()
                       {
                           fileID = f.fileID,
                           EntityItem = f.EntityItem
                       };

            return rval;
        }

		public IQueryable<FileEntity> GetFile(long file_id, long portal_id, bool with_only_active, bool with_only_nondeleted)
        {
			var rval = from f in this.FileEntity
					   join e in this.EntityItem on f.fileID equals e.entityID
					   join c in this.EntityItem on e.parentEntityID equals c.entityID
					   join cxp in this.CatalogItemXrefPortals on c.entityID equals cxp.catalogItemID
					   where f.fileID == file_id && cxp.portalID == portal_id
					   select new FileEntity()
					   {
						   alternateFilePath = f.alternateFilePath,
						   fileID = f.fileID,
						   filePath = f.filePath,
						   fileTypeID = f.fileTypeID,
						   FileEntityItem = e,
						   CatalogItem = c,
						   CatalogItemInPortal = cxp
					   };
			if (with_only_active)
				rval = rval.Where(f => f.CatalogItem.active);
			if (with_only_nondeleted)
				rval = rval.Where(f => !f.CatalogItem.deleted);


            return rval;
        }

        public IQueryable<FileEntity> GetFile(long file_id, bool with_only_active, bool with_only_nondeleted)
        {
            var rval = from f in this.FileEntity
                       join e in this.EntityItem on f.fileID equals e.entityID
                       join c in this.EntityItem on e.parentEntityID equals c.entityID
                       where f.fileID == file_id
                       select new FileEntity()
                       {
                           alternateFilePath = f.alternateFilePath,
                           fileID = f.fileID,
                           filePath = f.filePath,
                           fileTypeID = f.fileTypeID,
                           FileEntityItem = e,
                           CatalogItem = c,
                       };
            if (with_only_active)
                rval = rval.Where(f => f.CatalogItem.active);
            if (with_only_nondeleted)
                rval = rval.Where(f => !f.CatalogItem.deleted);


            return rval;
        }


        #endregion

        public int UpdateFilePath(long file_id, string file_path, string alternate_file_path)
        {
            if (file_path == null && alternate_file_path != null)
                return this.FileEntity.Where(f => f.fileID == file_id)
                    .Update(f => new FileEntity() { alternateFilePath = alternate_file_path });
            else if (file_path != null && alternate_file_path == null)
                return this.FileEntity.Where(f => f.fileID == file_id)
                    .Update(f => new FileEntity() { filePath = file_path });
            else if (file_path != null && alternate_file_path != null)
                return this.FileEntity.Where(f => f.fileID == file_id)
                    .Update(f => new FileEntity() { filePath = file_path, alternateFilePath = alternate_file_path });
            else return 0;

        }
    }
}
