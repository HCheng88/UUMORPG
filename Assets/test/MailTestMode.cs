using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MailTestMode : SingletonMono<MailTestMode> {


    public void Init()
    {
        EventDispatchet._instance.AddEventListener(ProtoCodeDef.Mail_Get_listProto, EventLisenterCallBack);
    }

    private void EventLisenterCallBack(byte[] buffer)
    {
        Mail_Get_listProto mail_Get_ListProto = Mail_Get_listProto.GetProto(buffer);
        Debug.Log(mail_Get_ListProto.Count + ";" + mail_Get_ListProto.MailID + ";" + mail_Get_ListProto.MailName);
    }
}
