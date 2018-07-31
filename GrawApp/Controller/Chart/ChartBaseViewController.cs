using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Foundation;
using GrawApp.Model;
using Syncfusion.SfChart.iOS;
using UIKit;
namespace GrawApp.Controller
{
    public class ChartBaseViewController : UIViewController
    {
        public int Index { get; set; }
        public List<InputData> Data {get;set;}
        public List<InputData> ReducedData { get; set; }

        public Dictionary<string, UIColor> Colors { get; private set; }= new Dictionary<string, UIColor>(){
            {"Temperature",new UIColor(2f / 255f, 64f / 255f, 123f / 255f, 1.0f)},
            {"Humidity",new UIColor(215f / 255f, 76f / 255f, 1f / 255f, 1.0f)},
            {"Pressure",new UIColor(166f / 255f, 2f / 255f, 2f / 255f, 1.0f)},
            {"WindSpeed",new UIColor(4f / 255f, 115f / 255f, 135f / 255f, 1.0f)}
        };
        //public List<SFChartDataPoint> TemperaturePoints { get; set; }
        //public List<SFChartDataPoint> PressurePoints { get; set; }
        //public List<SFChartDataPoint> HumidityPoints { get; set; }
        //public List<SFChartDataPoint> AltitudePoints { get; set; }
        //public List<SFChartDataPoint> WindSpeedPoints { get; set; }
        //public List<SFChartDataPoint> WindDirectionPoints { get; set; }
        //public List<SFChartDataPoint> DewpointPoints { get; set; }

        //public List<SFChartDataPoint> TemperatureReducedPoints { get; set; }
        //public List<SFChartDataPoint> PressureReducedPoints { get; set; }
        //public List<SFChartDataPoint> HumidityReducedPoints { get; set; }
        //public List<SFChartDataPoint> AltitudeReducedPoints { get; set; }
        //public List<SFChartDataPoint> WindSpeedReducedPoints { get; set; }
        //public List<SFChartDataPoint> WindDirectionReducedPoints { get; set; }
        //public List<SFChartDataPoint> DewpointReducedPoints { get; set; }

        public ChartBaseViewController()
        {

        }


        public ChartBaseViewController(IntPtr handle):base(handle)
        {
            var appDelegate = UIApplication.SharedApplication.Delegate as AppDelegate;
            Data = appDelegate.ProfileData;
            ReducedData = new List<InputData>();
            if(Data != null)
            {
                ReducedData = Data.Where(x => Convert.ToInt32(x.Time) % 600 == 0).ToList();
                Console.WriteLine("Reduced data created");
            }

        }

        public SFScatterSeries GetPointSeries(IEnumerable dataSource, string label, string xPath, string yPath, UIColor color,
                            bool needAxisX, int minX, int maxX,
                            bool needAxisY, int minY, int maxY,
                                      bool xAxisVisible = true, bool xAxisShowGridlines = false, bool xAxisIsLogarithmic = false,
                             bool yAxisVisible = true, bool yAxisShowGridlines = false, bool yAxisIsLogarithmic = false)
        {
            var series = new SFScatterSeries();
            series.ItemsSource = dataSource;
            series.Label = label;
            series.VisibleOnLegend = false;
            //humditySeries.ItemsSource = Data;
            series.EnableTooltip = true;
            //series.EnableAnimation = true;
            series.XBindingPath = xPath;
            series.YBindingPath = yPath;
            series.Color = color;
            if (needAxisX)
            {
                if (!xAxisIsLogarithmic)
                {
                    var axiX = new SFNumericalAxis()
                    {
                        Maximum = new NSNumber(maxX),
                        Minimum = new NSNumber(minX),
                        ShowMajorGridLines = xAxisShowGridlines,
                        Visible = xAxisVisible
                    };

                    series.XAxis = axiX;
                }
                else
                {
                    var axiX = new SFLogarithmicAxis()
                    {
                        Maximum = new NSNumber(maxY),
                        Minimum = new NSNumber(minY),
                        Interval = new NSNumber(100),
                        OpposedPosition = true,
                        Visible = xAxisVisible,
                        ShowMajorGridLines = xAxisShowGridlines
                    };

                    series.XAxis = axiX;
                }

            }
            if (needAxisY)
            {
                if (!yAxisIsLogarithmic)
                {
                    var axixY = new SFNumericalAxis();
                    axixY.Maximum = new NSNumber(maxY);
                    axixY.Minimum = new NSNumber(minY);
                    axixY.Visible = yAxisVisible;
                    axixY.ShowMajorGridLines = yAxisShowGridlines;
                    series.YAxis = axixY;
                }
                else
                {
                    var axixY = new SFLogarithmicAxis();
                    axixY.Maximum = new NSNumber(maxY);
                    axixY.Minimum = new NSNumber(minY);
                    axixY.Interval = new NSNumber(100);
                    axixY.OpposedPosition = true;
                    axixY.Visible = yAxisVisible;
                    axixY.ShowMajorGridLines = yAxisShowGridlines;
                    series.YAxis = axixY;
                }
            }
            return series;
        }

