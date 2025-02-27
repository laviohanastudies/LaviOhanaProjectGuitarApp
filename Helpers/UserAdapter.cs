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
    internal class UserAdapter : BaseAdapter<RegisterUser>
    {

        Context context;
        private List<RegisterUser> UserList;

        public UserAdapter(Context context)
        {
            this.context = context;
        }

        public UserAdapter(Context context, List<RegisterUser> UserList)
        {
            this.UserList = UserList;
            this.context = context;
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
            LayoutInflater = ((UserListActivity)context).LayoutInflater;
            View view = LayoutInflater.Inflate(Resource.Layout.UserListRowLayout, parent, false);
            
            TextView UserListRowName = view.FindViewById<TextView>(Resource.Id.UserListRowNameTv);
            TextView UserListRowEmail = view.FindViewById<TextView>(Resource.Id.UserListRowEmailTv);
            TextView UserListRowPassword = view.FindViewById<TextView>(Resource.Id.UserListRowPasswordTv);
            RegisterUser user = UserList[position];
            if (user != null)
            {
                UserListRowName.Text = UserListRowName.Text + user.Userame;
                UserListRowEmail.Text = UserListRowEmail.Text + user.Email;
                UserListRowPassword.Text = UserListRowPassword.Text + user.Password;
            }
            return view;
        }

        //Fill in cound here, currently 0
        public override int Count
        {
            get
            {
                return UserList.Count;
            }
        }

        public override RegisterUser this[int position]
        {
            get
            {
                return UserList[position];
            }
        }
    }

    internal class ClientAdapterViewHolder : Java.Lang.Object
    {
        //Your adapter views to re-use
        //public TextView Title { get; set; }
    }
}