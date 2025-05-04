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
    [Activity(Label = "SongProfileActivity")]
    public class SongProfileActivity : Activity, IOnSuccessListener
    {
        TextView tvSongName, tvSongPerformer, tvSongLevel;
        FireBaseData fbd;
        string sid;
        Song song;
        Android.Content.ISharedPreferences sp;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.SongProfileLayout);
            //sid = Intent.GetStringExtra("sid");
            InitObject();
            GetDataFromSp();
            InitViews();
            GetProfile();
        }

        private async void GetProfile()
        {
            await fbd.GetCollection(General.FS_SONG_COLLECTION, sid).AddOnSuccessListener(this);
        }

        private void InitViews()
        {
            tvSongName = FindViewById<TextView>(Resource.Id.tvSongName);
            tvSongPerformer = FindViewById<TextView>(Resource.Id.tvSongPerformer);
            tvSongLevel = FindViewById<TextView>(Resource.Id.tvSongLevel);
            
        }

        //private async void ProfileUpdateButton_ClickAsync(object sender, EventArgs e)
        //{
        //    if (await UpdateUsername(etProfileUsername.Text))
        //    {
        //        Toast.MakeText(this, "Updated", ToastLength.Short).Show();
        //    }
        //    else
        //    {
        //        Toast.MakeText(this, "Faild", ToastLength.Short).Show();
        //    }
        //}

        //private async Task<bool> UpdateUsername(string UserName)
        //{
        //    try
        //    {
        //        DocumentReference UserReference = fbd.firestore.Collection(General.FS_UsersCollection).Document(uid);
        //        await UserReference.Update(General.KEY_USERNAME, UserName);
        //    }
        //    catch { return false; }
        //    return true;
        //}

        private void InitObject()
        {
            fbd = new FireBaseData();
            song = new Song();
            sp = this.GetSharedPreferences("details", Android.Content.FileCreationMode.Private);
        }
        public void GetDataFromSp()
        {
            sid = sp.GetString("sid", null);
        }

        public void OnSuccess(Java.Lang.Object result)
        {
            var snapshot = (DocumentSnapshot)result;
            song = new Song(snapshot.Id, snapshot.Get("Name").ToString(), snapshot.Get("Performer").ToString(), snapshot.Get("Level").ToString());
            PrintSong(song);
        }

        private void PrintSong(Song song)
        {
            tvSongName.Text = song.Name;
            tvSongPerformer.Text = song.Performer;
            tvSongLevel.Text = song.Level;
        }
    }
}