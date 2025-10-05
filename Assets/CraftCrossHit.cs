using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class CraftCrossHit : MonoBehaviour
{
    // Start is called before the first frame update
    int counter = 0;
    private SpriteRenderer sprite = null;
    public static float fadeFactor = 0.01f;

    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        Assert.IsNotNull(sprite);
    }

    // Update is called once per frame
    void Update()
    {
        counter++;  //TODO sloppy
        sprite.color = new Color(1f, 1f, 1f, Mathf.Cos( counter * fadeFactor ));//it'll do
    }

    private void OnMouseUp() {
        Debug.Log("Place is selected.");
        Assert.IsNotNull(transform.parent);
        Assert.IsNotNull(transform.parent.GetComponent<CraftingCrosses>());
        transform.parent.GetComponent<CraftingCrosses>().craftHere( transform.position );
    }
    //TODO: A hit detector

}//class
