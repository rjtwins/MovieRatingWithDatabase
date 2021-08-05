using System;
using System.Windows.Forms;

namespace MovieRatingWithDatabase
{
    public partial class Form2 : Form, IDetailedDisplay
    {
        private IController controller;
        private Result result;

        public Form2()
        {
            InitializeComponent();
            Form2 form = this;
            this.FormClosing += new FormClosingEventHandler(OnFormClosingEvent);
        }

        public void AddDetails(Result r)
        {
            this.result = r;
            this.titleLabel.Text = r.title;
            HandleMovieOrTv(r);
            this.releaseDateLabel.Text = r.year.ToString();
            this.episodeCountLabel.Text = r.season.ToString();
            this.ratingComboBox.SelectedIndex = r.rating - 1;
            this.pictureBox1.Image = r.bitMap;
            HandleWatchedString(r);
            HandleRatingInteger(r);
            this.textBox1.Text = r.notes;
        }

        private void HandleRatingInteger(Result r)
        {
            r.rating = UTILS.LimitToRange(r.rating, 1, 5);
            this.ratingComboBox.SelectedIndex = r.rating - 1;
        }

        private void HandleWatchedString(Result r)
        {
            if (r.watched.Trim(' ') == "TRUE")
            {
                this.checkBox1.Checked = true;
                this.checkBox1.Enabled = false;
                this.watchedDate.Text = r.watchDate.ToString();
                return;
            }
            this.checkBox1.Checked = false;
        }

        private void HandleMovieOrTv(Result r)
        {
            if (r.titleType == "movie")
            {
                this.typeLabel.Text = "Movie";
                return;
            }
            this.typeLabel.Text = "TV Show";
        }

        private void Form2_Load(object sender, EventArgs e)
        {
        }

        public void RegisterController(IController controller)
        {
            this.controller = controller;
        }

        private void OnFormClosingEvent(object sender, FormClosingEventArgs e)
        {
            this.result.notes = this.textBox1.Text;
            HandleWatchedCheckBox();
            this.result.rating = this.ratingComboBox.SelectedIndex + 1;
            controller.NotifyDetailsDisplayClosing(this.result);
        }

        private void HandleWatchedCheckBox()
        {
            if (this.checkBox1.Checked)
            {
                this.result.watched = "TRUE";
                this.result.watchDate = DateTime.Parse(this.watchedDate.Text);
                return;
            }
            this.result.watched = "FALSE";
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (this.checkBox1.Checked)
            {
                this.checkBox1.Enabled = false;
                this.watchedDate.Text = DateTime.Now.ToString();
                this.result.watchDate = DateTime.Now;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
            controller.RemoveBookmark(result);
        }
    }
}