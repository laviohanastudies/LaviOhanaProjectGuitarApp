using Android.App;
using Android.Content;
using Android.Gms.Extensions;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Firebase.Firestore;
using Java.Util;
using LaviOhanaProjectGuitarApp.Helpers;
using LaviOhanaProjectGuitarApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaviOhanaProjectGuitarApp.Activities
{
    [Activity(Label = "RegisterActivity")]
    public class RegisterActivity : Activity
    {
        EditText etRegisterEmail, etRegisterPass, etRegisterUsername, etRegisterLevel;
        TextView tvRegisterDisplay;
        Button RegisterBtn;
        FireBaseData fbd;
        RegisterUser user;
        HashMap userMap;
        string uid;
        public static string id;

        
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Title = string.Empty;
            SetContentView(Resource.Layout.RegisterLayout);
            InitObject();
            // Create your application here
            InitViews();
        }

        private void InitObject()
        {
            fbd = new FireBaseData();
            user = new RegisterUser();
        }

        private void InitViews()
        {
            etRegisterUsername = FindViewById<EditText>(Resource.Id.etRegisterUsername);
            etRegisterEmail = FindViewById<EditText>(Resource.Id.etRegisterEmail);
            etRegisterPass = FindViewById<EditText>(Resource.Id.etRegisterPass);
            etRegisterLevel = FindViewById<EditText>(Resource.Id.etRegisterLevel);
            tvRegisterDisplay = FindViewById<TextView>(Resource.Id.tvRegisterDisplay);
            RegisterBtn = FindViewById<Button>(Resource.Id.RegisterBtn);
            RegisterBtn.Click += RegisterBtn_Click;
        }

        private void RegisterBtn_Click(object sender, EventArgs e)
        {
            if(0==0) // CheckInput בדיקת קלט
            {
                SaveImageAndDocument();
            }
        }

        private void SaveImageAndDocument()
        {
            SaveDocument();
        }

        private async void SaveDocument()
        {
            if(await Register(etRegisterUsername, etRegisterEmail, etRegisterPass, etRegisterLevel))
            {
                Toast.MakeText(this, "Registered Successfully", ToastLength.Short).Show();
                etRegisterUsername.Text = string.Empty;
                etRegisterEmail.Text = string.Empty;
                etRegisterPass.Text = string.Empty;
                etRegisterLevel.Text = string.Empty;
                
            }
            else
            {
                Toast.MakeText(this, "Register Failed", ToastLength.Short).Show();
            }
        }

        private async Task<bool> Register(EditText etRegisterUsername, EditText etRegisterEmail, EditText etRegisterPass, EditText etRegisterLevel)
        {
            try
            {
                await fbd.auth.CreateUserWithEmailAndPassword(etRegisterEmail.Text, etRegisterPass.Text);
                uid = fbd.auth.CurrentUser.Uid;
                userMap = new HashMap();
                userMap.Put(General.KEY_ID, uid);
                userMap.Put(General.KEY_USERNAME, etRegisterUsername.Text);
                userMap.Put(General.KEY_EMAIL, etRegisterEmail.Text);
                userMap.Put(General.KEY_PASSWORD, etRegisterPass.Text);
                userMap.Put(General.KEY_LEVEL, etRegisterLevel.Text);
                DocumentReference userReference = fbd.firestore.Collection(General.FS_UsersCollection).Document(uid);
                await userReference.Set(userMap);
            }
            catch (Exception ex)
            {

            }
            return true;
        }
    }
}