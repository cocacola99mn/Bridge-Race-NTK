using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stand : MonoBehaviour
{
    public Renderer First, Second, Third;

    [SerializeField]
    private Material firstMaterial, secondMaterial, thirdMaterial;

    public void Update()
    {
        if (LevelManager.Ins.IsState(LevelState.Win) == true)
            ChangeStandColor();
    }

    public void ChangeStandColor()
    {
        First.material = firstMaterial;
        Second.material = secondMaterial;
        Third.material = thirdMaterial;
    }
}
