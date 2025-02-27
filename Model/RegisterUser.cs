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
    internal class RegisterUser
    {
        public string Id {  get; set; }
        public string Userame { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Level { get; set; }
        public RegisterUser()
        {
            this.Id = string.Empty;
            this.Userame = string.Empty;
            this.Email = string.Empty;
            this.Password = string.Empty;
            this.Level = string.Empty;
        }
        public RegisterUser(string Id, string Username, string Mail, string Password, string Level)
        {
            this.Id = Id;
            this.Userame = Username;
            this.Email = Mail;
            this.Password = Password;
            this.Level = Level;
        }

    }
}