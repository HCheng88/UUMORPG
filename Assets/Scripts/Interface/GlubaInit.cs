using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlubaInit : SingletonMono<GlubaInit> {


    #region  常量
    /// <summary>
    /// 昵称KEY
    /// </summary>
    public const string MMO_NICKNAME = "MMO_NICKNAME";
    /// <summary>
    /// 密码KEY
    /// </summary>
    public const string MMO_PWD = "MMO_PWD";
    /// <summary>
    /// 账户服务器
    /// </summary>
    public const string WebAccountURL = "";

    public const string SocketIP = "192.168.31.236";

    public const ushort Prot = 5050;

    #endregion

    public delegate void OnReceiveProtoHaandler(ushort protoCode, byte[] buffer);

    public OnReceiveProtoHaandler OnReceiveProto;


}
