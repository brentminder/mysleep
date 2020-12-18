using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MySleep
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
    public class OuraSleep
    {
        public int awake { get; set; }
        public DateTime bedtime_end { get; set; }
        public int bedtime_end_delta { get; set; }
        public DateTime bedtime_start { get; set; }
        public int bedtime_start_delta { get; set; }
        public double breath_average { get; set; }
        public int deep { get; set; }
        public int duration { get; set; }
        public int efficiency { get; set; }
        public List<int> hr_5min { get; set; }
        public double hr_average { get; set; }
        public int hr_lowest { get; set; }
        public string hypnogram_5min { get; set; }
        public int is_longest { get; set; }
        public int light { get; set; }
        public int midpoint_at_delta { get; set; }
        public int midpoint_time { get; set; }
        public int onset_latency { get; set; }
        public int period_id { get; set; }
        public int rem { get; set; }
        public int restless { get; set; }
        public int rmssd { get; set; }
        public List<int> rmssd_5min { get; set; }
        public int score { get; set; }
        public int score_alignment { get; set; }
        public int score_deep { get; set; }
        public int score_disturbances { get; set; }
        public int score_efficiency { get; set; }
        public int score_latency { get; set; }
        public int score_rem { get; set; }
        public int score_total { get; set; }
        public string summary_date { get; set; }
        public double temperature_delta { get; set; }
        public double temperature_deviation { get; set; }
        public double temperature_trend_deviation { get; set; }
        public int timezone { get; set; }
        public int total { get; set; }
    }

    public class Root
    {
        public List<OuraSleep> sleep { get; set; }
    }

    public class Sleep 
    {
        private Sleep() { }

        public Sleep(OuraSleep ouraSleep) {
            OuraSleep = ouraSleep;
        }
        public OuraSleep OuraSleep { get; set; }
        
        //Date | Sleep Score | Total Sleep Time  | Time In Bed | Sleep Start Time | Sleep End TIme | REM(mins) | Deep(mins) | Lat ency(mins)
        /// <summary>
        /// 
        /// </summary>
        public string SleepDate { get { return OuraSleep.bedtime_end.ToString("MM/dd/yyyy"); }  }
        
        /// <summary>
        /// Sleep score represents overall sleep quality during the sleep period. It is calculated as a weighted average of sleep score contributors that represent one aspect of sleep quality each. The sleep score contributor values are also available as separate parameters.
        /// </summary>
        public int Score { get { return OuraSleep.score; } }


        /// <summary>
        /// Local time when the sleep period started
        /// </summary>
        public string BedtimeStart { get { return OuraSleep.bedtime_start == DateTime.MinValue ? "" : OuraSleep.bedtime_start.ToString("HH:mm"); } }
        
        /// <summary>
        /// Local time when the sleep period ended
        /// </summary>
        public string BedtimeEnd { get { return OuraSleep.bedtime_end == DateTime.MinValue ? "" : OuraSleep.bedtime_end.ToString("HH:mm"); } }

        /// <summary>
        /// Total duration of the sleep period (sleep.duration = sleep.bedtime_end - sleep.bedtime_start)
        /// </summary>
        //public int Duration { get { return (int)(OuraSleep.duration / 60); } }
        public string Duration { get { return Math.Round((double)(OuraSleep.duration / 60d / 60d), 2, MidpointRounding.ToEven).ToString(); } }
        
        /// <summary>
        /// Total amount of sleep registered during the sleep period (sleep.total = sleep.rem + sleep.light + sleep.deep).
        /// </summary>
        public string TotalSleep { get { return Math.Round((decimal)(OuraSleep.total / 60d / 60d), 2, MidpointRounding.ToEven).ToString(); } }
        
        /// <summary>
        /// Total amount of REM sleep registered during the sleep period.
        /// </summary>
        public int Rem { get { return (int)(OuraSleep.rem / 60); } }
        
        /// <summary>
        /// Total amount of deep (N3) sleep registered during the sleep period.
        /// </summary>
        public int Deep { get { return (int)(OuraSleep.deep / 60); } }

        /// <summary>
        /// Detected latency from bedtime_start to the beginning of the first five minutes of persistent sleep.
        /// </summary>
        public int Latency { get { return (int)(OuraSleep.onset_latency / 60); } }

        /// <summary>
        /// Represents sleep disturbances' contribution for sleep quality. Three separate measurements are used to calculate this contributor value: Wake-up count; Got-up count; Restless sleep (sleep.restless)
        /// </summary>
        public int Disturbances { get { return OuraSleep.score_disturbances; } }

    }

}
