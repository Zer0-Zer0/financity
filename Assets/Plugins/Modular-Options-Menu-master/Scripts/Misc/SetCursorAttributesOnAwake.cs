using UnityEngine;

namespace ModularOptions {
	/// <summary>
	/// Simple utility script for changing cursor attributes after loading scenes.
	/// </summary>
	[AddComponentMenu("Modular Options/Misc/Set Cursor Attributes On Awake")]
	public class SetCursorAttributesOnAwake : MonoBehaviour {

		public bool visible = true;

		void Awake(){
			Cursor.visible = visible;
		}

		/// <summary>
		/// Public function for potential reference in UI events.
		/// </summary>
		public void SetCursorVisibility(bool _visible){
			Cursor.visible = _visible;
		}
	}
}
