// https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x804 上介绍了“空白页”项模板
using System;
using System.Diagnostics;
using Windows.ApplicationModel.Background;
using Windows.Storage;
using Windows.System.Threading;
using Windows.UI.Core;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace BackgroundTask_2_26
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class MainPage :Page
    {
        public static bool SampleBackgroundTaskRegistered = false;
        public static string SampleBackgroundTaskProgress = "";
        public const string SampleBackgroundTaskName = "SampleBackgroundTask";
        public const string SampleBackgroundTaskEntryPoint = "Tasks.SampleBackgroundTask";
        public MainPage()
        {
            this.InitializeComponent();
            foreach (var task in BackgroundTaskRegistration.AllTasks)
            {
                if (task.Value.Name == SampleBackgroundTaskName)
                {
                    AttachProgressAndCompletedHandlers(task.Value);
                    SampleBackgroundTaskRegistered = true;
                    break;
                }
            }
        }
        private void RegisterButton_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            var task = RegisterBackgroundTask(SampleBackgroundTaskEntryPoint, SampleBackgroundTaskName,
                new SystemTrigger(SystemTriggerType.TimeZoneChange, false), null);
            AttachProgressAndCompletedHandlers(task);
            UpdateUI();

        }

        private void UnregisterButton_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {

        }
        private void AttachProgressAndCompletedHandlers(IBackgroundTaskRegistration task)
        {
            task.Progress += new BackgroundTaskProgressEventHandler(OnProgress);
            task.Completed += new BackgroundTaskCompletedEventHandler(OnCompleted);
        }
        private void OnCompleted(IBackgroundTaskRegistration task, BackgroundTaskCompletedEventArgs args)
        {
            UpdateUI();
        }
        private void OnProgress(IBackgroundTaskRegistration task,BackgroundTaskProgressEventArgs args)
        {
            var ignored = Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
               {
                   var progress = "Progress:" + args.Progress + "%";
                   SampleBackgroundTaskProgress = progress;
                   UpdateUI();
               });
        }
        private async void UpdateUI()
        {
            await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
             {
                 RegisterButton.IsEnabled = !SampleBackgroundTaskRegistered;
                 UnregisterButton.IsEnabled = SampleBackgroundTaskRegistered;
                 Progress.Text = SampleBackgroundTaskProgress;
                 var registered = false;
                 registered = SampleBackgroundTaskRegistered;
                 var status = registered ? "Registered" : "Unregistered";
                 object taskStatus;
                 var settings = ApplicationData.Current.LocalSettings;
                 if (settings.Values.TryGetValue(SampleBackgroundTaskName, out taskStatus))
                 {
                     status += " - " + taskStatus.ToString();
                 }
                 Status.Text = status;
             });
        }
        public static BackgroundTaskRegistration RegisterBackgroundTask(String taskEntryPoint,String name, IBackgroundTrigger trigger ,IBackgroundCondition condition,BackgroundTaskRegistrationGroup group = null)
        {
            var builder = new BackgroundTaskBuilder();
            builder.Name = name;
            builder.TaskEntryPoint = taskEntryPoint;
            builder.SetTrigger(trigger);
            if (condition != null)
            {
                builder.AddCondition(condition);
                //如果在后台任务执行时条件发生了变化，那么它将被取消。
                builder.CancelOnConditionLoss = true;
            }
            if (group != null)
            {
                builder.TaskGroup = group;
            }
            BackgroundTaskRegistration task = builder.Register();
            SampleBackgroundTaskRegistered = true;
            //删除以前的完成状态。
            var settings = ApplicationData.Current.LocalSettings;
            settings.Values.Remove(name);
            return task;
        }
    }
}
