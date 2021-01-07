using System;
using System.Collections.Generic;
using System.Text;
using Firebase.Database;
using Firebase.Database.Query;
using Newtonsoft.Json;

namespace Wildfire.Helper
{
    public class FirebaseHelper
    {
        FirebaseClient firebase = new FirebaseClient("https://driven-bulwark-297919-default-rtdb.firebaseio.com/");
    }
}
