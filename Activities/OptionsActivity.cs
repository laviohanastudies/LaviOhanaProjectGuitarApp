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

namespace LaviOhanaProjectGuitarApp.Activities
{
    [Activity(Label = "OptionsActivity")]
    public class OptionsActivity : Activity, View.IOnClickListener
    {
        Button btnOptionsSearch, btnOptionsUploadSong, btnOptionsSongList;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.activity_main);
            IniteViews();
        }

        private void IniteViews()
        {
            btnOptionsSearch = FindViewById<Button>(Resource.Id.btnOptionsSearch);

            btnOptionsUploadSong = FindViewById<Button>(Resource.Id.btnOptionsUploadSong);

            btnOptionsSongList = FindViewById<Button>(Resource.Id.btnOptionsSongList);

            btnOptionsSearch.SetOnClickListener(this);
            btnOptionsUploadSong.SetOnClickListener(this);
            btnOptionsSongList.SetOnClickListener(this);

        }

        private void RegisterButton_Click(object sender, System.EventArgs e)
        {
            Intent intent = new Intent(this, typeof(RegisterActivity));
            StartActivity(intent);
        }

        private void LoginButton_Click(object sender, System.EventArgs e)
        {
            Intent intent = new Intent(this, typeof(LoginActivity));
            //Intent intent = new Intent(this, typeof(UploadSongActivity));
            // Intent intent = new Intent(this, typeof(SongListActivity));

            StartActivity(intent);
        }

        public void OnClick(View v)
        {
            if (v == btnOptionsSearch)
            {
                Intent intent = new Intent(this, typeof(LoginActivity));
                StartActivity(intent);
            }
            else if (v == btnOptionsUploadSong)
            {
                Intent intent = new Intent(this, typeof(UploadSongActivity));
                StartActivity(intent);
            }
            else
            {
                Intent intent = new Intent(this, typeof(SongListActivity));
                StartActivity(intent);
            }
        }
    }
}