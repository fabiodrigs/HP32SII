using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

using HP32SII.Logic;
using System.IO;
using SQLite;

[assembly: Xamarin.Forms.Dependency(typeof(HP32SII.Droid.Database))]
namespace HP32SII.Droid
{
    class Database : IDatabase
    {
        private const double DefaultValue = 0.0;
        private const string Filename = "HP32SII.db";
        private SQLiteConnection connection;

        public Database()
        {
            string path = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), Filename);
            connection = new SQLiteConnection(path);
            connection.CreateTable<Item>();
        }

        public void Store(string key, double value)
        {
            var item = new Item
            {
                Key = key,
                Value = value,
            };

            connection.InsertOrReplace(item);
        }

        public double Recall(string key)
        {
            var items = connection.Query<Item>("SELECT * FROM Items WHERE Key=?", key);
            return items.Count > 0 ? items.First<Item>().Value : DefaultValue;
        }

        public void ClearAll()
        {
            connection.DeleteAll<Item>();
        }
    }
}