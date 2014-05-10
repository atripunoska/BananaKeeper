namespace BananaKeeper
{
    partial class Board
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Board));
            this.lblMvs = new System.Windows.Forms.Label();
            this.lblMoves = new System.Windows.Forms.Label();
            this.grpMoves = new System.Windows.Forms.GroupBox();
            this.lblPlayerName = new System.Windows.Forms.Label();
            this.lblLevelNr = new System.Windows.Forms.Label();
            
            this.grpMoves.SuspendLayout();            
            this.SuspendLayout();
            // 
            // lblMvs
            // 
            this.lblMvs.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMvs.ForeColor = System.Drawing.Color.White;
            this.lblMvs.Location = new System.Drawing.Point(16, 24);
            this.lblMvs.Name = "lblMvs";
            this.lblMvs.Size = new System.Drawing.Size(52, 16);
            this.lblMvs.TabIndex = 0;
            this.lblMvs.Text = "Moves:";
            // 
            // lblMoves
            // 
            this.lblMoves.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMoves.ForeColor = System.Drawing.Color.Orange;
            this.lblMoves.Location = new System.Drawing.Point(72, 24);
            this.lblMoves.Name = "lblMoves";
            this.lblMoves.Size = new System.Drawing.Size(44, 16);
            this.lblMoves.TabIndex = 2;
            // 
            // grpMoves
            // 
            this.grpMoves.Controls.Add(this.lblMvs);
            this.grpMoves.Controls.Add(this.lblMoves);
            this.grpMoves.Location = new System.Drawing.Point(40, 56);
            this.grpMoves.Name = "grpMoves";
            this.grpMoves.Size = new System.Drawing.Size(120, 64);
            this.grpMoves.TabIndex = 4;
            
            // 
            // lblPlayerName
            // 
            this.lblPlayerName.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPlayerName.ForeColor = System.Drawing.Color.White;
            this.lblPlayerName.Location = new System.Drawing.Point(80, 16);
            this.lblPlayerName.Name = "lblPlayerName";
            this.lblPlayerName.Size = new System.Drawing.Size(150, 24);
            this.lblPlayerName.TabIndex = 4;
            this.lblPlayerName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblLevelNr
            // 
            this.lblLevelNr.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLevelNr.ForeColor = System.Drawing.Color.White;
            this.lblLevelNr.Location = new System.Drawing.Point(168, 48);
            this.lblLevelNr.Name = "lblLevelNr";
            this.lblLevelNr.Size = new System.Drawing.Size(150, 16);
            this.lblLevelNr.TabIndex = 4;
            this.lblLevelNr.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            // Board
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(446, 200);
            
            this.Controls.Add(this.grpMoves);
            this.Controls.Add(this.lblPlayerName);
            this.Controls.Add(this.lblLevelNr);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Board";
            this.Text = "Banana Keeper";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.AKeyDown);
            this.grpMoves.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        
        private System.Windows.Forms.Label lblMvs;
        private System.Windows.Forms.Label lblMoves;
        private System.Windows.Forms.GroupBox grpMoves;
        private System.Windows.Forms.Label lblPlayerName;
        private System.Windows.Forms.Label lblLevelNr;
        
    }
}