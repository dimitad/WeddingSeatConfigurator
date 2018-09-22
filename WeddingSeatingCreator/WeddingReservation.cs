using System.Collections.Generic;
using System.Linq;

namespace WeddingSeatingCreator
{
    /// <summary>
    /// This class represents a wedding reservation for a guest
    /// </summary>
    internal class WeddingReservation
    {
        /// <summary>
        /// The title of the reservation (excludes disliked list of people if there is one)
        /// </summary>
        internal string Title { get; set; }

        /// <summary>
        /// The guest name on the reservation
        /// </summary>
        internal string Name { get; set; }

        /// <summary>
        /// The number of people in the guest's party
        /// </summary>
        internal int PartySize { get; set; }

        /// <summary>
        /// Boolean indicator whether a table has been assigned to the wedding reservation
        /// </summary>
        internal bool TableAssigned { get; set; }

        /// <summary>
        /// Collection of people that the guest dislikes and should be excluded from the table
        /// </summary>
        internal List<string> DislikedNames { get; set; }

        /// <summary>
        /// Collection of tables that are still available to the reservation
        /// </summary>
        internal List<string> AvailableTables { get; set; }

        /// <summary>
        /// Overrides the default implementation of Equals to support unit testing
        /// </summary>
        /// <param name="obj">The other WeddingReservation class being compared</param>
        /// <returns>True if the 2 classes are equal, false otherwise</returns>
        public override bool Equals(object obj)
        {
            var other = obj as WeddingReservation;
            return other != null && other.Title.Equals(Title)
                && other.Name.Equals(Name)
                && other.PartySize.Equals(PartySize)
                && other.TableAssigned == TableAssigned
                && other.DislikedNames.SequenceEqual(DislikedNames)
                && other.AvailableTables.SequenceEqual(AvailableTables);
        }

        /// <summary>
        /// Overrides the default implementation of GetHashCode to support using this class in unit tests
        /// </summary>
        /// <returns>Hash for the class</returns>
        public override int GetHashCode()
        {
            var hashCode = 1976174649;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Title);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Name);
            hashCode = hashCode * -1521134295 + PartySize.GetHashCode();
            hashCode = hashCode * -1521134295 + TableAssigned.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<List<string>>.Default.GetHashCode(DislikedNames);
            hashCode = hashCode * -1521134295 + EqualityComparer<List<string>>.Default.GetHashCode(AvailableTables);
            return hashCode;
        }
    }
}
