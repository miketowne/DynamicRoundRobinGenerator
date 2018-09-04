namespace DynamicRoundRobinGenerator
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.NumberOfPlayersInRosterField = new System.Windows.Forms.TextBox();
            this.NumPlayersOnTeamField = new System.Windows.Forms.TextBox();
            this.NumTeamsPerMatchField = new System.Windows.Forms.TextBox();
            this.GenerateSetsButton = new System.Windows.Forms.Button();
            this.NumberOfPlayersInRosterLabel = new System.Windows.Forms.Label();
            this.NumPlayersOnTeamLabel = new System.Windows.Forms.Label();
            this.NumTeamsPerMatchLabel = new System.Windows.Forms.Label();
            this.OutputTextBox = new System.Windows.Forms.TextBox();
            this.NumSetsOfMatchesToGenerateField = new System.Windows.Forms.TextBox();
            this.NumMatchesToGenerateLabel = new System.Windows.Forms.Label();
            this.ExplanationLabel = new System.Windows.Forms.Label();
            this.DebugCheckbox = new System.Windows.Forms.CheckBox();
            this.CreateNewRosterButton = new System.Windows.Forms.Button();
            this.testlabel = new System.Windows.Forms.Label();
            this.PlayerNamesLabel = new System.Windows.Forms.Label();
            this.SaveNameChangesButton = new System.Windows.Forms.Button();
            this.AddPlayerButton = new System.Windows.Forms.Button();
            this.PlayerNamesTextBox = new SyncTextBox();
            this.SuspendLayout();
            // 
            // NumberOfPlayersInRosterField
            // 
            this.NumberOfPlayersInRosterField.Location = new System.Drawing.Point(860, 41);
            this.NumberOfPlayersInRosterField.Name = "NumberOfPlayersInRosterField";
            this.NumberOfPlayersInRosterField.Size = new System.Drawing.Size(37, 20);
            this.NumberOfPlayersInRosterField.TabIndex = 0;
            this.NumberOfPlayersInRosterField.Text = "32";
            // 
            // NumPlayersOnTeamField
            // 
            this.NumPlayersOnTeamField.Location = new System.Drawing.Point(12, 12);
            this.NumPlayersOnTeamField.Name = "NumPlayersOnTeamField";
            this.NumPlayersOnTeamField.Size = new System.Drawing.Size(37, 20);
            this.NumPlayersOnTeamField.TabIndex = 1;
            this.NumPlayersOnTeamField.Text = "1";
            // 
            // NumTeamsPerMatchField
            // 
            this.NumTeamsPerMatchField.Location = new System.Drawing.Point(12, 37);
            this.NumTeamsPerMatchField.Name = "NumTeamsPerMatchField";
            this.NumTeamsPerMatchField.Size = new System.Drawing.Size(37, 20);
            this.NumTeamsPerMatchField.TabIndex = 2;
            this.NumTeamsPerMatchField.Text = "4";
            // 
            // GenerateSetsButton
            // 
            this.GenerateSetsButton.Location = new System.Drawing.Point(12, 87);
            this.GenerateSetsButton.Name = "GenerateSetsButton";
            this.GenerateSetsButton.Size = new System.Drawing.Size(111, 23);
            this.GenerateSetsButton.TabIndex = 8;
            this.GenerateSetsButton.Text = "Generate Sets";
            this.GenerateSetsButton.UseVisualStyleBackColor = true;
            this.GenerateSetsButton.Click += new System.EventHandler(this.GenerateSetsButton_Click);
            // 
            // NumberOfPlayersInRosterLabel
            // 
            this.NumberOfPlayersInRosterLabel.AutoSize = true;
            this.NumberOfPlayersInRosterLabel.Location = new System.Drawing.Point(903, 44);
            this.NumberOfPlayersInRosterLabel.Name = "NumberOfPlayersInRosterLabel";
            this.NumberOfPlayersInRosterLabel.Size = new System.Drawing.Size(63, 13);
            this.NumberOfPlayersInRosterLabel.TabIndex = 9;
            this.NumberOfPlayersInRosterLabel.Text = "# of Players";
            // 
            // NumPlayersOnTeamLabel
            // 
            this.NumPlayersOnTeamLabel.AutoSize = true;
            this.NumPlayersOnTeamLabel.Location = new System.Drawing.Point(55, 15);
            this.NumPlayersOnTeamLabel.Name = "NumPlayersOnTeamLabel";
            this.NumPlayersOnTeamLabel.Size = new System.Drawing.Size(130, 13);
            this.NumPlayersOnTeamLabel.TabIndex = 10;
            this.NumPlayersOnTeamLabel.Text = "# of players on each team";
            // 
            // NumTeamsPerMatchLabel
            // 
            this.NumTeamsPerMatchLabel.AutoSize = true;
            this.NumTeamsPerMatchLabel.Location = new System.Drawing.Point(55, 40);
            this.NumTeamsPerMatchLabel.Name = "NumTeamsPerMatchLabel";
            this.NumTeamsPerMatchLabel.Size = new System.Drawing.Size(523, 13);
            this.NumTeamsPerMatchLabel.TabIndex = 11;
            this.NumTeamsPerMatchLabel.Text = "# of teams for each match. Example, 2v2 will be 2 teams. 4 Player FFA is 1 Player" +
    " on each Team and 4 teams";
            // 
            // OutputTextBox
            // 
            this.OutputTextBox.Location = new System.Drawing.Point(12, 129);
            this.OutputTextBox.Multiline = true;
            this.OutputTextBox.Name = "OutputTextBox";
            this.OutputTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.OutputTextBox.Size = new System.Drawing.Size(698, 391);
            this.OutputTextBox.TabIndex = 12;
            this.OutputTextBox.Text = "Click the button above";
            this.OutputTextBox.WordWrap = false;
            // 
            // NumSetsOfMatchesToGenerateField
            // 
            this.NumSetsOfMatchesToGenerateField.Location = new System.Drawing.Point(12, 61);
            this.NumSetsOfMatchesToGenerateField.Name = "NumSetsOfMatchesToGenerateField";
            this.NumSetsOfMatchesToGenerateField.Size = new System.Drawing.Size(37, 20);
            this.NumSetsOfMatchesToGenerateField.TabIndex = 13;
            this.NumSetsOfMatchesToGenerateField.Text = "2";
            // 
            // NumMatchesToGenerateLabel
            // 
            this.NumMatchesToGenerateLabel.AutoSize = true;
            this.NumMatchesToGenerateLabel.Location = new System.Drawing.Point(55, 64);
            this.NumMatchesToGenerateLabel.Name = "NumMatchesToGenerateLabel";
            this.NumMatchesToGenerateLabel.Size = new System.Drawing.Size(160, 13);
            this.NumMatchesToGenerateLabel.TabIndex = 14;
            this.NumMatchesToGenerateLabel.Text = "# of sets of matches to generate";
            // 
            // ExplanationLabel
            // 
            this.ExplanationLabel.AutoSize = true;
            this.ExplanationLabel.Location = new System.Drawing.Point(9, 113);
            this.ExplanationLabel.Name = "ExplanationLabel";
            this.ExplanationLabel.Size = new System.Drawing.Size(733, 13);
            this.ExplanationLabel.TabIndex = 16;
            this.ExplanationLabel.Text = "(matchups are prioritized to reduce the # of times you\'ve been teamed up with som" +
    "eone, then it tries to reduce the amount you\'ve played against someone)";
            // 
            // DebugCheckbox
            // 
            this.DebugCheckbox.AutoSize = true;
            this.DebugCheckbox.Location = new System.Drawing.Point(652, 11);
            this.DebugCheckbox.Name = "DebugCheckbox";
            this.DebugCheckbox.Size = new System.Drawing.Size(58, 17);
            this.DebugCheckbox.TabIndex = 18;
            this.DebugCheckbox.Text = "Debug";
            this.DebugCheckbox.UseVisualStyleBackColor = true;
            this.DebugCheckbox.Visible = false;
            this.DebugCheckbox.CheckedChanged += new System.EventHandler(this.DebugCheckbox_CheckedChanged);
            // 
            // CreateNewRosterButton
            // 
            this.CreateNewRosterButton.Location = new System.Drawing.Point(860, 62);
            this.CreateNewRosterButton.Name = "CreateNewRosterButton";
            this.CreateNewRosterButton.Size = new System.Drawing.Size(107, 23);
            this.CreateNewRosterButton.TabIndex = 19;
            this.CreateNewRosterButton.Text = "Create New Roster";
            this.CreateNewRosterButton.UseVisualStyleBackColor = true;
            this.CreateNewRosterButton.Click += new System.EventHandler(this.CreateNewRosterButton_Click);
            // 
            // testlabel
            // 
            this.testlabel.AutoSize = true;
            this.testlabel.Location = new System.Drawing.Point(635, 68);
            this.testlabel.Name = "testlabel";
            this.testlabel.Size = new System.Drawing.Size(46, 13);
            this.testlabel.TabIndex = 21;
            this.testlabel.Text = "testlabel";
            this.testlabel.Visible = false;
            // 
            // PlayerNamesLabel
            // 
            this.PlayerNamesLabel.AutoSize = true;
            this.PlayerNamesLabel.Location = new System.Drawing.Point(798, 115);
            this.PlayerNamesLabel.Name = "PlayerNamesLabel";
            this.PlayerNamesLabel.Size = new System.Drawing.Size(72, 13);
            this.PlayerNamesLabel.TabIndex = 24;
            this.PlayerNamesLabel.Text = "Player Names";
            // 
            // SaveNameChangesButton
            // 
            this.SaveNameChangesButton.Location = new System.Drawing.Point(876, 110);
            this.SaveNameChangesButton.Name = "SaveNameChangesButton";
            this.SaveNameChangesButton.Size = new System.Drawing.Size(122, 23);
            this.SaveNameChangesButton.TabIndex = 25;
            this.SaveNameChangesButton.Text = "Save Name Changes";
            this.SaveNameChangesButton.UseVisualStyleBackColor = true;
            this.SaveNameChangesButton.Click += new System.EventHandler(this.SaveNameChangesButton_Click);
            // 
            // AddPlayerButton
            // 
            this.AddPlayerButton.Location = new System.Drawing.Point(860, 526);
            this.AddPlayerButton.Name = "AddPlayerButton";
            this.AddPlayerButton.Size = new System.Drawing.Size(75, 23);
            this.AddPlayerButton.TabIndex = 26;
            this.AddPlayerButton.Text = "Add Player";
            this.AddPlayerButton.UseVisualStyleBackColor = true;
            this.AddPlayerButton.Click += new System.EventHandler(this.AddPlayerButton_Click);
            // 
            // PlayerNamesTextBox
            // 
            this.PlayerNamesTextBox.Buddy = null;
            this.PlayerNamesTextBox.Location = new System.Drawing.Point(801, 136);
            this.PlayerNamesTextBox.Multiline = true;
            this.PlayerNamesTextBox.Name = "PlayerNamesTextBox";
            this.PlayerNamesTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.PlayerNamesTextBox.Size = new System.Drawing.Size(188, 384);
            this.PlayerNamesTextBox.TabIndex = 23;
            this.PlayerNamesTextBox.WordWrap = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1001, 561);
            this.Controls.Add(this.AddPlayerButton);
            this.Controls.Add(this.SaveNameChangesButton);
            this.Controls.Add(this.PlayerNamesLabel);
            this.Controls.Add(this.PlayerNamesTextBox);
            this.Controls.Add(this.testlabel);
            this.Controls.Add(this.CreateNewRosterButton);
            this.Controls.Add(this.DebugCheckbox);
            this.Controls.Add(this.ExplanationLabel);
            this.Controls.Add(this.NumMatchesToGenerateLabel);
            this.Controls.Add(this.NumSetsOfMatchesToGenerateField);
            this.Controls.Add(this.OutputTextBox);
            this.Controls.Add(this.NumTeamsPerMatchLabel);
            this.Controls.Add(this.NumPlayersOnTeamLabel);
            this.Controls.Add(this.NumberOfPlayersInRosterLabel);
            this.Controls.Add(this.GenerateSetsButton);
            this.Controls.Add(this.NumTeamsPerMatchField);
            this.Controls.Add(this.NumPlayersOnTeamField);
            this.Controls.Add(this.NumberOfPlayersInRosterField);
            this.Name = "Form1";
            this.Text = "Dynamic Round Robin Generator";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox NumberOfPlayersInRosterField;
        private System.Windows.Forms.TextBox NumPlayersOnTeamField;
        private System.Windows.Forms.TextBox NumTeamsPerMatchField;
        private System.Windows.Forms.Button GenerateSetsButton;
        private System.Windows.Forms.Label NumberOfPlayersInRosterLabel;
        private System.Windows.Forms.Label NumPlayersOnTeamLabel;
        private System.Windows.Forms.Label NumTeamsPerMatchLabel;
        private System.Windows.Forms.TextBox OutputTextBox;
        private System.Windows.Forms.TextBox NumSetsOfMatchesToGenerateField;
        private System.Windows.Forms.Label NumMatchesToGenerateLabel;
        private System.Windows.Forms.Label ExplanationLabel;
        private System.Windows.Forms.CheckBox DebugCheckbox;
        private System.Windows.Forms.Button CreateNewRosterButton;
        private System.Windows.Forms.Label testlabel;
        private SyncTextBox PlayerNamesTextBox;
        private System.Windows.Forms.Label PlayerNamesLabel;
        private System.Windows.Forms.Button SaveNameChangesButton;
        private System.Windows.Forms.Button AddPlayerButton;
    }
}

