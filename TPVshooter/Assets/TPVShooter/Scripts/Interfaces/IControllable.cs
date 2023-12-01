using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public interface IControllable 
{
    public void MovementKeys(bool forward , bool backward , bool left , bool right , bool shift);
    public void MouseAim(float vertical, float horizontal);
    public void ChangeWeapon(bool fWeapon, bool sWeapon, bool tWeapon , bool reload);
    public void JumpButton(bool jump);
    public void MouseButton(bool leftMouse , bool rightMouse);
}
