using Foundation;
using System;
using UIKit;
using Xamarin.SWRevealViewController;
using RestSharp;
using System.Collections.Generic;
using Newtonsoft.Json;
using GrawApp.Model;
using MapKit;
using CoreLocation;
using GrawApp.Database;
using Firebase.Auth;
using Firebase.Database;
using GrawApp.Controller;
using Syncfusion.SfBusyIndicator.iOS;
using System.Linq;
using GrawApp.Helper;
using CoreGraphics;
using GrawApp.FirebaseHelper;

namespace GrawApp
{
    public partial class StationInformationViewController : UIViewController
    {


        NSUserDefaults _plist;
        CLLocationManager locationManager = new CLLocationManager();
        public Station ActiveStation { get; private set; }
        TableSourceStationInformation TableSource = new TableSourceStationInformation();
       

        public UIActivityIndicatorView ActivityIndicator { get; set; }
        public UITableView TableView { get { return tableView; } }

        DateTime _startTime;
        DateTime _endTime;
        public StationInformationViewController(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            _plist = NSUserDefaults.StandardUserDefaults;
            var startDateEpoch = _plist.DoubleForKey("startDate");
            var endDateEpoch = _plist.DoubleForKey("endDate");

            if(startDateEpoch > 0.0 && endDateEpoch > 0.0)
            {
                _startTime = startDateEpoch.FromUnixTime();
                _endTime = endDateEpoch.FromUnixTime();
            }
            else
            {
                _startTime = DateTime.Now.AddDays(-7);
                _endTime = DateTime.Now.AddDays(2);
            }
            timeLabel.Text = $"{_startTime.ToShortDateString()}-{_endTime.ToShortDateString()}";

           
           
            InitializeActivityIndicator();
            TableSource.ViewController = this;
            menuButton.Clicked += (sender, e) => this.RevealViewController().RevealToggleAnimated(true);
            View.AddGestureRecognizer(this.RevealViewController().PanGestureRecognizer);
            var appDelegate = UIApplication.SharedApplication.Delegate as AppDelegate;
            ActiveStation = appDelegate.ActiveStation;
            tableView.Source = TableSource;

            locationManager.AuthorizationChanged += (sender, args) =>
            {
                Console.WriteLine("Authorization changed to: {0}", args.Status);
            };
            locationManager.RequestWhenInUseAuthorization();
            GetAnnotationOfStation();
            if (ActiveStation != null)
            {
                WeatherInformation.GetWeatherData(double.Parse(ActiveStation.Latitude), double.Parse(ActiveStation.Longitude),
                                                  (WeatherData data) =>
                                                  {
                                                      data.IconName = data.GetWeatherIcon(data.Condition);
                                                      InvokeOnMainThread(() =>
                                                      {
                                                          UpdateUIWeatherInformation(data);
                                                      });
                                                  });
               // GetWeatherData(double.Parse(ActiveStation.Latitude), double.Parse(ActiveStation.Longitude));
            }

            InitializeDatabase();


        }

        void InitializeActivityIndicator()
        {
            ActivityIndicator = new UIActivityIndicatorView(UIActivityIndicatorViewStyle.WhiteLarge);
            ActivityIndicator.Frame = new CGRect(150, 150, 40, 40);
            ActivityIndicator.Center = View.Center;
            View.AddSubview(ActivityIndicator);
            ActivityIndicator.BringSubviewToFront(View);
            //UIApplication.SharedApplication.NetworkActivityIndicatorVisible = true;

            ActivityIndicator.Color = UIColor.Gray;
        }

        public void UpdateTableView()
        {
            tableView.ReloadData();
        }

        #region Position Annotation
        void GetAnnotationOfStation()
        {
            if (ActiveStation != null)
            {
                double result;
                MKPointAnnotation anno = new MKPointAnnotation();
                var coord = anno.Coordinate;
                if (double.TryParse(ActiveStation.Latitude, out result))
                {
                    //anno.Coordinate.Latitude = result;
                    coord.Latitude = result;
                }

                if (double.TryParse(ActiveStation.Longitude, out result))
                {
                    //anno.Coordinate.Longitude = result;
                    coord.Longitude = result;
                }
                anno.Coordinate = coord;
                mapView.AddAnnotation(anno);

                var region = MKCoordinateRegion.FromDistance(anno.Coordinate, 1000, 1000);
                mapView.SetRegion(region, true);


            }
        }

        #endregion

        #region Firebase

        void UpdateTableData(FlightContent data)
        {
            InvokeOnMainThread(() =>
            {
                TableSource.FlightList.Add(data);
                tableView.ReloadData();
                View.SetNeedsDisplay();
                ActivityIndicator.StopAnimating();
                Console.WriteLine("Flight added");
            });
        }

