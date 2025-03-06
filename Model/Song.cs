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

namespace LaviOhanaProjectGuitarApp.Model
{
    public class Song
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Performer { get; set; }
        public string Level { get; set; }
        public string ImageTab { get; set; }

        public Song(string id, string name, string performer, string level, string imageTab)
        {
            Id = id;
            Name = name;
            Performer = performer;
            Level = level;
            ImageTab = imageTab;
        }
        public Song()
        {
            Id = string.Empty;
            Name = string.Empty;
            Performer = string.Empty;
            Level = string.Empty;
            ImageTab = string.Empty;
        }
    }
}