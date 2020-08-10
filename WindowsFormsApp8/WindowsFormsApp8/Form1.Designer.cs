namespace WindowsFormsApp8
{
    partial class Form1
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series3 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series4 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.archivoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.analisisTemporalToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.informeGeneralToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.informeEntreMarcadoresToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.analisisFrecuencialToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.transformadaRapidaDeFurierToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.transformadaOnditaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.Barra_Tiempo = new System.Windows.Forms.TrackBar();
            this.Marcador_Inicial = new System.Windows.Forms.TrackBar();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.Marcador_Final = new System.Windows.Forms.TrackBar();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.Recortar = new System.Windows.Forms.Button();
            this.Limpiar = new System.Windows.Forms.Button();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Barra_Tiempo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Marcador_Inicial)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Marcador_Final)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.archivoToolStripMenuItem,
            this.analisisTemporalToolStripMenuItem,
            this.analisisFrecuencialToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(904, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // archivoToolStripMenuItem
            // 
            this.archivoToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cToolStripMenuItem});
            this.archivoToolStripMenuItem.Name = "archivoToolStripMenuItem";
            this.archivoToolStripMenuItem.Size = new System.Drawing.Size(60, 20);
            this.archivoToolStripMenuItem.Text = "Archivo";
            // 
            // cToolStripMenuItem
            // 
            this.cToolStripMenuItem.Name = "cToolStripMenuItem";
            this.cToolStripMenuItem.Size = new System.Drawing.Size(109, 22);
            this.cToolStripMenuItem.Text = "Cargar";
            this.cToolStripMenuItem.Click += new System.EventHandler(this.cToolStripMenuItem_Click);
            // 
            // analisisTemporalToolStripMenuItem
            // 
            this.analisisTemporalToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.informeGeneralToolStripMenuItem,
            this.informeEntreMarcadoresToolStripMenuItem});
            this.analisisTemporalToolStripMenuItem.Name = "analisisTemporalToolStripMenuItem";
            this.analisisTemporalToolStripMenuItem.Size = new System.Drawing.Size(111, 20);
            this.analisisTemporalToolStripMenuItem.Text = "Análisis Temporal";
            // 
            // informeGeneralToolStripMenuItem
            // 
            this.informeGeneralToolStripMenuItem.Name = "informeGeneralToolStripMenuItem";
            this.informeGeneralToolStripMenuItem.Size = new System.Drawing.Size(211, 22);
            this.informeGeneralToolStripMenuItem.Text = "Informe general";
            this.informeGeneralToolStripMenuItem.Click += new System.EventHandler(this.informeGeneralToolStripMenuItem_Click);
            // 
            // informeEntreMarcadoresToolStripMenuItem
            // 
            this.informeEntreMarcadoresToolStripMenuItem.Name = "informeEntreMarcadoresToolStripMenuItem";
            this.informeEntreMarcadoresToolStripMenuItem.Size = new System.Drawing.Size(211, 22);
            this.informeEntreMarcadoresToolStripMenuItem.Text = "Informe entre marcadores";
            this.informeEntreMarcadoresToolStripMenuItem.Click += new System.EventHandler(this.informeEntreMarcadoresToolStripMenuItem_Click);
            // 
            // analisisFrecuencialToolStripMenuItem
            // 
            this.analisisFrecuencialToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.transformadaRapidaDeFurierToolStripMenuItem,
            this.transformadaOnditaToolStripMenuItem});
            this.analisisFrecuencialToolStripMenuItem.Name = "analisisFrecuencialToolStripMenuItem";
            this.analisisFrecuencialToolStripMenuItem.Size = new System.Drawing.Size(122, 20);
            this.analisisFrecuencialToolStripMenuItem.Text = "Análisis Frecuencial";
            // 
            // transformadaRapidaDeFurierToolStripMenuItem
            // 
            this.transformadaRapidaDeFurierToolStripMenuItem.Name = "transformadaRapidaDeFurierToolStripMenuItem";
            this.transformadaRapidaDeFurierToolStripMenuItem.Size = new System.Drawing.Size(237, 22);
            this.transformadaRapidaDeFurierToolStripMenuItem.Text = "Transformada Rapida de Furier ";
            this.transformadaRapidaDeFurierToolStripMenuItem.Click += new System.EventHandler(this.transformadaRapidaDeFurierToolStripMenuItem_Click);
            // 
            // transformadaOnditaToolStripMenuItem
            // 
            this.transformadaOnditaToolStripMenuItem.Name = "transformadaOnditaToolStripMenuItem";
            this.transformadaOnditaToolStripMenuItem.Size = new System.Drawing.Size(237, 22);
            this.transformadaOnditaToolStripMenuItem.Text = "Transformada Ondita ";
            // 
            // chart1
            // 
            chartArea1.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.chart1.Legends.Add(legend1);
            this.chart1.Location = new System.Drawing.Point(33, 35);
            this.chart1.Name = "chart1";
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            series1.Legend = "Legend1";
            series1.Name = "Señal";
            series2.ChartArea = "ChartArea1";
            series2.LabelBorderWidth = 5;
            series2.Legend = "Legend1";
            series2.MarkerSize = 1;
            series2.MarkerStep = 100;
            series2.Name = "Marcador_Inicial";
            series2.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Double;
            series2.YAxisType = System.Windows.Forms.DataVisualization.Charting.AxisType.Secondary;
            series3.BorderWidth = 5;
            series3.ChartArea = "ChartArea1";
            series3.Legend = "Legend1";
            series3.MarkerStep = 100;
            series3.Name = "Marcador_Final";
            series3.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Double;
            series3.YAxisType = System.Windows.Forms.DataVisualization.Charting.AxisType.Secondary;
            series4.ChartArea = "ChartArea1";
            series4.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Point;
            series4.IsValueShownAsLabel = true;
            series4.Legend = "Legend1";
            series4.Name = "Presión Sistolica";
            series4.YAxisType = System.Windows.Forms.DataVisualization.Charting.AxisType.Secondary;
            this.chart1.Series.Add(series1);
            this.chart1.Series.Add(series2);
            this.chart1.Series.Add(series3);
            this.chart1.Series.Add(series4);
            this.chart1.Size = new System.Drawing.Size(865, 205);
            this.chart1.TabIndex = 1;
            this.chart1.Text = "chart1";
            // 
            // Barra_Tiempo
            // 
            this.Barra_Tiempo.Location = new System.Drawing.Point(168, 246);
            this.Barra_Tiempo.Name = "Barra_Tiempo";
            this.Barra_Tiempo.Size = new System.Drawing.Size(577, 45);
            this.Barra_Tiempo.TabIndex = 2;
            this.Barra_Tiempo.ValueChanged += new System.EventHandler(this.Barra_Tiempo_ValueChanged);
            // 
            // Marcador_Inicial
            // 
            this.Marcador_Inicial.Location = new System.Drawing.Point(168, 298);
            this.Marcador_Inicial.Name = "Marcador_Inicial";
            this.Marcador_Inicial.Size = new System.Drawing.Size(577, 45);
            this.Marcador_Inicial.TabIndex = 3;
            this.Marcador_Inicial.ValueChanged += new System.EventHandler(this.Marcador_Inicial_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(33, 246);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(42, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Tiempo";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(36, 298);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(82, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Marcador Inicial";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(36, 343);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Marcador Final";
            // 
            // Marcador_Final
            // 
            this.Marcador_Final.Location = new System.Drawing.Point(168, 343);
            this.Marcador_Final.Maximum = 50;
            this.Marcador_Final.Minimum = 10;
            this.Marcador_Final.Name = "Marcador_Final";
            this.Marcador_Final.Size = new System.Drawing.Size(577, 45);
            this.Marcador_Final.TabIndex = 7;
            this.Marcador_Final.Value = 10;
            this.Marcador_Final.ValueChanged += new System.EventHandler(this.Marcador_Final_ValueChanged);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(820, 246);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(78, 17);
            this.checkBox1.TabIndex = 9;
            this.checkBox1.Text = "Mostrar PS";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // Recortar
            // 
            this.Recortar.Location = new System.Drawing.Point(806, 280);
            this.Recortar.Name = "Recortar";
            this.Recortar.Size = new System.Drawing.Size(92, 49);
            this.Recortar.TabIndex = 11;
            this.Recortar.Text = "Recortar señal entre marcadores";
            this.Recortar.UseVisualStyleBackColor = true;
            this.Recortar.Click += new System.EventHandler(this.Recortar_Click);
            // 
            // Limpiar
            // 
            this.Limpiar.Location = new System.Drawing.Point(806, 343);
            this.Limpiar.Name = "Limpiar";
            this.Limpiar.Size = new System.Drawing.Size(92, 23);
            this.Limpiar.TabIndex = 13;
            this.Limpiar.Text = "Limpiar señal";
            this.Limpiar.UseVisualStyleBackColor = true;
            this.Limpiar.Click += new System.EventHandler(this.Limpiar_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(904, 400);
            this.Controls.Add(this.Limpiar);
            this.Controls.Add(this.Recortar);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.Marcador_Final);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Marcador_Inicial);
            this.Controls.Add(this.Barra_Tiempo);
            this.Controls.Add(this.chart1);
            this.Controls.Add(this.menuStrip1);
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Form1";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Barra_Tiempo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Marcador_Inicial)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Marcador_Final)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem archivoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem analisisTemporalToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem informeGeneralToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem informeEntreMarcadoresToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem analisisFrecuencialToolStripMenuItem;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private System.Windows.Forms.TrackBar Barra_Tiempo;
        private System.Windows.Forms.TrackBar Marcador_Inicial;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TrackBar Marcador_Final;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.ToolStripMenuItem transformadaRapidaDeFurierToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem transformadaOnditaToolStripMenuItem;
        private System.Windows.Forms.Button Recortar;
        private System.Windows.Forms.Button Limpiar;
    }
}

