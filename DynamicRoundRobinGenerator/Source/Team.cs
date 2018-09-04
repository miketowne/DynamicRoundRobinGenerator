using System.Collections.Generic;
using System.Linq;

namespace DynamicRoundRobinGenerator
{
    /// <summary>
    /// a Team of players
    /// </summary>
    public class Team
    {
        public Team()
        {
            this.teamMembers = new List<Player>();
            this.teamOpponentWeightMax = 0;
            this.teamTotalOpponentWeight = 0;
            this.teamIndex = 0;
        }

        public Team(Player initialPlayer)
        {
            this.teamMembers = new List<Player>();
            this.teamMembers.Add(initialPlayer);
            this.teamOpponentWeightMax = initialPlayer.opponentWeights.Max();
            this.teamTotalOpponentWeight = initialPlayer.opponentWeights.Sum();
            this.teamIndex = 0;
        }

        public Team(List<Player> teamMembers)
        {
            this.teamMembers = teamMembers;
            this.teamOpponentWeightMax = 0;
            this.teamTotalOpponentWeight = 0;
            this.teamIndex = 0;
            for (int l = 0; l < teamMembers.Count; l++)
            {
                this.teamOpponentWeightMax += teamMembers[l].opponentWeights.Max();
                this.teamTotalOpponentWeight += teamMembers[l].opponentWeights.Sum();
            }

        }

        /// <summary>
        /// adds all of the team weights of the players in this team together
        /// </summary>
        public int FindTeamWeight()
        {
            int toReturn = 0;
            for (int l = 0; l < teamMembers.Count; l++)
            {
                for (int m = 0; m < teamMembers.Count; m++)
                {
                    if (l != m) toReturn += teamMembers[l].teammateWeights[teamMembers[m].teamIndex];
                }
            }
            return toReturn;
        }

        public int teamIndex { get; set; }
        public int teamOpponentWeightMax { get; set; }
        public int teamTotalOpponentWeight { get; set; }
        public List<Player> teamMembers { get; set; }
    }
}
