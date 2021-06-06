// ManagedUI (Managed User Interface)
// A managed user interface framework for .net desktop applications.
// 
// Copyright © Alaa Ibrahim Hadid 2021
//
// This program is free software; you can redistribute it and/or modify 
// it under the terms of the GNU Lesser General Public License as published 
// by the Free Software Foundation; either version 3 of the License, 
// or (at your option) any later version.
//
// This program is distributed in the hope that it will be useful, but 
// WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY
// or FITNESS FOR A PARTICULAR PURPOSE.See the GNU Lesser General Public 
// License for more details.
//
// You should have received a copy of the GNU Lesser General Public License 
// along with this library; if not, write to the Free Software Foundation, 
// Inc., 59 Temple Place, Suite 330, Boston, MA 02111-1307 USA
// 
// Author email: mailto:alaahadidfreeware@gmail.com
//
using System;
using System.Drawing;
using System.Windows.Forms;

namespace ManagedUI
{
    /// <summary>
    /// The drag panel for mtc pages
    /// </summary>
    public partial class ManagedTabControlDragPanel : Control
    {
        /// <summary>
        /// The drag panel for mtc pages
        /// </summary>
        public ManagedTabControlDragPanel()
        {
            InitializeComponent();

            ControlStyles flag = ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint;
            this.SetStyle(flag, true);
        }
        private bool DraggingMODE;
        private Bitmap cnt_p;

        private int s_h_i = -1;
        private int s_5_w_h = 70;
        private int s_1_3_h = 30;
        private int s_2_4_w = 30;

        private int s_1_x;
        private int s_1_y;
        private int s_2_x;
        private int s_2_y;
        private int s_3_x;
        private int s_3_y;
        private int s_4_x;
        private int s_4_y;
        private int s_5_x;
        private int s_5_y;

        /// <summary>
        /// Color for the drop rects when highlighted
        /// </summary>
        public Color s_h_c = Color.SkyBlue;
        /// <summary>
        /// Color for the drop rects
        /// </summary>
        public Color s_c = Color.Silver;
        /// <summary>
        /// Color for the drop rects when selected
        /// </summary>
        public Color s_s_c = Color.WhiteSmoke;

        //private bool isOverClose;
        /// <summary>
        /// Raised when a tab dropped
        /// </summary>
        public event EventHandler<MTCDropRequestEventArgs> TabDrop;
        /// <summary>
        /// Enter a drag situation
        /// </summary>
        /// <param name="isDragging"></param>
        /// <param name="cnt_p"></param>
        public void EnterDrag(bool isDragging, Bitmap cnt_p)
        {
            if (DraggingMODE == isDragging)
                return;
            DraggingMODE = isDragging;
            this.cnt_p = cnt_p;

            if (DraggingMODE)
            {
                // But first calculate the coordinates
                s_1_x = (this.Width / 2) - (s_5_w_h / 2);
                s_1_y = (this.Height / 2) - ((s_5_w_h + (s_1_3_h * 2)) / 2);
                s_2_x = s_1_x - s_2_4_w;
                s_2_y = s_1_y + s_1_3_h;
                s_3_x = s_1_x;
                s_3_y = s_1_y + s_5_w_h + s_1_3_h;
                s_4_x = s_2_x + s_5_w_h + s_2_4_w;
                s_4_y = s_2_y;
                s_5_x = s_1_x;
                s_5_y = s_1_y + s_1_3_h;

                this.Invalidate();
            }
        }
        /// <summary>
        /// OnPaint
        /// </summary>
        /// <param name="pe"></param>
        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
            pe.Graphics.Clear(this.BackColor);
            if (!DraggingMODE)
            {
                //    if (isOverClose)
                //       pe.Graphics.DrawImage(Properties.Resources.cross, new Point(this.Width - Properties.Resources.cross.Width - 2, 5));
                //   else
                //        pe.Graphics.DrawImage(Properties.Resources.cross_black, this.Width - Properties.Resources.cross.Width - 2, 5, Properties.Resources.cross.Width, Properties.Resources.cross.Height);
                return;
            }

            if (cnt_p != null)
                pe.Graphics.DrawImage(cnt_p, new Point(0, 0));

            // draw the rectangles
            if (s_h_i == 1)
            {
                pe.Graphics.FillRectangle(new SolidBrush(s_h_c), new Rectangle(s_1_x, s_1_y, s_5_w_h, s_1_3_h));
            }
            else
            {
                pe.Graphics.FillRectangle(new SolidBrush(s_c), new Rectangle(s_1_x, s_1_y, s_5_w_h, s_1_3_h));
            }

