using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum BrickColor { Blue,Red,Yellow,Green }

public abstract class BrickAbstract: MonoBehaviour
{
    protected virtual BrickColor Color { get; set; }
}
