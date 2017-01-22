using System;
using Android.App;
using Android.Content;
using Android.Database;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Spinner_SQLite.mCode.mDataBase;
using Spinner_SQLite.mCode.mDataObject;

namespace Spinner_SQLite
{
    [Activity(Label = "Spinner_SQLite", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {

        private Spinner sp;
        private EditText nameEditText;
        private Button saveBtn, retrieveBtn;
        JavaList<string> tvShows=new JavaList<string>();
        private ArrayAdapter adapter;
       
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            this.InitializeUI();

            //ADAPTER
            adapter=new ArrayAdapter(this,Android.Resource.Layout.SimpleListItem1,tvShows);
            sp.Adapter = adapter;

            //EVENTS
            saveBtn.Click += saveBtn_Click;
            retrieveBtn.Click += retrieveBtn_Click;
            sp.ItemSelected += sp_ItemSelected;

        }

        void sp_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            Toast.MakeText(this,tvShows[e.Position],ToastLength.Short).Show();
        }

        void retrieveBtn_Click(object sender, EventArgs e)
        {
            Retrieve();
        }

        void saveBtn_Click(object sender, EventArgs e)
        {
            Save(nameEditText.Text);
        }

        //INITIALIZE UI STUFF
        private void InitializeUI()
        {
            sp = FindViewById<Spinner>(Resource.Id.sp);
            nameEditText = FindViewById<EditText>(Resource.Id.nameEditTxt);
            saveBtn = FindViewById<Button>(Resource.Id.saveBtn);
            retrieveBtn = FindViewById<Button>(Resource.Id.retrieveBtn);

        }

        //SAVE
        private void Save(String name)
        {
            DBAdapter db=new DBAdapter(this);
            db.OpenDB();
            bool saved = db.Add(name);
            db.CloseDB();

            if (saved)
            {
                nameEditText.Text = "";
            }
            else
            {
                Toast.MakeText(this,"Unable To Save",ToastLength.Short).Show();
            }
            //refresh
            this.Retrieve();

        }


        //RETRIEVE
        private void Retrieve()
        {
            tvShows.Clear();
            DBAdapter db = new DBAdapter(this);
            db.OpenDB();
            ICursor c = db.Retrieve();

            if (c != null)
            {
                while (c.MoveToNext())
                {
                    int id = c.GetInt(0);
                    string name = c.GetString(1);

                    tvShows.Add(name);
                }   
            }
            else
            {
                Console.Write("Null Retrieved");
            }
            db.CloseDB();

            sp.Adapter = adapter;
        }
       

    }
}

