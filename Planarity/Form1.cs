using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Planarity
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private Graph _graph;
        private bool _edging;
        private Vert _edgeFrom;
        private Point _edgeTo;

        private void button1_Click(object sender, EventArgs e)
        {
            label2.Text = _graph.IsPlanar() ? @"Планарен" : @"Не планарен";
            Refresh();
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            var g = e.Graphics;
            if (_edging)
            {
                g.DrawLine(Pens.Black, new Point(_edgeFrom.Position.X + Vert.R, _edgeFrom.Position.Y + Vert.R), _edgeTo);
            }
            foreach (var v in _graph.GetVerts())
            {
                foreach (var v2 in v.LinkedVers)
                {
                    g.DrawLine(Pens.Black, new Point(v.Position.X + Vert.R, v.Position.Y + Vert.R), new Point(v2.Position.X + Vert.R, v2.Position.Y + Vert.R));
                }
            }
            foreach (var v in _graph.GetVerts())
            {
                g.FillEllipse(Brushes.Red, v.Position.X, v.Position.Y, Vert.R, Vert.R);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            _graph = new Graph();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            _graph.New();
            label2.Text = @"Неизвестно";
            dataGridView1.Rows.Clear();
            Refresh();
        }

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            Refresh();
            var need = true;
            if (e.Button == MouseButtons.Left)
            {
                if (!_graph.GetVerts().Any())
                {
                    var v = _graph.AddVert(new Vert(new Point(e.X - Vert.R, e.Y - Vert.R)));
                    dataGridView1.Rows.Add(dataGridView1.RowCount, (_graph.GetVerts().Last().Position.X - 200) / 2, (-_graph.GetVerts().Last().Position.Y + 200) / 2);
                    _edgeFrom = v;
                    _edging = true;
                }
                else
                {
                    Vert connectTo = null;
                    foreach (var v in _graph.GetVerts())
                    {
                        if (Math.Pow(v.Position.X - (e.X - Vert.R), 2) + Math.Pow(v.Position.Y - (e.Y - Vert.R), 2) <= Vert.R * Vert.R)
                        {
                            need = false;
                            connectTo = v;
                        }
                    }
                    if (need)
                    {
                        connectTo = _graph.AddVert(new Vert(new Point(e.X - Vert.R, e.Y - Vert.R)));
                        dataGridView1.Rows.Add(dataGridView1.RowCount, (_graph.GetVerts().Last().Position.X - 200) / 2, (-_graph.GetVerts().Last().Position.Y + 200) / 2);
                        if (!_edging)
                        {
                            _edgeFrom = connectTo;
                            _edging = true;
                        }
                        else
                        {
                            if (!_edgeFrom.LinkedVers.Contains(connectTo))
                                _edgeFrom.AddAsLinked(connectTo);
                            if (!connectTo.LinkedVers.Contains(_edgeFrom))
                                connectTo.AddAsLinked(_edgeFrom);
                            _edging = false;
                        }
                    }
                    else
                    {
                        if (_edging)
                        {
                            if (!_edgeFrom.LinkedVers.Contains(connectTo))
                                _edgeFrom.AddAsLinked(connectTo);
                            if (!connectTo.LinkedVers.Contains(_edgeFrom))
                                connectTo.AddAsLinked(_edgeFrom);
                            _edging = false;
                        }
                        else
                        {
                            _edgeFrom = connectTo;
                            _edging = true;
                        }
                    }
                }
            }

            if (e.Button == MouseButtons.Right)
            {
                foreach (var v in _graph.GetVerts())
                {
                    if (Math.Pow(v.Position.X - (e.X - Vert.R), 2) + Math.Pow(v.Position.Y - (e.Y - Vert.R), 2) <= Vert.R * Vert.R)
                    {
                        dataGridView1.Rows.RemoveAt(_graph.GetVerts().IndexOf(v));
                        _graph.GetVerts().Remove(v);
                        foreach (var ver in _graph.GetVerts().Where(ver => ver.LinkedVers.Contains(v)))
                        {
                            ver.LinkedVers.Remove(v);
                        }
                        for (var i = 0; i < dataGridView1.RowCount - 1; i++)
                        {
                            dataGridView1.Rows[i].Cells[0].Value = i + 1;
                        }
                        break;
                    }
                }
            }
            Refresh();
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (!_edging) return;
            _edgeTo = new Point(e.X, e.Y);
            Refresh();
        }
    }
}
