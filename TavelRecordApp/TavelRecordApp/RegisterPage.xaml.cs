using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TavelRecordApp.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TavelRecordApp {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegisterPage : ContentPage {
        public RegisterPage() {
            InitializeComponent();
        }

        private async void RegisterButton_Clicked(object sender, EventArgs e) {
            if(passwordRegister.Text == passwordRegisterConfirm.Text) {
                Users user = new Users() {
                    Email = emailRegister.Text,
                    Password = passwordRegister.Text
                };

                await App.client.GetTable<Users>().InsertAsync(user);
            }
            else {
                DisplayAlert("Error", "Password Mismatch", "Ok");
            }
        }
    }
}