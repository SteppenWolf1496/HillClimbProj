using System.Collections;
using UnityEngine;
using UnityEditor;

class ShowSize : EditorWindow
{
	[MenuItem("Window/ShowSize")]
	
		
		
	static void Init () {
		
		ShowSize sizeWindow = new ShowSize();
		
		sizeWindow.autoRepaintOnSceneChange = true;
		
		sizeWindow.Show(); 
		
	}
	
	
	
	void OnGUI () {
		
		var thisObject = Selection.activeObject as GameObject;
		
		if (!thisObject) {return;}
		

		
		var renderer = thisObject.renderer;

		if (!renderer) {
			bool inited = false;
			float minX = 0;
			float maxX = 0;
			float resX = 0;

			float minY = 0;
			float maxY = 0;
			float resY = 0;

			foreach (Transform childTransforms in thisObject.transform)
			{

				if (!childTransforms.gameObject) continue;
				if (!childTransforms.gameObject.renderer) continue; 


				if (!inited)
				{
					inited = true;
					minX = childTransforms.gameObject.transform.position.x;
					maxX = childTransforms.gameObject.transform.position.x + childTransforms.gameObject.renderer.bounds.size.x;

					minY = childTransforms.gameObject.transform.position.y;
					maxY = childTransforms.gameObject.transform.position.y + childTransforms.gameObject.renderer.bounds.size.y;
				}



				if (minX > childTransforms.gameObject.transform.position.x) 
				{
					minX = childTransforms.gameObject.transform.position.x;
				}

				if (minY > childTransforms.gameObject.transform.position.y) 
				{
					minY = childTransforms.gameObject.transform.position.y;
				}

				if (maxX < childTransforms.gameObject.transform.position.x + childTransforms.gameObject.renderer.bounds.size.x) 
				{
					maxX = childTransforms.gameObject.transform.position.x + childTransforms.gameObject.renderer.bounds.size.x;
				}

				if (maxY < childTransforms.gameObject.transform.position.y + childTransforms.gameObject.renderer.bounds.size.y) 
				{
					maxY = childTransforms.gameObject.transform.position.y + childTransforms.gameObject.renderer.bounds.size.y;
				}

			}

			resX = Mathf.Abs (maxX - minX);
			resY = Mathf.Abs (maxY - minY);

			GUILayout.Label("Size\nX: " + resX + 
			                
			                "   Y: " + resY + "   Z: " + 0);
			return;
		}
		
		
		
		var bounds = renderer.bounds;
		
		if (bounds == null) return;
		
		
		
		var size = bounds.size;
		
		GUILayout.Label("Size\nX: " + size.x + 
		                
		                "   Y: " + size.y + "   Z: " + size.z);
		
	}
	
	
	
	void OnSelectionChange() {
		
		if (Selection.activeGameObject != null) {
			
			Repaint();
			
		}
		
	}

}
