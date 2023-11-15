using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugButton : MonoBehaviour
{
    // Start is called before the first frame update
    public void ClickTest()
    {
        Debug.Log("clicked");
    }

    public void ClickTest2(string text)
    {
        Debug.Log("text");
    }


    
}
