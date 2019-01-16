using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DotSpatial.Controls;
using DotSpatial.Data;
using DotSpatial.Symbology;
using DotSpatial.Topology;

namespace SiG
{
    public partial class Form3 : Form
    {
        private DotSpatial.Controls.IMapLayerCollection maplayers;
        private List<IFeature> geometries;
        private double scale;
        public Form3(DotSpatial.Controls.IMapLayerCollection layers, double scale)
        {
            maplayers = layers;
            this.scale = scale;
            InitializeComponent();
            geometries = new List<IFeature>();
            List<string> lists = new List<string>();
            for (int i = 0; i < layers.Count; i++) {
                switch (layers[i].GetType().ToString()) {
                    case "DotSpatial.Controls.MapPointLayer":
                        for (int j = 0; j < ((MapPointLayer)layers[i]).FeatureSet.Features.Count; j++) {
                            lists.Add(layers[i].LegendText + " "+ ((MapPointLayer)layers[i]).FeatureSet.Features[j].Fid);
                            geometries.Add(((MapPointLayer)layers[i]).FeatureSet.Features[j]);
                        }
                            break;
                    case "DotSpatial.Controls.MapLineLayer":
                        for (int j = 0; j < ((MapLineLayer)layers[i]).FeatureSet.Features.Count; j++)
                        {
                            lists.Add(layers[i].LegendText + " " + ((MapLineLayer)layers[i]).FeatureSet.Features[j].Fid);
                            geometries.Add(((MapLineLayer)layers[i]).FeatureSet.Features[j]);
                        }
                        break;
                    case "DotSpatial.Controls.MapPolygonLayer":
                        for (int j = 0; j < ((MapPolygonLayer)layers[i]).FeatureSet.Features.Count; j++)
                        {
                            lists.Add(layers[i].LegendText + " " + ((MapPolygonLayer)layers[i]).FeatureSet.Features[j].Fid);
                            geometries.Add(((MapPolygonLayer)layers[i]).FeatureSet.Features[j]);
                        }
                        break;
                }
               
            }
            comboBox1.DataSource = lists;
            comboBox2.DataSource = lists.CloneList<string>();
        }

        private void Surface_Click(object sender, EventArgs e)
        {
            resultat.Text = (geometries[comboBox1.SelectedIndex].Area() * scale).ToString();
        }

        private void distance_Click(object sender, EventArgs e)
        {
            double distance = 0;
            for(int i = 1; i < geometries[comboBox1.SelectedIndex].Coordinates.Count; i++)
            {
                distance += geometries[comboBox1.SelectedIndex].Coordinates[i].Distance(geometries[comboBox1.SelectedIndex].Coordinates[i - 1]);
            }
            resultat.Text = (distance * scale).ToString();
        }

        private void union_Click(object sender, EventArgs e)
        {
            try {
                resultat.Text = (geometries[comboBox1.SelectedIndex].Union(geometries[comboBox2.SelectedIndex]).Area() * scale).ToString();
                } catch (Exception ee) { MessageBox.Show(ee.Message); }
           
        }

        private void intersection_Click(object sender, EventArgs e)
        {
            try   {resultat.Text = geometries[comboBox1.SelectedIndex].Intersection(geometries[comboBox2.SelectedIndex]) != null ? geometries[comboBox1.SelectedIndex].Intersection(geometries[comboBox2.SelectedIndex]).Area().ToString() : "0"; } catch (Exception ee) { MessageBox.Show(ee.Message); }
           
        }

        private void distanceBetween_Click(object sender, EventArgs e)
        {
            try { resultat.Text = (geometries[comboBox1.SelectedIndex].Distance(geometries[comboBox2.SelectedIndex]) * scale).ToString();  } catch (Exception ee) { MessageBox.Show(ee.Message); }
          
        }

        private void disjoint_Click(object sender, EventArgs e)
        {
            try { resultat.Text = geometries[comboBox1.SelectedIndex].Disjoint(geometries[comboBox2.SelectedIndex]).ToString();  } catch (Exception ee) { MessageBox.Show(ee.Message); }
           
        }

        private void difference_Click(object sender, EventArgs e)
        {
            try { resultat.Text = (geometries[comboBox1.SelectedIndex].Difference(geometries[comboBox2.SelectedIndex]).Area() * scale).ToString(); } catch (Exception ee) { MessageBox.Show(ee.Message); }
         
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try { resultat.Text = geometries[comboBox1.SelectedIndex].Crosses(geometries[comboBox2.SelectedIndex]).ToString(); } catch (Exception ee) { MessageBox.Show(ee.Message); }

        }
    }
}
