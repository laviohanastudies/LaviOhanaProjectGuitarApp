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
    public class UploadSongActivity : Activity ,Android.Views.View.IOnClickListener
    {
        EditText etUploadSongName, etUploadSongPerformer;
        string uploadSongLevel;

        RadioButton rBtnUploadSongLevelBeginner, rBtnUploadSongLevelMedium, rBtnUploadSongLevelAdvanced;
        RadioGroup rgUploadSongLevel;

        ImageView ivUploadSongImageTab;
        Button btnUploadImageFromGallery, btnUploadImageFromCamera, btnUploadSong;

        FireBaseData fbd;
        Song song;
        HashMap songMap;
        string song_id, image;
        Bitmap bitmap;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.UploadSongLayout);
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

            rgUploadSongLevel = FindViewById<RadioGroup>(Resource.Id.rgUploadSongLevel);
            rBtnUploadSongLevelBeginner = FindViewById<RadioButton>(Resource.Id.rBtnUploadSongLevelBeginner);
            rBtnUploadSongLevelMedium = FindViewById<RadioButton>(Resource.Id.rBtnUploadSongLevelMedium);
            rBtnUploadSongLevelAdvanced = FindViewById<RadioButton>(Resource.Id.rBtnUploadSongLevelAdvanced);

            ivUploadSongImageTab = FindViewById<ImageView>(Resource.Id.ivUploadSongImageTab);
            btnUploadImageFromGallery = FindViewById<Button>(Resource.Id.btnUploadImageFromGallery);
            btnUploadImageFromCamera = FindViewById<Button>(Resource.Id.btnUploadImageFromCamera);
            btnUploadSong = FindViewById<Button>(Resource.Id.btnUploadSong);

            btnUploadImageFromGallery.SetOnClickListener(this);
            btnUploadImageFromCamera.SetOnClickListener(this);
            btnUploadSong.SetOnClickListener(this);
            //btnUploadSongImageTab.Click += btnUploadSongImageTab_Click;
            //btnUploadSong.Click += btnUploadSong_Click;

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
            if (requestCode == General.REQUEST_OPEN_CAMERA) // מצלמה
            {
                if (resultCode == Result.Ok)
                {
                    bitmap = (Android.Graphics.Bitmap)data.Extras.Get(General.KEY_CAMERA_IMAGE);
                    ivUploadSongImageTab.SetImageBitmap(bitmap);
                    image = General.ConvertImageToBase64(bitmap);
                }
            }
            else if(requestCode == 0) // גלריה
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
            if (etUploadSongName.Text!=string.Empty && etUploadSongPerformer.Text!= string.Empty && uploadSongLevel!=string.Empty) // להוסיף לתמונה בדיקת קלט
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
            if (rBtnUploadSongLevelBeginner.Checked)
            {
                uploadSongLevel = rBtnUploadSongLevelBeginner.Text;
            }
            else if (rBtnUploadSongLevelMedium.Checked)
            {
                uploadSongLevel = rBtnUploadSongLevelMedium.Text;
            }
            else
            {
                uploadSongLevel = rBtnUploadSongLevelAdvanced.Text;
            }


            if (await UploadSong(etUploadSongName, etUploadSongPerformer, uploadSongLevel, image))
            {
                Toast.MakeText(this, "Uploaded Song Successfully", ToastLength.Short).Show();
                etUploadSongName.Text = string.Empty;
                etUploadSongPerformer.Text = string.Empty;
                rgUploadSongLevel.ClearCheck();
            }
            else
            {
                Toast.MakeText(this, "Upload Song Failed", ToastLength.Short).Show();
            }
        }

        private async Task<bool> UploadSong(EditText etUploadSongName, EditText etUploadSongPerformer, string uploadSongLevel, string image)
        {
            try
            {
                song_id = fbd.GetNewDocumentId(General.FS_SONG_COLLECTION);
                songMap = new HashMap();
                songMap.Put(General.KEY_ID, song_id);
                songMap.Put(General.KEY_SONG_NAME, etUploadSongName.Text);
                songMap.Put(General.KEY_PERFORMER, etUploadSongPerformer.Text);
                songMap.Put(General.KEY_LEVEL, uploadSongLevel);
                songMap.Put(General.KEY_IMAGE_TAB, image);
                DocumentReference songReference = fbd.firestore.Collection(General.FS_SONG_COLLECTION).Document(song_id);
                await songReference.Set(songMap);
            }
            catch (Exception ex)
            {

            }
            return true;
        }

        public void OnClick(View v)
        {
            if(v== btnUploadImageFromGallery)
            {
                Intent = new Intent(Intent.ActionPick, MediaStore.Images.Media.InternalContentUri);
                Intent.SetType("image/*");
                StartActivityForResult(Intent.CreateChooser(Intent, "SelectPicture"), 0);
            }
            if(v== btnUploadImageFromCamera)
            {
                Intent intent = new Intent(Android.Provider.MediaStore.ActionImageCapture);
                StartActivityForResult(intent, General.REQUEST_OPEN_CAMERA);
            }
            if(v== btnUploadSong)
            {
                SaveDocument();
            }
        }
    }
}