using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MainEntity.Models.File;

namespace MainEntity.Interfaces
{
    public interface IFileService
    {
        FileEntity[] SelectFilesByIDs(long[] file_ids, List<MainEntity.Models.File.FileEntity> fileList = null);
// this was added on feb 2 from version i had download from git hub 
FileRoyaltiesEntity[] SelectFilesByRoyalties(MainEntity.Models.Activity.RoyaltyLog[] files, List<MainEntity.Models.File.FileRoyaltiesEntity> fileList = null);

        FileEntity[] SelectFilesByClassesIDs(long[] classes_ids);

		FileEntity GetFile(long file_id, long? portal_id);

        int UpdateFilePath(long file_id, string file_path, string alternate_file_path);
    }
}
