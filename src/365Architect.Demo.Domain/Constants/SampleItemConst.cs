using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _365Architect.Demo.Domain.Entities;

namespace _365Architect.Demo.Domain.Constants
{
    public class SampleItemConst
    {
        public const string TABLE_NAME = "SampleItem";
        public const string FIELD_NAME = "Name";
        public const string FIELD_SAMPLE_ID = "SampleId";
        public const string FIELD_CREATED_AT = "CreatedAt";
        public const string FIELD_UPDATED_AT = "UpdatedAt";

        public const int NAME_MAX_LENGTH = 128;
        public const string MSG_SAMPLE_ITEM_ID_NOT_FOUND = $"{nameof(SampleItem)} with this id was not found";
        public const string MSG_SAMPLE_NOT_FOUND = $"{nameof(Sample)} not found for this item";
    }
}
