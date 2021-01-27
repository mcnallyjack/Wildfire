using System;
using System.Collections.Generic;
using System.Text;
using Wildfire.Models;
using Firebase.Database;
using Firebase.Database.Query;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;


namespace Wildfire.Helper
{
    public class FirebaseHelper
    {
        FirebaseClient firebase = new FirebaseClient("https://driven-bulwark-297919-default-rtdb.firebaseio.com/");

        public async Task<List<Fire>> GetAllFires()
        {
            return (await firebase
                .Child("Fire")
                .OnceAsync<Fire>()).Select(item => new Fire
                {
                    FireID = item.Object.FireID,
                    Latitude = item.Object.Latitude,
                    Longitude = item.Object.Longitude
                }).ToList();
        }

        public async Task AddFire(int fireId, string lat, string longi)
        {
            await firebase
                .Child("Fire")
                .PostAsync(new Fire() { FireID = fireId, Latitude = lat, Longitude = longi});
        }

        public async Task<Fire> GetFire(int fireId)
        {
            var allFires = await GetAllFires();
            await firebase
                .Child("Fire")
                .OnceAsync<Fire>();
            return allFires.Where(a => a.FireID == fireId).FirstOrDefault();
        }
    }
   
}
