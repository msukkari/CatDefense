using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerInputManager : MonoBehaviour {

	#region SINGLETON

	private static ControllerInputManager m_instance;
	#endregion


	#region INPUT
	#region INPUT_STRINGS
	//String that finds the platform to get the right input string to add
	private string platform;

	//String constatnts for inputs
	static string LEFT_STICK_HORIZONTAL = "HorizontalLeft";
	static string LEFT_STICK_VERTICAL = "VerticalLeft";

	static string RIGHT_STICK_HORIZONTAL = "HorizontalRight";
	static string RIGHT_STICK_VERTICAL = "VerticalRight";

	static string RT ="RT";
	static string LT ="LT";
	static string RB = "RB";
	static string LB = "LB";

	static string A = "A";
	static string B = "B";
	static string X = "X";
	static string Y = "Y";

	#endregion

	#region INPUT_EVENTS
	//Button clicks, call these if you wanna know whether that button was clicked
	public delegate void ButtonDownEvevnt();
	public event ButtonDownEvevnt OnADown;
	public event ButtonDownEvevnt OnBDown;
	public event ButtonDownEvevnt OnXDown;
	public event ButtonDownEvevnt OnYDown;
	public event ButtonDownEvevnt OnLBDown;
	public event ButtonDownEvevnt OnRBDown;
	public event ButtonDownEvevnt OnRTDown;
	public event ButtonDownEvevnt OnLTDown;

	//Axis changes is called every update, TODO: should it just when nonzero...?
	//Call these to get the value of an axis 
	public delegate void AxisChangeEvent(float amount);
	public event AxisChangeEvent OnLTChange;
	public event AxisChangeEvent OnRTChange;

	//Individual stick value changes
	public event AxisChangeEvent OnLSHChange;
	public event AxisChangeEvent OnLSVChange;
	public event AxisChangeEvent OnRSHChange;
	public event AxisChangeEvent OnRSVChange;

	//Event for sticks, is called every update
	public delegate void StickUpdateEvent(float horizontal, float vertical);
	public event StickUpdateEvent OnLSChange;
	public event StickUpdateEvent OnRSChange;
	#endregion
	#endregion

	#region Private_Variables
	//Track when LT and RT are down
	private bool lt_down = false;
	private bool rt_down = false;
	#endregion

	#region UnityUpdates
	void Start()
	{
		if (m_instance != null && m_instance != this)
		{
			Destroy (this);
		} else
		{
			m_instance = this;
		}

		if (Application.platform == RuntimePlatform.WindowsEditor || Application.platform == RuntimePlatform.WindowsPlayer)
		{
			platform = "Windows";
		} else if (Application.platform == RuntimePlatform.OSXEditor || Application.platform == RuntimePlatform.OSXPlayer)
		{
			platform = "OSX";
		}
	}

	// Update is called once per frame
	void Update () {

		float ls_h = Input.GetAxis (getPlayerInputString (LEFT_STICK_HORIZONTAL));
		float ls_v = Input.GetAxis (getPlayerInputString (LEFT_STICK_VERTICAL));

		float rs_h = Input.GetAxis (getPlayerInputString (RIGHT_STICK_HORIZONTAL));
		float rs_v = Input.GetAxis (getPlayerInputString (RIGHT_STICK_VERTICAL));


		//Right stick update events
        if(OnRSHChange != null) OnRSHChange(rs_h);
		if(OnRSVChange != null) OnRSVChange(rs_v);
		if(OnRSChange!=null) OnRSChange(rs_h, rs_v);

		if(OnLSHChange != null) OnLSHChange(ls_h);
		if(OnLSVChange != null) OnLSVChange(ls_v);
		if(OnLSChange!=null) OnLSChange (ls_h, ls_v);
		

		//Button updates
		if (OnADown != null && Input.GetButtonDown(getPlayerInputString(A)))
		{
			OnADown ();
		}
		if (OnBDown != null && Input.GetButtonDown(getPlayerInputString(B)))
		{
			OnBDown ();
		}
		if (OnXDown != null && Input.GetButtonDown(getPlayerInputString(X)))
		{
			OnXDown ();
		}
		if (OnYDown != null && Input.GetButtonDown(getPlayerInputString(Y)))
		{
			OnYDown ();
		}

		if (OnLBDown != null && Input.GetButtonDown(getPlayerInputString(LB)))
		{
			OnLBDown ();
		}
		if (OnRBDown != null && Input.GetButtonDown(getPlayerInputString(RB)))
		{
			OnRBDown ();
		}


		//Trigger update events

		//Right stick
		float rt_axis = Input.GetAxisRaw(getPlayerInputString(RT));
		if(rt_axis != 0)
		{
			if(!rt_down)
			{
				rt_down = true;
				if (OnRTDown != null)
				{
					OnRTDown ();
				}
			}
		}
		if(rt_axis == 0)
		{
			rt_down = false;
		}  

		if (OnRTChange!=null)
		{
			OnRTChange (rt_axis);
		}

		//Left stick
		float lt_axis = Input.GetAxisRaw(getPlayerInputString(LT));
		if(lt_axis != 0)
		{
			if(!lt_down)
			{
				lt_down = true;
				if (OnLTDown != null)
				{
					OnLTDown ();
				}
			}
		}
		if(lt_axis == 0)
		{
			lt_down = false;
		}  

		if (OnLTChange!=null)
		{
			OnLTChange (lt_axis);
		}
	}
	#endregion

	#region PublicMethods
	public static ControllerInputManager GetInstance()
	{
		if (m_instance == null)
		{
            GameObject g = new GameObject("ControllerInputManager");
            m_instance = g.AddComponent<ControllerInputManager>();
		}

		return m_instance;	
	}
	#endregion

	#region PrivateMethods
	private string getPlayerInputString(string input)
	{
		return platform + input;
	}

	#endregion 
}
