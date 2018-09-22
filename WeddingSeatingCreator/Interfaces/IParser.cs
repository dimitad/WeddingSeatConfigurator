using System.Collections.Generic;

namespace WeddingSeatingCreator.Interfaces
{
    /// <summary>
    /// This interface defines the methods that a wedding object parser must support.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IParser<out T>
    {
        /// <summary>
        /// Parse the wedding configuration based on each object's regex setting.
        /// </summary>
        /// <param name="config">The wedding seating configuration</param>
        /// <returns>An enumeration of the matches for the wedding object</returns>
        IEnumerable<T> Parse(string config);
    }
}
