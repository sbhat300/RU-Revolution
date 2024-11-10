using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

public class InputHandler : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnLeft(CallbackContext context)
    {
        if(context.started) print("start");
        else if(context.canceled) print("end");
    }
    public void OnRight(CallbackContext context)
    {
        if(context.started) print("start");
        else if(context.canceled) print("end");
    }
    public void OnUp(CallbackContext context)
    {
        if(context.started) print("start");
        else if(context.canceled) print("end");
    }
    public void OnDown(CallbackContext context)
    {
        if(context.started) print("start");
        else if(context.canceled) print("end");
    }
}