            if (s_h_i == 2)
            {
                pe.Graphics.FillRectangle(new SolidBrush(s_h_c), new Rectangle(s_2_x, s_2_y, s_2_4_w, s_5_w_h));
            }
            else
            {
                pe.Graphics.FillRectangle(new SolidBrush(s_c), new Rectangle(s_2_x, s_2_y, s_2_4_w, s_5_w_h));
            }

            if (s_h_i == 3)
            {
                pe.Graphics.FillRectangle(new SolidBrush(s_h_c), new Rectangle(s_3_x, s_3_y, s_5_w_h, s_1_3_h));
            }
            else
            {
                pe.Graphics.FillRectangle(new SolidBrush(s_c), new Rectangle(s_3_x, s_3_y, s_5_w_h, s_1_3_h));
            }

            if (s_h_i == 4)
            {
                pe.Graphics.FillRectangle(new SolidBrush(s_h_c), new Rectangle(s_4_x, s_4_y, s_2_4_w, s_5_w_h));
            }
            else
            {
                pe.Graphics.FillRectangle(new SolidBrush(s_c), new Rectangle(s_4_x, s_4_y, s_2_4_w, s_5_w_h));
            }

            if (s_h_i == 5)
            {
                pe.Graphics.FillRectangle(new SolidBrush(s_h_c), new Rectangle(s_5_x, s_5_y, s_5_w_h, s_5_w_h));
            }
            else
            {
                pe.Graphics.FillRectangle(new SolidBrush(s_c), new Rectangle(s_5_x, s_5_y, s_5_w_h, s_5_w_h));
            }

            pe.Graphics.DrawRectangle(new Pen(s_s_c), new Rectangle(s_1_x, s_1_y, s_5_w_h, s_1_3_h));
            pe.Graphics.DrawRectangle(new Pen(s_s_c), new Rectangle(s_2_x, s_2_y, s_2_4_w, s_5_w_h));
            pe.Graphics.DrawRectangle(new Pen(s_s_c), new Rectangle(s_3_x, s_3_y, s_5_w_h, s_1_3_h));
            pe.Graphics.DrawRectangle(new Pen(s_s_c), new Rectangle(s_4_x, s_4_y, s_2_4_w, s_5_w_h));
            pe.Graphics.DrawRectangle(new Pen(s_s_c), new Rectangle(s_5_x, s_5_y, s_5_w_h, s_5_w_h));
        }
        /// <summary>
        /// OnMouseMove
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (!DraggingMODE)
            {
                //  isOverClose = (e.Y < 5 + Properties.Resources.cross.Height) && (e.X >= this.Width - Properties.Resources.cross.Width - 2);
                //   Invalidate();
                return;
            }
            // if (!DraggingMODE)
            //     return;
            if (e.Button == MouseButtons.Left)
            {
                if (e.X >= s_2_x)
                {
                    if (e.Y >= s_1_y)
                    {
                        // Now determine the square we need
                        if (e.Y < s_2_y && e.X < s_4_x)
                        {
                            if (s_h_i != 1)
                            {
                                s_h_i = 1;
                                Invalidate();
                            }
                            return;
                        }

                        if (e.Y < s_3_y && e.Y > s_2_y && e.X < s_1_x)
                        {
                            if (s_h_i != 2)
                            {
                                s_h_i = 2;
                                Invalidate();
                            }
                            return;
                        }

                        if (e.Y < s_3_y + s_1_3_h && e.Y >= s_3_y && e.X < s_4_x && e.X >= s_3_x)
                        {
                            if (s_h_i != 3)
                            {
                                s_h_i = 3;
                                Invalidate();
                            }
                            return;
                        }

                        if (e.Y < s_3_y && e.Y >= s_4_y && e.X < s_4_x + s_2_4_w && e.X >= s_4_x)
                        {
                            if (s_h_i != 4)
                            {
                                s_h_i = 4;
                                Invalidate();
                            }
                            return;
                        }

                        if (e.Y < s_5_y + s_5_w_h && e.Y >= s_5_y && e.X < s_4_x && e.X >= s_5_x)
                        {
                            if (s_h_i != 5)
                            {
                                s_h_i = 5;
                                Invalidate();
                            }
                            return;
                        }
                    }
                }
                // Reached here means no square touches !!
                s_h_i = 0;
            }
            else
            {
                TabDrop?.Invoke(this, new MTCDropRequestEventArgs(-1));
                Invalidate();
            }
        }
        /// <summary>
        /// OnMouseUp
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            if (!DraggingMODE)
            {
                return;
            }
            if (s_h_i > 0)
                TabDrop?.Invoke(this, new MTCDropRequestEventArgs(s_h_i));
        }
        /// <summary>
        /// OnMouseEnter
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
        }
        /// <summary>
        /// OnMouseLeave
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            Invalidate();
        }
        /// <summary>
        /// OnResize
        /// </summary>
        /// <param name="e"></param>
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            Invalidate();
        }
    }
}
