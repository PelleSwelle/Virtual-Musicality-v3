using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class sliderScript : MonoBehaviour
{
    private float yMax, yMin, yRest;
    private float passiveZ, activeZ;
    public bool isActive;
    public GameObject mesh;
    public LibPdInstance pd;
    public TextMeshPro valueField;
    Mouse mouse;
    public Material activeMaterial, passiveMaterial;
    MeshRenderer meshRenderer;
    public GameObject sliderBase;
    public Collider topCollider, bottomCollider;
    // sliderButton sliderBtn;

    void OnValidate()
    {
        passiveZ = 1.6f;
        activeZ = .1f;
        isActive = false;
        mesh = this.transform.GetChild(0).gameObject;
        mouse = GameObject.Find("mouseControl").GetComponent<Mouse>();
        meshRenderer = mesh.GetComponent<MeshRenderer>();
        sliderBase = this.transform.GetChild(2).gameObject;
        topCollider = sliderBase.transform.GetChild(1).GetComponent<Collider>();
        bottomCollider = sliderBase.transform.GetChild(2).GetComponent<Collider>();
        // setting the rest position of the slider
        yRest = mesh.transform.position.y;
        yMin = yRest - .2f;
        yMax = yRest + .2f;
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawSphere(new Vector3(mesh.transform.position.x, yMin, mesh.transform.position.z), .1f);
        Gizmos.DrawSphere(new Vector3(mesh.transform.position.x, yMax, mesh.transform.position.z), .1f);
    }

    void Update()
    {
        if (this.isActive)
        {
            moveSlider();
        }

        // TODO this should be rounded to one or two decimals.
        valueField.SetText(this.transform.GetChild(0).transform.localPosition.y.ToString());
        // this script is attached to all sliders, so check which one, we are grabbing.
        if (this.name == "CutoffSlider")
        {
            pd.SendFloat("chordCutoff", this.transform.GetChild(0).transform.localPosition.y);
        }
        else if (this.name == "BassSlider")
        {
            pd.SendFloat("bassVolume", this.transform.GetChild(0).transform.localPosition.y);
        }
        else if (this.name == "DrumSlider")
        {
            pd.SendFloat("drumVolume", this.transform.GetChild(0).transform.localPosition.y);
        }
        else if (this.name == "ChordVolumeSlider")
        {
            pd.SendFloat("chordVolume", this.transform.GetChild(0).transform.localPosition.y);
        }
        else if (this.name == "TempoSlider")
        {
            // TODO this one does not work yet.
            pd.SendFloat("tempo", this.transform.GetChild(0).transform.localPosition.y * 1000);
        }
    }

    /// <summary>
    /// Sets a slider to passive, including the position and the ability to move on the y-axis
    /// </summary>
    public void setPassive()
    {
        this.isActive = false;
        mesh.transform.position = new Vector3(
            mesh.transform.position.x,
            mesh.transform.position.y,
            mesh.transform.position.z
        );
        print("set slider to passive");
        meshRenderer.material = passiveMaterial;
    }
    public void setActive()
    {
        this.isActive = true;

        print("set slider to active");

        meshRenderer.material = activeMaterial;
    }

    void moveSlider()
    {
        if (mesh.transform.position.y > yMax)
        {
            mesh.transform.position = new Vector3(
                mesh.transform.position.x,
                yMax,
                mesh.transform.position.z
            );
        }
        else if (mesh.transform.position.y < yMin)
        {
            mesh.transform.position = new Vector3(
                mesh.transform.position.x,
                yMin,
                mesh.transform.position.z
            );
        }
        else
        {
            mesh.transform.position = new Vector3(
                mesh.transform.position.x,
                mouse.worldPosition.y,
                mesh.transform.position.z
            );
        }
    }
}
