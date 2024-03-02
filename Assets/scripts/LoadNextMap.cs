using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadNextMap : MonoBehaviour
{
    public float delaySecond = 2;
    public string nameScene = "Scene3";
    public int requiredGemCount = 1;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            int collectedGemCount = CountCollectedGems(collision.gameObject);

            if (collectedGemCount >= requiredGemCount)
            {
                collision.gameObject.SetActive(false);
                ModeSelect();
            }
            // Add an else statement if you want to do something if the player doesn't have enough gems.
        }
    }

    private int CountCollectedGems(GameObject player)
    {
        
        XuliVaCham gemCollector = player.GetComponent<XuliVaCham>();

        if (gemCollector != null)
        {
            return gemCollector.CollectedGemsCount();
        }

        return 0;
    }

    public void ModeSelect()
    {
        StartCoroutine(LoadAfterDelay());
    }

    IEnumerator LoadAfterDelay()
    {
        yield return new WaitForSeconds(delaySecond);
        SceneManager.LoadScene(nameScene);
    }
}

