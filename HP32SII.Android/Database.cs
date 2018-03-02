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

[assembly: Xamarin.Forms.Dependency(typeof(HP32SII.Droid.Database))]
namespace HP32SII.Droid
{
    class Database : IDatabase
    {
        public Database()
        {

        }

        public void Store(string key, double value)
        {
            throw new NotImplementedException();
        }

        public double Recall(string key)
        {
            throw new NotImplementedException();
        }

        public void ClearAll()
        {
            throw new NotImplementedException();
        }
    }
}