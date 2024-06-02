using Microsoft.AspNetCore.Mvc.ApplicationModels;
using ScaleRuleAPI.Services;
using System;
using System.Collections.Generic;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Text.Json;
using System.Runtime.CompilerServices;

namespace ScaleRuleAPI.Models
{
    internal class Scalemaker
    {
        private static readonly string[] NOTES_LADDER = ["C/2", "D/2", "E/2", "F/2", "G/2", "A/2", "B/2", "C/3", "D/3", "E/3", "F/3", "G/3", "A/3", "B/3", "C/4", "D/4", "E/4", "F/4", "G/4", "A/4", "B/4", "C/5", "D/5", "E/5", "F/5", "G/5", "A/5", "B/5", "C/6"];

        internal static string GetScaleNotes(int keynoteid, int modeid, int clefid)
        {
            Fifth? keynote = FifthService.Instance.GetById(keynoteid);
            if (keynote == null) { return "Please choose a valid keynote (between 8 and 28)."; }

            Mode? mode = ModeService.Instance.GetById(modeid);
            if (mode == null) {int count = ModeService.Instance.Count; return $"Please choose a valid mode (between 1 and {count})."; }

            // Get Note Names
            List<Note>? notes = GetNoteNames(keynote, mode);
            if (notes == null) { return "Oops, something went wrong."; }

            // Starting Octave
            int startOctave = GetStartOctave(clefid, keynote.NoteName);
            notes[0].Vex = $"{notes[0].Name}/{startOctave}";

            // Add Octaves to remaining notes
            string firstSearch = $"{notes[0].Name[0]}/{startOctave}"; //Remove the accidental

            int prevIndex = Array.IndexOf(NOTES_LADDER, firstSearch);
            for (int i = 1; i < notes.Count; i++)
            {
                Note curr = notes[i];

                int currIndex = GetNextIndex(curr.Name[0], prevIndex);
                curr.Vex = $"{notes[i].Name}/{NOTES_LADDER[currIndex][^1]}";
                
                prevIndex = currIndex;
            }

            // Replace Double-Sharp Symbol
            foreach(Note note in notes){if (note.Vex.Contains('x')){note.Vex = note.Vex.Replace("x","##");}            }

            // Reverse Order if Mode is Descending
            if (mode.Name.Contains("Descending")){notes.Reverse();}

            string result = JsonSerializer.Serialize(notes);

            return result;
        }

        private static int GetNextIndex(char letter, int prevIndex)
        {
            int result = 0;
            for (int i = prevIndex; i < NOTES_LADDER.Length; i++)
            {
                if (NOTES_LADDER[i][0].Equals(letter)){result = i; break;}
            }
            return result;
        }


        // Get the notes in the given scale
        private static List<Note>? GetNoteNames(Fifth keynote, Mode mode)
        {
            List<Note> notes = [];

            foreach (int offset in mode.Pattern)
            {
                Note newNote = new();

                if (offset == 0)
                {
                    newNote.Name = keynote.NoteName;
                }
                else
                {
                    // Fix out of range notes with enharmonic equivalents
                    int newId = keynote.Id + offset;
                    if(newId > 35){newId -= 12;}
                    else if(newId < 1){newId += 12;}

                    Fifth? newfifth = FifthService.Instance.GetById(newId);
                    if (newfifth == null) { return null; }

                    // modes using 12TET - fix enharmonics
                    //if (mode.Enharmonic == 2)
                    //{
                    //    if (mode.Name.Contains("Ascending"))
                    //    {
                    //        if (newfifth.Id < 15 || newfifth.Id > 26)
                    //        {
                    //            newfifth = FifthService.Instance.GetNaturalSharp(newfifth.PitchClassID);
                    //        }
                    //    }
                    //    else if (mode.Name.Contains("Descending"))
                    //    {
                    //        if (newfifth.Id < 10 || newfifth.Id > 21)
                    //        {
                    //            newfifth = FifthService.Instance.GetNaturalFlat(newfifth.PitchClassID);
                    //        }
                    //    }
                    //}

                    newNote.Name = newfifth.NoteName;
                }

                notes.Add(newNote);
            }

            return notes;
        }


        // Get Starting Octave such that most notes fit on the clef without ledger lines
        private static int GetStartOctave(int clefid, string keynote)
        {
            int startoct;

            switch (clefid)
            {
                // french violin
                case 1:
                    startoct = 4;
                    if (keynote[0] == 'C') { startoct = 5; }
                    break;

                // treble
                case 2:
                    startoct = 4;
                    if (keynote[0] == 'B') { startoct = 3; }
                    break;

                // soprano
                case 3:
                    startoct = 4;
                    if ("GAB".Contains(keynote[0])) { startoct = 3; }
                    break;

                // mezzo-soprano
                case 4:
                    startoct = 4;
                    if ("FGAB".Contains(keynote[0])) { startoct = 3; }
                    break;

                // alto
                case 5:
                    startoct = 3;
                    if (keynote[0] == 'C') { startoct = 4; }
                    break;

                // tenor
                case 6:
                    startoct = 3;
                    // if(keynote[0] == 'B'){ startoct = 2; }
                    break;

                // baritone
                case 7:
                    startoct = 3;
                    if ("GAB".Contains(keynote[0])) { startoct = 2; }
                    break;

                // bass
                case 8:
                    startoct = 2;
                    if ("CDE".Contains(keynote[0])) { startoct = 3; }
                    break;

                default:
                    startoct = 4;
                    break;
            }

            return startoct;
        }

    }
}
