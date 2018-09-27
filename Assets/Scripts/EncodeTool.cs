using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;


/// <summary>
/// 关于编码的工具类
/// </summary>
public static class EncodeTool
{

    #region 粘包拆包问题 封装一个有规定的数据包

    /// <summary>
    /// 构造数据包 ： 包头 + 包尾
    /// </summary>
    /// <returns></returns>
    public static byte[] EncodePacket(byte[] data)
    {
        //内存流对象
        using (MemoryStream ms = new MemoryStream())
        {
            using (BinaryWriter bw = new BinaryWriter(ms))
            {
                //先写入长度
                bw.Write(data.Length);
                //再写入数据
                bw.Write(data);

                byte[] byteArray = new byte[(int)ms.Length];
                Buffer.BlockCopy(ms.GetBuffer(), 0, byteArray, 0, (int)ms.Length);

                return byteArray;
            }
        }
    }

    /// <summary>
    /// 解析消息体 从缓存里取出一个一个完整的数据包 
    /// </summary>
    /// <returns></returns>
    public static byte[] DecodePacket(ref List<byte> dataCache)
    {
        //四个字节 构成一个int长度 不能构成一个完整的消息
        if (dataCache.Count < 4)
            return null;
        //throw new Exception("数据缓存长度不足4 不能构成一个完整的消息");

        using (MemoryStream ms = new MemoryStream(dataCache.ToArray()))
        {
            using (BinaryReader br = new BinaryReader(ms))
            {
                // 1111 111 1
                int length = br.ReadInt32();
                int dataRemainLength = (int)(ms.Length - ms.Position);
                //数据长度不够包头约定的长度 不能构成一个完整的消息
                if (length > dataRemainLength)
                    return null;
                //throw new Exception("数据长度不够包头约定的长度 不能构成一个完整的消息");

                byte[] data = br.ReadBytes(length);
                //更新一下数据缓存
                dataCache.Clear();
                dataCache.AddRange(br.ReadBytes(dataRemainLength));

                return data;
            }
        }
    }

    #endregion

    #region 把一个object类型转换成byte[]

    /// <summary>
    /// 序列化对象
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static byte[] EncodeObj(object value)
    {
        using (MemoryStream ms = new MemoryStream())
        {
            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(ms, value);
            byte[] valueBytes = new byte[ms.Length];
            Buffer.BlockCopy(ms.GetBuffer(), 0, valueBytes, 0, (int)ms.Length);
            return valueBytes;
        }
    }

    /// <summary>
    /// 反序列化对象
    /// </summary>
    /// <param name="valueBytes"></param>
    /// <returns></returns>
    public static object DecodeObj(byte[] valueBytes)
    {
        using (MemoryStream ms = new MemoryStream(valueBytes))
        {
            BinaryFormatter bf = new BinaryFormatter();
            object value = bf.Deserialize(ms);
            return value;
        }
    }

    #endregion
}



