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
        private FeatureSet _markers,_gones;
        private DataTable dataTable;
        private MapPointLayer _markerLayer;
        private LinearRing linearRing;
        private MapPolygonLayer _goneLayer;
        private List<Coordinate> Corr;
        private int ID;
        private DotSpatial.Topology.Polygon mPolygone; 

        public Form1()
        {
            InitializeComponent();
            appManager1.LoadExtensions();
        }

        private void openRasterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            map1.AddImageLayer();
            map1.FunctionMode = DotSpatial.Controls.FunctionMode.Pan;
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

            }
           
        }

        private void openShapeFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            map1.AddLayer();
            map1.FunctionMode = FunctionMode.Pan;
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
