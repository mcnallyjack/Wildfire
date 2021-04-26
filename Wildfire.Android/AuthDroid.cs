﻿/* Author:      Jack McNally
 * Page Name:   AuthDroid
 * Purpose:     Authentication for Android.
 */
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wildfire.Services;
using Xamarin.Forms;
using Firebase.Auth;
using Wildfire.Droid;

[assembly : Dependency(typeof(AuthDroid))]
namespace Wildfire.Droid
{
    public class AuthDroid : IAuth
    {
        // Login 
        public async Task<string> LoginWithEmailAndPassword(string email, string password)
        {
            try
            {
                var newUser = await Firebase.Auth.FirebaseAuth.Instance.SignInWithEmailAndPasswordAsync(email, password);
                var token = newUser.User.Uid;
                
                return token;
            }
            catch (FirebaseAuthInvalidUserException e)
            {
                e.PrintStackTrace();
                return string.Empty;
            }
            catch (FirebaseAuthInvalidCredentialsException e)
            {
                e.PrintStackTrace();
                return string.Empty;
            };
        }

        // Sign In
        public bool SignIn()
        {
            var user = Firebase.Auth.FirebaseAuth.Instance.CurrentUser;
            return user != null;
        }

        // Sign Out
        public bool SignOut()
        {
            try
            {
                Firebase.Auth.FirebaseAuth.Instance.SignOut();
                return true;
            }
            catch(Exception)
            {
                return false;
            }
        }

        // Sign Up
        public async Task<string> SignUpWithEmailAndPassword(string email, string password)
        {
            try
            {
                var newUser = await Firebase.Auth.FirebaseAuth.Instance.CreateUserWithEmailAndPasswordAsync(email, password);
                var token = newUser.User.Uid;
                return token;
            }
            catch (FirebaseAuthInvalidUserException e)
            {
                e.PrintStackTrace();
                return string.Empty;
            }
            catch (FirebaseAuthInvalidCredentialsException e)
            {
                e.PrintStackTrace();
                return string.Empty;
            }
        }

        // Forgot Password
        public async Task ForgotPassword(string email)
        {
            await FirebaseAuth.Instance.SendPasswordResetEmailAsync(email);
        }

        // Chnage Password
        public async Task ChangePassword(string newPassword)
        {
            
            var currentUser = FirebaseAuth.Instance.CurrentUser;

            await currentUser.UpdatePasswordAsync(newPassword);

        }
    }

}