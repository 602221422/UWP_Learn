
using Microsoft.QueryStringDotNET;
using Microsoft.Toolkit.Uwp.Notifications;
using Windows.UI.Notifications;

namespace tilesApp.Helpers
{
    class ToastHelper
    {
        public const string TAG_FOR_LUNCH = "lunch";
        public static void ShowOrderLunchToast()
        {
            ToastContent content = new ToastContent()
            {
                //启动
                Launch = new QueryString()
                {
                    {"action","viewLunchInfo" }
                }.ToString(),
                //显示
                Visual = new ToastVisual()
                {
                    BindingGeneric = new ToastBindingGeneric()
                    {//主图
                        HeroImage = new ToastGenericHeroImage()
                        {
                            Source = "https://picsum.photos/728/360?image=1080"
                        },
                        //内容
                        Children =
                        {
                            new AdaptiveText
                            {
                                Text="AAAAAA"
                            }
                        }
                    }
                },
                //按钮
                Actions = new ToastActionsCustom()
                {
                    Buttons =
                    {
                        new ToastButton("Yes",new QueryString()
                        {
                            {"action","BBBB" }
                        }.ToString())
                        {
                            ActivationType =ToastActivationType.Background,
                            ActivationOptions=new ToastActivationOptions()
                            {
                                AfterActivationBehavior=ToastAfterActivationBehavior.PendingUpdate
                            }
                        },
                        //取消
                        new ToastButtonDismiss("CCCC")
                    }
                }
            };
            ShowToast(content);
        }
        public static void ShowWhichLunchToast()
        {
            ToastContent content = new ToastContent()
            {
                Launch = new QueryString()
                {
                    {"action","viewwhich" }
                }.ToString(),
                Visual = new ToastVisual()
                {
                    BindingGeneric = new ToastBindingGeneric()
                    {
                        Children =
                        {
                            new AdaptiveText()
                            {
                                Text="which like?"
                            }
                        }
                    }
                },
                Actions = new ToastActionsCustom()
                {
                    Inputs =
                    {
                        new ToastSelectionBox("lunch")
                        {
                            Items =
                            {
                                new ToastSelectionBoxItem("AAA","aaa"),
                                new ToastSelectionBoxItem("BBB","bbb"),
                                new ToastSelectionBoxItem("CCC","ccc")
                            },
                            DefaultSelectionBoxItemId ="AAA"
                        }
                    },
                    Buttons =
                    {
                        new ToastButton("submit",new QueryString()
                        {
                            {"action","submitOrder" }
                        }.ToString())
                        {
                            ActivationType=ToastActivationType.Background,
                            ActivationOptions=new ToastActivationOptions()
                            {
                                AfterActivationBehavior=ToastAfterActivationBehavior.PendingUpdate
                            }
                        },
                        new ToastButtonDismiss("Cancel")
                    }
                },
                //禁用后续音频
                Audio = new ToastAudio() { Silent = true }
            };
            ShowToast(content);
        }
        public static void ShowSendingOrderToast()
        {
            ToastContent content = new ToastContent()
            {
                Launch = new QueryString()
                {
                    {"action","viewSendingOrder" }
                }.ToString(),
                Visual = new ToastVisual()
                {
                    BindingGeneric = new ToastBindingGeneric()
                    {
                        Children =
                        {
                            new AdaptiveText()
                            {
                                Text="Hold on..."
                            },
                            new AdaptiveProgressBar()
                            {
                                Status="Sending...",
                                Value=AdaptiveProgressBarValue.Indeterminate
                            }
                        }
                    }
                },
                Audio = new ToastAudio() { Silent = true }
            };
            ShowToast(content);
        }
        public static void ShowOrderCompleteToast(string friendlyChoice)
        {
            ToastContent content = new ToastContent()
            {
                Launch = new QueryString()
                {
                    { "action", "viewOrder" }
                }.ToString(),

                Visual = new ToastVisual()
                {
                    BindingGeneric = new ToastBindingGeneric()
                    {
                        Children =
                        {
                            new AdaptiveText()
                            {
                                Text = $"Your {friendlyChoice} is on the way!"
                            },

                            new AdaptiveText()
                            {
                                Text = "Success"
                            }
                        }
                    }
                }
            };
            ShowToast(content);
        }
        private static void ShowToast(ToastContent content)
        {
            ToastNotificationManager.CreateToastNotifier().Show(new ToastNotification(content.GetXml())
            {
                Tag=TAG_FOR_LUNCH
            });
        }
    }
}
