using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

public class startTutorial : MonoBehaviour
{
    [SerializeField] private CanvasGroup bg;
    [SerializeField] private Transform[] positions;
    [SerializeField] private Transform leora;

    private void OnEnable()
    {
        if (mainDialogueManager.GLOBALcurrentlyRunningText == "introducingSuspects" && SceneManager.GetActiveScene().name == "NoCombatAreas")
        {
            bg.alpha = 0f;
            positions[0].DOMove(positions[1].transform.position, 2f).SetUpdate(true);
            return;
        }
        bg.alpha = 0f;
        bg.DOFade(1, 1).SetUpdate(true);
        positions[0].DOMove(positions[1].transform.position, 1f).SetUpdate(true);
    }

    public void yes()
    {
        //transition out to tutorial
        SpawnManager.SpawnNumber = 1;
        GameObject.FindObjectOfType<CityPortalManager>().LoadCityArea("TrainingEntry");
    }

    public void no()
    {
        bg.DOFade(0, 1).SetUpdate(true);
        //Teleport Leora; looks jarring rn but it's fine
        leora.position = positions[3].transform.position;
        positions[0].DOMove(positions[2].transform.position, 0.5f).SetUpdate(true).OnComplete(() =>
        {
            Debug.Log("I'm free");
            Time.timeScale = 1f;
            OpenPauseMenu.GLOBALcanOpenPause = true;
            OpenPauseMenu.canOpenPause = true;
            OpenPauseMenu.pauseOpened = false;
            NonCombatPlayerMovement.canMove = true;
            this.gameObject.SetActive(false);
        });
    }
}
