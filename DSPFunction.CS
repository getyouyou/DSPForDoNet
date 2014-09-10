/// <summary>
    /// 包含所有DSP数字信号处理方法类
    /// </summary>
    public class DSPFunctions
    {
        /// <summary>
        /// 找到变换中的频率最大的那个
        /// </summary>
        /// <param name="m_Fourier"></param>
        /// <returns></returns>
        public static int FnFundamentalFrequency(Complex[] m_Fourier)
        {
            int MaxModulus = 0;
            for (int i = 1; i < m_Fourier.Length; i++)
            {
                if (m_Fourier[i].Modulus > m_Fourier[MaxModulus].Modulus)
                {
                    MaxModulus = i;
                }
            }
            return MaxModulus;
        }

        /// <summary>
        /// 离散傅立叶变换
        /// </summary>
        /// <param name="data">信号采集数据</param>
        /// <param name="Length">采样数据长度</param>
        /// <param name="sign">为-1时反换变，为1时正变换</param>
        /// <returns></returns>
        public static Complex[] FnDiscreteFourierTrans(double[] data, int Length, int sign)
        {
            Complex[] m_Result = new Complex[Length];
            Complex[] m_Origin = new Complex[Length];

            int i = 0, k = 0;
            double c = 0, d = 0, q = 0, w = 0, s = 0;

            //填充原始实数列作为复数形式输入，实部为实数部分，虚部为0
            for(i=0;i<Length;i++)
            {
                m_Origin[i]= new Complex(data[i],0);
            }

            q = 2 * Math.PI / Length;
            for (k = 0; k < Length; k++)
            {
                w = k * q;
                m_Result[k]= new Complex();
                for (i = 0; i < Length; i++)
                {
                    d = i * w;
                    c = Math.Cos(d);
                    s = Math.Sin(d) * sign;
                    m_Result[k].Real = m_Result[k].Real + c * m_Origin[i].Real + s * m_Origin[i].Image;
                    m_Result[k].Image =m_Result[k].Image + c * m_Origin[i].Image - s * m_Origin[i].Real;
                }
            }
            if (sign == -1)
            {
                c = 1.0 / Length;
                for (k = 0; k < Length; k++)
                {
                    m_Result[k].Real = c * m_Result[k].Real;
                    m_Result[k].Image = c * m_Result[k].Image;
                }
            }

            return m_Result;
        }


        /// <summary>
        /// 离散傅立叶变换
        /// </summary>
        /// <param name="datax">实部</param>
        /// <param name="datay">虚部</param>
        /// <param name="Length">采样数据长度</param>
        /// <param name="sign">为-1时反换变，为1时正变换</param>
        /// <returns></returns>
        public static Complex[] FnDiscreteFourierTrans(double[] datax, double[] datay, int Length, int sign)
        {
            Complex[] m_Result = new Complex[Length];
            Complex[] m_Origin = new Complex[Length];

            int i = 0, k = 0;
            double c = 0, d = 0, q = 0, w = 0, s = 0;

            //填充原始实数列作为复数形式输入，实部为实数部分，虚部为0
            for (i = 0; i < Length; i++)
            {
                m_Origin[i] = new Complex(datax[i], datay[i]);
            }

            q = 2 * Math.PI / Length;
            for (k = 0; k < Length; k++)
            {
                w = k * q;
                m_Result[k] = new Complex();
                for (i = 0; i < Length; i++)
                {
                    d = i * w;
                    c = Math.Cos(d);
                    s = Math.Sin(d) * sign;
                    m_Result[k].Real = m_Result[k].Real + c * m_Origin[i].Real + s * m_Origin[i].Image;
                    m_Result[k].Image = m_Result[k].Image + c * m_Origin[i].Image - s * m_Origin[i].Real;
                }
            }
            if (sign == -1)
            {
                c = 1.0 / Length;
                for (k = 0; k < Length; k++)
                {
                    m_Result[k].Real = c * m_Result[k].Real;
                    m_Result[k].Image = c * m_Result[k].Image;
                }
            }

            return m_Result;
        }

        /// <summary>
        /// 离散傅立叶变换
        /// </summary>
        /// <param name="data">复数形式</param>
        /// <param name="Length">采样数据长度</param>
        /// <param name="sign">为-1时反换变，为1时正变换</param>
        /// <returns></returns>
        public static Complex[] FnDiscreteFourierTrans(Complex[] data, int Length, int sign)
        {
            Complex[] m_Result = new Complex[Length];
            Complex[] m_Origin = new Complex[Length];

            int i = 0, k = 0;
            double c = 0, d = 0, q = 0, w = 0, s = 0;

            //填充原始实数列作为复数形式输入，实部为实数部分，虚部为0
            for (i = 0; i < Length; i++)
            {
                m_Origin[i] = new Complex(data[i].Real, data[i].Image);
            }

            q = 2 * Math.PI / Length;
            for (k = 0; k < Length; k++)
            {
                w = k * q;
                m_Result[k].Real = m_Result[k].Image = 0;
                for (i = 0; i < Length; i++)
                {
                    d = i * w;
                    c = Math.Cos(d);
                    s = Math.Sin(d) * sign;
                    m_Result[k].Real = m_Result[k].Real + c * m_Origin[i].Real + s * m_Origin[i].Image;
                    m_Result[k].Image = m_Result[k].Image + c * m_Origin[i].Image - s * m_Origin[i].Real;
                }
            }
            if (sign == -1)
            {
                c = 1.0 / Length;
                for (k = 0; k < Length; k++)
                {
                    m_Result[k].Real = c * m_Result[k].Real;
                    m_Result[k].Image = c * m_Result[k].Image;
                }
            }

            return m_Result;
        }

        /// <summary>
        /// 基2快速傅立叶变换，常用的版本
        /// </summary>
        /// <param name="data">浮点数据</param>
        /// <param name="Length">数据长度，必须是2的整数次幂</param>
        /// <param name="sign">当sign=1时傅立叶正变换DFT，当sign=-1时计算傅立叶反变换IDFT</param>
        /// <returns>由于其对称性，一般使用前半部分，直流分量为第一个点的模值/nfft的长度，以后的其他的直流分量为模值/(nfft/2)，对应了各个频率下信号的振幅</returns>
        public static Complex[] FnFastFourierTransfer(double[] data, int Length, int sign)
        {
            int i = 0, j = 0, k = 0, l = 0, m = 0, n1 = 0, n2 = 0;
            double c = 0, c1 = 0, e = 0, s = 0, s1 = 0, t = 0, tr = 0, ti = 0;
            Complex[] m_Origin = new Complex[Length];
            //如果数据长度不够长则在后面补0
            if (data.Length < Length)
            {
                double[] m_Conact = new double[Length-data.Length];
                data = data.Concat(m_Conact).ToArray() ;
            }
            for(i=0;i<Length;i++)
            {
                m_Origin[i] =new Complex(data[i],0);
            }
            for (j = 1, i = 1; i < 16; i++)
            {
                m = i;
                j = 2 * j;
                if (j == Length)
                {
                    break;
                }
            }
            n1 = Length - 1;
            for (j = 0, i = 0; i < n1; i++)
            {
                if (i < j)
                {
                    tr = m_Origin[j].Real;
                    ti = m_Origin[j].Image;
                    m_Origin[j].Real = m_Origin[i].Real;
                    m_Origin[j].Image = m_Origin[i].Image;
                    m_Origin[i].Real = tr;
                    m_Origin[i].Image = ti;
                }
                k = Length / 2;
                while (k < (j + 1))
                {
                    j = j - k;
                    k = k / 2;
                }
                j = j + k;
            }
            n1 = 1;
            for (l = 1; l <= m; l++)
            {
                n1 = 2 * n1;
                n2 = n1 / 2;
                e = Math.PI/n2;
                c = 1.0;
                s = 0.0;
                c1 = Math.Cos(e);
                s1 = -sign * Math.Sin(e);
                for (j = 0; j < n2; j++)
                {
                    for (i = j; i < Length; i += n1)
                    {
                        k = i + n2;
                        tr = c * m_Origin[k].Real - s * m_Origin[k].Image;
                        ti = c * m_Origin[k].Image + s * m_Origin[k].Real;
                        m_Origin[k].Real = m_Origin[i].Real - tr;
                        m_Origin[k].Image = m_Origin[i].Image - ti;
                        m_Origin[i].Real = m_Origin[i].Real + tr;
                        m_Origin[i].Image = m_Origin[i].Image + ti;
                    }
                    t = c;
                    c = c * c1 - s * s1;
                    s = t * s1 + s * c1;
                }
            }
            if (sign == -1)
            {
                for (i = 0; i < Length; i++)
                {
                    m_Origin[i].Real /= Length;
                    m_Origin[i].Image /= Length;
                }
            }

            return m_Origin;
        }

        /// <summary>
        /// 基2快速傅立叶变换，常用的版本
        /// </summary>
        /// <param name="rdata">复数实数部分</param>
        /// <param name="idata">复数虚数部分</param>
        /// <param name="Length">数据长度，必须是2的整数次幂</param>
        /// <param name="sign">当sign=1时傅立叶正变换DFT，当sign=-1时计算傅立叶反变换IDFT</param>
        /// <returns></returns>
        public static Complex[] FnFastFourierTransfer(double[] rdata, double[] idata, int Length, int sign)
        {
            int i = 0, j = 0, k = 0, l = 0, m = 0, n1 = 0, n2 = 0;
            double c = 0, c1 = 0, e = 0, s = 0, s1 = 0, t = 0, tr = 0, ti = 0;
            Complex[] m_Origin = new Complex[Length];
            if (rdata.Length < Length)
            {
                rdata = rdata.Concat(new double[Length - rdata.Length]).ToArray();
                idata = idata.Concat(new double[Length - idata.Length]).ToArray();
            }
            for (i = 0; i < Length; i++)
            {
                m_Origin[i] = new Complex(rdata[i], idata[i]);
            }
            for (j = 1, i = 1; i < 16; i++)
            {
                m = i;
                j = 2 * j;
                if (j == Length)
                {
                    break;
                }
            }
            n1 = Length - 1;
            for (j = 0, i = 0; i < n1; i++)
            {
                if (i < j)
                {
                    tr = m_Origin[j].Real;
                    ti = m_Origin[j].Image;
                    m_Origin[j].Real = m_Origin[i].Real;
                    m_Origin[j].Image = m_Origin[i].Image;
                    m_Origin[i].Real = tr;
                    m_Origin[i].Image = ti;
                }
                k = Length / 2;
                while (k < (j + 1))
                {
                    j = j - k;
                    k = k / 2;
                }
                j = j + k;
            }
            n1 = 1;
            for (l = 1; l <= m; l++)
            {
                n1 = 2 * n1;
                n2 = n1 / 2;
                e = Math.PI / n2;
                c = 1.0;
                s = 0.0;
                c1 = Math.Cos(e);
                s1 = -sign * Math.Sin(e);
                for (j = 0; j < n2; j++)
                {
                    for (i = j; i <Length; i += n1)
                    {
                        k = i + n2;
                        tr = c * m_Origin[k].Real - s * m_Origin[k].Image;
                        ti = c * m_Origin[k].Image + s * m_Origin[k].Real;
                        m_Origin[k].Real = m_Origin[i].Real - tr;
                        m_Origin[k].Image = m_Origin[i].Image - ti;
                        m_Origin[i].Real = m_Origin[i].Real + tr;
                        m_Origin[i].Image = m_Origin[i].Image + ti;
                    }
                    t = c;
                    c = c * c1 - s * s1;
                    s = t * s1 + s * c1;
                }
            }
            if (sign == -1)
            {
                for (i = 0; i < Length; i++)
                {
                    m_Origin[i].Real /= Length;
                    m_Origin[i].Image /= Length;
                }
            }

            return m_Origin;
        }

        /// <summary>
        /// 基2快速傅立叶变换，常用的版本
        /// </summary>
        /// <param name="data">复数数据类型</param>
        /// <param name="Length">数据长度，必须是2的整数次幂</param>
        /// <param name="sign">当sign=1时傅立叶正变换DFT，当sign=-1时计算傅立叶反变换IDFT</param>
        /// <returns></returns>
        public static Complex[] FnFastFourierTransfer(Complex[] data, int Length, int sign)
        {
            int i = 0, j = 0, k = 0, l = 0, m = 0, n1 = 0, n2 = 0;
            double c = 0, c1 = 0, e = 0, s = 0, s1 = 0, t = 0, tr = 0, ti = 0;
            Complex[] m_Origin = new Complex[Length];
            if (data.Length < Length)
            {
               data = data.Concat(new Complex[Length - data.Length]).ToArray();
            }
            for (i = 0; i < Length; i++)
            {
                m_Origin[i] = new Complex(data[i].Real, data[i].Image);
            }
            for (j = 1, i = 1; i < 16; i++)
            {
                m = i;
                j = 2 * j;
                if (j == Length)
                {
                    break;
                }
            }
            n1 = Length - 1;
            for (j = 0, i = 0; i < n1; i++)
            {
                if (i < j)
                {
                    tr = m_Origin[j].Real;
                    ti = m_Origin[j].Image;
                    m_Origin[j].Real = m_Origin[i].Real;
                    m_Origin[j].Image = m_Origin[i].Image;
                    m_Origin[i].Real = tr;
                    m_Origin[i].Image = ti;
                }
                k = Length / 2;
                while (k < (j + 1))
                {
                    j = j - k;
                    k = k / 2;
                }
                j = j + k;
            }
            n1 = 1;
            for (l = 1; l <= m; l++)
            {
                n1 = 2 * n1;
                n2 = n1 / 2;
                e = Math.PI / n2;
                c = 1.0;
                s = 0.0;
                c1 = Math.Cos(e);
                s1 = -sign * Math.Sin(e);
                for (j = 0; j < n2; j++)
                {
                    for (i = j; i < Length; i += n1)
                    {
                        k = i + n2;
                        tr = c * m_Origin[k].Real - s * m_Origin[k].Image;
                        ti = c * m_Origin[k].Image + s * m_Origin[k].Real;
                        m_Origin[k].Real = m_Origin[i].Real - tr;
                        m_Origin[k].Image = m_Origin[i].Image - ti;
                        m_Origin[i].Real = m_Origin[i].Real + tr;
                        m_Origin[i].Image = m_Origin[i].Image + ti;
                    }
                    t = c;
                    c = c * c1 - s * s1;
                    s = t * s1 + s * c1;
                }
            }
            if (sign == -1)
            {
                for (i = 0; i < Length; i++)
                {
                    m_Origin[i].Real /= Length;
                    m_Origin[i].Image /= Length;
                }
            }
            return m_Origin;
        }

        /// <summary>
        /// 自动生成长度的快速傅立叶变换
        /// </summary>
        /// <param name="data">实数</param>
        /// <param name="sign">sign=1为正变换，sign=-1为逆变换</param>
        /// <returns></returns>
        public static Complex[] FnFastFourierTransfer(double[] data, int sign)
        {
            int FFTLength = (int)Math.Pow(2, Math.Ceiling(Math.Log(data.Length, 2)));
            return FnFastFourierTransfer(data, FFTLength, sign);
        }

        /// <summary>
        /// 自动生成长度的快速傅立叶变换
        /// </summary>
        /// <param name="rdata">实部</param>
        /// <param name="idata">虚部</param>
        /// <param name="sign">sign=1为正变换，sign=-1为逆变换</param>
        /// <returns></returns>
        public static Complex[] FnFastFourierTransfer(double[] rdata,double[] idata, int sign)
        {
            int FFTLength = (int)Math.Pow(2, Math.Ceiling(Math.Log(rdata.Length, 2)));
            return FnFastFourierTransfer(rdata, idata,FFTLength, sign);
        }

        /// <summary>
        /// 自动生成长度的快速傅立叶变换
        /// </summary>
        /// <param name="data">复数</param>
        /// <param name="sign">sign=1为正变换，sign=-1为逆变换</param>
        /// <returns></returns>
        public static Complex[] FnFastFourierTransfer(Complex[] data, int sign)
        {
            int FFTLength = (int)Math.Pow(2, Math.Ceiling(Math.Log(data.Length, 2)));
            return FnFastFourierTransfer(data, FFTLength, sign);
        }

        /// <summary>
        /// 快速离散余弦变换
        /// </summary>
        /// <param name="data">采样数据</param>
        /// <param name="Length">数据长度，必须为2的整数次幂即Length=2^n</param>
        /// <returns></returns>
        public static double[] FnFastCosinTransformation(double[] data, int Length)
        {
            int i = 0, n1 = 0;
            double q = 0, c = 0, s = 0;
            double[] y = new double[Length];
            double[] m_Result = new double[Length];
            n1 = Length / 2;
            for (i = 0; i < n1; i++)
            {
                y[i] = data[2 * i];
                y[Length - 1 - i] = data[2 * i + 1];
            }
            Complex[] m_FFTResult = FnFastFourierTransfer(y, Length, 1);

            q = 4.0 * Math.Atan(1.0) / (2 * Length);
            m_Result[0] = m_FFTResult[0].Real;
            m_Result[n1] = Math.Sin(n1 * q) * m_FFTResult[n1].Real;

            for (i = 1; i < n1; i++)
            {
                c = Math.Cos(i * q);
                s = Math.Sin(i * q);
                m_Result[i] = c * m_FFTResult[i].Real + s * m_FFTResult[i].Image;
                m_Result[Length - i] = s * m_FFTResult[i].Real - c * m_FFTResult[i].Image;
            }
            c = 1.0d / Math.Sqrt((double)Length);
            m_Result[0] = c * m_Result[0];
            c = Math.Sqrt(2.0d / Length);
            for (i = 1; i < Length; i++)
            {
                m_Result[i] = c * m_Result[i];
            }

            return m_Result;
        }

        /// <summary>
        /// 快速 离散余弦反变换IDCT
        /// </summary>
        /// <param name="data">需要反变换的数据</param>
        /// <param name="Length">数据的长度，必须为2的整数次幂即Length=2^n</param>
        /// <returns></returns>
        public static double[] FnFastInverseCosinTrans(double[] data, int Length)
        {
            int i = 0, n1 = 0;
            double q = 0, c = 0;
            Complex[] m_Origin = new Complex[Length];
            double[] m_Result = new double[Length];
            n1 = Length / 2;
            q = 4 * Math.Atan(1.0d) / (2 * Length);
            data[0] = data[0] / Math.Sqrt(2.0);
            for (i = 0; i < Length; i++)
            {
                m_Origin[i] = new Complex(data[i] * Math.Cos(i * q), data[i] * Math.Sin(i * q));
            }
            //执行傅立叶变换
            Complex[] m_FFT = FnFastFourierTransfer(m_Origin, Length, -1);
            for (i = 0; i < Length; i++)
            {
                m_FFT[i].Image = Length * m_FFT[i].Real;
            }
            c = Math.Sqrt(2.0d / Length);
            for (i = 0; i < n1; i++)
            {
                m_FFT[2 * i].Real = c * m_FFT[i].Image;
                m_FFT[2 * i + 1].Real = c * m_FFT[Length - i - 1].Image;
            }
            for(i=0;i<Length;i++)
            {
                m_Result[i] = m_FFT[i].Real;
            }

            return m_Result;
        }

        /// <summary>
        /// 快速离散正弦变换(正变换和反变换相同)
        /// </summary>
        /// <param name="data">变换数据</param>
        /// <param name="Length">数据长度，必须为2的整数次幂</param>
        /// <returns></returns>
        public static double[] FnFastSineTrans(double[] data, int Length)
        {
            int i = 0, n1 = 0;
            double q = 0, s = 0, x1 = 0;
            double[] y = new double[Length];
            double[] m_Result = new double[Length];
            for (i = 0; i < Length; i++)
            {
                m_Result[i] = data[i];
            }
            n1 = Length / 2;
            q = Math.PI / Length;
            y[0] = 0.0;
            for (i = 1; i < Length; i++)
            {
                y[i] = Math.Sin(i * q) * (data[i] + data[Length - i]) + 0.5 * (data[i] - data[Length - i]);
            }
            Complex[] m_FFT = FnFastFourierTransfer(y, Length, 1);
            x1 = 0.0;
            for (i = 1; i < Length; i++)
            {
                s = i * q;
                s = Math.Sin(s);
                x1 += (s * data[i]);
            }
            m_Result[0] = 0.0;
            m_Result[1] = x1;
            for (i = 1; i < n1; i++)
            {
                m_Result[2 * i] = -m_FFT[i].Image;
                m_Result[2 * i + 1] = m_Result[2 * i - 1] + m_FFT[i].Real;
            }
            s = Math.Sqrt(2.0 / Length);
            for (i = 1; i < Length; i++)
            {
                m_Result[i] *= s;
            }

            return m_Result;
        }

        /// <summary>
        /// 快速卷积算法
        /// </summary>
        /// <param name="x">数组1</param>
        /// <param name="y">数组2</param>
        /// <param name="m">数组1长度</param>
        /// <param name="n">数组2长度</param>
        /// <param name="length">线性卷积的长度，len>=m+n-1，必须是2的整数次幂即length=2^p</param>
        /// <returns></returns>
        public static double[] FnFastConvolution(double[] x, double[] y, int m, int n, int length)
        {
            double[] m_result = new double[length];
            double[] m_Newx = new double[length];
            double[] m_Newy = new double[length];

            int i = 0;
            //扩展两个输入序列
            for (i = 0; i < length; i++)
            {
                if (i < m )
                {
                    m_Newx[i] = x[i];
                }
                else
                {
                    m_Newx[i] = 0.0;
                }
            }
            for (i = 0; i < length; i++)
            {
                if (i < n)
                {
                    m_Newy[i] = y[i] ;
                }
                else
                {
                    m_Newy[i] = 0.0;
                }
            }
            //快速傅立叶变换
            Complex[] m_XFFT = FnFastFourierTransfer(m_Newx, length, 1);
            Complex[] m_YFFT = FnFastFourierTransfer(m_Newy, length, 1);

            Complex[] m_ZFFT = new Complex[length];

            for (i = 0; i < length; i++)
            {
                m_ZFFT[i] = new Complex();
                m_ZFFT[i] = m_XFFT[i].MuliplateOperate(m_YFFT[i]);
            }

            //执行傅立叶反变换
            Complex[] m_RFFT = FnFastFourierTransfer(m_ZFFT, length, -1);

            int len1 = m + n - 1;
            //提取实部作为输出结果
            for (i = 0; i < len1; i++)
            {
                m_result[i] = m_RFFT[i].Real;
            }
            return m_result;
        }

        /// <summary>
        /// 直接运用公式计算两个序列的卷积
        /// </summary>
        /// <param name="x">序列1</param>
        /// <param name="y">序列2</param>
        /// <param name="m">序列1长度</param>
        /// <param name="n">序列2长度</param>
        /// <param name="length">length>=m+n-1且为2的整数次幂</param>
        /// <returns></returns>
        public static double[] FnDirectConvolution(double[] x, double[] y, int length)
        {
            double[] z = new double[length];
            double[] m_NewX = new double[length];
            double[] m_NewY = new double[length];
            //扩展两个序列
            for (int i = 0; i < length; i++)
            {
                if (i < x.Length)
                {
                    m_NewX[i] =  x[i];
                }
                else
                {
                    m_NewX[i] =0;
                }
            }
            for (int i = 0; i < length; i++)
            {
                if (i < y.Length)
                {
                    m_NewY[i] = y[i];
                }
                else
                {
                    m_NewY[i] = 0;
                }
            }

            int len1 = x.Length + y.Length - 1;
            for (int j = 0; j < len1; j++)
            {
                z[j] = 0.0;
                for (int i = 0; i < x.Length; i++)
                {
                    if (((j - i) >= 0) && ((j - i) < y.Length))
                    {
                        z[j] = z[j] + m_NewX[i] * m_NewY[j - i];
                    }
                }
            }

            double[] m_Result = new double[len1];
            for (int i = 0; i < len1; i++)
            {
                m_Result[i] = z[i];
            }

            return m_Result;
        }

        /// <summary>
        /// 求取序列线性相关值，当x=y时为自相关，否则为互相关
        /// </summary>
        /// <param name="x">浮点序列1</param>
        /// <param name="y">浮点序列2</param>
        /// <param name="m">浮点序列1长度</param>
        /// <param name="n">浮点序列2长度</param>
        /// <param name="length">length>=m+n-1且为2的整数次幂</param>
        /// <returns></returns>
        public static double[] FnFastCorrelationCoe(double[] x, double[] y, int m, int n, int length)
        {
            int i = 0;
            double[] m_Result= new double[m+n-1];
            double[] m_Newx = new double[length];
            double[] m_Newy = new double[length];
            //扩展两个数组
            for (i = 0; i < length; i++)
            {
                if (i < m)
                {
                    m_Newx[i] = x[i];
                }
                else
                {
                    m_Newx[i] = 0;
                }
            }
            for (i = 0; i < m - 1; i++)
            {
                m_Newy[i] = 0.0;
            }
            for (i = m - 1; i < (m + n - 1); i++)
            {
                m_Newy[i] = y[i - m + 1];
            }
            for (i = (m + n - 1); i < length; i++)
            {
                m_Newy[i] = 0.0;
            }

            //对两个扩展的序列执行快速傅立叶变换
            Complex[] m_XFFT = FnFastFourierTransfer(m_Newx, length, 1);
            Complex[] m_YFFT = FnFastFourierTransfer(m_Newy, length, 1);

            //计算m_XFFT的各项共轭复数值
            for (i = 0; i < length; i++)
            {
                m_XFFT[i].Image = 0 - m_XFFT[i].Image;
            }

            Complex[] m_ZFFT = new Complex[length];
            for (i = 0; i < length; i++)
            {
                m_ZFFT[i] = new Complex();
                m_ZFFT[i] = m_XFFT[i].MuliplateOperate(m_YFFT[i]);
            }

            //执行傅立叶反变换过程
            Complex[] m_RFFT = FnFastFourierTransfer(m_ZFFT, length, -1);

            for (i = 0; i < m+n-1; i++)
            {
                m_Result[i] = m_RFFT[i].Real;
            }

            //返回相关系数结果
            return m_Result;
        }

        /// <summary>
        /// 直接计算序列互相关函数，也可以作为自相关函数，由公式直接计算
        /// </summary>
        /// <param name="x">序列1</param>
        /// <param name="y">序列2</param>
        /// <param name="m">序列1长度</param>
        /// <param name="n">序列2长度</param>
        /// <param name="length">length>=m+n-1，且为2的整数次幂</param>
        /// <returns></returns>
        public static double[] FnDirectCorrelationCoe(double[] x, double[] y, int m, int n, int length)
        {
            double[] z = new double[length];
            int len1 = m + n - 1;
            double[] m_Result = new double[len1];
            int m1 = m - 1;
            for (int j = -m1; j < n; j++)
            {
                z[j + m1] = 0.0;
                for (int i = 0; i < m; i++)
                {
                    if (((i + j) >= 0) && ((i + j) < n))
                    {
                        z[j + m1] = z[j + m1] + x[i] * y[i + j];
                    }
                }
            }

            for (int i = 0; i < len1; i++)
            {
                m_Result[i] = z[i];
            }

            //返回前m+n-1项为相关系数
            return m_Result;
        }

        /// <summary>
        /// 梅尔倒谱系数，不直接调用
        /// </summary>
        /// <param name="data">抽样数据的能量频谱值</param>
        /// <param name="SampleRate">信号采样频率</param>
        /// <param name="FilterNum">过滤器数量，一般取22-26个，通常为26</param>
        /// <param name="Nfft">傅立叶变换的长度</param>
        /// <returns>返回12维MFCC系数+12维一阶差分系数</returns>
        public static double[] FnMelFrequencyCepstralCoe(double[] data,int SampleRate,int FilterNum,int Nfft)
        {
            double[] m_CResult = new double[12];
            //计算第1个至第12个MFCC系数，其他的由于退化厉害不使用
            for (int i = 1; i < 13; i++)
            {
                m_CResult[i - 1] = MFCCExtract.GetCoefficient(data, SampleRate, FilterNum, Nfft, i);
            }
            //计算取K值为2的一阶差分系数
            double[] m_DResult = new double[12];
            for (int i = 2; i < 10; i++)
            {
                m_DResult[i] = ((m_CResult[i + 1] - m_CResult[i - 1]) + (m_CResult[i + 2] - m_CResult[i - 2])) / Math.Sqrt(10);
            }
            m_DResult[0] = m_CResult[1] - m_CResult[0];
            m_DResult[1] = m_CResult[2] - m_CResult[1];
            m_DResult[10] = m_CResult[10] - m_CResult[9];
            m_DResult[11] = m_DResult[11] - m_DResult[10];
            double[] m_Result = new double[24];
            for (int i = 0; i < 24; i++)
            {
                if (i < 12)
                {
                    m_Result[i] = m_CResult[i];
                }
                else
                {
                    m_Result[i] = m_DResult[i - 12];
                }
            }
            return m_Result;
        }

        /// <summary>
        /// 过零率
        /// </summary>
        /// <returns></returns>
        public static double FnZeroCrossRate(double[] data)
        {
            double m_ZCR = 0;
            for (int i = 1; i < data.Length; i++)
            {
                m_ZCR=m_ZCR+Math.Abs((FnSgn(data[i])-FnSgn(data[i-1])));
            }
            return m_ZCR/2.0d/data.Length;
        }

        /// <summary>
        /// Sgn函数，即符号函数
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public static int FnSgn(double x)
        {
            if (x < 0)
            {
                return -1;
            }
            else
            {
                return 1;
            }
        }

        /// <summary>
        /// 求取短时能量
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static double FnShortTimeEngerin(double[] data)
        {
            double m_Result = 0;
            for (int i = 0; i < data.Length; i++)
            {
                m_Result += Math.Pow(data[i], 2);
            }
            return m_Result;
        }

        /// <summary>
        /// 计算对数能量
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static double FnShortLogEnergy(double[] data)
        {
            return 10 * Math.Log10(FnShortTimeEngerin(data));
        }

        /// <summary>
        /// 计算短时平均幅度
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static double FnShortAverageRange(double[] data)
        {
            double m_Result = 0;
            for (int i = 0; i < data.Length; i++)
            {
                m_Result += Math.Abs(data[i]);
            }
            return m_Result;
        }

        /// <summary>
        /// 基于阀值Throld的改进的平均过零率
        /// </summary>
        /// <param name="data"></param>
        /// <param name="m_Throld"></param>
        /// <returns></returns>
        public static double FnImproveZeroCrossRateBaseThrold(double[] data, double m_Throld)
        {
            double m_Result = 0;
            for (int i = 1; i < data.Length; i++)
            {
                m_Result = m_Result + Math.Abs(FnSgn(data[i] - m_Throld) - FnSgn(data[i - 1] - m_Throld)) + Math.Abs(FnSgn(data[i] + m_Throld) - FnSgn(data[i - 1] + m_Throld));
            }
            return m_Result / 2;
        }

        /// <summary>
        /// 频域能量
        /// </summary>
        /// <param name="m_FFTResult">信号FFT变换结果</param>
        /// <returns></returns>
        public static double FnCalculateFreqEnergy(Complex[] m_FFTResult)
        {
            double m_Total = 0;
            for (int i = 0; i < m_FFTResult.Length; i++)
            {
                m_Total += Math.Pow(m_FFTResult[i].Modulus, 2);
            }
            if (m_Total == 0)
            {
                return 1e-15;
            }
            else
            {
                return Math.Log10(m_Total);
            }
        }

        /// <summary>
        /// 计算频谱质心，即亮度
        /// </summary>
        /// <param name="m_DFTResult">DFT结果</param>
        /// <returns></returns>
        public static double FnCalculateSpectralCentroid(Complex[] m_DFTResult)
        {
            double m_Total = 0;
            double m_KTotal = 0;
            for (int i = 0; i < m_DFTResult.Length; i++)
            {
                m_Total += m_DFTResult[i].Modulus;
                m_KTotal += i * m_DFTResult[i].Modulus;
            }
            return m_KTotal/m_Total;
        }
        
        /// <summary>
        /// 计算带宽，反映音频帧信号所占频率宽度
        /// </summary>
        /// <returns></returns>
        public static double FnCalculateBandwidth(Complex[] m_DFTResult)
        {
            double m_Total = 0;
            double m_BTotal = 0;
            double m_SpectralCen = FnCalculateSpectralCentroid(m_DFTResult);
            for (int i = 0; i < m_DFTResult.Length; i++)
            {
                m_BTotal += Math.Pow((i - m_SpectralCen), 2) * m_DFTResult[i].Modulus;
                m_Total += m_DFTResult[i].Modulus;
            }
            return m_BTotal / m_Total;
        }

        /// <summary>
        /// 求取信号的平均幅度值或者期望
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static double FnAverage(double[] data)
        {
            return data.Sum() / data.Length;
        }

        /// <summary>
        /// 求取信号方差
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static double FnVariance(double[] data)
        {
            double Ex = FnAverage(data);
            double m_Result = 0;
            for (int i = 0; i < data.Length; i++)
            {
                m_Result += Math.Pow(data[i], 2);
            }
            return (m_Result / data.Length) - Math.Pow(Ex, 2);
        }

        /// <summary>
        /// 求取信号标准差
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static double FnStandardDeviation(double[] data)
        {
            return Math.Sqrt(FnVariance(data));
        }

        /// <summary>
        /// 协方差
        /// </summary>
        /// <param name="X"></param>
        /// <param name="Y"></param>
        /// <returns></returns>
        public static double FnCovariance(double[] X, double[] Y)
        {
            double[] XY = new double[X.Length];
            for (int i = 0; i < X.Length; i++)
            {
                XY[i] = X[i] * Y[i];
            }
            return FnAverage(XY) - FnAverage(X) * FnAverage(Y);
        }

        /// <summary>
        /// 使用协方差计算出的统计相关系数
        /// </summary>
        /// <param name="X"></param>
        /// <param name="Y"></param>
        /// <returns></returns>
        public static double FnAssociationCoefficient(double[] X, double[] Y)
        {
            return FnCovariance(X, Y) / FnStandardDeviation(X) / FnStandardDeviation(Y);
        }

        /// <summary>
        /// 计算向量的欧几里德距离，用于判断相似性
        /// </summary>
        /// <param name="datax">向量1</param>
        /// <param name="datay">向量2</param>
        /// <returns></returns>
        public static double FnCalculateEuclideanDistance(double[] datax, double[] datay)
        {
            double Sum = 0;
            for (int i = 0; i < datax.Length; i++)
            {
                Sum += Math.Pow((datax[i] - datay[i]), 2);
            }
            return Math.Sqrt(Sum);
        }

        /// <summary>
        /// 信号预加重，y[n] = x[n]-a*x[n-1],y[0]=(1-a)x[0]
        /// </summary>
        /// <param name="data">待处理数据</param>
        ///  <param name="alpha">加重系数，通常取0.9-1之间，一般为0.97</param>
        /// <returns></returns>
        public static double[] FnPreEmphasis(double[] data,double alpha=0.97)
        {
            double[] m_Emphasis = new double[data.Length];
            for (int i = 1; i < data.Length; i++)
            {
                m_Emphasis[i] = data[i] - alpha * data[i - 1];
            }
            m_Emphasis[0] = (1 - alpha) * data[0];
            return m_Emphasis;
        }

        /// <summary>
        /// 计算信号的功率谱
        /// </summary>
        /// <param name="data">原始信号序列</param>
        /// <param name="nfft">傅立叶变换长度，必须为2的整数次幂即nfft=2^n</param>
        /// <param name="sign">sign=1线性功率谱，sign=-1时为对数功率谱</param>
        /// <returns></returns>
        public static double[] FnCalculatePowerSpectrum(double[] data, int nfft,int sign)
        {
            //执行傅立叶变换
            Complex[] m_FFT = FnFastFourierTransfer(data, nfft, 1);
            //对傅立叶变换求取模值的平方，即求取能量谱
            double[] m_Result = new double[nfft];
            for (int i = 0; i < nfft; i++)
            {
                m_Result[i] = Math.Pow(m_FFT[i].Modulus, 2);
            }
            
            //对能量谱中的每一个值除以相应的NFFT长度即可以得到相应的功率谱
            for (int i = 0; i < nfft; i++)
            {
                m_Result[i] /= nfft;
            }

            //如果求取对数谱
            if (sign==-1)
            {
                for(int i=0;i<nfft;i++)
                {
                    if (m_Result[i] == 0)
                    {
                        m_Result[i] = 1e-15;
                    }
                    m_Result[i] = 10 * Math.Log10(m_Result[i]);
                }
            }

            return m_Result;
        }

        /// <summary>
        /// 计算信号的对数谱
        /// </summary>
        /// <param name="data">原始信号序列</param>
        /// <param name="nfft">傅立叶变换长度，必须为2的整数次幂即nfft=2^n</param>
        /// <param name="sign">sign=1线性谱，sign=-1时为对数谱</param>
        /// <returns></returns>
        public static double[] FnCalculateSpectrum(double[] data, int nfft, int sign)
        {
            //执行傅立叶变换
            Complex[] m_FFT = FnFastFourierTransfer(data, nfft, 1);
            //对傅立叶变换求取模值的平方，即求取能量谱
            double[] m_Result = new double[nfft];
            for (int i = 0; i < nfft; i++)
            {
                m_Result[i] = m_FFT[i].Modulus;
            }

            //如果求取对数谱
            if (sign == -1)
            {
                for (int i = 0; i < nfft; i++)
                {
                    if (m_Result[i] == 0)
                    {
                        m_Result[i] = 1e-15;
                    }
                    m_Result[i] = 10 * Math.Log10(m_Result[i]);
                }
            }

            return m_Result;
        }

        /// <summary>
        /// 信号预处理，预加重与加窗
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static double[] FnPreProcessSign(double[] data)
        {
            //执行预加重、加窗操作
            return WindowFunction.FnUsingHammingWindow(FnPreEmphasis(data, 0.97));
        }
        /// <summary>
        /// 计算MFCC梅尔倒系数
        /// </summary>
        /// <param name="data">采样数据序列</param>
        /// <param name="nfft">快速傅立叶变换长度，通常取512或256</param>
        /// <param name="sign">sign=1使用线性功率谱计算，sign=-1使用对数功率谱计算</param>
        /// <param name="SampleRate">采样频率，通常取44100</param>
        /// <param name="filterNum">梅尔滤波器个数，通常取26</param>
        /// <returns></returns>
        public static double[] FnCalculateMFCCCoe(double[] data, int nfft = 512, int sign = 1, int SampleRate = 44100, int filterNum = 26)
        {
            //使用对数谱计算相应的MFCC系数
            return FnMelFrequencyCepstralCoe(FnCalculateSpectrum(data, nfft, -1), SampleRate, filterNum, nfft);
        }

        /// <summary>
        /// 计算每帧的所有系数
        /// </summary>
        /// <param name="data">待计算数据，已经进行了预处理，包括预加重，加窗</param>
        /// <param name="nfft">快速傅立叶长度</param>
        /// <param name="sign">是否使用线性功率谱sign=1为线性谱，sign=-1为对数谱</param>
        /// <param name="SampleRate">信号采样频率</param>
        /// <param name="filterNum">梅尔滤波器个数</param>
        /// <returns></returns>
        public static CoefficientCls FnCalculateFrameCoeVector(double[] data, int nfft, int sign, int SampleRate, int filterNum)
        {
            //所有的参数计算均要进行预加重，加窗再计算
            //计算过零率
            double m_ZCR = FnZeroCrossRate(data);
            //计算短时能量
            double m_Energy = FnShortTimeEngerin(data);
            //计算短时幅度
            double m_Amp = FnShortAverageRange(data);
            //计算短时对数能量
            double m_LogEnergy = ((m_Amp == 0) ? (1e-15) : 10 * Math.Log10(m_Amp));
            double[] m_Mfccs = FnCalculateMFCCCoe(data, nfft, sign, SampleRate, filterNum);
            double m_Mean = FnAverage(data);
            double m_Variance = FnVariance(data);
            return new CoefficientCls(m_ZCR, m_Energy, m_Amp, m_LogEnergy, m_Mfccs, m_Mean, m_Variance);
        }

        /// <summary>
        /// 计算两个向量的内积
        /// </summary>
        /// <param name="m_First"></param>
        /// <param name="m_Secod"></param>
        /// <returns></returns>
        public static double FnCalculateVectorInnerProduct(double[] m_First, double[] m_Secod)
        {
            double Sum = 0;
            for (int i = 0; i < m_First.Length; i++)
            {
                Sum += m_First[i] * m_Secod[i];
            }
            return Sum;
        }

        /// <summary>
        /// 计算向量长度
        /// </summary>
        /// <param name="m_Vector"></param>
        /// <returns></returns>
        public static double FnCalculateVectorLength(double[] m_Vector)
        {
            double Sum = 0;
            for (int i = 0; i < m_Vector.Length; i++)
            {
                Sum += Math.Pow(m_Vector[i], 2);
            }
            return Math.Sqrt(Sum);
        }

        /// <summary>
        /// 计算两个向量的余弦距离
        /// </summary>
        /// <param name="m_First"></param>
        /// <param name="m_Second"></param>
        /// <returns></returns>
        public static double FnCalculateVecotrCosing(double[] m_First, double[] m_Second)
        {
            return FnCalculateVectorInnerProduct(m_First, m_Second) / (FnCalculateVectorLength(m_First) * FnCalculateVectorLength(m_Second));
        }

        /// <summary>
        /// 计算频谱中对应的频率值
        /// </summary>
        /// <param name="Fs">采样频率</param>
        /// <param name="Index">第index个点</param>
        /// <param name="nfft">傅立叶变换长度</param>
        /// <returns></returns>
        public static double FnCalculateSpectrumFrequency(double Fs, int Index, int nfft)
        {
            return (Index - 1) * Fs / nfft;
        }

        /// <summary>
        /// 计算指定时间t的傅立叶变换第i个结果决定的信号表达示的结果
        /// </summary>
        /// <param name="m_FFTResult">傅立叶变换的结果</param>
        /// <param name="Fs">采样频率</param>
        /// <param name="Index">第几个傅立叶变换结果</param>
        /// <param name="nfft">傅立叶变换长度</param>
        /// <param name="t">时间</param>
        /// <returns></returns>
        public static double FnSignExpression(Complex m_FFTResult, double Fs, int Index, int nfft,double t)
        {
            return 2 * m_FFTResult.Modulus / nfft * Math.Cos(2 * Math.PI * FnCalculateSpectrumFrequency(Fs, Index, nfft) * t + m_FFTResult.Phase);
        }
        /// <summary>
        /// 对已经加窗的信号数据进行常数Q变换
        /// </summary>
        /// <param name="data"></param>
        /// <param name="m_ConstantQ"></param>
        /// <returns></returns>
        public static double[] FnCalculateConstantQTransfrom(double[] data, ConstQTransform m_ConstantQ)
        {
            double[] m_Result = new double[m_ConstantQ.sparKernel.Length];
            Complex[] m_CResult = new Complex[m_ConstantQ.sparKernel.Length];
            for (int i = 0; i < m_CResult.Length; i++)
            {
                m_CResult[i] = new Complex();
            }
            //执行傅立叶变换
            Complex[] m_FFTX = DSPFunctions.FnFastFourierTransfer(data, m_ConstantQ.FFTLength, 1);
            //计算结果
            for (int i = 0; i < m_ConstantQ.sparKernel.Length; i++)
            {
                for (int j = 0; j < m_ConstantQ.sparKernel[i].Length; j++)
                {
                    m_CResult[i] = m_CResult[i].AddOperate((m_FFTX[j].MuliplateOperate(m_ConstantQ.sparKernel[i][j])));
                }
            }
            for (int i = 0; i < m_CResult.Length; i++)
            {
                m_Result[i] = m_CResult[i].Modulus;
            }
            return m_Result;
        }
    }
