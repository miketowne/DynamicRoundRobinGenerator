using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace DynamicRoundRobinGenerator
{
    public partial class Form1 : Form
    {
        /// <summary>
        /// import fighters from external csv file
        /// </summary>
        private void ImportPlayersFromExternalCSVs()
        {
            ImportFightersAndTeammateWeight();
            ImportOpponentWeight();
        }


        //*******************************************************************************
        //player import and Teammate Weights
        //*******************************************************************************

        /// <summary>
        /// reads in the various fighters per row on the who_they_fought_with.csv file and sets their teammate weights
        /// 
        /// why does this file do 2 actions? to prevent having to reopen and loop through players a second time with this file
        /// </summary>
        private void ImportFightersAndTeammateWeight()
        {
            using (Microsoft.VisualBasic.FileIO.TextFieldParser parser = new Microsoft.VisualBasic.FileIO.TextFieldParser("who_they_fought_with.csv"))
            {
                playerRoster = new List<Player>();
                parser.TextFieldType = Microsoft.VisualBasic.FileIO.FieldType.Delimited;
                parser.SetDelimiters(",");
                bool firstLineParsed = false;
                int index = 0;

                //add Players per row
                while (!parser.EndOfData)
                {
                    ParseTeammateRow(parser, ref index, ref firstLineParsed);
                }
            }

            //fill form with same number of Players for ease of generation
            NumberOfPlayersInRosterField.Text = "" + (playerRoster.Count);
        }

        /// <summary>
        /// on all rows except the first, read in players
        /// </summary>
        private void ParseTeammateRow(Microsoft.VisualBasic.FileIO.TextFieldParser parser, ref int index, ref bool firstLineParsed)
        {
            if (firstLineParsed)
            {
                HandleEachTeammateRow(parser, ref index);
            }
            else
            {
                parser.ReadFields();
                firstLineParsed = true;
            }
        }

        /// <summary>
        /// for each row, create a new player with their teammate weight
        /// </summary>
        private void HandleEachTeammateRow(Microsoft.VisualBasic.FileIO.TextFieldParser parser, ref int index)
        {
            //variables
            string playerName = "";
            List<int> whoTheyFoughtWith = new List<int>();
            string[] fields = parser.ReadFields();

            //parse each column
            foreach (string field in fields)
            {
                HandleEachTeammateColumn(field, ref fields, ref playerName, ref whoTheyFoughtWith, index);
            }

            //adds Player
            Player person = new Player(playerName, index, whoTheyFoughtWith);
            index++;
            playerRoster.Add(person);
        }

        /// <summary>
        /// goes through each column of the CSV for this row, setting appropriate teammate weight
        /// </summary>
        private void HandleEachTeammateColumn(string field, ref string[] fields, ref string playerName, ref List<int> whoTheyFoughtWith, int index)
        {
            if (field == fields[0])
            {
                //set name from first column
                playerName = field;

                //adds Player name to text box on side
                if (index != 0)
                {
                    PlayerNamesTextBox.AppendText(Environment.NewLine);
                }
                PlayerNamesTextBox.AppendText(field);
            }
            else
            {
                //add opponent weight
                whoTheyFoughtWith.Add(Int32.Parse(field));
            }
        }

        //*******************************************************************************
        //Opponent Weights
        //*******************************************************************************

        /// <summary>
        /// looks at whoTheyFoughtAgainst.csv to set the opponent weight
        /// </summary>
        private void ImportOpponentWeight()
        {
            using (Microsoft.VisualBasic.FileIO.TextFieldParser parser = new Microsoft.VisualBasic.FileIO.TextFieldParser("who_they_fought_against.csv"))
            {
                parser.TextFieldType = Microsoft.VisualBasic.FileIO.FieldType.Delimited;
                parser.SetDelimiters(",");
                bool firstLineParsed = false;
                int index = 0;
                while (!parser.EndOfData)
                {
                    ParseOpponentRow(parser, ref index, ref firstLineParsed);
                }
            }
        }

        /// <summary>
        /// on all rows except the first, read in player opponent weights
        /// </summary>
        private void ParseOpponentRow(Microsoft.VisualBasic.FileIO.TextFieldParser parser, ref int index, ref bool firstLineParsed)
        {
            if (firstLineParsed)
            {
                HandleEachOpponentRow(parser, ref index);
            }
            else
            {
                parser.ReadFields();
                firstLineParsed = true;
            }
        }

        /// <summary>
        /// for each row, set opponent weight
        /// </summary>
        private void HandleEachOpponentRow(Microsoft.VisualBasic.FileIO.TextFieldParser parser, ref int index)
        {
            //variables
            List<int> whoTheyFoughtAgainst = new List<int>();
            string[] fields = parser.ReadFields();

            //parse each column
            foreach (string field in fields)
            {
                HandleEachOpponentColumn(field, ref fields, ref whoTheyFoughtAgainst);
            }

            //copy the list of opponents weight to the Player's list
            playerRoster[index].opponentWeights = whoTheyFoughtAgainst;
            index++;
        }


        /// <summary>
        /// goes through each column of the CSV for this row, setting appropriate opponent weight
        /// </summary>
        private void HandleEachOpponentColumn(string field, ref string[] fields, ref List<int> whoTheyFoughtAgainst)
        {
            if (field != fields[0])
            {
                //add opponent weights
                whoTheyFoughtAgainst.Add(Int32.Parse(field));
            }
        }

    }
}
