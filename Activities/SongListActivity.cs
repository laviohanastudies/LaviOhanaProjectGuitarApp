using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Firebase.Firestore.Auth;
using Firebase.Firestore;
using LaviOhanaProjectGuitarApp.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.Gms.Tasks;
using LaviOhanaProjectGuitarApp.Model;
using Android.Gms.Extensions;

namespace LaviOhanaProjectGuitarApp.Activities
{
    [Activity(Label = "SongListActivity")]
    public class SongListActivity : Activity, IOnCompleteListener, IEventListener
    {
        ListView lvSongList;
        List<Song> lstSongs;
        SongAdapter sa;
        Button btnSongListBack;
        FireBaseData fbd;
        Android.Content.ISharedPreferences sp;


        protected override void OnCreate(Bundle savedInstanceState)
        {
            //  Toast.MakeText(this, "*****"+DocumentActivity.id.ToString(), ToastLength.Long).Show();
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.SongListLayout);
            //uid = Intent.GetStringExtra("uid");
            //Toast.MakeText(this, uid.ToString(), ToastLength.Long).Show();
            InitObjects();

            InitViews();
            GetSongListAsync();
        }

        private async void GetSongListAsync()
        {
            await fbd.GetCollection(General.FS_SONG_COLLECTION).AddOnCompleteListener(this);
        }


        //protected override void OnListItemClick(ListView l, View v, int position, long id)
        //{
        //    var t = lvUsers[position];
        //    Android.Widget.Toast.MakeText(this, t, Android.Widget.ToastLength.Short).Show();
        //}
        private void InitObjects()
        {
            //    Toast.MakeText(this, "ok", ToastLength.Long).Show();
            fbd = new FireBaseData();
            fbd.AddCollectionSnapShotListener(this, General.FS_SONG_COLLECTION);
            sp = this.GetSharedPreferences("details", Android.Content.FileCreationMode.Private);

        }

        private void InitViews()
        {

            //  Toast.MakeText(this, uid, ToastLength.Long).Show();
            lvSongList = FindViewById<ListView>(Resource.Id.lvSongList);
            lvSongList.ItemClick += lvSongList_ItemClick;
            //lvSongList.ItemLongClick += lvSongList_ItemLongClick;

        }

        private void lvSongList_ItemLongClick(object sender, AdapterView.ItemLongClickEventArgs e)
        {
            //Song song = lstSongs[e.Position];
        }

        private void lvSongList_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            Song song = lstSongs[e.Position];
            PutSp(song);
            Intent intent = new Intent(this, typeof(SongProfileActivity));
            Intent.PutExtra("sid", song.Id);
            StartActivity(intent);
        }
        public void PutSp(Song song)
        {
            var editor = sp.Edit();
            editor.PutString("sid", song.Id);
            editor.Commit();
        }

        //public async void OnClick(View v)
        //{
        //    if (v == btnLOk)
        //    {

        //        //  Toast.MakeText(this, "ok", ToastLength.Long).Show();
        //        await fbd.GetCollection().AddOnCompleteListener(this);

        //    }
        //}

        public void OnComplete(Task task)
        {
            if (task.IsSuccessful)
            {

                // Toast.MakeText(this, "ok", ToastLength.Long).Show();
                lstSongs = GetDocuments2((QuerySnapshot)task.Result);
                if (lstSongs.Count != 0)
                {


                    Toast.MakeText(this, "ok", ToastLength.Long).Show();

                }
                else
                    Toast.MakeText(this, "empty", ToastLength.Long).Show();

            }
        }

        private List<Song> GetDocuments2(QuerySnapshot result)
        {
            lstSongs = new List<Song>();

            Toast.MakeText(this, "okgetdocument", ToastLength.Long).Show();
            foreach (DocumentSnapshot item in result.Documents)
            {
                Song song = new Song
                {
                    Id = item.Id,
                    Name = item.Get(General.KEY_SONG_NAME).ToString(),
                    Performer = item.Get(General.KEY_PERFORMER).ToString(),
                    Level = item.Get(General.KEY_LEVEL).ToString(),
                    ImageTab = string.Empty,
                };
                lstSongs.Add(song);
            }
            sa = new SongAdapter(this, lstSongs, 1);
            lvSongList.Adapter = sa;
            return lstSongs;
        }




        public void OnEvent(Java.Lang.Object value, FirebaseFirestoreException error)
        {
            Toast.MakeText(this, "event", ToastLength.Long).Show();
        }
    }
}
