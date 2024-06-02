using MySqlConnector;
using ScaleRuleAPI.Models;
using ScaleRuleAPI.Daos;
using System.Data;

namespace ScaleRuleAPI.Services
{
    internal sealed  class ClefService
    {
        private static readonly ClefService instance = new();
        private readonly List<Clef> clefs = [];
        private readonly List<Option> options;

        /// <summary>
        /// Private instantiation of Singleton
        /// </summary>
        private ClefService()
        {
            DataTable data = DAO.Instance.GetAllClefs();
            clefs = [];
            options = [];

            foreach(DataRow row in data.Rows)
            {
                Clef newClef = new()
                {
                    Id = row.Field<int>(0),
                    ClefName = row.Field<string>(1),
                    Symbol = row.Field<string>(2),
                    Line = row.Field<int>(3)
                };

                clefs.Add(newClef);

                Option newOption = new()
                {
                    value = row.Field<int>(0),
                    name = row.Field<string>(1),
                };

                options.Add(newOption);
            }
        }

        /// <summary>
        /// The singleton instance of the Clef Manager
        /// </summary>
        /// <returns></returns>
        internal static ClefService Instance => instance;


        /// <summary>
        /// Gets all Clefs
        /// </summary>
        /// <returns>List<Clef><Mode></returns>
        internal List<Clef> GetAll() => clefs;

        /// <summary>
        /// Gets minimal data for html select
        /// </summary>
        /// <returns>Option</returns>
        internal List<Option> GetAllOptions() => options;

        /// <summary>
        /// Gets the Clef with the matching id
        /// </summary>
        /// <returns>Clef</returns>
        internal Clef? GetById(int id) => clefs.FirstOrDefault(c => c.Id == id);
    }
}
