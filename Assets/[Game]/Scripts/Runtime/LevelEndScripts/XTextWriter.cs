using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class XTextWriter : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        WriteAll();
    }

    // Update is called once per frame
    void WriteAll()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).GetComponent<TextMeshProUGUI>().text = "X" + (i + 2).ToString();
        }

    }

}
