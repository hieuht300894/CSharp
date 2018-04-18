/*GrapNode class -   Copyright © 2013, Glushchikov Dmitriy
 * All rights reserved.
 * 
 * GraphNode is a Node element of our graph
 * Its parent is Button, but we can use any other control
 * GraphNode class marked as Serializable,therefore we can easy save it like array or file
 * This class has its own fieds and methods.  
 * Our Graph is oriented and can has only to outcoming connections (Edges), I called it Yes and No edges.
 * Yes edge starts from the left bound and No edge from the right bound
 * NodeYes and NodeNo are proper links to connected nodes
 * EdgeYes and EdgeNo are proper relevant coordinates 
 * 
 */

using System;
using System.Reflection;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;
using System.Windows.Forms;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;


namespace GraphEditor
{

    [Serializable()]
    public class GraphNode : Button, ISerializable
    {
        /*
         * Button control use as our Node
         */


        //Question for action (User have to answer Yes No)
        public String statusQuestion = "";

        //Edge on Yes question
        public Point[] EdgeYes = null;
        //Edge on NO question
        public Point[] EdgeNo = null;
        

        public GraphNode NodeYes=null; //Link to the Yes connection
        public GraphNode NodeNo=null; // Link to the No conncetion

            
      
        //Set first node of our graph
        public Boolean isFirst
        {
            get
            {
                return b_isFirst;
            }
            set
            {
                if (Convert.ToBoolean(value))  
                    this.BackColor = Color.Yellow;  //if isFirst then set Yellow back color
                else
                    this.BackColor = (new GraphNode()).BackColor; //else use defaul control back color
                b_isFirst = Convert.ToBoolean(value);

            }
        }

        private bool b_isFirst = false;


        /// <summary>
        /// Contstructor - init Control properties
        /// </summary>
        public GraphNode()
        {
            /*Intit parameters*/
            this.AllowDrop = true;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowOnly;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.FlatAppearance.BorderColor = System.Drawing.Color.Red;
            this.FlatAppearance.BorderSize = 1;
            this.FlatStyle = System.Windows.Forms.FlatStyle.Flat;

            this.UseVisualStyleBackColor = false;
        }

        //Set Node offset
        //Params: offset
        public void SetOffset(Point offset )
        {
            //Get current location
            Point p=this.Location;
            ///set offset
            p.Offset(offset);
            //set new location
            this.Location = p;

        }
        //Delete Edge and appropriate node link
        public void DeleteConnection(bool isYes)
        {
            if (isYes)
            {
                EdgeYes = null;
                NodeYes = null;
            }
            else
            {
                EdgeNo = null;
                NodeNo = null;
            }
        }
        //Create new connection
        //p2 - point on graph 
        public void AddConnection(Point p2, bool isYes, GraphNode graph)
        {
            //for connection we use two points 
            //the first point in current node
            Point p1 = new Point(); 
            //if isYes   the p1 is on the left side, else p2 on the right side
            if (isYes)
                p1.X = 0;
            else
                p1.X = this.Width;
            p1.Y = this.Height / 2;


            //check NodeYes not exists
            if ((isYes) && (NodeYes == null))
            {
                //save location
                EdgeYes = new Point[2] { p1, p2 };
                NodeYes = graph;
            }//Check NodeNo not exists

            else if ((!isYes) && (NodeNo == null))
            {
                //save location
                EdgeNo = new Point[2] { p1, p2 };
                NodeNo = graph;
            }


        }

