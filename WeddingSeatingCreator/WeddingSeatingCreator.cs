using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WeddingSeatingCreator
{
    /// <summary>
    /// Main seating arranger class
    /// </summary>
    public class WeddingSeatingCreator
    {
        private readonly List<WeddingTable> _weddingTables;
        private readonly List<WeddingReservation> _weddingReservations;

        public WeddingSeatingCreator(string config)
        {
            _weddingTables = (new WeddingTableParser()).Parse(config).ToList();
            _weddingReservations = (new WeddingReservationParser(_weddingTables.Select(t => t.Id).ToList())).Parse(config).ToList();
        }

        /// <summary>
        /// Assign all available tables to all unassigned reservations. The algorithm starts by looking at the reservations that have the highest number of disliked people.
        /// Then the processing continues with the smallest size reservation until no tables are available.
        /// </summary>
        public void AssignWeddingTables()
        {
            foreach (var reservation in _weddingReservations.OrderByDescending(r => r.DislikedNames.Count).ThenBy(r => r.PartySize))
            {
                var weddingTable = FindAvailableTable(reservation.AvailableTables, reservation.PartySize);

                if (weddingTable == null) throw new Exception("Unable to seat guests with the provided table configuration!");

                weddingTable.Reservations.Add(reservation);
                reservation.TableAssigned = true;
                RemoveAvailableTableForDislikedNames(reservation, weddingTable.Id);
            }
        }

        /// <summary>
        /// The result of the table assignments
        /// </summary>
        /// <returns>String representing the final assignment of the wedding tables</returns>
        public string GetTableAssignments()
        {
            var sb = new StringBuilder();

            foreach (var table in _weddingTables)
            {
                sb.AppendLine(string.Format("Table {0}: {1}", table.Id, string.Join(" & ", table.Reservations.Select(r => r.Title))));
            }

            return sb.ToString();
        }

        private WeddingTable FindAvailableTable(ICollection<string> availableTables, int partySize)
        {
            return _weddingTables.OrderBy(tbl => tbl.AvailableCapacity - partySize).FirstOrDefault(tbl =>
                availableTables.Contains(tbl.Id) && tbl.AvailableCapacity >= partySize);
        }

        private void RemoveAvailableTableForDislikedNames(WeddingReservation reservation, string tableId)
        {
            foreach (var disliked in reservation.DislikedNames)
            {
                var dislikedReservation = _weddingReservations.FirstOrDefault(r => r.Name.Equals(disliked, StringComparison.OrdinalIgnoreCase) && !r.TableAssigned);

                if (dislikedReservation != null)
                {
                    dislikedReservation.AvailableTables.Remove(tableId);
                }
            }
        }
    }
}
