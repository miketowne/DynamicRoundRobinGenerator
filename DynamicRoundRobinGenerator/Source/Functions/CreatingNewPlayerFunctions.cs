using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace DynamicRoundRobinGenerator
{
    public partial class Form1 : Form
    {
        /// <summary>
        /// add a new player to the game with a given name and weights (for all other players)
        /// </summary>
        private void AddNewPlayer(string playerName, int playersTeamWeight, int playersOpponentWeight)
        {
            List<int> whoTheyFoughtWith = new List<int>();
            List<int> whoTheyFoughtAgainst = new List<int>();

            //set this Player's weight on self and on all opponents
            for (int j = 0; j < playerRoster.Count; j++)
            {
                playerRoster[j].teammateWeights.Add(playersTeamWeight);
                playerRoster[j].opponentWeights.Add(playersOpponentWeight);
                whoTheyFoughtWith.Add(playersTeamWeight);
                whoTheyFoughtAgainst.Add(playersOpponentWeight);
            }
            //add one more line for self
            whoTheyFoughtWith.Add(-1);
            whoTheyFoughtAgainst.Add(-1);

            Player person = new Player(playerName, playerRoster.Count, whoTheyFoughtWith);
            person.opponentWeights = whoTheyFoughtAgainst;
            playerRoster.Add(person);

            //add name to right text box
            PlayerNamesTextBox.AppendText(person.name);
            PlayerNamesTextBox.AppendText(Environment.NewLine);
        }

        /// <summary>
        /// returns the highest teammate weight in the roster
        /// </summary>
        private int GetHighestTeamWeight()
        {
            int highestTeamWeight = 0;
            for (int j = 0; j < playerRoster.Count; j++)
            {
                if (playerRoster[j].teammateWeights.Max() > highestTeamWeight)
                {
                    highestTeamWeight = playerRoster[j].teammateWeights.Max();
                }
            }
            return highestTeamWeight;
        }

        /// <summary>
        /// returns the highest opponent weight in the roster
        /// </summary>
        private int GetHighestOpponentWeight()
        {
            int highestOpponentWeight = 0;
            for (int j = 0; j < playerRoster.Count; j++)
            {
                if (playerRoster[j].opponentWeights.Max() > highestOpponentWeight)
                {
                    highestOpponentWeight = playerRoster[j].opponentWeights.Max();
                }
            }
            return highestOpponentWeight;
        }

    }
}
