# Erosion
### PCG Erosion Demo by Anthony Menninger

This should add the ability to perform a basic erosion on a terrain heightmap used in GameAI Procedural Generation assignment.  It doesn't change the terrain asset that is created or save erosion changes, but does show how the heightmap is adjusted in the editor.

## Installation
There are two files: Erosion.cs and ErosionEditor.cs.

- Git clone the repository to access the two files.
    
        git clone https://github.com/BigTMiami/Erosion.git

- Create a folder called Editor in the root of your Assets folder.  
- Place ErosionEditor.cs in this folder.  This will allow you to adjust the terrain heightmap in the editor.
- Place the Erosion.cs folder in your StudentWork folder.  I think you could place it anywhere.
- In the Hierarchy Window, select the Terrain object, then add the Erosion.cs as a component.  You can drag the script into the inspector or use Add Component on the bottom of the inspector and select Erosion.cs Do Not add ErosionEditor.cs.
- After adding, you should see a Erode Map button and Create Heightmap Button on the botton of the inspector for the terrain.

## Usage
- To erode whatever terrain is loaded, press the **Erode Height Map** button.  It will perform X number of Erosion Cycles (20 default, but this can be adjusted).  Each cycle moves Erosion Factor (0.1 default) * ( height difference to the lowest neighbor) from the higher position to the lower position. 
- There is a very basic terrain heightmap creation using the **Create Height Map** button.  This uses the base PerlinNoise model and doesn't have the nice additive features in the core model. 
    - Only two "frequencies" are supported and they change depending on the resolution of the terrain setting.  The defaults are best for 513 resolution.
- These terrain adjustments are not saved and are not part of "playing" the model - any screenshots need to be done from the editor.

## Future Enhancements
- Use a DAG (Directed Acyclic Graph) to create a water flow model.  It looks tricky to put "water" on top of terrain.  I am  tempted to go all the way and replace the terrain and create a Mesh model.
- Add this to general processing to allow for it to be saved \ serialized with the asset.