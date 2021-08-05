using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MovieRatingWithDatabase
{
    class Controller : IController
    {
        private BackgroundWorker SearchWorker = new BackgroundWorker();
        private BackgroundWorker BitMapWorker = new BackgroundWorker();
        private BackgroundWorker AddResultsToBookmarksAwaiter = new BackgroundWorker();

        IDisplay Display;
        IDataController DataController;

        Dictionary<string, Result> ResultsInMemory = new Dictionary<string, Result>();
        Dictionary<int, string> SearchIndexToIdDict = new Dictionary<int, string>();
        Dictionary<string, Result> Bookmarks = new Dictionary<string, Result>();

        public Controller(IDisplay Display, IDataController dataController)
        {
            this.Display = Display;
            this.DataController = dataController;

            SearchWorker.DoWork += SearchWorker_DoWork;
            SearchWorker.RunWorkerCompleted += SearchWorker_RunWorkerCompleted;
            SearchWorker.WorkerSupportsCancellation = true;

            BitMapWorker.DoWork += BitMapWorker_DoWork;
            BitMapWorker.RunWorkerCompleted += BitMapWorker_RunWorkerCompleted;
            BitMapWorker.WorkerSupportsCancellation = false;

            AddResultsToBookmarksAwaiter.DoWork += AddResultsToBookmarksAwaiter_DoWork;
            AddResultsToBookmarksAwaiter.RunWorkerCompleted += AddResultsToBookmarksAwaiter_RunWorkerCompleted;
            
            GetBookmarks();
        }

        private void AddResultsToBookmarksAwaiter_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            List<Result> results = e.Result as List<Result>;
            FinishAddResultsToBookmarks(e.Result as List<Result>);
        }

        private void AddResultsToBookmarksAwaiter_DoWork(object sender, DoWorkEventArgs e)
        {
            object[] arguments = e.Argument as object[];
            Task[] taskArray = arguments[0] as Task[];
            List<Result> resultList = arguments[1] as List<Result>;

            Task.WaitAll(taskArray);

            for (int i = 0; i < taskArray.Length; i++)
            {
                resultList[i].bitMap = (taskArray[i] as Task<Bitmap>).Result as Bitmap;
                //UTILS.DisplayFullBitmap(resultList[i].bitMap);
            }
            e.Result = resultList;
        }

        private void GetBookmarks()
        {
            var results = DataController.GetAllFromDatabase();
            AddResultsToBookmarks(results);
        }

        private void BitMapWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            FinishAddIdToBookmarks(e.Result as Result);
        }

        private void BitMapWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            Result r = e.Argument as Result;
            r.bitMap = DataController.GetBitMapFromURL(r.image.url).Result;
            e.Result = r;
        }

        private void SearchWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            DataController.EnableSearch();

            if(e.Result == null)
            {
                return;
            }
            this.Display.DisplayPublicResults(e.Result as List<Result>);
        }

        private void SearchWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;

            if (worker.CancellationPending)
            {
                e.Result = null;
                return;
            }
            var temp = this.DataController.SearchWebByTitle(e.Argument as string);

            if (temp == null)
            {
                e.Result = null;
                return;
            }

            for (int i = 0; i < temp.Count; i++)
            {
                var r = temp[i];
                Debug.WriteLine(r.title + " : " + r.id);
                ResultsInMemory[r.id] = r;
                SearchIndexToIdDict[i] = r.id;
            }
            e.Result = temp;
        }

        public void HandlePublicSearchString(string query)
        {
            if (SearchWorker.IsBusy)
            {
                return;
            }
            SearchWorker.RunWorkerAsync(query);
        }

        public void HandlPrivateSearchString(string q)
        {
            throw new NotImplementedException();
        }

        private void AddResultsToBookmarks(List<Result> results)
        {
            List<Task> taskList = new List<Task>();
            foreach(Result r in results)
            {
                this.Bookmarks[r.id] = r;
                Task<Bitmap> t = GetBitmapAsync(r);
                taskList.Add(t);
            }
            AddResultsToBookmarksAwaiter.RunWorkerAsync(new Object[]{ taskList.ToArray(), results});
        }

        private void FinishAddResultsToBookmarks(List<Result> results)
        {
            Display.DisplayPrivateResults(results);
        }

        private async Task<Bitmap> GetBitmapAsync(Result r)
        {
            return await DataController.GetBitMapFromURL(r.imageUrl);
        }

        public void AddIdToBookmarks(string id)
        {
            if (Bookmarks.ContainsKey(id))
            {
                return;
            }
            var result = ResultsInMemory[id];
            result = FillUserFields(result);
            this.Bookmarks[id] = result;
            DataController.StoreResultInDatabase(result);
            BitMapWorker.RunWorkerAsync(result);
        }

        private Result FillUserFields(Result result)
        {
            result.rating = 0;
            result.notes = "";
            result.watched = "false";
            result.watchDate = DateTime.Parse("9999-01-01");
            result.notes = "";

            return result;
        }

        private void FinishAddIdToBookmarks(Result result)
        {
            Display.DisplayPrivateResults(new List<Result>() { result });
        }

        public void AddIdToBookmarks(int index)
        {
            var id = SearchIndexToIdDict[index];
            AddIdToBookmarks(id);
        }

        public void StopSearch()
        {
            if (!this.SearchWorker.IsBusy)
            {
                return;
            }
            this.SearchWorker.CancelAsync();
            DataController.StopSearch();
        }

        public void GetResultFromIndex(int index)
        {
            throw new NotImplementedException();
        }

        public void SetupDetailedDisplay(string id)
        {
            IDetailedDisplay detailsDisplay = new Form2();
            detailsDisplay.RegisterController(this);
            detailsDisplay.AddDetails(Bookmarks[id]);
            Debug.WriteLine(Bookmarks[id].watched);
            detailsDisplay.Show();
        }

        public void NotifyDetailsDisplayClosing(Result r)
        {
            DataController.UpdateInDatabase(r);
        }

        public void RemoveBookmark(Result r)
        {
            this.Bookmarks.Remove(r.id);
            this.Display.RemovePrivateResult(r.id);
            this.DataController.RemoveFromDataBase(r);
        }
    }
}
