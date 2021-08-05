using MovieRatingWithDatabase;
using System.Collections.Generic;
using System.Windows.Forms;

interface IDisplay
{
    /// <summary>
    /// Display a list of results to the user via a display
    /// </summary>
    /// <param name="r"></param>
    void DisplayPublicResults(List<Result> r);
    /// <summary>
    /// Display a list of private (bookmark) results to a user via display.
    /// </summary>
    /// <param name="r"></param>
    void DisplayPrivateResults(List<Result> r);

    /// <summary>
    /// Remove a result from the private results (bookmarks).
    /// </summary>
    /// <param name="id"></param>
    void RemovePrivateResult(string id);
}

interface IDetailedDisplay
{
    /// <summary>
    /// Add details to the details display by supplying a Result object with the details.
    /// </summary>
    /// <param name="r"></param>
    void AddDetails(Result r);
    /// <summary>
    /// Show details display to user.
    /// </summary>
    void Show();
    /// <summary>
    /// Register the controller to details display so it may notify the controller.
    /// </summary>
    /// <param name="controller"></param>
    void RegisterController(IController controller);
}