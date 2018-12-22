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
        private MapPointLayer _markerLayer;
        private LinearRing linearRing;
        private MapPolygonLayer _goneLayer;
        private List<Coordinate> Corr;
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
           
            // Intercept only the right click for adding markers
            if (((MouseEventArgs)e).Button != MouseButtons.Left) return;
            if (point)
            {
                // Get the geographic location that was clicked
                Coordinate c = map1.PixelToProj(new System.Drawing.Point(e.X, e.Y));
                
                // Add the new coordinate as a "point" to the point featureset
                _markers.AddFeature(new DotSpatial.Topology.Point(c));

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
                    _gones.AddFeature(mPolygone as IGeometry);
                    // Drawing will take place from a bitmap buffer, so if data is updated,
                    // we need to tell the map to refresh the buffer 
                    map1.MapFrame.Invalidate();
                }
              
               
            }
           
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

            Corr = new List<Coordinate>();

            // The Symbolizer controls what the points look like
            _goneLayer.Symbolizer = new PolygonSymbolizer(Color.Blue);

            _goneLayer.LegendText = "Surface";
            // A drawing layer draws on top of data layers, but is still georeferenced.
            map1.Layers.Add(_goneLayer);
        }
    }

    
}
