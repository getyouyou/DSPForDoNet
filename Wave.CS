/// <summary>
    /// Wav文件类
    /// </summary>
    public class WavCls
    {
        //文件路径
        private string _FilePath;
        public string m_FilePath 
        {
            get { return _FilePath; }
            set { _FilePath = value; }
        }
        //RIFF WAVE Chunk
        //文件标志，"RIFF"
        private byte[] _RIFF = new byte[4];
        public string m_RIFF
        {
            get { return Encoding.ASCII.GetString(_RIFF); }
        }
        //文件从下个地址开始的长度，整个文件大小=m_FileSize+8，4Byte，长整型
        private byte[] _FileSize = new byte[4];
        public Int32 m_FileSize
        {
            get { return BitConverter.ToInt32(_FileSize, 0); }
        }
        //WAV文件标志，"WAVE"，4Byte
        private byte[] _WaveFlag = new byte[4];
        public string m_WaveFlag
        {
            get { return Encoding.ASCII.GetString(_WaveFlag); }
        }

        //Format Chunk
        //波形格式标志，4Byte，"fmt"
        private byte[] _WaveFormatFlag = new byte[4];
        public string m_WaveFormatFlag
        {
            get { return Encoding.ASCII.GetString(_WaveFormatFlag); }
        }
        //过滤字节，用于表示是否有附加字节，为16时无附加信息，18时则有2字节附加信息，4Byte，整型
        private byte[] _FilterBytes = new byte[4];
        public Int32 m_FilterBytes
        {
            get { return BitConverter.ToInt32(_FilterBytes,0); }
        }
        //格式种类，取1时为PCM，2Byte，整型
        private byte[] _FormatType = new byte[2];
        public Int16 m_FormatType
        {
            get { return BitConverter.ToInt16(_FormatType,0); }
        }
        //通道数量，单声道为1，双声道为2，2Byte，整型
        private byte[] _ChannelCount = new byte[2];
        public Int16 m_ChannelCount
        {
            get { return BitConverter.ToInt16(_ChannelCount, 0); }
        }
        //采样频率，4Byte，长整型
        private byte[] _SampleFrequence = new byte[4];
        public Int32 m_SampleFrequence
        {
            get { return BitConverter.ToInt32(_SampleFrequence, 0); }
        }
        //平均每秒字节数，长整型，4Byte
        private byte[] _AvgBytesPerSecond = new byte[4];
        public Int32 m_AvgBytesPerSecond
        {
            get { return BitConverter.ToInt32(_AvgBytesPerSecond, 0); }
        }
        //每个采样需要的字节数，2Byte，整型
        private byte[] _SampleBytes = new byte[2];
        public Int16 m_SampleBytes
        {
            get { return BitConverter.ToInt16(_SampleBytes, 0); }
        }
        //每个采样需要的Bit位数，2Byte，整型
        private byte[] _SampleBits = new byte[2];
        public Int16 m_SampleBits
        {
            get { return BitConverter.ToInt16(_SampleBits, 0); }
        }
        //附加信息，2Bytes，由m_FilterBytes决定
        public byte[] m_OptionBytes = new byte[2];

        //Fact Chunk   ID="fact"
        private byte[] _FactID = new byte[4];
        public string m_FactID
        {
            get { return Encoding.ASCII.GetString(_FactID); }
        }
        private byte[] _FactSize = new byte[4];
        public Int32 m_FactSize
        {
            get { return BitConverter.ToInt32(_FactSize, 0); }
        }
        private byte[] _FactData = new byte[4];

        //Data Chunk   ID="data"
        //数据Chunk头结构
        private byte[] _DataID = new byte[4];
        public string m_DataID
        {
            get { return Encoding.ASCII.GetString(_DataID); }
        }
        //返回数据块长度
        private byte[] _DataSize;
        public Int32 m_DataSize
        {
            get { return BitConverter.ToInt32(_DataSize, 0); }
        }

        /// <summary>
        /// 比特率，由于在通信中1K不等于1024而是1000，因比特率为每秒平均字节数*8/1000
        /// </summary>
        public string m_BitRate
        {
            get { return m_AvgBytesPerSecond * 8.0d / 1000 + "kbps"; }
        }

        //两个声道的字节数据
        public byte[] m_PCMBytesData;

        //左声道采样数据
        public Int16[] m_LeftPCMInt16Data;

        //右声道采样数据
        public Int16[] m_RightPCMInt16Data;

        //左声道采样数据转换浮点数
        public double[] m_LeftPCMFloatData;

        //右声道采样数据转换为浮点数
        public double[] m_RightPCMFloatData;

        public WavCls(string m_FilePath)
        {
            _FilePath = m_FilePath;
            if (m_FilePath.EndsWith(".wav", StringComparison.OrdinalIgnoreCase))
            {
                FileStream m_FileStream = new FileStream(_FilePath, FileMode.Open);
                BinaryReader m_BinaryReader = new BinaryReader(m_FileStream);
                _RIFF = m_BinaryReader.ReadBytes(4);
                _FileSize = m_BinaryReader.ReadBytes(4);
                _WaveFlag = m_BinaryReader.ReadBytes(4);
                _WaveFormatFlag = m_BinaryReader.ReadBytes(4);
                _FilterBytes = m_BinaryReader.ReadBytes(4);
                _FormatType = m_BinaryReader.ReadBytes(2);
                _ChannelCount = m_BinaryReader.ReadBytes(2);
                _SampleFrequence = m_BinaryReader.ReadBytes(4);
                _AvgBytesPerSecond = m_BinaryReader.ReadBytes(4);
                _SampleBytes = m_BinaryReader.ReadBytes(2);
                _SampleBits = m_BinaryReader.ReadBytes(2);
                //如果有附加信息，则读取附加信息
                if (m_FilterBytes == 18)
                {
                    m_OptionBytes = m_BinaryReader.ReadBytes(2);
                }

                //读取下一个ID
                byte[] m_Temp = new byte[4];
                m_Temp = m_BinaryReader.ReadBytes(4);
                //如果是fact块
                if (Encoding.ASCII.GetString(m_Temp) == "fact")
                {
                    _FactID = m_Temp;
                    _FactSize = m_BinaryReader.ReadBytes(4);
                    _FactData = m_BinaryReader.ReadBytes(4);
                    //数据ID
                    _DataID = m_BinaryReader.ReadBytes(4);
                    //数据块大小，字节数量
                    _DataSize = m_BinaryReader.ReadBytes(4);
                }
                else//data Chunk
                {
                    //数据ID
                    _DataID = m_Temp;
                    //数据块大小，字节数量
                    _DataSize = m_BinaryReader.ReadBytes(4);
                }
                
                //读取两个声道的数据
                m_PCMBytesData = new byte[m_DataSize];
                for (int i = 0; i < m_DataSize; i++)
                {
                    m_PCMBytesData[i] = m_BinaryReader.ReadByte();
                }

                //分割两个声道的数据
                FnExtractLeftChannel();

                //转换两个声道的数据
                FnConvertLeftChannelFloat();

                m_BinaryReader.Close();
                m_FileStream.Close();
            }
        }

        /// <summary>
        /// 提取各个通道的数据
        /// </summary>
        public void FnExtractLeftChannel()
        {
            int i=0;
            //单声道
            if (m_ChannelCount == 1)
            {
                switch (m_SampleBits)
                {
                    case 8:FnSingleChannel8Bits();break;
                    case 16: FnSingleChannel16Bits(); break;
                    default: break;
                }
            }
            else//单声道
            {
                switch (m_SampleBits)
                {
                    case 8: FnDoubleChannel8Bits(); break;
                    case 16: FnDoubleChannel16Bits(); break;
                    default: break;
                }
            }
        }

        /// <summary>
        /// 单声道8bit采样，左右声道使用一个值
        /// </summary>
        public void FnSingleChannel8Bits()
        {
            m_LeftPCMInt16Data = new short[m_PCMBytesData.Length];
            m_RightPCMInt16Data = new short[m_PCMBytesData.Length];
            for (int i = 0; i < m_PCMBytesData.Length; i++)
            {
                m_LeftPCMInt16Data[i] =m_RightPCMInt16Data[i]= m_PCMBytesData[i];
            }
        }

        /// <summary>
        /// 双声道8Bit采样，左右声道分开
        /// </summary>
        public void FnDoubleChannel8Bits()
        {
            m_RightPCMInt16Data = new short[m_PCMBytesData.Length / 2];
            m_LeftPCMInt16Data = new short[m_PCMBytesData.Length / 2];

            for (int i = 0; i < m_LeftPCMInt16Data.Length; i++)
            {
                m_LeftPCMInt16Data[i] = m_PCMBytesData[2*i];
                m_RightPCMInt16Data[i] = m_PCMBytesData[2*i + 1];
            }
        }

        /// <summary>
        /// 单声道16bit采样，左右声道相同
        /// </summary>
        public void FnSingleChannel16Bits()
        {
            m_RightPCMInt16Data = new short[m_PCMBytesData.Length / 4];
            m_LeftPCMInt16Data = new short[m_PCMBytesData.Length / 4];

            for (int i = 0; i < m_LeftPCMInt16Data.Length; i++)
            {
                m_LeftPCMInt16Data[i] =m_RightPCMInt16Data[i]= BitConverter.ToInt16(m_PCMBytesData, i * 2);
            }
        }

        /// <summary>
        /// 双声道16Bit采样，左右声道分开
        /// </summary>
        public void FnDoubleChannel16Bits()
        {
            m_RightPCMInt16Data = new short[m_PCMBytesData.Length / 4];
            m_LeftPCMInt16Data = new short[m_PCMBytesData.Length / 4];

            for (int i = 0; i < m_LeftPCMInt16Data.Length; i++)
            {
                m_LeftPCMInt16Data[i] = BitConverter.ToInt16(m_PCMBytesData, 4 * i);
                m_RightPCMInt16Data[i] = BitConverter.ToInt16(m_PCMBytesData, 4 * i + 2);
            }
        }

        /// <summary>
        /// 将左右声道数据转换为浮点数，PCM采样的原始数据为正负双向的数据
        ///整个样的数据有正有负，是正负双向PCM量化编码，根据采样的Bit位数决定，最高位为0表示为正，为1为负数，根据不同的位数除以不同的分母进行转换，16位则表示-32768~32767
        ///因此正数需要除以32767，负数需要除以32768才能得到结果
        /// </summary>
        public void FnConvertLeftChannelFloat()
        {
            //8bit为-128-127，16Bit为-32768-32767
            double m_PositiveConvertBase = Math.Pow(2,m_SampleBits-1)-1;
            double m_DePositiveConverBase = Math.Pow(2, m_SampleBits - 1);

            m_LeftPCMFloatData = new double[m_LeftPCMInt16Data.Length];
            m_RightPCMFloatData = new double[m_RightPCMInt16Data.Length];

            //执行转换
            for (int i = 0; i < m_LeftPCMInt16Data.Length; i++)
            {
                m_LeftPCMFloatData[i] = m_LeftPCMInt16Data[i] > 0 ? (m_LeftPCMInt16Data[i] * 1.0d / m_PositiveConvertBase) : (m_LeftPCMInt16Data[i] * 1.0d / m_DePositiveConverBase);
                m_RightPCMFloatData[i] = m_RightPCMInt16Data[i] > 0 ? (m_RightPCMInt16Data[i] * 1.0d / m_PositiveConvertBase) : (m_RightPCMInt16Data[i] * 1.0d / m_DePositiveConverBase);
            }
        }
    }
