using HedgehogTeam.EasyTouch;

namespace FoodStoryTAS
{
	public class ObjectsContainerRotatorAnimation : RotatorAnimation
	{
		public override void OnEnable()
		{
			EasyTouch.On_TwistEnd += BeginRotation;
			EasyTouch.On_PinchEnd += BeginRotation;
			EasyTouch.On_Drag += BeginRotation;
		}

		public override void OnDisable()
		{
			EasyTouch.On_TwistEnd -= BeginRotation;
			EasyTouch.On_PinchEnd -= BeginRotation;
			EasyTouch.On_Drag -= BeginRotation;
		}
	}
}