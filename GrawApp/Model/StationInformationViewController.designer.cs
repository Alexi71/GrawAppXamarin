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
    [Register ("StationInformationViewController")]
    partial class StationInformationViewController
    {
        [Outlet]
        UIKit.UILabel cityLabel { get; set; }


        [Outlet]
        MapKit.MKMapView mapView { get; set; }


        [Outlet]
        UIKit.UITableView tableView { get; set; }


        [Outlet]
        UIKit.UILabel temperatureLabel { get; set; }


        [Outlet]
        UIKit.UIImageView weatherImage { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIBarButtonItem menuButton { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (cityLabel != null) {
                cityLabel.Dispose ();
                cityLabel = null;
            }

            if (mapView != null) {
                mapView.Dispose ();
                mapView = null;
            }

            if (menuButton != null) {
                menuButton.Dispose ();
                menuButton = null;
            }

            if (tableView != null) {
                tableView.Dispose ();
                tableView = null;
            }

            if (temperatureLabel != null) {
                temperatureLabel.Dispose ();
                temperatureLabel = null;
            }

            if (weatherImage != null) {
                weatherImage.Dispose ();
                weatherImage = null;
            }
        }
    }
}