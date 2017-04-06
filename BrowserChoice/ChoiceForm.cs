using System.Configuration;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace BrowserChoice
{
    public partial class ChoiceForm : Form
    {
        private const int buttonHeight = 40;

        public ChoiceForm(string url)
        {
            InitializeComponent();

            var configuration = (WebBrowsersConfiguration)ConfigurationManager.GetSection("webBrowsers");
            var index = 0;
            foreach (var browser in configuration.webBrowsers)
            {
                var button = new Button
                {
                    Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right,
                    Location = new System.Drawing.Point(12, 15 + buttonHeight * index),
                    Margin = new Padding(12, 12, 12, 12),
                    Name = browser.Title +"_btn",
                    Size = new Size(190, buttonHeight),
                    TabIndex = index,
                    Text = browser.Title,
                    UseVisualStyleBackColor = true,
                    BackColor = ControlPaint.Light(SystemColors.ControlDarkDark, 30),
                };
                button.Click += (sender, e) =>
                {
                    Process.Start(browser.Path, url);
                    Application.Exit();
                };

                Controls.Add(button);
                index++;
            }
        }
    }
}
