using System;
using System.Drawing;
using System.Windows.Forms;

namespace MovieRatingWithDatabase
{
    public static class UTILS
    {
        public static Passwords Passwords;

        public static void DisplayFullBitmap(Bitmap img)
        {
            using (Form form = new Form())
            {
                form.StartPosition = FormStartPosition.CenterScreen;
                form.Size = img.Size;

                PictureBox pb = new PictureBox();
                pb.Dock = DockStyle.Fill;
                pb.Image = img;

                form.Controls.Add(pb);
                form.ShowDialog();
            }
        }

        public static int LimitToRange(this int value, int inclusiveMinimum, int inclusiveMaximum)
        {
            if (value < inclusiveMinimum) { return inclusiveMinimum; }
            if (value > inclusiveMaximum) { return inclusiveMaximum; }
            return value;
        }

        public static ResultDataSet.resultRow ResultToRow(Result result)
        {
            ResultDataSet template = new ResultDataSet();
            ResultDataSet.resultRow newRow = template.result.NewresultRow();
            string[] propertyNames = new string[template.result.Columns.Count];

            for (int i = 0; i < template.result.Columns.Count; i++)
            {
                string columnName = template.result.Columns[i].ColumnName;
                var propInfo = result.GetType().GetProperty(columnName);
                if (propInfo == null)
                {
                    continue;
                }
                var value = propInfo.GetValue(result);
                if (value != DBNull.Value && value != null)
                {
                    newRow[i] = value;
                    continue;
                }
                if (propInfo.PropertyType == typeof(string))
                {
                    newRow[i] = "";
                    continue;
                }
                if (propInfo.PropertyType == typeof(int))
                {
                    newRow[i] = 0;
                    continue;
                }
                //Debug.WriteLine("Matching: " + columnName + " value: " + newRow[i]);
            }
            return newRow;
        }

        public static Result RowToResult(ResultDataSet.resultRow row)
        {
            Result result = new Result();
            ResultDataSet template = new ResultDataSet();
            string[] propertyNames = new string[template.result.Columns.Count];

            for (int i = 0; i < template.result.Columns.Count; i++)
            {
                string columnName = template.result.Columns[i].ColumnName;
                var propInfo = result.GetType().GetProperty(columnName);
                //Debug.WriteLine("Matching: " + columnName + " value: " + row[i]);
                if (propInfo == null)
                {
                    continue;
                }
                var value = row[i];
                if (value != DBNull.Value && value != null)
                {
                    propInfo.SetValue(result, row[i], null);
                    continue;
                }
                if (propInfo.PropertyType == typeof(string))
                {
                    propInfo.SetValue(result, "", null);
                    continue;
                }
                if (propInfo.PropertyType == typeof(int))
                {
                    propInfo.SetValue(result, 0, null);
                    continue;
                }
            }
            return result;
        }
    }
}