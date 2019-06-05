using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MorozovWPF.Models;

namespace MorozovWPF.Helpers {
    public enum Tables {
        Consumers,
        Services,
        Orders,
        Auth
    }
    class DatabaseHelper {
        public static bool Authorize(string userName, string password) {
            if (userName == "Morozov" && password =="Kirill") {
                return true;
            }
            else {
                return false;
            }
        }

        public static List<T> GetFullData<T>(Tables table) {
            throw new NotImplementedException();
        }

        public static bool AddRow<T>(Tables table, T data) {
            throw new NotImplementedException();
        }

        public static bool UpdateRow<T>(Tables table, T data) {
            throw new NotImplementedException();
        }
        
    }
}
