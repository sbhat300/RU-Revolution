using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreCounter : MonoBehaviour
{
    public float bestBuffer; 
    public float secondBestBuffer;
    public float thirdBestBuffer;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] Spawner spawner;
    [SerializeField] Transform player;
    public float yPos;
    int score;
    public int id;
    // Start is called before the first frame update
    
    void Start()
    {
        scoreText.text = "Score: 0";
        score = 0;
    }

    // Update is called once per frame
    void Update()
    {
    }
    public void CountScore(Vector3 pos)
    {
        float dist = Mathf.Abs(pos.y - yPos);
        if(dist < bestBuffer)
        {
            score += 5;
            scoreText.text = "Score: " + score;
            spawner.RemoveNoteDestroy(id);
        }
        else if(dist < secondBestBuffer) 
        {
            score += 3;
            scoreText.text = "Score: " + score;
            spawner.RemoveNoteDestroy(id);
        }
        else if(dist < thirdBestBuffer)
        {
            scoreText.text = "Score: " + ++score;
            spawner.RemoveNoteDestroy(id);
        }
    }
}
