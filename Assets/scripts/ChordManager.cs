using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChordManager : MonoBehaviour
{
    public LibPdInstance pd;

    // BUTTONS
    public GameObject btn_major, btn_minor, btn_7, btn_m7, btn_maj7, btn_mMaj7, btn_dim, btn_aug;

    // public GameObject[] buttons;

    public ArrayList buttonsList;

    // NOTE VALUES
    public int root;
    public int third;
    public int fifth;
    public int seventh;


    private float passiveZ, activeZ;
    void OnValidate()
    {
        // base values. equates to a C7 chord
        root = -42;
        third = -38;
        fifth = -35;
        seventh = -32;

        btn_major = GameObject.Find("Btn_major");


        btn_minor = GameObject.Find("Btn_minor");

        btn_7 = GameObject.Find("Btn_7");

        btn_m7 = GameObject.Find("Btn_m7");

        btn_maj7 = GameObject.Find("Btn_maj7");

        btn_mMaj7 = GameObject.Find("Btn_mMaj7");

        btn_dim = GameObject.Find("Btn_dim");

        btn_aug = GameObject.Find("Btn_aug");


        // buttons = new GameObject[6];
        // buttons[0] = majorBtn;
        // buttons[1] = minorBtn;
        // buttons[2] = seventhBtn;

        // buttons[3] = maj7Btn;
        // buttons[4] = dimBtn;
        // buttons[5] = augBtn;
        buttonsList = new ArrayList();
        buttonsList.Add(btn_major);
        buttonsList.Add(btn_minor);

        // foreach (GameObject button in buttons)
        // {
        //     print(button.name);
        // }

        // MANIPULATE THESE VALUES
        passiveZ = 1.6f;
        activeZ = 1.8f;

    }
    void Start()
    {
        // for (int i = 0; i < buttons.Length; i++)
        // {
        //     // initially set all the chord buttons to not pressed
        //     // buttons[i].GetComponent<ButtonState>().isPressed = false;
        // }

    }

    void Update()
    {
        toggleOnOff();


        // if (Input.GetKeyDown(KeyCode.O))
        // {
        //     toggleActive(btn_dim);
        //     playNotes(root, third - 1, fifth - 1, seventh - 1);
        // }
        // if (Input.GetKeyDown(KeyCode.P))
        // {
        //     toggleActive(btn_aug);
        //     playNotes(root, third + 1, fifth + 1, seventh + 2);
        // }
    }

    public void toggleActive(GameObject button)
    {
        Transform btnTransform = button.transform.GetChild(0);
        if (button.GetComponent<ButtonState>().isPressed)
        {
            // set the state of the button
            button.GetComponent<ButtonState>().isPressed = false;

            // physically move the button
            btnTransform.position = new Vector3(
                btnTransform.position.x,
                btnTransform.position.y,
                passiveZ
            );
        }
        else
        {
            button.GetComponent<ButtonState>().isPressed = true;

            btnTransform.position = new Vector3(
                btnTransform.position.x,
                btnTransform.position.y,
                activeZ
            );
        }
        print("button: " + button.GetComponent<ButtonState>().isPressed);
    }

    // used for all buttons that are not pressed
    public void setPassive(GameObject button)
    {
        toggleActive(button);
        // button.GetComponent<ButtonState>().isPressed = false;
    }
    public void playNotes(int root, int third, int fifth, int seventh)
    {
        pd.SendFloat("root", root);
        pd.SendFloat("third", third);
        pd.SendFloat("fifth", fifth);
        pd.SendFloat("seventh", seventh);
    }
    void keyboardChords()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            toggleActive(btn_major);
            playNotes(root, third, fifth, seventh + 2);

        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            toggleActive(btn_minor);
            playNotes(root, third - 1, fifth, seventh - 1);
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            toggleActive(btn_7);
            playNotes(root, third, fifth, seventh);

        }
        if (Input.GetKeyDown(KeyCode.Y))
        {
            toggleActive(btn_m7);
            playNotes(root, third + 1, fifth + 1, seventh);

        }
        if (Input.GetKeyDown(KeyCode.U))
        {
            toggleActive(btn_maj7);
            playNotes(root, third, fifth, seventh + 1);

        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            toggleActive(btn_mMaj7);
            playNotes(root, third - 1, fifth, seventh + 1);
        }
        // foreach (GameObject button in buttonsList)
        // {
        //     // if (button.GetComponent<ButtonState>().isPressed)
        // }
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

        // if (Input.GetKeyDown(KeyCode.U))
        // {
        //     print("drums muted");
        //     pd.SendBang("drumsOff");
        // }
        // else if (Input.GetKeyDown(KeyCode.I))
        // {
        //     print("drums playing");
        //     pd.SendBang("drumsOn");
        // }

        // if (Input.GetKeyDown(KeyCode.T))
        // {
        //     print("bass muted");
        //     pd.SendBang("bassOff");
        // }
        // else if (Input.GetKeyDown(KeyCode.Y))
        // {
        //     print("bass playing");
        //     pd.SendBang("bassOn");
        // }

        // // TODO lead scale should always respond to the chord being played
        // if (Input.GetKeyDown(KeyCode.E))
        // {
        //     print("lead muted");
        //     pd.SendBang("leadOff");
        // }
        // else if (Input.GetKeyDown(KeyCode.R))
        // {
        //     print("lead playing");
        //     pd.SendBang("leadOn");
        // }
    }
}
