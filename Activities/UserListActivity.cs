using Android.App;
using Android.Content;
using Android.Gms.Extensions;
using Android.Gms.Tasks;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using LaviOhanaProjectGuitarApp.Helpers;
using LaviOhanaProjectGuitarApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;

namespace LaviOhanaProjectGuitarApp.Activities
{
    [Activity(Label = "UserListActivity")]
    public class UserListActivity : Activity,IOnCompleteListener
    {
        ListView UserListLv;
        List<RegisterUser> UserList;
        UserAdapter ua;
        //Button btnBck;
        FireBaseData fbd;
        string uid;
        string result;


        public void OnComplete(Task task)
        {
            throw new NotImplementedException();
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.UserListLayout);
            InitObjects();
            IniteViews();
            GetList();

        }

        private async void GetList()
        {
            Toast.MakeText(this, "GettingList", ToastLength.Short).Show();
            //await fbd.GetCollaction
        }

        private void IniteViews()
        {
            //UserListLv = FindViewById<ListView>(Resource.Id.UserListLv);
            UserListLv.ItemLongClick += UserListLv_ItemLongClick;
        }

        private async void UserListLv_ItemLongClick(object sender, AdapterView.ItemLongClickEventArgs e)
        {
            RegisterUser user = UserList[e.Position];
            if (await DeleteUserAsync(user.Id))
            {
                Toast.MakeText(this, "Deleted Successfuly", ToastLength.Short).Show();
            }
            else
            {
                Toast.MakeText(this, "Delete Failed", ToastLength.Short).Show();
            }
        }

        private async System.Threading.Tasks.Task<bool> DeleteUserAsync(string id)
        {
            try
            {
                await fbd.DeleteFsDocument(General.FS_UsersCollection,id);
            }
            catch(System.Exception ex)
            {
                string s = ex.Message;
                return false;
            }
            return true;
        }

        private void InitObjects()
        {
            throw new NotImplementedException();
        }
    }
}