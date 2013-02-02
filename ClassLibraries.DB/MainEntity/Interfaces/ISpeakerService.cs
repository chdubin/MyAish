using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MainEntity.Models.Speaker;
using MainCommon;
using System.Web;

namespace MainEntity.Interfaces
{
    public interface ISpeakerService
    {
        string GetSpeakerPhoto(long speaker_id);

        long InsertSpeaker(Guid creator_id, long root_entity_id, string speaker_name, string speaker_description, string speaker_contact_info);

        long InsertSpeaker(Guid creator_id, long root_entity_id, string name, HttpPostedFileBase photo, string photo_path, string description,
            string contact_info, bool active);
        /*
        int GetSpeakerClassesCnt(long from_root_id, long speaker_id, bool? with_only_active, bool with_only_nondeleted, long portal_id);

        EntityItem[] GetSpeakerClasses(long from_root_id, long speaker_id, int start_row_index, int max_rows_count, bool? with_only_active, bool with_only_nondeleted, long portal_id);
        */
        EntityItem GetSpeaker(string name);

        EntityItem GetSpeaker(long speaker_id);

        EntityItem[] GetSpeakers(long from_root_id, bool? with_only_active, bool with_only_nondeleted, int start_row_index, int max_rows_count, SortParametersEnum sort, bool descending, long portal_id);

        EntityItem[] GetSpeakers1(long from_root_id, bool? with_only_active, bool with_only_nondeleted, int start_row_index, int max_rows_count, SortParametersEnum sort, bool descending, long portal_id);

        int GetSpeakersCnt(long from_root_id, bool? with_only_active, bool with_only_nondeleted, long portal_id);

        int GetSpeakersCnt1(long from_root_id, bool? with_only_active, bool with_only_nondeleted, long portal_id);

        void Update(Guid creator_id, long root_entity_id, long speaker_id, string name, HttpPostedFileBase photo, string photo_path, string contact_info, string descr, bool active);

        int DeleteSpeaker(long speaker_id);
    }
}
