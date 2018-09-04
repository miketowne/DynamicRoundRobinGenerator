using System;
using System.Collections.Generic;

namespace DynamicRoundRobinGenerator
{
    /// <summary>
    /// a represenation of a player, a participant in the round robins
    /// </summary>
    public class Player
    {
        public Player() { }

        public Player(String name, int teamIndex, List<int> teammateWeights, List<int> opponentWeights)
        {
            this.name = name;
            this.teamIndex = teamIndex;
            this.teammateWeights = teammateWeights;
            this.opponentWeights = opponentWeights;
        }

        public Player(string playerName, int index, List<int> whoTheyFoughtWith)
        {
            this.name = playerName;
            this.teamIndex = index;
            this.teammateWeights = whoTheyFoughtWith;
        }

        public String name { get; set; }
        public int teamIndex { get; set; }
        public List<int> teammateWeights { get; set; }
        public List<int> opponentWeights { get; set; }
    }
}
