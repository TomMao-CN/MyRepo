using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Weather
{
    //如果好用，请收藏地址，帮忙分享。
    public class CityInfo
    {
        /// <summary>
        /// 北京市
        /// </summary>
        public string city { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string citykey { get; set; }
        /// <summary>
        /// 北京
        /// </summary>
        public string parent { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string updateTime { get; set; }
    }

    public class ForecastItem
    {
        /// <summary>
        /// 
        /// </summary>
        public string date { get; set; }
        /// <summary>
        /// 高温 5℃
        /// </summary>
        public string high { get; set; }
        /// <summary>
        /// 低温 -7℃
        /// </summary>
        public string low { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ymd { get; set; }
        /// <summary>
        /// 星期一
        /// </summary>
        public string week { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string sunrise { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string sunset { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int aqi { get; set; }
        /// <summary>
        /// 北风
        /// </summary>
        public string fx { get; set; }
        /// <summary>
        /// <3级
        /// </summary>
        public string fl { get; set; }
        /// <summary>
        /// 晴
        /// </summary>
        public string type { get; set; }
        /// <summary>
        /// 愿你拥有比阳光明媚的心情
        /// </summary>
        public string notice { get; set; }
    }

    public class Yesterday
    {
        /// <summary>
        /// 
        /// </summary>
        public string date { get; set; }
        /// <summary>
        /// 高温 6℃
        /// </summary>
        public string high { get; set; }
        /// <summary>
        /// 低温 -4℃
        /// </summary>
        public string low { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ymd { get; set; }
        /// <summary>
        /// 星期日
        /// </summary>
        public string week { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string sunrise { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string sunset { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int aqi { get; set; }
        /// <summary>
        /// 西南风
        /// </summary>
        public string fx { get; set; }
        /// <summary>
        /// <3级
        /// </summary>
        public string fl { get; set; }
        /// <summary>
        /// 晴
        /// </summary>
        public string type { get; set; }
        /// <summary>
        /// 愿你拥有比阳光明媚的心情
        /// </summary>
        public string notice { get; set; }
    }

    public class Data
    {
        /// <summary>
        /// 
        /// </summary>
        public string shidu { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string pm25 { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string pm10 { get; set; }
        /// <summary>
        /// 优
        /// </summary>
        public string quality { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string wendu { get; set; }
        /// <summary>
        /// 各类人群可自由活动
        /// </summary>
        public string ganmao { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<ForecastItem> forecast { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Yesterday yesterday { get; set; }
    }

    public class Root
    {
        /// <summary>
        /// success感谢又拍云(upyun.com)提供CDN赞助
        /// </summary>
        public string message { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int status { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string date { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string time { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public CityInfo cityInfo { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Data data { get; set; }
    }

}
