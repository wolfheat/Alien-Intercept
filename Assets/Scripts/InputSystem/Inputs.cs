using UnityEngine;
using UnityEngine.InputSystem;

public class Inputs : MonoBehaviour
{
	//Singelton
	public static Inputs Instance { get; private set; }

	public PlayerControls Controls { get; private set; }

	public float Shift { get; private set; }
	public float G { get; private set; }
	public float X { get; private set; }
	public float CTRL { get; private set; }
	public float LClick { get; private set; }	
	public float Space { get; private set; }

	private void Awake()
	{
		//Singelton
		if (Instance != null) Destroy(this);
		else Instance = this;

		Controls = new PlayerControls();

	}
	public void OnLeftClick(InputValue value)
	{
		Debug.Log("On Left Click called");
	}
	private void OnEnable()
	{
		Controls.Enable();
		// ARE these aven needed?
		
		Controls.MainActionMap.LeftClick.performed += _ => LClick = _.ReadValue<float>();
		Controls.MainActionMap.LeftClick.canceled+= _ => LClick = _.ReadValue<float>();
		Controls.MainActionMap.CTRL.performed += _ => CTRL = _.ReadValue<float>();
		Controls.MainActionMap.Shift.performed += _ => Shift = _.ReadValue<float>();
		Controls.MainActionMap.Shift.canceled += _ => Shift = _.ReadValue<float>();
		Controls.MainActionMap.Space.performed += _ => Space = _.ReadValue<float>();
		Controls.MainActionMap.Space.canceled+= _ => Space = _.ReadValue<float>();
		Controls.MainActionMap.CTRL.canceled+= _ => CTRL = _.ReadValue<float>();
		Controls.MainActionMap.G.performed += _ => G = _.ReadValue<float>();
		Controls.MainActionMap.G.canceled+= _ => G = _.ReadValue<float>();
		Controls.MainActionMap.X.performed += _ => X = _.ReadValue<float>();
		Controls.MainActionMap.X.canceled+= _ => X = _.ReadValue<float>();
		
	}
	private void OnDisable()
	{
		Controls.Disable();
	}
}
