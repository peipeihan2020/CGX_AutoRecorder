using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Automation;
using System.Threading;
using System.Messaging;

namespace Sync
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        static bool start = true;

        static AutomationElement FindDevice() {
            AutomationElement desktop = AutomationElement.RootElement;
            var cgx = desktop.FindFirst(TreeScope.Children, new PropertyCondition(

              AutomationElement.NameProperty, "Cognionics Data Acquisition"));
            cgx.SetFocus();
             Thread.Sleep(100);
            var device = cgx.FindFirst(TreeScope.Descendants, new PropertyCondition(

              AutomationElement.NameProperty, "Device"));
            SelectionItemPattern selectionItemPattern = (device.GetCurrentPattern(SelectionItemPattern.Pattern)) as SelectionItemPattern;
            selectionItemPattern.Select();
            return device;
        }

        static void Start() {
            var device = FindDevice();
            Thread.Sleep(500);



            var record = device.FindFirst(TreeScope.Descendants, new PropertyCondition(

                AutomationElement.NameProperty, "Record"));
            var clickPattern = (record.GetCurrentPattern(InvokePattern.Pattern)) as InvokePattern;
            clickPattern.Invoke();
            System.Windows.Forms.SendKeys.SendWait(@"C:\Users\peipeihan\Desktop" + System.DateTime.Now.ToString("yyyyMMddhhmmssfff"));
            System.Windows.Forms.SendKeys.SendWait("{ENTER}");
            
        }

        static void Stop() {
            var device = FindDevice();
            var record = device.FindFirst(TreeScope.Descendants, new PropertyCondition(
                      AutomationElement.NameProperty, "Stop Recording"));
            var clickPattern = (record.GetCurrentPattern(InvokePattern.Pattern)) as InvokePattern;
            clickPattern.Invoke();
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {

            var btn = sender as Button;
            if (btn.Content.ToString().ToLower() == "start")
            {
                
                btn.Content = "Stop";
            }
            else
            {
                btn.Content = "Start";
            }
            if (start)
            {
                Start();
                SendStringMarkers.Send("Start");
            }
            else
            {
                Stop();
                SendStringMarkers.Send("Stop");
            }
            start = !start;
        }
    }
}
