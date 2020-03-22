using System.Threading.Tasks;
using Android.App;
using Android.App.Job;
using Android.Content;
using StayAtHoome.Background;

namespace StayAtHoome.Droid
{
[Service(Name = "StayAtHoome.Android.DownloadJob",
         Permission = "android.permission.BIND_JOB_SERVICE")]
public class PeriodicLocationTrackerJobService : JobService
{
    public override bool OnStartJob(JobParameters @params)
    {
        new PeriodicLocationTracker().Execute().ContinueWith(t => {
            JobFinished(@params, !t.Result);
        });

        return true;
    }

    public override bool OnStopJob(JobParameters @params)
    {
        throw new System.NotImplementedException();
    }
}

public static class JobSchedulerHelpers
{
    public static JobInfo.Builder CreateJobBuilderUsingJobId<T>(this Context context, int jobId)
    where T : JobService
    {
        var javaClass = Java.Lang.Class.FromType(typeof(T));
        var componentName = new ComponentName(context, javaClass);
        return new JobInfo.Builder(jobId, componentName);
    }

    public static void SchedulePeriodicTrackerJob(this Context context)
    {
        var builder = context.CreateJobBuilderUsingJobId<PeriodicLocationTrackerJobService>(1)
                      .SetPersisted(false)
                      .SetPeriodic(60_000);

        var scheduler = (JobScheduler) context.GetSystemService(Context.JobSchedulerService);
        scheduler.Schedule(builder.Build());
    }
}
}