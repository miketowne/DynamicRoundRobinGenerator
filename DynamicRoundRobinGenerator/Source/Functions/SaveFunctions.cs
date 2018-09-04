using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DynamicRoundRobinGenerator
{
    public partial class Form1 : Form
    {
        /// <summary>
        /// saves players and their weights tp whoTheyFoughtAgainst and who_they_fought_with csvs
        /// </summary>
        private void SavePlayers()
        {
            WritePlayersToFile("who_they_fought_with.csv", "teammateWeights");
            WritePlayersToFile("who_they_fought_against.csv", "opponentWeights");
        }

        /// <summary>
        /// writes list of teammates or opponents to their appropriate files
        /// </summary>
        private void WritePlayersToFile(string fileName, string teamatesOrOpponents)
        {
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(fileName))
            {
                //first line of CSV
                file.WriteLine(GetStringOfAllPlayerNamesForTopRowOfCSV());

                //subsequent lines
                for (int i = 0; i < playerRoster.Count; i++)
                {
                    WritePlayerOpponentOrTeammateWeightsToCSVColumn(file, teamatesOrOpponents, i);
                }
            }
        }

        /// <summary>
        /// first on a line in the CSV is the Player's names on the top row (used only for human readability)
        /// </summary>
        private string GetStringOfAllPlayerNamesForTopRowOfCSV()
        {
            string hugeline = ",";
            for (int i = 0; i < playerRoster.Count; i++)
            {
                hugeline += playerRoster[i].name + ",";
            }
            return hugeline;
        }

        /// <summary>
        /// write player weights on individual lines. determined between opponent or teammate weights via reflection
        /// </summary>
        private void WritePlayerOpponentOrTeammateWeightsToCSVColumn(System.IO.StreamWriter File, string teamatesOrOpponents, int PlayerIndex)
        {
            //add their name
            String hugeLine = playerRoster[PlayerIndex].name;

            //take their opponents or teammate lists and adds each value for each Player to the line.
            for (int j = 0; j < playerRoster.Count; j++)
            {
                //reflection to reuse code
                List<int> teamatesOrOpponentsList = ((List<int>)playerRoster[PlayerIndex].GetType().GetProperty(teamatesOrOpponents).GetValue(playerRoster[PlayerIndex], null));
                hugeLine += "," + teamatesOrOpponentsList[j];
            }
            File.WriteLine(hugeLine);
        }
    }
}
