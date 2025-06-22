using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace LaviOhanaProjectGuitarApp.Helpers
{
    internal class General
    {
        public const string FS_UsersCollection = "UsersCollection"; // fs - firestore
        public const string KEY_ID = "Id";
        public const string KEY_USERNAME = "Username";
        public const string KEY_EMAIL = "Email";
        public const string KEY_PASSWORD = "Password";
        public const string KEY_LEVEL = "Level";

        public const string FS_SONG_COLLECTION = "SongCollection";
        public const string KEY_SONG_NAME = "Name";
        public const string KEY_PERFORMER = "Performer";
        public const string KEY_IMAGE_TAB = "ImageTab";
        
        public const int REQUEST_OPEN_CAMERA = 1;
        public const string KEY_CAMERA_IMAGE = "data";
        public const string KEY_UPLOAD_IMAGE = "Udata";


        public static byte[] BitmapToByteArray(Bitmap bitmap) // מקבל תמונה וממיר אותה לביטים
        {
            MemoryStream ms = new MemoryStream();
            bitmap.Compress(Bitmap.CompressFormat.Png, 100, ms);
            return ms.ToArray();
        }
        public static Bitmap ByteArrayToBitmap(byte[] bytes) // מקבל מערך של ביטים וממיר לתמונה
        {
            return BitmapFactory.DecodeByteArray(bytes, 0, bytes.Length);
        }
        
        public static string ConvertImageToBase64(Bitmap bitmap)
        {//**1
            using (MemoryStream stream = new MemoryStream())
            {
                bitmap.Compress(Bitmap.CompressFormat.Png, 100, stream);
                byte[] byteArray = stream.ToArray();
                return Convert.ToBase64String(byteArray);
            }
        }

        public static Bitmap ConvertBase64ToImage(string base64String)
        {//**2
            byte[] byteArray = Convert.FromBase64String(base64String);
            return BitmapFactory.DecodeByteArray(byteArray, 0, byteArray.Length);
        }
    }
}