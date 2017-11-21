using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Xml.Linq;
using System.Web;
using System.Linq;

using System.Xml;
using System.Net;
using MS.TF.Test.Web.AutomationFramework;
using System.IO;
using System.Data.SqlClient;


namespace Mtps.WebApplication.E2E.Test
{
    public static class Helper
    {
        private static Random Rdm = new Random();

        /// <summary>
        /// To read content xml file.
        /// </summary>
        /// <param name="xmlContentName"></param>
        /// <param name="fileName"></param>
        /// <param name="element"></param>
        /// <returns></returns>
        public static List<string> ReadContentXml(string xmlContentName, string fileName, string element)
        {
            if (!string.IsNullOrEmpty(fileName) && !string.IsNullOrEmpty(element))
            {
                IEnumerable<XElement> contentXml = XElement.Load(fileName).Element(element).Elements();
                List<string> expectedValues = new List<string>();
                IEnumerable<XElement> expectedContent = contentXml.Elements(xmlContentName);
                foreach (XElement content in expectedContent)
                {
                    expectedValues.Add(content.Value);
                }
                return expectedValues;
            }
            return null;
        }

        /// <summary>
        /// To read content xml file.
        /// </summary>
        /// <param name="xmlContentName"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static List<string> ReadContentXml(string xmlContentName, string fileName)
        {
            if (!string.IsNullOrEmpty(fileName))
            {
                IEnumerable<XElement> contentXml = XElement.Load(fileName).Elements();
                List<string> expectedValues = new List<string>();
                IEnumerable<XElement> expectedContent = contentXml.Elements(xmlContentName);
                foreach (XElement content in expectedContent)
                {
                    expectedValues.Add(content.Value);
                }
                return expectedValues;
            }
            return null;
        }

        //public static Dictionary<string, string> GetPageContent(string page)
        //{
        //    BaseURLBuilder urlBuilder = new BaseURLBuilder(); ;
        //    Dictionary<string, string> resourceLinksList = new Dictionary<string, string>();
        //    ResourceManager resMgr = new ResourceManager("Mtps.WebApplication.E2E.Test.Resources." + urlBuilder.GetLocale(), Assembly.GetExecutingAssembly());
        //    CultureInfo cultureInfo = new CultureInfo(urlBuilder.GetLocale());
        //    ResourceSet resources = resMgr.GetResourceSet(cultureInfo, true, true);
        //    IDictionaryEnumerator enumerator = resources.GetEnumerator();
        //    while (enumerator.MoveNext())
        //    {
        //        if (enumerator.Key.ToString().Contains(page))
        //        {
        //            resourceLinksList.Add((string)enumerator.Key, (string)enumerator.Value);
        //        }
        //    }

        //    return resourceLinksList;
        //}

        /// <summary>
        /// To read from resource file.
        /// </summary>
        /// <param name="endsWith"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public static List<string> ReadContentResource(string endsWith, Dictionary<string, string> result)
        {
            List<string> expectedUrls = new List<string>();

            if (!string.IsNullOrEmpty(endsWith))
            {
                foreach (KeyValuePair<string, string> item in result)
                {
                    if (item.Key.ToString().EndsWith(endsWith))
                    {
                        expectedUrls.Add(System.Net.WebUtility.HtmlDecode(item.Value));
                    }
                }
                return expectedUrls;
            }
            return null;
        }

        /// <summary>
        /// function to generate a random string based off the length specificed. 
        /// </summary>
        /// <param name="length">length of the string</param>
        /// <returns>the string</returns>
        public static string RandomString(int length)
        {
            var stringBuilder = new StringBuilder();
            while (length-- > 0)
            {
                var addingChar = RandomNumber(0, 2) == 1
                                     ? (char)((int)'a' + RandomNumber(0, 25))
                                     : (char)((int)'A' + RandomNumber(0, 25));
                stringBuilder.Append(addingChar);
            }
            return stringBuilder.ToString();
        }

        /// <summary>
        /// Looks up the build number for the enviroment
        /// </summary>
        /// <param name="siteurl">url for the site</param>
        /// <returns></returns>
        public static string GetBuildNumber(string siteUrl)
        {
            XmlDocument BuildInfo = LookUpBuildInfo(siteUrl);
            return BuildInfo.FirstChild.Attributes["number"].Value;

        }

        /// <summary>
        /// Looks up the build release string (e.g. S63, main, etc).
        /// </summary>
        /// <param name="siteUrl"></param>
        /// <returns></returns>
        public static string GetBuildRelease(string siteUrl)
        {
            XmlDocument BuildInfo = LookUpBuildInfo(siteUrl);
            return BuildInfo.FirstChild.Attributes["release"].Value;
        }

