using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class Rotire : MonoBehaviour
{
	
	public float timer = 0f;  
	public float timer_2 = 0f;
	public float sec_open = 2.0f;
	public float sec_close = 5.0f;	
	public float grade = 0.0f;
	private int grade0 = 0;
	
	public bool isOpen = false;
	public bool isManual = false;
	
	private int timer_for_open= 0;
	private int timer_for_close= 0;
	
	
	
	
	private float temp;
	private float temp1;
	private float temp2;

	//public bool esteManual = false;
	public Toggle toggle;
	public Button yourButton;
	
	
	public Button button;
	public Button button_Close;
	
	
	
	public bool DeschisSauInchis= false;
	private int numaratorSunet=0;
	
	public Slider mainSlider;

			
			
    // Start is called before the first frame update
    void Start()
    {
		timer = 0f;  
		timer_2 = 0f;		
		isManual = false;
		call();
		
		Button btn = yourButton.GetComponent<Button>();
		btn.onClick.AddListener(TaskOnClick);       
    }

    // Update is called once per frame
    void Update()
    {
		isManual = toggle.isOn;
		
		if( isManual == false){
			
			
			if ( isOpen == true ){
				timer += Time.deltaTime;
				
				if ( timer >= sec_open ){
					transform.rotation = Quaternion.Euler(0, 0, grade0); // this is 90 degrees around Z axis
					timer = 0;
					timer_2 = 0;
					isOpen = false;	
				}		
			}
			
			if ( isOpen == false ){
				timer_2 += Time.deltaTime;
				
				if ( timer_2 >= sec_close ){
					transform.rotation = Quaternion.Euler(0, 0, grade); // this is 90 degrees around Z axis
					timer = 0;
					timer_2 = 0;
					isOpen = true;	
				}		
			}
			
			
			
			
			
			/*
			
			timer += Time.deltaTime;
			timer_2 += Time.deltaTime;
			if ( ( timer >= sec_open ) && (!isOpen) ){
				timer_for_open++;
				
				if (timer_for_open == 1 ){
					//var rot = transform.rotation;
					//transform.rotation = rot * Quaternion.Euler(0, 0, grade); // this is 90 degrees around Z axis
					transform.rotation = Quaternion.Euler(0, 0, grade); // this is 90 degrees around Z axis
					timer_2 = 0;
					isOpen = true;
				}
				
				
				if ( (timer_2 >= sec_close) && isOpen ) {
				transform.rotation = Quaternion.Euler(0, 0, grade0); // this is 90 degrees around Z axis
				timer_for_open = 0;
				timer = 0;
				timer_2 = 0;
				isOpen = false;			
				
				}
			}
			
			*/
		}//end if(isManual == false)
			
		if( isManual == true ){ //activeaza modul manual
			
			if (DeschisSauInchis == true){
				transform.rotation = Quaternion.Euler(0, 0, grade); // this is 90 degrees around Z axis
				
				timer = 0f;  
				timer_2 = 0f;
				timer_for_open = 0;
				isOpen = true;

			}
			if (DeschisSauInchis == false){
				transform.rotation = Quaternion.Euler(0, 0, grade0); // this is 90 degrees around Z axis
				
				timer = 0;
				timer_2 = 0;
				timer_for_open = 0;
				isOpen = false;	
			}
			
		}
		
		
		if(isOpen){
			TaskOnClick();			
		}
		
		if(!isOpen){
			TaskOnClickClosed();			
		}
		
    }
	
	
	public void Text_Change(string newText){
		temp = float.Parse(newText);
		
	}
	
	public void Text_Change1(string newText1){
		temp1 = float.Parse(newText1);
	
	}

	public void Text_Change2(string newText2){
		temp2 = float.Parse(newText2);
	
	}
	
	
	public void setTemp(){
		
		sec_open = temp;
		
	}
	
	public void setTemp1(){
		
		sec_close = temp1;
		
	}

	public void setTemp2(){
		
		grade = mainSlider.value;
		
	}
	
	
	
	public void ApasaButonulManual(){
		if(isManual){
		DeschisSauInchis = false;
		}

	}
	
	public void ApasaButonulManual2(){   //aici e closed    adica are bifa
		if(isManual){
		DeschisSauInchis = true;
		}

	}
	
	
	void TaskOnClick(){
		numaratorSunet++;

		if (numaratorSunet == 1 ){
			Debug.Log ("You have clicked the button!");
			button.onClick.Invoke();
		}
		
		if (numaratorSunet == 10 ){
			button_Close.onClick.Invoke();
			
		}
		
		if (numaratorSunet == 25 ){
			numaratorSunet = 0;

		}		

    }
	
	
		void TaskOnClickClosed(){
		numaratorSunet++;
		
		if (numaratorSunet == 10 ){
			Debug.Log ("You have clicked the button!");
			button_Close.onClick.Invoke();
		}
		
		if (numaratorSunet >= 11 ){
			numaratorSunet = 0;
		}		

    }

	void call(){
		
		transform.rotation = Quaternion.Euler(0, 0, grade0);
		isOpen = false;
		
	}

}
