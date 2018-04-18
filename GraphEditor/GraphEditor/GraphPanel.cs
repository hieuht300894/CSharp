/*GrapPanel class -   Copyright © 2013, Glushchikov Dmitriy
 * All rights reserved.
 * GraphPanel is a child class of Panel control which provides a base class for all Panel elements
 * This class uses as container for node elements and a background for drawing
 * It includes methods for node managing and building
 * Mouse events for ther GraphNode class define in it
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
    public class GraphPanel : Panel
    {
        //Type which include information about selected edge
        public struct SelectedGraphEdge
        {
            public GraphNode graph;
            public bool isYes;
        }

        //Active GraphNode Control
        private GraphNode activeControl;
        //Previous mouse location
        private Point previousLocation;
        //Draw line mode 
        private bool isDrawLine = false;
        //current mouse location
        private Point currentLocation;
        //Edge width
        public int LineWidth = 2;
        //Edge color
        public Color LineColor = Color.Black;

        public ToolTip toolTip1;
        //Current node over the mouse cursor
        private SelectedGraphEdge currentGraph = new SelectedGraphEdge();

        // List of graph nodes
        public List<GraphNode> Graph = new List<GraphNode>();

        //define node menu, calls on mouse right click
        public System.Windows.Forms.ContextMenuStrip NodeMenu = null;




        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            //Draw all Edges
            DrawAllEdges();
        }

        /*Draw vector from point1 to point2, using absolute values*/
        private void DrawEdge(Color color, Point p1, Point p2)
        {
            using (Graphics g = this.CreateGraphics())
            {

                AdjustableArrowCap bigArrow = new AdjustableArrowCap(5, 5);
                Pen p = new Pen(color);
                p.Width = LineWidth;
                p.CustomEndCap = bigArrow;
                g.DrawLine(p, p1, p2);
                p.Dispose();
            }
        }



        /// <summary>
        /// Draw all edges of the graph in the cicle
        /// </summary>
        private void DrawAllEdges()
        {
            using (Graphics g = this.CreateGraphics())
            {
                foreach (GraphNode graph in Graph)
                {
                    //Для всех узлов выводим ребра
                    graph.DrawEdges(g, LineColor, LineWidth);

                }
            }
        }


        //if cursor over edge we change edge color
        //where px  is a point on the panel control
        private void SelectLine(Point px)
        {
            /*Erase previous selected edge (draw it using default color)*/
            if (currentGraph.graph != null)
            {
                currentGraph.graph.DrawEdge(this.CreateGraphics(), LineColor, LineWidth, currentGraph.isYes);
                currentGraph.graph = null;
            }
            //looking for the edge which point px belongs
            foreach (GraphNode graph in Graph)
            {

                if (graph.NodeNo != null)
                {
                    Point p1 = graph.EdgeNo[0];
                    Point p2 = graph.EdgeNo[1];

                    //p1 and p2 is realtive coordinates,  converts it to absolute values
                    p1.Offset(graph.Location);
                    p2.Offset(graph.NodeNo.Location);
                    //We need to solve equation of line, if a point on the edge 
                    //draw the edge using red color
                    if (isPointIn(p1, p2, px))
                    {

                        graph.DrawEdge(this.CreateGraphics(), Color.Red, LineWidth, false);
                        //save result for the next use 
                        currentGraph.graph = graph;
                        currentGraph.isYes = false;
                        return;
                    }
                    //if node connect to itself
                    if (graph.NodeNo == graph)
                    {
                        //we need to solve two equations, cause we have a curve which consits of two lines
                        Point pCenter = p1;
                        pCenter.Offset(20, -10);
                        if ((isPointIn(p1, pCenter, px)) || (isPointIn(p2, pCenter, px)))
                        {
                            graph.DrawEdge(this.CreateGraphics(), Color.Red, LineWidth, false);
                            currentGraph.graph = graph;
                            currentGraph.isYes = false;
                            return;



                        }
                    }


                }
                if (graph.NodeYes != null)
                {
                    Point p1 = graph.EdgeYes[0];
                    Point p2 = graph.EdgeYes[1];

                    //p1 and p2 is realtive coordinates,  converts it to absolute values
                    p1.Offset(graph.Location);
                    p2.Offset(graph.NodeYes.Location);
                    if (isPointIn(p1, p2, px))
                    {


                        graph.DrawEdge(this.CreateGraphics(), Color.Red, LineWidth, true);
                        currentGraph.graph = graph;
                        currentGraph.isYes = true;
                        return;
                    }//if node connect to itself
                    if (graph.NodeYes == graph)
                    {
                        //we need to solve two equations, cause we have a curve which consits of two lines
                        Point pCenter = p1;
                        pCenter.Offset(-20, -10);
                        if ((isPointIn(p1, pCenter, px)) || (isPointIn(p2, pCenter, px)))
                        {
                            graph.DrawEdge(this.CreateGraphics(), Color.Red, LineWidth, true);
                            currentGraph.graph = graph;
                            currentGraph.isYes = true;

                            return;
                        }
                    }

                }
            }

        }
        /// <summary>
        /// This method solves simple equation of line (x-x1)/(x2-x1)=(y-y1)/(y2-y1)
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <param name="px"></param>
        /// <returns></returns>
        private bool isPointIn(Point p1, Point p2, Point px)
        {
            //Check line bounds
            if (((px.X > p1.X) && (px.X > p2.X)) || ((px.X < p1.X) && (px.X < p2.X)))
                return false;

            double r1 = (double)(px.X - p1.X) / (p2.X - p1.X);
            double r2 = (double)(px.Y - p1.Y) / (p2.Y - p1.Y);
            //if r1==r2 or r1=0 or r2=0 then px belongs to the line p1;p2
            if ((r1 == 0) || (r2 == 0))
                return true;
            return Math.Round(r1, 1) == Math.Round(r2, 1);
        }


        /// <summary>
        /// Delets graph node and all incoming and outcoming connections (edges)
        /// </summary>
        /// <param name="curG"></param>
        public void DeleteNode(GraphNode curG)
        {
            //delete all edges which connect to the current graph
            foreach (GraphNode g in Graph)
            {
                if (g.NodeNo != null)
                    if (g.NodeNo == curG)
                    {
                        g.DeleteConnection(false);
                    }
                if (g.NodeYes != null)
                    if (g.NodeYes == curG)
                    {
                        g.DeleteConnection(true);
                    }

            }
            //Delete graph node from the list and panel control
            Graph.Remove(curG);
            this.Controls.Remove(curG);
            this.Invalidate();

        }

        /// <summary>
        /// Mouse Down event for GraphNode control
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void graphNode_MouseDown(object sender, MouseEventArgs e)
        {

            //if DrawLine mode then doesn't process this event
            if (isDrawLine)
                return;

            //Left button click,
            //By Left mouse click we select node for it's moving (by mouse_move event)
            //Save active node and current mouse location
            //change cursor
            if (e.Button == MouseButtons.Left)
            {
                activeControl = sender as GraphNode;

                previousLocation = e.Location;
                Cursor = Cursors.Hand;
                //pnGraph.Invalidate();
            }//right button click - is a draw line mode (draw connections between nodes)
            else if (e.Button == MouseButtons.Right)
            {
                isDrawLine = true; //enable draw mode
                activeControl = sender as GraphNode;  //get active node
                //if click is on the left half then "Yes", else is "No"
                previousLocation = new Point(e.Location.X > activeControl.Width / 2 ? activeControl.Width : 0, activeControl.Height / 2); //запоминаем текущее расположение
                //convert to absolute values
                previousLocation.Offset(activeControl.Location);
                //change cursor
                Cursor = Cursors.Cross;
                //At the start point current and previous location are equal
                currentLocation = previousLocation;
            }

        }
        /// <summary>
        /// Mouse move event for GraphNode control
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void graphNode_MouseMove(object sender, MouseEventArgs e)
        {
            //work if activeControl defined (Mouse Click was a previous event)
            if (activeControl == null || activeControl != sender)
            {
                return;
            }

            //Moving control mode
            if (!isDrawLine)
            {
                //calculate difference between previous and current location
                var offseLocation = new Point(e.Location.X - previousLocation.X, e.Location.Y - previousLocation.Y);
                //move active control with edges to currrent location
                activeControl.SetOffset(offseLocation);
                //redraw 
                this.Invalidate();

            }
            //Draw edge on the panel
            else
            {
                var location = previousLocation;
                location.Offset(activeControl.Location);
                location.Offset(e.Location.X - previousLocation.X, e.Location.Y - previousLocation.Y);
                using (Graphics g = this.CreateGraphics())
                {
                    //Draw all edges
                    DrawAllEdges();
                    //Erase edge which we start draw
                    DrawEdge(this.BackColor, previousLocation, currentLocation);
                    //redraw in the new location
                    DrawEdge(LineColor, previousLocation, location);
                    currentLocation = location;
                }



            }

        }

        /// <summary>
        /// Mouse up event for the GraphNode control
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void graphNode_MouseUp(object sender, MouseEventArgs e)
        {


            Cursor = Cursors.Default;

            //if draw line enabled
            if (isDrawLine)
            {
                //disable it
                isDrawLine = false;
                //current location
                Point curLoc = e.Location;
                curLoc.Offset(activeControl.Location);
                //Get Node over mouse 
                Control curCont = this.GetChildAtPoint(curLoc);


                //if mouse is not over Node control then return
                if (curCont == null)
                {
                    activeControl = null;
                    this.Invalidate();
                    return;
                }
                //check control typr
                if ((!curCont.GetType().Name.Equals("GraphNode")) || (curCont.Equals(activeControl)))
                {

                    this.Invalidate();


                    //if activeControl over mouse then connect node to itself
                    if (curCont.Equals(activeControl))
                    {

                        Point p = e.Location;
                        p.Offset(activeControl.Location);

                        //check if Node Menu defined 
                        if (NodeMenu != null)
                        {
                            NodeMenu.Tag = activeControl;
                            NodeMenu.Items[0].Tag = (previousLocation.X == activeControl.Location.X).ToString();
                            NodeMenu.Show(this, p);
                        }

                    }

                    activeControl = null;
                    return;
                }
                /*check start point left side is Yes and right side is No */
                bool isYes = previousLocation.X == activeControl.Location.X;
                activeControl.AddConnection(new Point(currentLocation.X - curCont.Location.X,
       previousLocation.Y < curCont.Location.Y ? 0 : curCont.Height), isYes, (curCont as GraphNode));
                activeControl = null;
                this.Invalidate();
                isDrawLine = false;
            }
            //обнуляем текущий элемент
            if (activeControl != null)
                activeControl = null;


        }

        /*Save graph on the file*/
        public void SaveGraphToFile(String fileName)
        {


            try
            {
                using (Stream stream = File.Open(fileName, FileMode.Create))
                {
                    MemoryStream ms = new MemoryStream();
                    BinaryFormatter bin = new BinaryFormatter();
                    bin.Serialize(stream, Graph);

                }
            }
            catch (IOException)
            {
            }

        }
        //Load Graph From File
        public void OpenGraphFromFile(String fileName)
        {
            try
            {
                using (Stream stream = File.Open(fileName, FileMode.Open))
                {
                    BinaryFormatter bin = new BinaryFormatter();

                    Graph = (List<GraphNode>)bin.Deserialize(stream);
                    foreach (GraphNode g in Graph)
                    {
                        //define events
                        g.MouseDown += new MouseEventHandler(graphNode_MouseDown);
                        g.MouseMove += new MouseEventHandler(graphNode_MouseMove);
                        g.MouseUp += new MouseEventHandler(graphNode_MouseUp);
                        this.Controls.Add(g);
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }


        /// <summary>
        /// Override on mouse event 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseMove(MouseEventArgs e)
        {
            //Select edge if cursor over it
            SelectLine(e.Location);
            base.OnMouseMove(e);

        }

        //Delete current selected edge
        public void DeleteEdge(SelectedGraphEdge currentgraph)
        {
            //delete current graph connection
            currentgraph.graph.DeleteConnection(currentgraph.isYes);
            currentgraph.graph = null;
            currentGraph = currentgraph;
            this.Invalidate();
        }

        public SelectedGraphEdge GetSelectedEdge()
        {
            return currentGraph;
        }

        /// <summary>
        /// Add node at the point
        /// </summary>
        /// <param name="location"></param>
        /// <returns></returns>
        public GraphNode AddNode(Point location)
        {

            GraphNode graphnode = new GraphNode();
            graphnode.Location = location;
            graphnode.MouseDown += new MouseEventHandler(graphNode_MouseDown);
            graphnode.MouseMove += new MouseEventHandler(graphNode_MouseMove);
            graphnode.MouseUp += new MouseEventHandler(graphNode_MouseUp);
            graphnode.Name = "Node" + Graph.Count.ToString();
            //Set text - may be other 
            graphnode.Text = graphnode.Name;


            this.Controls.Add(graphnode);
            Graph.Add(graphnode);
            return graphnode;

        }






    }
}
