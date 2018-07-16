// WARNING
//
// This file has been generated automatically by Visual Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;

namespace GrawApp
{
    [Register ("ChartAltitudeViewController")]
    partial class ChartAltitudeViewController
    {
        [Outlet]
        UIKit.UIView ChartView { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (ChartView != null) {
                ChartView.Dispose ();
                ChartView = null;
            }
        }
    }
}