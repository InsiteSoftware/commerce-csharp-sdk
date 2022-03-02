using System;
using System.Collections.Generic;

namespace CommerceApiSDK.Models.Results
{
    public class DashboardpanelsResult : BaseModel
    {
        public List<DashboardPanel> dashboardPanels { get; set; }
    }

    public class DashboardPanel
    {
        public string text { get; set; }
        public string quickLinkText { get; set; }
        public string url { get; set; }
        public int count { get; set; }
        public bool isPanel { get; set; }
        public bool isQuickLink { get; set; }
        public string panelType { get; set; }
        public int order { get; set; }
        public int quickLinkOrder { get; set; }
        public bool openInNewTab { get; set; }
    }
}
