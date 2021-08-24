using System;
using Android.Content;
using Android.Widget;

namespace Foreground.Droid.Jobs
{

    [BroadcastReceiver]
    public class AlarmReceiver : BroadcastReceiver
    {
        public override void OnReceive(Context context, Intent intent)
        {
            Toast.MakeText(context, "Serviçorodando", ToastLength.Long);
        }
    }
}
