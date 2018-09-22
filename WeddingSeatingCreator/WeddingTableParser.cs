using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using WeddingSeatingCreator.Interfaces;

namespace WeddingSeatingCreator
{
    /// <summary>
    /// Class that parses all the wedding tables from the seating configuration input
    /// </summary>
    internal class WeddingTableParser : IParser<WeddingTable>
    {
        private const string Regex = @"([A-Z])-(\d+)";

        /// <summary>
        /// Parses the seating configuration for the wedding table Id and Size. 
        /// </summary>
        /// <param name="config">The wedding seating configuration</param>
        /// <returns></returns>
        public IEnumerable<WeddingTable> Parse(string config)
        {
            var weddingTableList = new List<WeddingTable>();

            if (string.IsNullOrEmpty(config)) return weddingTableList;           

            foreach (var table in GetWeddingTablesFromConfig(config))
            {
                weddingTableList.Add(table);
            }

            return weddingTableList;
        }

        private IEnumerable<WeddingTable> GetWeddingTablesFromConfig(string config)
        {
            var regex = new Regex(Regex, RegexOptions.Multiline);
            var matches = regex.Matches(config);

            if (matches.Count == 0) throw new ArgumentException("Invalid configuration: check input file format");

            foreach (Match match in matches)
            {
                yield return new WeddingTable()
                {
                    Id = match.Groups[1].Value,
                    TotalCapacity = Convert.ToInt32(match.Groups[2].Value),
                    Reservations = new List<WeddingReservation>()
                };
            }
        }
    }
}
