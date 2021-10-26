using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageAttack : MonoBehaviour
{
    [SerializeField] float damageToDeal;
    CameraControl cameracontrol;
    Transform bulletTransform;
    [SerializeField] GameObject hitVFX;
    TurnControl turnControl;
    Rigidbody rb;
    HP hpScript;
    [FMODUnity.EventRef]
    public string floorCol;
    [FMODUnity.EventRef]
    public string wallCol;
    [FMODUnity.EventRef]
    public string DamageSound;
    private void Start()
    {
        turnControl = GameObject.FindGameObjectWithTag("MainCinemachineCamera").GetComponent<TurnControl>();
        cameracontrol = GameObject.FindGameObjectWithTag("MainCinemachineCamera").GetComponent<CameraControl>();
        rb = GetComponent<Rigidbody>();
        bulletTransform = GetComponentInChildren<Transform>();
    }
    private void Update()
    {
        float angle = Mathf.Atan2(rb.velocity.y, rb.velocity.x) * Mathf.Rad2Deg;
        bulletTransform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
    private void OnCollisionEnter(Collision collision)
    {
        cameracontrol.TEspera = 120;
        
        //Berifica si golpeo al jugador toma su script de HP y le pasa un balor de daño a recivir
        if (collision.gameObject.tag == "Player")
        {
            FMODUnity.RuntimeManager.PlayOneShotAttached(DamageSound, gameObject);
            hpScript = collision.transform.GetComponent<HP>();
            hpScript.TackeDamage(damageToDeal);
            GameObject hit = Instantiate(hitVFX, transform.position, Quaternion.identity) as GameObject;
            if (turnControl.Estado >= 4)
            {
                turnControl.Estado += 2;
            }
            Destroy(gameObject);
        }
        //Berifica si es una superficie distinta al muro y si si es distinta prosede con la destrucion del proyectil
        else
        {
            if (collision.gameObject.tag == "jumpingWall")
            {
                FMODUnity.RuntimeManager.PlayOneShotAttached(wallCol, gameObject);
                GameObject hit = Instantiate(hitVFX, transform.position, Quaternion.identity) as GameObject;
            }
            if (collision.gameObject.tag != "jumpingWall")
            {
                FMODUnity.RuntimeManager.PlayOneShotAttached(floorCol, gameObject);
                GameObject hit = Instantiate(hitVFX, transform.position, Quaternion.identity) as GameObject;
                if (turnControl.Estado >= 4)
                {
                    turnControl.Estado += 2;
                }
                Destroy(gameObject);
            }
        }
    }
}
