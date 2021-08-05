using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MovieRatingWithDatabase
{
    public partial class Form1 : Form, IDisplay
    {
        public ImageList ImageList = new ImageList();
        public ImageList BookmarkImageList = new ImageList();

        IController Controller;

        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            this.ImageList.ImageSize = new Size(128, 128);
            this.BookmarkImageList.ImageSize = new Size(128, 128);
            this.PublicDisplayListView.LargeImageList = ImageList;
            this.PrivateDisplayListView.LargeImageList = BookmarkImageList;
            SetColorMode();
        }

        private List<Control> GetAllControls(Control container, List<Control> list)
        {
            foreach (Control c in container.Controls)
            {
                list.Add(c);
                if (c.Controls.Count > 0)
                {
                    list = GetAllControls(c, list);
                }
            }

            return list;
        }
        private List<Control> GetAllControls(Control container)
        {
            return GetAllControls(container, new List<Control>() { container });
        }

        private void SetColorMode()
        {
            List<Control> controlls = GetAllControls(this);
            foreach (Control component in controlls)
            {
                //Debug.WriteLine(component.GetType());
                component.BackColor = Color.White;
                component.ForeColor = Color.Black;
            }
        }

        internal void SetController(IController Controller)
        {
            this.Controller = Controller;
        }

        public void DisplayPrivateResults(List<Result> results)
        {
            //this.BookmarkImageList.Images.Clear();
            //this.PrivateDisplayListView.Items.Clear();

            for (int i = 0; i < results.Count; i++)
            { 
                //UTILS.DisplayFullBitmap(results[i].bitMap);
                Result r = results[i];
                AddBookmarkBitmap(r);
                PrivateDisplayListView.Items.Add(r.id, r.title, r.id);
            }
        }

        public void DisplayPublicResults(List<Result> results)
        {
            this.ImageList.Images.Clear();
            this.PublicDisplayListView.Items.Clear();

            PublicDisplayListView.LargeImageList = ImageList;
            PublicDisplayListView.SmallImageList = ImageList;

            for (int i = 0; i < results.Count; i++)
            {
                Result r = results[i];
                ListViewItem item = new ListViewItem(r.title, r.id);
                AddPublicBitmap(r);
                PublicDisplayListView.Items.Add(item);
            }
        }

        private void AddBookmarkBitmap(Result r)
        {
            //Debug.WriteLine(r.title + " " + r.id + " " + r.image.url);
            if (r.bitMap == null)
            {
                return;
            }
            BookmarkImageList.Images.Add(r.id, r.bitMap);
        }

        private void AddPublicBitmap(Result r)
        {
            if (r.bitMap == null)
            {
                return;
            }
            ImageList.Images.Add(r.id, r.bitMap);
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //ListViewItem i = new ListViewItem();
            //PrivateDisplayListView.Items.Add()
        }

        /// <summary>
        /// Called when text changed in the search menu.
        /// Will start a timer that will start a search 1.5 secondes after the user is done typing.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void waterMarkTextBox1_TextChanged(object sender, EventArgs e)
        {
            if(waterMarkTextBox1.Text.Length > 0 && timer1.Enabled)
            {
                timer1.Stop();
                timer1.Interval = 1500;
            }
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (waterMarkTextBox1.Text.Length == 0)
                return;

            HandlePublicSearch();

            Console.WriteLine("TEST123");

            HandlePrivateSearch();

            timer1.Stop();
            timer1.Interval = 1500;
        }

        private void HandlePrivateSearch()
        {
            if (tabControl1.SelectedTab.Text != "Bookmarks")
            {
                return;
            }
            Controller.HandlPrivateSearchString(waterMarkTextBox1.Text);

        }

        private void HandlePublicSearch()
        {
            if (tabControl1.SelectedTab.Text != "Search")
            {
                return;
            }
            Controller.HandlePublicSearchString(waterMarkTextBox1.Text);
        }

        private void PrivateDisplayListView_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ListViewItem item = PrivateDisplayListView.SelectedItems[0];
            //ListViewHitTestInfo info = PublicDisplayListView.HitTest(e.X, e.Y);
            //ListViewItem item = info.Item;

            if (item == null)
            {
                return;
            }

            //Controller.AddIdToBookmarks(item.Index);
            Controller.SetupDetailedDisplay(item.ImageKey);
        }

        private void PublicDisplayListView_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ListViewItem item = PublicDisplayListView.SelectedItems[0];
            //ListViewHitTestInfo info = PublicDisplayListView.HitTest(e.X, e.Y);
            //ListViewItem item = info.Item;

            if (item == null)
            {
                return;
            }

            Controller.AddIdToBookmarks(item.Index);
        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        private void PrivateDisplayListView_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        public void RemovePrivateResult(string id)
        {
            this.PrivateDisplayListView.Items.RemoveByKey(id);
        }
    }

    class WaterMarkTextBox : TextBox
    {
        private Font oldFont = null;
        private Boolean waterMarkTextEnabled = false;

        #region Attributes 
        private Color _waterMarkColor = Color.Gray;
        public Color WaterMarkColor
        {
            get { return _waterMarkColor; }
            set
            {
                _waterMarkColor = value; Invalidate();/*thanks to Bernhard Elbl
                                                              for Invalidate()*/
            }
        }

        private string _waterMarkText = "Water Mark";
        public string WaterMarkText
        {
            get { return _waterMarkText; }
            set { _waterMarkText = value; Invalidate(); }
        }
        #endregion

        //Default constructor
        public WaterMarkTextBox()
        {
            JoinEvents(true);
        }

        //Override OnCreateControl ... thanks to  "lpgray .. codeproject guy"
        protected override void OnCreateControl()
        {
            base.OnCreateControl();
            WaterMark_Toggel(null, null);
        }

        //Override OnPaint
        protected override void OnPaint(PaintEventArgs args)
        {
            // Use the same font that was defined in base class
            System.Drawing.Font drawFont = new System.Drawing.Font(Font.FontFamily,
                Font.Size, Font.Style, Font.Unit);
            //Create new brush with gray color or 
            SolidBrush drawBrush = new SolidBrush(WaterMarkColor);//use Water mark color
            //Draw Text or WaterMark
            args.Graphics.DrawString((waterMarkTextEnabled ? WaterMarkText : Text),
                drawFont, drawBrush, new PointF(0.0F, 0.0F));
            base.OnPaint(args);
        }

        private void JoinEvents(Boolean join)
        {
            if (join)
            {
                this.TextChanged += new System.EventHandler(this.WaterMark_Toggel);
                this.LostFocus += new System.EventHandler(this.WaterMark_Toggel);
                this.FontChanged += new System.EventHandler(this.WaterMark_FontChanged);
                //No one of the above events will start immeddiatlly 
                //TextBox control still in constructing, so,
                //Font object (for example) couldn't be catched from within
                //WaterMark_Toggle
                //So, call WaterMark_Toggel through OnCreateControl after TextBox
                //is totally created
                //No doupt, it will be only one time call

                //Old solution uses Timer.Tick event to check Create property
            }
        }

        private void WaterMark_Toggel(object sender, EventArgs args)
        {
            if (this.Text.Length <= 0)
                EnableWaterMark();
            else
                DisbaleWaterMark();
        }

        private void EnableWaterMark()
        {
            //Save current font until returning the UserPaint style to false (NOTE:
            //It is a try and error advice)
            oldFont = new System.Drawing.Font(Font.FontFamily, Font.Size, Font.Style,
               Font.Unit);
            //Enable OnPaint event handler
            this.SetStyle(ControlStyles.UserPaint, true);
            this.waterMarkTextEnabled = true;
            //Triger OnPaint immediatly
            Refresh();
        }

        private void DisbaleWaterMark()
        {
            //Disbale OnPaint event handler
            this.waterMarkTextEnabled = false;
            this.SetStyle(ControlStyles.UserPaint, false);
            //Return back oldFont if existed
            if (oldFont != null)
                this.Font = new System.Drawing.Font(oldFont.FontFamily, oldFont.Size,
                    oldFont.Style, oldFont.Unit);
        }

        private void WaterMark_FontChanged(object sender, EventArgs args)
        {
            if (waterMarkTextEnabled)
            {
                oldFont = new System.Drawing.Font(Font.FontFamily, Font.Size, Font.Style,
                    Font.Unit);
                Refresh();
            }
        }
    }
}
