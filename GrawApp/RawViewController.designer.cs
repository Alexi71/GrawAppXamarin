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
    [Register ("RawViewController")]
    partial class RawViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel altitudeValueLabel { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel humidityValueLabel { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        MapKit.MKMapView mapView { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel pressureValueLabel { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel temperatureValueLabel { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel timeLabel { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel windDirectionValueLabel { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel windSpeedValueLabel { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (altitudeValueLabel != null) {
                altitudeValueLabel.Dispose ();
                altitudeValueLabel = null;
            }

            if (humidityValueLabel != null) {
                humidityValueLabel.Dispose ();
                humidityValueLabel = null;
            }

            if (mapView != null) {
                mapView.Dispose ();
                mapView = null;
            }

            if (pressureValueLabel != null) {
                pressureValueLabel.Dispose ();
                pressureValueLabel = null;
            }

            if (temperatureValueLabel != null) {
                temperatureValueLabel.Dispose ();
                temperatureValueLabel = null;
            }

            if (timeLabel != null) {
                timeLabel.Dispose ();
                timeLabel = null;
            }

            if (windDirectionValueLabel != null) {
                windDirectionValueLabel.Dispose ();
                windDirectionValueLabel = null;
            }

            if (windSpeedValueLabel != null) {
                windSpeedValueLabel.Dispose ();
                windSpeedValueLabel = null;
            }
        }
    }
}