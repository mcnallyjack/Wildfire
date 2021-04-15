using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wildfire.Services;
using Xamarin.Forms;
using Firebase.Auth;
using Wildfire.Droid;
using Android.Media;
using Android.Support.V4.App;


[assembly: Dependency(typeof(NotificationHelper))]
namespace Wildfire.Droid
{
    class NotificationHelper : INotification
    {
        private Context mContext;
        //private NotificationManager mNotificationManager;
        private NotificationCompat.Builder mBuilder;
        public static String NOTIFICATION_CHANNEL_ID = "10023";

        public NotificationHelper()
        {
            mContext = global::Android.App.Application.Context;
        }

        [Obsolete]
        public void CreateNotification(String title, String message)
        {
            try
            {
                var intent = new Intent(this.mContext, typeof(MainActivity));
                intent.AddFlags(ActivityFlags.SingleTop);
                intent.AddFlags(ActivityFlags.NewTask);

                var pendingIntent = PendingIntent.GetActivity(this.mContext, 0, intent, PendingIntentFlags.OneShot);

                mBuilder = new NotificationCompat.Builder(mContext);
                mBuilder.SetSmallIcon(Resource.Drawable.common_google_signin_btn_icon_dark_normal);
                mBuilder.SetContentTitle(title)
                    .SetAutoCancel(true)
                    .SetContentTitle(title)
                    .SetContentText(message)
                    .SetChannelId(NOTIFICATION_CHANNEL_ID)
                    .SetPriority((int)NotificationPriority.High)
                    .SetVibrate(new long[0])
                    .SetVisibility((int)NotificationVisibility.Public)
                    .SetSmallIcon(Resource.Drawable.common_google_signin_btn_icon_dark_normal)
                    .SetContentIntent(pendingIntent);

                NotificationManager notificationManager = mContext.GetSystemService(Context.NotificationService) as NotificationManager;
                if (global::Android.OS.Build.VERSION.SdkInt >= global::Android.OS.BuildVersionCodes.O){
                    NotificationImportance importance = global::Android.App.NotificationImportance.High;

                    NotificationChannel notificationChannel = new NotificationChannel(NOTIFICATION_CHANNEL_ID, title, importance);
                    notificationChannel.EnableLights(true);
                    notificationChannel.EnableVibration(true);
                    notificationChannel.SetShowBadge(true);
                    notificationChannel.Importance = NotificationImportance.High;
                    notificationChannel.SetVibrationPattern(new long[] { 100, 200, 300, 400, 500, 400, 300, 200, 400 });

                    if (notificationManager != null)
                    {
                        mBuilder.SetChannelId(NOTIFICATION_CHANNEL_ID);
                        notificationManager.CreateNotificationChannel(notificationChannel);
                    }
                }
                notificationManager.Notify(0, mBuilder.Build());
            }
            catch(Exception ex)
            {
                ex.ToString();
            }
        }
    }
}