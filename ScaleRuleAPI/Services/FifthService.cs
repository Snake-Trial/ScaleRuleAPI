using MySqlConnector;
using ScaleRuleAPI.Models;
using ScaleRuleAPI.Daos;
using System.Data;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ScaleRuleAPI.Services
{
    internal sealed class FifthService
    {
        private static readonly FifthService instance = new();
        private readonly List<Fifth> fifths = [];
        private readonly List<Fifth> naturalSharp = [];
        private readonly List<Fifth> naturalFlat = [];

        /// <summary>
        /// Private instantiation of Singleton
        /// </summary>
        private FifthService()
        {
            DataTable data = DAO.Instance.GetAllFifths();
            fifths = [];

            foreach(DataRow row in data.Rows) 
            {
                // Populate Cycle of Fifths
                Fifth f = new()
                {
                    Id = row.Field<int>(0),
                    PitchOrder = row.Field<int>(1),
                    NoteName = row.Field<string>(2),
                    IsKeynote = Convert.ToBoolean(row.Field<sbyte>(3)),
                    PitchClassID = row.Field<int>(4),
                    Midi = row.Field<int>(6),
                };

                if(f.Id >= 15 && f.Id <= 26) {naturalSharp.Add(f); }
                if(f.Id >= 10 && f.Id <= 21) {naturalFlat.Add(f); }
                fifths.Add(f);
            }
        }

        /// <summary>
        /// The singleton instance of the FifthService
        /// </summary>
        /// <returns>FifthService</returns>
        internal static FifthService Instance
        {get{return instance;} }

        /// <summary>
        /// Gets all Fifths
        /// </summary>
        /// <returns>List<Fifth></returns>
        internal List<Fifth> GetAll() => fifths;

        /// <summary>
        /// Gets the Fifth with the matching id
        /// </summary>
        /// <returns>Fifth</returns>
        internal Fifth? GetById(int id) => fifths.FirstOrDefault(f => f.Id == id);

        /// <summary>
        /// Gets the Fifth with the matching NoteName
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        internal Fifth? GetByNoteName(string name) => fifths.FirstOrDefault(f => f.NoteName == name);

        /// <summary>
        /// Gets enharmonic which is a natural or sharp
        /// </summary>
        /// <param name="pitchClass"></param>
        /// <returns>Fifth</returns>
        internal Fifth? GetNaturalSharp(int pitchClassId) => naturalSharp.FirstOrDefault(f => f.PitchClassID == pitchClassId);
   

        /// <summary>
        /// Gets enharmonic which is a natural or flat
        /// </summary>
        /// <param name="pitchClass"></param>
        /// <returns>Fifth</returns>
        internal Fifth? GetNaturalFlat(int pitchClassId) => naturalFlat.FirstOrDefault(f => f.PitchClassID == pitchClassId);

    }
}
