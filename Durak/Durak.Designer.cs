namespace Durak
{
    partial class Durak
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
            this.tlpMainMenu = new System.Windows.Forms.TableLayoutPanel();
            this.btnPlay = new System.Windows.Forms.Button();
            this.btnOptions = new System.Windows.Forms.Button();
            this.btnStats = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.lblTitle = new System.Windows.Forms.Label();
            this.tlpOptionMenu = new System.Windows.Forms.TableLayoutPanel();
            this.grpVariations = new System.Windows.Forms.GroupBox();
            this.clbVariationsList = new System.Windows.Forms.CheckedListBox();
            this.grpCardSkins = new System.Windows.Forms.GroupBox();
            this.cmbCardSkins = new System.Windows.Forms.ComboBox();
            this.picCardSkinPreview = new System.Windows.Forms.PictureBox();
            this.btnMainMenu = new System.Windows.Forms.Button();
            this.btnHelp = new System.Windows.Forms.Button();
            this.tlpStatsMenu = new System.Windows.Forms.TableLayoutPanel();
            this.btnMainMenuStats = new System.Windows.Forms.Button();
            this.btnStatsReset = new System.Windows.Forms.Button();
            this.lblPlayerName = new System.Windows.Forms.Label();
            this.lblGames = new System.Windows.Forms.Label();
            this.lblWins = new System.Windows.Forms.Label();
            this.lblLosses = new System.Windows.Forms.Label();
            this.lblNumberOfGames = new System.Windows.Forms.Label();
            this.lblNumberOfWins = new System.Windows.Forms.Label();
            this.lblNumberOfLosses = new System.Windows.Forms.Label();
            this.tlpMainMenu.SuspendLayout();
            this.tlpOptionMenu.SuspendLayout();
            this.grpVariations.SuspendLayout();
            this.grpCardSkins.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picCardSkinPreview)).BeginInit();
            this.tlpStatsMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlpMainMenu
            // 
            this.tlpMainMenu.ColumnCount = 2;
            this.tlpMainMenu.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpMainMenu.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpMainMenu.Controls.Add(this.btnPlay, 0, 1);
            this.tlpMainMenu.Controls.Add(this.btnOptions, 1, 1);
            this.tlpMainMenu.Controls.Add(this.btnStats, 0, 2);
            this.tlpMainMenu.Controls.Add(this.btnExit, 1, 2);
            this.tlpMainMenu.Controls.Add(this.lblTitle, 0, 0);
            this.tlpMainMenu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpMainMenu.Location = new System.Drawing.Point(0, 0);
            this.tlpMainMenu.Name = "tlpMainMenu";
            this.tlpMainMenu.RowCount = 3;
            this.tlpMainMenu.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tlpMainMenu.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tlpMainMenu.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tlpMainMenu.Size = new System.Drawing.Size(704, 411);
            this.tlpMainMenu.TabIndex = 0;
            // 
            // btnPlay
            // 
            this.btnPlay.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnPlay.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(218)))), ((int)(((byte)(196)))));
            this.btnPlay.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPlay.Location = new System.Drawing.Point(112, 177);
            this.btnPlay.Name = "btnPlay";
            this.btnPlay.Size = new System.Drawing.Size(128, 56);
            this.btnPlay.TabIndex = 0;
            this.btnPlay.Text = "Play";
            this.btnPlay.UseVisualStyleBackColor = false;
            this.btnPlay.Click += new System.EventHandler(this.btnPlay_Click);
            // 
            // btnOptions
            // 
            this.btnOptions.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnOptions.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(218)))), ((int)(((byte)(196)))));
            this.btnOptions.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOptions.Location = new System.Drawing.Point(464, 177);
            this.btnOptions.Name = "btnOptions";
            this.btnOptions.Size = new System.Drawing.Size(128, 56);
            this.btnOptions.TabIndex = 1;
            this.btnOptions.Text = "Options";
            this.btnOptions.UseVisualStyleBackColor = false;
            this.btnOptions.Click += new System.EventHandler(this.btnOptions_Click);
            // 
            // btnStats
            // 
            this.btnStats.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnStats.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(218)))), ((int)(((byte)(196)))));
            this.btnStats.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Bold);
            this.btnStats.Location = new System.Drawing.Point(112, 314);
            this.btnStats.Name = "btnStats";
            this.btnStats.Size = new System.Drawing.Size(128, 56);
            this.btnStats.TabIndex = 2;
            this.btnStats.Text = "Stats";
            this.btnStats.UseVisualStyleBackColor = false;
            this.btnStats.Click += new System.EventHandler(this.btnStats_Click);
            // 
            // btnExit
            // 
            this.btnExit.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnExit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(218)))), ((int)(((byte)(196)))));
            this.btnExit.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Bold);
            this.btnExit.Location = new System.Drawing.Point(464, 314);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(128, 56);
            this.btnExit.TabIndex = 3;
            this.btnExit.Text = "Exit";
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // lblTitle
            // 
            this.lblTitle.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblTitle.AutoSize = true;
            this.tlpMainMenu.SetColumnSpan(this.lblTitle, 2);
            this.lblTitle.Font = new System.Drawing.Font("Impact", 27.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(218)))), ((int)(((byte)(196)))));
            this.lblTitle.Location = new System.Drawing.Point(297, 46);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(109, 45);
            this.lblTitle.TabIndex = 4;
            this.lblTitle.Text = "Durak";
            // 
            // tlpOptionMenu
            // 
            this.tlpOptionMenu.ColumnCount = 2;
            this.tlpOptionMenu.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpOptionMenu.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpOptionMenu.Controls.Add(this.grpVariations, 0, 0);
            this.tlpOptionMenu.Controls.Add(this.grpCardSkins, 1, 0);
            this.tlpOptionMenu.Controls.Add(this.btnMainMenu, 0, 1);
            this.tlpOptionMenu.Controls.Add(this.btnHelp, 1, 1);
            this.tlpOptionMenu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpOptionMenu.Location = new System.Drawing.Point(0, 0);
            this.tlpOptionMenu.Name = "tlpOptionMenu";
            this.tlpOptionMenu.RowCount = 2;
            this.tlpOptionMenu.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpOptionMenu.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 68F));
            this.tlpOptionMenu.Size = new System.Drawing.Size(704, 411);
            this.tlpOptionMenu.TabIndex = 1;
            this.tlpOptionMenu.Visible = false;
            // 
            // grpVariations
            // 
            this.grpVariations.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpVariations.Controls.Add(this.clbVariationsList);
            this.grpVariations.Font = new System.Drawing.Font("Segoe UI Semibold", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpVariations.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(218)))), ((int)(((byte)(196)))));
            this.grpVariations.Location = new System.Drawing.Point(3, 3);
            this.grpVariations.Name = "grpVariations";
            this.grpVariations.Size = new System.Drawing.Size(346, 337);
            this.grpVariations.TabIndex = 0;
            this.grpVariations.TabStop = false;
            this.grpVariations.Text = "Variations";
            // 
            // clbVariationsList
            // 
            this.clbVariationsList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.clbVariationsList.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(73)))), ((int)(((byte)(115)))));
            this.clbVariationsList.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.clbVariationsList.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.clbVariationsList.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(218)))), ((int)(((byte)(196)))));
            this.clbVariationsList.FormattingEnabled = true;
            this.clbVariationsList.Items.AddRange(new object[] {
            "Passing Durak",
            "Fool with Epaulettes"});
            this.clbVariationsList.Location = new System.Drawing.Point(33, 49);
            this.clbVariationsList.Name = "clbVariationsList";
            this.clbVariationsList.Size = new System.Drawing.Size(288, 252);
            this.clbVariationsList.TabIndex = 0;
            // 
            // grpCardSkins
            // 
            this.grpCardSkins.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpCardSkins.Controls.Add(this.cmbCardSkins);
            this.grpCardSkins.Controls.Add(this.picCardSkinPreview);
            this.grpCardSkins.Font = new System.Drawing.Font("Segoe UI Semibold", 15.75F, System.Drawing.FontStyle.Bold);
            this.grpCardSkins.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(218)))), ((int)(((byte)(196)))));
            this.grpCardSkins.Location = new System.Drawing.Point(355, 3);
            this.grpCardSkins.Name = "grpCardSkins";
            this.grpCardSkins.Size = new System.Drawing.Size(346, 337);
            this.grpCardSkins.TabIndex = 1;
            this.grpCardSkins.TabStop = false;
            this.grpCardSkins.Text = "Card Skins";
            // 
            // cmbCardSkins
            // 
            this.cmbCardSkins.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.cmbCardSkins.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbCardSkins.FormattingEnabled = true;
            this.cmbCardSkins.Location = new System.Drawing.Point(100, 280);
            this.cmbCardSkins.Name = "cmbCardSkins";
            this.cmbCardSkins.Size = new System.Drawing.Size(177, 33);
            this.cmbCardSkins.TabIndex = 1;
            // 
            // picCardSkinPreview
            // 
            this.picCardSkinPreview.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.picCardSkinPreview.Location = new System.Drawing.Point(100, 49);
            this.picCardSkinPreview.Name = "picCardSkinPreview";
            this.picCardSkinPreview.Size = new System.Drawing.Size(150, 225);
            this.picCardSkinPreview.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picCardSkinPreview.TabIndex = 0;
            this.picCardSkinPreview.TabStop = false;
            // 
            // btnMainMenu
            // 
            this.btnMainMenu.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btnMainMenu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(218)))), ((int)(((byte)(196)))));
            this.btnMainMenu.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMainMenu.Location = new System.Drawing.Point(3, 349);
            this.btnMainMenu.Name = "btnMainMenu";
            this.btnMainMenu.Size = new System.Drawing.Size(150, 56);
            this.btnMainMenu.TabIndex = 2;
            this.btnMainMenu.Text = "Main Menu";
            this.btnMainMenu.UseVisualStyleBackColor = false;
            this.btnMainMenu.Click += new System.EventHandler(this.btnMainMenu_Click);
            // 
            // btnHelp
            // 
            this.btnHelp.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnHelp.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(218)))), ((int)(((byte)(196)))));
            this.btnHelp.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnHelp.Location = new System.Drawing.Point(551, 349);
            this.btnHelp.Name = "btnHelp";
            this.btnHelp.Size = new System.Drawing.Size(150, 56);
            this.btnHelp.TabIndex = 3;
            this.btnHelp.Text = "Help";
            this.btnHelp.UseVisualStyleBackColor = false;
            this.btnHelp.Click += new System.EventHandler(this.btnHelp_Click);
            // 
            // tlpStatsMenu
            // 
            this.tlpStatsMenu.ColumnCount = 2;
            this.tlpStatsMenu.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpStatsMenu.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpStatsMenu.Controls.Add(this.btnMainMenuStats, 0, 4);
            this.tlpStatsMenu.Controls.Add(this.btnStatsReset, 1, 4);
            this.tlpStatsMenu.Controls.Add(this.lblPlayerName, 0, 0);
            this.tlpStatsMenu.Controls.Add(this.lblGames, 0, 1);
            this.tlpStatsMenu.Controls.Add(this.lblWins, 0, 2);
            this.tlpStatsMenu.Controls.Add(this.lblLosses, 0, 3);
            this.tlpStatsMenu.Controls.Add(this.lblNumberOfGames, 1, 1);
            this.tlpStatsMenu.Controls.Add(this.lblNumberOfWins, 1, 2);
            this.tlpStatsMenu.Controls.Add(this.lblNumberOfLosses, 1, 3);
            this.tlpStatsMenu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpStatsMenu.Location = new System.Drawing.Point(0, 0);
            this.tlpStatsMenu.Name = "tlpStatsMenu";
            this.tlpStatsMenu.RowCount = 5;
            this.tlpStatsMenu.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tlpStatsMenu.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tlpStatsMenu.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tlpStatsMenu.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 24.3309F));
            this.tlpStatsMenu.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 15.81509F));
            this.tlpStatsMenu.Size = new System.Drawing.Size(704, 411);
            this.tlpStatsMenu.TabIndex = 2;
            this.tlpStatsMenu.Visible = false;
            // 
            // btnMainMenuStats
            // 
            this.btnMainMenuStats.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btnMainMenuStats.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(218)))), ((int)(((byte)(196)))));
            this.btnMainMenuStats.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMainMenuStats.Location = new System.Drawing.Point(3, 350);
            this.btnMainMenuStats.Name = "btnMainMenuStats";
            this.btnMainMenuStats.Size = new System.Drawing.Size(150, 56);
            this.btnMainMenuStats.TabIndex = 0;
            this.btnMainMenuStats.Text = "MainMenu";
            this.btnMainMenuStats.UseVisualStyleBackColor = false;
            this.btnMainMenuStats.Click += new System.EventHandler(this.btnMainMenuStats_Click);
            // 
            // btnStatsReset
            // 
            this.btnStatsReset.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnStatsReset.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(218)))), ((int)(((byte)(196)))));
            this.btnStatsReset.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStatsReset.Location = new System.Drawing.Point(551, 350);
            this.btnStatsReset.Name = "btnStatsReset";
            this.btnStatsReset.Size = new System.Drawing.Size(150, 56);
            this.btnStatsReset.TabIndex = 1;
            this.btnStatsReset.Text = "Reset Stats";
            this.btnStatsReset.UseVisualStyleBackColor = false;
            // 
            // lblPlayerName
            // 
            this.lblPlayerName.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblPlayerName.AutoSize = true;
            this.tlpStatsMenu.SetColumnSpan(this.lblPlayerName, 2);
            this.lblPlayerName.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPlayerName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(218)))), ((int)(((byte)(196)))));
            this.lblPlayerName.Location = new System.Drawing.Point(247, 10);
            this.lblPlayerName.Margin = new System.Windows.Forms.Padding(3, 10, 3, 0);
            this.lblPlayerName.Name = "lblPlayerName";
            this.lblPlayerName.Size = new System.Drawing.Size(210, 45);
            this.lblPlayerName.TabIndex = 2;
            this.lblPlayerName.Text = "Player Name";
            // 
            // lblGames
            // 
            this.lblGames.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblGames.AutoSize = true;
            this.lblGames.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGames.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(218)))), ((int)(((byte)(196)))));
            this.lblGames.Location = new System.Drawing.Point(148, 108);
            this.lblGames.Name = "lblGames";
            this.lblGames.Size = new System.Drawing.Size(201, 30);
            this.lblGames.TabIndex = 3;
            this.lblGames.Text = "Number Of Games:";
            // 
            // lblWins
            // 
            this.lblWins.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblWins.AutoSize = true;
            this.lblWins.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblWins.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(218)))), ((int)(((byte)(196)))));
            this.lblWins.Location = new System.Drawing.Point(164, 190);
            this.lblWins.Name = "lblWins";
            this.lblWins.Size = new System.Drawing.Size(185, 30);
            this.lblWins.TabIndex = 3;
            this.lblWins.Text = "Number Of Wins:";
            // 
            // lblLosses
            // 
            this.lblLosses.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblLosses.AutoSize = true;
            this.lblLosses.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLosses.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(218)))), ((int)(((byte)(196)))));
            this.lblLosses.Location = new System.Drawing.Point(151, 280);
            this.lblLosses.Name = "lblLosses";
            this.lblLosses.Size = new System.Drawing.Size(198, 30);
            this.lblLosses.TabIndex = 3;
            this.lblLosses.Text = "Number Of Losses:";
            // 
            // lblNumberOfGames
            // 
            this.lblNumberOfGames.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblNumberOfGames.AutoSize = true;
            this.lblNumberOfGames.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNumberOfGames.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(218)))), ((int)(((byte)(196)))));
            this.lblNumberOfGames.Location = new System.Drawing.Point(355, 108);
            this.lblNumberOfGames.Name = "lblNumberOfGames";
            this.lblNumberOfGames.Size = new System.Drawing.Size(25, 30);
            this.lblNumberOfGames.TabIndex = 3;
            this.lblNumberOfGames.Text = "0";
            // 
            // lblNumberOfWins
            // 
            this.lblNumberOfWins.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblNumberOfWins.AutoSize = true;
            this.lblNumberOfWins.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNumberOfWins.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(218)))), ((int)(((byte)(196)))));
            this.lblNumberOfWins.Location = new System.Drawing.Point(355, 190);
            this.lblNumberOfWins.Name = "lblNumberOfWins";
            this.lblNumberOfWins.Size = new System.Drawing.Size(25, 30);
            this.lblNumberOfWins.TabIndex = 3;
            this.lblNumberOfWins.Text = "0";
            // 
            // lblNumberOfLosses
            // 
            this.lblNumberOfLosses.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblNumberOfLosses.AutoSize = true;
            this.lblNumberOfLosses.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNumberOfLosses.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(218)))), ((int)(((byte)(196)))));
            this.lblNumberOfLosses.Location = new System.Drawing.Point(355, 280);
            this.lblNumberOfLosses.Name = "lblNumberOfLosses";
            this.lblNumberOfLosses.Size = new System.Drawing.Size(25, 30);
            this.lblNumberOfLosses.TabIndex = 3;
            this.lblNumberOfLosses.Text = "0";
            // 
            // Durak
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(73)))), ((int)(((byte)(115)))));
            this.ClientSize = new System.Drawing.Size(704, 411);
            this.Controls.Add(this.tlpStatsMenu);
            this.Controls.Add(this.tlpOptionMenu);
            this.Controls.Add(this.tlpMainMenu);
            this.MinimumSize = new System.Drawing.Size(720, 450);
            this.Name = "Durak";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "OOP 4200 Durak";
            this.Load += new System.EventHandler(this.Durak_Load);
            this.tlpMainMenu.ResumeLayout(false);
            this.tlpMainMenu.PerformLayout();
            this.tlpOptionMenu.ResumeLayout(false);
            this.grpVariations.ResumeLayout(false);
            this.grpCardSkins.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picCardSkinPreview)).EndInit();
            this.tlpStatsMenu.ResumeLayout(false);
            this.tlpStatsMenu.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlpMainMenu;
        private System.Windows.Forms.Button btnPlay;
        private System.Windows.Forms.Button btnOptions;
        private System.Windows.Forms.Button btnStats;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.TableLayoutPanel tlpOptionMenu;
        private System.Windows.Forms.GroupBox grpVariations;
        private System.Windows.Forms.GroupBox grpCardSkins;
        private System.Windows.Forms.ComboBox cmbCardSkins;
        private System.Windows.Forms.PictureBox picCardSkinPreview;
        private System.Windows.Forms.CheckedListBox clbVariationsList;
        private System.Windows.Forms.Button btnMainMenu;
        private System.Windows.Forms.Button btnHelp;
        private System.Windows.Forms.TableLayoutPanel tlpStatsMenu;
        private System.Windows.Forms.Button btnMainMenuStats;
        private System.Windows.Forms.Button btnStatsReset;
        private System.Windows.Forms.Label lblPlayerName;
        private System.Windows.Forms.Label lblGames;
        private System.Windows.Forms.Label lblWins;
        private System.Windows.Forms.Label lblLosses;
        private System.Windows.Forms.Label lblNumberOfGames;
        private System.Windows.Forms.Label lblNumberOfWins;
        private System.Windows.Forms.Label lblNumberOfLosses;
    }
}

