/// <summary>
    /// 提取梅尔倒谱系数
    /// </summary>
    public class MFCCExtract
    {
        /// <summary>
        /// 返回指定的MFCC系数
        /// </summary>
        /// <param name="spectralData">频谱数据，原始数据加窗处理后，进行离散傅立叶变换，对复数值取绝对值或者平方得到</param>
        /// <param name="samplingRate">采样频率，如44100Hz</param>
        /// <param name="NumFilters">计算时用的滤波器数量，一般取48，实验中取值能常为22-26</param>
        /// <param name="binSize">频谱数据的长度，一般为2的整数次幂2^N，通常为FFT的长度</param>
        /// <param name="m">指示当前计算的第m个MFCC系数，一般使用第2-13个系数，即对应下标为1-12，丢弃剩余数据</param>
        /// <returns></returns>
        public static double GetCoefficient(double[] spectralData, int samplingRate, int NumFilters, int binSize, int m)
        {
            double result = 0.0f;
            double outerSum = 0.0f;
            double innerSum = 0.0f;
            int k, l;

            // 0 <= m < L
            if(m >= NumFilters)
            {
                    return 0.0f;
            }

            result = NormalizationFactor(NumFilters, m);

            for(l = 1; l <= NumFilters; l++)
            {
                    // 计算每个滤波器组输出的对数能量
                    innerSum = 0.0f;
                    //频谱值累加，其中的spectralData[k]经过取模的平方求得，求得能量
                    //源代码是for(k = 0; k < binSize-1; k++)
                    for(k = 0; k < binSize; k++)
                    {
                            innerSum += Math.Abs(spectralData[k] * GetFilterParameter(samplingRate, binSize, k, l));
                    }
                    //求取对数能量
                    if(innerSum > 0.0d)
                    {
                            innerSum = Math.Log(innerSum); 
                    }
                    //对数能量*cos(n*(l-0.5)*pi/滤波器个数)，即执行离散余弦变换过程
                    innerSum = innerSum * Math.Cos(((m * Math.PI) / NumFilters) * (l - 0.5d));

                    outerSum += innerSum;
            }
            //执行离散余弦过程中的最后一步
            result *= outerSum;

            return result;
        }

        /// <summary>
        /// 计算归一化因子，加权系数
        /// </summary>
        /// <param name="NumFilters"></param>
        /// <param name="m"></param>
        /// <returns></returns>
        public static double NormalizationFactor(int NumFilters, int m)
        {
            double normalizationFactor = 0.0f;

            if (m == 0)
            {
                normalizationFactor = Math.Sqrt(1.0f / NumFilters);
            }
            else
            {
                normalizationFactor = Math.Sqrt(2.0f / NumFilters);
            }

            return normalizationFactor;
        }

        /// <summary>
        /// 为指定频率和滤波器计算相应的滤波参数
        /// </summary>
        /// <param name="samplingRate"></param>
        /// <param name="binSize"></param>
        /// <param name="frequencyBand"></param>
        /// <param name="filterBand"></param>
        /// <returns></returns>
        public static double GetFilterParameter(int samplingRate, int binSize, int frequencyBand, int filterBand)
        {
            double filterParameter = 0.0f;

            //计算真实的各个快速傅立叶变换中第i项值对应的频谱频率
            double boundary = (frequencyBand * samplingRate) / binSize;             // k * Fs / N
            //计算上一个滤波器对应的中心频率f(m-1)
            double prevCenterFrequency = GetCenterFrequency(filterBand - 1);                // fc(l - 1) etc.
            //计算当前滤波器的中心频率f(m)
            double thisCenterFrequency = GetCenterFrequency(filterBand);
            //计算下一个滤波器中心频率f(m+1)
            double nextCenterFrequency = GetCenterFrequency(filterBand + 1);
            //如果频谱频率的值要小于上一个中心频率则说明有错误，返回0
            if (boundary >= 0 && boundary < prevCenterFrequency)
            {
                filterParameter = 0.0f;
            }
            //如果介于前一个中心频率与当时中心频率之间，则Hm(k_=2(k-f(m-1))/(f(m+1)-f(m-1))/(f(m)-f(m-1))
            else if (boundary >= prevCenterFrequency && boundary < thisCenterFrequency)
            {
                filterParameter = (boundary - prevCenterFrequency) / (thisCenterFrequency - prevCenterFrequency);
                filterParameter *= GetMagnitudeFactor(filterBand);
            }
            else if (boundary >= thisCenterFrequency && boundary < nextCenterFrequency)
            {
                filterParameter = (boundary - nextCenterFrequency) / (thisCenterFrequency - nextCenterFrequency);
                filterParameter *= GetMagnitudeFactor(filterBand);
            }
            else if (boundary >= nextCenterFrequency && boundary < samplingRate)
            {
                filterParameter = 0.0f;
            }

            return filterParameter;
        }

        /// <summary>
        /// 计算给定滤波器组的幅度尺
        /// </summary>
        /// <param name="filterBand"></param>
        /// <returns></returns>
        public static double GetMagnitudeFactor(int filterBand)
        {
            double magnitudeFactor = 0.0f;

            if (filterBand >= 1 && filterBand <= 14)
            {
                magnitudeFactor = 0.015;
            }
            else if (filterBand >= 15 && filterBand <= 48)
            {
                magnitudeFactor = 2.0f / (GetCenterFrequency(filterBand + 1) - GetCenterFrequency(filterBand - 1));
            }

            return magnitudeFactor;
        }

        /// <summary>
        /// 指定滤波频段的中心频率
        /// </summary>
        /// <param name="filterBand"></param>
        /// <returns></returns>
        public static double GetCenterFrequency(int filterBand)
        {
            double centerFrequency = 0.0f;
            double exponent;

            if (filterBand == 0)
            {
                centerFrequency = 0;
            }
            else if (filterBand >= 1 && filterBand <= 14)//Mel滤波器中前1000Hz的14个滤波器是线性分布的，14滤波器需要,相邻滤波器中心距离为66.6即1000-0/15=200/3
            {
                centerFrequency = (200.0f * filterBand) / 3.0f;
            }
            else//1000Hz以后是26个对数线性分布的滤波器，相邻滤波器的左半宽度的比值为1.0711703
            {
                exponent = filterBand - 14.0f;
                centerFrequency = Math.Pow(1.0711703, exponent);
                centerFrequency *= 1073.4;
            }

            return centerFrequency;
        }
    }
