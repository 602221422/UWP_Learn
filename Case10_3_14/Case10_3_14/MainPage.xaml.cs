using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.System;
using Windows.UI.Core;
using Windows.UI.Input.Preview.Injection;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Case10_3_14
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }
        private async void TypeText()
        {
            ShowOne("hello");

        }
        /*private async void OnClick()
        {           
            show.Focus(FocusState.Programmatic);
            await Task.Delay(100); //we must yield the UI thread so that focus can be acquired
            string a = "H";
            InputInjector injector =  InputInjector.TryCreate();
            char c = a[0];
            InjectedInputKeyboardInfo keyinfo = new InjectedInputKeyboardInfo();
            keyinfo.VirtualKey = (ushort)(VirtualKey)Enum.Parse(typeof(VirtualKey), a.ToString(), true); ;
            InjectedInputKeyboardInfo[] infos =
            {
                keyinfo
            };
            // 让文本框获得键盘焦点，不然输不进去
            
            injector.InjectKeyboardInput(infos);
            await Task.Delay(100);
        }*/
        private async void ShowOne(string mystring)
        {
            show.Focus(FocusState.Programmatic);
            await Task.Delay(100);
            InputInjector injector = InputInjector.TryCreate();
            List<InjectedInputKeyboardInfo> inputs = new List<InjectedInputKeyboardInfo>();
            InjectedInputKeyboardInfo leftshiftkeydown = new InjectedInputKeyboardInfo();
            for (int i=0;i<mystring.Length;i++)
            {
                if (i == 0)
                {
                    leftshiftkeydown.VirtualKey = (ushort)((VirtualKey)Enum.Parse(typeof(VirtualKey), "LeftShift", true));
                    leftshiftkeydown.KeyOptions = InjectedInputKeyOptions.None;
                    inputs.Add(leftshiftkeydown);
                    await Task.Delay(100);
                    InjectedInputKeyboardInfo hkey = new InjectedInputKeyboardInfo();
                    hkey.VirtualKey = (ushort)((VirtualKey)Enum.Parse(typeof(VirtualKey), mystring[i].ToString(), true));
                    hkey.KeyOptions = InjectedInputKeyOptions.None;
                    inputs.Add(hkey);
                    await Task.Delay(100);
                    InjectedInputKeyboardInfo leftshiftkeyup = new InjectedInputKeyboardInfo();
                    leftshiftkeyup.VirtualKey = (ushort)((VirtualKey)Enum.Parse(typeof(VirtualKey), "LeftShift", true));
                    leftshiftkeyup.KeyOptions = InjectedInputKeyOptions.KeyUp;
                    inputs.Add(leftshiftkeyup);
                    await Task.Delay(100);
                    injector.InjectKeyboardInput(inputs);
                }
                else
                {
                    var info = new InjectedInputKeyboardInfo();
                    info.VirtualKey = (ushort)(VirtualKey)Enum.Parse(typeof(VirtualKey), mystring[i].ToString(), true);
                    injector.InjectKeyboardInput(new[] { info });
                    await Task.Delay(100);
                }
                
            }
        }
    }
}
