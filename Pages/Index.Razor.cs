using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
//using Newtonsoft.Json;
using System.Text;
using Newtonsoft.Json;

namespace MySleep.Pages
{
    public partial class Index
    {
        public string Title = "Test";
                                
        public static string SleepData, SleepDataToday = "";

        public string tokenBrent = "735Q5REGAV3BLINQSNV5GX4T4ZRJDW2Y";
        public string tokenSabrina = "HTE4B2UTXFYIH5C52U6ETLNPKVHWXEN5";
        public string startDate = "";
        public string endDate = "";
        public int who = 1;
        private const string dateFormat = "yyyy-MM-dd";

        protected override async Task OnInitializedAsync()
        {
            startDate = DateTime.Now.AddDays(-6).ToString(dateFormat);
            endDate = DateTime.Now.ToString(dateFormat);
            if (SleepDataToday.Length == 0)
            {
                await getSleepDataOnClick();
            }
        }

        public async Task getSleepDataOnClick()
        {
            {
                var tokenSelected = who == 1 ? tokenBrent : tokenSabrina;
                var startDateMinusOne = DateTime.Parse(startDate).AddDays(-1).ToString(dateFormat);
                var uri = $"https://api.ouraring.com/v1/sleep?start={startDateMinusOne}&end={endDate}&access_token={tokenSelected}";
                try
                {
                    var httpClient = new HttpClient();

                    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    var response = await httpClient.GetAsync(uri).ConfigureAwait(false);

                    var rawSleep = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    var rawSleeps = JsonConvert.DeserializeObject<Root>(rawSleep);

                    var rows = new StringBuilder();
                    //rows.AppendLine("day\tscore\ttotalsleep\tbedtime_start\trem\tdeep\t");
                    var row = "";
                    foreach (var raw in rawSleeps.sleep)
                    {
                        var sleep = new Sleep(raw);
                        //                      Date       |     Sleep Score    |     Total Sleep Time    |       Time In Bed     |     Disturbances          |    Sleep Start Time      |     Sleep End Time      |     REM(mins)    |     Deep(mins)    |      Latency(mins)  
                        row = sleep.SleepDate + "\t" + sleep.Score + "\t" + sleep.TotalSleep + "\t" + sleep.Duration + "\t" + sleep.Disturbances + "\t" + sleep.BedtimeStart + "\t" + sleep.BedtimeEnd + "\t" + sleep.Rem + "\t" + sleep.Deep + "\t" + sleep.Latency;
                        rows.AppendLine(row);
                    }
                    SleepDataToday = row;
                    SleepData = rows.ToString();                    
                }
                catch (Exception ex)
                {
                    SleepData = ex.Message;
                }

            }

        }

    }

}
