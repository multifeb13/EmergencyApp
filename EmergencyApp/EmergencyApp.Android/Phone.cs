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

using Xamarin.Forms;

[assembly: Xamarin.Forms.Dependency(typeof(EmergencyApp.Droid.Phone))]

namespace EmergencyApp.Droid
{
    [Activity(Label ="MainActivity")]
    class Phone:Activity,IPhone
    {
        public Phone()
        {

        }

        public void Call(string name, string number)
        {
            var uri = Android.Net.Uri.Parse( "tel:" + number.Trim( ) );
            var intent = new Intent( Intent.ActionCall, uri );
            Forms.Context.StartActivity( intent );
        }
    }
}