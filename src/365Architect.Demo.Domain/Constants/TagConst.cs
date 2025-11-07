using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _365Architect.Demo.Domain.Entities;

namespace _365Architect.Demo.Domain.Constants
{
    public class TagConst
    {
        public const string TABLE_NAME = "Tag";
        public const string FIELD_NAME = "Name";
        public const string FIELD_CREATED_AT = "CreatedAt";
        public const string FIELD_UPDATED_AT = "UpdatedAt";

        public const int NAME_MAX_LENGTH = 100;

        public const string MSG_TAG_ID_NOT_FOUND = $"{nameof(Tag)} with this id was not found";
    }
}
