﻿using DotSpatial.Controls;
using DotSpatial.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using DotSpatial.Symbology;
using DotSpatial.Topology;



namespace SiG
{
    public partial class Form1 : Form , Interface1 
    {
        private bool point = false, polyline = false, polygone = false;
        private FeatureSet _markers, _gones, _lines;
        private DataTable dataTable;
        private LineString lineString;
        private MapPointLayer _markerLayer;
        private LinearRing linearRing;
        private DataTable dataTableWorking;
        private MapPolygonLayer _goneLayer;
        private MapLineLayer _lineLayer;
        private Timer timer;
        private Form2 entreType;
        private Form3 topology;
        private Form4 sca;
        private List<Coordinate> Corr;
        private int ID;
        private IFeature mapPolyGonelayer,mapPolylinelayer;
        private DotSpatial.Topology.Polygon mPolygone;
        private double scale;
        private double coef;

        public Form1()
        {
            entreType = new Form2(this);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            this.WindowState = FormWindowState.Maximized;
            InitializeComponent();
            coef = 1110000;
            timer = new Timer();
            timer.Tick += new EventHandler(GETXY);
            timer.Interval = 500;
            attributeTable.AllowUserToAddRows = false;
            label2.Text = "1";
            label4.Text = "1";

            
            
        }

        private void mousePos(object sender, EventArgs e)
        {
            timer.Start();
        }

        private void mouseExit(object sender, EventArgs e)
        {
            timer.Stop();
        }
        private void GETXY(object o, EventArgs e)
        {
            Coordinate c = map1.PixelToProj(new System.Drawing.Point(MousePosition.X, MousePosition.Y));
            xPos.Text = c.X.ToString();
            yPos.Text = c.Y.ToString();
            scale = (map1.ViewExtents.Width) * coef / map1.Extent.Width;
            label4.Text = Math.Ceiling(scale).ToString();
        }

        private void openRasterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            map1.AddImageLayer();
            map1.FunctionMode = DotSpatial.Controls.FunctionMode.Pan;
            map1.MouseEnter += mousePos;
            map1.MouseLeave += mouseExit;
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
         
        }


        private void map1_Load(object sender, MouseEventArgs e)
        {

            if (((MouseEventArgs)e).Button == MouseButtons.Right && polygone) {
                MessageBox.Show("New Polygone");
                linearRing.Coordinates.Add(linearRing.Coordinates[0]);
                Corr = new List<Coordinate>();
                ID = _goneLayer.DataSet.Features.Count;
                mapPolyGonelayer = null;
                _gones.InitializeVertices();
            }
            if (((MouseEventArgs)e).Button == MouseButtons.Right && polyline)
            {
                MessageBox.Show("New PolyLine");
                Corr = new List<Coordinate>();
                ID = _lineLayer.DataSet.Features.Count;
                mapPolylinelayer = null;
                _lines.InitializeVertices();
             }

            // Intercept only the right click for adding markers
            if (((MouseEventArgs)e).Button != MouseButtons.Left) return;
            if (point)
            {
                // Get the geographic location that was clicked
                Coordinate c = map1.PixelToProj(new System.Drawing.Point(e.X, e.Y));

                // Add the new coordinate as a "point" to the point featureset
                IFeature pointfeature = _markerLayer.DataSet.AddFeature(new DotSpatial.Topology.Point(c));
                pointfeature.DataRow["ID"] = ID ;
                pointfeature.DataRow["X"] = c.X;
                pointfeature.DataRow["Y"] = c.Y;
                ID = _markerLayer.DataSet.Features.Count;
                _markers.InitializeVertices();
                // Drawing will take place from a bitmap buffer, so if data is updated,
                // we need to tell the map to refresh the buffer 
                map1.MapFrame.Invalidate();
            }
            else if (polyline) {
                Coordinate c = map1.PixelToProj(new System.Drawing.Point(e.X, e.Y));
                if (Corr.Count == 0)
                {
                    lineString = new LineString(Corr);
                    lineString.Coordinates.Add(c);
                }
                else
                {
                    lineString.Coordinates.Add(c);
                    if (mapPolylinelayer == null) {
                        mapPolylinelayer = _lineLayer.DataSet.AddFeature(lineString as IGeometry);
                        mapPolylinelayer.DataRow["ID"] = ID;
                    }
                    
                }
                _lines.InitializeVertices();
                map1.ResetBuffer();
            } else {
                // Get the geographic location that was clicked
                Coordinate c = map1.PixelToProj(new System.Drawing.Point(e.X, e.Y));


                if (Corr.Count == 0)
                {
                    linearRing = new LinearRing(Corr);
                    linearRing.Coordinates.Add(c);
                    mPolygone = new DotSpatial.Topology.Polygon(linearRing);
                }
                else {
                    linearRing.Coordinates.Add(c);
                    if (mapPolyGonelayer == null) {
                        mapPolyGonelayer = _goneLayer.DataSet.AddFeature(mPolygone as IGeometry);
                        mapPolyGonelayer.DataRow["ID"] = ID;
                    } 
                }
                _gones.InitializeVertices();
                map1.ResetBuffer();
            }
           
        }

