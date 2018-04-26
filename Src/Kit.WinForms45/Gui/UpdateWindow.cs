using System;
using System.Collections.Generic;
using System.Globalization;
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
#pragma warning disable 1305

            var caption = string.Format(CultureInfo.CurrentCulture, 
                $"New Version Online avalailable\n" + 
                $"Local: {0}\n" +
                $"Remote: {1}\n" +
                $"Notes: {2}", 
                _localVersion, newVersion.Version, newVersion.Notes);

            MessageBox.Show(
                caption,
                "Update",
                MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
#pragma warning restore 1305
        }
    }
}
