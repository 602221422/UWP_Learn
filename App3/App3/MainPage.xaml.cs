using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using App3.Helper;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace App3
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            ObservableCollection<string> names = new ObservableCollection<string>();
            names.Add("aaa");
            names.Add("333");
            names.Add("dvfd");
            //namelist.ItemsSource = SqlDBHelper.QueryDataInDB();
            //namelist.ItemsSource = names;

        }
        private static string connectionString = @"Data Source = 172.17.16.133; Initial Catalog = ServerDB; User ID = sa; Password=Password001!;Connect Timeout = 30; Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        private void Button_Click(object sender, RoutedEventArgs e)

        {
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = connectionString;
                conn.Open();
                tb.Text = "open";
            }
        }
    }
}
