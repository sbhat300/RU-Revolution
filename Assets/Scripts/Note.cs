using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

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
    public float yStart;
    public float yEnd;
    public Spawner spawner;
    public ScoreCounter scoreCounter;
    public bool inQueue;

    // Start is called before the first frame update
    void Start()
    {
        inQueue = true;
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        if(noteType == NoteType.RED)
        {
            spriteRenderer.color = new Color32(192, 85, 84, 255);
            transform.position =  new Vector3(player.position.x + 0.40f, yStart, 0);
        }
        else if(noteType == NoteType.YELLOW)
        {
            transform.position = new Vector3(player.position.x + 0.20f, yStart, 0);
            spriteRenderer.color = new Color32(255, 245, 71, 255);
        }
        else if(noteType == NoteType.BLUE)
        {
            transform.position = new Vector3(player.position.x - 0.20f, yStart, 0);
            spriteRenderer.color = new Color32(111, 153, 233, 255);
        }
        else if(noteType == NoteType.GREEN)
        {
            transform.position =  new Vector3(player.position.x - 0.40f, yStart, 0);
            spriteRenderer.color = new Color32(131, 200, 136, 255);
        }
    }

    void Update()
    {
        transform.position -= new Vector3(0, speed * Time.deltaTime, 0);
        if(inQueue && transform.position.y < player.position.y - scoreCounter.thirdBestBuffer) 
            spawner.RemoveNote();
        if(transform.position.y < yEnd) Destroy(gameObject);
    }
}
