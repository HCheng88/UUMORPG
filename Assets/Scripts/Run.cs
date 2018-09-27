using LitJson;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using UnityEngine;

public class Run : MonoBehaviour
{

    void Start()
    { 
        //TestProto testProto = new TestProto();
        //testProto.Id = 1;
        //testProto.Name = "测试";
        //testProto.Type = 0;
        //testProto.Price = 99.9f;
        NetSocket._instance.MyConnect("192.168.31.236", 5050);
        //NetSocket._instance.SendMsg(testProto.ToArray());
        //EventDispatchet._instance.AddEventListener(ProtoCodeDef.Test, OnReceiveProtoCallBack);
        MailTestMode._instance.Init();
    }


    public byte[] Creadata(string str)
    {
        using (MMO_MemoryStream ms = new MMO_MemoryStream())
        {
            ms.WriteUTF8String(str);
            return ms.ToArray();
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            byte[] by = Creadata("你好啊！a");
            NetSocket._instance.SendMsg(by);
        }
        else if (Input.GetKeyDown(KeyCode.B))
        {
            Mail_Requet_ListProto test = new Mail_Requet_ListProto();

            NetSocket._instance.SendMsg(test.ToArray());
        }
        else if (Input.GetKeyDown(KeyCode.C))
        {
            for (int i = 0; i < 10; i++)
            {
                byte[] by = Creadata("你好啊循环" + i);
                NetSocket._instance.SendMsg(by);
            }
        }
    }
}
