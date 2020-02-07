using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using TavelRecordApp.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TavelRecordApp {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HistoryPage : ContentPage {
        public HistoryPage() {
            InitializeComponent();
        }

        protected override void OnAppearing() {
            base.OnAppearing();
            using (var conn = new SQLiteConnection(App.DatabaseLocation)) {
                conn.CreateTable<Post>();
                var posts = conn.Table<Post>().ToList();
                postListView.ItemsSource = posts;
            };
        }

        private void postListView_ItemSelected(object sender, SelectedItemChangedEventArgs e) {
            var selectedPost = postListView.SelectedItem as Post;
            if(selectedPost != null) {
                Navigation.PushAsync(new PostDetail(selectedPost));
            }
        }
    }
}