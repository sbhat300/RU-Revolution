using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;
public enum Keys {
    UP,
    DOWN,
    LEFT,
    RIGHT
};
public class InputHandler : MonoBehaviour
{
    // [SerializeField] ScoreCounter scoreCounter;
    int id;
    [SerializeField] Spawner spawner;
    private PlayerInput playerInput;
    bool[] inputs;
    // Start is called before the first frame update
    void Start()
    {
       inputs = new bool[4];
       playerInput = GetComponent<PlayerInput>();
       spawner = FindObjectOfType<Spawner>();
       id = playerInput.playerIndex;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnLeft(CallbackContext context)
    {
        if(context.started) 
        {
            spawner.UpdateInputs(id, Keys.LEFT, true);
        }
        else if(context.canceled)
        {
            spawner.UpdateInputs(id, Keys.LEFT, false);
        }
    }
    public void OnRight(CallbackContext context)
    {
        if(context.started)
        {
            spawner.UpdateInputs(id, Keys.RIGHT, true);
        }
        else if(context.canceled)
        {
            spawner.UpdateInputs(id, Keys.RIGHT, false);
        }
    }
    public void OnUp(CallbackContext context)
    {
        if(context.started) 
        {
            spawner.UpdateInputs(id, Keys.UP, true);
        }
        else if(context.canceled) 
        {
            spawner.UpdateInputs(id, Keys.UP, false);
        }
    }
    public void OnDown(CallbackContext context)
    {
        if(context.started) 
        {
            spawner.UpdateInputs(id, Keys.DOWN, true);
        }
        else if(context.canceled) 
        {
            spawner.UpdateInputs(id, Keys.DOWN, false);
        }
    }
}
