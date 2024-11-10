using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;
public enum Keys {
    UP,
    DOWN,
    LEFT,
    RIGHT
};
public class InputHandler : MonoBehaviour
{
    [SerializeField] ScoreCounter scoreCounter;
    [SerializeField] Spawner spawner;
    bool[] inputs;
    // Start is called before the first frame update
    void Start()
    {
       inputs = new bool[4];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnLeft(CallbackContext context)
    {
        if(context.started) 
        {
            spawner.UpdateInputs(scoreCounter.id, Keys.LEFT, true);
        }
        else if(context.canceled)
        {
            spawner.UpdateInputs(scoreCounter.id, Keys.LEFT, false);
        }
    }
    public void OnRight(CallbackContext context)
    {
        if(context.started)
        {
            spawner.UpdateInputs(scoreCounter.id, Keys.RIGHT, true);
        }
        else if(context.canceled)
        {
            spawner.UpdateInputs(scoreCounter.id, Keys.RIGHT, false);
        }
    }
    public void OnUp(CallbackContext context)
    {
        if(context.started) 
        {
            spawner.UpdateInputs(scoreCounter.id, Keys.UP, true);
        }
        else if(context.canceled) 
        {
            spawner.UpdateInputs(scoreCounter.id, Keys.UP, false);
        }
    }
    public void OnDown(CallbackContext context)
    {
        if(context.started) 
        {
            spawner.UpdateInputs(scoreCounter.id, Keys.DOWN, true);
        }
        else if(context.canceled) 
        {
            spawner.UpdateInputs(scoreCounter.id, Keys.DOWN, false);
        }
    }
}
