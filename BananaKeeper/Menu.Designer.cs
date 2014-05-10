namespace BananaKeeper
{
    partial class Menu
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Menu));
            this.newgame = new System.Windows.Forms.Button();
            this.load = new System.Windows.Forms.Button();
            this.exit = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // newgame
            // 
            this.newgame.FlatAppearance.BorderSize = 0;
            this.newgame.Location = new System.Drawing.Point(460, 12);
            this.newgame.Name = "newgame";
            this.newgame.Size = new System.Drawing.Size(108, 37);
            this.newgame.TabIndex = 0;
            this.newgame.Text = "New Game";
            this.newgame.UseVisualStyleBackColor = true;
            this.newgame.Click += new System.EventHandler(this.newgame_Click);
            // 
            // load
            // 
            this.load.Location = new System.Drawing.Point(460, 67);
            this.load.Name = "load";
            this.load.Size = new System.Drawing.Size(108, 37);
            this.load.TabIndex = 1;
            this.load.Text = "Load Game";
            this.load.UseVisualStyleBackColor = true;
            this.load.Click += new System.EventHandler(this.load_Click);
            // 
            // exit
            // 
            this.exit.Location = new System.Drawing.Point(460, 124);
            this.exit.Name = "exit";
            this.exit.Size = new System.Drawing.Size(108, 37);
            this.exit.TabIndex = 2;
            this.exit.Text = "Exit";
            this.exit.UseVisualStyleBackColor = true;
            this.exit.Click += new System.EventHandler(this.exit_Click);
            // 
            // button1
            // 
            this.button1.Image = global::BananaKeeper.Properties.Resources.mute_2;
            this.button1.Location = new System.Drawing.Point(515, 279);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(53, 50);
            this.button1.TabIndex = 3;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Menu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::BananaKeeper.Properties.Resources.logo;
            this.ClientSize = new System.Drawing.Size(580, 341);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.exit);
            this.Controls.Add(this.load);
            this.Controls.Add(this.newgame);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Menu";
            this.Text = "Banana Keeper";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button newgame;
        private System.Windows.Forms.Button load;
        private System.Windows.Forms.Button exit;
        private System.Windows.Forms.Button button1;
    }
}

