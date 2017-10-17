// WARNING
//
// This file has been generated automatically by Visual Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace GrawApp
{
    [Register ("LogSignViewController")]
    partial class LogSignViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField emailText { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton lowerButton { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField passwordTextField { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton topButton { get; set; }

        [Action ("TopButton_TouchUpInside:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void TopButton_TouchUpInside (UIKit.UIButton sender);

        void ReleaseDesignerOutlets ()
        {
            if (emailText != null) {
                emailText.Dispose ();
                emailText = null;
            }

            if (lowerButton != null) {
                lowerButton.Dispose ();
                lowerButton = null;
            }

            if (passwordTextField != null) {
                passwordTextField.Dispose ();
                passwordTextField = null;
            }

            if (topButton != null) {
                topButton.Dispose ();
                topButton = null;
            }
        }
    }
}