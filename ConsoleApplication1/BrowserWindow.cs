using MS.TF.Test.Web.AutomationFramework;
using MS.TF.Test.Web.AutomationFramework.NativeHelpers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;

namespace Mtps.WebApplication.Test.Framework
{
    /// <summary>
    /// Represents a web browser window and supports functions related to that such as opening
    /// windows of a particular browser type, page navigation, and clearing cache
    /// </summary>
    public class BrowserWindow
    {
        private const int TabSleep = 1000;
        private static IWebFrameworkAgent Agent;
        internal IWebBrowserWindow TfsBrowserWindow;
        public enum SourceTypes
        {
            debugSource = 1,
            pageSource = 2
        }

        #region Properties
        /// <summary>
        /// Returns the number of open tabs in the current window
        /// <para>Note: This does not work in Chrome</para>
        /// </summary>
        private int TabCount
        {
            get
            {
                return BrowserTypeHelper.GetTabbedDocumentCount(Agent.BrowserType, TfsBrowserWindow.WindowHandle);
            }
        }

        /// <summary>
        /// The browser instances for the current browser type
        /// <para>Returns an empty list if there are no open browsers</para>
        /// </summary>
        public static List<BrowserWindow> Instances
        {
            get
            {
                List<BrowserWindow> browserInstances = new List<BrowserWindow>();
                if (Agent != null)
                {
                    IWebBrowserWindow[] attachedBrowserWindows = Agent.GetAttachedBrowserWindows();
                    foreach (IWebBrowserWindow attachedBrowserWindow in attachedBrowserWindows)
                        browserInstances.Add(new BrowserWindow(attachedBrowserWindow));
                }
                return browserInstances;
            }
        }

        /// <summary>
        /// The current URL of the browser window
        /// </summary>
        public string Url
        {
            get
            {
                return TfsBrowserWindow.CurrentUrl;
            }
        }

        /// <summary>
        /// Gets or set the vertical web page offset
        /// </summary>
        public int YScrollPosition
        {
            get
            {
                return TfsBrowserWindow.ExecuteJScript<int>("return window.pageYOffset;");
            }
            set
            {
                int xPos = TfsBrowserWindow.ExecuteJScript<int>("return window.pageXOffset;");
                TfsBrowserWindow.ExecuteJScript(string.Format("window.scrollTo({0}, {1});", xPos, value));
            }
        }

        /// <summary>
        /// The position and size of the window viewport
        /// <para>See ResizeBrowserWindow to alter the viewport size</para>
        /// </summary>
        //public Rectangle Viewport
        //{
        //    get
        //    {
        //        int XOffset = TfsBrowserWindow.ExecuteJScript<int>("return window.pageXOffset;");
        //        int YOffset = TfsBrowserWindow.ExecuteJScript<int>("return window.pageYOffset;");

        //        int Height = TfsBrowserWindow.ExecuteJScript<int>("return window.innerHeight;");
        //        //Using document.width so we can get the width excluding the scroll bar.
        //        int Width = TfsBrowserWindow.ExecuteJScript<int>("return document.body.clientWidth;");

        //        ElementDefinition eleDef = ElementDefinition.JQuery("body");
        //        IWebElement ele = TfsBrowserWindow.GetWebElement<IWebElement>(eleDef);

        //        int top = ele.Coordinates.Top + YOffset;
        //        int left = ele.Coordinates.Left + XOffset;


        //        return new Rectangle(left, top, Width, Height);
        //    }
        //}

       

        /// <summary>
        /// The web page title as defined in the <title> tag of <head>
        /// <para>Returns the empty string if no title is found</para>
        /// </summary>
        public string PageTitle
        {
            get
            {
                ElementDefinition defSearchButton = ElementDefinition.JQuery("head").Children("title");
                IWebElement element = TfsBrowserWindow.GetWebElement(defSearchButton);
                return element.CurrentlyExists ? element.InnerText : string.Empty;
            }
        }

