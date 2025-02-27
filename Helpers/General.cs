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
    }
}