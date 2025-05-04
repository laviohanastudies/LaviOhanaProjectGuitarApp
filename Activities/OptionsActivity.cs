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
        Button btnOptionsSearch, btnOptionsUploadSong, btnOptionsSongList, btnOptionsProfile;
        string uid;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.OptionsLayout);
            uid = Intent.GetStringExtra("uid");
            IniteViews();
        }

        private void IniteViews()
        {
            btnOptionsSearch = FindViewById<Button>(Resource.Id.btnOptionsSearch);

            btnOptionsUploadSong = FindViewById<Button>(Resource.Id.btnOptionsUploadSong);

            btnOptionsSongList = FindViewById<Button>(Resource.Id.btnOptionsSongList);

            btnOptionsProfile = FindViewById<Button>(Resource.Id.btnOptionsProfile);


            btnOptionsSearch.SetOnClickListener(this);
            btnOptionsUploadSong.SetOnClickListener(this);
            btnOptionsSongList.SetOnClickListener(this);
            btnOptionsProfile.SetOnClickListener(this);


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
            else if (v == btnOptionsSongList)
            {
                Intent intent = new Intent(this, typeof(SongListActivity));
                StartActivity(intent);
            }
            else
            {
                Intent intent = new Intent(this, typeof(ProfileActivity));
                intent.PutExtra("uid", uid);
                StartActivity(intent);
            }
        }
    }
}