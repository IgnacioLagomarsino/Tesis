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
    public partial class Informe_Temporal : Form
    {
        public Informe_Temporal()
        {
            InitializeComponent();
        }

        public void Calcular_PM(int[] Max, int[] Min)
        {
            //Calcular PM
            double P = 0;
            int N = Max.Length;
            for (int i=0; i<N-1; i++)
            {
                P = P + (double)(Max[i] - Min[i]) / 3 + Min[i];
            }
            P = P / (N - 1);
            P = Math.Truncate(P * 100) / 100; //recorar decimales
            PM.Text = P.ToString();

            //Desvio Estandar
            double D = 0;
            double aux = 0;
            for(int i=0; i<N-1; i++)
            {
                aux= (double)(Max[i] - Min[i]) / 3 + Min[i];
                D = D + Math.Pow(aux - P, 2);
            }
            D = Math.Sqrt(D / (double)(N - 2));
            D = Math.Truncate(D * 100) / 100; //recorar decimales
            PM_DE.Text = D.ToString();

            //Coeficiente devariablidad 
            double cv;
            cv = D / P;
            cv = cv * 100;            
            cv = Math.Truncate(cv * 100) / 100; //recorar decimales
            PM_CV.Text = cv.ToString();
            
        }

        public void Calcular_Promedios_PS_PD(int[] Max, int[] Min)
        {//Reever si tengo un min antes del primer maximo¡¡¡¡¡¡¡¡
            double ps = 0;
            double pd = 0;
            int c = Max.Length;
            for (int i=0; i<c-1;i++)
            {
                ps = ps + Max[i];
                pd = pd + Min[i];
            }
            ps = ps + Max[c - 1];
            ps = ps / (double)(c);
            pd = pd / (double)(c-1);
            ps = Math.Truncate(ps * 100) / 100; //recorar decimales
            pd = Math.Truncate(pd * 100) / 100; //recorar decimales
            PS.Text = ps.ToString();
            PD.Text = pd.ToString();

            //Desvio Estandar
            double D = 0;
            double S = 0;
            for (int i = 0; i < c-1; i++)
            {
                D = D + Math.Pow(Min[i] - pd, 2);
                S = S + Math.Pow(Max[i] - ps, 2);
            }
            S = S + Math.Pow(Max[c-1] - ps, 2);
            D = Math.Sqrt(D / (double)(c - 1));
            D = Math.Truncate(D * 100) / 100; //recorar decimales
            PD_DE.Text = D.ToString();            
            S = Math.Sqrt(S / (double)(c));
            S = Math.Truncate(S * 100) / 100; //recorar decimales
            PS_DE.Text = S.ToString();

            //Coeficiente devariablidad 
            double cvd;
            double cvs;
            cvd = D / pd;
            cvd = cvd * 100;
            cvd = Math.Truncate(cvd * 100) / 100; //recorar decimales
            PD_CV.Text = cvd.ToString();
            cvs = S / ps;
            cvs = cvs * 100;
            cvs = Math.Truncate(cvs * 100) / 100; //recorar decimales
            PS_CV.Text = cvs.ToString();


        }

        public void Calcular_FR(double[] Per)
        {
            double prom = 0;
            int N = Per.Length;

            for(int i=0; i<N; i++)
            {
                prom = prom + 1/Per[i];
            }
            prom = prom / N;
            prom = Math.Truncate(prom * 100) / 100; //recorar decimales
            FC.Text = prom.ToString();

            //Desvio Estandar
            double D = 0;            
            for (int i = 0; i < N; i++)
            {                
                D = D + Math.Pow((1/Per[i]) - prom, 2);
            }
            D = Math.Sqrt(D / (double)(N - 1));
            D = Math.Truncate(D * 100) / 100; //recorar decimales
            FC_DE.Text = D.ToString();

            //Coeficiente devariablidad 
            double cv;
            cv = D / prom;
            cv = cv * 100;
            cv = Math.Truncate(cv * 100) / 100; //recorar decimales
            FC_CV.Text = cv.ToString();

        }

        private void label2_Click(object sender, EventArgs e)
        {
            
        }
    }
}