        //method which Draw Edges of current node, 
        //color and LineWidth
        public void DrawEdges(Graphics g,Color color,int LineWidth)
        {
            {

                if (NodeNo != null)
                    DrawEdge(g, color, LineWidth, false);
                if (NodeYes != null)
                    DrawEdge(g, color, LineWidth, true);


            }
        }
        /// <summary>
        /// Method which draw edges of current node 
        /// 
        /// </summary>
        /// <param name="g"></param>  
        /// <param name="c"></param>
        /// <param name="LineWidth"></param>
        /// <param name="isYes"></param>
        public void DrawEdge(Graphics g, Color c, int LineWidth, bool isYes)
        {

            if (isYes && (NodeYes != null))
            {
                /*if EdgeYes is null, then calculate points of the edge*/
                if (EdgeYes == null)
                {
 
                    EdgeYes = new Point[]{new Point(0,0+(this.Height/2)),
                    new Point(0,NodeYes.Location.Y>this.Location.Y?0:(0+NodeYes.Height))};
                }
                if (NodeYes == this)  //draw edge using two points
                    DrawEdge(g, c, LineWidth, EdgeYes[0], EdgeYes[1], true);
                else
                    DrawEdge(g, c, LineWidth, EdgeYes[0], EdgeYes[1], false);

            }
            if (!isYes && (NodeNo != null))
            {   
                /*if EdgeYes is null, then calculate points of the edge*/
                if (EdgeNo == null)
                {
                    
                    EdgeNo = new Point[]{new Point(this.Width,0+(this.Height/2)),
                    new Point(this.Width,NodeNo.Location.Y>this.Location.Y?0:(0+NodeNo.Height))};
                }
                if (NodeNo == this)//draw edge using two points
                    DrawEdge(g, c, LineWidth, EdgeNo[0], EdgeNo[1], true);
                else
                    DrawEdge(g, c, LineWidth, EdgeNo[0], EdgeNo[1], false);

            }



        }
        /// <summary>
        /// Draw edge (Line) which connect two nodes, using 2 points
        /// </summary>
        /// <param name="g"></param>
        /// <param name="c"></param>
        /// <param name="LineWidth"></param>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <param name="isCurve">Using curve if edge connect node with itself </param>   
        public void DrawEdge(Graphics g, Color c, int LineWidth, Point p1, Point p2, bool isCurve)
        {
            Pen p = new Pen(c);

            //Draw BIG Arrow
            AdjustableArrowCap bigArrow = new AdjustableArrowCap(5, 5);


            // set width
            p.Width = LineWidth;
            p.CustomEndCap = bigArrow;
            //define isYes if point starts from the left side
            bool isYes = p1.X == 0;

            //p1 and p2 are relative coordinates
            //we have to convert it to absolute valuess
            p1.Offset(this.Location);
            if (isYes)
                p2.Offset(NodeYes.Location);
            else
                p2.Offset(NodeNo.Location);
            
            /*if is not curve*/
            if (!isCurve) //draw common line
                g.DrawLine(p, p1, p2);
            else//Draw curve we need three points 
                g.DrawCurve(p, new Point[] { p1, new Point(p1.X + 20 * (isYes ? -1 : 1), p1.Y - 10), p2 });
            p.Dispose();



        }
        //Override MouseEnter event
        protected override void OnMouseEnter(EventArgs e)
        {

         
            //Draw YES - NO near the Node
            Graphics g = this.Parent.CreateGraphics();
            g.DrawString("Yes", new Font(FontFamily.GenericSerif, 8, FontStyle.Underline), Brushes.Black,
               new Point(this.Location.X - 20, this.Location.Y));
            g.DrawString("No", new Font(FontFamily.GenericSerif, 8, FontStyle.Underline), Brushes.Black,
               new Point(this.Location.X + this.Width, this.Location.Y)); 
            base.OnMouseEnter(e);
        }
        //Override MouseLeave event
        protected override void OnMouseLeave(EventArgs e)
        {
            
            //Draw YES-NO near the Node using white color
            Graphics g = this.Parent.CreateGraphics();
           
            g.DrawString("Yes", new Font(FontFamily.GenericSerif, 8, FontStyle.Underline), Brushes.White ,
               new Point(this.Location.X - 20, this.Location.Y));
            g.DrawString("No", new Font(FontFamily.GenericSerif, 8, FontStyle.Underline), Brushes.White,
               new Point(this.Location.X + this.Width, this.Location.Y));
            //redraw edges
            DrawEdges(g, (this.Parent as GraphPanel).LineColor, (this.Parent as GraphPanel).LineWidth);

            base.OnMouseLeave(e);
        }

        #region Serialization
        //Simple Deserialization use only fields which we need
        protected GraphNode(SerializationInfo info, StreamingContext context) : this() {
           
            Type pGraphNode = typeof(GraphNode);

       
            typeof(Button).GetProperty("Name").SetValue(this,info.GetValue("Name", this.Name.GetType()),null);
            typeof(Button).GetProperty("Location").SetValue(this, info.GetValue("Location", this.Location.GetType()), null);
            
            typeof(Button).GetProperty("Text").SetValue(this, info.GetValue("Text", this.Text.GetType()), null);

            pGraphNode.GetProperty("isFirst").SetValue(this, info.GetValue("isFirst", typeof(Boolean)),null);
            pGraphNode.GetField("NodeYes").SetValue(this, info.GetValue("NodeYes",typeof(GraphNode)));
            pGraphNode.GetField("NodeNo").SetValue(this, info.GetValue("NodeNo", typeof(GraphNode)));
            pGraphNode.GetField("EdgeYes").SetValue(this, info.GetValue("EdgeYes", typeof(Point[])));
            pGraphNode.GetField("EdgeNo").SetValue(this, info.GetValue("EdgeNo", typeof(Point[])));
        
            
        
		}
        //Simple Serialization use only fields which we need
    	public virtual void GetObjectData(SerializationInfo info, StreamingContext context) {
            

			info.AddValue("Name",this.Name);
            info.AddValue("Location", this.Location);
            info.AddValue("Width", this.Width);
            info.AddValue("Text", this.Text);
            info.AddValue("isFirst", this.isFirst,typeof(Boolean));
            info.AddValue("NodeYes", this.NodeYes,typeof(GraphNode));
            info.AddValue("NodeNo", this.NodeNo, typeof(GraphNode));
            info.AddValue("EdgeYes", this.EdgeYes, typeof(Point[]));
            info.AddValue("EdgeNo", this.EdgeNo, typeof(Point[]));		

		}
		#endregion


    }
}

