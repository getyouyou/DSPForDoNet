/// <summary>
/// 窗函数
/// </summary>
public class WindowFunction
{
    /// <summary>
    /// 汉明窗Hamming Window
    /// </summary>
    /// <param name="index">索引</param>
    /// <param name="frameSize">窗口大小</param>
    /// <returns></returns>
    public static double HammingWindow(int index, int frameSize)
    {
        return 0.54 - 0.46 * Math.Cos((2 * Math.PI * index) / (frameSize - 1));
    }

    /// <summary>
    /// Hann汉宁窗
    /// </summary>
    /// <param name="index">索引</param>
    /// <param name="frameSize">窗口大小</param>
    /// <returns></returns>
    public static double HannWindow(int index, int frameSize)
    {
        return 0.5 * (1 - Math.Cos((2 * Math.PI * index) / (frameSize - 1)));
    }

    /// <summary>
    /// 矩形窗
    /// </summary>
    /// <param name="index">索引</param>
    /// <param name="frameSize">窗口大小</param>
    /// <returns></returns>
    public static double RectWindow(int index, int frameSize)
    {
        return 1;
    }

    /// <summary>
    /// Blackman窗
    /// </summary>
    /// <param name="index">索引</param>
    /// <param name="frameSize">窗口大小</param>
    /// <returns></returns>
    public static double BlackmanWindow(int index, int frameSize)
    {
        return 0.42 - 0.5 * Math.Cos((2 * Math.PI * index) / (frameSize - 1)) + 0.08 * Math.Cos((4 * Math.PI * index) / (frameSize - 1));
    }

    /// <summary>
    /// 高斯窗
    /// </summary>
    /// <param name="index">索引</param>
    /// <param name="frameSize">窗口大小</param>
    /// <param name="alpha">系数，小于等于0.5</param>
    /// <returns></returns>
    public static double GuaseWindow(int index, int frameSize, double alpha)
    {
        return Math.Pow(Math.E, (-1 / 2) * Math.Pow(((index - (frameSize - 1) / 2) / (alpha * (frameSize - 1) / 2)), 2));
    }

    /// <summary>
    /// Barlett窗即三角窗
    /// </summary>
    /// <param name="index">索引</param>
    /// <param name="frameSize">窗口大小</param>
    /// <returns></returns>
    public static double BartlettWindow(int index, int frameSize)
    {
        return 1.0 - Math.Abs(1.0 - 2 * index * Math.PI / (frameSize - 1));
    }

    /// <summary>
    /// BarlettHann窗
    /// </summary>
    /// <param name="index">索引</param>
    /// <param name="frameSize">窗口大小</param>
    /// <returns></returns>
    public static double BartlettHannWindow(int index, int frameSize)
    {
        return 0.62 - 0.48 * Math.Abs((index / (frameSize - 1)) - 1 / 2) - 0.38 * Math.Cos((2 * Math.PI * index) / (frameSize - 1));
    }

    /// <summary>
    /// BlackmanHarris窗
    /// </summary>
    /// <param name="index">索引</param>
    /// <param name="frameSize">窗大小</param>
    /// <returns></returns>
    public static double BlackmannHarrisWindow(int index, int frameSize)
    {
        return 0.35875 - (0.48829 * Math.Cos((2 * Math.PI * index) / (frameSize - 1))) + (0.14128 * Math.Cos((4 * Math.PI * index) / (frameSize - 1))) - (0.01168 * Math.Cos((6 * Math.PI * index) / (frameSize - 1)));
    }

    /// <summary>
    /// Kaiser窗函数
    /// </summary>
    /// <param name="index">索引</param>
    /// <param name="frameSize">窗大小</param>
    /// <param name="beta">系数，一般取3~9，包括3和9</param>
    /// <returns></returns>
    public static double KaiserWindow(int index, int frameSize, double beta)
    {
        double a = 0, w = 0, a2 = 0, b1 = 0, b2 = 0, beta1 = 0;
        b1 = FnZeroOrderBessel(beta);
        a = 2.0 * index / (double)(frameSize - 1) - 1;
        a2 = a * a;
        beta1 = beta * Math.Sqrt(1.0 - a2);
        b2 = FnZeroOrderBessel(beta1);
        w = b2 / b1;
        return w;
    }

