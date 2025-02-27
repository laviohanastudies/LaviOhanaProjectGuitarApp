using Android.App;
using Android.Content;
using Android.Gms.Extensions;
using Android.Gms.Tasks;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Firebase.Firestore;
using Firebase.Firestore.Model;
using LaviOhanaProjectGuitarApp.Helpers;
using LaviOhanaProjectGuitarApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaviOhanaProjectGuitarApp.Activities
{
    [Activity(Label = "ProfileActivity")]
    public class ProfileActivity : Activity, IOnSuccessListener
    {
        EditText etProfileEmail, etProfileUsername, etProfilePassword, etProfileLevel;
        Button profileUpdateBtn;
        FireBaseData fbd;
        string uid;
        RegisterUser user;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.ProfileLayout);
            uid = Intent.GetStringExtra("uid");
            InitObject();
            InitViews();
            GetProfile();
        }

        private async void GetProfile()
        {
            await fbd.GetCollection(General.FS_UsersCollection, uid).AddOnSuccessListener(this);
        }

        private void InitViews()
        {
            etProfileUsername = FindViewById<EditText>(Resource.Id.etProfileUsername);
            etProfileEmail = FindViewById<EditText>(Resource.Id.etProfileEmail);
            etProfilePassword = FindViewById<EditText>(Resource.Id.etProfilePassword);
            etProfileLevel = FindViewById<EditText>(Resource.Id.etProfileLevel);
            profileUpdateBtn = FindViewById<Button>(Resource.Id.UpdateProfileBtn);
            //ProfileUpdateButton.Click += ProfileUpdateButton_ClickAsync;
        }

        private async void ProfileUpdateButton_ClickAsync(object sender, EventArgs e)
        {
            if(await UpdateUsername(etProfileUsername.Text))
            {
                Toast.MakeText(this, "Updated", ToastLength.Short).Show();
            }
            else
            {
                Toast.MakeText(this, "Faild", ToastLength.Short).Show();
            }
        }

        private async Task<bool> UpdateUsername(string UserName)
        {
            try
            {
                DocumentReference UserReference = fbd.firestore.Collection(General.FS_UsersCollection).Document(uid);
                await UserReference.Update(General.KEY_USERNAME, UserName);
            }
            catch { return false; }
            return true;
        }

        private void InitObject()
        {
            fbd = new FireBaseData();
        }

        public void OnSuccess(Java.Lang.Object result)
        {
            
            //public RegisterUser(string Id, string Username, string Mail, string Password, string Level)
            var snapshot = (DocumentSnapshot)result;
            user = new RegisterUser(snapshot.Id, snapshot.Get("Username").ToString(), snapshot.Get("Email").ToString(), snapshot.Get("Password").ToString(), snapshot.Get("Level").ToString());
            PrintUser(user);
        }

        private void PrintUser(RegisterUser user)
        {
            etProfileUsername.Text = user.Userame;
            etProfileEmail.Text = user.Email;
            etProfilePassword.Text = user.Password;
            etProfileLevel.Text = user.Level;
        }
    }
}