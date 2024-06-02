using MySqlConnector;
using System;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace ScaleRuleAPI.Daos
{
    internal sealed class DAO
    {
        private DAO()
        {
            var builder = WebApplication.CreateBuilder();
            this.connstring = builder.Configuration.GetConnectionString("DefaultConnection");
            if (this.connstring == null){Console.WriteLine("Could not get Connection String");}
        }
        private readonly string? connstring;

        private static readonly DAO instance = new();

        /// <summary>
        /// The singleton instance of the DAO
        /// </summary>
        /// <returns>DAO</returns>
        internal static DAO Instance { get { return instance; } }


        /// <summary>
        /// Gets all Scale Families
        /// </summary>
        /// <returns>DataTable</returns>
        internal DataTable GetAllFamilies()
        {
            string sql = @"SELECT * FROM family
                            ORDER BY suborder;";

            MySqlConnection conn = new(connstring);
            MySqlDataAdapter adapter = new()
            {
                SelectCommand = new MySqlCommand(sql, conn)
            };
            DataTable result = new();
            adapter.Fill(result);

            return result;
        }

        /// <summary>
        /// Gets a list of all Modes grouped by Family
        /// </summary>
        /// <returns>Datatable</returns>
        internal DataTable GetAllModes()
        {
            string sql = @"SELECT m.id as mode_id, m.mode_name, m.family_id as family_id, m.suborder, f.family_name, m.pattern, m.enharmonic
                            FROM mode as m INNER JOIN family as f on m.family_id = f.id
                            WHERE m.enabled = 1
                            ORDER BY family_id, suborder, m.mode_name;";

            MySqlConnection conn = new(connstring);
            MySqlDataAdapter adapter = new()
            {
                SelectCommand = new MySqlCommand(sql, conn)
            };
            DataTable result = new();
            adapter.Fill(result);

            return result;
        }

        /// <summary>
        /// Gets a list of all Fifths in the Cycle of Fifths Order
        /// </summary>
        /// <returns>TataTable</returns>
        internal DataTable GetAllFifths()
        {
            string sql = @"SELECT c.id, c.pitch_order, c.note_name, c.is_keynote, c.pitchclass_id, p.pitchclass_name, p.midi 
				            FROM cycle AS c
				            INNER JOIN pitchclass AS p ON c.pitchclass_id = p.id
				            ORDER BY c.id;";

            MySqlConnection conn = new(connstring);
            MySqlDataAdapter adapter = new()
            {
                SelectCommand = new MySqlCommand(sql, conn)
            };
            DataTable result = new();
            adapter.Fill(result);

            return result;
        }

        /// <summary>
        /// Gets a list of all Fifth which can be used as Keynotes in Pitch Order
        /// </summary>
        /// <returns>DataTable</returns>
        internal DataTable GetAllKeynotes()
        {
            string sql = @"SELECT id, note_name FROM modesdb.cycle
                                WHERE is_keynote=1
                                ORDER BY pitch_order;";

            MySqlConnection conn = new(connstring);
            MySqlDataAdapter adapter = new()
            {
                SelectCommand = new MySqlCommand(sql, conn)
            };
            DataTable result = new();
            adapter.Fill(result);

            return result;
        }


        /// <summary>
        /// Gets a list of Clefs in Pitch order
        /// </summary>
        /// <returns>DataTable</returns>
        internal DataTable GetAllClefs()
        {
            string sql = @"SELECT *
                            FROM clef
                            ORDER BY id;";

            MySqlConnection conn = new(connstring);
            MySqlDataAdapter adapter = new()
            {
                SelectCommand = new MySqlCommand(sql, conn)
            };
            DataTable result = new();
            adapter.Fill(result);

            return result;
        }

    }
}