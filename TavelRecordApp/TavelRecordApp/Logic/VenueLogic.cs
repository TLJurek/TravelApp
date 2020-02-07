using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TavelRecordApp.Model;

namespace TavelRecordApp.Logic {
    public class VenueLogic {
        public async static Task<List<Venue>> GetVenues(double lat, double @long) {
            var venues = new List<Venue>();

            var url = VenueRoot.GenerateURL(lat, @long);

            using (var client = new HttpClient()) {
                var response = await client.GetAsync(url);
                var json = await response.Content.ReadAsStringAsync();

                var venueRoot = JsonConvert.DeserializeObject<VenueRoot>(json);
                venues = venueRoot.response.venues as List<Venue>;
            };

            return venues;
        }
    }
}
