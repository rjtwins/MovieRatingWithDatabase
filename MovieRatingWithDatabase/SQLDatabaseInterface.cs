using System;
using System.Data.SqlClient;
using System.Diagnostics;

namespace MovieRatingWithDatabase
{
    internal class SQLDatabaseInterface : IDatabaseInterface
    {
        private ResultDataSetTableAdapters.resultTableAdapter ResultAdaptor = new ResultDataSetTableAdapters.resultTableAdapter();

        public SQLDatabaseInterface()
        {
            string connectionString =
                "Data Source=DESKTOP-MQNG1EF;" +
                "Initial Catalog=DummyDatabase;" +
                "User id=" + UTILS.Passwords.SQLAccountName + ";" +
                "Password=" + UTILS.Passwords.SQLPassword + ";";

            ResultAdaptor.Connection = new SqlConnection(connectionString);
        }

        public ResultDataSet.resultDataTable GetAllFromDatabase()
        {
            var data = ResultAdaptor.GetData();
            foreach (ResultDataSet.resultRow row in data)
            {
                Debug.WriteLine(row.title);
            }

            return ResultAdaptor.GetData();
        }

        ResultDataSet.resultRow IDatabaseInterface.GetFromDatabase(string id)
        {
            var rows = ResultAdaptor.GetData().Rows;
            if (rows.Contains(id))
            {
                return (ResultDataSet.resultRow)rows.Find(id);
            }
            return null;
        }

        public void PutInDatabase(ResultDataSet.resultRow row)
        {
            if ((this as IDatabaseInterface).GetFromDatabase(row.id) == null)
            {
                object[] dataList = row.ItemArray;
                ResultAdaptor.Insert(dataList);
                return;
            }
            ResultAdaptor.Update(row);
        }

        //I realize you can also do an sql query that basically does the same thing but I decided on this instead.
        public ResultDataSet.resultDataTable SearchInDatabase(string q)
        {
            ResultDataSet ResultDataSet = new ResultDataSet();
            ResultAdaptor.Fill(ResultDataSet.result);
            ResultDataSet newDataSet = new ResultDataSet();
            for (int i = 0; i < ResultDataSet.result.Rows.Count; i++)
            {
                foreach (var column in ResultDataSet.result.Rows[i].ItemArray)
                {
                    if (NewDataSetAllreadyContainsKey(i))
                    {
                        continue;
                    }
                    if (IsNotString(column))
                    {
                        continue;
                    }
                    string value = column as string;
                    if (value.ToLower().Contains(q.ToLower()))
                    {
                        newDataSet.result.Rows.Add(ResultDataSet.result.Rows[i]);
                    }
                }
            }

            return newDataSet.result;

            bool NewDataSetAllreadyContainsKey(int i)
            {
                return newDataSet.result.Rows.Contains(ResultDataSet.result.Rows[i][0]);
            }

            bool IsNotString(object column)
            {
                return column.GetType() != typeof(string);
            }
        }

        public void TryRemoveFromDatabase(string id)
        {
            ResultAdaptor.DeleteQuery(id);
        }
        public void UpdateInDatabase(ResultDataSet.resultRow resultRow)
        {
            //foreach (var c in resultRow.ItemArray)
            //{
            //    Debug.WriteLine(c);
            //}
            object[] o = resultRow.ItemArray;
            //int result = ResultAdaptor.UpdateQuery(o);
            int result = ResultAdaptor.UpdateQuery
                (
                (string)o[0],
                (string)o[1],
                (string)o[2],
                (int)o[3],
                (int)o[4],
                (string)o[5],
                (int)o[6],
                (int)o[7],
                (int)o[8],
                (string)o[9],
                (string)o[10],
                (DateTime?)o[11]
                );
            Debug.WriteLine("Update result: " + result);
        }
    }
}