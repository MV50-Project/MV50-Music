using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GetFinalScore : MonoBehaviour
{

    public GameObject laserRight;
    public GameObject laserLeft;
    public GameObject gunRight;
    public GameObject gunLeft;
    public TMP_Text finalScoreText;


    private LaserHit scriptLaserRight;
    private LaserHit scriptLaserLeft;

    void Start()
    {
        scriptLaserRight = laserRight.GetComponent<LaserHit>();
        scriptLaserLeft = laserLeft.GetComponent<LaserHit>();
        int rightValue = scriptLaserRight.score;
        int leftValue = scriptLaserLeft.score;
        int finalScore = rightValue + leftValue;
        gunLeft.SetActive(false);
        gunRight.SetActive(false);


        finalScoreText.text = "Final Score : " + finalScore;
    }
}
