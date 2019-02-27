using System;
using System.Diagnostics;
using Windows.ApplicationModel.Background;
using Windows.Storage;
using Windows.System.Threading;

namespace BackgroundTask
{
    public sealed class SampleBackground : IBackgroundTask
    {
        BackgroundTaskCancellationReason _CancelReason = BackgroundTaskCancellationReason.Abort;
        volatile bool _cancelRequested = false;
        BackgroundTaskDeferral _deferral = null;
        ThreadPoolTimer _periodicTimer = null;
        uint _progress = 0;
        IBackgroundTaskInstance _taskInstance = null;
        //run方法是后台任务的入口点
        public void Run(IBackgroundTaskInstance taskInstance)
        {
            Debug.WriteLine("Background" + taskInstance.Task.Name + "Starting...");
            //查询BackgroundWorkCost.
            //如果后台工作成本很高，那么只执行后台任务中的最小工作量并立即返回。
            var cost = BackgroundWorkCost.CurrentBackgroundWorkCost;
            var settings = ApplicationData.Current.LocalSettings;
            settings.Values["BackgroundWorkCost"] = cost.ToString();
            //将取消处理程序与后台任务关联。
            taskInstance.Canceled += new BackgroundTaskCanceledEventHandler(OnCanceled);
            //从任务实例中获取deferred对象，并引用task kinstance;
            _deferral = taskInstance.GetDeferral();
            _taskInstance = taskInstance;
            _periodicTimer = ThreadPoolTimer.CreatePeriodicTimer
                (new TimerElapsedHandler(PeriodicTimerCallback), TimeSpan.FromSeconds(1));
        }
        //处理后台任务取消.
        private void OnCanceled(IBackgroundTaskInstance sender, BackgroundTaskCancellationReason reason)
        {
            //指示取消后台任务
            _cancelRequested = true;
            _CancelReason = reason;
            Debug.WriteLine("Background" + sender.Task.Name + "Cancel Requested...");
        }
        //模拟后台任务活动
        private void PeriodicTimerCallback(ThreadPoolTimer timer)
        {
            if ((_cancelRequested == false) && (_progress < 100))
            {
                _progress += 10;
                _taskInstance.Progress = _progress;
            }
            else
            {
                _periodicTimer.Cancel();
                var key = _taskInstance.Task.Name;
                //记录此后台任务已运行。
                String taskStatus = (_progress < 100) ? "Canceled with reason:" + _CancelReason.ToString() : "Completed";
                var settings = ApplicationData.Current.LocalSettings;
                settings.Values[key] = taskStatus;
                Debug.WriteLine("Background" + _taskInstance.Task.Name + settings.Values[key]);
                //指示后台任务已完成。
                _deferral.Complete();
            }
        }
    }
}
