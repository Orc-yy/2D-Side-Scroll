using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public int totalPoint;
    public int stagePoint;
    public int stageIndex;
    public GameObject[] Stages;
    public MapExit mapExit;

    // Score Manager
    public TextMeshProUGUI UIScore;
    
    // nextStage Manager
    public void NextStage()
    {
        // Change Stage
        if(stageIndex < Stages.Length - 1)
        {
            Stages[stageIndex].SetActive(false);
            stageIndex++;
            Stages[stageIndex].SetActive(true);
            mapExit.PlayerReposition();
            
        }
        else
        {
            // Game Clear

            // Player Control Lock
            Time.timeScale = 0;
            // Result UI
            Debug.Log("게임 클리어!!");
            // Return Button UI
        }

        // Calculate Point
        totalPoint += stagePoint;
        stagePoint = 0;
    }
    private void Update()
    {
        UIScore.text = (totalPoint + stagePoint).ToString();
    }

}
