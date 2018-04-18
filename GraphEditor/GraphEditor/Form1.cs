/**GrapEditor -   Copyright © 2013, Glushchikov Dmitriy
 * All rights reserved.
 * 
 * 
 * Main class it does nothing special,  only creates GraphPanel and load graph from file 
 * (if file exists), and save graph to file
 * In this class defines mnuNode , which uses to process events on node click
 * 
 * Manual:
 * 
 * ADD NODE
 * Mouse right click on the panel and click on the "Add" menu item
 * MOVE NODE
 * Mouse left click and move it until mouse up
 * ADD CONNECTION (EDGE)
 * Mouse right click on the Node and move it to other node and mouse button up
 * Yes connection has to start from the left side , No connection from the right side
 * If we need to make cicle node to itself - mouse right button click then mouse up and select "Cicle" from popup menu.
 * Using this menu we can Delete node and mark it as a First Node (Yellow color)
 * SELECT EDGE
 * Set mouse cursor over the edge (it becomes red) and then mouse right click 
 * 
 * 
 */

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;


namespace GraphEditor
{

    public partial class Form1 : Form
    {

        Point currentLocation;
        public Form1()
        {
            InitializeComponent();
            pnGraph.NodeMenu = mnuNode;
        }
       
        private void removeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pnGraph.DeleteEdge(pnGraph.GetSelectedEdge());
        }
        /*add new item GraphNode*/
        private void AddToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GraphNode g=pnGraph.AddNode(currentLocation);
        }


        private void DelNodeToolStripMenuItem_Click(object sender, EventArgs e)
        {
             pnGraph.DeleteNode(mnuNode.Tag as GraphNode);
        }
        
        //Open graph from file
        private void Form1_Load(object sender, EventArgs e)
        {
            pnGraph.OpenGraphFromFile(Application.StartupPath + "\\graph2.bin");
        }

        /// <summary>
        /// Save graph to file on form close
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
          
            pnGraph.SaveGraphToFile(Application.StartupPath + "\\graph2.bin");
        }


        private void pnGraph_MouseClick(object sender, MouseEventArgs e)
        {
           
           if ((e.Button == MouseButtons.Right))
           {
               //if click on the edge
               if ((pnGraph.GetSelectedEdge().graph != null))
               {
                   AddToolStripMenuItem.Enabled = false;
                   DeleteToolStripMenuItem.Enabled = true;
                   mnuEdge.Show(this, e.X, e.Y);
                   return;
               }//if click on the free panel place
               else
               {
                   AddToolStripMenuItem.Enabled = true;
                   DeleteToolStripMenuItem.Enabled = false;
                   currentLocation = e.Location;
                   mnuEdge.Show(this, e.X, e.Y);
               }
           }

        }
        //Change node is first or not
        private void ChangeNodeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            (mnuNode.Tag as GraphNode).isFirst = !(mnuNode.Tag as GraphNode).isFirst;
            ChangeNodeToolStripMenuItem.Checked =(mnuNode.Tag as GraphNode).isFirst;
        }

        private void mnuNode_Opening(object sender, CancelEventArgs e)
        {
            ChangeNodeToolStripMenuItem.Checked = (mnuNode.Tag as GraphNode).isFirst;
        }

        private void cicleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool isYes = Convert.ToBoolean(mnuNode.Items[0].Tag);
            GraphNode g = (GraphNode)mnuNode.Tag;
            if (isYes)
                /*
                g.AddConnection(new Point(g.Location.X, g.Location.Y + g.Height / 2),
                    g.Location, isYes, g);*/
                g.AddConnection(new Point(0, 0), isYes, g);

            else
                g.AddConnection(new Point(g.Width, 0), isYes, g);
        }  
    }
}