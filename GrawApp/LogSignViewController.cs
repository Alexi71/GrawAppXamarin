using Foundation;
using System;
using UIKit;
using Firebase.Auth;
using GrawApp.Database;
using System.Linq;

namespace GrawApp
{
    public partial class LogSignViewController : UIViewController
    {
        bool LoginIsActive = true;
        Station ActiveStation = null;

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

                    ActiveStation = GetActiveStation();
                    if(ActiveStation != null)
                    {

                        var storyboard = UIStoryboard.FromName("Main", null);
                        var nc = storyboard.InstantiateViewController("StationNavigation") as UINavigationController;

                        //var vc = nc.TopViewController as StationViewController;
                        //    vc.activeStation = self.activeStation


                            let rvc:SWRevealViewController = self.revealViewController() as SWRevealViewController
                            rvc.pushFrontViewController(nc, animated: true)
                    }

                }
                else
                {
                    ShowError(error.LocalizedDescription);
                }
            });
        }

        private Station GetActiveStation()
        {
            var list = DatabaseHelper.Read<UserStation>();
            if(list != null)
            {
                var result = list.FirstOrDefault(x => x.User.UserId == Auth.DefaultInstance.CurrentUser.Uid &&
                                                  x.IsDefault);
                return result.Station;
            }
            return null;

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