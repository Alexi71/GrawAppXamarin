// This file has been autogenerated from a class added in the UI designer.

using System;
using CoreGraphics;
using Foundation;
using GrawApp.Controller;
using GrawApp.Helper;
using Syncfusion.SfChart.iOS;
using UIKit;

namespace GrawApp
{
    public partial class ChartPressureViewController : ChartBaseViewController
	{
        SFChart Chart;
        ChartDelegate CDelegate;

		public ChartPressureViewController (IntPtr handle) : base (handle)
		{
		}

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            EdgesForExtendedLayout = UIRectEdge.None;
            Chart = new SFChart();
            CDelegate = new ChartDelegate();
            Chart.Delegate = CDelegate;

            var sampleFrame = new CGRect(ChartView.Bounds.Location.X,
                                         ChartView.Bounds.GetMinY(),
                                         ChartView.Bounds.Width,
                                         ChartView.Bounds.Height);
            Chart.Frame = sampleFrame;
            Chart.TranslatesAutoresizingMaskIntoConstraints = false;

            View.AddSubview(Chart);

            var horizontalConstraint = NSLayoutConstraint.Create(Chart, NSLayoutAttribute.Top,
                                                              NSLayoutRelation.Equal,
                                                              ChartView,
                                                              NSLayoutAttribute.Top, 1, 0);

            var verticalConstraint = NSLayoutConstraint.Create(Chart,
                                                                NSLayoutAttribute.Bottom,
                                                                 NSLayoutRelation.Equal,
                                                                ChartView,
                                                                NSLayoutAttribute.Bottom, 1, 0);
            var widthConstraint = NSLayoutConstraint.Create(Chart,
                                                               NSLayoutAttribute.Width,
                                                              NSLayoutRelation.Equal,
                                                              ChartView, NSLayoutAttribute.Width, 1, 0);
            var heightConstraint = NSLayoutConstraint.Create(Chart,
                                                               NSLayoutAttribute.Height,
                                                                NSLayoutRelation.Equal,
                                                               ChartView,
                                                               NSLayoutAttribute.Height, 1, 0);
            NSLayoutConstraint.ActivateConstraints(new NSLayoutConstraint[] { horizontalConstraint, verticalConstraint, widthConstraint, heightConstraint });
            InitializeChartData();
        }

        private void InitializeChartData()
        {
            Chart.Legend.Visible = true;
            Chart.Legend.ToggleSeriesVisibility = true;
            var axisY = new SFLogarithmicAxis()
            {
                //Minimum = new NSNumber(5),
                //Maximum = new NSNumber(1200),
                //LogarithmicBase = 10,
                //Interval = new NSNumber(100),
                Visible = true,
                IsInversed = true,
                ShowMajorGridLines = true
                    
            };
            axisY.Title.Text = new NSString("Pressure in mB".GetLocalString());
            var axisX = new SFNumericalAxis()
            {
                Maximum = new NSNumber(40),
                Minimum = new NSNumber(-90),
                ShowMajorGridLines = true,
                Interval = new NSNumber(10)
            };

            axisX.Title.Text = new NSString("Temperature/Dewpoint in °C".GetLocalString());

            Chart.PrimaryAxis = axisX;
            Chart.SecondaryAxis = axisY;

            var temperaturSeries = GetSeries(Data, "Temperature".GetLocalString(), "Temperature", "Pressure",
                                             Colors["Temperature"],
                                          false, 0, 0, false, 0, 100,true,true);



            Chart.Series.Add(temperaturSeries);

            var humditySeries = GetSeries(Data, "Dewpoint".GetLocalString(), "Dewpoint", "Pressure",
                                          Colors["Humidity"],
                                          false, 0, 100, false, 0, 100, false, false, false);


            Chart.Series.Add(humditySeries);


          
            //var series = GetSeries(Data, "Wind speed", "WindSpeed", "Geopotential",
            //                   Colors["WindSpeed"],
            //                   true, 0, 100, false, 0, 100, false, false, false);
            //Chart.Series.Add(series);

            //var pointSeries = GetPointSeries(ReducedData, "Temperature", "Temperature", "Pressure",
            //                                 Colors["Temperature"],
            //                              false, 0, 0, false, 0, 100);
            //Chart.Series.Add(pointSeries);

            //pointSeries = GetPointSeries(ReducedData, "Dewpoint", "Dewpoint", "Pressure",
            //                              Colors["Humidity"],
            //                              true, 0, 100, false, 0, 100, false, false, false);
            //Chart.Series.Add(pointSeries);

            //pointSeries = GetPointSeries(ReducedData, "Pressure", "Pressure", "Geopotential",
            //                       Colors["Pressure"],
            //                             true, 1, 1200, false, 1, 1200, false, false, true);
            //Chart.Series.Add(pointSeries);

            //pointSeries = GetPointSeries(ReducedData, "Wind speed", "WindSpeed", "Geopotential",
            //                   Colors["WindSpeed"],
            //                             true, 0, 100, true, 0, 100, false, false, false);
            //Chart.Series.Add(pointSeries);

            //var test = GetNewSeries(ReducedData, typeof(SFScatterSeries), "Wind speed", "Time", "WindSpeed",
            //Colors["WindSpeed"],
            //false, 0, 0, true, 0, 150, false);

        }
	}
}
