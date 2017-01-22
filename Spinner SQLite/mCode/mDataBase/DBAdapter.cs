using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Database;
using Android.Database.Sqlite;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace Spinner_SQLite.mCode.mDataBase
{
    class DBAdapter
    {
        private Context c;
        private SQLiteDatabase db;
        private DBHelper helper;

        public DBAdapter(Context c)
        {
            this.c = c;
            helper=new DBHelper(c);
        }

        //OPEN CONNECTION
        public void OpenDB()
        {
            try
            {
                db = helper.WritableDatabase;
            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message);
            }
        }

        //CLOSE DB
        public void CloseDB()
        {
            try
            {
                helper.Close();
            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message);
            }
        }

        //INSERT/ADD
        public bool Add(String name)
        {
            try
            {
                ContentValues cv=new ContentValues();
                cv.Put(Constants.NAME,name);

                db.Insert(Constants.TB_NAME, Constants.ROW_ID, cv);

                return true;
            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message);
            }

            return false;
        }

        //SELECT/RETRIEVE
        public ICursor Retrieve()
        {
            String[] columns = {Constants.ROW_ID, Constants.NAME};

            return db.Query(Constants.TB_NAME, columns, null, null, null, null, null);
        }

    }
}