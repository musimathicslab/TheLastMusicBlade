using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SoundTrackManager : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioSource powerchords;
    public AudioSource bass;
    public AudioSource doublebass;
    public AudioSource guitar;
    public AudioSource guitar2;
    public AudioSource violin;
    public AudioSource violin2;
    public AudioSource drum;
    public AudioSource tambourine;

    //MAIN CANVAS
    public GameObject CARDUI;
    
    //IMAGE
    public GameObject bass_image;
    public GameObject doublebass_image;
    public GameObject guitar_image;
    public GameObject violin_image;
    public GameObject drum_image;
    public GameObject tambourine_image;

    public GraphicRaycaster raycaster;
    public EventSystem eventSystem;

    private bool isRotating=false;
    private float RotationDuration = 1f;

    void Start()
    {
        // Imposta ignoreListenerPause = true su tutti gli AudioSource
        AudioSource[] allSources = {
            powerchords, bass, doublebass, guitar, violin, guitar2, violin2, drum, tambourine
        };

        double startTime = AudioSettings.dspTime + 1.0; // Pianifica con 1 secondo di anticipo


        foreach (AudioSource source in allSources)
        {
            if (source != null)
                source.ignoreListenerPause = true;
                source.PlayScheduled(startTime);
        }



        /*
        powerchords.PlayScheduled(startTime);
        bass.PlayScheduled(startTime);
        doublebass.PlayScheduled(startTime);
        guitar.PlayScheduled(startTime);
        violin.PlayScheduled(startTime);
        guitar2.PlayScheduled(startTime);
        violin2.PlayScheduled(startTime);
        drum.PlayScheduled(startTime);
        tambourine.PlayScheduled(startTime);
        */

        powerchords.volume = 1f;

        bass_image.SetActive(false);
        doublebass_image.SetActive(false);
        guitar_image.SetActive(false);
        violin_image.SetActive(false);
        drum_image.SetActive(false);
        violin_image.SetActive(false);
        tambourine_image.SetActive(false);




    }

    public void CheckPoint1()
    {
        resetImages();
        drum_image.SetActive(true);
        tambourine_image.SetActive(true);
        CARDUI.SetActive(true);
        //RotateZ(drum_image.gameObject.transform.parent?.gameObject.transform.gameObject, 180f);
    }


    public void CheckPoint2()
    {
        resetImages();
        guitar_image.SetActive(true);
        violin_image.SetActive(true);
        CARDUI.SetActive(true);
        //RotateZ(guitar_image.gameObject.transform.parent?.gameObject.transform.gameObject, 180f);
    }

    public void CheckPoint3()
    {
        resetImages();
        bass_image.SetActive(true);
        doublebass_image.SetActive(true);
        CARDUI.SetActive(true);
        //RotateZ(bass_image.gameObject.transform.parent?.gameObject.transform.gameObject, 180f);
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            PointerEventData pointerData = new PointerEventData(eventSystem);
            pointerData.position = Input.mousePosition;

            List<RaycastResult> results = new List<RaycastResult>();
            raycaster.Raycast(pointerData, results);

            foreach (RaycastResult result in results)
            {
                if (result.gameObject.CompareTag("CLICKABLE"))
                    //Debug.Log("UI element cliccato: " + result.gameObject.name);
                    if (result.gameObject.name.Equals("Butt1")){
                        Debug.Log("Clicked " + CheckActive("L"));
                        activateTrack(CheckActive("L"));
                        Time.timeScale = 1f;
                        CARDUI.SetActive(false);
                        FightManager.Instance.cardChosing = false;
                        Cursor.visible = false;
                        Cursor.lockState = CursorLockMode.Locked;
                    }
                    else
                    {
                        Debug.Log("Clicked " + CheckActive("R"));
                        activateTrack(CheckActive("R"));
                        Time.timeScale = 1f;
                        CARDUI.SetActive(false);
                        FightManager.Instance.cardChosing = false;
                        Cursor.visible = false;                
                        Cursor.lockState = CursorLockMode.Locked;
                    }
            }
        }
    }

    public void resetImages()
    {
        GameObject parent1 = drum_image.gameObject.transform.parent?.gameObject;
        foreach (Transform g in parent1.transform)
        {
            g.gameObject.SetActive(false);
        }

        GameObject parent2 = tambourine_image.gameObject.transform.parent?.gameObject;
        foreach (Transform g in parent2.transform)
        {
            g.gameObject.SetActive(false);
        }
    }

    public string CheckActive(string side)
    {
        string chosen_instr = "";
        if (side.Equals("L")){
            GameObject parent1 = drum_image.gameObject.transform.parent?.gameObject;
            foreach (Transform g in parent1.transform)
            {
                if (g.gameObject.active) chosen_instr = g.name;
            }
        }
        else if (side.Equals("R")) {

            GameObject parent2 = tambourine_image.gameObject.transform.parent?.gameObject;
            foreach (Transform g in parent2.transform)
            {
                if (g.gameObject.active) chosen_instr = g.name;
            }
        }

        return chosen_instr;
    }

    public void RotateZ(GameObject target, float angle)
    {
        if (target != null && !isRotating)
        {
            isRotating = true;
            StartCoroutine(RotateOverTime(target.transform, angle));
        }
    }

    private IEnumerator RotateOverTime(Transform targetTransform, float angle)
    {
        Quaternion startRotation = targetTransform.rotation;
        Quaternion endRotation = startRotation * Quaternion.Euler(0f, 0f, angle);

        float elapsed = 0f;

        while (elapsed < RotationDuration)
        {
            targetTransform.rotation = Quaternion.Slerp(startRotation, endRotation, elapsed / RotationDuration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        targetTransform.rotation = endRotation;
        isRotating = false;
    }

    public void activateTrack(string instrument)
    {
        switch (instrument)
        {
            case ("ElectricBass"):
                bass.volume = 1f;
                break;
            case ("Violin"):
                violin.volume = 1f;
                violin2.volume = 1f;
                break;
            case ("Drum"):
                drum.volume = 1f;
                break;
            case ("DoubleBass"):
                doublebass.volume = 1f;
                break;
            case ("Guitar"):
                guitar.volume = 1f;
                guitar2.volume = 1f;
                break;
            case ("Tambourine"):
                tambourine.volume = 1f;
                break;

        }
    }

    public void setAllVolumeToZero()
    {
        Debug.Log("DOING THESE");
        AudioSource[] allSources = {
            powerchords, bass, doublebass, guitar, violin, guitar2, violin2, drum, tambourine
        };

        foreach (AudioSource source in allSources)
        {
            source.volume = 0f;
        }
    }
}

    