        /// <summary>
        /// The source document when ?view=debug is appended to the current URL
        /// </summary>
        public string DebugPageSource
        {
            get
            {
                string webPageContent = string.Empty;
                string debugModeUrl = Url;
                try
                {
                    if (!debugModeUrl.EndsWith("?view=debug"))
                    {
                        debugModeUrl = string.Format("{0}?view=debug", debugModeUrl);
                    }

                    HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(debugModeUrl);
                    httpWebRequest.Credentials = CredentialCache.DefaultCredentials;
                    HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                    webPageContent = new StreamReader(httpWebResponse.GetResponseStream()).ReadToEnd();
                }
                catch { }
                return webPageContent.Replace(System.Environment.NewLine, "");
            }
        }

        /// <summary>
        /// The source document
        /// </summary>
        public string PageSource
        {
            get
            {
                WebRequest wReq = WebRequest.Create(Url);
                WebResponse wResp = wReq.GetResponse();
                Stream respStream = wResp.GetResponseStream();
                StreamReader reader = new StreamReader(respStream, Encoding.ASCII);
                return reader.ReadToEnd();
            }
        }
        #endregion

        #region Controls
       

        

      
        #endregion

        /// <summary>
        /// Create a BrowserWindow by using the static method Open
        /// </summary>
        private BrowserWindow(IWebBrowserWindow browser)
        {
            TfsBrowserWindow = browser;
        }

        #region Public Methods
        /// <summary>
        /// Opens a new browser window
        /// <para>Note: If changing browser types, all windows opened by the previous type must be closed first</para>
        /// </summary>
        /// <param name="url">The URL for the window to navigate to</param>
        /// <param name="webBrowserType">The type of browser to open (e.g. "IE", "Chrome", "Firefox", "Safari")</param>
        /// <param name="closeAllOtherBrowserWindows">If true, all browsers of type webBrowserType will be closed before opening the new window</param>
        /// <param name="maximizeWindow">If true, the new window will be maximized if not already</param>
        public static BrowserWindow Open(string url, string webBrowserType, bool closeAllOtherBrowserWindows = true, bool maximizeWindow = true)
        {
            WebBrowserType browserType = ParseBrowserType(webBrowserType);

            InitializeAgent(browserType);

            if (closeAllOtherBrowserWindows)
            {
                Agent.CloseAllBrowserWindows();
            }

            IWebBrowserWindow _browser = Agent.OpenNewBrowser(url);
            if (maximizeWindow)
            {
                _browser.MaximizeBrowserWindow();
            }

            //hackhack: below waitForDocumentReadyState occasionally raise exception, so catch it then retry to open the browser
            try
            {
                _browser.WaitForDocumentReadyState();
            }
            catch (WebOperationFailedException)
            {
                Agent.CloseAllBrowserWindows();
                _browser = Agent.OpenNewBrowser(url);
                _browser.WaitForDocumentReadyState();
            }

            //Logger.Instance.LogInfo("Opening a browser window, url: " + url + ", browser type: " + webBrowserType);
            //Historian.AddVisistedUrl(url);

            return new BrowserWindow(_browser);
        }

        /// <summary>
        /// Clears the browser cache
        /// <para>No windows should be attached to the framework agent</para>
        /// <para>Any browsers not attached to the framework agent of the same type as browserType will be closed</para>
        /// </summary>
        /// <param name="browserType">The browser type that will have its cache cleared</param>
        /// <param name="options">The cached items to be cleared</param>
        public static void ClearCache(string browserType, ClearCacheOptions options = ClearCacheOptions.CachedFiles | ClearCacheOptions.Cookies)
        {
            if (Agent == null)
            {
                InitializeAgent(ParseBrowserType(browserType));
            }
            else if (Agent.GetAttachedBrowserWindows().Length > 0)
            {
                throw new Exception(string.Format("Current framework agent has {0} attached windows."
                    + " All browser windows of type {1} must be closed before cache can be cleared.",
                        Agent.GetAttachedBrowserWindows().Length, Agent.BrowserType));
            }

            // Close all browser windows that may not be attached to this agent before flushing the cache
            // The file system gets a permission error if any browser windows are still open when trying to delete the cache
            Agent.CloseAllBrowserWindows();
            Agent.ClearBrowserCache(options);

           // Logger.Instance.LogInfo("Clearing browser cache in browser: " + browserType);
        }

