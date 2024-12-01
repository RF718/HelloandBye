using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class SceneManager : MonoBehaviour
{
    private MainStageManager mainStageManager;

    public GameObject[] startActivesObject;
    public GameObject[] startInactiveObjects;
    public bool StartFlash;
    public bool StartDrizzle;
    public bool StartRainstorm;
    public bool StartGameDisplayer;
    public bool endQuietly;
    public SceneStep[] steps;

    [fsIgnore]
    private int lastStep;

    [fsIgnore]
    private int currentStep;

    [fsIgnore]
    public int LastStep
    {
        get=>lastStep; set => lastStep = value;
    }

    private void Awake()
    {
        mainStageManager = MainStageManager.instance;
    }

    private void Start()
    {
        lastStep = -1;
        currentStep = -1;
    }

    private void OnEnable()
    {
        ResetScene();
    }

    private void OnDisable()
    {
        if (endQuietly)
        {
            mainStageManager.audioManager.StopBgmPlayer();
            mainStageManager.audioManager.StopAllAudioEffect();

            mainStageManager.FlashManager.SwitchFlashManager(false);
            mainStageManager.RainManager.SwitchDrizzle(false);
            mainStageManager.RainManager.SwitchRainStrom(false);
        }
    }



    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            GoToTheStep(-1);
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            PreviousStep();
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            NextStep();
        }
    }

    public void ResetScene()
    {
        lastStep = currentStep;
        for (int i = 0; i < startActivesObject.Length; i++)
        {
            startActivesObject[i].SetActive(true);
        }
        for (int i = 0;i < startInactiveObjects.Length; i++)
        {
            startInactiveObjects[i].SetActive(false);
        }
        mainStageManager.FlashManager.SwitchFlashManager(StartFlash);
        mainStageManager.RainManager.SwitchDrizzle(StartDrizzle);
        mainStageManager.RainManager.SwitchRainStrom(StartRainstorm);
        mainStageManager.scenesManager.GameStartDisplay.SetActive(StartGameDisplayer);
        currentStep = -1;
    }

    public void NextStep()
    {
        if (currentStep >= steps.Length - 1)
        {
            Debug.Log("���һ��,������һ����");
            mainStageManager.scenesManager.GoToTheNextScene();
            lastStep = currentStep;
            return;
        }
        lastStep = currentStep++;
        Debug.Log("��һ���� " + currentStep);
        GoToTheStep(currentStep);
    }

    public void PreviousStep()
    {
        if (currentStep == -1)
        {
            Debug.Log("��ʼ״̬");
            mainStageManager.scenesManager.GoToThePreviousScene();
            lastStep= currentStep;
            return;
        }
        lastStep = currentStep--;
        Debug.Log("���أ� " + currentStep);
        GoToTheStep(currentStep);
    }
    
    public void GoToTheStep(int step,bool jump = false)
    {
        if (step == -1)
        {
            ResetScene();
        }
        if (steps.Length>0&&step >= 0 && step < steps.Length)
        {
            steps[step].Execute();
        }
        if (jump)
        {
            lastStep = currentStep;
            currentStep=step;
        }
        
    }

    
}