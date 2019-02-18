using Microsoft.QueryStringDotNET;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using tilesApp.Helpers;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.ApplicationModel.Background;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Notifications;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace tilesApp
{
    /// <summary>
    /// 提供特定于应用程序的行为，以补充默认的应用程序类。
    /// </summary>
    sealed partial class App : Application
    {
        /// <summary>
        /// 初始化单一实例应用程序对象。这是执行的创作代码的第一行，
        /// 已执行，逻辑上等同于 main() 或 WinMain()。
        /// </summary>
        public App()
        {
            this.InitializeComponent();
            this.Suspending += OnSuspending;
        }

        /// <summary>
        /// 在应用程序由最终用户正常启动时进行调用。
        /// 将在启动应用程序以打开特定文件等情况下使用。
        /// </summary>
        /// <param name="e">有关启动请求和过程的详细信息。</param>
        protected override void OnLaunched(LaunchActivatedEventArgs args)
        {
            OnlaunchedOrActivited(args);
        }
        private void OnlaunchedOrActivited(IActivatedEventArgs e)
        {
            if (System.Diagnostics.Debugger.IsAttached)
            {
                this.DebugSettings.EnableFrameRateCounter = true;
            }
            if (!(Window.Current.Content is Frame rootFrame))
            {
                rootFrame = new Frame();

                rootFrame.NavigationFailed += OnNavigationFailed;

                if (e.PreviousExecutionState == ApplicationExecutionState.Terminated)
                {
                    //TODO: Load state from previously suspended application
                }

                // Place the frame in the current Window
                Window.Current.Content = rootFrame;
            }

            RegisterBackgroundTasks();

            if (e is LaunchActivatedEventArgs && (e as LaunchActivatedEventArgs).PrelaunchActivated == false)
            {
                if (rootFrame.Content == null)
                {
                    // When the navigation stack isn't restored navigate to the first page,
                    // configuring the new page by passing required information as a navigation
                    // parameter
                    rootFrame.Navigate(typeof(MainPage), (e as LaunchActivatedEventArgs).Arguments);
                }
                // Ensure the current window is active
                Window.Current.Activate();
            }

            if (e is ToastNotificationActivatedEventArgs)
            {
                var dontWait = new MessageDialog("Please click the buttons instead of the toast body. This quickstart does not handle activation of the body of the toast.", "Unsupported action").ShowAsync();
            }
        }
        private void RegisterBackgroundTasks()
        {
            const string taskName = "ToastBackgroundTask";

            // If background task is already registered, do nothing
            if (BackgroundTaskRegistration.AllTasks.Any(i => i.Value.Name.Equals(taskName)))
                return;

            // Create the background task
            BackgroundTaskBuilder builder = new BackgroundTaskBuilder()
            {
                Name = "MyToastNotificationActionTrigger",
            };

            // Assign the toast action trigger
            builder.SetTrigger(new ToastNotificationActionTrigger());

            // And register the task
            BackgroundTaskRegistration registration = builder.Register();
        }
        protected override async void OnBackgroundActivated(BackgroundActivatedEventArgs args)
        {
            var deferral = args.TaskInstance.GetDeferral();

            if (args.TaskInstance.TriggerDetails is Windows.UI.Notifications.ToastNotificationActionTriggerDetail)
            {
                var toastArgs = args.TaskInstance.TriggerDetails as ToastNotificationActionTriggerDetail;

                QueryString qs = QueryString.Parse(toastArgs.Argument);

                if (qs.TryGetValue("action", out string action))
                {
                    switch (action)
                    {
                        case "BBBB":
                            ToastHelper.ShowWhichLunchToast();
                            break;

                        case "submitOrder":

                            string choice = toastArgs.UserInput["lunch"] as string;
                            string friendlyChoice = choice;
                            if (choice == "CCC")
                            {
                                friendlyChoice = "ccc";
                            }

                            ToastHelper.ShowSendingOrderToast();

                            // Mock delay, actual app would do HTTP request to send order
                            await Task.Delay(3000);

                            ToastHelper.ShowOrderCompleteToast(friendlyChoice);
                            break;
                    }
                }
            }

            deferral.Complete();
        }
        /// <summary>
        /// 导航到特定页失败时调用
        /// </summary>
        ///<param name="sender">导航失败的框架</param>
        ///<param name="e">有关导航失败的详细信息</param>
        void OnNavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            throw new Exception("Failed to load Page " + e.SourcePageType.FullName);
        }

        /// <summary>
        /// 在将要挂起应用程序执行时调用。  在不知道应用程序
        /// 无需知道应用程序会被终止还是会恢复，
        /// 并让内存内容保持不变。
        /// </summary>
        /// <param name="sender">挂起的请求的源。</param>
        /// <param name="e">有关挂起请求的详细信息。</param>
        private void OnSuspending(object sender, SuspendingEventArgs e)
        {
            var deferral = e.SuspendingOperation.GetDeferral();
            //TODO: 保存应用程序状态并停止任何后台活动
            deferral.Complete();
        }
    }
}
