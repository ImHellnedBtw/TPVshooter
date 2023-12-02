using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterBase : MonoBehaviour, IControllable
{
    public static GameObject Character;

    private void Awake()
    {
        if (Character == null)
            Character = this.gameObject;
        else
            Destroy(this.gameObject);

    }

    //Interfaces
    public void ChangeWeapon(bool fWeapon, bool sWeapon, bool tWeapon, bool reload)
    {
        InteractWeapon(fWeapon,sWeapon,tWeapon,reload);
    }

    public void JumpButton(bool jump)
    {
        Jump(jump);
    }

    public void MouseAim(float vertical, float horizontal)
    {
        Aim(vertical, horizontal);
    }

    public void MovementKeys(bool forward, bool backward, bool left, bool right, bool shift)
    {
        Move(forward, backward, left, right , shift);
    }
    public void MouseButton(bool leftMouse, bool rightMouse)
    {
        Shoot(leftMouse, rightMouse);
    }


    //Methods
    public virtual void InteractWeapon(bool FirstW , bool SecondW , bool ThirdW , bool Reload)
    {
        //weapon usage and reloading
    }
    public virtual void Jump(bool Jump) 
    { 
        //jumping
    }
    public virtual void Aim(float Vertical, float Horizontal)
    {
        //aiming
    }
    public virtual void Move(bool Forward, bool Backward, bool Left, bool Right, bool Shift)
    {
        //movement
    }
    public virtual void Shoot(bool LeftMouse , bool RightMouse)
    {
        // left and right mouse buttons
    }
}
