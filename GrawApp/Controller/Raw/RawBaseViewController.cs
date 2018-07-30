using System;
using GrawApp.Controller.Raw;
using GrawApp.Model;
using UIKit;

namespace GrawApp.Controller
{
    public partial class RawBaseViewController : UIViewController, ISetData<RawData>
    {


        /*RawDataHandler DataHandler;*/
        public RawBaseViewController(IntPtr handle) : base(handle)
        {
        }
        /*
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            var appDelegate = UIApplication.SharedApplication.Delegate as AppDelegate;
            var station = appDelegate.ActiveStation;
            var flighData = appDelegate.CurrentFlight;
            DataHandler = new RawDataHandler()
            {
                FlightKey = flighData.Key,
                StationKey = station.Key
            };
            DataHandler.InitializeDataBase();
            // Perform any additional setup after loading the view, typically from a nib.
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }*/
        public virtual void SetData(RawData data)
        {
            
        }
    }
}

