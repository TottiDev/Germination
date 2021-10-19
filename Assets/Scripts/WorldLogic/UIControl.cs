using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIControl : MonoBehaviour
{
    [SerializeField] GameObject ZoomCamera;
    [SerializeField] string levelToLoad;
    [SerializeField] GameObject PlayerPanel;
    [SerializeField] GameObject WeaponSelectionPanel;
    [SerializeField] Button jumpButon;
    [SerializeField] Button WeaponSelectionButon;
    [SerializeField] Button spitButon;
    [SerializeField] Button pistol;
    [SerializeField] Button Sniper;
    [SerializeField] Button pasturn;
    [SerializeField] Button cancel;
    CharctesSelection teamSelectio;
    CameraControl cameracontrol;
    TurnControl turnControl;
    BoxCollider trigerUI;
    Attack attackScript;
    Drag dragScript;
    int laststate;
    [FMODUnity.EventRef]
    public string JumpBSound;
    [FMODUnity.EventRef]
    public string BackBSound;
    [FMODUnity.EventRef]
    public string WeaponBSound;

    public GameObject ZoomCamera1 { get => ZoomCamera; set => ZoomCamera = value; }

    // Start is called before the first frame update
    void Start()
    {
        teamSelectio = GetComponent<CharctesSelection>();
        cameracontrol = GetComponent<CameraControl>();
        turnControl = GetComponent<TurnControl>();
        trigerUI = GetComponent<BoxCollider>();
        jumpButon.interactable = false;
        WeaponSelectionButon.interactable = false;
        pasturn.interactable = false;
        cancel.interactable = false;
    }
    private void Update()
    {
        if (turnControl.Estado == 0)
        {
            trigerUI.enabled = true;
            PlayerPanel.SetActive(false);
        }

        if (turnControl.Estado != laststate && turnControl.Estado < 7 && cameracontrol.TEspera == 0)
        {
            PlayerPanel.SetActive(true);
            laststate = turnControl.Estado;
        }
        if (turnControl.Estado >= 5 && dragScript == null)
        {
            turnControl.Estado = 7;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (turnControl.Estado == 4)
        {
            dragScript = teamSelectio.CurentPlayer.GetComponent<Drag>();
            attackScript = teamSelectio.CurentPlayer.GetComponent<Attack>();
            PlayerPanel.SetActive(true);
            //butons start
            jumpButon.interactable = true;
            WeaponSelectionButon.interactable = true;
            pasturn.interactable = true;
            cancel.interactable = true;
            trigerUI.enabled = false;
            //butons end
        }
    }
    public void WeaponSelection()
    {
        WeaponSelectionButon.interactable = false;

        //Weapon butons start
        spitButon.interactable = true;
        pistol.interactable = true;
        Sniper.interactable = true;
        //Weapon butons end

        WeaponSelectionPanel.SetActive(true);

    }
    public void Weapon1()
    {
        attackScript.Attack1();
        FMODUnity.RuntimeManager.PlayOneShotAttached(WeaponBSound, gameObject);
        spitButon.interactable = false;
        cancel.interactable = false;
        ZoomCamera1.SetActive(true);
        PlayerPanel.SetActive(false);
        WeaponSelectionPanel.SetActive(false);
        laststate = turnControl.Estado;

    }
    public void Weapon2()
    {
        attackScript.Attack2();
        FMODUnity.RuntimeManager.PlayOneShotAttached(WeaponBSound, gameObject);
        pistol.interactable = false;
        cancel.interactable = false;
        ZoomCamera1.SetActive(true);
        PlayerPanel.SetActive(false);
        WeaponSelectionPanel.SetActive(false);
        laststate = turnControl.Estado;
    }
    public void Weapon3()
    {
        attackScript.Attack3();
        FMODUnity.RuntimeManager.PlayOneShotAttached(WeaponBSound, gameObject);
        Sniper.interactable = false;
        cancel.interactable = false;
        ZoomCamera1.SetActive(true);
        PlayerPanel.SetActive(false);
        WeaponSelectionPanel.SetActive(false);
        laststate = turnControl.Estado;
    }
    public void JumpButon()
    {
        dragScript.IsShoot = false;
        jumpButon.interactable = false;
        cancel.interactable = false;
        PlayerPanel.SetActive(false);
        ZoomCamera1.SetActive(true);
        laststate = turnControl.Estado;
        FMODUnity.RuntimeManager.PlayOneShotAttached(JumpBSound, gameObject);
    }

    public void Cancel()
    {
        FMODUnity.RuntimeManager.PlayOneShotAttached(BackBSound, gameObject);
        WeaponSelectionPanel.SetActive(false);
        WeaponSelectionButon.interactable = true;
        turnControl.Estado = 0;
    }
    public void EndTurn()
    {
        jumpButon.interactable = false;
        WeaponSelectionButon.interactable = false;
        spitButon.interactable = false;
        pasturn.interactable = false;
        cancel.interactable = false;
        turnControl.Estado = 7;
    }
    public void Reload()
    {
        SceneManager.LoadScene(levelToLoad);
        Time.timeScale = 1;
    }


}