        /// <summary>
        /// List of the Iroots that we consider Pri 1 for verification.  This list is used
        /// primarally if we have a common test we want to make sure workes across the important
        /// sites (for instance visual verification and/or site level verification).
        /// </summary>
        public static string[] Pri1IRoots = {  "msdn|", 
                                               "msdn|/Office",
                                               "msdn|/windows/apps",
                                               "msdn|/windows/desktop",
                                               "msdn|/windows/hardware",
                                               "msdn|/ie",
                                               "msdn|/onedrive",
                                               "msdn|/vstudio",
                                               "technet|",
                                               "technet|/windows", 
                                               "technet|/windowsserver", 
                                               "technet|/ie", 
                                               "technet|/office", 
                                               "technet|/exchange", 
                                               "technet|/lync", 
                                               "technet|/systemcenter", 
                                               "technet|/sqlserver",
                                               "vscom|",
                                               "vscom|/integrate"
                                            };

        /// <summary>
        /// List of locales we use to tests with, used by GetRandomeLocales. helpful if the test needs a random locale or set of locales.  Note that 
        /// the first locale should always be en-us so test that want to use en-us can assume it in the order.
        /// 
        /// Note, removing en-ph, es-xl and hu-hu from the list due to odd behaviors with these locales.  If a test needs to verify these directly they
        /// will need to call them instead of hoping to get them from a random draw.
        /// </summary>
        private static string[] locales = { "en-us", "ar-sa", "cs-cz", "da-dk", "de-at", "de-ch", "de-de", "el-gr",
                                            "en-au", "en-ca", "en-gb", "en-ie", "en-in", "en-jm", "en-nz", "en-sg",
                                            "en-tt", "en-za", "es-ar", "es-bo", "es-cl", "es-co", "es-cr", "es-do",
                                            "es-ec", "es-es", "es-gt", "es-hn", "es-mx", "es-ni", "es-pa", "es-pe",
                                            "es-pr", "es-py", "es-sv", "es-uy", "es-ve", "fi-fi", "fr-be", "fr-ca",
                                            "fr-ch", "fr-fr", "he-il", "id-id", "it-it", "ja-jp", "ko-kr", "nb-no",
                                            "nl-be", "nl-nl", "pl-pl", "pt-br", "pt-pt", "ru-ru", "sk-sk", "sv-se",
                                            "tr-tr", "zh-cn", "zh-tw"
                                         };

        /// <summary>
        /// List of locales of comment set of vscom and windows iroots
        /// </summary>
        private static string[] subsetLocales = { "en-us", "de-de", "es-es", "fr-fr", "it-it", "ja-jp", "ko-kr", 
                                                    "pt-br", "ru-ru", "zh-cn", "zh-tw"
                                         };

        /// <summary>
        /// <para>Get the ramdomlize list of locales.</para>
        /// <para>Note:</para>
        /// <para>  If mandatoryLocales is null then the test will treat the configured locale as manditory.</para>
        /// </summary>
        /// <param name="numLocales">Number of locales to return</param>
        /// <param name="mandatoryLocales">List of mandatory locales to include in the return (see note above for special behavior)</param>
        /// <param name="excludedLocales">List of locales to exlcude from the return</param>
        /// <param name="subSet">Subset of locales</param>
        /// <returns>list of locales that can be used for testing</returns>

        public static List<string> GetRandomLocales(int numLocales, List<string> mandatoryLocales = null, List<string> excludedLocales = null, bool subSet = false)
        {
            List<string> localesList = null;
            if (subSet)
            {
                localesList = subsetLocales.ToList();
            }
            else
            {
                localesList = locales.ToList();
            }

            //if (mandatoryLocales == null)
            //{
            //    BaseURLBuilder builder = new BaseURLBuilder();

            //    mandatoryLocales = new List<string>();
            //    mandatoryLocales.Add(builder.GetLocale());
            //}

            if (excludedLocales != null)
            {
                foreach (string excluded in excludedLocales)
                {
                    localesList.Remove(excluded);
                }
            }

            if (mandatoryLocales != null)
            {
                foreach (string mandatory in mandatoryLocales)
                {
                    localesList.Remove(mandatory);
                }
            }

            List<string> randomLocales = mandatoryLocales ?? new List<string>();

            while (randomLocales.Count < numLocales)
            {
                int index = Helper.RandomNumber(0, localesList.Count);
                randomLocales.Add(localesList[index]);
                localesList.RemoveAt(index);
            }

            return randomLocales;
        }

        private static XmlDocument LookUpBuildInfo(string siteUrl)
        {
            XmlDocument ret = null;

            string versionUrl = siteUrl + "?view=build";

            HttpWebRequest hwr = (HttpWebRequest)WebRequest.Create(versionUrl);

            using (HttpWebResponse hwresp = (HttpWebResponse)hwr.GetResponse())
            {
                ret = new XmlDocument();
                ret.Load(hwresp.GetResponseStream());
            }

            return ret;

        }

