using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    [SerializeField] float noteSpeed;
    [SerializeField] GameObject note;
    [SerializeField] Transform[] players;
    [SerializeField] float xStart;
    [SerializeField] float xEnd;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnNotes());
    }

    // Update is called once per frame
    void Update()
    {
        
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
        noteScript.xStart = xStart;
        noteScript.xEnd = xEnd;
        noteScript.spawner = this;
    }
}
