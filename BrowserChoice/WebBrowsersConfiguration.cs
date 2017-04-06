using System.Collections.Generic;
using System.Configuration;

namespace BrowserChoice
{
    public class WebBrowsersConfiguration : ConfigurationSection
    {
        [ConfigurationProperty("", IsDefaultCollection = true)]
        [ConfigurationCollection(typeof(WebBrowserCollection), AddItemName = "webBrowser")]
        public WebBrowserCollection webBrowsers => (WebBrowserCollection)this[""];
    }

    public class WebBrowserCollection : ConfigurationElementCollection, IEnumerable<WebBrowserConfiguration>
    {
        private readonly List<WebBrowserConfiguration> webBrowsers;

        public WebBrowserCollection()
        {
            this.webBrowsers = new List<WebBrowserConfiguration>();
        }

        protected override ConfigurationElement CreateNewElement()
        {
            var element = new WebBrowserConfiguration();
            this.webBrowsers.Add(element);
            return element;
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((WebBrowserConfiguration)element).Title;
        }

        public new IEnumerator<WebBrowserConfiguration> GetEnumerator()
        {
            return this.webBrowsers.GetEnumerator();
        }
    }

    public class WebBrowserConfiguration : ConfigurationElement
    {
        [ConfigurationProperty("title", IsKey = true, IsRequired = true)]
        public string Title => (string)this["title"];

        [ConfigurationProperty("path", IsRequired = true)]
        public string Path => (string)this["path"];
    }
}