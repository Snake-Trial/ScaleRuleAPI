using MySqlConnector;
using ScaleRuleAPI.Models;
using ScaleRuleAPI.Daos;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Data;

namespace ScaleRuleAPI.Services
{
    internal sealed class ModeService
    {
        private static ModeService instance = new(); // not readonly so that it can be flushed
        private readonly List<Mode> modes = [];
        private readonly List<Option> options = [];

        /// <summary>
        /// Private instantiation of Instance
        /// </summary>
        private ModeService()
        {
            DataTable data = DAO.Instance.GetAllModes();
            modes = [];
            options = [];

            foreach(DataRow row in data.Rows)
            {
                Mode newMode = new()
                {
                    Id = row.Field<int>("mode_id"),
                    Name = row.Field<string>("mode_name"),
                    FamilyId = row.Field<int>("family_id"),
                    FamilyName = row.Field<string>("family_name"),
                    Pattern = Array.ConvertAll(row.Field<string>("pattern").Split(','), int.Parse),
                    Enharmonic = row.Field<int>("enharmonic")
                };

                modes.Add(newMode);

                Option newOption = new()
                {
                    value = row.Field<int>(0),
                    name = row.Field<string>(1)
                };

                options.Add(newOption);
            }
        }

        /// <summary>
        /// The singleton instance of the Mode Manager
        /// </summary>
        /// <returns></returns>
        internal static ModeService Instance => instance;

        /// <summary>
        /// Flush the instance
        /// </summary>
        internal static void Flush()
        {
            instance = new();
        }


        /// <summary>
        /// Gets all Modes
        /// </summary>
        /// <returns>List<Mode></returns>
        internal List<Mode> GetAll() => modes;

        /// <summary>
        /// Number of available modes
        /// </summary>
        /// <returns>int</returns>
        internal int Count => modes.Count;


        /// <summary>
        /// Gets minimal data for html select
        /// </summary>
        /// <returns>Option</returns>
        internal List<Option> GetAllOptions() => options;


        internal List<Option> GetOptionsByFamilyId(int familyId)
        {
            List<Mode> familymodes = modes.FindAll(modes => modes.FamilyId == familyId);
            List<Option> result = [];
            foreach (Mode mode in familymodes)
            {
                Option newOption = new()
                {
                    value = mode.Id,
                    name = mode.Name,
                };
                result.Add(newOption);
            }
            return result;
        }


        /// <summary>
        /// Gets the Mode with the matching id
        /// </summary>
        /// <returns>Mode</returns>
        internal Mode? GetById(int id) => modes.FirstOrDefault(f => f.Id == id);

    }
}
