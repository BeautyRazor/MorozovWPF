using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MorozovWPF.Helpers
{
    [System.AttributeUsage(AttributeTargets.Property)]
    class SQLiteFieldAttribute : Attribute {
        public string FieldName       { get; } = null;
        public bool   ConvertToString { get; } = false;
        public bool   ConvertToId { get; } = false;

        public SQLiteFieldAttribute(string fieldName = null, bool convertToString = false, bool convertToId = false) {
            FieldName       = fieldName;
            ConvertToString = convertToString;
            ConvertToId = convertToId;
        }
    }
}
