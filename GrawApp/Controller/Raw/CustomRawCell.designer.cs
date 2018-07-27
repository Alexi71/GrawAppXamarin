// WARNING
//
// This file has been generated automatically by Visual Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
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
