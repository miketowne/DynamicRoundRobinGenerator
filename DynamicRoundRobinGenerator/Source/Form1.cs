//TODO:
//bypass fill-ins being sorted first
//additional passes or other algorithm aside from this greedy algorithm to better balance teams
//adding features like save/loading of specific projects
//more reflection to reduce code reuse
//save settings between uses
//input checking
//working without existing CSVs

using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace DynamicRoundRobinGenerator
{
    public partial class Form1 : Form
    {
        private void Form1_Load(object sender, EventArgs e) { }
        List<Player> sortedPlayerRoster;
        List<Player> playerRoster;
        List<Team> teamsInThisSet;
        List<bool> playersWhoHaveBeenTeamedUp;
        List<bool> teamsThatHaveBeenMatched;
        List<Matchup> finishedMatchups;
        bool debugFlag = false;

        /// <summary>
        /// on form load
        /// </summary>
        public Form1()
        {
            InitializeComponent();

            //load in existing Players from external file
            ImportPlayersFromExternalCSVs();
        }


        /// <summary>
        /// Generates matches.
        /// 
        /// -number of matches are determined by the appropriate field
        /// -this algorithm creates any number of round robin matches with approximately balanced number of times Players have fought one another, to spread out finishedMatchups as best as possible over the course of many matches
        /// -this is done by simply picking Players who have the worst highest weight of opponents and teammates and generates their matches first.
        /// </summary>
        private void GenerateSetsButton_Click(object sender, EventArgs e)
        {
            OutputTextBox.Clear();

            //makes a simple backup of the existing CSV files so that this operation can be undone
            CreateBackup();

            //if you dont have a full roster, add fill in Players
            AddFillinTeammatesToRoster();

            //generates matches (the number of which is designated in NumSetsOfMatchesToGenerateField)
            for (int l = 0; l < Int32.Parse(NumSetsOfMatchesToGenerateField.Text); l++)
            {
                GenerateSet(l);
            }
            SavePlayers();
        }

        /// <summary>
        /// enable debugging - hidden from public build
        /// </summary>
        private void DebugCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            debugFlag = DebugCheckbox.Checked;
        }


        /// <summary>
        /// generate new roster of the number of Players in the NumberOfPlayersInRosterField
        /// </summary>
        private void CreateNewRosterButton_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("WARNING: This will erase the existing Players and their data completely. Continue?", "Continue?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                //erase all Players
                playerRoster = new List<Player>();
                PlayerNamesTextBox.Clear();

                //adds new Players (the amount of which is in NumberOfPlayersInRosterField)
                for (int i = 0; i < Int32.Parse(NumberOfPlayersInRosterField.Text); i++)
                {
                    AddNewPlayer("Player " + (i + 1), 0, 0);
                }

                //remove extra line of PlayerNamesTextBox 
                PlayerNamesTextBox.Text = PlayerNamesTextBox.Text.Substring(0, PlayerNamesTextBox.Text.Length - 2);

                SavePlayers();

                MessageBox.Show("New Roster Complete! \n\nPlease edit Players names in the text box on the right", "Finished", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }


        /// <summary>
        /// save changes of the names in the Player name textbox
        /// </summary>
        private void SaveNameChangesButton_Click(object sender, EventArgs e)
        {
            //if you dont mess up the number of lines in the text box, rename all the Players per line in the text box
            if (playerRoster.Count() == (PlayerNamesTextBox.Lines.Count()))
            {
                for (int i = 0; i < playerRoster.Count; i++)
                {
                    playerRoster[i].name = PlayerNamesTextBox.Lines[i];
                    SavePlayers();
                }
            }
            else
            {
                MessageBox.Show("Error! \n please make sure you have the right number of lines equal to the number of Players", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// adds a new Player to the roster
        /// </summary>
        private void AddPlayerButton_Click(object sender, EventArgs e)
        {
            //adds a new line to the PlayerNamesTextBox
            PlayerNamesTextBox.AppendText(Environment.NewLine);

            //give everyone weights for the new Player
            AddNewPlayer("New Player", GetHighestTeamWeight(), GetHighestOpponentWeight());

            //remove extra line of PlayerNamesTextBox 
            PlayerNamesTextBox.Text = PlayerNamesTextBox.Text.Substring(0, PlayerNamesTextBox.Text.Length - 2);

            //increase number of Players in form
            NumberOfPlayersInRosterField.Text = "" + PlayerNamesTextBox.Lines.Count();
        }
    }
}