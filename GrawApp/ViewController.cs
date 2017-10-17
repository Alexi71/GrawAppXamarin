using System;

using UIKit;
using Firebase.Auth;

namespace GrawApp
{
    public partial class ViewController : UIViewController
    {
        protected ViewController(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            Auth.DefaultInstance.SignIn("123@aa.com","123456",(user, error) => {
                if (error != null)
                {
                    AuthErrorCode errorCode = (AuthErrorCode)((long)error.Code);
                    Console.WriteLine(errorCode);
                }
                else
                {
                    Console.WriteLine("Success");
                }
            }
                                        
                                       );
            // Perform any additional setup after loading the view, typically from a nib.
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }
    }
}
