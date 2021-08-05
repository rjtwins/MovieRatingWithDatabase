using MovieRatingWithDatabase;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Net;
using System.Threading.Tasks;

abstract class IDataController
{
    public IDatabaseInterface DatabaseInterface;
    private protected ResultDataSet DataSet = new ResultDataSet();
    private protected bool StopSearchFlag = false;

    /// <summary>
    /// Search internet by a given title and return as list.
    /// </summary>
    /// <param name="title"></param>
    /// <returns>List<Result> results</Result></returns>
    public abstract List<Result> SearchWebByTitle(string title);

    /// <summary>
    /// Search web by a unique ID (this may only work for cirtain APIs)
    /// </summary>
    /// <param name="id"></param>
    /// <returns>Result result</returns>
    public abstract Result SearchWebByID(string id);

    /// <summary>
    /// Async get a Bitmap from a given image url.
    /// </summary>
    /// <param name="url"></param>
    /// <returns>Task<Bitmap> getBitmapTask</Bitmap></returns>
    public async virtual Task<Bitmap> GetBitMapFromURL(string url)
    {
        Debug.WriteLine("image URL:" + url);
        WebRequest requ = WebRequest.Create(url);
        WebResponse resp = await requ.GetResponseAsync();
        System.IO.Stream respStream = resp.GetResponseStream();
        Bitmap bmp = new Bitmap(respStream);
        respStream.Dispose();
        return bmp;
    }

    /// <summary>
    /// Get a entry from underlaying database by unique id key.
    /// </summary>
    /// <param name="id"></param>
    /// <returns>Result result</returns>
    public virtual Result GetFromDatabase(string id)
    {
        ResultDataSet.resultRow row = DatabaseInterface.GetFromDatabase(id);
        return UTILS.RowToResult(row);
    }

    public abstract void LoadPasswordFile();

    /// <summary>
    /// Update-Add a result in the database.
    /// </summary>
    /// <param name="r"></param>
    public virtual void StoreResultInDatabase(Result r)
    {
        DatabaseInterface.PutInDatabase(UTILS.ResultToRow(r));
    }

    /// <summary>
    /// Stop current and future websearch.
    /// </summary>
    public virtual void StopSearch()
    {
        this.StopSearchFlag = true;
    }

    /// <summary>
    /// Allow furture web search.
    /// </summary>
    public virtual void EnableSearch()
    {
        this.StopSearchFlag = false;
    }

    /// <summary>
    /// Search database by given title.
    /// </summary>
    /// <param name="title"></param>
    /// <returns>List<result> results</result></returns>
    public virtual List<Result> SearchDatabase(string title)
    {
        List<Result> results = new List<Result>();
        foreach(ResultDataSet.resultRow row in DatabaseInterface.SearchInDatabase(title).Rows)
        {
            results.Add(UTILS.RowToResult(row));
        }
        return results;
    }

    /// <summary>
    /// Get all entries from database.
    /// </summary>
    /// <returns>List<Result> results</Result></returns>
    public virtual List<Result> GetAllFromDatabase()
    {
        List<Result> results = new List<Result>();
        var dataTable = DatabaseInterface.GetAllFromDatabase();
        foreach(ResultDataSet.resultRow row in dataTable.Rows)
        {
            Result r = UTILS.RowToResult(row);
            results.Add(r);
        }
        return results;
    }
    /// <summary>
    /// Update given result in database.
    /// </summary>
    /// <param name="r"></param>
    public virtual void UpdateInDatabase(Result r)
    {
        DatabaseInterface.UpdateInDatabase(UTILS.ResultToRow(r));
    }

    /// <summary>
    /// Remove given Result from database.
    /// </summary>
    /// <param name="r"></param>
    public virtual void RemoveFromDataBase(Result r)
    {
        this.DatabaseInterface.TryRemoveFromDatabase(r.id);
    }
}