        /// <summary>
        /// Clears the browser local storage and refreshes the current page
        /// </summary>
        /// <param name="serverReload">If true, force an unconditional GET from the server. If false, use client cache if available.</param>
        public void ClearLocalStorage(bool serverReload = false)
        {
            TfsBrowserWindow.ExecuteJScript("window.localStorage.clear();");
            Refresh(serverReload);
           // Logger.Instance.LogInfo("Cleared browser local storage");
        }

        /// <summary>
        /// Refresh the page
        /// <para>Note: The TFS automation framework's Reload() method calls WaitForPageReload(), so a wait after calling Refresh should be unnecessary</para>
        /// </summary>
        /// <param name="serverReload">If true, force an unconditional GET from the server. If false, use client cache if available.</param>
        public void Refresh(bool serverReload = false)
        {
            TfsBrowserWindow.Reload(serverReload);
           // Logger.Instance.LogInfo("Refreshed browser");

            if (Agent.BrowserType == WebBrowserType.Chrome || Agent.BrowserType == WebBrowserType.Firefox)
            {
                // Chrome and Firefox seem to exit Reload prematurely occasionally (ノಠ益ಠ)ノ
                // But an extra call to WaitForPageReload seems to fix it
                // However, it also makes this method several seconds slower
                TfsBrowserWindow.WaitForPageReload();
              //  Logger.Instance.LogInfo("Waited for page reload (extra Chrome call)");
            }
        }

        /// <summary>
        /// Find a browser window by title of the same browser type as this object
        /// </summary>
        /// <param name="windowTitle">The window title to look for</param>
        /// <param name="ignorePreviouslyAttachedWindows">If true, ignore any windows that have previously been attached to by the framework</param>
        /// <param name="isModal">
        /// Whether or not the window to attach to is modal, meaning a child window that requires user interaction 
        /// before being able to return to operating the parent window
        /// </param>
        /// <param name="timeoutMs">The duration in milliseconds to try to find the popup window</param>
        /// <returns>The reference to the browser window</returns>
        public BrowserWindow FindPopupBrowserWindow(string windowTitle, bool ignorePreviouslyAttachedWindows = true, bool isModal = false, int timeoutMs = 5000)
        {
            return FindPopupBrowserWindow(new string[] { windowTitle }, ignorePreviouslyAttachedWindows, isModal, timeoutMs);
        }

        /// <summary>
        /// Find a browser window by title of the same browser type as this object
        /// </summary>
        /// <param name="possibleWindowTitles">An array of the possible browser window titles to look for</param>
        /// <param name="ignorePreviouslyAttachedWindows">If true, ignore any windows that have previously been attached to by the framework</param>
        /// <param name="isModal">
        /// Whether or not the window to attach to is modal, meaning a child window that requires user interaction 
        /// before being able to return to operating the parent window
        /// </param>
        /// <param name="timeoutMs">The duration in milliseconds to try to find the popup window</param>
        /// <returns>The reference to the browser window</returns>
        public BrowserWindow FindPopupBrowserWindow(string[] possibleWindowTitles, bool ignorePreviouslyAttachedWindows = true, bool isModal = false, int timeoutMs = 5000)
        {
            int originalTimeout = TfsBrowserWindow.FrameworkAgent.FindBrowserWindowTimeout;
            TfsBrowserWindow.FrameworkAgent.FindBrowserWindowTimeout = timeoutMs;

            try
            {
                IWebBrowserWindow popup = (possibleWindowTitles.Length == 1) ?
                    TfsBrowserWindow.GetPopupWindow(possibleWindowTitles[0], isModal, ignorePreviouslyAttachedWindows) :
                    TfsBrowserWindow.GetPopupWindow(possibleWindowTitles, isModal, ignorePreviouslyAttachedWindows);

                return new BrowserWindow(popup);
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                TfsBrowserWindow.FrameworkAgent.FindBrowserWindowTimeout = originalTimeout;
            }
        }

