using Android.App;
using Android.Content;
using Android.Gms.Extensions;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Firebase.Firestore.Auth;
using LaviOhanaProjectGuitarApp.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaviOhanaProjectGuitarApp.Activities
{
    [Activity(Label = "LoginActivity")]
    public class LoginActivity : Activity, View.IOnClickListener
    {
        EditText etEmail, etPassword;
        Button LoginBtn, btnTogglePassword;
        FireBaseData fbd;
        string uid;
        bool isPasswordVisible = false;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.LoginLayout);

            fbd = new FireBaseData();
            InitViews();
        }

        private void InitViews()
        {
            etEmail = FindViewById<EditText>(Resource.Id.etEmail);
            etPassword = FindViewById<EditText>(Resource.Id.etPassword);
            LoginBtn = FindViewById<Button>(Resource.Id.LoginBtn);
            btnTogglePassword = FindViewById<Button>(Resource.Id.btnTogglePassword);

            LoginBtn.SetOnClickListener(this);
            btnTogglePassword.Click += BtnTogglePassword_Click;
        }

        private void BtnTogglePassword_Click(object sender, EventArgs e)
        {
            if (isPasswordVisible)
            {
                etPassword.InputType = Android.Text.InputTypes.ClassText | Android.Text.InputTypes.TextVariationPassword;
                btnTogglePassword.Text = "Show";
                isPasswordVisible = false;
            }
            else
            {
                etPassword.InputType = Android.Text.InputTypes.ClassText | Android.Text.InputTypes.TextVariationVisiblePassword;
                btnTogglePassword.Text = "Hide";
                isPasswordVisible = true;
            }
            etPassword.SetSelection(etPassword.Text.Length);
        }


        public async void OnClick(View v)
        {
            if (v == LoginBtn)
            {
                if (await Login(etEmail.Text, etPassword.Text))
                {
                    Toast.MakeText(this, "Logged In Successfully", ToastLength.Short).Show();
                    etEmail.Text = "";
                    etPassword.Text = "";
                    Intent intent = new Intent(this, typeof(OptionsActivity));
                    intent.PutExtra("uid", uid);
                    StartActivity(intent);
                }
                else
                {
                    Toast.MakeText(this, "Login Failed", ToastLength.Short).Show();
                }
            }
        }

        private async Task<bool> Login(string email, string password)
        {
            try
            {
                await fbd.auth.SignInWithEmailAndPassword(email, password);
                uid = fbd.auth.CurrentUser.Uid;
            }
            catch (System.Exception ex)
            {
                string s = ex.Message;
                return false;
            }
            return true;
        }
    }
}