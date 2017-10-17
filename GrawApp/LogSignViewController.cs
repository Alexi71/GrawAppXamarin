using Foundation;
using System;
using UIKit;
using Firebase.Auth;

namespace GrawApp
{
    public partial class LogSignViewController : UIViewController
    {
        bool LoginIsActive = true;

        public LogSignViewController (IntPtr handle) : base (handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            var plist = NSUserDefaults.StandardUserDefaults;
            var userName = plist.StringForKey("username");
            var password = plist.StringForKey("password");
            var firstCall = plist.BoolForKey("firstcall");

            if (userName != null && password != null &&
              firstCall)
            {
                LoginAndMove(userName, password);
            }
        }

        partial void TopButton_TouchUpInside(UIButton sender)
        {
            LoginAndMove(emailText.Text, passwordTextField.Text);

        }

        private void LoginAndMove(string userName, string password)
        {
            Auth.DefaultInstance.SignIn(userName,password,(user, error) => 
            {
                if(error == null)
                {
                    var plist = NSUserDefaults.StandardUserDefaults;
                    plist.SetString(userName,"username");
                    plist.SetString(password,"password");

                }
                else
                {
                    ShowError(error.LocalizedDescription);
                }
            });
        }

        private void ShowError(string errorText)
        {
            var alertVC = UIAlertController.Create("Error", errorText, UIAlertControllerStyle.Alert);
            alertVC.AddAction(UIAlertAction.Create("OK",
                                                   UIAlertActionStyle.Default,action => 
            {
              alertVC.DismissViewController(true,null);  
            }));

            PresentViewController(alertVC,true,null);
        }
    }
}