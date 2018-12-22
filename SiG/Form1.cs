using DotSpatial.Controls;
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
    public partial class Form1 : Form
    {
        private bool point = false, polyline = false, polygone = false;
        private FeatureSet _markers, _gones, _lines;
        private DataTable dataTable;
        private LineString lineString;
        private MapPointLayer _markerLayer;
        private LinearRing linearRing;
        private MapPolygonLayer _goneLayer;
        private MapLineLayer _lineLayer;
        private Timer timer;
        private List<Coordinate> Corr;
        private int ID;
        private DotSpatial.Topology.Polygon mPolygone;

        public Form1()
        {

            InitializeComponent();
            timer = new Timer();
            timer.Tick += new EventHandler(GETXY);
            timer.Interval = 500;

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
                MessageBox.Show("new age");
                Corr = new List<Coordinate>();
                ID++;
                _gones.InitializeVertices();
                map1.Refresh();
            }
            if (((MouseEventArgs)e).Button == MouseButtons.Right && polyline)
            {
                MessageBox.Show("new age");
                Corr = new List<Coordinate>();
                ID++;
                _lines.InitializeVertices();
                map1.Refresh();
             }

            // Intercept only the right click for adding markers
            if (((MouseEventArgs)e).Button != MouseButtons.Left) return;
            if (point)
            {
                // Get the geographic location that was clicked
                Coordinate c = map1.PixelToProj(new System.Drawing.Point(e.X, e.Y));
                
                // Add the new coordinate as a "point" to the point featureset
                _markers.AddFeature(new DotSpatial.Topology.Point(c)).DataRow["ID"] = ID;
                ID++;
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
                    _lines.RemoveShapeAt(ID);
                    _lines.AddFeature(lineString as IGeometry).DataRow["ID"] = ID.ToString();
                    // Drawing will take place from a bitmap buffer, so if data is updated,
                    // we need to tell the map to refresh the buffer 
                }
                _lines.InitializeVertices();
                map1.ResetBuffer();
                map1.Refresh();
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
                    _gones.RemoveShapeAt(ID);
                    _gones.AddFeature(mPolygone as IGeometry).DataRow["ID"] = ID.ToString();
                    // Drawing will take place from a bitmap buffer, so if data is updated,
                    // we need to tell the map to refresh the buffer 
                }
                _gones.InitializeVertices();
                map1.ResetBuffer();
                map1.Refresh();

            }
           
        }

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

            // Handle mouse up event on the map
            map1.MouseDoubleClick += map1_Load;

            // The FeatureSet starts with no data; be sure to set it to the point featuretype
            _markers = new FeatureSet(FeatureType.Point);
            dataTable = _markers.DataTable;
            ID = 0;
            dataTable.Columns.Add("ID");
            // The MapPointLayer controls the drawing of the marker features
            _markerLayer = new MapPointLayer(_markers);
            // The Symbolizer controls what the points look like
            _markerLayer.Symbolizer = new PointSymbolizer(Color.Black, DotSpatial.Symbology.PointShape.Ellipse, 10);

            _markerLayer.LegendText = "Points";
            // A drawing layer draws on top of data layers, but is still georeferenced.
            map1.Layers.Add(_markerLayer);
        }

        private void polyLineToolStripMenuItem_Click(object sender, EventArgs e)
        {
            point = false;
            polygone = false;
            polyline = true;
            // Enable left click panning and mouse wheel zooming
            map1.FunctionMode = FunctionMode.Pan;

            // Handle mouse up event on the map
            map1.MouseDoubleClick += map1_Load;

            // The FeatureSet starts with no data; be sure to set it to the polygone featuretype
            _lines = new FeatureSet(FeatureType.Line);

            // The MapPolygone controls the drawing of the marker features
            _lineLayer = new MapLineLayer(_lines);
            ID = 0;
            Corr = new List<Coordinate>();
            _lines.DataTable.Columns.Add("ID", typeof(string));

            _lineLayer.LegendText = "Line";
            // A drawing layer draws on top of data layers, but is still georeferenced.
            map1.Layers.Add(_lineLayer);
        }

        private void polyGoneToolStripMenuItem_Click(object sender, EventArgs e)
        {
            point = false;
            polygone = true;
            polyline = false;
            // Enable left click panning and mouse wheel zooming
            map1.FunctionMode = FunctionMode.Pan;

            // Handle mouse up event on the map
            map1.MouseDoubleClick += map1_Load;

            // The FeatureSet starts with no data; be sure to set it to the polygone featuretype
            _gones = new FeatureSet(FeatureType.Polygon);

            // The MapPolygone controls the drawing of the marker features
            _goneLayer = new MapPolygonLayer(_gones);
            ID = 0;
            Corr = new List<Coordinate>();
            _gones.DataTable.Columns.Add("ID", typeof(string));

            // The Symbolizer controls what the points look like
            _goneLayer.Symbolizer = new PolygonSymbolizer(Color.Blue);

            _goneLayer.LegendText = "Surface";
            // A drawing layer draws on top of data layers, but is still georeferenced.
            map1.Layers.Add(_goneLayer);
        }
    }

    
}
