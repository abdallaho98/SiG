namespace SiG
{
    partial class Form3
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.Surface = new System.Windows.Forms.Button();
            this.resultat = new System.Windows.Forms.Label();
            this.distance = new System.Windows.Forms.Button();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.union = new System.Windows.Forms.Button();
            this.intersection = new System.Windows.Forms.Button();
            this.distanceBetween = new System.Windows.Forms.Button();
            this.disjoint = new System.Windows.Forms.Button();
            this.difference = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(1, 163);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(260, 24);
            this.comboBox1.TabIndex = 0;
            // 
            // Surface
            // 
            this.Surface.Location = new System.Drawing.Point(309, 12);
            this.Surface.Name = "Surface";
            this.Surface.Size = new System.Drawing.Size(125, 30);
            this.Surface.TabIndex = 1;
            this.Surface.Text = "Surface";
            this.Surface.UseVisualStyleBackColor = true;
            this.Surface.Click += new System.EventHandler(this.Surface_Click);
            // 
            // resultat
            // 
            this.resultat.AutoSize = true;
            this.resultat.Location = new System.Drawing.Point(358, 308);
            this.resultat.Name = "resultat";
            this.resultat.Size = new System.Drawing.Size(64, 17);
            this.resultat.TabIndex = 2;
            this.resultat.Text = "Resultat:";
            // 
            // distance
            // 
            this.distance.Location = new System.Drawing.Point(309, 49);
            this.distance.Name = "distance";
            this.distance.Size = new System.Drawing.Size(125, 29);
            this.distance.TabIndex = 3;
            this.distance.Text = "Distance";
            this.distance.UseVisualStyleBackColor = true;
            this.distance.Click += new System.EventHandler(this.distance_Click);
            // 
            // comboBox2
            // 
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Location = new System.Drawing.Point(502, 163);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(286, 24);
            this.comboBox2.TabIndex = 4;
            // 
            // union
            // 
            this.union.Location = new System.Drawing.Point(309, 85);
            this.union.Name = "union";
            this.union.Size = new System.Drawing.Size(125, 29);
            this.union.TabIndex = 5;
            this.union.Text = "Union";
            this.union.UseVisualStyleBackColor = true;
            this.union.Click += new System.EventHandler(this.union_Click);
            // 
            // intersection
            // 
            this.intersection.Location = new System.Drawing.Point(309, 121);
            this.intersection.Name = "intersection";
            this.intersection.Size = new System.Drawing.Size(125, 30);
            this.intersection.TabIndex = 6;
            this.intersection.Text = "Intersection";
            this.intersection.UseVisualStyleBackColor = true;
            this.intersection.Click += new System.EventHandler(this.intersection_Click);
            // 
            // distanceBetween
            // 
            this.distanceBetween.Location = new System.Drawing.Point(309, 158);
            this.distanceBetween.Name = "distanceBetween";
            this.distanceBetween.Size = new System.Drawing.Size(125, 29);
            this.distanceBetween.TabIndex = 7;
            this.distanceBetween.Text = "Distance_Between";
            this.distanceBetween.UseVisualStyleBackColor = true;
            this.distanceBetween.Click += new System.EventHandler(this.distanceBetween_Click);
            // 
            // disjoint
            // 
            this.disjoint.Location = new System.Drawing.Point(309, 193);
            this.disjoint.Name = "disjoint";
            this.disjoint.Size = new System.Drawing.Size(125, 27);
            this.disjoint.TabIndex = 8;
            this.disjoint.Text = "Disjoint";
            this.disjoint.UseVisualStyleBackColor = true;
            this.disjoint.Click += new System.EventHandler(this.disjoint_Click);
            // 
            // difference
            // 
            this.difference.Location = new System.Drawing.Point(309, 227);
            this.difference.Name = "difference";
            this.difference.Size = new System.Drawing.Size(125, 27);
            this.difference.TabIndex = 9;
            this.difference.Text = "Difference";
            this.difference.UseVisualStyleBackColor = true;
            this.difference.Click += new System.EventHandler(this.difference_Click);
            // 
            // Form3
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.difference);
            this.Controls.Add(this.disjoint);
            this.Controls.Add(this.distanceBetween);
            this.Controls.Add(this.intersection);
            this.Controls.Add(this.union);
            this.Controls.Add(this.comboBox2);
            this.Controls.Add(this.distance);
            this.Controls.Add(this.resultat);
            this.Controls.Add(this.Surface);
            this.Controls.Add(this.comboBox1);
            this.Name = "Form3";
            this.Text = "Form3";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Button Surface;
        private System.Windows.Forms.Label resultat;
        private System.Windows.Forms.Button distance;
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.Button union;
        private System.Windows.Forms.Button intersection;
        private System.Windows.Forms.Button distanceBetween;
        private System.Windows.Forms.Button disjoint;
        private System.Windows.Forms.Button difference;
    }
}