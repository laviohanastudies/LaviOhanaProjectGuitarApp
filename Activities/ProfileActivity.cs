using Android.App;
using Android.Content;
using Android.Gms.Extensions;
using Android.Gms.Tasks;
using Android.OS;
using Android.Runtime;
using Android.Text;
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
        EditText etProfileEmail, etProfileUsername, etProfilePassword;
        Button profileUpdateBtn, btnTogglePassword;
        FireBaseData fbd;
        string uid;
        User user;
        bool isPasswordVisible = false;

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

            profileUpdateBtn = FindViewById<Button>(Resource.Id.UpdateProfileBtn);
            btnTogglePassword = FindViewById<Button>(Resource.Id.btnTogglePassword);

            profileUpdateBtn.Click += ProfileUpdateButton_ClickAsync;
            btnTogglePassword.Click += BtnTogglePassword_Click;
        }

        private void BtnTogglePassword_Click(object sender, EventArgs e)
        {
            if (isPasswordVisible)
            {
                etProfilePassword.InputType = Android.Text.InputTypes.ClassText | Android.Text.InputTypes.TextVariationPassword;
                btnTogglePassword.Text = "Show";
                isPasswordVisible = false;
            }
            else
            {
                etProfilePassword.InputType = Android.Text.InputTypes.ClassText | Android.Text.InputTypes.TextVariationVisiblePassword;
                btnTogglePassword.Text = "Hide";
                isPasswordVisible = true;
            }
        }


        private async void ProfileUpdateButton_ClickAsync(object sender, EventArgs e)
        {
            if (await UpdateUsername(etProfileUsername.Text) && await UpdateEmail(etProfileEmail.Text) && await UpdatePassword(etProfilePassword.Text))
            {
                Toast.MakeText(this, "Updated", ToastLength.Short).Show();
            }
            else
            {
                Toast.MakeText(this, "Failed", ToastLength.Short).Show();
            }
        }

        private async Task<bool> UpdateUsername(string Username)
        {
            try
            {
                DocumentReference UserReference = fbd.firestore.Collection(General.FS_UsersCollection).Document(uid);
                await UserReference.Update(General.KEY_USERNAME, Username);
            }
            catch
            {
                return false;
            }
            return true;
        }
        private async Task<bool> UpdateEmail(string Email)
        {
            try
            {
                DocumentReference UserReference = fbd.firestore.Collection(General.FS_UsersCollection).Document(uid);
                await UserReference.Update(General.KEY_EMAIL, Email);
            }
            catch
            {
                return false;
            }
            return true;
        }
        private async Task<bool> UpdatePassword(string Password)
        {
            try
            {
                DocumentReference UserReference = fbd.firestore.Collection(General.FS_UsersCollection).Document(uid);
                await UserReference.Update(General.KEY_PASSWORD, Password);
            }
            catch
            {
                return false;
            }
            return true;
        }

        private void InitObject()
        {
            fbd = new FireBaseData();
        }

        public void OnSuccess(Java.Lang.Object result)
        {
            var snapshot = (DocumentSnapshot)result;
            user = new User(snapshot.Id, snapshot.Get("Username").ToString(), snapshot.Get("Email").ToString(), snapshot.Get("Password").ToString());
            PrintUser(user);
        }


        private void PrintUser(User user)
        {
            etProfileUsername.Text = user.Username;
            etProfileEmail.Text = user.Email;
            etProfilePassword.Text = user.Password;
        }
    }
}