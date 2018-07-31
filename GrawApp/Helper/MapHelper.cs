using System;
using CoreLocation;
using MapKit;
using UIKit;

namespace GrawApp.Helper
{


    public class MapDelegate : MKMapViewDelegate
    {
        //static string annotationId = "ConferenceAnnotation";
        public override MKOverlayRenderer OverlayRenderer(MKMapView mapView, IMKOverlay overlay)
        {
            if (overlay is MKPolyline)
            {
                var polyLine = overlay as MKPolyline;
                var polyLineView = new MKPolylineRenderer(polyLine)
                {
                    StrokeColor = UIColor.Red,
                    LineWidth = 3
                };
                return polyLineView;
            }

            return new MKOverlayRenderer();
        }


    }

    public class FlightAnnotation : MKAnnotation
    {

        CLLocationCoordinate2D coord;

        public override CLLocationCoordinate2D Coordinate { get { return coord; } }
        public override void SetCoordinate(CLLocationCoordinate2D value)
        {
            coord = value;
        }

        string title, subtitle;
        //public override CLLocationCoordinate2D Coordinate => throw new NotImplementedException();

        public override string Title { get { return title; } }
        public override string Subtitle { get { return subtitle; } }
        public UIColor TintColor { get; set; }

        public FlightAnnotation(string title,
                         string subtitle, CLLocationCoordinate2D coordinate)
        {
            coord = coordinate;
            this.title = title;
            this.subtitle = subtitle;
            TintColor = UIColor.Red;
        }



    }

}
