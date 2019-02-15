﻿using DocumentFormat.OpenXml.Packaging;
using SpreadSheetWorking.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
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


// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace SpreadSheetWorking
{
    public sealed partial class MainPage : Page
    {
        MemberInfo mymember = new MemberInfo();
        ObservableCollection<MemberInfo> memcoll = new ObservableCollection<MemberInfo>();
        public MainPage()
        {
            this.InitializeComponent();
            
        }
        private async void Button_Click(object sender, RoutedEventArgs e)
        {
                Stream finalstream = await SpreadsheetHelper.filepathhelper();
            try
            {
                SpreadsheetHelper.ReadDataFromExcel(finalstream, "Sheet1", "A2", "F29", memcoll);
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex.Message.ToString());
            }
        }
    }
}
