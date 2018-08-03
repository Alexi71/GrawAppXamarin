using Foundation;
using System;
using UIKit;
using GrawApp.Controller;
using Syncfusion.SfChart.iOS;
using CoreGraphics;
using System.Collections.Generic;
using System.Collections;
using GrawApp.Database;
using GrawApp.Helper;

namespace GrawApp
{
    public partial class ChartTimeViewController : ChartBaseViewController
    {
        SFChart Chart;
        ChartDelegate CDelegate;

        public ChartTimeViewController(IntPtr handle) : base(handle)
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
            var axisX = new SFNumericalAxis();
            axisX.Interval = new NSNumber(600);
           
                 
            axisX.Title.Text = new NSString("Time in min".GetLocalString());
            var axisY = new SFNumericalAxis();
            axisY.Visible = false;
            Chart.PrimaryAxis = axisX;
            Chart.SecondaryAxis = axisY;

            var temperaturSeries = GetSeries(Data, "Temperature".GetLocalString(), "Time", "Temperature",
                                             Colors["Temperature"],
                                          false, 0, 0, false, 0, 100);
           


            Chart.Series.Add(temperaturSeries);

            var humditySeries = GetSeries(Data, "Humidity".GetLocalString(), "Time", "Humidity",
                                          Colors["Humidity"],
                                          false, 0, 0, true, 0, 100,true, true, false, false);


            Chart.Series.Add(humditySeries);


            var series = GetSeries(Data, "Pressure".GetLocalString(), "Time", "Pressure",
                                   Colors["Pressure"],
                                   false, 0, 0, true, 1, 1200,true, true, false, false,false,true);


            Chart.Series.Add(series);

            series = GetSeries(Data, "Wind speed".GetLocalString(), "Time", "WindSpeed",
                               Colors["WindSpeed"],
                               false, 0, 0, true, 0, 150,true, true, false, false);
            Chart.Series.Add(series);

            var pointSeries = GetPointSeries(ReducedData, "Temperature".GetLocalString(), "Time", "Temperature",
                                             Colors["Temperature"],
                                          false, 0, 0, false, 0, 100);
            Chart.Series.Add(pointSeries);

            pointSeries = GetPointSeries(ReducedData, "Humidity".GetLocalString(), "Time", "Humidity",
                                          Colors["Humidity"],
                                          false, 0, 0, true, 0, 100,true,true,false,false);
            Chart.Series.Add(pointSeries);

            pointSeries = GetPointSeries(ReducedData, "Pressure".GetLocalString(), "Time", "Pressure",
                                   Colors["Pressure"],
                                         false, 0, 0, true, 1, 1200, true, true, false, false, false, true);
            Chart.Series.Add(pointSeries);

            pointSeries = GetPointSeries(ReducedData, "Wind speed".GetLocalString(), "Time", "WindSpeed",
                               Colors["WindSpeed"],
                                         false, 0, 0, true, 0, 150, true, true, false, false);
            Chart.Series.Add(pointSeries);

            //var test = GetNewSeries(ReducedData, typeof(SFScatterSeries), "Wind speed", "Time", "WindSpeed",
                               //Colors["WindSpeed"],
                                    //false, 0, 0, true, 0, 150, false);

        }

      

