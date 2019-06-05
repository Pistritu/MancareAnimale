using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.Events;

public class toggle : MonoBehaviour
{
	
	public Button yourButton;
	
    // Start is called before the first frame update
    void Start()
    {
		Button btn = yourButton.GetComponent<Button>();
		btn.onClick.AddListener(TaskOnClick);       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
	
	void TaskOnClick(){
		Debug.Log ("You have clicked the button!");
	}
}
