namespace WeddingSeating
{
    /// <summary>
    /// This interface defines the methods that a data loader object must support.
    /// </summary>
    public interface IDataLoader
    {
        /// <summary>
        /// Read all contents from a text file.
        /// </summary>
        /// <returns>String containing all the text from a given source.</returns>
        string Read();
    }
}
