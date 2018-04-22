using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Microsoft.HockeyApp.Gui
{
    public class UpdateWindow
    {
        private IAppVersion newVersion;
        private Version _localVersion;

        public UpdateWindow(IAppVersion newVersion, Version localVersion)
        {
            this.newVersion = newVersion;
            _localVersion = localVersion;
        }

        internal void Show()
        {
            MessageBox.Show($"New Version Online avalailable\nLocal: {_localVersion.ToString()}\nRemote: {newVersion.Version}\nNotes: {newVersion.Notes}", "Update",
                MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
        }
    }
}
