using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;



namespace WindowsFormsApp8
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


        int[] datos; //Datos crudos
        double[] Y; //Señal filtrada
        double[] D; //Cuadrado de primera derivada
        double[] D1; //Primer derivada
        double[] tiempo;
        int[] Maximos; //Valor de  la presion sistolica
        int[] Minimos; //Posicion en datos
        double[] T_Maximos;//Posicion en datos
        double[] T_Minimos;
        double[] Periodos;
        double[] T_Periodos;
        int size = 0;        
        int FM = 100; //Frecuencia de Muestreo
        int L_Ventana = 600;
        double PM = 0; //Presion Media
        int P_Max = 0; //Presion Maxima 
        int inicio = 0;
    
        

        private void cToolStripMenuItem_Click(object sender, EventArgs e)
        {
            

            OpenFileDialog fdlg = new OpenFileDialog();
            fdlg.Title = "C# Corner Open File Dialog";
            fdlg.InitialDirectory = @"c:\";
            fdlg.Filter = "All files (*.*)|*.*|All files (*.*)|*.*";
            fdlg.FilterIndex = 2;
            fdlg.RestoreDirectory = true;
            if (fdlg.ShowDialog() == DialogResult.OK)
            {
                // textBox.Text = fdlg.FileName;
                string text1 = File.ReadAllText(fdlg.FileName);

                string[] text = File.ReadAllLines(fdlg.FileName);
                size = text.Length;
                datos = new int[size];
                tiempo = new double[size];
                                
                Barra_Tiempo.SmallChange = 1;
                Marcador_Inicial.SmallChange = 1;
                Marcador_Final.SmallChange = 1;
                Barra_Tiempo.Maximum = (size - L_Ventana - 1);
                Marcador_Inicial.Maximum = (size - 10);
                Marcador_Final.Maximum = size;
                Marcador_Final.Minimum = 10;                

                //llevar todos los elementos a cero
                if (inicio > 0)
                {

                    Barra_Tiempo.Value = 0;
                    Marcador_Final.Value = 10;
                    Marcador_Inicial.Value = 0;
                    checkBox1.Checked = false;
                    chart1.Series["Señal"].Points.Clear();
                    chart1.Series["Marcador_Inicial"].Points.Clear();
                    chart1.Series["Marcador_Final"].Points.Clear();
                    chart1.Series["Presión Sistolica"].Points.Clear();
                }

                inicio++;

                //Carga de datos
                for (int i = 0; i < size; i++)
                {
                    tiempo[i] = (double)i / (double)FM;
                    datos[i] = Int32.Parse(text[i]);

                }


            

                //Calculo de Presion Media y P MAxima

                Presion_Media();

                //Calculo de Maximos

                Calculo_Maximos();

                //Calculo de Minimos

                Calculo_Minimos();

                //Tacograma
                
                Tacograma();

                // Graicar señal

                for (int i = 0; i < L_Ventana; i++)
                {

                    this.chart1.Series["Señal"].Points.AddXY(tiempo[i], datos[i]);                    

                }



            }
        }


        //Calculo de Presion Media y P MAxima

        private void Presion_Media()
        {  
            for (int i = 0; i < size; i++)
            {
                PM = PM + datos[i];
                if (datos[i] > P_Max)
                {
                    P_Max = datos[i];
                }
            }
            PM = PM / size;
        }

        //Calculo de Maximos

        private void Calculo_Maximos()
        {
            double promedio = 0;            
            double[] aux;
            double[] D;
            double[] D1;
            double[] aux_t;
            int[] b;
            int[] a;
            double[] S;
            //double[] Y;
            int cont = 0;            
            aux = new double[(int)(size / 3)];
            aux_t = new double[(int)(size / 3)];

            //Aplico filtro pasa bajos con Fc en 15HZ

            b = new int[] { 1, 0, 0, 0, 0, 0, -2, 0, 0, 0, 0, 0, 1 };
            a = new int[] { 1, -2, 1 };
            S = new double[size];
            Y = new double[size + 6];


            for (int i=0; i<size; i++)
            {
                S[i] = (datos[i] - PM) / (P_Max - PM);
            }

            Y[0] = b[0] * S[0];
            Y[1] = b[0] * S[1] - a[1] * Y[0];
            Y[2] = b[0] * S[2] - a[1] * Y[1] - a[2] * Y[0];
            Y[3] = b[0] * S[3] - a[1] * Y[2] - a[2] * Y[1];
            Y[4] = b[0] * S[4] - a[1] * Y[3] - a[2] * Y[2];
            Y[5] = b[0] * S[5] - a[1] * Y[4] - a[2] * Y[3];
            Y[6] = b[0] * S[6] + b[6] * S[0] - a[1] * Y[5] - a[2] * Y[4];
            Y[7] = b[0] * S[7] + b[6] * S[1] - a[1] * Y[6] - a[2] * Y[5];
            Y[8] = b[0] * S[8] + b[6] * S[2] - a[1] * Y[7] - a[2] * Y[6];
            Y[9] = b[0] * S[9] + b[6] * S[3] - a[1] * Y[8] - a[2] * Y[7];
            Y[10] = b[0] * S[10] + b[6] * S[4] - a[1] * Y[9] - a[2] * Y[8];
            Y[11] = b[0] * S[11] + b[6] * S[5] - a[1] * Y[10] - a[2] * Y[9];
            Y[12] = b[0] * S[12] + b[6] * S[6] + b[12] * S[0] - a[1] * Y[11] - a[2] * Y[10];

            for(int i=13; i<size; i++)
            {
                Y[i] = b[0] * S[i] + b[6] * S[i-6] + b[12] * S[i-12] - a[1] * Y[i-1] - a[2] * Y[i-2];
            }
            double max = 0;

            //Corregir dalay de 6 muestras
            for (int i=0; i<size-1;i++)
            {
                if(Y[i+6]>max)
                {
                    max = Y[i+6];
                }
                Y[i] = Y[i + 6];                
            }

            
            //Normalizar
            for(int i=0;i<size;i++)
            {
                Y[i] = Y[i] / max;
            }

            //Derivo
            D = new double[size - 2];
            D1 = new double[size - 2];
            double MM = 0;
            double P = 0;
            for (int i=1; i<size-1;i++)
            {
                D1[i - 1] = (Y[i + 1] - Y[i - 1]);
                D1[i - 1] = D1[i - 1] * FM / 2;
                D[i - 1] = D1[i - 1] * D1[i - 1];
                P = P + D[i - 1];
                if(D[i-1]>MM && i<600)
                {
                    MM = D[i-1];
                }
            }
            P = P / size - 2;
            double P_Tolerancia = 0.45;          
                   

            for (int i = 0; i < size - 8; i++)
            {
                if (cont < 4)
                {
                    if (D[i + 2] < D[i + 1] && D[i] < D[i + 1]  && D[i + 1] > (MM * P_Tolerancia) && D1[i + 1] > 0)
                    {
                        aux_t[cont] = tiempo[i + 1];
                        aux[cont] = D[i + 1];
                        cont++;
                        i = i + 30;
                    }
                    else
                    {
                        if (D[i + 2] == D[i + 1] && D[i + 1] > D[i + 3] && D[i] < D[i + 1] && D[i + 1] > (MM * P_Tolerancia) && D1[i + 1] > 0)
                        {
                            aux_t[cont] = tiempo[i + 1];
                            aux[cont] = D[i + 1];
                            cont++;
                            i = i + 30;
                        }
                    }

                }
                else
                {                    
                    promedio = (aux[cont - 3] + aux[cont - 1] + aux[cont - 2]) / 3;
                    if (D[i + 3] < D[i + 1] && D[i + 2] < D[i + 1] && D[i] < D[i + 1] && D[i - 1] < D[i + 1] && D[i + 1] > (promedio * P_Tolerancia) && D1[i + 1]>0)
                    {
                        aux_t[cont] = tiempo[i + 1];
                        aux[cont] = D[i + 1];
                        cont++;
                        i = i + 30;
                    }
                    else
                    {
                        if (D[i + 2] == D[i + 1] && D[i + 1] > D[i + 3] && D[i] < D[i + 1] && D[i + 1] > (promedio * P_Tolerancia) && D1[i + 1] > 0)
                        {
                            aux_t[cont] = tiempo[i + 1];
                            aux[cont] = D[i + 1];
                            cont++;
                            i = i + 30;
                        }
                    }
                    
                }

            }
                       
            double[] aux1_t;            
            aux1_t = new double[cont];
            int k = 0;
            int f = 0;
            int con1 = 0;
            
            for(int i=0; i<cont-1; i++)
            {
                k = (int)(aux_t[i] * FM);
                f= (int)(aux_t[i+1] * FM);
                for (int j = k; j<f; j++)
                {                    
                    if (Y[j + 3] < Y[j + 1] && Y[j + 2] < Y[j + 1] && Y[j] < Y[j + 1] && Y[j - 1] < Y[j + 1])
                    {                           
                        for(int l=j; l<f-1; l++)
                        {
                            if (D1[l] <= 0 )
                            {
                                for (int r = l; r < f; r++)
                                {
                                    if (D1[r] >= 0 )
                                    {
                                        if(Y[r] < Y[j]*0.90 && Y[l] < Y[j] && Y[l] < Y[r] )
                                        {
                                            aux1_t[con1] = tiempo[j + 1];                                            
                                            con1++;
                                            j = f + 1;
                                            r = f + 1;
                                            l = f + 1;
                                        }
                                        if (r > j + 150)
                                        {
                                            j = f + 1;
                                            r = f + 1;
                                            l = f + 1;
                                        }
                                    }
                                }
                            }                            
                        } 
                        
                    }
                    else
                    {
                        if (Y[j + 2] == Y[j + 1] && Y[j + 1] > Y[j + 3] && Y[j] < Y[j + 1])
                        {                            
                            for (int l = j; l < f - 3; l++)
                            {
                                if (D1[l + 3] > D1[l + 1] && D1[l + 2] > D1[l + 1] && D1[l] > D1[l + 1] && D1[l - 1] > D1[l + 1])
                                {
                                    for (int r = l; r < f; r++)
                                    {
                                        if (D1[r + 3] < D1[r + 1] && D1[r + 2] < D1[r + 1] && D1[r] < D1[r + 1] && D1[r - 1] < D1[r + 1])
                                        {
                                            if (Y[r] < Y[j]*0.9 && Y[l] < Y[j] && Y[l] < Y[r])
                                            {
                                                aux_t[con1] = tiempo[j + 1];                                                
                                                con1++;
                                                j = f + 1;
                                                r = f + 1;
                                                l = f + 1;
                                            }
                                            if(r > j + 150)
                                            {
                                                j = f + 1;
                                                r = f + 1;
                                                l = f + 1;
                                            }
                                        }
                                    }
                                }
                            }
                            
                        }

                    }
                }

            }

            Maximos = new int[con1];
            T_Maximos = new double[con1];

            for(int i=0; i < con1; i++)
            {
                Maximos[i] = datos[(int)(aux1_t[i])];
                T_Maximos[i] = aux1_t[i];
            }

        }

        //Minimos - Presion diastolica 

        private void Calculo_Minimos()
        {                       
            Minimos = new int[Maximos.Length - 1];
            T_Minimos = new double[Maximos.Length - 1];
            int k = 0;
            for (int i=1; i<Maximos.Length; i++)
            {
                k = (int)(T_Maximos[i] * FM);
                for (int j=k; tiempo[j] > T_Maximos[i-1]; j--)
                {                    
                    if (Y[j - 3] > Y[j - 1] && Y[j - 2] >= Y[j - 1] && Y[j] >= Y[j - 1] && Y[j + 1] > Y[j - 1])
                    {
                        T_Minimos[i - 1] = tiempo[j - 1];
                        Minimos[i - 1] = datos[j - 1];
                        j = 1;
                    }
                }           
                
            }
        }

        //Construir Tacograma 

        private void Tacograma()
        {
            Periodos = new double[Maximos.Length - 1];
            T_Periodos = new double[Maximos.Length - 1];

            for (int i = 0; i < Maximos.Length - 1; i++)
            {
                T_Periodos[i] = T_Maximos[i];
                Periodos[i] = (T_Maximos[i + 1] - T_Maximos[i]);
            }
        }
    

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Barra_Tiempo_ValueChanged(object sender, EventArgs e)
        {
            if (inicio > 0)
            {
                chart1.Series["Señal"].Points.Clear();
                chart1.Series["Marcador_Inicial"].Points.Clear();
                chart1.Series["Marcador_Final"].Points.Clear();
                chart1.Series["Presión Sistolica"].Points.Clear();

                for (int i = (int)(Barra_Tiempo.Value); i <= (int)(Barra_Tiempo.Value + L_Ventana); i++)
                {

                    this.chart1.Series["Señal"].Points.AddXY(tiempo[i], datos[i]);


                }

                if (Marcador_Inicial.Value > Barra_Tiempo.Value && Marcador_Inicial.Value < Barra_Tiempo.Value + L_Ventana)
                {
                    this.chart1.Series["Marcador_Inicial"].Points.Clear();
                    for (int i = (int)(Barra_Tiempo.Value); i <= ((int)(Barra_Tiempo.Value) + L_Ventana); i++)
                    {
                        this.chart1.Series["Marcador_Inicial"].Points.AddXY((double)i / FM, 0);

                    }
                    this.chart1.Series["Marcador_Inicial"].Points.AddXY((double)Marcador_Inicial.Value / FM, 300);
                    //this.chart1.Series["Marcador_Inicial"].Points.AddXY((double)Marcador_Inicial.Value/FM, 300);
                }

                if (Marcador_Final.Value >= Barra_Tiempo.Value && Marcador_Final.Value <= Barra_Tiempo.Value + L_Ventana)
                {
                    this.chart1.Series["Marcador_Final"].Points.Clear();
                    for (int i = (int)(Barra_Tiempo.Value); i <= ((int)(Barra_Tiempo.Value) + L_Ventana); i++)
                    {

                        this.chart1.Series["Marcador_Final"].Points.AddXY((double)i / FM, 0);

                    }
                    this.chart1.Series["Marcador_Final"].Points.AddXY((double)Marcador_Final.Value / FM, 300);
                    //this.chart1.Series["Marcador_Final"].Points.AddXY((double)Marcador_Final.Value/FM, 300);
                }





                if (checkBox1.Checked == true)
                {
                    int a = 0;
                    //Graficar Maximos
                    for (int i = 0; i < Maximos.Length; i++)
                    {

                        if (T_Maximos[i]*FM >= Barra_Tiempo.Value && T_Maximos[i]*FM <= Barra_Tiempo.Value + L_Ventana)
                        {
                            a = (int)(T_Maximos[i] * FM);
                            this.chart1.Series["Presión Sistolica"].Points.AddXY(T_Maximos[i], datos[a]);
                        }
                    }
                    
                    //Graficar Minimos
                    for (int i = 0; i < Minimos.Length; i++)
                    {

                        if (T_Minimos[i] * FM >= Barra_Tiempo.Value && T_Minimos[i] * FM <= Barra_Tiempo.Value + L_Ventana)
                        {

                            this.chart1.Series["Presión Sistolica"].Points.AddXY(T_Minimos[i], Minimos[i]);
                        }
                    }
                }
            }
                
        }

        private void informeGeneralToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (inicio > 0)
            {
                //Informe en nueva ventana 
                Informe_Temporal Informe = new Informe_Temporal();
                Informe.Calcular_PM(Maximos, Minimos);
                Informe.Calcular_Promedios_PS_PD(Maximos, Minimos);
                Informe.Calcular_FR(Periodos);
                Informe.ShowDialog();
            }
               
            
        }

        private void Marcador_Inicial_ValueChanged(object sender, EventArgs e)
        {
            if (inicio > 0)
            {
                if (Marcador_Final.Value - Marcador_Inicial.Value < 10)
                {
                    Marcador_Final.Value = Marcador_Inicial.Value + 10;
                }


                if (Marcador_Inicial.Value >= Barra_Tiempo.Value && Marcador_Inicial.Value <= Barra_Tiempo.Value + L_Ventana)
                {                    
                    this.chart1.Series["Marcador_Inicial"].Points.Clear();
                    for (int i = (int)(Barra_Tiempo.Value); i <= ((int)(Barra_Tiempo.Value) + L_Ventana); i++)
                    {                        
                        this.chart1.Series["Marcador_Inicial"].Points.AddXY((double)i/FM, 0);

                    }
                    this.chart1.Series["Marcador_Inicial"].Points.AddXY((double)Marcador_Inicial.Value/FM, 300);

                }
                if (Marcador_Inicial.Value < Barra_Tiempo.Value)
                {
                    Barra_Tiempo.Value = Marcador_Inicial.Value;
                }
                if (Marcador_Inicial.Value > Barra_Tiempo.Value + L_Ventana)
                {
                    Barra_Tiempo.Value = Marcador_Inicial.Value - L_Ventana;
                }

            }
        }
                

        private void Marcador_Final_ValueChanged(object sender, EventArgs e)
        {
            if (inicio > 0)
            {
                if (Marcador_Final.Value - Marcador_Inicial.Value < 10)
                {
                    Marcador_Inicial.Value = Marcador_Final.Value - 10;
                }


                if (Marcador_Final.Value >= Barra_Tiempo.Value && Marcador_Final.Value <= Barra_Tiempo.Value + L_Ventana)
                {
                    this.chart1.Series["Marcador_Final"].Points.Clear();
                    for (int i = (int)(Barra_Tiempo.Value); i <= ((int)(Barra_Tiempo.Value) + L_Ventana); i++)
                    {

                        this.chart1.Series["Marcador_Final"].Points.AddXY((double)i/FM, 0);

                    }
                    this.chart1.Series["Marcador_Final"].Points.AddXY((double)Marcador_Final.Value/FM, 300);
                }
                if (Marcador_Final.Value < Barra_Tiempo.Value)
                {
                    Barra_Tiempo.Value = Marcador_Final.Value;

                }
                if (Marcador_Final.Value > Barra_Tiempo.Value + L_Ventana)
                {
                    Barra_Tiempo.Value = Marcador_Final.Value - (L_Ventana) - 1;

                }
            }
                
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (inicio > 0)
            {
                if (checkBox1.Checked == true)
                {
                    int a = 0;
                    //Graicar Maximos
                    for (int i = 0; i < Maximos.Length; i++)
                    {

                        if (T_Maximos[i]*FM > Barra_Tiempo.Value && T_Maximos[i]*FM < Barra_Tiempo.Value + L_Ventana)
                        {
                            a = (int)(T_Maximos[i] * FM);
                            this.chart1.Series["Presión Sistolica"].Points.AddXY(T_Maximos[i], datos[a]);
                        }
                    }
                    
                    //Graficar Minimos
                    for (int i = 0; i < Minimos.Length; i++)
                    {

                        if (T_Minimos[i] * FM >= Barra_Tiempo.Value && T_Minimos[i] * FM <= Barra_Tiempo.Value + L_Ventana)
                        {

                            this.chart1.Series["Presión Sistolica"].Points.AddXY(T_Minimos[i], Minimos[i]);
                        }
                    }
                }
                else
                {
                    this.chart1.Series["Presión Sistolica"].Points.Clear();
                }
            }
                
                         

          
        }

        private void transformadaRapidaDeFurierToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (inicio > 0)
            {
                //Informe en nueva ventana 
                Informe_Frecuencial Informe_F = new Informe_Frecuencial();
                Informe_F.getvariables(Periodos, T_Periodos, Marcador_Inicial.Value, Marcador_Final.Value);
                Informe_F.ShowDialog();
            }
                

        }

        private void Recortar_Click(object sender, EventArgs e)
        {           
               DialogResult r = MessageBox.Show("Desea eliminar la señal entre marcadores?", "Recortar Señal", MessageBoxButtons.YesNo);
                if(r==DialogResult.Yes)
                {
                    int I = 0; //Posicion del Maximo anterios a MI
                    int im = 0; //Indice del Maximo anterios a MI
                    int F = 0; //Posicion del Maximo siguiente a MF
                    int fm = 0; //Indice del Maximo anterios a MF
                    int N = Maximos.Length;
                
                    for(int i=0; i<N; i++)
                    {
                        if(T_Maximos[i]>tiempo[Marcador_Inicial.Value])
                        {
                            im = i - 1;
                            I = (int)(T_Maximos[im]*FM);

                            for(int j=i; j<N; j++)
                            {
                                if(T_Maximos[j]>=tiempo[Marcador_Final.Value])
                                {
                                    fm = j;
                                F = (int)(T_Maximos[fm] * FM);
                                    j = N;
                                }
                            }
                            i = N;
                        }
                    }

                    //Recortar Señal
                                        
                    if(Maximos[im]==Maximos[fm])
                    {
                        int n = datos.Length - (F - I); //largo nuevo vector
                        int[] d_aux = new int[n];
                        double[] t_aux = new double[n];

                        for (int i=0; i<I; i++)
                        {
                            d_aux[i] = datos[i];
                            t_aux[i] = tiempo[i];
                        }

                        for (int i = I; i < n; i++)
                        {
                            d_aux[i] = datos[i + (F - I)];
                            t_aux[i] = tiempo[i];
                        }
                        
                        datos = new int[n];
                        tiempo = new double[n];
                        size = n;

                        for (int i = 0; i < n; i++)
                        {
                            datos[i] = d_aux[i];
                            tiempo[i] = t_aux[i];
                        }
                    }

                    //Caso Maximo[I]>Maximos[F]

                    if (Maximos[im] > Maximos[fm])
                    {
                        for(int i=I; i>T_Maximos[im-1]; i--)
                        {
                            if(datos[i]<Maximos[fm])
                            {
                                I = i + 1;
                                i = 0;
                            }
                        }

                        int n = datos.Length - (F - I); //largo nuevo vector
                        int[] d_aux = new int[n];
                        double[] t_aux = new double[n];

                        for (int i = 0; i < I; i++)
                        {
                            d_aux[i] = datos[i];
                            t_aux[i] = tiempo[i];
                        }

                        for (int i = I; i < n; i++)
                        {
                            d_aux[i] = datos[i + (F - I)];
                            t_aux[i] = tiempo[i];
                        }

                        datos = new int[n];
                        tiempo = new double[n];
                        size = n;

                        for (int i = 0; i < n; i++)
                        {
                            datos[i] = d_aux[i];
                            tiempo[i] = t_aux[i];
                        }
                    }

                    //Caso Maximo[I]<Maximos[F]

                    if (Maximos[im] < Maximos[fm])
                    {
                        for (int i = F; i < T_Maximos[fm + 1]; i++)
                        {
                            if (datos[i] < Maximos[im])
                            {
                                F = i;
                            i = (int)(T_Maximos[fm + 2] * FM);
                            }
                        }
                        int R = F - I ;
                        int n = datos.Length - R; //largo nuevo vector
                        int[] d_aux = new int[n];
                        double[] t_aux = new double[n];

                        for (int i = 0; i <= I; i++)
                        {
                            d_aux[i] = datos[i];
                            t_aux[i] = tiempo[i];
                        }
                        
                        for (int i = I+1; i < n; i++)
                        {
                            d_aux[i] = datos[i + R];
                            t_aux[i] = tiempo[i];
                        }

                        datos = new int[n];
                        tiempo = new double[n];
                        size = n;

                        for (int i = 0; i < n; i++)
                        {
                            datos[i] = d_aux[i];
                            tiempo[i] = t_aux[i];
                        }
                    }
                                    
                Barra_Tiempo.Maximum = (size - L_Ventana - 1);
                Marcador_Inicial.Maximum = (size - 10);
                Marcador_Final.Maximum = size;

                //Calculo de Presion Media y P MAxima

                Presion_Media();

                //Calculo de Maximos

                Calculo_Maximos();

                //Tacograma

                Tacograma();

                //Graficar de nuevo
                if(Barra_Tiempo.Value>=(size- L_Ventana - 10))
                {
                    Barra_Tiempo.Value = Barra_Tiempo.Value - 1;
                }
                else
                {
                    Barra_Tiempo.Value = Barra_Tiempo.Value + 1;
                }
                
            }
        }

        private void informeEntreMarcadoresToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (inicio > 0)
            {
                //Nuevo vector Maximos
                int cont = 0;

                for (int i = 0; i < T_Maximos.Length; i++)
                {
                    if (T_Maximos[i] >= (double)Marcador_Inicial.Value / FM && T_Maximos[i] <= (double)Marcador_Final.Value / FM)
                    {
                        cont++;
                    }
                }

                int[] Aux_Max = new int[cont];
                double[] Aux_T_Max = new double[cont];
                double[] Aux_Periodos = new double[cont - 1];

                int pos = 0;

                for (int i = 0; i < cont; i++)
                {
                    if (T_Maximos[i] >= (double)Marcador_Inicial.Value / FM && T_Maximos[i] <= (double)Marcador_Final.Value / FM)
                    {
                        Aux_Max[pos] = Maximos[i];
                        Aux_T_Max[pos] = T_Maximos[i];
                        pos++;
                    }
                }
                
                //Nuvo vector de periodos
                for (int  i=0; i<cont-1;i++)
                {
                    Aux_Periodos[i] = Aux_T_Max[i + 1] - Aux_T_Max[i];
                }

                //Nuevo vector de Minimos
                cont = 0;
                for (int i = 0; i < T_Minimos.Length; i++)
                {
                    if (T_Minimos[i] >= (double)Marcador_Inicial.Value / FM && T_Minimos[i] <= (double)Marcador_Final.Value / FM)
                    {
                        cont++;
                    }
                }

                int[] Aux_Min = new int[cont];
                pos = 0;

                for (int i = 0; i < cont; i++)
                {
                    if (T_Minimos[i] >= (double)Marcador_Inicial.Value / FM && T_Minimos[i] <= (double)Marcador_Final.Value / FM)
                    {
                        Aux_Min[pos] = Minimos[i];                        
                        pos++;
                    }
                }                
                
                
                //Informe en nueva ventana 
                Informe_Temporal Informe = new Informe_Temporal();
                Informe.Calcular_PM(Aux_Max, Aux_Min);
                Informe.Calcular_Promedios_PS_PD(Aux_Max, Aux_Min);
                Informe.Calcular_FR(Aux_Periodos);
                Informe.ShowDialog();
            }
        }

        private void Limpiar_Click(object sender, EventArgs e)
        {
            int n = 0;
            //ELimino latidos si Ps con una diferencia de mas del 20% 
            
            n = Maximos.Length;
            double prom = (Periodos[1] + Periodos[2] + Periodos[3]) / 3;

            for(int i=2; i<n-1; i++)
            {
                if(Math.Abs(Maximos[i]-Maximos[i+1]) > Maximos[i]*0.2)
                {
                    //ELimino Ps 
                    List<int> S = new List<int>(Maximos);
                    S.RemoveAt(i+1);
                    Maximos = S.ToArray();

                    List<double> Ts = new List<double>(T_Maximos);
                    Ts.RemoveAt(i + 1);
                    T_Maximos = Ts.ToArray();

                    //Acorto vector 
                    n--;
                    i--;

                }

                if (T_Maximos[i + 1] - T_Maximos[i] < prom * 0.5)
                {
                    //ELimino Ps 
                    List<int> S = new List<int>(Maximos);
                    S.RemoveAt(i + 1);
                    Maximos = S.ToArray();

                    List<double> Ts = new List<double>(T_Maximos);
                    Ts.RemoveAt(i + 1);
                    T_Maximos = Ts.ToArray();


                    //Acorto vector 
                    n--;
                    i--;

                }

                prom = (Periodos[i] + Periodos[i-1] + Periodos[i-2]) / 3;


            }

            //Elimino Pd con diferencia menor al 10%
            n = Minimos.Length;

            for (int i = 1; i < n - 1; i++)
            {
                if (Math.Abs(Minimos[i] - Minimos[i + 1]) > Minimos[i] * 0.1)
                {
                    //ELimino Pd
                    List<int> S = new List<int>(Minimos);
                    S.RemoveAt(i + 1);
                    Minimos = S.ToArray();

                    List<double> Ts = new List<double>(T_Minimos);
                    Ts.RemoveAt(i + 1);
                    T_Minimos = Ts.ToArray();

                    //Acorto vector 
                    n--;
                    i--;

                }
            }

                //ELimino periodos con diferencia del 30%
                n = Periodos.Length;
            for (int i = 1; i < n - 2; i++)
            {
                if (Math.Abs(Periodos[i] - Periodos[i + 1]) > Periodos[i] * 0.30)
                {
                    /*
                    //ELimino Ps 
                    List<int> S = new List<int>(Maximos);
                    S.RemoveAt(i + 1);
                    Maximos = S.ToArray();
                    */
                   
                    //Elimino Periodos correspondientes a este latido 
                    List<double> P1 = new List<double>(Periodos);
                    P1.RemoveAt(i + 1);
                    Periodos = P1.ToArray();

                    List<double> P1t = new List<double>(T_Periodos);
                    P1t.RemoveAt(i + 1);
                    T_Periodos = P1t.ToArray();

                    List<double> P2 = new List<double>(Periodos);
                    P2.RemoveAt(i + 1);
                    Periodos = P2.ToArray();

                    List<double> P2t = new List<double>(T_Periodos);
                    P2t.RemoveAt(i + 1);
                    T_Periodos = P2t.ToArray();

                    //Acorto vector 
                    n = n - 2;
                    i--;
                }


            }

        }
    }
}
