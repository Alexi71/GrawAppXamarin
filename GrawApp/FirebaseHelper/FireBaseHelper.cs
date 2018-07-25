using System;
using Firebase.Storage;
using GrawApp.Model;

namespace GrawApp.FirebaseHelper
{
    public class FireBaseHelper
    {
        public FireBaseHelper()
        {
        }

        public static void DeleteFlightFromCloud(FlightContent data, string stationKey)
        {

            var storage = Storage.DefaultInstance;
            if (!string.IsNullOrEmpty(data.Url))
            {


                var urlDeleted = storage.GetReferenceFromUrl(data.Url);
                urlDeleted.Delete((error) =>
                {
                    if (error != null)
                    {
                        // Uh-oh, an error occurred!
                        Console.WriteLine($"Delete file :{data.Url} failed");
                    }
                    else
                    {
                        // File deleted successfully
                        Console.WriteLine($"Delete file :{data.Url} was successfully");
                    }
                });
            }
            if (!string.IsNullOrEmpty(data.Url100))
            {
                var url100Deleted = storage.GetReferenceFromUrl(data.Url100);
                url100Deleted.Delete((error) =>
                {
                    if (error != null)
                    {
                        // Uh-oh, an error occurred!
                        Console.WriteLine($"Delete file :{data.Url100} failed");
                    }
                    else
                    {
                        // File deleted successfully
                        Console.WriteLine($"Delete file :{data.Url100} was successfully");
                    }
                });
            }

            if (!string.IsNullOrEmpty(data.UrlEnd))
            {
                var urlEndDeleted = storage.GetReferenceFromUrl(data.UrlEnd);
                urlEndDeleted.Delete((error) =>
                {
                    if (error != null)
                    {
                            // Uh-oh, an error occurred!
                        Console.WriteLine($"Delete file :{data.UrlEnd} failed");
                        }
                    else
                    {
                            // File deleted successfully
                        Console.WriteLine($"Delete file :{data.UrlEnd} was successfully");
                        }
                });
            }
                                        
            var rootNode = Firebase.Database.Database.DefaultInstance.GetRootReference();
            rootNode.GetChild("station").GetChild(stationKey)
                    .GetChild("flights").GetChild(data.Key).RemoveValue((error, reference) =>
            {
                if (error != null)
                {
                    //do something
                    Console.WriteLine($"Delete flight with key:{data.Key} failed");
                }
                else
                {
                    Console.WriteLine($"Delete flight with key:{data.Key} was successfully");
                }
            });


        }
    }
}
