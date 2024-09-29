using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageHandler
{

    public event Action<string> onMessage;



    public void OnMessage(string message)
    {
        onMessage?.Invoke(message);
    }
}
