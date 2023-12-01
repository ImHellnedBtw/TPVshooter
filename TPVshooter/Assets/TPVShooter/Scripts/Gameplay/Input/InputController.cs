using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class InputController : MonoBehaviour
{
    public static GameObject inputController;
    private IControllable controllable;
    private bool buttonForward;
    private bool buttonBackward;
    private bool buttonLeft;
    private bool buttonRight;
    private bool reload;
    private bool jump;
    private bool shift;
    private bool first;
    private bool second;
    private bool third;
    private bool mouseLeft;
    private bool mouseRight;

    //bind keys
    public static KeyCode ButtonForward => KeyCode.W;
    public static KeyCode ButtonBackward => KeyCode.S;
    public static KeyCode ButtonLeft => KeyCode.A;
    public static KeyCode ButtonRight => KeyCode.D;
    public static KeyCode Reload => KeyCode.R;
    public static KeyCode Jump => KeyCode.Space;
    public static KeyCode Shift => KeyCode.LeftShift;
    public static KeyCode First => KeyCode.Alpha1;
    public static KeyCode Second => KeyCode.Alpha2;
    public static KeyCode Third => KeyCode.Alpha3;
    public static KeyCode MouseLeft => KeyCode.Mouse0;
    public static KeyCode MouseRight => KeyCode.Mouse1;

    private void Awake()
    {
        if (inputController == null)
            inputController = this.gameObject;
        else
            Destroy(this.gameObject);
    }


    void Start()
    {
        controllable = CharacterBase.Character.GetComponent<IControllable>();
        DontDestroyOnLoad(gameObject);
    }

    private void FixedUpdate()
    {
        // movement keys
        if (Input.GetKey(ButtonForward))
            buttonForward = true; else
            buttonForward = false;
        if (Input.GetKey(ButtonBackward))
            buttonBackward = true; else 
            buttonBackward = false;
        if (Input.GetKey(ButtonLeft))
            buttonLeft = true; else
            buttonLeft = false;
        if(Input.GetKey(ButtonRight))
            buttonRight = true; else
            buttonRight = false;
        if(Input.GetKey(Shift))
            shift = true; else
            shift = false;
        controllable.MovementKeys(buttonForward, buttonBackward, buttonLeft,buttonRight, shift);

        // change weapon
        if(Input.GetKeyDown(Reload))
            reload = true; else
            reload = false;
        if(Input.GetKeyDown(First)) 
            first = true; else
            first = false;
        if(Input.GetKeyDown(Second))
            second = true; else
            second = false;
        if(!Input.GetKeyDown(Third))
            third = true; else
            third = false;
        controllable.ChangeWeapon(first,second, third , reload);

        // jump button
        if(Input.GetKeyDown(Jump)) 
            jump = true; else
            jump = false;
        controllable.JumpButton(jump);

        //mouse button
        if(Input.GetKey(MouseLeft))
            mouseLeft = true; else
            mouseLeft = false;
        if (Input.GetKey(MouseRight))
            mouseRight = true; else
            mouseRight = false;
        controllable.MouseButton(mouseLeft,mouseRight);

        // mouse aim
        controllable.MouseAim(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));


    }
}