        public SFLineSeries GetSeries(IEnumerable dataSource, string label, string xPath, string yPath, UIColor color,
                             bool needAxisX, int minX, int maxX,
                             bool needAxisY, int minY, int maxY,
                              bool xAxisVisible = true, bool xAxisShowGridlines = false, bool xAxisIsLogarithmic = false,
                              bool yAxisVisible = true, bool yAxisShowGridlines = false, bool yAxisIsLogarithmic = false)
        {
            var series = new SFLineSeries();
            series.ItemsSource = dataSource;
            series.Label = label;
            //humditySeries.ItemsSource = Data;
            series.EnableTooltip = true;
            //series.EnableAnimation = true;
            series.XBindingPath = xPath;
            series.YBindingPath = yPath;
            series.Color = color;
            if (needAxisX)
            {
                if (!xAxisIsLogarithmic)
                {
                    var axiX = new SFNumericalAxis()
                    {
                        Maximum = new NSNumber(maxX),
                        Minimum = new NSNumber(minX),
                        ShowMajorGridLines = xAxisShowGridlines,
                        Visible = xAxisVisible
                    };

                    series.XAxis = axiX;
                }
                else
                {
                    var axiX = new SFLogarithmicAxis()
                    {
                        Maximum = new NSNumber(maxY),
                        Minimum = new NSNumber(minY),
                        Interval = new NSNumber(100),
                        OpposedPosition = true,
                        Visible = xAxisVisible,
                        ShowMajorGridLines = xAxisShowGridlines
                    };

                    series.XAxis = axiX;
                }
            }
            if (needAxisY)
            {
                if (!yAxisIsLogarithmic)
                {
                    var axixY = new SFNumericalAxis();
                    axixY.Maximum = new NSNumber(maxY);
                    axixY.Minimum = new NSNumber(minY);
                    axixY.Visible = yAxisVisible;
                    axixY.ShowMajorGridLines = yAxisShowGridlines;
                    series.YAxis = axixY;
                }
                else
                {
                    var axixY = new SFLogarithmicAxis();
                    axixY.Maximum = new NSNumber(maxY);
                    axixY.Minimum = new NSNumber(minY);
                    axixY.Interval = new NSNumber(100);
                    axixY.OpposedPosition = true;
                    axixY.Visible = yAxisVisible;
                    axixY.ShowMajorGridLines = yAxisShowGridlines;
                    series.YAxis = axixY;
                }
            }
            return series;
        }

        //protected void GetChartData()
        //{
        //    var appDelegate = UIApplication.SharedApplication.Delegate as AppDelegate;
        //    Data = appDelegate.ProfileData;

        //    //TemperaturePoints = new List<SFChartDataPoint>();
        //    //HumidityPoints = new List<SFChartDataPoint>();
        //    //PressurePoints = new List<SFChartDataPoint>();
        //    //WindSpeedPoints = new List<SFChartDataPoint>();
        //    //WindDirectionPoints = new List<SFChartDataPoint>();
        //    //AltitudePoints = new List<SFChartDataPoint>();
        //    //DewpointPoints = new List<SFChartDataPoint>();

        //    //foreach (var item in data)
        //    //{
        //    //    TemperaturePoints.Add(new SFChartDataPoint(){XValue = (Foundation.NSObject)item.Time,YValue = item.Temperature});
        //    //}
        //}

    }
}
