 static void Main(string[] args)
        {
            int i = 0, n = 0, j = 0;
            double a1 = 0, a2 = 0, c = 0, c1 = 0, c2 = 0, d1 = 0, d2 = 0, q1 = 0, q2 = 0, w = 0, w1 = 0, w2 = 0;
            double[] x = new double[64];
            double[] y = new double[64];
            double[] a = new double[64];
            double[] b = new double[64];
            double m_Sqrt = Math.Sqrt(64);
            n = 64;
            a1 = 0.9;
            a2 = 0.3;
            x[0] = 1.0;
            y[0] = 0.0;
            for (i = 1; i < n; i++)
            {
                x[i] = a1 * x[i - 1] - a2 * y[i - 1];
                y[i] = a2 * x[i - 1] + a1 * y[i - 1];
            }
            Console.WriteLine("原始序列如下：");
            for (i = 0; i < n / 2; i++)
            {
                for (j = 0; j < 2; j++)
                {
                    Console.Write("\t{0:F7}+j*{1:F7}               ", x[2 * i + j], y[2 * i + j]);
                }
                Console.Write("\n");
            }
            q1 = x[n - 1];
            q2 = y[n - 1];
           Complex[] m_DDT = DSPFunctions.FnDiscreteFourierTrans(x, y, n, 1);
           Console.WriteLine("******************************************************");
           for (i = 0; i < n/2; i++)
           {
               for (j = 0; j < 2; j++)
               {
                   Console.Write("\t{0:F7}+j*{1:F7}               ", m_DDT[2 * i + j].Real, m_DDT[2 * i + j].Image);
               }
               Console.WriteLine();
           }

           Console.WriteLine("******************************************************");
           Complex[] m_FFT = DSPFunctions.FnFastFourierTransfer(x, y, n, 1);
           for (i = 0; i < n / 2; i++)
           {
               for (j = 0; j < 2; j++)
               {
                   Console.Write("\t{0:F7}+j*{1:F7}               ", m_FFT[2 * i + j].Real, m_FFT[2 * i + j].Image);
               }
               Console.WriteLine();
           }

           Console.WriteLine("******************************************************");
           double[] m_Real = new double[64];
           double[] m_xReal = new double[63];
           for (i = 0; i < 10; i++)
           {
               m_Real[i] = 0;
               m_xReal[i] = 0;
           }
            for(i=10;i<n;i++)
            {
                m_Real[i] = Math.Pow(Math.E, -(i - 10) / 15.0) * Math.Sin(2 * Math.PI * (i - 10) / 16.0);
            }
            for (i = 10; i < 63; i++)
            {
                m_xReal[i] = Math.Pow(Math.E, -(i - 10) / 15.0) * Math.Sin(2 * Math.PI * (i - 10) / 16.0);
            }
            Complex[] m_xFFTR = DSPFunctions.FnDiscreteFourierTrans(m_xReal, 63, 1);
            Complex[] m_FFTR = DSPFunctions.FnFastFourierTransfer(m_Real, 64, 1);
            for (i = 0; i < n / 2; i++)
            {
                for (j = 0; j < 2; j++)
                {
                    Console.Write("\t{0:F7}+j*{1:F7}               ", m_FFTR[2 * i + j].Real, m_FFTR[2 * i + j].Image);
                }
                Console.WriteLine();
            }

            Console.WriteLine("******************************************************");
            double[] m_CosInput = new double[16];
            n = 16;
            for (i = 0; i < 16; i++)
            {
                m_CosInput[i] = Math.Pow(Math.E, -0.5 * i);
            }
            double[] m_CosT = DSPFunctions.FnFastCosinTransformation(m_CosInput, n);
            for (i = 0; i < n; i+=4)
            {
                Console.Write("\t{0:F7}           {1:F7}", m_CosT[i], m_CosT[i + 1]);
                Console.Write("\t{0:F7}           {1:F7}", m_CosT[i+2], m_CosT[i + 3]);
                Console.WriteLine();
            }

            //离散余弦变换结果经过离散余弦反变换得到余弦变换之前的结果
            Console.WriteLine("******************************************************");
            double[] m_ICosT = DSPFunctions.FnFastInverseCosinTrans(m_CosT, 16);
            for (i = 0; i < n; i += 4)
            {
                Console.Write("\t{0:F7}           {1:F7}", m_ICosT[i], m_ICosT[i + 1]);
                Console.Write("\t{0:F7}           {1:F7}", m_ICosT[i + 2], m_ICosT[i + 3]);
                Console.WriteLine();
            }
            Console.WriteLine();
            Console.WriteLine("******************************************************");
            double[] m_SinT = DSPFunctions.FnFastSineTrans(m_CosInput, n);
            for (i = 1; i < n; i ++)
            {
                Console.Write("\t{0:F7}", m_SinT[i]);
                if (i % 4 == 0)
                {
                    Console.WriteLine();
                }
            }
            Console.WriteLine();
            Console.WriteLine("******************************************************");
            double[] m_ISinT = DSPFunctions.FnFastSineTrans(m_SinT, n);
            for (i = 1; i < n; i ++ )
            {
                Console.Write("\t{0:F7}", m_ISinT[i]);
                if (i % 4 == 0)
                {
                    Console.WriteLine();
                }
            }
            Console.WriteLine();

            double[] ConX = { -1, 5, 2, 7, 6, 9, 4 };
            double[] ConY = { 1, 2, 1, -3, 4, 5 };
            Console.WriteLine("******************************************************");
            double[] m_ConResult = DSPFunctions.FnFastConvolution(ConX, ConY, 7, 6, 16);
            for (i = 0; i < 12; i++)
            {
                Console.Write("\t{0:F7}", m_ConResult[i]);
                if (i % 4 == 3)
                {
                    Console.WriteLine();
                }
            }

            Console.WriteLine();
            Console.WriteLine("****************自相关*****************************");
            double[] m_CorreCoe = DSPFunctions.FnFastCorrelationCoe(ConX, ConX, ConX.Length, ConX.Length, 16);
            for (i = 0; i < m_CorreCoe.Length; i++)
            {
                Console.Write("\t{0:F7}", m_CorreCoe[i]);
                if (i % 4 == 3)
                {
                    Console.WriteLine();
                }
            }

            Console.WriteLine();
            Console.WriteLine("******************************************************");
            //30阶FIR低通数字滤波器，边界频率0.2，n=30,band=1,fl=0.2,fs=1,wn=1,矩形窗
            double[] m_FIR = FIRFilter.FnFIRWin(30, 1, 0.2, 0, 1, 0);
            for (i = 0; i < m_FIR.Length/2+1; i++)
            {
                Console.Write("{0:F8}\n", m_FIR[i]);
                
            }

            Console.WriteLine();
            Console.WriteLine("******************************************************");
            //30阶FIR高通数字滤波器，边界频率0.3，n=30,band=2,fl=0.3,fs=1,wn=4,海明窗
            double[] m_HIR = FIRFilter.FnFIRWin(40, 4, 0.1, 0.25, 4, 0);
            for (i = 0; i < m_HIR.Length / 2; i++)
            {
                Console.Write("{0:F8}\n", m_HIR[i]);

            }

            Console.WriteLine();
            Console.WriteLine("******************************************************");
            double[] SoundData = new double[8192];

            FileStream m_Fs = new FileStream("sample.dat", FileMode.Open);
            StreamReader m_Reader = new StreamReader(m_Fs);
            string m_Tmep = m_Reader.ReadToEnd();
            string[] m_strArray = m_Tmep.Split(new string[]{"\r\n"},StringSplitOptions.None);

                for (i = 0; i < 8192; i++)
                {
                    SoundData[i] = double.Parse(m_strArray[i]);
                }
            m_Reader.Close();
            m_Fs.Close();
            double[] m_Coe = new double[14];
            for (i = 0; i < 13; i++)
            {
                m_Coe[i] = MFCCExtract.GetCoefficient(SoundData, 44100, 26, 1024, i);
                Console.WriteLine("第{0}个系数为{1}", i + 1, m_Coe[i]);
            }

            Console.WriteLine();
            Console.WriteLine("******************************************************");
            ConstQTransform m_ConstQ = new ConstQTransform(55, 7040, 24, 44100, 0.0054);
            double[] m_ConstR = DSPFunctions.FnCalculateConstantQTransfrom(SoundData, m_ConstQ);

            Console.WriteLine();
            Console.WriteLine("*******************Q变换阵计算完成**********************");

            double[] m_Xcc = { 2, 3, 1 };
            double[] m_Co = DSPFunctions.FnFastCorrelationCoe(m_Xcc, m_Xcc, 3, 3, 8);
            
            Console.ReadKey();
        }
        
    }