        void DeleteTableDate(FlightContent data)
        {
            var index = TableSource.FlightList.FindIndex(x => x.Key == data.Key);
            InvokeOnMainThread(() =>
            {
                //TableSource.FlightList.RemoveAt(index);
                //tableView.BeginUpdates();
                tableView.ReloadData();
                View.SetNeedsDisplay();
                //tableView.EndUpdates();
                ActivityIndicator.StopAnimating();
                Console.WriteLine("Flight deleted");
            });
        }

        void InitializeDatabase()
        {
            //BusyIndicator.Hidden = false;
            ActivityIndicator.StartAnimating();
            TableSource.FlightList.Clear();
            tableView.ReloadData();


            var fireBaseHelper = new FireBaseHelper();

            fireBaseHelper.StartFlightFromStationObserver(UpdateTableData, DeleteTableDate,
                                                          ActiveStation.Key,
                                                          _startTime, _endTime);
            

            //var rootNode = Firebase.Database.Database.DefaultInstance.GetRootReference();
            //var childNode = rootNode.GetChild("station").GetChild(ActiveStation.Key).GetChild("flights");
            //                       /* .GetQueryOrderedByChild("EpochTime")
            //                        .GetQueryStartingAtValue(NSObject.FromObject(_startTime.GetUnixEpoch()))
            //                        .GetQueryEndingAtValue(NSObject.FromObject(_endTime.GetUnixEpoch()));*/
            
            //var referenceNode = childNode.ObserveEvent(DataEventType.ChildAdded, (snapshot, prevKey) =>
            //{
            //    var flightData = GetFlightDataFromSnapshot(snapshot);
            //    InvokeOnMainThread(() =>
            //    {
            //        TableSource.FlightList.Add(flightData);
            //        tableView.ReloadData();
            //        View.SetNeedsDisplay();
            //        ActivityIndicator.StopAnimating();
            //        Console.WriteLine("Flight added");
            //    });
            //});

            //var changeNode = childNode.ObserveEvent(DataEventType.Value, (snapshot, prevKey) =>
            //{
            //    if (snapshot == null || !snapshot.HasChildren)
            //    {
            //        InvokeOnMainThread(() =>
            //        {
            //            ActivityIndicator.StopAnimating();
            //            Console.WriteLine("Flight added");
            //        });
            //    }
            //});


            //var deleteNode = childNode.ObserveEvent(DataEventType.ChildRemoved, (snapshot, prevKey) =>
            //{
            //    var flightData = GetFlightDataFromSnapshot(snapshot);
            //    var index = TableSource.FlightList.FindIndex(x => x.Key == flightData.Key);
            //    InvokeOnMainThread(() =>
            //    {
            //        //TableSource.FlightList.RemoveAt(index);
            //        //tableView.BeginUpdates();
            //        tableView.ReloadData();
            //        View.SetNeedsDisplay();
            //        //tableView.EndUpdates();
            //        ActivityIndicator.StopAnimating();
            //        Console.WriteLine("Flight deleted");
            //    });

            //});
        }

        //private  FlightContent GetFlightDataFromSnapshot(DataSnapshot snapshot)
        //{
        //    var data = snapshot.GetValue<NSDictionary>();
        //    Console.WriteLine(snapshot.Key);
        //    FlightContent flightData = new FlightContent();
        //    flightData.Key = snapshot.Key;
        //    if (data["Date"] != null)
        //        flightData.Date = Convert.ToString(data["Date"]);
        //    if (data["Time"] != null)
        //        flightData.Time = Convert.ToString(data["Time"]);
        //    if (data["FileName"] != null)
        //        flightData.FileName = Convert.ToString(data["FileName"]);
        //    if (data["Url"] != null)
        //        flightData.Url = Convert.ToString(data["Url"]);
        //    if (data["Url100"] != null)
        //        flightData.Url100 = Convert.ToString(data["Url100"]);
        //    if (data["UrlEnd"] != null)
        //        flightData.UrlEnd = Convert.ToString(data["UrlEnd"]);
        //    if (data["IsRealTimeDataAvailable"] != null)
        //    {
        //        if (data["IsRealTimeDataAvailable"] is NSNumber)
        //        {
        //            var x = (NSNumber)data["IsRealTimeDataAvailable"];
        //            var ob = x.BoolValue;
        //            flightData.IsRealTimeFlight = Convert.ToBoolean(ob);
        //        }


        //    }
        //    if (data["EpochTime"] != null)
        //    {
        //        if (data["EpochTime"] is NSNumber)
        //        {
        //            var x = (NSNumber)data["EpochTime"];
        //            var ob = x.DoubleValue;
        //            flightData.EpochTime = Convert.ToDouble(ob);
        //        }
        //    }

        //    return flightData;
        //}

