using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//INHERITANCE
public class Human : Unit
{
    //POLYMORPHISM
    protected override void PlayAnimation(string movingBoolName)
    {
        //Maybe another unit will have different name or extra animations that special to it
        base.PlayAnimation("_isRunning");
    }
}