        /// <summary>
        /// Close the browser window
        /// </summary>
        public void Close()
        {
            try
            {
                if (TfsBrowserWindow != null)
                {
                    TfsBrowserWindow.CloseBrowserWindow();
                }
            }
            catch
            {
                WindowHelper.CloseWindow(TfsBrowserWindow.WindowHandle);
            }
            finally
            {
                TfsBrowserWindow = null;
            }
        }

     
        /// <summary>
        /// Maximize the browser window's viewport
        /// </summary>
        public void Maximize()
        {
            BrowserNativeMethods.ShowWindow(TfsBrowserWindow.WindowHandle, BrowserNativeMethods.ShowWindowCommands.SW_MAXIMIZE);
        }

        /// <summary>
        /// Set the browser's viewport size
        /// </summary>
        public void ResizeBrowserWindow(int width, int height)
        {
            var originalWindowDimensions = TfsBrowserWindow.GetDimensions();

            BrowserNativeMethods.ShowWindow(TfsBrowserWindow.WindowHandle, BrowserNativeMethods.ShowWindowCommands.SW_SHOWNORMAL);
            BrowserNativeMethods.SetWindowPos(TfsBrowserWindow.WindowHandle, IntPtr.Zero, 0, 0, width, height, BrowserNativeMethods.SetWindowPosFlags.ShowWindow);

            TfsBrowserWindow.WaitOnJScriptExpressionTrue(String.Format("(window.outerWidth !== {0}) && (window.outerHeight !== {1})",
                originalWindowDimensions.OuterWidth, originalWindowDimensions.OuterHeight));
           // Logger.Instance.LogInfo("Resizing the browser window to " + width + " x " + height);
        }

        /// <summary>
        /// Navigate the browser to the provided url
        /// <para>If the argument does not include the URL scheme (such as // or http://) then it will be treated as a URL path</para>
        /// <para>Note: The browser may or may not not record the navigation in the browser history for back/forward navigation</para>
        /// </summary>
        /// <param name="url">The destination address</param>
        public void Navigate(string url)
        {
            // In Firefox, using window.location = <url>; (which is what the TFS Framework's NavigateTo() method does)
            // replaces the current URL in the browser history, so you are unable to navigate back.
            // A hack that seems to fix this is to wrap window.location in a setTimeout.
            ExecuteJScript(string.Format("setTimeout(function() {{ window.location.href='{0}'; }}, 0)", url), true);

            WaitForNavigate();
            //Logger.Instance.LogInfo("Navigate to " + url);
            //Historian.AddVisistedUrl(url);
        }

        /// <summary>
        /// Wait for the page to finish navigation
        /// <para>This method should be called after page navigations that are not initiated by calls to Navigate</para>
        /// <para>An example of this would be clicking a link that takes you to a new page</para>
        /// <para>Known Quirks:</para>
        /// <para>- Chrome doesn't always obey the wait, so in Chrome it actually calls the wait twice</para>
        /// <para>- Even if the navigation appears complete, sometimes it takes another second or two for the method to return</para>
        /// </summary>
        public void WaitForNavigate()
        {
            if (TfsBrowserWindow.FrameworkAgent.BrowserType == WebBrowserType.Chrome)
            {
                // Chrome seems to exit WaitForNavigate prematurely occasionally (ノಠ益ಠ)ノ
                // But an extra call to WaitForPageReload seems to fix it
                // However, it also makes this method several seconds slower
                TfsBrowserWindow.WaitForPageReload();
            }

            TfsBrowserWindow.WaitForPageReload();
            //Logger.Instance.LogInfo("Wait for the navigation to another page.");
            //Historian.AddVisistedUrl(Url);
        }

       

       
        /// <summary>
        /// Executes JavaScript in the browser
        /// </summary>
        /// <param name="jScript">The script to run</param>
        /// <param name="throwOnScriptException">If set to false, the method fails silently should the javascript fail.
        /// The return value in this case is unknown</param>
        public string ExecuteJScript(string jScript, bool throwOnScriptException)
        {
            return TfsBrowserWindow.ExecuteJScript(jScript, throwOnScriptException);
        }

