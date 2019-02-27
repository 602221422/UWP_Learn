using System;
using System.Diagnostics;
using Windows.ApplicationModel.Background;
using Windows.Storage;
using Windows.System.Threading;

namespace BackgroundTask
{
    //后台任务实现IBackgroundTask接口。
    public sealed class ServicingComplete:IBackgroundTask
    {
        volatile bool _cancelRequested = false;
        //Run方法是后台任务的入口点。
        public void Run(IBackgroundTaskInstance taskInstance)
        {
            Debug.WriteLine("ServicingComplete" + taskInstance.Task.Name + "starting...");
            //将取消处理程序与后台任务关联。
            taskInstance.Canceled += new BackgroundTaskCanceledEventHandler(OnCanceled);
            //完成后台服务工作。
            uint Progress;
            for(Progress = 0;Progress <= 100; Progress += 10)
            {
                //如果取消处理程序指示任务已被取消，则停止执行该任务。
                if (_cancelRequested)
                {
                    break;
                }
                //指示前台应用程序的进度。
                taskInstance.Progress = Progress;
            }
            var settings = ApplicationData.Current.LocalSettings;
            var key = taskInstance.Task.Name;
            //写入LocalSettings以指示此后台任务已运行。
            settings.Values[key] = (Progress < 100) ? "Canceled" : "Completed";
            Debug.WriteLine("ServicingComplete" + taskInstance.Task.Name 
                + ((Progress < 100) ? "Canceled" : "Completed"));
        }
        //处理后台任务取消。
        private void OnCanceled(IBackgroundTaskInstance sender,BackgroundTaskCancellationReason reason)
        {
            //指示取消后台任务。
            _cancelRequested = true;
            Debug.WriteLine("ServicingComplete" + sender.Task.Name + "Cancel Requested...");
        }
    }
}
