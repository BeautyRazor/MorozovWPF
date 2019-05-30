using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MorozovWPF.Helpers {
    public enum Tables {
        Consumers,
        Services,
        Sales,
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
        
    }
}
