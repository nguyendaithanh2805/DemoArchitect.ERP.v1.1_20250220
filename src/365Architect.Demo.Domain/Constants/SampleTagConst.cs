using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _365Architect.Demo.Domain.Entities;

namespace _365Architect.Demo.Domain.Constants
{
    public class SampleTagConst
    {
        public const string TABLE_NAME = "SampleTag";
        public const string FIELD_NOTE = "Note";
        public const string FIELD_CREATED_AT = "CreatedAt";
        public const string FIELD_UPDATED_AT = "UpdatedAt";

        public const int NOTE_MAX_LENGTH = 100;

        public const string MSG_SAMPLE_TAG_ID_NOT_FOUND = $"{nameof(SampleTag)} with this id was not found";
    }
}
