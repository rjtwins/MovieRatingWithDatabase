using MovieRatingWithDatabase;

public interface IDatabaseInterface
{
    /// <summary>
    /// Get all (rows) data in database and return it as a ResultDataSet table.
    /// </summary>
    /// <returns>ResultDataSet.resultDataTable table</returns>
    ResultDataSet.resultDataTable GetAllFromDatabase();

    /// <summary>
    /// Get a row with a specific key from database and return it as a row in ResultDataSet.
    /// </summary>
    /// <param name="id"></param>
    /// <returns>ResultDataSet.resultRow row</returns>
    ResultDataSet.resultRow GetFromDatabase(string id);
    /// <summary>
    /// Put a given row in the database.
    /// </summary>
    /// <param name="row"></param>
    void PutInDatabase(ResultDataSet.resultRow row);

    /// <summary>
    /// Search given database with a specific query and return resutls as ResultDataSet table.
    /// </summary>
    /// <param name="q"></param>
    /// <returns>ResultDataSet.resultDataTable table</returns>
    ResultDataSet.resultDataTable SearchInDatabase(string q);

    /// <summary>
    /// Remove a (row) data entry from the database with given key.
    /// </summary>
    /// <param name="id"></param>
    void TryRemoveFromDatabase(string id);

    /// <summary>
    /// Update given (row) entry in database
    /// </summary>
    /// <param name="row"></param>
    void UpdateInDatabase(ResultDataSet.resultRow row);
}