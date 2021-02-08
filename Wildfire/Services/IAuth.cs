﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Wildfire.Services
{
    public interface IAuth
    {
        Task<string> LoginWithEmailAndPassword(string email, string password);

        Task<string> SignUpWithEmailAndPassword(string email, string password);

        bool SignOut();

        bool SignIn();


    }
}
