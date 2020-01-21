using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;

namespace WebServer.Controllers
{
    public class WeatherController : SuperController
    {
        #region 获取天气信息
        public JsonResult GetWeather()
        {
            object result = null;
            try
            {
                //参数ip是淘宝ip地址库特定的参数
                string strRegion = Common.HandleHttp.HttpGet("http://ip.taobao.com/service/getIpInfo.php", "ip=myip");
                //将json数据转化为动态类型
                dynamic ObjRegion = Common.HandleJson.JsonToDynamic(strRegion);
                //获取城市
                string city = string.Format("{0}{1}", ObjRegion.data.region, ObjRegion.data.city);
                //读取xml，获取城市的代码
                string path = string.Format(@"{0}\Models\weatherConfig.xml", Models.SharedData.ProjectPath);
                XDocument document = XDocument.Load(path);
                XElement element = document.Descendants("City").Where(m => m.Attribute("FullName").Value == city).FirstOrDefault();
                //根据城市代码获取天气
                string url = string.Format("http://t.weather.sojson.com/api/weather/city/{0}", element.Attribute("Code").Value);
                string strWeather = Common.HandleHttp.HttpGet(url, null);
                //将天气信息转换为实体数据
                Models.Weather.Root weatherInfo = Common.HandleJson.DeserializeJsonToObject<Models.Weather.Root>(strWeather);
                //图表数据
                List<string> date = new List<string>();
                List<int> hight = new List<int>();
                List<int> low = new List<int>();
                //表格数据
                List<object> list = new List<object>();

                foreach (var item in weatherInfo.data.forecast)
                {
                    date.Add(item.ymd);
                    hight.Add(int.Parse(new BLL.ServiceOther().GetTemperature(item.high)));
                    low.Add(int.Parse(new BLL.ServiceOther().GetTemperature(item.low)));

                    list.Add(new
                    {
                        date = item.ymd,
                        item.week,
                        item.sunrise,
                        item.sunset,
                        item.fx,
                        item.fl,
                        item.aqi,
                        item.type
                    });
                }
                result = new
                {
                    city,
                    map = new
                    {
                        date,
                        hight,
                        low
                    },
                    table = list

                };
            }
            catch (Exception ex)
            {
                errorCode = 10001;
                new BLL.ServiceException().AddExceptionLog(ex);
            }
            if (errorCode != 0)
                error = new BLL.ServiceError().GetErrorInfo(errorCode);
            return Json(errorCode, error, result, true);
        }
        #endregion

    }
}