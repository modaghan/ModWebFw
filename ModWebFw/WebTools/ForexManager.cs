using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ModWebFw
{
    public static class ForexManager
    {
        public static string Date = "";

        public static List<CurrencyRate> Get(DateTime date)
        {
            string url = "http://www.tcmb.gov.tr/kurlar/" + date.ToString("yyyyMM") + "/" + date.ToString("ddMMyyyy") + ".xml";
            var forex = GetForexFromUrl(url);
            if (forex.Count > 0)
            {
                return forex;
            }
            else
            {
                return Get(date.AddDays(-1));
            }
        }

        private static List<CurrencyRate> GetForexFromUrl(string url = "http://www.tcmb.gov.tr/kurlar/today.xml")
        {
            List<CurrencyRate> Rates = new List<CurrencyRate>();
            //DataTable dt = new DataTable();
            // DataTable nesnemizi yaratıyoruz
            try
            {
                XmlTextReader rdr = new XmlTextReader(url);
                // XmlTextReader nesnesini yaratıyoruz ve parametre olarak xml dokümanın urlsini veriyoruz
                // XmlTextReader urlsi belirtilen xml dokümanlarına hızlı ve forward-only giriş imkanı sağlar.
                XmlDocument myxml = new XmlDocument();
                // XmlDocument nesnesini yaratıyoruz.
                myxml.Load(rdr);
                // Load metodu ile xml yüklüyoruz
                XmlNode tarih = myxml.SelectSingleNode("/Tarih_Date/@Tarih");
                XmlNodeList mylist = myxml.SelectNodes("/Tarih_Date/Currency");
                XmlNodeList adi = myxml.SelectNodes("/Tarih_Date/Currency/Isim");
                XmlNodeList kod = myxml.SelectNodes("/Tarih_Date/Currency/@Kod");
                XmlNodeList doviz_alis = myxml.SelectNodes("/Tarih_Date/Currency/ForexBuying");
                XmlNodeList doviz_satis = myxml.SelectNodes("/Tarih_Date/Currency/ForexSelling");
                XmlNodeList efektif_alis = myxml.SelectNodes("/Tarih_Date/Currency/BanknoteBuying");
                XmlNodeList efektif_satis = myxml.SelectNodes("/Tarih_Date/Currency/BanknoteSelling");
                Date = tarih.InnerText.ToString();
                // datagridimin captionu ayarlıyoruz.
                int x = 19;
                /*  Burada xmlde bahsettiğim - bence-  mantık hatasından dolayı x gibi bir değişken tanımladım.
                bu x =19  DataTable a sadece 19 satır eklenmesini sağlıyor. çünkü xml dökümanında 19. node dan sonra
                güncel kur bilgileri değil Euro dönüşüm kurları var ve bu node dan sonra yapı ilk 18 node ile tutmuyor
                Bence ayrı bir xml dökümanda tutulması gerekirdi. 
                */
                string title = "", code = "",  f_buying = "", f_selling = "", e_buying = "", e_selling = "";
                for (int i = 0; i < x; i++)
                {
                    title = kod.Item(i).InnerText.ToString();
                    code = kod.Item(i).InnerText.ToString();
                    // Kod satırları                    

                    f_buying = doviz_alis.Item(i).InnerText.ToString();
                    // Döviz Alış
                    f_selling = doviz_satis.Item(i).InnerText.ToString();
                    // Döviz  Satış
                    e_buying = efektif_alis.Item(i).InnerText.ToString();
                    // Efektif Alış
                    e_selling = efektif_satis.Item(i).InnerText.ToString();
                    // Efektif Satış.
                    CurrencyRate rate = new CurrencyRate();
                    rate.Title = title;
                    rate.Code = code;
                    rate.ForexBuying = f_buying.Parse<decimal>();
                    rate.ForexSelling = f_selling.Parse<decimal>();
                    rate.BanknoteBuying = e_buying.Parse<decimal>();
                    rate.BanknoteSelling = e_selling.Parse<decimal>();
                    Rates.Add(rate);

                }
            }
            catch (Exception ex)
            {

            }
            return Rates;
        }
        public static List<CurrencyRate> GetLatest()
        {
            List<CurrencyRate> CurList = new List<CurrencyRate>();
            try
            {
                using (WebClient client = new WebClient())
                {
                    var json = client.DownloadString("http://www.doviz.com/api/v1/currencies/all/latest");
                    List<Doviz> list = JsonConvert.DeserializeObject<List<Doviz>>(json);
                    foreach (Doviz doviz in list)
                    {
                        CurrencyRate rate = new CurrencyRate();
                        rate.Title = doviz.name.Replace("-"," ").ToUpper();
                        rate.Code = doviz.code;
                        rate.ForexBuying = rate.BanknoteBuying = doviz.buying.Parse<decimal>();
                        rate.ForexSelling = rate.BanknoteSelling = doviz.selling.Parse<decimal>();
                        CurList.Add(rate);
                    }
                }
            }
            catch (Exception ex)
            {
                //LogService.SaveLog(ex, "FOREX VALUES COULD NOT BE RETRIEVED.");
            }
            return CurList;
        }
    }
}