        public string GetCookie(string cookieKey)
        {
            var cookieVal = TfsBrowserWindow.ExecuteJScript("return document.cookie");
            var expectedCookieKvp = cookieVal.Split(';').ToList().FirstOrDefault(cookieKvp => cookieKvp.Trim().StartsWith(cookieKey));
            return string.IsNullOrWhiteSpace(expectedCookieKvp) ? "" : expectedCookieKvp.Trim().Substring(cookieKey.Length);
        }

        public bool CookieExists(string cookieKey)
        {
            var cookieVal = TfsBrowserWindow.ExecuteJScript("return document.cookie");
            var expectedCookieKvp = cookieVal.Split(';').ToList().FirstOrDefault(cookieKvp => cookieKvp.Trim().StartsWith(cookieKey));
            return !string.IsNullOrEmpty(expectedCookieKvp);
        }


        //public bool IsLinkBroken(ILink link, out string errorMsg)
        //{
        //    string error;
        //    errorMsg = "";

        //    if (!link.CurrentlyExists || link.LinkTarget.Contains("javascript:void(0)"))
        //    {
        //        //If the link doesn't exist we can't test it...
        //        return false;
        //    }

        //    string text = link.Text;
        //    string target = link.LinkTarget;
        //    if (string.IsNullOrWhiteSpace(target))
        //    {
        //        //We have links on the page that do not target anything (for instance the search flyout link).
        //        //Don't check them since they won't really do anything.
        //        return false;
        //    }
        //    System.Net.HttpStatusCode statusCode = DirectHttp.GetPageLoadResponseCode(target, out error);

        //    //100 range is for information, 200 range is for success,  300 is for redirect. all of these are 
        //    //in some manor success.
        //    //400 range and beyond are errors.
        //    if ((int)statusCode >= 400)
        //    {
        //        errorMsg = "LinkText: " + text + ", Link Target: " + target + ", Status Code: " + ((int)statusCode).ToString() + ", Message: " + error;
        //        //Logger.Instance.LogError(errorMsg);
        //        return true;
        //    }
        //    else
        //        return false;
        //}

        /// <summary>
        /// Get the string of injected scripts  
        /// </summary>
        /// <returns></returns>
        public string GetMtpsInjectedScript()
        {
            ElementDefinition ele = ElementDefinition.JQuery("body").Find("script.mtps-injected");

            List<IWebElement> scriptTags = TfsBrowserWindow.GetWebElementList(ele);
            string scriptTagInnerHtml = scriptTags[0].InnerHtml;

            //script loader change in msdn, needs to add null check
            if (!string.IsNullOrWhiteSpace(scriptTagInnerHtml))
            {
                return scriptTagInnerHtml.Substring(scriptTagInnerHtml.IndexOf("MTPS.injectScripts"));
            }

            return null;
        }

        public bool IsMetaExists(string strMetaName, SourceTypes sourceTypes, string strMetaContent = "")
        {
            return !string.IsNullOrEmpty(GetMetaValue(strMetaName, sourceTypes, strMetaContent));
        }

        public bool IsLinkExistFromPageSource(string strLinkRel, string strLinkHref)
        {
            strLinkHref = strLinkHref.Replace("(", "\\u0028").Replace(")", "\\u0029");
            string pattern = string.Format(@"<link(.*)rel=""{0}(.*)href=""(.*){1}(.*)/>", strLinkRel, strLinkHref);
            bool propertyFound = false;
            try
            {
                Match match = Regex.Match(PageSource, pattern, RegexOptions.IgnoreCase);
                if (match.Success)
                {
                    propertyFound = true;
                }
            }
            catch { }
            return propertyFound;
        }

