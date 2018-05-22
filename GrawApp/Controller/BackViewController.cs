using Foundation;
using System;
using UIKit;
using System.Collections.Generic;
using GrawApp.Database;
using Firebase.Auth;
using Xamarin.SWRevealViewController;

namespace GrawApp
{
    public partial class BackViewController : UIViewController
    {
        
        public BackViewController (IntPtr handle) : base (handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            var dataSource = new TableSource();
            TableView.Source = dataSource;
            dataSource.ParentController = this;
            TableView.BackgroundColor = new UIColor(2 / 255f, 64 / 255f, 123 / 255f, 1.0f);
            this.View.BackgroundColor = new UIColor(2 / 255, 64 / 255, 123 / 255, 1.0f);

        }




    }

    public class TableSource:UITableViewSource
    {
        List<String> menuData = new List<string>() { "add", "settings", "logout" };
        List<Station> stationData;// = new List<string>() { "Nürnberg", "Rostock" };
        public UIViewController ParentController { get; set; }
        Station DefaultStation;
        public TableSource() => stationData = DatabaseHelper.ReadStations();
        public override nint NumberOfSections(UITableView tableView)
        {
            return 2;
        }
        public override nint RowsInSection(UITableView tableview, nint section)
        {
            if(section == 0)
            {
                return menuData.Count;
            }
            else
            {
                return stationData.Count;
            }
        }

        public override UITableViewCell GetCell(UITableView tableView,
                                                NSIndexPath indexPath)
        {
            if (indexPath.Section == 0)
            {
                var cell = tableView.DequeueReusableCell(menuData[indexPath.Row]) as UITableViewCell;
                cell.BackgroundColor = new UIColor(2 / 255f, 64 / 255f, 123 / 255f, 1.0f);
                cell.TextLabel.TextColor = UIColor.White;
                return cell;
            }
            else
            {
                
                var cell = tableView.DequeueReusableCell("cellStation") as UITableViewCell;
                cell.TextLabel.Text = stationData[indexPath.Row].City;
                cell.BackgroundColor = new UIColor(2 / 255f, 64 / 255f, 123 / 255f, 1.0f);

                cell.TextLabel.TextColor = UIColor.White;
                DefaultStation = DatabaseHelper.GetDefaultStation(Auth.DefaultInstance.CurrentUser.Uid);
                if (DefaultStation?.Key == stationData[indexPath.Row].Key)
                {
                    cell.ImageView.Image = new UIImage("005-facebook-placeholder-for-locate-places-on-maps.png");
                }
                else
                {
                    cell.ImageView.Image = new UIImage("004-placeholder.png");
                }
                return cell;
            }
            //return new UITableViewCell();
		
        }

        public override string TitleForHeader(UITableView tableView, nint section)
        {
            if (section == 0)
            {
                return "SETTINGS";
            }
            else
            {
                return "STATIONS";
            }
        }

        public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
        {
           if(indexPath.Section == 0)
            {
                var menuItem = menuData[indexPath.Row];
                switch (menuItem)
                {
                    case "logout":
                        {
                            var storyboard = UIStoryboard.FromName("Main", null);
                            var nc = storyboard.InstantiateViewController("home") as UINavigationController;
                            ParentController.RevealViewController().PushFrontViewController(nc, true);
                        }
                        break;
                    case "add":
                        {
                            var storyboard = UIStoryboard.FromName("Main", null);
                            var nc = storyboard.InstantiateViewController("addStationNavigation") as UINavigationController;
                            ParentController.RevealViewController().PushFrontViewController(nc, true);
                        }
                        break;
                    default:
                        break;
                }
            }
            else
            {
                var storyboard = UIStoryboard.FromName("Main", null);
                var nc = storyboard.InstantiateViewController("StationNavigation") as UINavigationController;
                var item = stationData[indexPath.Row];
                var appDelegate = UIApplication.SharedApplication.Delegate as AppDelegate;
                appDelegate.ActiveStation = item;
                if(DefaultStation.Key != item.Key)
                {
                    DatabaseHelper.SetDefaultUserStation(Auth.DefaultInstance.CurrentUser.Uid,item.Key);
                }
                ParentController.RevealViewController().PushFrontViewController(nc, true);
            }
        }
    }

}