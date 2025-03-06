using Android.App;
using Android.Content;
using Android.Gms.Extensions;
using Android.Graphics;
using Android.OS;
using Android.Provider;
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
    [Activity(Label = "UploadSong_Activity")]
    public class UploadSongActivity : Activity
    {
        EditText etUploadSongName, etUploadSongPerformer, etUploadSongLevel;
        ImageView ivUploadSongImageTab;
        Button btnUploadSongImageTab, btnUploadSong;
        FireBaseData fbd;
        Song song;
        HashMap songMap;
        string song_id, image;
        Bitmap bitmap;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.UploadSong_Layout);
            InitObject();
            // Create your application here
            InitViews();
        }

        private void InitObject()
        {
            fbd = new FireBaseData();
            song = new Song();
        }

        private void InitViews()
        {
            etUploadSongName = FindViewById<EditText>(Resource.Id.etUploadSongName);
            etUploadSongPerformer = FindViewById<EditText>(Resource.Id.etUploadSongPerformer);
            etUploadSongLevel = FindViewById<EditText>(Resource.Id.etUploadSongLevel);
            ivUploadSongImageTab = FindViewById<ImageView>(Resource.Id.ivUploadSongImageTab);
            btnUploadSongImageTab = FindViewById<Button>(Resource.Id.btnUploadSongImageTab);
            btnUploadSong = FindViewById<Button>(Resource.Id.btnUploadSong);
            btnUploadSongImageTab.Click += btnUploadSongImageTab_Click;
            btnUploadSong.Click += btnUploadSong_Click;
        }

        private void btnUploadSongImageTab_Click(object sender, EventArgs e)
        {
            Intent = new Intent(Intent.ActionPick, MediaStore.Images.Media.InternalContentUri);
            Intent.SetType("image/*");
            StartActivityForResult(Intent.CreateChooser(Intent, "SelectPicture"), 0);
        }
        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            if (requestCode == 0)
            {
                if (resultCode == Result.Ok)
                {

                    System.IO.Stream stream = ContentResolver.OpenInputStream(data.Data);
                    bitmap = BitmapFactory.DecodeStream(stream);
                    ivUploadSongImageTab.SetImageBitmap(bitmap);
                    image = General.ConvertImageToBase64(bitmap);
                }

            }
        }

        private void btnUploadSong_Click(object sender, EventArgs e)
        {
            if (0 == 0) // CheckInput בדיקת קלט
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
            if (await UploadSong(etUploadSongName, etUploadSongPerformer, etUploadSongLevel))
            {
                Toast.MakeText(this, "Uploaded Song Successfully", ToastLength.Short).Show();
                etUploadSongName.Text = string.Empty;
                etUploadSongPerformer.Text = string.Empty;
                etUploadSongLevel.Text = string.Empty;
            }
            else
            {
                Toast.MakeText(this, "Upload Song Failed", ToastLength.Short).Show();
            }
        }

        private async Task<bool> UploadSong(EditText etUploadSongName, EditText etUploadSongPerformer, EditText etUploadSongLevel)
        {
            try
            {
                song_id = fbd.GetNewDocumentId(General.FS_SONG_COLLECTION);
                songMap = new HashMap();
                songMap.Put(General.KEY_ID, song_id);
                songMap.Put(General.KEY_SONG_NAME, etUploadSongName.Text);
                songMap.Put(General.KEY_PERFORMER, etUploadSongPerformer.Text);
                songMap.Put(General.KEY_LEVEL, etUploadSongLevel.Text);
                songMap.Put(General.KEY_IMAGE_TAB, image);
                DocumentReference songReference = fbd.firestore.Collection(General.FS_SONG_COLLECTION).Document(song_id);
                await songReference.Set(songMap);
            }
            catch (Exception ex)
            {

            }
            return true;
        }
    }
}