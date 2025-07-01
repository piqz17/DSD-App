using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSD_WinformsApp.View
{
    public class CustomButton : Button
    {
        private Color enabledColor;
        private Color disabledColor;

        public CustomButton(Color enabledColor, Color disabledColor)
        {
            this.enabledColor = enabledColor;
            this.disabledColor = disabledColor;

            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            UpdateAppearance();
        }

        protected override void OnEnabledChanged(EventArgs e)
        {
            base.OnEnabledChanged(e);
            UpdateAppearance();
        }

        private void UpdateAppearance()
        {
            BackColor = Enabled ? enabledColor : disabledColor;

            // Set the cursor based on the button's enabled state
            Cursor = Enabled ? Cursors.Hand : Cursors.Default;
        }
    }
}