        public override void PrepareForSegue(UIStoryboardSegue segue, NSObject sender)
        {
            base.PrepareForSegue(segue, sender);

            //var viewController = (StationDataTabBarController)segue.DestinationViewController;
            //if(viewController != null)
            //{

            //}

        }

        #endregion


        #region Weather data from open weather API
        //void GetWeatherData(double latitude, double longitude)
        //{
        //    var uri = $"{WEATHER_URL}?lat={latitude}&long={longitude}&appip={APP_ID}";
        //    var client = new RestClient(WEATHER_URL);

        //    var request = new RestRequest();
        //    request.AddQueryParameter("lat", latitude.ToString());
        //    request.AddQueryParameter("lon", longitude.ToString());
        //    request.AddQueryParameter("appid", APP_ID);

        //    //var response = client.Execute(request);
        //    WeatherData data = new WeatherData();
        //    client.ExecuteAsync(request, response =>
        //    {
        //        Console.WriteLine(response.Content);
        //        RestSharp.Deserializers.JsonDeserializer deserial = new RestSharp.Deserializers.JsonDeserializer();
        //        //dynamic results = JsonConvert.DeserializeObject(response.Content);
        //        //var results = JsonConvert.DeserializeObject<Dictionary<string,string[]>>(response.Content);

        //        //  var coord = results["coord"];
        //        //var lon = coord["lon"];
        //        //Console.WriteLine($"longitude:{lon.Value}");
        //        //var main = results["main"][0];


        //        var jsonObject = deserial.Deserialize<Dictionary<string, string>>(response);
        //        foreach (var item in jsonObject)
        //        {
        //            Console.WriteLine($"{item.Key} {item.Value}");

        //            if (item.Key == "name")
        //            {
        //                Console.WriteLine($"City: {item.Value}");
        //                data.City = item.Value;
        //            }

        //            if (item.Key == "weather")
        //            {
        //                var res = JsonConvert.DeserializeObject<List<Dictionary<string, string>>>(item.Value);
        //                Console.WriteLine($"{res[0]["id"]}");
        //                data.Condition = int.Parse(res[0]["id"]);
        //            }

        //            if (item.Key == "main")
        //            {
        //                var res = JsonConvert.DeserializeObject<Dictionary<string, string>>(item.Value);
        //                Console.WriteLine($"Temperature is: {res["temp"]}");

        //                data.Temperature = (res["temp"]).ToDouble() - 273.15;
        //            }
        //            //Console.WriteLine("Stop");
        //        }
        //        //var myObj = jsonObject["main"];

        //        //Console.WriteLine(jsonObject["main"]);
        //        data.IconName = data.GetWeatherIcon(data.Condition);
        //        InvokeOnMainThread(() =>
        //        {
        //            UpdateUIWeatherInformation(data);
        //        });
        //    });

        //}

        void UpdateUIWeatherInformation(WeatherData data)
        {
            weatherImage.Image = new UIImage($"{data.IconName}.png");
            temperatureLabel.Text = $"{data.Temperature:0.0} °C";
            cityLabel.Text = data.City;
        }
        #endregion

        #region Calendar
        partial void calendarButtonTapped(UIKit.UIButton sender)
        {
            Console.WriteLine("Calendar Tapped");
            var startTime = _startTime;
            var dialog = new DatePickerDialog();
            dialog.Show("Start date", "Done", "Cancel", UIDatePickerMode.Date, (dt)=>{

                _startTime = dt;
                var endDialog = new DatePickerDialog();
                endDialog.Show("End date", "Done", "Cancel", UIDatePickerMode.Date, (endTime) => {

                    _endTime = endTime;
                    if(_startTime > _endTime)
                    {
                        _endTime = _startTime.AddDays(7);
                    }
                    timeLabel.Text = $"{_startTime.ToShortDateString()}-{_endTime.ToShortDateString()}";
                    _plist.SetDouble(_startTime.GetUnixEpoch(), "startDate");
                    _plist.SetDouble(_endTime.GetUnixEpoch(), "endDate");

                     InitializeDatabase();


                }, _endTime);

            }, startTime);
        }
        #endregion



    }
    #region Table Source
    internal class TableSourceStationInformation : UITableViewSource
    {
        NSIndexPath DeleteIndexPath;
        public List<FlightContent> FlightList { get; set; } = new List<FlightContent>();
        public StationInformationViewController ViewController { get; set; }
        public override nint RowsInSection(UITableView tableview, nint section)
        {
            return FlightList.Count;
        }


        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            var cell = tableView.DequeueReusableCell("cellId") as UITableViewCell;

