using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobileStick : MonoBehaviour
{
    public VegetablesGenerator vg;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void MoveLeft()
    {
        vg.MobileAxisH(-1);
    }

    public void MoveRight()
    {
        vg.MobileAxisH(1);
    }

    public void MoveStop()
    {
        vg.MobileAxisH(0);
    }

    public void ScrollLeft()
    {
        vg.MobileAxisV(1);
    }

    public void ScrollRight()
    {
        vg.MobileAxisV(-1);
    }

    public void ScrollStop()
    {
        vg.MobileAxisV(0);
    }
}
