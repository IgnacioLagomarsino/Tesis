using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;





namespace WindowsFormsApp8
{
    public partial class Informe_Frecuencial : Form
    {
        public Informe_Frecuencial()
        {
            InitializeComponent();
        }

        double[] Tacograma;
        double[] tiempo;
        int FM = 100; //Frecuencia de Muestreo


        //adquiero tacograma y tiempos
        public void getvariables(double[] T, double[] tiemp, int MI, int MF)
        {
            int cont = 0;

            for (int i = 0; i < tiemp.Length; i++)
            {                
                if (tiemp[i] >= (double)MI/FM && tiemp[i] <= (double)MF/FM)
                {
                    cont++;
                }
            } 

            Tacograma = new double[cont];
            tiempo = new double[cont];

            cont = 0;

            for (int i = 0; i < tiemp.Length; i++)
            {
                if (tiemp[i] >= (double)MI/FM && tiemp[i] < (double)MF/FM)
                {
                    Tacograma[cont] = T[i];
                    tiempo[cont] = tiemp[i];
                    cont++;
                }
            }
                                  
            
            //LLevo la señal al  origen 
            
            for(int i= tiempo.Length - 1; i>=0; i--)
            {
                tiempo[i] = tiempo[i] - tiempo[0];
            }
            
            //Interpolacion Splin Cubicos

            int N = tiempo.Length - 1;

            //Armamos hk

            double[] h = new double[N];
            double[] d = new double[N];



            for (int i = 0; i < N; i++)
            {
                h[i] = tiempo[i + 1] - tiempo[i];
            }



            for (int i = 0; i < N; i++)
            {
                d[i] = (Tacograma[i + 1] - Tacograma[i]) / h[i];
            }

            int mo = 0;
            int mN = 0;
            double[] a = new double[N - 2];//a(0)=0

            for (int i = 0; i < N - 2; i++)
            {
                a[i] = h[i + 1];
            }
            double[] b = new double[N - 1];


            for (int i = 0; i < N - 1; i++)
            {
                b[i] = 2 * (h[i]);
            }
            for (int i = 0; i < N - 1; i++)
            {
                b[i] = b[i] + 2 * (h[i + 1]);
            }



            double[] u = new double[N - 1];

            for (int i = 0; i < N - 1; i++)
            {
                u[i] = 6 * d[i + 1];
            }
            for (int i = 0; i < N - 1; i++)
            {
                u[i] = u[i] - 6 * d[i];
            }

            double[] w = new double[N - 1];

            w = u;
            w[0] = u[0] - h[0] * mo;
            w[N - 2] = u[N - 2] - h[N - 2] * mN;


            double[] c = new double[N - 2];

            for (int i = 0; i < N - 2; i++)
            {
                c[i] = h[i + 1];
            }
            double[] M = new double[N + 1];

            M[0] = mo;
            M[N] = mN;
            int N1 = N - 1;
            double Piv;
            //w es B
            for (int i = 1; i < N1; i++)
            {
                Piv = a[i - 1] / b[i - 1];
                b[i] = b[i] - Piv * c[i - 1];
                w[i] = w[i] - Piv * w[i - 1];

            }

            double[] X = new double[N1];
            X[N1 - 1] = w[N1 - 1] / b[N1 - 1];

            // sustitucion hacia atras
            for (int i = N1 - 2; i > -1; i--)
            {
                X[i] = ((w[i] - c[i] * X[i + 1]) / b[i]);
            }
            for (int i = 1; i < N; i++)
            {
                M[i] = X[i - 1];
            }


            // MAtriz de Splines cubicos

            double[,] SM = new double[N, 4];

            for (int i = 0; i < N; i++)
            {
                SM[i, 0] = (M[i + 1] - M[i]) / (6 * h[i]);
                SM[i, 1] = M[i] / 2;
                SM[i, 2] = d[i] - (h[i] * ((2 * M[i]) + M[i + 1]) / 6);
                SM[i, 3] = T[i];
            }


            //Nuevo tacograma 

            int l = (int)(tiempo[N] * 10);// - (int)tiempo[0];                  
            double[] Xn = new double[l];//Nueva varieble de tiempo
            double[] Y = new double[l];//Tacograma interpolado
            
            int j = 0;

            for(int i=0; i<l; i++)
            {
                Xn[i] = (double)i*0.1;// + tiempo[0];
            }

            for(int i=0; i<l; i++)
            {
           
                if (j<N)
                {
                    if (Xn[i] < tiempo[j + 1])
                    {
                        Y[i] = SM[j, 0] * Math.Pow(Xn[i]-tiempo[j], 3) + SM[j, 1] * Math.Pow(Xn[i]- tiempo[j], 2) + SM[j, 2] * (Xn[i]- tiempo[j]) + SM[j, 3];
                    }
                    else
                    {
                        j++;

                        if(j<N)
                        {
                            Y[i] = SM[j, 0] * Math.Pow(Xn[i] - tiempo[j], 3) + SM[j, 1] * Math.Pow(Xn[i] - tiempo[j], 2) + SM[j, 2] * (Xn[i] - tiempo[j]) + SM[j, 3];
                        }

                    }
                }
                                
            }

            
            //Graficar Tacograma interpolado 

            for (int i = 0; i < l; i++)
            {
                               
                    this.grafico.Series["Periodos"].Points.AddXY(Xn[i], Y[i]);
                
            }

            
            // puntos origiales 

            for (int i = 0; i < tiempo.Length; i++)
            {

                this.grafico.Series["Ptos Originales"].Points.AddXY(tiempo[i], Tacograma[i]);

            }

            //Transformada de Fourier 

            int Fs = 10; //Frecuencia de muestreo
            int L = l;  //Cantidad de puntos
            float FT = L;                      

            double[] t;
            t = new double[L];
            double[] S;
            S = new double[L];

            double[] FR;   //Vector d frecuencias
            FR = new double[L];

            double[] HR;    //Parte Real de la TTF
            HR = new double[L];

            double[] HI;    //PArte Imaginaria de la TTF
            HI = new double[L];

            double[] ABS;   //Modulo de la TTF
            ABS = new double[L];

            /*
            //Señal de prueba para la TTF
            for (int i =0; i<L; i++)
            {
                t[i] = i*0.001 ;
                S[i] = Math.Sin(2 * Math.PI * 50 * t[i]) + Math.Sin(2 * Math.PI * 120 * t[i]);
                
            }
            */

            //Inicio de varaiables TTF
            for (int i = 0; i < L; i++)
            {                
                FR[i] = (double)i * (double)Fs / FT;
                HR[i] = 0;
                HI[i] = 0;

                //this.grafico.Series["Periodos"].Points.AddXY(t[i],S[i]);

            }

            //Transormada de Fourier 

            for (int i = 0; i < L; i++)
            {
                for (int k = 0; k < L; k++)
                {
                    HR[i] = HR[i] + Y[k] * Math.Cos(2 * Math.PI * (k * i) / L);
                    HI[i] = HI[i] + Y[k] * Math.Sin(2 * Math.PI * (k * i) / L);
                }
                ABS[i] = Math.Sqrt(Math.Pow(HR[i] / L, 2) + Math.Pow(HI[i] / L, 2));

                if (i > 0)  //L/2
                {
                    if (FR[i]<1)
                    {
                        if (ABS[i] > 0)
                        {
                            Fourier.Series["Espectro"].Points.AddXY(FR[i], ABS[i] * 2);

                        }
                    }
                    

                }

            }

            double lf = 0;
            double hf = 0;
            double lfhf = 0;
            double P = 0;
            int C1 = 0;
            int C2 = 0;

            //Calculo de LF
            
            for(int i=0; FR[i]<=0.15; i++)
            {
                lf = lf + Math.Pow(ABS[i], 2);
                C1++;
            }
            //lf = lf / (Fs * C1);
            LF.Text = lf.ToString();

            //Calculo de HF

            for (int i=C1; FR[i] <= 0.4; i++)
            {
                hf = hf + Math.Pow(ABS[i], 2);
                C2++;
            }
            //hf = hf / (Fs * C2); 
            HF.Text = hf.ToString();

            //Calculo de LP/HF

            lfhf = lf / hf;
            LFHF.Text = lfhf.ToString();

            //Calculo de PVE

            for (int i=1; i<FR.Length; i++)
            {
               P = P + Math.Pow(ABS[i] , 2);
            }
            P = P / (Fs * (FR.Length - 1));
            PEV.Text = P.ToString();
            
        }

        

        private void grafico_Click(object sender, EventArgs e)
        {

        }
    }
}
