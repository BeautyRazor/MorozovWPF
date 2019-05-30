using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using MorozovWPF.Helpers;
using MorozovWPF.Models;
using MorozovWPF.Views;

namespace MorozovWPF.Commands {
    class AuthCommands {
        private static Application app => Application.Instance;

        public static RelayCommand SignInCommand => new RelayCommand(
            actionParameter => {
                if (actionParameter is PasswordBox passwordBox) {
                    if (DatabaseHelper.Authorize(app.UserName, passwordBox.Password)) {
                        app.CurrentControl = new MainWorkPlane();
                    }
                    else {
                        app.CurrentMessage = "Неверное имя пользователя или пароль";
                    }
                }
                else {
                    app.CurrentMessage = "Неизвестная ошибка";
                }
            }
        );
    }
}