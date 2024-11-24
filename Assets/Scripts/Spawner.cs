using System;
using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEditor.Tilemaps;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

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
    [SerializeField] GameObject[,] displays; // 0:red, 1:blue, 2:green, 3:yellow
    [SerializeField] Sprite[] displaySprites; // 0:red, 1:blue, 2:green, 3:yellow
    [SerializeField] Sprite[] graySprites; // 0:red, 1:blue, 2:green, 3:yellow

    bool[] rightReleased, upReleased;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnNotes());
        notes = new Queue<Note>[4];
        rightReleased = new bool[4];
        upReleased = new bool[4];
        displays = new GameObject[4,4];
        for(int i = 0; i < 4; i++)
        {
            notes[i] = new Queue<Note>();
        }
        for(int i = 0; i < 4; i++)
        {
            scoreCounter[i].yPos = yDeactivate;
        }
        for(int i = 0; i < 4; i++)
        {
            for(int j = 0; j < 4; j++)
            {
                displays[i,j] = scoreCounter[i].gameObject.GetComponent<PlayerBars>().displays[j];
            }
        }
    }
    
    // Update is called once per frame
    void Update()
    {
    }
    public void UpdateInputs(int i, Keys input, bool down)
    {
        if(down)
        {
            if(input == Keys.LEFT)
            {
                displays[i,1].GetComponent<SpriteRenderer>().sprite = graySprites[1];
                if(notes[i].Peek() != null && notes[i].Peek().noteType == NoteType.BLUE)
                {
                    NoteGet(i);
                }
            }
            else if(input == Keys.RIGHT)
            {
                displays[i,3].GetComponent<SpriteRenderer>().sprite = graySprites[3];
                if(notes[i].Peek() != null && notes[i].Peek().noteType == NoteType.YELLOW)
                {
                    NoteGet(i);
                }
            }
            else if(input == Keys.DOWN)
            {
                displays[i,2].GetComponent<SpriteRenderer>().sprite = graySprites[2];
                if(notes[i].Peek() != null && notes[i].Peek().noteType == NoteType.GREEN)
                {
                    NoteGet(i);
                }
            }
            else if(input == Keys.UP)
            {
                displays[i,0].GetComponent<SpriteRenderer>().sprite = graySprites[0];
                if(notes[i].Peek() != null && notes[i].Peek().noteType == NoteType.RED)
                {
                    NoteGet(i);
                }
            }
        }
        else
        {
            // Key Up Detection
            if (input == Keys.LEFT) // Left released
            {
                displays[i,1].GetComponent<SpriteRenderer>().sprite = displaySprites[1];
            }
            else if (input == Keys.RIGHT) // Right released
            {
                displays[i,3].GetComponent<SpriteRenderer>().sprite = displaySprites[3];
            }

            if (input == Keys.DOWN) // Down released
            {
                displays[i,2].GetComponent<SpriteRenderer>().sprite = displaySprites[2];
            }
            else if (input == Keys.UP) // Up released
            {
                displays[i,0].GetComponent<SpriteRenderer>().sprite = displaySprites[0];
            }
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
