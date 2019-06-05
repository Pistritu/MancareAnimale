using UnityEngine;
using System.Collections.Generic;

using UINT8 = System.Byte;


namespace Ardunity
{
	[ExecuteInEditMode]
	[AddComponentMenu("ARDUnity/Controller/Motor/GenericServo")]
    [HelpURL("https://sites.google.com/site/ardunitydoc/references/controller/genericservo")]
	public class GenericServo : ArdunityController, IWireOutput<float>
	{
		public int pin;
		public bool smooth = false;

		[Range(-180, 180)]
		public int calibratedAngle = 0;
		[Range(-180, 180)]
		public int minAngle = -180;
		[Range(-180, 180)]
		public int maxAngle = 180;
		[Range(-180, 180)]
		public float angle = 0;

		public Transform handleObject;


		private int _preCalibratedAngle = 0;
		private int _preMinAngle = -180;
		private int _preMaxAngle = 180;
		private float _preAngle = 0;
		
		protected override void Awake()
		{
			base.Awake();
			
			enableUpdate = false; // only output.
		}

		void Start()
		{
			_preCalibratedAngle = calibratedAngle;
			_preMinAngle = minAngle;
			_preMaxAngle = maxAngle;
			_preAngle = angle;
		}

		void Update()
		{
			if(handleObject != null)
			{
				angle = handleObject.localRotation.eulerAngles.z;
				if(angle > 180f)
					angle -= 360f;
			}

			if(_preCalibratedAngle != calibratedAngle)
			{
				calibratedAngle = Mathf.Clamp(calibratedAngle, -180, 180);
				if(_preCalibratedAngle != calibratedAngle)
				{
					_preCalibratedAngle = calibratedAngle;
					SetDirty();
				}
			}

			if(_preMinAngle != minAngle)
			{
				minAngle = Mathf.Clamp(minAngle, -180, _preMaxAngle);
				if(_preMinAngle != minAngle)
				{
					_preMinAngle = minAngle;
					if(angle < _preMinAngle)
						angle = _preMinAngle;
				}
			}

			if(_preMaxAngle != maxAngle)
			{
				maxAngle = Mathf.Clamp(maxAngle, _preMinAngle, 180);
				if(_preMaxAngle != maxAngle)
				{
					_preMaxAngle = maxAngle;
					if(angle > _preMaxAngle)
						angle = _preMaxAngle;
				}
			}

			if(_preAngle != angle)
			{
				angle = Mathf.Clamp(angle, _preMinAngle, _preMaxAngle);
				if(_preAngle != angle)
				{
					_preAngle = angle;
					SetDirty();
				}
			}
		}
		
		protected override void OnPush()
		{
			Push((UINT8)Mathf.Clamp(_preAngle + _preCalibratedAngle + 180, 0, 360));
		}
		
		public override string[] GetCodeIncludes()
		{
			List<string> includes = new List<string>();
			includes.Add("#include <Servo.h>");
			return includes.ToArray();
		}
		
		public override string GetCodeDeclaration()
		{
			string declaration = string.Format("{0} {1}({2:d}, {3:d}, ", this.GetType().Name, GetCodeVariable(), id, pin);
			if(smooth)
				declaration += "true);";
			else
				declaration += "false);";
			
			return declaration;
		}
		
		public override string GetCodeVariable()
		{
			return string.Format("servo{0:d}", id);
		}
		
		float IWireOutput<float>.output
        {
			get
			{
				return (float)angle;
			}
            set
            {
				if(value > 360f)
					value -= 360f;
				else if(value < -360f)
					value += 360f;
				angle = (int)value;
            }
		}
		
		protected override void AddNode(List<Node> nodes)
        {
			base.AddNode(nodes);
			
            nodes.Add(new Node("pin", "", null, NodeType.None, "Arduino Digital Pin"));
            nodes.Add(new Node("angle", "Angle", typeof(IWireOutput<float>), NodeType.WireTo, "Output<float>"));
        }
        
        protected override void UpdateNode(Node node)
        {
            if(node.name.Equals("pin"))
            {
				node.updated = true;
                node.text = string.Format("Pin: {0:d}", pin);
                return;
            }
			else if(node.name.Equals("angle"))
            {
				node.updated = true;
                return;
            }
            
            base.UpdateNode(node);
        }
	}
}
