using System;
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
    Queue<Note>[] notes;
    [SerializeField] ScoreCounter[] scoreCounter;
    // The things on the bottom
    [SerializeField] GameObject[] displays; // 0:red, 1:blue, 2:green, 3:yellow
    [SerializeField] Sprite[] displaySprites; // 0:red, 1:blue, 2:green, 3:yellow
    [SerializeField] Sprite[] graySprites; // 0:red, 1:blue, 2:green, 3:yellow

    bool rightReleased, upReleased;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnNotes());
        notes = new Queue<Note>[4];
        for(int i = 0; i < 4; i++)
        {
            notes[i] = new Queue<Note>();
        }
        for(int i = 0; i < 4; i++)
        {
            scoreCounter[i].yPos = yDeactivate;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Key Up
        if(rightReleased && Input.GetAxisRaw("Horizontal") == -1) // left pressed
        {
            rightReleased = false;

            for(int i = 0; i < 4; i++)
            if(notes[i].Peek() != null && notes[i].Peek().noteType == NoteType.BLUE)
            {
                NoteGet(i);
            }
            displays[1].GetComponent<SpriteRenderer>().sprite = displaySprites[1];
        }
        else if(rightReleased && Input.GetAxisRaw("Horizontal") > 0) // right pressed
        {
            rightReleased = false;
            
            for(int i = 0; i < 4; i++)
            if(notes[i].Peek() != null && notes[i].Peek().noteType == NoteType.YELLOW)
            {
                NoteGet(i);
            }
        }

        if (upReleased && Input.GetAxisRaw("Vertical") == -1) // down pressed
        {
            upReleased = false;

            for(int i = 0; i < 4; i++)
            if(notes[i].Peek() != null && notes[i].Peek().noteType == NoteType.GREEN)
            {
                NoteGet(i);
            }
        }
        else if(upReleased && Input.GetAxisRaw("Vertical") > 0) // up pressed
        {
            upReleased = false;

            for(int i = 0; i < 4; i++)
            if(notes[i].Peek() != null && notes[i].Peek().noteType == NoteType.RED)
            {
                NoteGet(i);
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
            // for(int i = 0; i < 4; i++)
            // if (notes[i].Peek() != null && notes[i].Peek().noteType == NoteType.BLUE)
            // {
            //     NoteGet(i);
            // }
            displays[1].GetComponent<SpriteRenderer>().sprite = displaySprites[1];
        }
        else if (Input.GetKeyUp(KeyCode.RightArrow)) // Right released
        {
            rightReleased = true;
            // for(int i = 0; i < 4; i++)
            // if (notes[i].Peek() != null && notes[i].Peek().noteType == NoteType.YELLOW)
            // {
            //     NoteGet(i);
            // }
            displays[3].GetComponent<SpriteRenderer>().sprite = displaySprites[3];
        }

        if (Input.GetKeyUp(KeyCode.DownArrow)) // Down released
        {
            upReleased = true;
            // for(int i = 0; i < 4; i++)
            // if (notes[i].Peek() != null && notes[i].Peek().noteType == NoteType.GREEN)
            // {
            //     NoteGet(i);
            // }
            displays[2].GetComponent<SpriteRenderer>().sprite = displaySprites[2];
        }
        else if (Input.GetKeyUp(KeyCode.UpArrow)) // Up released
        {
            upReleased = true;
            // for(int i = 0; i < 4; i++)
            // if (notes[i].Peek() != null && notes[i].Peek().noteType == NoteType.RED)
            // {
            //     NoteGet(i);
            // }
            displays[0].GetComponent<SpriteRenderer>().sprite = displaySprites[0];
        }

    }
    IEnumerator SpawnNotes()
    {
        while(true)
        {
            yield return new WaitForSeconds(1);
            Spawn((NoteType)UnityEngine.Random.Range(0, 3));
        }
    }
    void Spawn(NoteType noteType)
    {
        for(int i = 0; i < 4; i++)
        {
            GameObject newNote = Instantiate(note, transform.position, Quaternion.identity);
            Note noteScript = newNote.GetComponent<Note>();
            noteScript.speed = noteSpeed;
            noteScript.noteType = noteType;
            noteScript.player = players[i]; //change this to spawn for all players later
            noteScript.yStart = yStart;
            noteScript.yEnd = yEnd;
            noteScript.spawner = this;
            noteScript.scoreCounter = scoreCounter[i];
            noteScript.yDeactivate = yDeactivate;
            notes[i].Enqueue(noteScript);
        }
    }
    public void RemoveNote(int id)
    {
        Note n = notes[id].Dequeue();
        n.inQueue = false;
    }
    // Properly Remove Note
    public void RemoveNoteDestroy(int id)
    {
        Note n = notes[id].Dequeue();
        n.inQueue = false;
        Destroy(n.gameObject);
    }
    void NoteGet(int id)
    {        
        scoreCounter[id].CountScore(notes[id].Peek().transform.position);
    }
}
