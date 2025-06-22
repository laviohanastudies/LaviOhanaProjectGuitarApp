using Android.App;
using Android.Content;
using Android.Gms.Extensions;
using Android.Gms.Tasks;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Firebase.Firestore;
using LaviOhanaProjectGuitarApp.Helpers;
using LaviOhanaProjectGuitarApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaviOhanaProjectGuitarApp.Activities
{
    [Activity(Label = "OptionsActivity")]
    public class OptionsActivity : Activity, View.IOnClickListener, IOnSuccessListener
    {
        Button btnOptionsSearch, btnOptionsUploadSong, btnOptionsSongList, btnOptionsProfile;
        TextView tvWelcome;
        string uid, name;
        FireBaseData fbd;
        User user;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.OptionsLayout);
            uid = Intent.GetStringExtra("uid");
            Initobject();
            IniteViews();
            FindNameAsync();
        }

        private async void FindNameAsync()
        {
            await fbd.GetCollection(General.FS_UsersCollection, uid).AddOnSuccessListener(this);
        }

        private void Initobject()
        {
            fbd = new FireBaseData();
        }

        private void IniteViews()
        {
            btnOptionsSearch = FindViewById<Button>(Resource.Id.btnOptionsSearch);
            btnOptionsUploadSong = FindViewById<Button>(Resource.Id.btnOptionsUploadSong);
            btnOptionsSongList = FindViewById<Button>(Resource.Id.btnOptionsSongList);
            btnOptionsProfile = FindViewById<Button>(Resource.Id.btnOptionsProfile);

            tvWelcome = FindViewById<TextView>(Resource.Id.tvWelcome);

            btnOptionsSearch.SetOnClickListener(this);
            btnOptionsUploadSong.SetOnClickListener(this);
            btnOptionsSongList.SetOnClickListener(this);
            btnOptionsProfile.SetOnClickListener(this);

        }



        public void OnClick(View v)
        {
            if (v == btnOptionsSearch)
            {
                Intent intent = new Intent(this, typeof(SearchSongActivity));
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

        public void OnSuccess(Java.Lang.Object result)
        {
            var snapshot = (DocumentSnapshot)result;
            user = new User(snapshot.Id, snapshot.Get("Username").ToString(), snapshot.Get("Email").ToString(), snapshot.Get("Password").ToString());
            name = user.Username;
            tvWelcome.Text += name;
        }
    }
}