        /// <summary>
        /// Get the attribute name value from debug page
        /// </summary>
        /// <param name="labelName"></param>
        /// <param name="attributeName"></param>
        /// <param name="iRoot"></param>
        /// <returns></returns>
        public string GetDebugAttributeNameValue(string labelName, string attributeName, string iRoot)
        {
            string attributeValue = string.Empty;
            string pattern = string.Format(@"<{0}(.*)</{0}>", labelName);
            try
            {
                Match match = Regex.Match(DebugPageSource, pattern, RegexOptions.IgnoreCase);
                if (match.Success)
                {
                    if (match.Groups[0].ToString().Contains(attributeName))
                    {
                        string content = match.Groups[0].ToString();
                        XmlDocument xmlDoc = new XmlDocument();
                        xmlDoc.LoadXml(content);
                        attributeValue = xmlDoc.SelectSingleNode(iRoot).Attributes[attributeName].Value;
                    }
                }
            }
            catch { }
            return attributeValue;
        }

        /// <summary>
        /// Get the meta value from debug page or page source
        /// provide a 'constentStartWith' string to filter the selection if there are more than one same meta tag.
        /// </summary>
        /// <param name="metaName">The metadata name to get the value</param>
        /// <param name="sourceType">choose the source from pageSource or debugSource</param>
        /// <param name="contentStartWith">the string to filter the meta tag/param>
        /// <returns></returns>
        public string GetMetaValue(string metaName, SourceTypes sourceTypes = SourceTypes.debugSource, string contentStartWith = "")
        {
            string sourceType;
            if (sourceTypes.ToString().Equals(Enum.GetName(typeof(SourceTypes), 1)))
            {
                sourceType = DebugPageSource;
            }
            else
            {
                sourceType = PageSource;
            }
            string attributeValue = string.Empty;
            string pattern = pattern = string.Format(@"<meta name=""{0}""\scontent=""{1}(.*?)""", metaName, contentStartWith);

            try
            {
                Match match = Regex.Match(sourceType, pattern, RegexOptions.IgnoreCase);
                if (match.Success)
                {
                    attributeValue = match.Groups[1].Value;
                }
            }
            catch { }
            return attributeValue;
        }

        /// <summary>
        /// Switch interaction to the next open tab
        /// </summary>
        public void SwitchTab()
        {
            System.Windows.Forms.SendKeys.SendWait("randomstringforfocus ^{TAB}");
            System.Threading.Thread.Sleep(TabSleep); // Random wait. Without it this method didn't seem to work cross-browser.
            RefreshAttachmentToAgent();
        }

        /// <summary>
        /// Open a new blank tab and switch interaction to it
        /// </summary>
        public void OpenNewTab()
        {
            System.Windows.Forms.SendKeys.SendWait("^t");
            System.Threading.Thread.Sleep(TabSleep); // Random wait. Without it this method didn't seem to work cross-browser.
            if (Agent.BrowserType == WebBrowserType.Firefox)
            {
                System.Windows.Forms.SendKeys.SendWait("^l about:home {ENTER}");
            }

            RefreshAttachmentToAgent();
        }

        /// <summary>
        /// Closes the current tab or the entire window if it is the last tab
        /// <para>Note: TabCount doesn't work in Chrome, so this method assumes the test writer knows there is more than 1 tab for Chrome</para>
        /// </summary>
        public void CloseCurrentTab()
        {
            if (Agent.BrowserType == WebBrowserType.IE)
            {
                throw new NotImplementedException("Haven't gotten this to work in IE yet");
            }

            if (TabCount > 1 || Agent.BrowserType == WebBrowserType.Chrome)
            {
                System.Windows.Forms.SendKeys.SendWait("^w");
                System.Threading.Thread.Sleep(TabSleep); // Random wait. Without it this method didn't seem to work cross-browser.
                System.Windows.Forms.SendKeys.SendWait("^l");
                RefreshAttachmentToAgent();
            }
            else
            {
                Close();
            }
        }
        #endregion

