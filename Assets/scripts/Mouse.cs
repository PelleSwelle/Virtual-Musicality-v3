using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Mouse : MonoBehaviour
{
    // public GameObject button;
    // public Rigidbody buttonRigidBody;

    // base
    // public GameObject sliderControl;
    // public Rigidbody baseRigidBody;
    // public GameObject slider;

    // raycasting 
    public Vector3 worldPosition;
    public LibPdInstance pd;
    public TextMeshPro cursorText;
    public ArrayList buttonsList;
    // public GameObject chordComponent;
    public ChordManager chordManager;
    private int root, third, fifth, seventh;
    private float passiveZ, activeZ;
    public Ray ray;
    public RaycastHit hitData;


    void OnValidate()
    {
        pd = GameObject.Find("synth").GetComponent<LibPdInstance>();
        // slider = GameObject.Find("slider");
        passiveZ = 1.6f;
        activeZ = 1.8f;
        // chordsPanel = GameObject.Find("ChordsPanel");
        chordManager = GameObject.Find("synth").GetComponent<ChordManager>();

        root = chordManager.root;
        third = chordManager.third;
        fifth = chordManager.fifth;
        seventh = chordManager.seventh;

        // Add all the chord buttons to a list in order to manipulate them in bulk.
        buttonsList = new ArrayList();
        buttonsList.Add(GameObject.Find("Btn_major"));
        buttonsList.Add(GameObject.Find("Btn_m"));
        buttonsList.Add(GameObject.Find("Btn_7"));
        buttonsList.Add(GameObject.Find("Btn_m7"));
        buttonsList.Add(GameObject.Find("Btn_maj7"));
        buttonsList.Add(GameObject.Find("Btn_mMaj7"));
        buttonsList.Add(GameObject.Find("Btn_aug"));
        buttonsList.Add(GameObject.Find("Btn_dim"));

        cursorText = this.GetComponent<TextMeshPro>();

    }

    void Start()
    {

    }


    void Update()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        // turning on and off the entire synth
        toggleOnOff();


        // if the raycast hits something at all
        if (Physics.Raycast(ray, out hitData, 1000))
        {

            // show the title of the object pointed at, at the cursor position
            worldPosition = hitData.point;
            cursorText.transform.position = new Vector3(
                worldPosition.x,
                worldPosition.y,
                worldPosition.z - 0.1f
            );
            cursorText.SetText(hitData.transform.ToString());


            // slider
            if (Input.GetMouseButtonDown(0))
            {
                if (hitData.transform.tag == "slider")
                {
                    hitData.transform.parent.gameObject.GetComponent<sliderScript>().setActive();
                }
            }
            else if (Input.GetMouseButtonUp(0))
            {
                if (hitData.transform.tag == "slider")
                {
                    hitData.transform.parent.gameObject.GetComponent<sliderScript>().setPassive();
                }
            }

            // buttons
            if (Input.GetMouseButtonDown(0))
            {
                foreach (GameObject chordButton in buttonsList)
                {
                    if (hitData.transform.parent.name == chordButton.name)
                    {
                        // press the button
                        chordManager.toggleActive(hitData.transform.parent.gameObject);
                        // play the chord
                        if (hitData.transform.parent.name == "Btn_major")
                        {
                            // chordManager.playNotes(-42, -37, -35, -30);
                            chordManager.playNotes(
                                root,
                                third,
                                fifth,
                                seventh + 2
                            );
                        }
                        else if (hitData.transform.parent.name == "Btn_m")
                        {
                            chordManager.playNotes(
                                root,
                                third - 1,
                                fifth,
                                seventh + 2
                            );
                        }
                        else if (hitData.transform.parent.name == "Btn_7")
                        {
                            chordManager.playNotes(
                                root,
                                third,
                                fifth,
                                seventh
                            );
                        }
                        else if (hitData.transform.parent.name == "Btn_m7")
                        {
                            chordManager.playNotes(
                                root,
                                third - 1,
                                fifth,
                                seventh
                            );
                        }
                        else if (hitData.transform.parent.name == "Btn_maj7")
                        {
                            chordManager.playNotes(
                                root,
                                third,
                                fifth,
                                seventh + 1
                            );
                        }
                        else if (hitData.transform.parent.name == "Btn_mMaj7")
                        {
                            chordManager.playNotes(
                                root,
                                third - 1,
                                fifth,
                                seventh + 1
                            );
                        }
                        else if (hitData.transform.parent.name == "Btn_dim")
                        {
                            chordManager.playNotes(
                                root,
                                third - 1,
                                fifth - 1,
                                seventh - 1
                            );
                        }
                        else if (hitData.transform.parent.name == "Btn_aug")
                        {
                            chordManager.playNotes(
                                root,
                                third,
                                fifth + 1,
                                seventh + 2
                            );
                        }
                    }
                    // else
                    // {
                    //     chordManager.setPassive(chordButton);
                    // }
                }

            }
        }
    }

    void toggleOnOff()
    {
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            print("muted");
            pd.SendBang("chordOff");
            pd.SendBang("bassOff");
            pd.SendBang("drumsOff");
        }
        else if (Input.GetKeyDown(KeyCode.Return))
        {
            print("playing");
            pd.SendBang("chordOn");
            pd.SendBang("bassOn");
            pd.SendBang("drumsOn");
        }
    }
    void grabButton()
    {
        // buttonRigidBody.constraints = RigidbodyConstraints.;
    }
    void toggleSliderActive(GameObject slider)
    {
        if (slider.GetComponent<sliderScript>().isActive)
        {
            slider.GetComponent<sliderScript>().isActive = false;
        }
        else
        {
            slider.GetComponent<sliderScript>().isActive = true;
        }
        print("slider: " + slider.GetComponent<sliderScript>().isActive);
    }


}
