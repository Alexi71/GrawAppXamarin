using System;
using System.IO;
using System.Net;
using System.Text;
using Foundation;
using GrawApp.Model;
using Syncfusion.SfBusyIndicator.iOS;
using UIKit;
namespace GrawApp.Controller.Messages
{
    public class MessageBaseViewController : UIViewController
    {
        public int Index { get; set; }

        FlightContent Flight;
        public SFBusyIndicator BusyIndicator {get; set;}
        public MessageBaseViewController(IntPtr handle) : base(handle)
        {
            var appDelegate = UIApplication.SharedApplication.Delegate as AppDelegate;
            Flight = appDelegate.CurrentFlight;


        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

        }

        public void InitializeIndicator(UIView view)
        {
            BusyIndicator = new SFBusyIndicator()
            {
                Frame = view.Frame,
                Opaque = true,
                BackgroundColor = new UIColor(255f / 255f, 255f / 255f, 255f / 255f, 0.5f),
                AnimationType = SFBusyIndicatorAnimationType.SFBusyIndicatorAnimationTypeSlicedCircle,
                Title = (NSString)"Loading...",
                ViewBoxWidth = 80,
                ViewBoxHeight = 80,
                Foreground = UIColor.Gray,
                Duration = 2,
                IsBusy = true,
                Hidden = false
            };
            view.AddSubview(BusyIndicator);
        }

        public void GetText(bool isHundred)
        {
            var webClient = new WebClient();

            webClient.DownloadStringCompleted += (s, e) => {
                var text = e.Result; // get the downloaded text
                Console.WriteLine(text);
                InvokeOnMainThread(() => {
                    UpdateText(text);
                });
              
            };
            if((isHundred && string.IsNullOrEmpty(Flight.Url100) ||
                (!isHundred && string.IsNullOrEmpty(Flight.UrlEnd))))
            {
                UpdateText("no data available");
                return;
            }
            var url = new Uri(isHundred?Flight.Url100:Flight.UrlEnd); 
            webClient.Encoding = Encoding.UTF8;
            webClient.DownloadStringAsync(url);
        }

        public virtual void UpdateText(string text)
        {
            
        }

    }

    //public class DownloadText
    //{
    //    FlightContent Flight { get; set; }
    //    UITextView TextView { get; set; }

    //    public void GetText(bool isHundred)
    //    {
    //        var webClient = new WebClient();

    //        webClient.DownloadStringCompleted += (s, e) => {
    //            var text = e.Result; // get the downloaded text
    //            Console.WriteLine(text);

    //            InvokeOnMainThread(() => {
    //                TextView.Text = text;
    //                //new UIAlertView("Done", "File downloaded and saved", null, "OK", null).Show();
    //            });
    //            //string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
    //            //string localFilename = "downloaded.txt";
    //            //string localPath = Path.Combine(documentsPath, localFilename);
    //            //File.WriteAllText(localpath, text); // writes to local storage
    //        };

    //        var url = new Uri(isHundred ? Flight.Url100 : Flight.UrlEnd);
    //        webClient.Encoding = Encoding.UTF8;
    //        webClient.DownloadStringAsync(url);
    //    }
    //}
}
