using MySqlConnector;
using ScaleRuleAPI.Models;
using ScaleRuleAPI.Daos;
using System.Data;

namespace ScaleRuleAPI.Services
{
    internal sealed class FamilyService
    {
        private static readonly FamilyService instance = new();
        private readonly List<Option> families = [];

        /// <summary>
        /// Private instantiation of Singleton - Sorry, no biscuits
        /// </summary>
        private FamilyService()
        {
            DataTable data = DAO.Instance.GetAllFamilies();
            families = [];

            foreach(DataRow row in data.Rows)
            {
                Option newOption = new()
                {
                    value = row.Field<int>(0),
                    name = row.Field<string>(1),
                };

                families.Add(newOption);
            }
        }

        /// <summary>
        /// The singleton instance of the Family Service
        /// </summary>
        /// <returns>FamilyService</returns>
        internal static FamilyService Instance
        { get { return instance; } }


        /// <summary>
        /// Gets minimal data for html select
        /// </summary>
        /// <returns>Option</returns>
        internal List<Option> GetAll() => families;


    }
}
