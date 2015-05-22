# statuevision
This unity project is set up to work with 10 example 3d models in the repository. To properly get a project running, follow these steps:

1. Download the project folder you want here. 

2. Go to google drive and download the corresponding binaries folder: https://drive.google.com/file/d/0B9xrGq3470WfUGhkNFVUa0VxYnc/view?usp=sharing

3. Move the contents of the binaries folder into the standard assets folder of the Unity project.

To import additional models into the project, follow these steps:

1. Open "Template.unity" scene. M

2. Place the folder containing the statue model into the assets folder of the Unity project. Somewhere like a models folder.

3. The statue should import into the project automatically, but if it doesn't, just right-click on the folder and click "Reimport".

4. Insert the "Scripted Sculpture" from the prefabs folder into the scene. 

5. Click and drag the statue mesh onto the "Scripted Sculpture" in the hierarch (left side of window) to parent it. 
6. Go into the "Materials" folder that should be automatically created inside the statue folder.

7. Repeat steps 8-10 on each of the materials in the folder.

8. If the ball in the preview has no texture on it, this material is for the part of the sculpture that didnt get scanned properly, so you can end after this step. Look at the inspector (right side of the window), click on the dropdown menu titled "Shader" and select "Distort". (if "Distort" is not there, you are not using the right Unity project.)

9. If the ball had texture, the sculpture in your scene should now be white. You'll have to manually reattach the correct texture to the sculpture by clicking on "Select" on the box indicating texture just below the "Shader" dropdown menu. Watching the sculpture in the scene should let you see if you've chosen the correct texture.

10. Take note of which direction the sculpture is facing. Set the twist to 180. If the sculpture is not facing the direct opposite of where it was before, tweak the "Eye Level" value until it does. Ensure that all materials in the folder have the same Eye Level value.

Now that you've created a unique, functional sculpture prefab, click and drag that prefab into the prefabs folder and rename it something reasonable.
