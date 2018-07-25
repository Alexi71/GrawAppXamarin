// WARNING
//
// This file has been generated automatically by Visual Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace GrawApp
{
    [Register ("StationInformationViewController")]
    partial class StationInformationViewController
    {
        [Outlet]
        UIKit.UIButton calendarButton { get; set; }

        [Outlet]
        UIKit.UIImageView calendarImage { get; set; }

        [Outlet]
        UIKit.UILabel cityLabel { get; set; }

        [Outlet]
        MapKit.MKMapView mapView { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIBarButtonItem menuButton { get; set; }

        [Outlet]
        UIKit.UITableView tableView { get; set; }

        [Outlet]
        UIKit.UILabel temperatureLabel { get; set; }

        [Outlet]
        UIKit.UILabel timeLabel { get; set; }

        [Outlet]
        UIKit.UIImageView weatherImage { get; set; }

        [Action ("calendarButtonTapped:")]
        partial void calendarButtonTapped (UIKit.UIButton sender);
        
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

            if (menuButton != null) {
                menuButton.Dispose ();
                menuButton = null;
            }

            if (calendarImage != null) {
                calendarImage.Dispose ();
                calendarImage = null;
            }

            if (timeLabel != null) {
                timeLabel.Dispose ();
                timeLabel = null;
            }

            if (calendarButton != null) {
                calendarButton.Dispose ();
                calendarButton = null;
            }
        }
    }
}
