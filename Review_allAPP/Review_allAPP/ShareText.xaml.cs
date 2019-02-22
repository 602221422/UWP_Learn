using System;
using Windows.ApplicationModel.DataTransfer;
using Review_allAPP.Model;
using Windows.UI.Xaml;
// https://go.microsoft.com/fwlink/?LinkId=234238 上介绍了“空白页”项模板

namespace Review_allAPP
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class ShareText
    {
        public ShareText()
        {
            this.InitializeComponent();
        }
        protected bool GetShareContent(DataRequest request)
        {
            bool succeeded = false;

            string dataPackageText = TextToShare.Text;
            if (!String.IsNullOrEmpty(dataPackageText))
            {
                DataPackage requestData = request.Data;
                requestData.Properties.Title = TitleInputBox.Text;
                requestData.Properties.Description = DescriptionInputBox.Text; // The description is optional.
                requestData.Properties.ContentSourceApplicationLink = ApplicationLink;
                requestData.SetText(dataPackageText);
                succeeded = true;
            }
            else
            {
                request.FailWithDisplayText("Enter the text you would like to share and try again.");
            }
            return succeeded;
        }
        protected Uri ApplicationLink
        {
            get
            {
                return GetApplicationLink(GetType().Name);
            }
        }

        public static Uri GetApplicationLink(string sharePageName)
        {
            return new Uri("ms-sdk-sharesourcecs:navigate?page=" + sharePageName);
        }
        public void ShowUIButton_Click(object sender, RoutedEventArgs e)
        {
            // If the user clicks the share button, invoke the share flow programatically.
            DataTransferManager.ShowShareUI();
        }
    }
}
