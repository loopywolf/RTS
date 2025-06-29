using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Assertions;

public class MaterialSlot : MonoBehaviour
{
    public int materialIndex;
    public int amount = 1; //when instantiated it will be 1

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update() {

    }

    internal void setMaterialIndex(int material) {
        //throw new NotImplementedException();
        this.materialIndex = material;
    }

    internal void setAmount(int v) {
        //throw new NotImplementedException();
        this.amount = v;
    }

    internal void increment() {
        //throw new NotImplementedException();
        this.amount++;
        updateNumberDisplay();
    }

    private void updateNumberDisplay() {
        //throw new NotImplementedException();

        //get a refrence to the textmeshpro component
        TextMeshProUGUI textComponent = transform.GetComponentInChildren<TextMeshProUGUI>();
        Assert.IsNotNull(textComponent); //should never be null
        textComponent.text = this.amount.ToString();
    }

}//F
