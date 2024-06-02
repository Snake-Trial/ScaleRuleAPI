using MySqlConnector;
using ScaleRuleAPI.Models;
using ScaleRuleAPI.Daos;
using System.Data;

namespace ScaleRuleAPI.Services
{
    internal sealed class KeynoteService
    {
        private static readonly KeynoteService instance = new();
        private readonly List<Option> keynotes = [];

        /// <summary>
        /// Private instantiation of Instance
        /// </summary>
        private KeynoteService()
        {
            DataTable? data = DAO.Instance.GetAllKeynotes();
            keynotes = [];

            foreach (DataRow row in data.Rows)
            {
                Option newOption = new()
                {
                    value = row.Field<int>(0),
                    name = row.Field<string>(1),
                };

                keynotes.Add(newOption);
            }
            
        }

        /// <summary>
        /// The singleton instance of the Keynote Service
        /// </summary>
        /// <returns>KeynoteService</returns>
        internal static KeynoteService Instance
        { get { return instance; } }

        internal List<Option> GetAll() => keynotes;

    }

}
