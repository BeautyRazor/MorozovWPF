using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.RightsManagement;
using System.Text;
using System.Threading.Tasks;
using MorozovWPF.Helpers;

namespace MorozovWPF.Models {
    public abstract class DataModel<T> : ModelBase<T>
        where T : DataModel<T>, new() {
        private int id;

        [SQLiteField]
        public int ID {
            get => id;
            set => SetProperty(ref id, value);
        }

        public abstract T FromDataRow(DataRow row);

        protected static string GetString(object any) => any as string;
        protected static bool   GetBool(object   any) => int.TryParse(any.ToString(), out int tempBool) && tempBool != 0;
        protected static int    GetInt(object    any) => int.TryParse(any.ToString(), out int tempInt) ? tempInt : 0;
        protected static double GetDouble(object any) => double.TryParse(any.ToString(), out double tempDouble) ? tempDouble : 0;

    }

   public interface IConvertiblrToId {
        int convertToId();
    }
}