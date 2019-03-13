using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.Activation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Automation.Peers;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace ModiaPlayerApp_3._7
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Main : Page
    {
        public static Main Current;
        public List<ScenarioModel> Scenarios;
        public Main()
        {
            this.InitializeComponent();
            Current = this;
            Scenarios = ScenarioModel.scenariosShow();
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            // Populate the scenario list from the SampleConfiguration.cs file
            ScenarioControl.ItemsSource = Scenarios;
            if (Window.Current.Bounds.Width < 640)
            {
                ScenarioControl.SelectedIndex = -1;
            }
            else
            {
                ScenarioControl.SelectedIndex = 0;
            }
        }
        private void ScenarioControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            ListBox scenarioListBox = sender as ListBox;
            ScenarioModel s = scenarioListBox.SelectedItem as ScenarioModel;
            if (s != null)
            {
                ScenarioFrame.Navigate(s.ClassType);
                if (Window.Current.Bounds.Width < 640)
                {
                    Spiltter.IsPaneOpen = false;
                }
            }
        }

        private void ToggleButton_Click(object sender, RoutedEventArgs e)
        {
            Spiltter.IsPaneOpen = !Spiltter.IsPaneOpen;
        }

        public void LoadScenarioForProtocolActivation(ProtocolActivatedEventArgs protocolArgs)
        {
            foreach (ScenarioModel scenario in Scenarios)
            {
                if (protocolArgs.Uri.Equals(GetApplicationLink(scenario.ClassType.Name)))
                {
                    ScenarioControl.SelectedIndex = Scenarios.IndexOf(scenario);
                    break;
                }
            }
        }
        public static Uri GetApplicationLink(string sharePageName)
        {
            return new Uri("ms-sdk-sharesourcecs:navigate?page=" + sharePageName);
        }
        public enum NotifyType
        {
            StatusMessage,
            ErrorMessage
        };
    }
}
