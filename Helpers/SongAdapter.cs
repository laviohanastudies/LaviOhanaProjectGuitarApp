using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using LaviOhanaProjectGuitarApp.Activities;
using LaviOhanaProjectGuitarApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LaviOhanaProjectGuitarApp.Helpers
{
    internal class SongAdapter : BaseAdapter<Song>
    {

        Context context;
        private List<Song> songList;
        private int activityNumber;

        public SongAdapter(Context context)
        {
            this.context = context;
        }


        public SongAdapter(Context context, List<Song> SongList, int activityNumber)
        {
            this.songList = SongList;
            this.context = context;
            this.activityNumber = activityNumber;
        }

        public override Java.Lang.Object GetItem(int position)
        {
            return position;
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            LayoutInflater LayoutInflater;
            if (activityNumber==1)
            {
                LayoutInflater = ((SongListActivity)context).LayoutInflater;
            }
            else
            {
                LayoutInflater = ((SearchSongActivity)context).LayoutInflater;
            }
            View view = LayoutInflater.Inflate(Resource.Layout.SongListRowLayout, parent, false);

            TextView tvSongListRowName = view.FindViewById<TextView>(Resource.Id.tvSongListRowName);
            TextView tvSongListRowPerformer = view.FindViewById<TextView>(Resource.Id.tvSongListRowPerformer);
            TextView tvSongListRowLevel = view.FindViewById<TextView>(Resource.Id.tvSongListRowLevel);
            Song song = songList[position];
            if (song != null)
            {
                tvSongListRowName.Text = tvSongListRowName.Text + song.Name;
                tvSongListRowPerformer.Text = tvSongListRowPerformer.Text + song.Performer;
                tvSongListRowLevel.Text = tvSongListRowLevel.Text + song.Level;
            }
            return view;
        }

        //Fill in cound here, currently 0
        public override int Count
        {
            get
            {
                return songList.Count;
            }
        }

        public override Song this[int position]
        {
            get
            {
                return songList[position];
            }
        }
    }

}
