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
    public partial class ProfilePage : ContentPage {
        public ProfilePage() {
            InitializeComponent();
        }

        protected override async void OnAppearing() {
            base.OnAppearing();
            //using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation)) {
                
                var postTable = await App.client.GetTable<Post>().Where(p => p.UserId == App.user.Id).ToListAsync();

                //Full LINQ
                var catogaries = (from p in postTable
                                  orderby p.CategoryId
                                  select p.CategoryName).Distinct().ToList();

                //LINQ with Lambdas
                var cats = postTable.OrderBy(p => p.CategoryId).Select(p => p.CategoryName).Distinct().ToList();

                var categoriesCount = new Dictionary<string, int>();
                foreach (var category in cats) {

                    //Full linq
                    var count = (from post in postTable
                                 where post.CategoryName == category
                                 select post).ToList().Count;

                    //Linq with Lambda
                    var cnts = postTable.Where(p => p.CategoryName == category).ToList().Count;

                    categoriesCount.Add((category ?? "No Category"), cnts);
                }

                categoriesListView.ItemsSource = categoriesCount;

                postCountLabel.Text = postTable.Count.ToString();
            //}
        }
    }
}