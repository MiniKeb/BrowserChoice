using System;
using System.Configuration;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace BrowserChoice
{
    public partial class ChoiceForm : Form
    {
        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn
        (
            int nLeftRect,     // x-coordinate of upper-left corner
            int nTopRect,      // y-coordinate of upper-left corner
            int nRightRect,    // x-coordinate of lower-right corner
            int nBottomRect,   // y-coordinate of lower-right corner
            int nWidthEllipse, // width of ellipse
            int nHeightEllipse // height of ellipse
        );

        private const int containerWidth = 204;
        private const int containerHeight = 100;
        private const int buttonWidth = 164;
        private const int buttonHeight = 44;

        public ChoiceForm(string url)
        {
            InitializeComponent();

            var configuration = (WebBrowsersConfiguration)ConfigurationManager.GetSection("webBrowsers");
            var newContainerHeight = containerHeight + 50 * (configuration.webBrowsers.Count - 1);

            this.FormBorderStyle = FormBorderStyle.None;
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, containerWidth, newContainerHeight, 20, 20));

            this.Height = newContainerHeight + 20;
            this.linkUrl.Text = url;

            var index = 0;
            foreach (var browser in configuration.webBrowsers)
            {
                var button = new Button
                {
                    Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right,
                    Location = new System.Drawing.Point(20, 40 + buttonHeight * index + 5 * index),
                    //Margin = new Padding(12, 12, 12, 12),
                    Name = browser.Title + "_btn",
                    Size = new Size(buttonWidth, buttonHeight),
                    TabIndex = index,
                    Text = browser.Title,
                    UseVisualStyleBackColor = true,
                    BackColor = Color.FromArgb(73, 117, 82),
                    FlatStyle = FlatStyle.Flat,
                    Font = new Font("Segoe UI", 12, FontStyle.Bold),
                    ForeColor = Color.FromArgb(234, 228, 218),


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
