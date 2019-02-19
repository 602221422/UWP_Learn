using Microsoft.Toolkit.Uwp.Notifications;
using Microsoft.QueryStringDotNET;
using Windows.UI.Notifications;
namespace SpreadSheetWorking.Model
{
    class ToastHelper
    {
        public const string TAG = "team";
        public static void ShowInfoToast(string name,int vacationhour)
        {
            ToastContent content = new ToastContent()
            {
                Launch = new QueryString()
                {
                    {"action","viewInfo" }
                }.ToString(),
                Visual =new ToastVisual()
                {
                    BindingGeneric =new ToastBindingGeneric()
                    {
                        Children =
                        {
                            new AdaptiveText
                            {
                                Text=$"{name} apply for leave {vacationhour}"
                            }
                        }
                    }
                },
                Actions=new ToastActionsCustom()
                {
                    Buttons =
                    {
                        new ToastButtonDismiss("Yes")
                    }
                }
            };
            ShowToast(content);
        }
        private static void ShowToast(ToastContent content)
        {
            ToastNotificationManager.CreateToastNotifier().Show(new ToastNotification(content.GetXml())
            {
                Tag = TAG
            });
        }
    }
}
