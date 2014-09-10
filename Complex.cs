/// <summary>
/// 虚数类，Real+Image*i；i为虚数单位，i^2 = -1，Real为实部，Image为虚部，当Image=0时，表示实数Real，当Real=0而Image不为0时为纯虚数
/// </summary>
public class Complex
{
    /// <summary>
    /// 实部
    /// </summary>
    public double Real;
    /// <summary>
    /// 虚部
    /// </summary>
    public double Image;

    /// <summary>
    /// 虚数的模
    /// </summary>
    public double Modulus
    {
        get { return Math.Sqrt(Math.Pow(Real, 2) + Math.Pow(Image, 2)); }
    }
    /// <summary>
    /// 相位
    /// </summary>
    public double Phase
    {
        get { return Math.Atan2(Image,Real); }
    }

    /// <summary>
    /// 构造方法
    /// </summary>
    /// <param name="R"></param>
    /// <param name="I"></param>
    public Complex(double R, double I)
    {
        Real = R;
        Image = I;
    }

    public Complex()
    {
        Real = 0;
        Image = 0;
    }

    /// <summary>
    /// 求其共轭复数，即实部相等，虚部为相反数
    /// </summary>
    /// <returns></returns>
    public Complex ConjugateComplex()
    {
        return new Complex(Real, -Image);
    }

    /// <summary>
    /// 从一个元素中复制
    /// </summary>
    /// <param name="other"></param>
    public void Copy(Complex other)
    {
        this.Image = other.Image;
        this.Real = other.Real;
    }

    /// <summary>
    /// 加法
    /// </summary>
    /// <param name="other"></param>
    /// <returns></returns>
    public Complex AddOperate(Complex other)
    {
        return new Complex(this.Real + other.Real, this.Image + other.Image);
    }

    /// <summary>
    /// 乘法
    /// </summary>
    /// <param name="other"></param>
    /// <returns></returns>
    public Complex MuliplateOperate(Complex other)
    {
        return new Complex(this.Real * other.Real - this.Image * other.Image, this.Image * other.Real + this.Real * other.Image);
    }
}
