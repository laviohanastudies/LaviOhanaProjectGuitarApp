using Android.App;
using Android.Content;
using Android.Gms.Extensions;
using Android.Gms.Tasks;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AndroidX.Core.OS;
using Firebase.Firestore;
using LaviOhanaProjectGuitarApp.Helpers;
using LaviOhanaProjectGuitarApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LaviOhanaProjectGuitarApp.Activities
{
    [Activity(Label = "SearchSongActivity")]
    public class SearchSongActivity : Activity, IOnCompleteListener, IEventListener
    {
        RadioButton rBtnSearchByName, rBtnSearchByPerformer, rBtnSearchByLevel;
        
        RadioButton rBtnLevelBeginner, rBtnLevelMedium, rBtnLevelAdvanced;
        RadioGroup rgLevelOptions;

        Button btnEnterSearch;
        EditText etSearchSong;
        ListView lvSearchSongList;

        FireBaseData fbd;
        List<Song> lstSongs;
        SongAdapter sa;
        Android.Content.ISharedPreferences sp;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.SearchSongLayout);
            InitObject();
            InitViews();
        }

        private void InitObject()
        {
            fbd = new FireBaseData();
            fbd.AddCollectionSnapShotListener(this, General.FS_SONG_COLLECTION);
            sp = this.GetSharedPreferences("details", FileCreationMode.Private);
        }

        private void InitViews()
        {
            rBtnSearchByName = FindViewById<RadioButton>(Resource.Id.rBtnSearchByName);
            rBtnSearchByPerformer = FindViewById<RadioButton>(Resource.Id.rBtnSearchByPerformer);
            rBtnSearchByLevel = FindViewById<RadioButton>(Resource.Id.rBtnSearchByLevel);

            rBtnLevelBeginner = FindViewById<RadioButton>(Resource.Id.rBtnLevelBeginner);
            rBtnLevelMedium = FindViewById<RadioButton>(Resource.Id.rBtnLevelMedium);
            rBtnLevelAdvanced = FindViewById<RadioButton>(Resource.Id.rBtnLevelAdvanced);
            rgLevelOptions = FindViewById<RadioGroup>(Resource.Id.rgLevelOptions);

            etSearchSong = FindViewById<EditText>(Resource.Id.etSearchSong);
            btnEnterSearch = FindViewById<Button>(Resource.Id.btnEnterSearch);
            lvSearchSongList = FindViewById<ListView>(Resource.Id.lvSearchSongList);

            btnEnterSearch.Click += btnEnterSearch_Click;
            lvSearchSongList.ItemClick += lvSearchSongList_ItemClick;

            rBtnSearchByName.CheckedChange += RadioButtons_CheckedChange;
            rBtnSearchByPerformer.CheckedChange += RadioButtons_CheckedChange;
            rBtnSearchByLevel.CheckedChange += RadioButtons_CheckedChange;
        }

        private void RadioButtons_CheckedChange(object sender, CompoundButton.CheckedChangeEventArgs e)
        {
            if (rBtnSearchByLevel.Checked)
            {
                etSearchSong.Visibility = ViewStates.Gone;
                rgLevelOptions.Visibility = ViewStates.Visible;
            }
            else
            {
                etSearchSong.Visibility = ViewStates.Visible;
                rgLevelOptions.Visibility = ViewStates.Gone;
                rgLevelOptions.ClearCheck();
            }
        }

        private void btnEnterSearch_Click(object sender, EventArgs e)
        {
            if (rBtnSearchByName.Checked)
            {
                ListByName(etSearchSong.Text);
            }
            else if (rBtnSearchByPerformer.Checked)
            {
                ListByPerformer(etSearchSong.Text);
            }
            else if (rBtnSearchByLevel.Checked)
            {
                string level = string.Empty;
                if (rBtnLevelBeginner.Checked)
                    level = "Beginner";
                else if (rBtnLevelMedium.Checked)
                    level = "Medium";
                else if (rBtnLevelAdvanced.Checked)
                    level = "Advanced";

                if (level != string.Empty)
                    ListByLevel(level);
                else
                    Toast.MakeText(this, "Please select a level", ToastLength.Short).Show();
            }
        }

        private async void ListByName(string text)
        {
            await fbd.GetEqualCollection(General.KEY_SONG_NAME, text).AddOnCompleteListener(this);
        }

        private async void ListByPerformer(string text)
        {
            await fbd.GetEqualCollection(General.KEY_PERFORMER, text).AddOnCompleteListener(this);
        }

        private async void ListByLevel(string level)
        {
            await fbd.GetEqualCollection(General.KEY_LEVEL, level).AddOnCompleteListener(this);
        }

        public void OnComplete(Task task)
        {
            if (task.IsSuccessful)
            {
                lstSongs = GetDocuments2((QuerySnapshot)task.Result);
                if (lstSongs.Count != 0)
                    Toast.MakeText(this, "Found " + lstSongs.Count + " songs", ToastLength.Short).Show();
                else
                    Toast.MakeText(this, "No songs found", ToastLength.Short).Show();
            }
        }

        private List<Song> GetDocuments2(QuerySnapshot result)
        {
            lstSongs = new List<Song>();
            foreach (DocumentSnapshot item in result.Documents)
            {
                Song song = new Song
                {
                    Id = item.Id,
                    Name = item.Get(General.KEY_SONG_NAME).ToString(),
                    Performer = item.Get(General.KEY_PERFORMER).ToString(),
                    Level = item.Get(General.KEY_LEVEL).ToString(),
                };
                lstSongs.Add(song);
            }

            sa = new SongAdapter(this, lstSongs, 2);
            lvSearchSongList.Adapter = sa;
            return lstSongs;
        }

        private void lvSearchSongList_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            Song song = lstSongs[e.Position];
            PutSp(song);
            Intent intent = new Intent(this, typeof(SongProfileActivity));
            intent.PutExtra("sid", song.Id);
            StartActivity(intent);
        }

        public void PutSp(Song song)
        {
            var editor = sp.Edit();
            editor.PutString("sid", song.Id);
            editor.Commit();
        }

        public void OnEvent(Java.Lang.Object value, FirebaseFirestoreException error)
        {
            Toast.MakeText(this, "event", ToastLength.Short).Show();
        }
    }

}