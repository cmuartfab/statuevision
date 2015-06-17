# statuevision Unity Project for Glasgow, UK
This unity project is set up to work with 10 example 3d models in the repository. To properly get a project running, follow these steps:

1. Download the project folder you want here. 

2. Go to google drive and download the corresponding binaries folder: https://drive.google.com/file/d/0B9xrGq3470WfTUhNZU5VNFd2Zkk/view?usp=sharing

3. Move the contents of the binaries folder into "Standard Assets/Models/"

To import additional models into the project, follow these steps:

NOTE: Statue should face towards the positive X direction. Its top should be pointing in the positive Y direction.

1. Open "glasgow.unity" scene.

2. Place the folder containing the statue model into "Standard Assets/Models/".

3. The statue should import into the project automatically, but if it doesn't, just right-click on the folder and click "Reimport".

4. Insert the "Statue Prefab" from the Standard Assets/Prefabs folder into the scene. 

5. Click and drag the statue mesh onto the "Statue Prefab" in the hierarchy (left side of window) to parent it. 

6. Go into the "Materials" folder that should be automatically created inside the statue folder.

7. Repeat steps 8-9 on each of the materials in the folder.

8. If the ball in the preview has no texture on it, this material is for the part of the sculpture that didnt get scanned properly, so you can end after this step. Look at the inspector (right side of the window), click on the dropdown menu titled "Shader" and select "Distort".

9. If the ball had texture, the sculpture in your scene should now be white. You'll have to manually reattach the correct texture to the sculpture by clicking on "Select" on the box indicating texture just below the "Shader" dropdown menu. Watching the sculpture in the scene should let you see if you've chosen the correct texture.
