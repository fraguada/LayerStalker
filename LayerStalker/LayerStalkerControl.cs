using System;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.IO;
using System.Reflection;
using System.Diagnostics;

namespace LayerStalker
{
    [Guid("06FC7EBF-A09E-4B69-A556-7ADE0E6580FF")]
    public partial class LayerStalkerControl : UserControl
    {
        public WebBrowser Browser { get; private set; }
        private string Index;
        private bool IndexLoaded = false;

        public static Guid PanelId
        {
            get
            {
                return typeof(LayerStalkerControl).GUID;
            }
        }

        public LayerStalkerControl()
        {
            InitializeComponent();
            InitializeBrowser();
            LayerStalkerPlugIn.Instance.UserControl = this;
            Controls.Add(Browser);
            Browser.Dock = DockStyle.Fill;
            Disposed += new EventHandler(OnDisposed);

        }



        private void InitializeBrowser()
        {
            Browser = new WebBrowser();
            Browser.DocumentCompleted += OnBrowser_DocumentCompleted;
            Browser.Navigating += OnBrowser_Navigating;
            Browser.VisibleChanged += OnBrowser_VisibleChanged;
#if !DEBUG
            Index = "http://localhost:8080";
#else
            var path = Directory.GetParent(Assembly.GetExecutingAssembly().Location);

            var indexPath = string.Format(@"{0}\app\index.html", path);

            if (!File.Exists(indexPath))
                Rhino.RhinoApp.WriteLine("Error. The html file doesn't exists : {0}", indexPath);

            Index = indexPath.Replace("\\", "/");
#endif
            Browser.Url = new Uri(Index);
        }


        internal void UpdateLayers(string json)
        {

            if (!IndexLoaded) return;

            Object[] objArray = new Object[1];
            objArray[0] = json;

            var result = Browser.Document.InvokeScript("updateLayers", objArray).ToString();

        }

        private void OnBrowser_VisibleChanged(object sender, EventArgs e)
        {
            if (((WebBrowser)sender).Visible && IndexLoaded)
            {
                RhinoEventListeners.Instance.WriteLayers();
            }
        }

        private void OnBrowser_Navigating(object sender, WebBrowserNavigatingEventArgs e)
        {
            Debug.WriteLine("Browser Navigating","Layer Stalker");
            if (IndexLoaded) e.Cancel = true;

        }
            

        private void OnBrowser_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            Debug.WriteLine("Browser Document Completed", "Layer Stalker");
            if (e.Url.AbsolutePath == Index) IndexLoaded = true;
            RhinoEventListeners.Instance.WriteLayers();
        }

        private void OnDisposed(object sender, EventArgs e)
        {
            Browser.Dispose();

            LayerStalkerPlugIn.Instance.UserControl = null;
        }

        
    }
}
