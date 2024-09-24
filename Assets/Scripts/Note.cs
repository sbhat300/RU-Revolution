using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum NoteType {
    RED, 
    BLUE,
    GREEN,
    YELLOW
};

public class Note : MonoBehaviour
{
    public float speed;

    public NoteType noteType;
    
    public Transform player;
    public float xStart;
    public float xEnd;
    public Spawner spawner;

    // Start is called before the first frame update
    void Start()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        if(noteType == NoteType.RED)
        {
            spriteRenderer.color = new Color32(192, 85, 84, 255);
            transform.position =  new Vector3(xStart, player.position.y + 0.40f, 0);
        }
        else if(noteType == NoteType.YELLOW)
        {
            transform.position = new Vector3(xStart, player.position.y + 0.20f, 0);
            spriteRenderer.color = new Color32(255, 245, 71, 255);
        }
        else if(noteType == NoteType.BLUE)
        {
            transform.position = new Vector3(xStart, player.position.y - 0.20f, 0);
            spriteRenderer.color = new Color32(111, 153, 233, 255);
        }
        else if(noteType == NoteType.GREEN)
        {
            transform.position =  new Vector3(xStart, player.position.y - 0.40f, 0);
            spriteRenderer.color = new Color32(131, 200, 136, 255);
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.position -= new Vector3(speed * Time.deltaTime, 0, 0);
        if(transform.position.x < xEnd) Destroy(gameObject);
    }
}