        #region Helper Methods
        /// <summary>
        /// Parses the string into a WebBrowserType or returns the OS default if the string is empty
        /// </summary>
        /// <param name="browserType">The string to be parsed</param>
        private static WebBrowserType ParseBrowserType(string browserType)
        {
            if (!string.IsNullOrWhiteSpace(browserType))
            {
                return (WebBrowserType)Enum.Parse(typeof(WebBrowserType), browserType);
            }
            else
            {
                return BrowserTypeHelper.GetOSDefaultBrowserType();
            }
        }

        /// <summary>
        /// Sets and initializes the web framework agent if necessary
        /// </summary>
        /// <exception cref="Exception">Thrown if the browser type is different than the agent's current
        /// browser type and the agent still has attached/open browser windows</exception>
        /// <param name="browserType">The browser to use (ex. Firefox, IE, Chrome, Safari...)</param>
        private static void InitializeAgent(WebBrowserType browserType)
        {
            // Action based upon condition:
            // Agent is null                                          -> Set the agent and initialize it
            // Agent is set
            //     Requested browser type is different than the agent
            //         Agent has attached browser windows             -> Throw exception
            //         Agent has no attached browser windows          -> Get a new agent of the requested browser type and initialize it
            //     Requested browser type is the same as the agent    -> Do nothing
            if (Agent == null || (Agent.BrowserType != browserType && Agent.GetAttachedBrowserWindows().Length == 0))
            {
                Agent = WebAgentFactory.GetAgent(browserType);
                Agent.Initialize();
                //Agent.Logger = Logger.Instance;
            }
            else if (Agent.BrowserType != browserType)
            {
                throw new Exception(string.Format("Current framework agent still has attached windows."
                    + " All windows attached to the {0} agent must be closed before you can initialize a {1} agent.",
                        Agent.BrowserType, browserType));
            }
        }

        /// <summary>
        /// Unattaches the window from the framework agent and subsequently reattaches it
        /// <para>Users can input the amount of waiting time they want (unit: ms)</para>
        /// </summary>
        public void RefreshAttachmentToAgent(int waitTime = TabSleep)
        {
            IntPtr handle = TfsBrowserWindow.WindowHandle;
            TfsBrowserWindow.Unattach();
            System.Threading.Thread.Sleep(waitTime); // Random wait. Without it this method didn't seem to work cross-browser.
            TfsBrowserWindow = Agent.AttachToExistingBrowserWindow(handle);
        }
        #endregion
    }

    #region Browser Helper Classes
    public static class IWebBrowserWindowExtensions
    {
        public static T ExecuteJScript<T>(this IWebBrowserWindow browser, string script)
        {
            var result = browser.ExecuteJScript(script);

            return (T)Convert.ChangeType(result, typeof(T));
        }

        public static WebBrowserWindowDimensions GetDimensions(this IWebBrowserWindow browser)
        {
            var width = browser.ExecuteJScript<int>("return window.outerWidth;");
            var height = browser.ExecuteJScript<int>("return window.outerHeight;");
            return new WebBrowserWindowDimensions { OuterHeight = height, OuterWidth = width };
        }

        /// <param name="booleanExpression">Ex. foo == true</param>
        public static void WaitOnJScriptExpressionTrue(this IWebBrowserWindow browser, string booleanExpression)
        {
            browser.WaitOnJScriptExpression(60, string.Format("return {0};", booleanExpression), "true");
        }
    }

    public class WebBrowserWindowDimensions
    {
        public int OuterWidth { get; internal set; }

        public int OuterHeight { get; internal set; }
    }

    internal static class BrowserNativeMethods
    {
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, SetWindowPosFlags uFlags);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool ShowWindow(IntPtr hWnd, ShowWindowCommands nCmdShow);

