public class ConstQTransform
    {
        public double Q;
        public int K;
        public int FFTLength;
        public int SparLen;
        public Complex[][] sparKernel;

        public ConstQTransform(double minFreq, double MaxFreq, int Bins, int Fs, double Thresh)
        {
            Q = 1 / (Math.Pow(2, 1.0d / Bins) - 1);
            K = (int)Math.Ceiling(Bins * Math.Log(MaxFreq / minFreq, 2));
            FFTLength = (int)Math.Round(Math.Pow(2, NextPow2(Math.Ceiling(Q * Fs / minFreq))));

            double[] m_TempKernelReal = new double[FFTLength];
            double[] m_TempKernelImage = new double[FFTLength];

            sparKernel = new Complex[K][];

            for (int k = K; k > 0; k--)
            {
                int len = (int)Math.Ceiling(Q * Fs / (minFreq * Math.Pow(2, ((k - 1) * 1.0d / Bins))));
                double[] m_Hamm = Hamming(len);
                double[] tmp = new double[len];
                double u = 2 * Math.PI * Q / len;

                for (int n = 0; n < len; n++)
                {
                    double hl = m_Hamm[n] / len;
                    double un = u * n;
                    m_TempKernelReal[n] = hl * Math.Cos(un);//实部
                    m_TempKernelImage[n] = hl * Math.Sin(un);//虚部
                }

                Complex[] specKernerl = DSPFunctions.FnFastFourierTransfer(m_TempKernelReal, m_TempKernelImage, FFTLength, 1);

                for (int z = 0; z < specKernerl.Length; z++)
                {
                    if (specKernerl[z].Modulus >= Thresh)
                    {
                        specKernerl[z].Real /= FFTLength;
                        specKernerl[z].Image /= -FFTLength;
                    }
                    else
                    {
                        specKernerl[z].Image = specKernerl[z].Real = 0;
                    }

                }
                sparKernel[k - 1] = specKernerl;
            }
        }

        public double[] Hamming(int length)
        {
            double[] m_Result = new double[length];
            for (int i = 0; i < length; i++)
            {
                m_Result[i] = 0.54 - (0.46 * Math.Cos(Math.PI * 2 * i / (length - 1)));
            }
            return m_Result;
        }

        public double NextPow2(double m_Input)
        {
            int m_Result = 1;
            while (true)
            {
                m_Result++;
                if (Math.Pow(2, m_Result) >= m_Input)
                {
                    return m_Result;
                }
            }
        }
    }
