using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorControl : MonoBehaviour
{

    SpriteRenderer ImageOfColor;

    Color ColorOfMaterial;
    Color VampireColor = new Color(0.075f, 0.066f, 0.066f);
    private void Start()
    {
        ColorOfMaterial = ListOfStyles.Instance.CurrentMaterialForWalls.color;
        ImageOfColor = GetComponent<SpriteRenderer>();
        ImageOfColor.color = ColorOfMaterial;
     
    }
    private void Update()
    {
       /* if (ListOfStyles.Instance.CurrentMaterialForWalls.color == VampireColor)
        {
            ColorOfMaterial = new Color(0.1f, 0.066f, 0.066f);
            print("Good");
        }*/
          
        ColorOfMaterial = ListOfStyles.Instance.CurrentMaterialForWalls.color;

        ImageOfColor.color = ColorOfMaterial;
    }





}
