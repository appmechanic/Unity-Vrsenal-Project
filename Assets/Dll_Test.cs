using UnityEngine;
using System.Collections;

using System.Runtime.InteropServices;

using System;
using System.Text;

public class Dll_Test : MonoBehaviour {

    // A simple custom test function from GUN_BLE
    [DllImport("GUN_BLE")]
    public static extern int GetSimpleInt(int n);


    
    [DllImport("GUN_BLE")]
    public static extern void VRsenal_RumbleOn();


    [DllImport("GUN_BLE")]
    public static extern void VRsenal_GameContinue();


    [DllImport("GUN_BLE")]
    public static extern void VRsenal_RumbleOff();


    [DllImport("GUN_BLE")]
    public static extern void VRsenal_InitHaptic(string audioDeviceName);


    [DllImport("GUN_BLE")]
    public static extern void VRsenal_Shutdown();


    [DllImport("GUN_BLE")]
    public static extern void VRsenal_DisconnectFromBluetooth();



    [DllImport("GUN_BLE")]
    public static extern void VRsenal_CloseCOMPort();





    [DllImport("GUN_BLE")]
    public static extern void VRsenal_MatchEnd(int score, bool success, string stats);




    [DllImport("GUN_BLE")]
    public static extern void VRsenal_HapticEvent(string name);







    /* A simple delegate callback setup where we continously retrieve X which is incremented from server */

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate void UpdateCallback(int value);




    /*Actual work function for the callback*/
    [DllImport("GUN_BLE")]
    public static extern void DoWork([MarshalAs(UnmanagedType.FunctionPtr)] UpdateCallback callbackPointer);


    //Delegate setup for logging X data from dll
    UpdateCallback test_callback;



    /*  Modified logger  */

    /* A simple delegate callback setup where we continously retrieve X which is incremented from server */

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate void LoggerCallback(string value);




    /*Actual work function for the callback*/
    [DllImport("GUN_BLE")]
    public static extern void DoLog([MarshalAs(UnmanagedType.FunctionPtr)] LoggerCallback callbackPointer);


    //Delegate setup for logging X data from dll
    LoggerCallback logger_callback;




    //Delegate setup for logging data from dll
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void MyDelegate(StringBuilder str);


    //Delegate setup for logging data from dll
    [DllImport("GUN_BLE")]
    public static extern void SetDebugFunction(IntPtr fp);


    

    /*
    //JOYSTICK
    public struct Joystick
    {
       
        Joystick(float _x, float _y, bool _button)
        {
            X = _x; Y = _y; selectButton = _button;
        }
        float X;
        float Y;
        bool selectButton;
        float Len() { return Mathf.Sqrt(X * X + Y * Y); }
    };

    public struct GameStartAt
    {
        int level;
        int difficulty;
        int timeLimit;
    };





    [DllImport("GUN_BLE")]
    public static extern bool VRsenal_Update(Joystick lowerLeft, Joystick lowerRight,
        Joystick upperLeft,
        Joystick upperRight,
        bool trigger,
        GameStartAt startAt,
        bool requestShutdown);

    Joystick x;
    GameStartAt g;
        */



    // Use this for initialization
    void Start () {




        // Custom callback logging setup from dll to unity
        MyDelegate callback_delegate = new MyDelegate(CallBackFunction);

        // Convert callback_delegate into a function pointer that can be
        // used in unmanaged code.
        IntPtr intptr_delegate = Marshal.GetFunctionPointerForDelegate(callback_delegate);

        // Call the API passing along the function pointer.
        SetDebugFunction(intptr_delegate);





    

        /////////////
        //Custom callback of getting the integer value x from dll

        test_callback =
    (value) =>
    {

        //Console.WriteLine("Progress = {0}", value);

        Debug.Log("Update from UpdateCallBack = "+value);

    };



        logger_callback =
(value) =>
{

        //Console.WriteLine("Progress = {0}", value);

        if(value!="")
        Debug.Log("Update from LoggerCallBack = " + value);

};




        // call DoWork in C code
        //DoWork(test_callback);
        //////////////////



        //   VRsenal_Update(x,x,x,x,false,g,false);






        //VRSENAL RUMBLEON EVENT WITH INPUT PARAMETER
        VRsenal_RumbleOn();




        //VRSENAL HAPTIC EVENT WITH INPUT PARAMETER
        VRsenal_HapticEvent("h");




      VRsenal_GameContinue();


     VRsenal_RumbleOff();


     VRsenal_InitHaptic("x");


     VRsenal_Shutdown();


     VRsenal_DisconnectFromBluetooth();



     VRsenal_CloseCOMPort();





     VRsenal_MatchEnd(5, false, "y");




     VRsenal_HapticEvent("z");





}







static void CallBackFunction(StringBuilder str)
    {
        Debug.Log(":: DLL LOG : " + str);
    }


    // Update is called once per frame
    void Update () {



        //Continous callback function from dll
        //  DoWork(test_callback);

//        VRsenal_HapticEvent("h");

        

    }
}
