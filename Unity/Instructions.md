# statuevision
To implement the statue into Unity with all the shaders and functionality included, follow these steps:

1. Open "Template.unity" scene. Make sure the Unity project you're using is the one Ralph uploaded to git or Drive.

2. Place the folder containing the statue model into the assets folder of the Unity project. Somewhere like a models folder.

3. The statue should import into the project automatically, but if it doesn't, just right-click on the folder and click "Reimport".

4. Click and drag the statue mesh onto the scene so you'll be able to see how you're affecting the shaders.

5. Go into the "Materials" folder that should be automatically created inside the statue folder.

6. Repeat steps 7-9 on each of the materials in the folder.

7. If the ball in the preview has no texture on it, this material is for the part of the sculpture that didnt get scanned properly, so you can end after this step. Look at the inspector (right side of the window), click on the dropdown menu titled "Shader" and select "Distort". (if "Distort" is not there, you are not using the right Unity project.)

8. If the ball had texture, the sculpture in your scene should now be white. You'll have to manually reattach the correct texture to the sculpture by clicking on "Select" on the box indicating texture just below the "Shader" dropdown menu. Watching the sculpture in the scene should let you see if you've chosen the correct texture.

9. Take note of which direction the sculpture is facing. Set the twist to 180. If the sculpture is not facing the direct opposite of where it was before, tweak the "Eye Level" value until it does. Ensure that all materials in the folder have the same Eye Level value.

The following steps will attach the proper scripts that will control the shader parameters and making a new prefab. These are tentative depending on what scripts we actually decide to use.

10. Find your sculpture on the Hierarchy (left side of the window) and expand it to show its components.

11. There should be only one called "default" (if not, you're using a broken file), select it. This will open its attributes on the inspector.

12. Click "Add Component" in the inspector and choose Script > Twist To Face

13. Click "Add Component" in the inspector and choose Script > Leap Control

14. The sculpture now constantly faces you and can be manipulated with Leap. Click and drag the "default" component into the statue folder to create a sculpture prefab that includes all the script. 