        /// <summary>
        /// Function to generate a random number
        /// </summary>
        /// <param name="min">Lower bound of the random number - inclusive</param>
        /// <param name="max">Upper bound of the random number - exclusive</param>
        public static int RandomNumber(int min = 0, int max = 100)
        {
            return Rdm.Next(min, max);
        }

        public static string HtmlEncode(string contentToEncode)
        {
            return HttpUtility.HtmlEncode(contentToEncode);
        }

        /// <summary>
        /// To set the browser type. e.g(IE,FireFox,Chrome,etc)
        /// </summary>
        /// <param name="browser"></param>
        //public static void SetTargetBrowser(WebBrowserType browserType)
        //{
        //    if (!string.IsNullOrWhiteSpace(browserType.ToString()))
        //    {
        //        TestConstants.browserType = browserType.ToString();
        //    }
        //}
        /// <summary>
        /// Transform code for special character in html
        /// e.g &nbsp
        /// it is not equal to space character, and it cannot be coverted using decode/encode
        /// </summary>
        /// <param name="html">input text</param>
        /// <param name="htmlCode">original html code</param>
        /// <param name="convertCode">code to convert</param>
        /// <returns>converted string</returns>
        public static string ConvertHtmlCode(string html, int htmlCode, int convertCode)
        {
            if (!string.IsNullOrWhiteSpace(html))
            {
                return html.Replace(Convert.ToChar(htmlCode), Convert.ToChar(convertCode));
            }
            return null;
        }

        /// <summary>
        /// Get the value from debug mode
        /// </summary>
        /// <param name="url">input browser url</param>
        /// <param name="value">the value from UrlPart</param>
        /// <returns>Locale,PageID,FamilyVersion,HostName</returns>
        public static string UrlParts(string url, UrlPart value)
        {
            XmlDocument xmlDoc = null;
            string versionUrl = url.ToLower() + "?view=debug";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(versionUrl);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            xmlDoc = new XmlDocument();
            xmlDoc.Load(response.GetResponseStream());
            XmlNodeList nodeList = xmlDoc.DocumentElement.ChildNodes;

            if (value.ToString().Equals(Enum.GetName(typeof(UrlPart), 1)))
            {
                return nodeList[0].ChildNodes[4].InnerText;     //return Locale
            }
            else if (value.ToString().Equals(Enum.GetName(typeof(UrlPart), 2)))
            {
                return nodeList[0].ChildNodes[3].InnerText;      //return PageID
            }
            else if (value.ToString().Equals(Enum.GetName(typeof(UrlPart), 3)))
            {
                return nodeList[0].ChildNodes[8].InnerText;     //return FamilyVersion
            }
            else if (value.ToString().Equals(Enum.GetName(typeof(UrlPart), 4)))
            {
                return nodeList[0].ChildNodes[2].InnerText;     //Retrun HostName
            }
            else if (value.ToString().Equals(Enum.GetName(typeof(UrlPart), 5)))
            {
                return nodeList[0].ChildNodes[5].InnerText;     //Return logicalPath
            }
            else if (value.ToString().Equals(Enum.GetName(typeof(UrlPart), 6)))
            {
                return nodeList[0].ChildNodes[6].InnerText;     //Return logicalSiteName
            }
            else
            {
                return null;
            }
        }

        // Returns submit feedback value , insert UTC date, rate value
        public static Tuple<string, string, int> GetLatestValueFromRatingDB(string connectionString, string feedbackValue)
        {
            Tuple<string, string, int> dbValue = null;
            using (SqlConnection sqlConnect = new SqlConnection(connectionString))
            {
                sqlConnect.Open();
                SqlCommand command = sqlConnect.CreateCommand();
                command.CommandText = string.Format(@"SELECT  [insertUTCDate],[feedback],[rateValue]                                                    
                                                    FROM [Ratings].[dbo].[rating]
                                                    Where [feedback] like '%{0}%'", feedbackValue);
                SqlDataReader reader = command.ExecuteReader();
                reader.Read();
                DateTime insertUTCDate = (DateTime)reader["insertUTCDate"];
                string feedback = reader["feedback"].ToString();
                int rateValue = Convert.ToInt32(Convert.ToDouble(reader["rateValue"].ToString()));
                dbValue = new Tuple<string, string, int>(feedback.ToString(), insertUTCDate.ToString("yyyy/MM/dd hh:mm"), rateValue);
                reader.Close();
                sqlConnect.Close();
            }
            return dbValue;
        }

        /// <summary>
        /// The value from debug mode
        /// </summary>
        public enum UrlPart
        {
            Locale = 1,
            PageID = 2,
            FamilyVersion = 3,
            HostName = 4,
            LogicalPath = 5,
            logicalSiteName = 6
        }
    }
}