        [Flags]
        public enum SetWindowPosFlags : uint
        {
            /// <summary>If the calling thread and the thread that owns the window are attached to different input queues, 
            /// the system posts the request to the thread that owns the window. This prevents the calling thread from 
            /// blocking its execution while other threads process the request.</summary>
            /// <remarks>SWP_ASYNCWINDOWPOS</remarks>
            AsynchronousWindowPosition = 0x4000,
            /// <summary>Prevents generation of the WM_SYNCPAINT message.</summary>
            /// <remarks>SWP_DEFERERASE</remarks>
            DeferErase = 0x2000,
            /// <summary>Draws a frame (defined in the window's class description) around the window.</summary>
            /// <remarks>SWP_DRAWFRAME</remarks>
            DrawFrame = 0x0020,
            /// <summary>Applies new frame styles set using the SetWindowLong function. Sends a WM_NCCALCSIZE message to 
            /// the window, even if the window's size is not being changed. If this flag is not specified, WM_NCCALCSIZE 
            /// is sent only when the window's size is being changed.</summary>
            /// <remarks>SWP_FRAMECHANGED</remarks>
            FrameChanged = 0x0020,
            /// <summary>Hides the window.</summary>
            /// <remarks>SWP_HIDEWINDOW</remarks>
            HideWindow = 0x0080,
            /// <summary>Does not activate the window. If this flag is not set, the window is activated and moved to the 
            /// top of either the topmost or non-topmost group (depending on the setting of the hWndInsertAfter 
            /// parameter).</summary>
            /// <remarks>SWP_NOACTIVATE</remarks>
            DoNotActivate = 0x0010,
            /// <summary>Discards the entire contents of the client area. If this flag is not specified, the valid 
            /// contents of the client area are saved and copied back into the client area after the window is sized or 
            /// repositioned.</summary>
            /// <remarks>SWP_NOCOPYBITS</remarks>
            DoNotCopyBits = 0x0100,
            /// <summary>Retains the current position (ignores X and Y parameters).</summary>
            /// <remarks>SWP_NOMOVE</remarks>
            IgnoreMove = 0x0002,
            /// <summary>Does not change the owner window's position in the Z order.</summary>
            /// <remarks>SWP_NOOWNERZORDER</remarks>
            DoNotChangeOwnerZOrder = 0x0200,
            /// <summary>Does not redraw changes. If this flag is set, no repainting of any kind occurs. This applies to 
            /// the client area, the nonclient area (including the title bar and scroll bars), and any part of the parent 
            /// window uncovered as a result of the window being moved. When this flag is set, the application must 
            /// explicitly invalidate or redraw any parts of the window and parent window that need redrawing.</summary>
            /// <remarks>SWP_NOREDRAW</remarks>
            DoNotRedraw = 0x0008,
            /// <summary>Same as the SWP_NOOWNERZORDER flag.</summary>
            /// <remarks>SWP_NOREPOSITION</remarks>
            DoNotReposition = 0x0200,
            /// <summary>Prevents the window from receiving the WM_WINDOWPOSCHANGING message.</summary>
            /// <remarks>SWP_NOSENDCHANGING</remarks>
            DoNotSendChangingEvent = 0x0400,
            /// <summary>Retains the current size (ignores the cx and cy parameters).</summary>
            /// <remarks>SWP_NOSIZE</remarks>
            IgnoreResize = 0x0001,
            /// <summary>Retains the current Z order (ignores the hWndInsertAfter parameter).</summary>
            /// <remarks>SWP_NOZORDER</remarks>
            IgnoreZOrder = 0x0004,
            /// <summary>Displays the window.</summary>
            /// <remarks>SWP_SHOWWINDOW</remarks>
            ShowWindow = 0x0040,
        }

        public enum ShowWindowCommands
        {
            SW_FORCEMINIMIZE = 11,
            SW_HIDE = 0,
            SW_MAXIMIZE = 3,
            SW_MINIMIZE = 6,
            SW_RESTORE = 9,
            SW_SHOW = 5,
            SW_SHOWDEFAULT = 10,
            SW_SHOWMAXIMIZED = 3,
            SW_SHOWMINIMIZED = 2,
            SW_SHOWMINNOACTIVE = 7,
            SW_SHOWNA = 8,
            SW_SHOWNOACTIVE = 4,
            SW_SHOWNORMAL = 1
        }
    }
    #endregion
}
