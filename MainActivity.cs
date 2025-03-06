using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Widget;
using AndroidX.AppCompat.App;
using LaviOhanaProjectGuitarApp.Activities;
using Microsoft.Win32;

namespace LaviOhanaProjectGuitarApp
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        Button LoginButton, RegisterButton;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);
            IniteViews();
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        private void IniteViews()
        {
            LoginButton = FindViewById<Button>(Resource.Id.LoginButton);
            LoginButton.Click += LoginButton_Click;

            RegisterButton = FindViewById<Button>(Resource.Id.RegisterButton);
            RegisterButton.Click += RegisterButton_Click;
        }

        private void RegisterButton_Click(object sender, System.EventArgs e)
        {
            Intent intent = new Intent(this, typeof(RegisterActivity));
            StartActivity(intent);
        }

        private void LoginButton_Click(object sender, System.EventArgs e)
        {
            //Intent intent = new Intent(this, typeof(LoginActivity));
            Intent intent = new Intent(this, typeof(UploadSongActivity));
            StartActivity(intent);
        }



    }
}