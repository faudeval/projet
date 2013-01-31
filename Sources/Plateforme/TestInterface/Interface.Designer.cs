namespace TestInterface
{
    partial class Interface
    {
        /// <summary>
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur Windows Form

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.g2IUTToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.téléchargerDesJeuxToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.envoyerLesScoresToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.quitterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.affichageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.miniaturesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mosaïquesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.icônesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.listeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.détailsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.body = new System.Windows.Forms.Panel();
            this.listView1 = new System.Windows.Forms.ListView();
            this.name = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.installState = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.description = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.mediumIconList = new System.Windows.Forms.ImageList(this.components);
            this.smallIconList = new System.Windows.Forms.ImageList(this.components);
            this.menuStrip1.SuspendLayout();
            this.body.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.g2IUTToolStripMenuItem,
            this.affichageToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(650, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // g2IUTToolStripMenuItem
            // 
            this.g2IUTToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.téléchargerDesJeuxToolStripMenuItem,
            this.envoyerLesScoresToolStripMenuItem,
            this.quitterToolStripMenuItem});
            this.g2IUTToolStripMenuItem.Name = "g2IUTToolStripMenuItem";
            this.g2IUTToolStripMenuItem.Size = new System.Drawing.Size(59, 20);
            this.g2IUTToolStripMenuItem.Text = "Actions";
            // 
            // téléchargerDesJeuxToolStripMenuItem
            // 
            this.téléchargerDesJeuxToolStripMenuItem.Name = "téléchargerDesJeuxToolStripMenuItem";
            this.téléchargerDesJeuxToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
            this.téléchargerDesJeuxToolStripMenuItem.Text = "Télécharger des jeux";
            this.téléchargerDesJeuxToolStripMenuItem.Click += new System.EventHandler(this.téléchargerDesJeuxToolStripMenuItem_Click);
            // 
            // envoyerLesScoresToolStripMenuItem
            // 
            this.envoyerLesScoresToolStripMenuItem.Name = "envoyerLesScoresToolStripMenuItem";
            this.envoyerLesScoresToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
            this.envoyerLesScoresToolStripMenuItem.Text = "Envoyer les scores";
            this.envoyerLesScoresToolStripMenuItem.Click += new System.EventHandler(this.envoyerLesScoresToolStripMenuItem_Click);
            // 
            // quitterToolStripMenuItem
            // 
            this.quitterToolStripMenuItem.Name = "quitterToolStripMenuItem";
            this.quitterToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
            this.quitterToolStripMenuItem.Text = "Quitter";
            this.quitterToolStripMenuItem.Click += new System.EventHandler(this.quitterToolStripMenuItem_Click);
            // 
            // affichageToolStripMenuItem
            // 
            this.affichageToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miniaturesToolStripMenuItem,
            this.mosaïquesToolStripMenuItem,
            this.icônesToolStripMenuItem,
            this.listeToolStripMenuItem,
            this.détailsToolStripMenuItem});
            this.affichageToolStripMenuItem.Name = "affichageToolStripMenuItem";
            this.affichageToolStripMenuItem.Size = new System.Drawing.Size(70, 20);
            this.affichageToolStripMenuItem.Text = "Affichage";
            // 
            // miniaturesToolStripMenuItem
            // 
            this.miniaturesToolStripMenuItem.Name = "miniaturesToolStripMenuItem";
            this.miniaturesToolStripMenuItem.Size = new System.Drawing.Size(131, 22);
            this.miniaturesToolStripMenuItem.Text = "Miniatures";
            this.miniaturesToolStripMenuItem.Click += new System.EventHandler(this.miniaturesToolStripMenuItem_Click);
            // 
            // mosaïquesToolStripMenuItem
            // 
            this.mosaïquesToolStripMenuItem.Name = "mosaïquesToolStripMenuItem";
            this.mosaïquesToolStripMenuItem.Size = new System.Drawing.Size(131, 22);
            this.mosaïquesToolStripMenuItem.Text = "Mosaïques";
            this.mosaïquesToolStripMenuItem.Click += new System.EventHandler(this.mosaïquesToolStripMenuItem_Click);
            // 
            // icônesToolStripMenuItem
            // 
            this.icônesToolStripMenuItem.Name = "icônesToolStripMenuItem";
            this.icônesToolStripMenuItem.Size = new System.Drawing.Size(131, 22);
            this.icônesToolStripMenuItem.Text = "Icônes";
            this.icônesToolStripMenuItem.Click += new System.EventHandler(this.icônesToolStripMenuItem_Click);
            // 
            // listeToolStripMenuItem
            // 
            this.listeToolStripMenuItem.Name = "listeToolStripMenuItem";
            this.listeToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.listeToolStripMenuItem.Text = "Liste";
            this.listeToolStripMenuItem.Click += new System.EventHandler(this.listeToolStripMenuItem_Click);
            // 
            // détailsToolStripMenuItem
            // 
            this.détailsToolStripMenuItem.Checked = true;
            this.détailsToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.détailsToolStripMenuItem.Name = "détailsToolStripMenuItem";
            this.détailsToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.détailsToolStripMenuItem.Text = "Détails";
            this.détailsToolStripMenuItem.Click += new System.EventHandler(this.détailsToolStripMenuItem_Click);
            // 
            // body
            // 
            this.body.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.body.Controls.Add(this.listView1);
            this.body.Dock = System.Windows.Forms.DockStyle.Fill;
            this.body.Location = new System.Drawing.Point(0, 24);
            this.body.Name = "body";
            this.body.Size = new System.Drawing.Size(650, 316);
            this.body.TabIndex = 1;
            // 
            // listView1
            // 
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.name,
            this.installState,
            this.description});
            this.listView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listView1.LargeImageList = this.mediumIconList;
            this.listView1.Location = new System.Drawing.Point(0, 0);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(646, 312);
            this.listView1.SmallImageList = this.smallIconList;
            this.listView1.TabIndex = 0;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            this.listView1.DoubleClick += new System.EventHandler(this.listView1_DoubleClick);
            // 
            // name
            // 
            this.name.Text = "Nom";
            this.name.Width = 150;
            // 
            // installState
            // 
            this.installState.DisplayIndex = 2;
            this.installState.Text = "Etat";
            this.installState.Width = 100;
            // 
            // description
            // 
            this.description.Text = "Description";
            this.description.Width = 400;
            // 
            // mediumIconList
            // 
            this.mediumIconList.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.mediumIconList.ImageSize = new System.Drawing.Size(32, 32);
            this.mediumIconList.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // smallIconList
            // 
            this.smallIconList.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.smallIconList.ImageSize = new System.Drawing.Size(16, 16);
            this.smallIconList.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // Interface
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(650, 340);
            this.Controls.Add(this.body);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Interface";
            this.Text = "G2IUT";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.body.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem g2IUTToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem téléchargerDesJeuxToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem envoyerLesScoresToolStripMenuItem;
        private System.Windows.Forms.Panel body;
        private System.Windows.Forms.ToolStripMenuItem quitterToolStripMenuItem;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ImageList smallIconList;
        private System.Windows.Forms.ToolStripMenuItem affichageToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem miniaturesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mosaïquesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem icônesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem listeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem détailsToolStripMenuItem;
        private System.Windows.Forms.ImageList mediumIconList;
        private System.Windows.Forms.ColumnHeader name;
        private System.Windows.Forms.ColumnHeader description;
        private System.Windows.Forms.ColumnHeader installState;
    }
}

