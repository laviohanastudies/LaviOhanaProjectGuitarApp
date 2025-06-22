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

namespace LaviOhanaProjectGuitarApp.Model
{
    internal class User
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public User()
        {
            this.Id = string.Empty;
            this.Username = string.Empty;
            this.Email = string.Empty;
            this.Password = string.Empty;
        }
        public User(string Id, string Username, string Email, string Password)
        {
            this.Id = Id;
            this.Username = Username;
            this.Email = Email;
            this.Password = Password;
        }
    }
}