using Android;
using Android.App;
using Android.Content;

[assembly:UsesPermission(
              Manifest.Permission.ReceiveBootCompleted)] namespace StayAtHoome
    .Droid {

  [BroadcastReceiver(Enabled = true, Exported = true, DirectBootAware = true)]
  [IntentFilter(new[]{
      Intent.ActionLockedBootCompleted})] public class BootedBroadcastReceiver
      : BroadcastReceiver {
    public override void OnReceive(Context context, Intent intent) {
      context.SchedulePeriodicTrackerJob();
    }
  }
}