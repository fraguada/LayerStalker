using Rhino.PlugIns;
using Rhino.UI;

namespace LayerStalker
{
    ///<summary>
    /// <para>Every RhinoCommon .rhp assembly must have one and only one PlugIn-derived
    /// class. DO NOT create instances of this class yourself. It is the
    /// responsibility of Rhino to create an instance of this class.</para>
    /// <para>To complete plug-in information, please also see all PlugInDescription
    /// attributes in AssemblyInfo.cs (you might need to click "Project" ->
    /// "Show All Files" to see it in the "Solution Explorer" window).</para>
    ///</summary>
    public class LayerStalkerPlugIn : Rhino.PlugIns.PlugIn

    {
        public LayerStalkerPlugIn()
        {
            Instance = this;
        }

        /// <summary>
        /// The tabbed dockbar user control
        /// </summary>
        public LayerStalkerControl UserControl { get; set; }

        ///<summary>Gets the only instance of the RhinoPanelD3PlugIn plug-in.</summary>
        public static LayerStalkerPlugIn Instance
        {
            get; private set;
        }

        // You can override methods here to change the plug-in behavior on
        // loading and shut down, add options pages to the Rhino _Option command
        // and mantain plug-in wide options in a document.

        protected override LoadReturnCode OnLoad(ref string errorMessage)
        {
            var panel_type = typeof(LayerStalkerControl);
            Panels.RegisterPanel(this, panel_type, "Layer Stalker", Properties.Resources.RhinoD3);

            RhinoEventListeners.Instance.Enable(true);
            RhinoEventListeners.Instance.WriteLayers();

            return LoadReturnCode.Success;
        }
    }
}