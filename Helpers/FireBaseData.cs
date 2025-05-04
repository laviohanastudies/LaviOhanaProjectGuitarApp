using Android.App;
using Android.Content;
using Android.Gms.Extensions;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Firebase;
using Firebase.Auth;
using Firebase.Firestore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaviOhanaProjectGuitarApp.Helpers
{
    internal class FireBaseData
	{
        public readonly FirebaseAuth auth;
        public readonly FirebaseApp app;
        public FirebaseFirestore firestore;

        public FireBaseData()
        {
            app = FirebaseApp.InitializeApp(Application.Context);
            if(app is null)
            {
                FirebaseOptions options = GetMyOptions();
                app = FirebaseApp.InitializeApp(Application.Context, options);
            }
            firestore = FirebaseFirestore.GetInstance(app);
            auth = FirebaseAuth.Instance;
        }
        private FirebaseOptions GetMyOptions()
        {
            return new FirebaseOptions.Builder().SetProjectId("laviohanaprojectguitarapp")
                .SetApplicationId("laviohanaprojectguitarapp")
                .SetApiKey("AIzaSyCHuIkrnABHTF6JTQS3chlcF9bwhu30uUE")
                .SetStorageBucket("laviohanaprojectguitarapp.firebasestorage.app").Build();
                
        }
        public async Task CreateUser(string email, string password) ///ggygy
        {
            await auth.CreateUserWithEmailAndPassword(email, password);
        }
        public async Task SingIn(string email, string password)
        {
            await auth.SignInWithEmailAndPassword(email, password);
        }
      
        public string GetNewDocumentId(string cName)
        {
            DocumentReference dr = firestore.Collection(cName).Document();
            return dr.Id;
        }

        public Android.Gms.Tasks.Task DeleteFsDocument(string cName, string id)//asyncron
        {
            return firestore.Collection(cName).Document(id).Delete();
        }

        public Android.Gms.Tasks.Task GetCollection(string CollectionName)
        {
            return firestore.Collection(CollectionName).Get();
        }
        public void AddCollectionSnapShotListener(Activity activity, string cName)
        {
            firestore.Collection(cName).AddSnapshotListener((IEventListener)activity);
        }

        public Android.Gms.Tasks.Task GetCollection(string cName, string id)
        {
            return firestore.Collection(cName).Document(id).Get();
        }
    }
}