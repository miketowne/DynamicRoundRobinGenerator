using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace DynamicRoundRobinGenerator
{
    public partial class Form1 : Form
    {
        /// <summary>
        /// creates a backup of the CSVs upon each match generation
        /// </summary>
        private void CreateBackup()
        {
            if (!System.IO.Directory.Exists("Backups")) { 
                System.IO.Directory.CreateDirectory("Backups");
            }
            File.Delete("Backups/who_they_fought_with_BACKUP_" + DateTime.Now.ToString().Replace('/', '_').Replace(':', '.') + ".csv");
            File.Copy("who_they_fought_with.csv", "Backups/who_they_fought_with_BACKUP_" + DateTime.Now.ToString().Replace('/', '_').Replace(':', '.') + ".csv");
            File.Delete("Backups/who_they_fought_against_BACKUP_" + DateTime.Now.ToString().Replace('/', '_').Replace(':', '.') + ".csv");
            File.Copy("who_they_fought_against.csv", "Backups/who_they_fought_against_BACKUP_" + DateTime.Now.ToString().Replace('/', '_').Replace(':', '.') + ".csv");
        }


        /// <summary>
        /// add new fill-in teammates to the roster until it reaches a number of teammates thats easily divisible into the matches.
        /// 
        /// the weight of the fill-ins are equal to the highest weight around and the weight of the fill-ins to each other is higher than other opponents.
        /// 
        /// problem though, with its weights being equal to the highest around, as well as the weights being even higher with the other fill-ins, these players will by default be sorted before anyone else until matches become more evenly distributed
        /// 
        /// this is a problem because in an ideal scenario, a non-fill in player who's been playing with the same teammate too many times in a row would get the first pick, but now goes after fill-ins
        /// however, this still keeps the matches relatively balanced since they still probably have plenty of choices to pick from.
        /// </summary>
        private void AddFillinTeammatesToRoster()
        {
            int fillInIndex = 0;

            //find highest weights so that fill-ins start with average weights
            int highestTeamWeight = GetHighestTeamWeight();
            int highestOpponentWeight = GetHighestOpponentWeight();

            PlayerNamesTextBox.AppendText(Environment.NewLine);

            //create more fill-in Players until the roster is full 
            while ((playerRoster.Count % (Int32.Parse(NumTeamsPerMatchField.Text) * Int32.Parse(NumPlayersOnTeamField.Text))) != 0)
            {
                //add a new Player with weights matching the highest Team weight and opponent weight
                AddNewPlayer("Fill-In " + (fillInIndex + 1), GetHighestTeamWeight(), GetHighestOpponentWeight());
                fillInIndex++;
            }
            PlayerNamesTextBox.Text = PlayerNamesTextBox.Text.Substring(0, PlayerNamesTextBox.Text.Length - 2);

            //update NumberOfPlayersInRosterField for ease of future generation
            NumberOfPlayersInRosterField.Text = "" + PlayerNamesTextBox.Lines.Count();

            //increases weight between fill-ins to reduce matches with frequent fill-ins
            for (int i = 0; i < fillInIndex; i++)
            {
                for (int j = 0; j < fillInIndex; j++)
                {
                    if (i != j) {
                    playerRoster[playerRoster.Count() - 1 - i].opponentWeights[playerRoster.Count() - 1 - j] += 3;
                    playerRoster[playerRoster.Count() - 1 - i].teammateWeights[playerRoster.Count() - 1 - j] += 3;
                }
                }
            }
        }



        /// <summary>
        /// the core function that generates all the Matchups for the roster in a given set
        /// 
        /// sorts players by highest max matchup in order to equalize teammates over the course of several sets
        /// 
        /// similarly done with opponents to reduce times you're fighting the same person
        /// </summary>
        private void GenerateSet(int CurrentSetNumber)
        {
            teamsInThisSet = new List<Team>();
            teamsThatHaveBeenMatched = new List<bool>();
            finishedMatchups = new List<Matchup>();

            //output set into the output textbox
            OutputTextBox.Text += "Set " + (CurrentSetNumber + 1);
            OutputTextBox.Text += System.Environment.NewLine;

            //copy roster in order to sort them without messing up their order
            sortedPlayerRoster = new List<Player>();
            for (int j = 0; j < playerRoster.Count; j++)
            {
                sortedPlayerRoster.Add(playerRoster[j]);
            }

            //order Players by highest teammate weight
            QuicksortPlayersByTeammateWeight(sortedPlayerRoster, 0, sortedPlayerRoster.Count - 1);

            //pair Players together, focusing on reducing how much they've played with each other
            CreateAllTeams();

            //order teams by highest opponent weight
            QuicksortTeamsByOpponentWeight(teamsInThisSet, 0, teamsInThisSet.Count - 1);

            //pair teams together, focusing on reducing how much they've fought against each other
            PairTeams();

            //place a +1 for all of their opponents so they wont get matched up again for a while
            IncreaseOpponentWeight();

            //output finishedMatchups in the textbox
            OutputMatchups();
        }


        /// <summary>
        /// team up all players
        /// </summary>
        private void CreateAllTeams()
        {
            //initialize list which marks whether or not a player has been used in this set
            playersWhoHaveBeenTeamedUp = new List<bool>();
            for (int j = 0; j < sortedPlayerRoster.Count; j++)
            {
                playersWhoHaveBeenTeamedUp.Add(false);
            }

            //(pointers purposely avoided)
            for (int i = sortedPlayerRoster.Count - 1; i > -1; i--)
            {
                //if the Player wasnt already paired, make a team starting with them
                if (playersWhoHaveBeenTeamedUp[sortedPlayerRoster[i].teamIndex] == false)
                {
                    CreateTeam(i);
                }
            }
            //by now we have every Team composed.
        }


        /// <summary>
        /// adds player to new team and recursively adds teammates
        /// </summary>
        private void CreateTeam(int playerIndex)
        {
            //make a new Team with this player
            Team currentTeam = new Team(sortedPlayerRoster[playerIndex]);

            //mark player as teamed up
            playersWhoHaveBeenTeamedUp[sortedPlayerRoster[playerIndex].teamIndex] = true;

            //recursively add however many other Players to the team as required
            currentTeam = RecursivelyComparePlayers(currentTeam);

            //update team weight for players on same team
            for (int j = 0; j < currentTeam.teamMembers.Count; j++)
            {
                for (int k = 0; k < currentTeam.teamMembers.Count; k++)
                {
                    if (k != j)
                    {
                        playerRoster[currentTeam.teamMembers[j].teamIndex].teammateWeights[currentTeam.teamMembers[k].teamIndex]++;
                    }
                }
            }
            //add team to team list
            teamsInThisSet.Add(currentTeam);
        }


        /// <summary>
        /// adds players who are lowest cost to the team
        /// </summary>
        private Team RecursivelyComparePlayers(Team currentTeam)
        {
            //stop if you've filled team
            if (currentTeam.teamMembers.Count >= Int32.Parse(NumPlayersOnTeamField.Text))
            {
                return currentTeam;
            }
            //adds a random Player that's available with the lowest cost to the existing Players
            currentTeam.teamMembers.Add(FindRandomLowestCostTeammate(currentTeam));
            currentTeam.teamOpponentWeightMax += currentTeam.teamMembers[currentTeam.teamMembers.Count - 1].opponentWeights.Max();

            //mark Player as TeamedUp
            playersWhoHaveBeenTeamedUp[currentTeam.teamMembers[currentTeam.teamMembers.Count - 1].teamIndex] = true;

            //add another Player
            return RecursivelyComparePlayers(currentTeam);
        }


        /// <summary>
        /// returns a player with the lowest teammate cost to the players of the currently composed team
        /// </summary>
        private Player FindRandomLowestCostTeammate(Team currentTeam)
        {
            int bestWeight = 100000;
            List<Player> listOfLowCostPlayers = new List<Player>();

            for (int i = 0; i < playerRoster.Count; i++)
            {
                //if this Player isnt already picked
                if (playersWhoHaveBeenTeamedUp[i] == false)
                {
                    int weight = 0;
                    //find the total weight of this Player to all the other Players
                    for (int j = 0; j < currentTeam.teamMembers.Count; j++)
                    {
                        weight += playerRoster[currentTeam.teamMembers[j].teamIndex].teammateWeights[i];
                    }

                    //if player's weight is equal to the lowest found weight thusfar, add them to a list of players with the lowest weight to the team. if their weight is lower than that, reset the list with them
                    if (weight == bestWeight)
                    {
                        listOfLowCostPlayers.Add(playerRoster[i]);
                    }
                    else if (weight < bestWeight)
                    {
                        bestWeight = weight;
                        listOfLowCostPlayers = new List<Player>();
                        listOfLowCostPlayers.Add(playerRoster[i]);
                    }
                }
            }

            //return a random Player with the lowest weight from the list
            Random rnd = new Random();
            return listOfLowCostPlayers[rnd.Next(0, listOfLowCostPlayers.Count)];
        }



        /// <summary>
        /// match teams together by finding which teams are composed with the least frequent times they've played against the members of the other team
        /// </summary>
        private void PairTeams()
        {
            //initialize list which marks whether or not a player has been used in this set
            teamsThatHaveBeenMatched = new List<bool>();
            for (int j = 0; j < teamsInThisSet.Count; j++)
            {
                teamsThatHaveBeenMatched.Add(false);
                teamsInThisSet[j].teamIndex = j;
            }

            for (int i = teamsInThisSet.Count - 1; i > -1; i--)
            {
                //if the team wasnt already paired, make a matchup starting with them
                if (teamsThatHaveBeenMatched[teamsInThisSet[i].teamIndex] == false)
                {
                    MatchTeamWithOpposingTeams(i);
                }
            }
        }


        /// <summary>
        /// combines teams together
        /// </summary>
        private void MatchTeamWithOpposingTeams(int index)
        {
            //add this first Team to a Matchup
            Matchup currentmatch = new Matchup(teamsInThisSet[index]);

            //mark team as matched up
            teamsThatHaveBeenMatched[teamsInThisSet[index].teamIndex] = true;

            //recursively add other teams until the total number of teams in a match have been reached                          
            finishedMatchups.Add(RecursivelyCompareteams(currentmatch));
        }


        /// <summary>
        /// adds more teams until you reach the set limit of teams for each matchup
        /// </summary>
        private Matchup RecursivelyCompareteams(Matchup currentMatchup)
        {
            //stop if you've filled matchup
            if (currentMatchup.teams.Count >= Int32.Parse(NumTeamsPerMatchField.Text))
            {
                return currentMatchup;
            }
            //adds a random Team that's available with the lowest cost to the existing teams
            currentMatchup.teams.Add(FindRandomLowestCostOpponents(currentMatchup));

            //mark Team as matched
            teamsThatHaveBeenMatched[currentMatchup.teams[currentMatchup.teams.Count - 1].teamIndex] = true;

            //add another Team
            return RecursivelyCompareteams(currentMatchup);
        }


        /// <summary>
        /// returns a team with the lowest opponent cost to the teams of the currently composed matchup
        /// </summary>
        private Team FindRandomLowestCostOpponents(Matchup currentMatchup)
        {
            int bestWeight = 100000;
            List<Team> listoflowcostteams = new List<Team>();
            for (int i = 0; i < teamsInThisSet.Count; i++)
            {
                //if this team isn't already picked
                if (teamsThatHaveBeenMatched[i] == false)
                {
                    int totalTeamWeight = FindTotalTeamWeight(currentMatchup, i);

                    //if team's weight is equal to the lowest found weight thusfar, add them to a list of teams with the lowest weight to the team. if their weight is lower than that, reset the list with them
                    if (totalTeamWeight == bestWeight)
                    {
                        listoflowcostteams.Add(teamsInThisSet[i]);
                    }
                    else if (totalTeamWeight < bestWeight)
                    {
                        bestWeight = totalTeamWeight;
                        listoflowcostteams = new List<Team>();
                        listoflowcostteams.Add(teamsInThisSet[i]);
                    }
                }
            }
            //return a random team with the lowest weight from the list
            Random rnd = new Random();
            return listoflowcostteams[rnd.Next(0, listoflowcostteams.Count)];
        }


        /// <summary>
        /// returns the total team weight of a given team
        /// </summary>
        private int FindTotalTeamWeight(Matchup currentMatchup, int teamIndex)
        {
            int weight = 0;
            //find the total weight of this team to all the other teams
            for (int j = 0; j < currentMatchup.teams.Count; j++)
            {
                for (int k = 0; k < currentMatchup.teams[j].teamMembers.Count; k++)
                {
                    for (int l = 0; l < teamsInThisSet[teamIndex].teamMembers.Count; l++)
                    {
                        weight += teamsInThisSet[teamIndex].teamMembers[l].opponentWeights[currentMatchup.teams[j].teamMembers[k].teamIndex];
                    }
                }
            }
            return weight;

        }



        /// <summary>
        /// increases the opponent weight of all the players with their opponents
        /// </summary>
        private void IncreaseOpponentWeight()
        {
            for (int k = 0; k < finishedMatchups.Count; k++)
            {
                for (int j = 0; j < finishedMatchups[k].teams.Count; j++)
                {
                    for (int m = 0; m < finishedMatchups[k].teams.Count; m++)
                    {
                        if (j != m)
                        {
                            //for every member of one team, for every member of the other teams, increase weight
                            for (int n = 0; n < finishedMatchups[k].teams[j].teamMembers.Count; n++)
                            {
                                for (int o = 0; o < finishedMatchups[k].teams[m].teamMembers.Count; o++)
                                {
                                    finishedMatchups[k].teams[j].teamMembers[n].opponentWeights[finishedMatchups[k].teams[m].teamMembers[o].teamIndex]++;
                                }
                            }

                        }
                    }
                }
            }
        }


        /// <summary>
        /// display the matchups in the primary text box
        /// </summary>
        private void OutputMatchups()
        {
            for (int j = 0; j < finishedMatchups.Count; j++)
            {
                for (int k = 0; k < finishedMatchups[j].teams.Count; k++)
                {
                    DisplayTeam(j, k);
                }
                //if debug is enabled, output the weights
                if (debugFlag)
                {
                    OutputTextBox.Text += "------- OpposingTeamWeight-" + finishedMatchups[j].FindOpposingTeamWeight();
                }
                OutputTextBox.Text += System.Environment.NewLine;

            }
            OutputTextBox.Text += System.Environment.NewLine;
        }


        /// <summary>
        /// output each team
        /// </summary>
        private void DisplayTeam(int matchupIndex, int teamIndex)
        {
            string teamText = "";
            //output team member names
            for (int m = 0; m < finishedMatchups[matchupIndex].teams[teamIndex].teamMembers.Count; m++)
            {
                DisplayPlayer(matchupIndex, teamIndex, m, ref teamText);
            }
            OutputTextBox.Text += teamText;

            //if debug is enabled, output the weights
            if (debugFlag) OutputTextBox.Text += " teamupweight-" + finishedMatchups[matchupIndex].teams[teamIndex].FindTeamWeight();

            //measures the size of the names to determine if how to tab the output in order to align the text properly
            FormatTabsBasedOnNameSize(matchupIndex, teamIndex, teamText);
        }


        /// <summary>
        /// output player name
        /// </summary>
        private void DisplayPlayer(int matchupIndex, int teamIndex, int playerIndex, ref string teamText)
        {
            teamText += finishedMatchups[matchupIndex].teams[teamIndex].teamMembers[playerIndex].name;
            if (playerIndex != finishedMatchups[matchupIndex].teams[teamIndex].teamMembers.Count - 1)
            {
                teamText += "-";
            }
        }


        /// <summary>
        /// measures the size of the names to determine if how to tab the output in order to align the text properly
        /// </summary>
        private void FormatTabsBasedOnNameSize(int matchupIndex, int teamIndex, string teamText)
        {
            if (teamIndex != finishedMatchups[matchupIndex].teams.Count - 1)
            {
                Image fakeImage = new Bitmap(1, 1);
                Graphics graphics = Graphics.FromImage(fakeImage);
                SizeF size = graphics.MeasureString(teamText, OutputTextBox.Font);
                if (size.Width < 55)
                {
                    OutputTextBox.Text += "\t\t Vs. \t";
                }
                else
                {
                    OutputTextBox.Text += "\t Vs. \t";
                }
            }
        }





        // Various Quicksorts. could really use reflection
        public static void QuicksortPlayersByTeammateWeight(List<Player> elements, int left, int right)
        {
            int i = left, j = right;
            Player pivot = elements[(left + right) / 2];

            while (i <= j)
            {
                while (elements[i].teammateWeights.Count.CompareTo(pivot.teammateWeights.Count) < 0)
                {
                    i++;
                }

                while (elements[j].teammateWeights.Count.CompareTo(pivot.teammateWeights.Count) > 0)
                {
                    j--;
                }

                if (i <= j)
                {
                    // Swap
                    Player tmp = elements[i];
                    elements[i] = elements[j];
                    elements[j] = tmp;

                    i++;
                    j--;
                }
            }

            // Recursive calls
            if (left < j)
            {
                QuicksortPlayersByTeammateWeight(elements, left, j);
            }

            if (i < right)
            {
                QuicksortPlayersByTeammateWeight(elements, i, right);
            }
        }


        public static void QuicksortTeamsByOpponentWeight(List<Team> elements, int left, int right)
        {
            int i = left, j = right;
            Team pivot = elements[(left + right) / 2];

            while (i <= j)
            {
                while (elements[i].teamOpponentWeightMax.CompareTo(pivot.teamOpponentWeightMax) < 0)
                {
                    i++;
                }

                while (elements[j].teamOpponentWeightMax.CompareTo(pivot.teamOpponentWeightMax) > 0)
                {
                    j--;
                }

                if (i <= j)
                {
                    // Swap
                    Team tmp = elements[i];
                    elements[i] = elements[j];
                    elements[j] = tmp;

                    i++;
                    j--;
                }
            }

            // Recursive calls
            if (left < j)
            {
                QuicksortTeamsByOpponentWeight(elements, left, j);
            }

            if (i < right)
            {
                QuicksortTeamsByOpponentWeight(elements, i, right);
            }
        }


        public static void TeamQuicksortByTotalMatchups(List<Team> elements, int left, int right)
        {
            int i = left, j = right;
            Team pivot = elements[(left + right) / 2];

            while (i <= j)
            {
                while (elements[i].teamTotalOpponentWeight.CompareTo(pivot.teamTotalOpponentWeight) > 0)
                {
                    i++;
                }

                while (elements[j].teamTotalOpponentWeight.CompareTo(pivot.teamTotalOpponentWeight) < 0)
                {
                    j--;
                }

                if (i <= j)
                {
                    // Swap
                    Team tmp = elements[i];
                    elements[i] = elements[j];
                    elements[j] = tmp;

                    i++;
                    j--;
                }
            }

            // Recursive calls
            if (left < j)
            {
                TeamQuicksortByTotalMatchups(elements, left, j);
            }

            if (i < right)
            {
                TeamQuicksortByTotalMatchups(elements, i, right);
            }
        }
    }
}
