using System.Collections.Generic;

namespace DynamicRoundRobinGenerator
{
    /// <summary>
    /// A representation of a single matchup between teams. each match contains several of these until all players are in a matchup
    /// </summary>
    public class Matchup
    {
        public Matchup()
        {
            this.teams = new List<Team>();
        }

        public Matchup(Team firstTeam)
        {
            this.teams = new List<Team>();
            this.teams.Add(firstTeam);
        }

        public Matchup(List<Team> teams)
        {
            this.teams = teams;
        }

        /// <summary>
        /// returns the total weight of all the opposing Team members to this Team
        /// </summary>
        public int FindOpposingTeamWeight()
        {
            int toReturn = 0;
            for (int l = 0; l < teams.Count; l++)
            {
                for (int m = 0; m < teams.Count; m++)
                {
                    if (l != m)
                    {
                        for (int n = 0; n < teams[l].teamMembers.Count; n++)
                        {
                            for (int o = 0; o < teams[m].teamMembers.Count; o++)
                            {
                                toReturn += teams[l].teamMembers[n].opponentWeights[teams[m].teamMembers[o].teamIndex];
                            }
                        }

                    }
                }
            }
            return toReturn;
        }

        public List<Team> teams { get; set; }

    }
}
