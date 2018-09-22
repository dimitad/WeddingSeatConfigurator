using System.Collections.Generic;
using System.Linq;

namespace WeddingSeatingCreator
{
    /// <summary>
    /// This class represents a wedding table object that can be used to sit wedding guests
    /// </summary>
    internal class WeddingTable
    {
        /// <summary>
        /// Table identifier from the seating configuration
        /// </summary>
        internal string Id { get; set; }

        /// <summary>
        /// The size of the wedding table
        /// </summary>
        internal int TotalCapacity { get; set; }

        /// <summary>
        /// Calculated field tracking the remaining available seats on a wedding table
        /// </summary>
        internal int AvailableCapacity
        {
            get { return TotalCapacity - Reservations.Sum(r => r.PartySize); }
        }

        /// <summary>
        /// Collection of current reservations assigned to the wedding table
        /// </summary>
        internal List<WeddingReservation> Reservations { get; set; }

        /// <summary>
        /// Overrides the default implementation of Equals to support unit testing
        /// </summary>
        /// <param name="obj">The other WeddingTable class being compared</param>
        /// <returns>True if the 2 classes are equal, false otherwise</returns>
        public override bool Equals(object obj)
        {
            var other = obj as WeddingTable;
            return other != null && other.Id.Equals(Id) 
                && other.TotalCapacity.Equals(TotalCapacity) 
                && other.AvailableCapacity.Equals(AvailableCapacity) 
                && other.Reservations.SequenceEqual(Reservations);
        }

        /// <summary>
        /// Overrides the default implementation of GetHashCode to support using this class in unit tests
        /// </summary>
        /// <returns>Hash for the class</returns>
        public override int GetHashCode()
        {
            var hashCode = 1141414123;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Id);
            hashCode = hashCode * -1521134295 + TotalCapacity.GetHashCode();
            hashCode = hashCode * -1521134295 + AvailableCapacity.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<List<WeddingReservation>>.Default.GetHashCode(Reservations);
            return hashCode;
        }
    }
}
