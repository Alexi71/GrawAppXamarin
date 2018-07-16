// WARNING
//
// This file has been generated automatically by Visual Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;

namespace GrawApp.View
{
    [Register ("RawContentViewController")]
    partial class RawContentViewController
    {
        [Outlet]
        UIKit.UIImageView GpsImage { get; set; }

        [Outlet]
        UIKit.UILabel LastUpdateText { get; set; }

        [Outlet]
        UIKit.UIImageView SensorImage { get; set; }

        [Outlet]
        UIKit.UILabel StartTimeText { get; set; }

        [Outlet]
        UIKit.UIView SubView { get; set; }

        [Outlet]
        UIKit.UIImageView TelemetryImage { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (GpsImage != null) {
                GpsImage.Dispose ();
                GpsImage = null;
            }

            if (LastUpdateText != null) {
                LastUpdateText.Dispose ();
                LastUpdateText = null;
            }

            if (SensorImage != null) {
                SensorImage.Dispose ();
                SensorImage = null;
            }

            if (StartTimeText != null) {
                StartTimeText.Dispose ();
                StartTimeText = null;
            }

            if (SubView != null) {
                SubView.Dispose ();
                SubView = null;
            }

            if (TelemetryImage != null) {
                TelemetryImage.Dispose ();
                TelemetryImage = null;
            }
        }
    }
}