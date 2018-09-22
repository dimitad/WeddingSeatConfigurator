using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using WeddingSeatingCreator.Interfaces;

namespace WeddingSeatingCreator
{
    /// <summary>
    /// Class that parses all the wedding reservations from the seating configuration input
    /// </summary>
    internal class WeddingReservationParser : IParser<WeddingReservation>
    {
        private const string Regex = @"^(\b\w+\b[,].*?)(\d+)(?:\s+dislikes\s+)?(.*?)$";
        private readonly List<string> _availableWeddingTables;

        public WeddingReservationParser(List<string> availableWeddingTables)
        {
            _availableWeddingTables = availableWeddingTables;
        }

        /// <summary>
        /// Parses the seating configuration for individual reservations
        /// </summary>
        /// <param name="config">The wedding seating configuration</param>
        /// <returns></returns>
        public IEnumerable<WeddingReservation> Parse(string config)
        {
            var weddingReservationList = new List<WeddingReservation>();

            if (string.IsNullOrEmpty(config)) return weddingReservationList;

            foreach (var reservation in GetWeddingReservationsFromConfig(config))
            {
                _availableWeddingTables.ForEach((tbl) => reservation.AvailableTables.Add(tbl));
                weddingReservationList.Add(reservation);
            }

            AttachDislikedNames(weddingReservationList);

            return weddingReservationList;
        }

        private IEnumerable<WeddingReservation> GetWeddingReservationsFromConfig(string config)
        {
            var regex = new Regex(Regex, RegexOptions.Multiline);
            var matches = regex.Matches(config);

            if (matches.Count == 0) throw new ArgumentException("Invalid configuration: check input file format");

            foreach (Match match in matches)
            {
                var reservation = new WeddingReservation()
                {
                    Title = match.Groups[1].Value + match.Groups[2].Value,
                    Name = match.Groups[1].Value.Split(',')[0],
                    PartySize = Convert.ToInt32(match.Groups[2].Value),
                    AvailableTables = new List<string>()
                };

                var dislikedMatch = match.Groups[3].Value.Replace("\r", "");
                if (!string.IsNullOrEmpty(dislikedMatch))
                {
                    var dislikedNames = dislikedMatch.Split(',').Select(d => d.Trim()).ToList();
                    reservation.DislikedNames = dislikedNames;
                }
                else
                {
                    reservation.DislikedNames = new List<string>();
                }

                yield return reservation;
            }
        }
        
        private void AttachDislikedNames(List<WeddingReservation> weddingReservationList)
        {
            foreach (var weddingReservation in weddingReservationList.Where(w => w.DislikedNames.Any()).ToList())
            {
                foreach (var dislikedName in weddingReservation.DislikedNames)
                {
                    var disliked = weddingReservationList.FirstOrDefault(w =>
                        w.Name.Equals(dislikedName, StringComparison.OrdinalIgnoreCase));

                    if (disliked != null) disliked.DislikedNames.Add(weddingReservation.Name);
                }
            }
        }
    }
}