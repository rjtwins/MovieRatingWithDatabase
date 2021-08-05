using System.Collections.Generic;

internal interface IDetailedDisplay
{
    /// <summary>
    /// Add details to the details display by supplying a Result object with the details.
    /// </summary>
    /// <param name="r"></param>
    void AddDetails(Result r);

    /// <summary>
    /// Register the controller to details display so it may notify the controller.
    /// </summary>
    /// <param name="controller"></param>
    void RegisterController(IController controller);

    /// <summary>
    /// Show details display to user.
    /// </summary>
    void Show();
}

internal interface IDisplay
{
    /// <summary>
    /// Display a list of private (bookmark) results to a user via display.
    /// </summary>
    /// <param name="r"></param>
    void DisplayPrivateResults(List<Result> r);

    /// <summary>
    /// Display a list of results to the user via a display
    /// </summary>
    /// <param name="r"></param>
    void DisplayPublicResults(List<Result> r);
    /// <summary>
    /// Remove a result from the private results (bookmarks).
    /// </summary>
    /// <param name="id"></param>
    void RemovePrivateResult(string id);
}
