namespace GraphEditor
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
            this.components = new System.ComponentModel.Container();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.mnuEdge = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.DeleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AddToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuNode = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.DelNodeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ChangeNodeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pnGraph = new GraphEditor.GraphPanel();
            this.cicleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuEdge.SuspendLayout();
            this.mnuNode.SuspendLayout();
            this.SuspendLayout();
            // 
            // mnuEdge
            // 
            this.mnuEdge.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.DeleteToolStripMenuItem,
            this.AddToolStripMenuItem});
            this.mnuEdge.Name = "mnuEdge";
            this.mnuEdge.Size = new System.Drawing.Size(106, 48);
            // 
            // DeleteToolStripMenuItem
            // 
            this.DeleteToolStripMenuItem.Name = "DeleteToolStripMenuItem";
            this.DeleteToolStripMenuItem.Size = new System.Drawing.Size(105, 22);
            this.DeleteToolStripMenuItem.Text = "Delete";
            this.DeleteToolStripMenuItem.Click += new System.EventHandler(this.removeToolStripMenuItem_Click);
            // 
            // AddToolStripMenuItem
            // 
            this.AddToolStripMenuItem.Name = "AddToolStripMenuItem";
            this.AddToolStripMenuItem.Size = new System.Drawing.Size(105, 22);
            this.AddToolStripMenuItem.Text = "Add";
            this.AddToolStripMenuItem.Click += new System.EventHandler(this.AddToolStripMenuItem_Click);
            // 
            // mnuNode
            // 
            this.mnuNode.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.DelNodeToolStripMenuItem,
            this.ChangeNodeToolStripMenuItem,
            this.cicleToolStripMenuItem});
            this.mnuNode.Name = "mnuNode";
            this.mnuNode.Size = new System.Drawing.Size(153, 92);
            this.mnuNode.Opening += new System.ComponentModel.CancelEventHandler(this.mnuNode_Opening);
            // 
            // DelNodeToolStripMenuItem
            // 
            this.DelNodeToolStripMenuItem.Name = "DelNodeToolStripMenuItem";
            this.DelNodeToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.DelNodeToolStripMenuItem.Text = "Delete";
            this.DelNodeToolStripMenuItem.Click += new System.EventHandler(this.DelNodeToolStripMenuItem_Click);
            // 
            // ChangeNodeToolStripMenuItem
            // 
            this.ChangeNodeToolStripMenuItem.Name = "ChangeNodeToolStripMenuItem";
            this.ChangeNodeToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.ChangeNodeToolStripMenuItem.Text = "Is first";
            this.ChangeNodeToolStripMenuItem.Click += new System.EventHandler(this.ChangeNodeToolStripMenuItem_Click);
            // 
            // pnGraph
            // 
            this.pnGraph.AutoScroll = true;
            this.pnGraph.AutoSize = true;
            this.pnGraph.BackColor = System.Drawing.Color.White;
            this.pnGraph.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnGraph.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnGraph.Location = new System.Drawing.Point(0, 0);
            this.pnGraph.Name = "pnGraph";
            this.pnGraph.Size = new System.Drawing.Size(438, 375);
            this.pnGraph.TabIndex = 0;
            this.pnGraph.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pnGraph_MouseClick);
            // 
            // cicleToolStripMenuItem
            // 
            this.cicleToolStripMenuItem.Name = "cicleToolStripMenuItem";
            this.cicleToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.cicleToolStripMenuItem.Text = "Cicle";
            this.cicleToolStripMenuItem.Click += new System.EventHandler(this.cicleToolStripMenuItem_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(438, 375);
            this.Controls.Add(this.pnGraph);
            this.Name = "Form1";
            this.Text = "Graph Editor v0.1.";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.mnuEdge.ResumeLayout(false);
            this.mnuNode.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private GraphPanel pnGraph;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.ContextMenuStrip mnuEdge;
        private System.Windows.Forms.ToolStripMenuItem DeleteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem AddToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip mnuNode;
        private System.Windows.Forms.ToolStripMenuItem DelNodeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ChangeNodeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cicleToolStripMenuItem;

    }
}

