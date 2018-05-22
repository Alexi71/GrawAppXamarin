using System;
using System.Collections.Generic;
using GrawApp.Model;
using Newtonsoft.Json;

namespace GrawApp.Controller
{
    public class InputDataController
    {
        public List<InputData> InputDataList { get; set; }
        string JsonData;
        public InputDataController(string jsonData)
        {
            JsonData = jsonData;
            CreateDataFromJson();
        }

        private void CreateDataFromJson()
        {
            InputDataList = JsonConvert.DeserializeObject<List<InputData>>(JsonData);

        }
    }
}