        private void showAttributeTableToolStripMenuItem_Click(object sender, EventArgs e)
        {
         
            foreach (Layer lyr in map1.Layers ) {
                bool isThere = false;
                for (int i = 0; i < showAttributeTableToolStripMenuItem.DropDownItems.Count; i++) {
                    if (showAttributeTableToolStripMenuItem.DropDownItems[i].Text.Equals(lyr.LegendText)) isThere = true;
                }
                if(!isThere)showAttributeTableToolStripMenuItem.DropDownItems.Add(lyr.LegendText);
            }

            foreach (ToolStripDropDownItem dropDownItem in showAttributeTableToolStripMenuItem.DropDownItems) {
                dropDownItem.Click += new EventHandler(ShowTable);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            entreType.ShowDialog();
        }

        private void ShowTable(object sender, EventArgs e)
        {
            AddComlumn.Visible = true;
            for (int i = 0; i < map1.Layers.Count; i++) {
                if (map1.Layers[i].LegendText.Trim().Equals(((ToolStripMenuItem)sender).Text.Trim())) {
                    switch (map1.Layers[i].GetType().ToString().Trim()) {
                        case "DotSpatial.Controls.MapPointLayer":
                            dataTableWorking = ((MapPointLayer)map1.GetLayers()[i]).FeatureSet.DataTable;
                            attributeTable.DataSource = ((MapPointLayer)map1.GetLayers()[i]).FeatureSet.DataTable;
                            break;
                        case "DotSpatial.Controls.MapLineLayer":
                            dataTableWorking = ((MapLineLayer)map1.GetLayers()[i]).FeatureSet.DataTable; ;
                            attributeTable.DataSource = ((MapLineLayer)map1.GetLayers()[i]).FeatureSet.DataTable;
                            break;
                        case "DotSpatial.Controls.MapPolygonLayer":
                            dataTableWorking = ((MapPolygonLayer)map1.GetLayers()[i]).FeatureSet.DataTable; ;
                            attributeTable.DataSource = ((MapPolygonLayer)map1.GetLayers()[i]).FeatureSet.DataTable;
                            break;
                    }
                   
                }
            }
        }

        private void topologyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //MessageBox.Show(  map1.Layers[1].LegendText.ToString()+" "+((MapPolygonLayer)map1.Layers[1]).FeatureSet.Features[0].Area().ToString());
            topology = new Form3(map1.Layers,scale);
            topology.ShowDialog();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            map1.FunctionMode = FunctionMode.Info;
        }

        private void changeScaleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            sca = new Form4(this);
            sca.ShowDialog();
        }
        public void setCoef(int co) { sca.Close(); coef = co * (map1.Extent.Width) / map1.ViewExtents.Width; }

        private void openShapeFileToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            map1.AddLayer();
            map1.FunctionMode = FunctionMode.Pan;
            map1.MouseEnter += mousePos;
            map1.MouseLeave += mouseExit;
           
            
        }

        private void pointToolStripMenuItem_Click(object sender, EventArgs e)
        {
            point = true;
            polygone = false;
            polyline = false;
            // Enable left click panning and mouse wheel zooming
            map1.FunctionMode = FunctionMode.Pan;


            // The FeatureSet starts with no data; be sure to set it to the point featuretype
            _markers = new FeatureSet(FeatureType.Point);
            dataTable = _markers.DataTable;
            ID = 0;
            dataTable.Columns.Add("ID");
            dataTable.Columns.Add("X");
            dataTable.Columns.Add("Y");
            // The MapPointLayer controls the drawing of the marker features
            _markerLayer = new MapPointLayer(_markers);
            // The Symbolizer controls what the points look like
            _markerLayer.Symbolizer = new PointSymbolizer(Color.Black, DotSpatial.Symbology.PointShape.Ellipse, 10);

            _markerLayer.LegendText = "Points"+_markerLayer.GetHashCode();
            // A drawing layer draws on top of data layers, but is still georeferenced.
            map1.Layers.Add(_markerLayer);
        }

        private void polyLineToolStripMenuItem_Click(object sender, EventArgs e)
        {


            MessageBox.Show("Double Right Click to Finnish this line and create another Line");
            point = false;
            polygone = false;
            polyline = true;
            // Enable left click panning and mouse wheel zooming
            map1.FunctionMode = FunctionMode.Pan;

            mapPolylinelayer = null;

            // The FeatureSet starts with no data; be sure to set it to the polygone featuretype
            _lines = new FeatureSet(FeatureType.Line);

            // The MapPolygone controls the drawing of the marker features
            _lineLayer = new MapLineLayer(_lines);
            ID = 0;
            Corr = new List<Coordinate>();
            _lines.DataTable.Columns.Add("ID");

            _lineLayer.LegendText = "Line" + _lineLayer.GetHashCode();
            // A drawing layer draws on top of data layers, but is still georeferenced.
            map1.Layers.Add(_lineLayer);
        }

        private void polyGoneToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Double Right Click to Finnish this polygones and create another Polygone");
            point = false;
            polygone = true;
            polyline = false;
            // Enable left click panning and mouse wheel zooming
            map1.FunctionMode = FunctionMode.Pan;
            mapPolyGonelayer = null;
     

            // The FeatureSet starts with no data; be sure to set it to the polygone featuretype
            _gones = new FeatureSet(FeatureType.Polygon);

            // The MapPolygone controls the drawing of the marker features
            _goneLayer = new MapPolygonLayer(_gones);
            ID = 0;
            Corr = new List<Coordinate>();
            _goneLayer.DataSet.DataTable.Columns.Add("ID", typeof(string));
            // The Symbolizer controls what the points look like
            _goneLayer.Symbolizer = new PolygonSymbolizer(Color.Blue);

            _goneLayer.LegendText = "Surface" + _goneLayer.GetHashCode();
            // A drawing layer draws on top of data layers, but is still georeferenced.
            map1.Layers.Add(_goneLayer);
        }

        public void SendData(string name, string type)
        {
            entreType.Close();
            switch (type) {
                case "String":
                    dataTableWorking.Columns.Add(name, typeof(string));
                    break;
                case "Int":
                    dataTableWorking.Columns.Add(name, typeof(int));
                    break;
                case "Double":
                    dataTableWorking.Columns.Add(name, typeof(double));
                    break;
            } 
            map1.Refresh();
            legend1.Refresh();
        }
    }

    
}
