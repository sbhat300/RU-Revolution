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
    Queue<Note> notes;
    [SerializeField] ScoreCounter scoreCounter;
    bool rightReleased, upReleased;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnNotes());
        notes = new Queue<Note>();
    }

    // Update is called once per frame
    void Update()
    {
        if(rightReleased && Input.GetAxisRaw("Horizontal") == -1) 
        {
            rightReleased = false;
            if(notes.Peek() != null && notes.Peek().noteType == NoteType.GREEN)
            {
                NoteGet();
            }

        }
        else if(rightReleased && Input.GetAxisRaw("Horizontal") > 0) 
        {
            rightReleased = false;
            if(notes.Peek() != null && notes.Peek().noteType == NoteType.BLUE)
            {
                NoteGet();
            }
        }
        else if(Input.GetAxisRaw("Horizontal") == 0)
        {
            rightReleased = true;
        }

        if(upReleased && Input.GetAxisRaw("Vertical") == -1) 
        {
            upReleased = false;
            if(notes.Peek() != null && notes.Peek().noteType == NoteType.YELLOW)
            {
                NoteGet();
            }
        }
        else if(upReleased && Input.GetAxisRaw("Vertical") > 0) 
        {
            upReleased = false;
            if(notes.Peek() != null && notes.Peek().noteType == NoteType.RED)
            {
                NoteGet();
            }
        }
        else if(Input.GetAxisRaw("Vertical") == 0)
        {
            upReleased = true;
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
        notes.Enqueue(noteScript);
    }
    public void RemoveNote()
    {
        Note n = notes.Dequeue();
        n.inQueue = false;
    }
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
