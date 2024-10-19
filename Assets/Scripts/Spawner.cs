using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    [SerializeField] float noteSpeed;
    [SerializeField] GameObject note;
    [SerializeField] Transform[] players;
    [SerializeField] float yStart;
    [SerializeField] float yEnd;
    [SerializeField] float yDeactivate;
    Queue<Note> notes;
    [SerializeField] ScoreCounter scoreCounter;
    // The things on the bottom
    [SerializeField] GameObject[] displays; // 0:red, 1:blue, 2:green, 3:yellow
    [SerializeField] Sprite[] displaySprites; // 0:red, 1:blue, 2:green, 3:yellow
    [SerializeField] Sprite[] graySprites; // 0:red, 1:blue, 2:green, 3:yellow

    bool rightReleased, upReleased;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnNotes());
        notes = new Queue<Note>();
        scoreCounter.yPos = yDeactivate;
    }

    // Update is called once per frame
    void Update()
    {
        // Key Up
        if(rightReleased && Input.GetAxisRaw("Horizontal") == -1) // left pressed
        {
            rightReleased = false;
           
            if(notes.Peek() != null && notes.Peek().noteType == NoteType.BLUE)
            {
                NoteGet();
            }
            displays[1].GetComponent<SpriteRenderer>().sprite = displaySprites[1];
        }
        else if(rightReleased && Input.GetAxisRaw("Horizontal") > 0) // right pressed
        {
            rightReleased = false;
            
            if(notes.Peek() != null && notes.Peek().noteType == NoteType.YELLOW)
            {
                NoteGet();
            }
        }

        if (upReleased && Input.GetAxisRaw("Vertical") == -1) // down pressed
        {
            upReleased = false;
             if(notes.Peek() != null && notes.Peek().noteType == NoteType.GREEN)
            {
                NoteGet();
            }
        }
        else if(upReleased && Input.GetAxisRaw("Vertical") > 0) // up pressed
        {
            upReleased = false;
            if(notes.Peek() != null && notes.Peek().noteType == NoteType.RED)
            {
                NoteGet();
            }
        }

        // Key Down Detection - not using axis because we aren't going to use the stick anyway just buttons sorry if this breaks anything
        if (Input.GetKeyDown(KeyCode.LeftArrow)) // Left arrow key pressed
        {
            displays[1].GetComponent<SpriteRenderer>().sprite = graySprites[1];
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow)) // Right arrow key pressed
        {
            displays[3].GetComponent<SpriteRenderer>().sprite = graySprites[3];
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow)) // Up arrow key pressed
        {
            displays[0].GetComponent<SpriteRenderer>().sprite = graySprites[0];
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow)) // Down arrow key pressed
        {
            displays[2].GetComponent<SpriteRenderer>().sprite = graySprites[2];
        }

        // Key Up Detection
        if (Input.GetKeyUp(KeyCode.LeftArrow)) // Left released
        {
            rightReleased = true;
            if (notes.Peek() != null && notes.Peek().noteType == NoteType.BLUE)
            {
                NoteGet();
            }
            displays[1].GetComponent<SpriteRenderer>().sprite = displaySprites[1];
        }
        else if (Input.GetKeyUp(KeyCode.RightArrow)) // Right released
        {
            rightReleased = true;
            if (notes.Peek() != null && notes.Peek().noteType == NoteType.YELLOW)
            {
                NoteGet();
            }
            displays[3].GetComponent<SpriteRenderer>().sprite = displaySprites[3];
        }

        if (Input.GetKeyUp(KeyCode.DownArrow)) // Down released
        {
            upReleased = true;
            if (notes.Peek() != null && notes.Peek().noteType == NoteType.GREEN)
            {
                NoteGet();
            }
            displays[2].GetComponent<SpriteRenderer>().sprite = displaySprites[2];
        }
        else if (Input.GetKeyUp(KeyCode.UpArrow)) // Up released
        {
            upReleased = true;
            if (notes.Peek() != null && notes.Peek().noteType == NoteType.RED)
            {
                NoteGet();
            }
            displays[0].GetComponent<SpriteRenderer>().sprite = displaySprites[0];
        }

    }
    IEnumerator SpawnNotes()
    {
        while(true)
        {
            yield return new WaitForSeconds(1);
            Spawn((NoteType)Random.Range(0, 3));
        }
    }
    void Spawn(NoteType noteType)
    {
        GameObject newNote = Instantiate(note, transform.position, Quaternion.identity);
        Note noteScript = newNote.GetComponent<Note>();
        noteScript.speed = noteSpeed;
        noteScript.noteType = noteType;
        noteScript.player = players[0]; //change this to spawn for all players later
        noteScript.yStart = yStart;
        noteScript.yEnd = yEnd;
        noteScript.spawner = this;
        noteScript.scoreCounter = scoreCounter;
        noteScript.yDeactivate = yDeactivate;
        notes.Enqueue(noteScript);
    }
    // Idk if this is deprecated
    public void RemoveNote()
    {
        Note n = notes.Dequeue();
        n.inQueue = false;
    }
    // Properly Remove Note
    public void RemoveNoteDestroy()
    {
        Note n = notes.Dequeue();
        n.inQueue = false;
        Destroy(n.gameObject);
    }
    void NoteGet()
    {        
        scoreCounter.CountScore(notes.Peek().transform.position);
    }
}
