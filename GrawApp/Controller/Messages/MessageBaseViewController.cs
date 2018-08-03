using System;
using System.IO;
using System.Net;
using System.Text;
using CoreGraphics;
using Foundation;
using GrawApp.Helper;
using GrawApp.Model;
using Syncfusion.SfBusyIndicator.iOS;
using UIKit;
namespace GrawApp.Controller.Messages
{
    public class MessageBaseViewController : UIViewController
    {
        public int Index { get; set; }

        FlightContent Flight;

        public UIActivityIndicatorView ActivityIndicator { get; set; }
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

            ActivityIndicator = new UIActivityIndicatorView(UIActivityIndicatorViewStyle.WhiteLarge);
            ActivityIndicator.Frame = new CGRect(150, 150, 40, 40);
            ActivityIndicator.Center = view.Center;
            View.AddSubview(ActivityIndicator);
            ActivityIndicator.BringSubviewToFront(view);
            //UIApplication.SharedApplication.NetworkActivityIndicatorVisible = true;

            ActivityIndicator.Color = UIColor.Gray;
           
        }

        public void GetText(bool isHundred)
        {
            ActivityIndicator.StartAnimating();
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
                UpdateText("no data available".GetLocalString());
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
