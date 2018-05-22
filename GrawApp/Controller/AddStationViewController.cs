using Foundation;
using System;
using UIKit;
using GrawApp.Database;
using System.Collections.Generic;
using Firebase.Auth;
using Firebase.Database;
using System.Linq;
using Xamarin.SWRevealViewController;

namespace GrawApp
{
    public partial class AddStationViewController : UIViewController
    {
        Station ActiveStation;
        private TableSourceNewSation Source = new TableSourceNewSation();
        public AddStationViewController (IntPtr handle) : base (handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            Source.BaseController = this;
            tableView.Source = Source;
            ActiveStation = DatabaseHelper.GetDefaultStation(Auth.DefaultInstance.CurrentUser.Uid);
            menuButton.Clicked += (sender, e) => this.RevealViewController().RevealToggleAnimated(true);
            View.AddGestureRecognizer(this.RevealViewController().PanGestureRecognizer);
                                      
            searchBar.SearchButtonClicked += (sender, e) =>
            {
                Search();
            };

            searchBar.TextChanged += (sender, e) =>
            {
                //Search();
            };
            InitializeDatabase();

        }
        void Search()
        {
            Console.WriteLine(searchBar.Text);
            if (!string.IsNullOrEmpty(searchBar.Text))
            {
                Source.FilteredItems = Source.StationItems.
                    Where(x => x.City == searchBar.Text ||
                          x.Name == searchBar.Text ||
                         x.Name.IndexOf(searchBar.Text,StringComparison.InvariantCultureIgnoreCase) != -1 ||
                          x.City.IndexOf(searchBar.Text, StringComparison.InvariantCultureIgnoreCase) != -1).ToList();
                Source.IsFiltered = true;
            }
            else 
            {
                Source.IsFiltered = false;
            }
            tableView.ReloadData();
        }

        void InitializeDatabase()
        {
            var rootNode = Firebase.Database.Database.DefaultInstance.GetRootReference();
            var childNode = rootNode.GetChild("station");
            var referenceNode = childNode.ObserveEvent(DataEventType.ChildAdded, (snapshot, prevKey) =>
             {
                 var data = snapshot.GetValue<NSDictionary>();
                 var station = new Station();
                 if ((ActiveStation != null && 
                      ActiveStation.Key != snapshot.Key) || 
                     ActiveStation == null)
                 {
                     station.Key = snapshot.Key;
                     station.Id = Convert.ToInt32(data["Id"].ToString());
                     station.Name = data["Name"].ToString();
                     station.City = data["City"].ToString();
                     station.Country = data["Country"].ToString();
                     station.Longitude = data["Longitude"].ToString();
                     station.Latitude = data["Latitude"].ToString();
                     station.Altitude = data["Altitude"].ToString();
                     Source.StationItems.Add(station);
                     DatabaseHelper.InsertStation(station);
                     tableView.ReloadData();
                 }
             });
        }
    }



    public class TableSourceNewSation:UITableViewSource
    {
        public List<Station> StationItems { get; set; }= new List<Station>();
        public List<Station> FilteredItems { get; set; }= new List<Station>();
        public bool IsFiltered { get; set; } = false;
        public UIViewController BaseController { get; set; }



        public override nint RowsInSection(UITableView tableview, nint section)
        {
            return !IsFiltered?StationItems.Count:FilteredItems.Count;
        }

        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            var cell = tableView.DequeueReusableCell("cellId") as UITableViewCell;
            cell.TextLabel.Text = !IsFiltered ?StationItems[indexPath.Row].City:FilteredItems[indexPath.Row].City;
            cell.DetailTextLabel.Text = !IsFiltered ? StationItems[indexPath.Row].Name : FilteredItems[indexPath.Row].Name;
            return cell;
        }

        public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
        {
            var station = new Station();
            if (IsFiltered)
            {
                station = FilteredItems[indexPath.Row];
            }
            else
            {
                station = StationItems[indexPath.Row];
            }

            var userId = Auth.DefaultInstance.CurrentUser.Uid;

            DatabaseHelper.SetDefaultUserStation(userId,station.Key);
            var appDelegate = UIApplication.SharedApplication.Delegate as AppDelegate;
            appDelegate.ActiveStation = station;
            var storyboard = UIStoryboard.FromName("Main", null);
            var nc = storyboard.InstantiateViewController("StationNavigation") as UINavigationController;
            BaseController.RevealViewController().PushFrontViewController(nc, true);
        }
    }
}