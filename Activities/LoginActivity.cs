using Android.App;
using Android.Content;
using Android.Gms.Extensions;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
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
        EditText etEmail, etPass;
        TextView tvDisplay;
        Button LoginBtn;
        FireBaseData fbd;
        string uid;

        public async void OnClick(View v)
        {
            if (v == LoginBtn)
            {
                if (await Login(etEmail.Text, etPass.Text))
                {
                    Toast.MakeText(this, "Logged In Successfully", ToastLength.Short).Show();
                    etEmail.Text = "";
                    etPass.Text = "";
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

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.LoginLayout);
            fbd = new FireBaseData();
            IniteViews();
        }

        private void IniteViews()
        {
            etEmail = FindViewById<EditText>(Resource.Id.etEmail);
            etPass = FindViewById<EditText>(Resource.Id.etPass);
            tvDisplay = FindViewById<TextView>(Resource.Id.tvDisplay);
            LoginBtn = FindViewById<Button>(Resource.Id.LoginBtn);
            LoginBtn.SetOnClickListener(this);
        }
    }
}