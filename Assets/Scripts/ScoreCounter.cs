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
    int[] scores;
    // Start is called before the first frame update
    
    void Start()
    {
        scoreText.text = "Score: 0";
        scores = new int[4];
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void CountScore(Vector3 pos)
    {
        float dist = Mathf.Abs(pos.x - player.transform.position.x);
        if(dist < bestBuffer)
        {
            scores[0] += 5;
            scoreText.text = "Score: " + scores[0];
            spawner.RemoveNoteDestroy();
        }
        else if(dist < secondBestBuffer) 
        {
            scores[0] += 3;
            scoreText.text = "Score: " + scores[0];
            spawner.RemoveNoteDestroy();
        }
        else if(dist < thirdBestBuffer)
        {
            scoreText.text = "Score: " + ++scores[0];
            spawner.RemoveNoteDestroy();
            
        }
    }
}
