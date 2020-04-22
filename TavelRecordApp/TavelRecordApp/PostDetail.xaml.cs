using SQLite;
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
    public partial class PostDetail : ContentPage {
        Post selectedPost;

        public PostDetail() {
            InitializeComponent();
        }

        public PostDetail(Post selectedPost) {
            InitializeComponent();
            this.selectedPost = selectedPost;
            experienceEntry.Text = selectedPost.Experience;
        }

        private async void updateButton_Clicked(object sender, EventArgs e) {
            selectedPost.Experience = experienceEntry.Text;
            await App.client.GetTable<Post>().UpdateAsync(selectedPost);
            await DisplayAlert("Success", "Experience successfully updated", "Ok");

            /*
            selectedPost.Experience = experienceEntry.Text;
            using (var connection = new SQLiteConnection(App.DatabaseLocation)) {
                connection.CreateTable<Post>();
                var rows = connection.Update(selectedPost);

                if (rows > 0) {
                    DisplayAlert("Added", "Your gibberish has been updated", "Epic dab");
                }
                else {
                    DisplayAlert("Mf failed", "You ting failed fam", "Sad dev noises");
                }
            }
            */
        }

        private async void deleteButton_Clicked(object sender, EventArgs e) {
            await App.client.GetTable<Post>().DeleteAsync(selectedPost);
            await DisplayAlert("Success", "Experience successfully deleted", "Ok");
            /*
            using (var connection = new SQLiteConnection(App.DatabaseLocation)) {
                connection.CreateTable<Post>();
                var rows = connection.Delete(selectedPost);

                if (rows > 0) {
                    DisplayAlert("Added", "Your gibberish has been deleted", "Epic dab");
                }
                else {
                    DisplayAlert("Mf failed", "You ting failed fam", "Sad dev noises");
                }
            }
            */
        }
    }
}