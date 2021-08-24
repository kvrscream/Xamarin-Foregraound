using System;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Widget;
using Xamarin.Forms;
using Android.Content;
using Java.Lang;
using AndroidX.Core.App;

namespace Foreground.Droid.Jobs
{

    [Service]
    public class TimeService : Service
    {


        public override IBinder OnBind(Intent intent)
        {
            return null;
        }

        public override void OnCreate()
        {
            base.OnCreate();

            Console.WriteLine("Começou a rodar");
        }

        [return: GeneratedEnum]
        public override StartCommandResult OnStartCommand(Intent intent, [GeneratedEnum] StartCommandFlags flags, int startId)
        {
            registerNotificationChannel();
            Alarme();

            int notifyId = (int)JavaSystem.CurrentTimeMillis();
            NotificationCompat.Builder mBuilder = new NotificationCompat.Builder(this, "103");
            mBuilder.SetSmallIcon(Resource.Mipmap.icon);
            if (Build.VERSION.SdkInt < BuildVersionCodes.N)
            {
                mBuilder.SetContentTitle("app name");
            }

            
            StartForeground(notifyId, mBuilder.Build());

            return StartCommandResult.Sticky;
        }

        public override void OnDestroy()
        {
            base.OnDestroy();

            StopForeground(true);
        }

        public void Alarme()
        {
            Task.Run(() =>
            {
                Intent alarm = new Intent(Forms.Context, typeof(AlarmReceiver));

                PendingIntent pendingIntent = PendingIntent.GetBroadcast(Forms.Context, 0, alarm, PendingIntentFlags.UpdateCurrent);
                AlarmManager manager = (AlarmManager)Forms.Context.GetSystemService(Context.AlarmService);
                manager.Set(AlarmType.RtcWakeup, SystemClock.ElapsedRealtime() + 1 * 1000, pendingIntent);
            });
        }


        private void registerNotificationChannel()
        {
            if (Build.VERSION.SdkInt >= BuildVersionCodes.O)
            {
                NotificationManager mNotificationManager = (NotificationManager)GetSystemService(Context.NotificationService);
                NotificationChannel notificationChannel = mNotificationManager.GetNotificationChannel("103");
                if (notificationChannel == null)
                {
                    NotificationChannel channel = new NotificationChannel("103",
                            "Name", NotificationImportance.High);
                    channel.EnableLights(true);
                    channel.LockscreenVisibility = NotificationVisibility.Public;
                    mNotificationManager.CreateNotificationChannel(channel);
                }
            }
        }


    }
}