            var dateString = "Date".GetLocalString();
            var timeString = "Time".GetLocalString();
            cell.TextLabel.Text = $"{dateString}: {FlightList[indexPath.Row].EpochTime.FromUnixTime():dd.MM.yyyy}";//  {FlightList[indexPath.Row].Time}";
            cell.DetailTextLabel.Text = $"{timeString}: {FlightList[indexPath.Row].EpochTime.FromUnixTime():HH:mm} UTC";
            if(FlightList[indexPath.Row].IsRealTimeFlight)
            {
                cell.TextLabel.TextColor = UIColor.Red;
            }
            else
            {
                cell.TextLabel.TextColor = UIColor.Black;
            }

            //cell.DetailTextLabel.Text = !IsFiltered ? StationItems[indexPath.Row].Name : FilteredItems[indexPath.Row].Name;
            return cell;
        }

        public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
        {
            
            var flight = FlightList[indexPath.Row];
            ViewController.ActivityIndicator.StartAnimating();

            if(flight.IsRealTimeFlight)
            {
                var appDelegate = UIApplication.SharedApplication.Delegate as AppDelegate;
                appDelegate.CurrentFlight = flight;
                ViewController.ActivityIndicator.StopAnimating();
                GotoRawPage();
                return;
            }

            var inputDataList = DatabaseHelper.GetFlightData(flight.Key);
           
            if (inputDataList != null)
            {
                var appDelegate = UIApplication.SharedApplication.Delegate as AppDelegate;
                appDelegate.CurrentFlight = inputDataList;
                appDelegate.ProfileData = new List<InputData>(inputDataList.InputDataList);
                Console.WriteLine("Input data available");
                ViewController.ActivityIndicator.StopAnimating();
                GotoNextPage();
                return;
            }

            var client = new RestClient(flight.Url);
            var request = new RestRequest();


            //var response = client.Execute(request);
            WeatherData data = new WeatherData();
            client.ExecuteAsync(request, response =>
            {
                // Console.WriteLine(response.Content);
                var inputController = new InputDataController(response.Content);
                //add data into database first.....
                if (!string.IsNullOrEmpty(response.Content))
                {
                    var flightData = DatabaseHelper.InsertFlight(flight, inputController.InputDataList);
                    var appDelegate = UIApplication.SharedApplication.Delegate as AppDelegate;
                    appDelegate.ProfileData = inputController.InputDataList;
                    appDelegate.CurrentFlight = flightData;
                    Console.WriteLine("OK");
                    InvokeOnMainThread(() =>
                    {
                        ViewController.ActivityIndicator.StopAnimating();
                        GotoNextPage();
                    });
                }


            });
        }

        private void GotoRawPage()
        {
            ViewController.PerformSegue("gotoRaw", null);
        }

        public override void CommitEditingStyle(UITableView tableView, UITableViewCellEditingStyle editingStyle, NSIndexPath indexPath)
        {
            switch (editingStyle)
            {
                case UITableViewCellEditingStyle.Delete:
                    var flightData = FlightList[indexPath.Row];
                    DeleteIndexPath = indexPath;
                    ConfirmDelete(flightData,tableView);
                    break;
                default:
                    break;
            } 
        }

        void ConfirmDelete(FlightContent flight, UITableView tableView)
        {
            var confirmationString = "FLIGHT_DELETE_CONFIRMATION".GetLocalString().Replace("###REPLACE###", $"{flight.Date}/{flight.Time}");
            var okCancelAlertController = UIAlertController.Create("Delete flight".GetLocalString(),
                                                                   confirmationString,
                                                                   UIAlertControllerStyle.Alert);

            //Add Actions
            okCancelAlertController.AddAction(UIAlertAction.Create("OK".GetLocalString(),
                                                                   UIAlertActionStyle.Default,
                                                                   alert => {
                                                                       Console.WriteLine("Okay was clicked");
                DeleteFlight(flight,tableView);
            }));
            okCancelAlertController.AddAction(UIAlertAction.Create("Cancel".GetLocalString(),
                                                                   UIAlertActionStyle.Cancel,
                                                                   alert => Console.WriteLine("Cancel was clicked")));

            //Present Alert
            ViewController.PresentViewController(okCancelAlertController, true, null);

        }

        void DeleteFlight(FlightContent flight,UITableView tableView)
        {
            if (DatabaseHelper.DeleteFlight(flight))
            {
                Console.WriteLine($"Flight with key: {flight.Key} was deleted");
                FirebaseHelper.FireBaseHelper.DeleteFlightFromCloud(flight, ViewController.ActiveStation.Key);
                FlightList.RemoveAt(DeleteIndexPath.Row);
                tableView.BeginUpdates();
                tableView.DeleteRows(new NSIndexPath[] { DeleteIndexPath }, UITableViewRowAnimation.Automatic);
                tableView.ReloadData();
                tableView.EndUpdates();
            }
        }

        private void GotoNextPage()
        {
            ViewController.PerformSegue("gotoChartView",null);
        }
    }
#endregion
}