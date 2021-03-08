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
                    Longitude = item.Object.Longitude,
                    Time = item.Object.Time,
                    WindDirection = item.Object.WindDirection
                }).ToList();
        }

        public async Task AddFire(string fireId, string lat, string longi, string time, string direction)
        {
            await firebase
                .Child("Fire")
                .PostAsync(new Fire() { FireID = fireId, Latitude = lat, Longitude = longi, Time=time, WindDirection = direction});
        }

        public async Task ResolveFire(string fireId)
        {
            var toResolve = (await firebase
                .Child("Fire")
                .OnceAsync<Fire>()).Where(a => a.Object.FireID == fireId).FirstOrDefault();
            await firebase.Child("Fire").Child(toResolve.Key).DeleteAsync();
        }

        public async Task<Fire> GetFire(string fireId)
        {
            var allFires = await GetAllFires();
            await firebase
                .Child("Fire")
                .OnceAsync<Fire>();
            return allFires.Where(a => a.FireID == fireId).FirstOrDefault();
        }
    }
   
}
