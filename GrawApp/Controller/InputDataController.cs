using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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

        public InputDataController()
        {
            InputDataList = new List<InputData>();

        }

        private void CreateDataFromJson()
        {
            InputDataList = JsonConvert.DeserializeObject<List<InputData>>(JsonData);

        }

        public async Task<List<InputData>> GetListAsync(string json)
        {
            await Task.Run(() =>
            {
                InputDataList = JsonConvert.DeserializeObject<List<InputData>>(json);

            });

            return InputDataList;
        }
    }
}
