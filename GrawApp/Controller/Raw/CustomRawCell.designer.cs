// WARNING
//
// This file has been generated automatically by Visual Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;

namespace GrawApp.Controller.Raw
{
    [Register ("CustomRawCell")]
    partial class CustomRawCell
    {
        [Outlet]
        UIKit.UIImageView CustomImage { get; set; }

        [Outlet]
        UIKit.UILabel HeaderLabel { get; set; }

        [Outlet]
        UIKit.UILabel ValueLabel { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (CustomImage != null) {
                CustomImage.Dispose ();
                CustomImage = null;
            }

            if (HeaderLabel != null) {
                HeaderLabel.Dispose ();
                HeaderLabel = null;
            }

            if (ValueLabel != null) {
                ValueLabel.Dispose ();
                ValueLabel = null;
            }
        }
    }
}