        //SFSeries GetNewSeries(IEnumerable dataSource, Type seriesType, string label, string xPath, string yPath, UIColor color,
        //                      bool needAxisX, int minX, int maxX,
        //                      bool needAxisY, int minY, int maxY,
        //                       bool yAxisVisible = true, bool yAxisShowGridlines = false, bool yAxisIsLogarithmic = false)
        //{
        //    var lineSeries = new SFLineSeries();
        //    SFSeries series;
        //    var pointSeries = new SFScatterSeries();
        //    if(seriesType.ToString() == "SFLineSeries")
        //    {
        //        //var lineSeries = new SFLineSeries();
        //        lineSeries.ItemsSource = dataSource;
        //        lineSeries.Label = label;
        //        lineSeries.EnableTooltip = true;
        //        lineSeries.EnableAnimation = true;
        //        lineSeries.XBindingPath = xPath;
        //        lineSeries.YBindingPath = yPath;
        //        lineSeries.Color = color;
        //        series = lineSeries;
        //    }
        //    else
        //    {
        //        pointSeries.ItemsSource = dataSource;
        //        pointSeries.Label = label;
        //        pointSeries.EnableTooltip = true;
        //        pointSeries.EnableAnimation = true;
        //        pointSeries.XBindingPath = xPath;
        //        pointSeries.YBindingPath = yPath;
        //        pointSeries.Color = color;
        //        series = pointSeries;
        //    }

           
        //    //if (needAxisX)
        //    //{
        //    //    var axiX = new SFNumericalAxis();
        //    //    axiX.Maximum = new NSNumber(maxX);
        //    //    axiX.Minimum = new NSNumber(minX);
        //    //    series.XAxis = axiX;
        //    //}
        //    //if (needAxisY)
        //    //{
        //    //    if (!yAxisIsLogarithmic)
        //    //    {
        //    //        var axixY = new SFNumericalAxis();
        //    //        axixY.Maximum = new NSNumber(maxY);
        //    //        axixY.Minimum = new NSNumber(minY);
        //    //        axixY.Visible = yAxisVisible;
        //    //        axixY.ShowMajorGridLines = yAxisShowGridlines;
        //    //        series.YAxis = axixY;
        //    //    }
        //    //    else
        //    //    {
        //    //        var axixY = new SFLogarithmicAxis();
        //    //        axixY.Maximum = new NSNumber(maxY);
        //    //        axixY.Minimum = new NSNumber(minY);
        //    //        axixY.Interval = new NSNumber(100);
        //    //        axixY.OpposedPosition = true;
        //    //        axixY.Visible = yAxisVisible;
        //    //        axixY.ShowMajorGridLines = yAxisShowGridlines;
        //    //        series.YAxis = axixY;
        //    //    }
        //    //}
        //    return series;
        //}

       
    }

    public class ChartDelegate:SFChartDelegate
    {
        public override NSString GetFormattedAxisLabel(SFChart chart, NSString label, NSObject value, SFAxis axis)
        {
            if (axis.Title.Text == "Time in min")
            {
                var nsNumber = (NSNumber)value;

                var newValue = nsNumber.Int32Value / 60;
                return NSString.FromData(NSData.FromString(newValue.ToString()), NSStringEncoding.ASCIIStringEncoding);
            }
            return base.GetFormattedAxisLabel(chart, label, value, axis);
        }

       

        public override void WillShowTooltip(SFChart chart, SFChartTooltip tooltipView)
        {
            var point = tooltipView.DataPoint as InputDataTable;
            var label = tooltipView.Series.Label;
            if (point != null)
            {
                var minutes = point.Time / 60;

                switch (label)
                {
                    case "Temperature":
                        tooltipView.Text = NSString.FromData(NSData.FromString($"{point.Temperature:0.0}°C {minutes:0} min"), NSStringEncoding.ASCIIStringEncoding);
                        break;
                    case "Pressure":
                        tooltipView.Text = NSString.FromData(NSData.FromString($"{point.Pressure:0.0}mB {minutes:0} min"), NSStringEncoding.ASCIIStringEncoding);
                        break;
                    case "Humidity":
                        tooltipView.Text = NSString.FromData(NSData.FromString($"{point.Humidity:0}% {minutes:0} min"), NSStringEncoding.ASCIIStringEncoding);
                        break;
                    case "Wind speed":
                        tooltipView.Text = NSString.FromData(NSData.FromString($"{point.WindSpeed:0.0}m/s {minutes:0} min"), NSStringEncoding.ASCIIStringEncoding);
                        break;
                    default:
                        break;
                }
            }
            base.WillShowTooltip(chart, tooltipView);
        }
    }


}