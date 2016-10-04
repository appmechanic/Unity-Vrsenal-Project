using UnityEngine;
using UnityEngine.UI;
using System;
using System.Runtime.InteropServices;

/*
    This code is part of the C++ DLL Tutorial package,
    and will work only with the example DLL.

    Use this code as a reference for implementing your own
    DLL interfaces, or modify it to try some new features.

    Those interfaces implements the smallest way to connect
    a C++ native DLL with C# code, within Unity (Mono develop)
    or Visual Studio.

    The only diference with the "DllAccess.cs" file is the dll name.
*/

public class MFCDllAccess : MonoBehaviour {

    Text text;  // Text on screen, just to see what happens

    [DllImport("UnityTestMFCLibrary")] static extern int GetInt(int n);
    [DllImport("UnityTestMFCLibrary")] static extern float GetFloat(float n);
    [DllImport("UnityTestMFCLibrary")] static extern bool GetBool(bool n);
    [DllImport("UnityTestMFCLibrary")] static extern IntPtr GetConstWString();
    [DllImport("UnityTestMFCLibrary")] static extern IntPtr GetString(string n);
    [DllImport("UnityTestMFCLibrary")] static extern IntPtr GetWString(string n);
    [DllImport("UnityTestMFCLibrary")] static extern IntPtr GetByteArray();

    // Local persistent data copies:
    byte[] buffer = new byte[10];
    string data;
    string message = " : Hello!";

    // Use this for initialization
    void Start ()
    {
        text = GameObject.Find("Text").GetComponent<Text>();

        // Gets an integer (returns n + 2):
        text.text += "GetInt (2 + 2): " + GetInt(2).ToString() + Environment.NewLine;
        
        // Gets a float (returns n + 0.5):
        text.text += "GetFloat (2f + 0.5f): " + GetFloat(2f).ToString() + Environment.NewLine;
        
        // Gets a boolean (returns !n):
        text.text += "GetBool (!false): " + GetBool(false).ToString() + Environment.NewLine;
        text.text += Environment.NewLine;

        // Gets a string from a constant string:
        data = Marshal.PtrToStringUni(GetConstWString());
        text.text += "GetConstWString: '" + data + "'" + Environment.NewLine;
        
        // Gets a wideChar memory allocated string:
        data = Marshal.PtrToStringUni(GetWString(message));
        text.text += "GetWString (+ ' : Hello!'): '" + data + "'" + Environment.NewLine;
        
        // Gets a char memory allocated string:
        data = Marshal.PtrToStringAnsi(GetString(message));
        text.text += "GetString (+ ' : Hello!'): '" + data + "'" + Environment.NewLine;
        text.text += Environment.NewLine;

        // Gets a ByteArray { 1,2,3,4,5,6,7,8,9,0 }:
        text.text += "Gets byte array in one single line of code { 1,2,3,4,5,6,7,8,9,0 }:" + Environment.NewLine;
        Marshal.Copy(GetByteArray(), buffer, 0, 10);
        // Prints the array content:
        for (int c = 0; c < buffer.Length; c++)
        {
            text.text += "input buffer[" + c.ToString()+"] content: "+buffer[c].ToString() + Environment.NewLine;
        }
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