    /// <summary>
    /// 第一类0阶修正贝塞耳函数
    /// </summary>
    /// <param name="x"></param>
    /// <returns></returns>
    public static double FnZeroOrderBessel(double x)
    {
        double m_Result = 0;
        double d = 0, y = 0, d2 = 0;
        y = x / 2.0;
        d = 1.0d;
        m_Result = 1.0d;
        for (int i = 1; i <= 25; i++)
        {
            d = d * y / i;
            d2 = d * d;
            m_Result += d2;
            if (d2 < m_Result * 1.0e-8)
            {
                break;
            }
        }
        return m_Result;
    }

    /// <summary>
    /// 使用汉明窗对序列进行加窗过滤
    /// </summary>
    /// <param name="data">数据帧</param>
    /// <returns></returns>
    public static double[] FnUsingHammingWindow(double[] data)
    {
        double[] m_Result = new double[data.Length];
        for (int i = 0; i < data.Length; i++)
        {
            m_Result[i] = HammingWindow(i, data.Length) * data[i];
        }
        return m_Result;
    }

    /// <summary>
    /// 使用汉宁窗
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    public static double[] FnUsingHannWindow(double[] data)
    {
        double[] m_Result = new double[data.Length];
        for (int i = 0; i < data.Length; i++)
        {
            m_Result[i] = HannWindow(i, data.Length) * data[i];
        }
        return m_Result;
    }

    /// <summary>
    /// 使用矩形窗
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    public static double[] FnUsingRectWindow(double[] data)
    {
        double[] m_Result = new double[data.Length];
        for (int i = 0; i < data.Length; i++)
        {
            m_Result[i] = RectWindow(i, data.Length) * data[i];
        }
        return m_Result;
    }

    /// <summary>
    /// 使用Blackman窗
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    public static double[] FnUsingBlackmanWindow(double[] data)
    {
        double[] m_Result = new double[data.Length];
        for (int i = 0; i < data.Length; i++)
        {
            m_Result[i] = BlackmanWindow(i, data.Length) * data[i];
        }
        return m_Result;
    }

    /// <summary>
    /// 使用高斯窗
    /// </summary>
    /// <param name="data"></param>
    /// <param name="alpha">过滤系数，一般取小于等于0.5</param>
    /// <returns></returns>
    public static double[] FnUsingGuaseWindow(double[] data,double alpha=0.5)
    {
        double[] m_Result = new double[data.Length];
        for (int i = 0; i < data.Length; i++)
        {
            m_Result[i] = GuaseWindow(i, data.Length, alpha) * data[i];
        }
        return m_Result;
    }

    /// <summary>
    /// 使用Barllet窗即三角窗
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    public static double[] FnUsingBartlettWindow(double[] data)
    {
        double[] m_Result = new double[data.Length];
        for (int i = 0; i < data.Length; i++)
        {
            m_Result[i] = BartlettWindow(i, data.Length);
        }
        return m_Result;
    }

    /// <summary>
    /// 使用BarlettHann窗
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    public static double[] FnUsingBartlettHannWindow(double[] data)
    {
        double[] m_Result = new double[data.Length];
        for (int i = 0; i < data.Length; i++)
        {
            m_Result[i] = BartlettHannWindow(i, data.Length);
        }
        return m_Result;
    }

    /// <summary>
    /// 使用BlackmanHarris窗
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    public static double[] FnUsingBlackmannHarrisWindow(double[] data)
    {
        double[] m_Result = new double[data.Length];
        for (int i = 0; i < data.Length; i++)
        {
            m_Result[i] = BlackmannHarrisWindow(i, data.Length);
        }
        return m_Result;
    }

    /// <summary>
    /// 使用Kaiser窗口
    /// </summary>
    /// <param name="data"></param>
    /// <param name="beta"></param>
    /// <returns></returns>
    public static double[] FnUsingKaiserWindow(double[] data,double beta)
    {
        double[] m_Result = new double[data.Length];
        for (int i = 0; i < data.Length; i++)
        {
            m_Result[i] = KaiserWindow(i, data.Length, beta);
        }
        return m_Result;
    }
}
