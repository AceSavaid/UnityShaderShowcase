# ComputerGraphicsProject
This project is a level based, 3D third person platformer.  In this game the plyaer avoids obstacles, shoot enemies and platform in their attempt to reach the end of each stage.  

## Final Project:  
Credits: Line shadow texture was from tutorrial.  Bases of scripts were from lectures.  Probuilder and Cinemachine were used. 
All other textures, normal maps were made by us.  
Slides: https://docs.google.com/presentation/d/10Lnsc44KVlqOOGGfMC5SAeE4YbPRhudr1aMxsp9T6-0/edit?usp=sharing  
Youtube Video:  https://youtu.be/zTegaFG-DZ8

Roles 
Alannis - Itegrations, Toon Shader, Water Shader, Holograms, Shadow Shader, Upgraded outlines, Creating Debug mode
Bradley - Level Design and Implementaion, Depth of Field and Bloom shader, LUT Shader, Selection and Placement of Materials 
Carlos - Pixelization 

### Itegrations
- Combined shaders so that more effects can be done as well as more variations to the possibilities of materials.  It is also for optimization, for example having the toon shader and the outline shader be one shader, as well as having variations with displacement maps, normal maps and more for different effects.   
- Adjusted the bloom and blur to fit the aesthetic of the game more.  
-  Added systems to make things togglable in debug mode.  

![image](https://user-images.githubusercontent.com/91763901/229975701-0d36eb0a-7c3c-4494-8eb5-cdb9f9571ce5.png)
![image](https://user-images.githubusercontent.com/91763901/229975711-81a7eb9b-4b03-4f8e-bc15-9e3f6492ee2d.png)
![image](https://user-images.githubusercontent.com/91763901/229975721-bba73904-9bcd-4c74-9ab6-cebfd16f30be.png)


### Texturing
- Use of material and uv sampling
- use of different textures such as toon ramps, normal maps and displacement maps

### Colour Correction
- Uses a script on the camera to render LUTs stored in materials onto the camera, the camera refereces the currently inplace lut to know what colour style it should use in visualizing the scene.  
- Added areas that change what LUT the camera is rendering, this is to help with showing off area changes and adding to the environmental aestetic of the game. 
- Several Luts were created to give off different feels such as cool, warm and very hot.  
![image](https://user-images.githubusercontent.com/91763901/229975955-e6a0d78d-08d1-4b87-a71f-31fb81bccd6d.png)
![image](https://user-images.githubusercontent.com/91763901/229975967-0707a21c-3038-4205-9509-baf613b68d82.png)
![image](https://user-images.githubusercontent.com/91763901/229975976-72be0509-c6d3-4cc3-93f1-7e7a7f88b1d5.png)


### Visual Effects
- Holograms are similar to rim lighting while void is similar to reverse rimlighting, however these effects havethe added style of transparency and the ability to use a material on the effect for more details and variations.  

![image](https://user-images.githubusercontent.com/91763901/229976168-2046a459-6a78-43eb-9cdb-d4ee8e06efad.png)


### Additional Effects
#### Outlining 
- Combining this shader into other shaders to add this effect to more shaders.
- This extuds the edges of a shape and colours it a specific colour before rendering the actual object infront of this to give the illusion of outlining. 
- This adds clarity and distinction to our objects, especially since toon shading can make some objects lack depth. 
![Screenshot 2023-04-04 060107](https://user-images.githubusercontent.com/69608587/229983712-ac30132b-5566-448b-9ac6-504bd334c046.png)
![Screenshot 2023-04-04 055728](https://user-images.githubusercontent.com/69608587/229983736-6e3cd579-ae91-46cf-a37c-b470a08d7f09.png)


#### Toon Shading 
- Uses a material called a toon ramp to apply shading to an object based on how much light is directly hitting that part of the object
- Also added normal mapping and shadows to enhance the toon shading.  
- This was used to create a more toony style to the game.  
![image](https://user-images.githubusercontent.com/69608587/229981858-b6148ff3-892f-4634-a5dd-00f259ab8984.png)

#### Water 
- Displaces the vertecies of the object in the form of a sine wave
- This is to add life and movement to the scene.  Toon shading was added to it to help the shader have shadows but also fit the aethetic. 
![Screenshot 2023-04-04 023331](https://user-images.githubusercontent.com/69608587/229982829-0cba186f-2bd1-400e-a7bd-133e33a15041.png)

#### Shadows
- Added textured shadows that can also be coloured and have their transpaceny altered. 
- This allows for the object to have a completely different colour scheme and material dependant on whether that part of the object is in shadow or not.
- This was used to add more stylization to the game as well as more customizations to the look of the game
![Screenshot 2023-04-04 063620](https://user-images.githubusercontent.com/69608587/229983601-52fd1ace-e1cd-4f0d-9079-5262d0d7aa2b.png)
![Screenshot 2023-04-04 063748](https://user-images.githubusercontent.com/69608587/229983612-eb8bb6e7-cd6e-4790-9f10-a699f838a951.png)


### Post processing
#### Bloom 
- Adds a blurring intesity to the objects it is rendered on.  Makes objects more bright and saturated.
- Iterations affects how bright it gets, thresholds is like the lower limit to things affected by the blur.   
- This was used to add to the style of the game, making it more similar to early 3D console games.
![image](https://user-images.githubusercontent.com/91763901/229976471-8bc79d18-6858-496a-b567-c781c1c9f6ff.png)
![image](https://user-images.githubusercontent.com/91763901/229976476-a1ca00ce-8815-45fc-8410-70a3ae72a0a7.png)
 
#### Depth of field 
- turned the regular blur script into a depth of field script  
- takes in a circle of confusion value and using the given range and depth apples a level of bluring to what is being rendered on the screen.  
- Range is how far the transition from blurry to not blury is from each other, coc is the actual circle of blurriness.  Depth is distance from camera that it blurs.  

- this was used to add depth to our game as well as fade away the borders of the level.   
![image](https://user-images.githubusercontent.com/91763901/229976592-2d4d5be2-f5e6-4336-a6b2-70f86bea775a.png)
![image](https://user-images.githubusercontent.com/91763901/229976603-801c995e-2c52-4e62-8606-533a20270d91.png)

#### Pixelizatoin

![image](https://user-images.githubusercontent.com/91763901/229976637-9b372e8d-e39b-46ab-baf7-9b1d1b4a0e56.png)
 - - - - - - - - - - - - - - - - - - - -

# **Group Assignmemt**   
## Supporting Documents
Slides:  https://docs.google.com/presentation/d/1qHUVmM7UDbQPsCz0wPt5AmObGQ26cu6l9dbFuFq-ypY/edit?usp=sharing
YoutubeLink: https://youtu.be/R_RzUvM6myo  
## Contributions  
Bradley  
- Bloom Shader  
- Decals  
- Level Designs and Layout  

Alannis  
- Depth of Field  
- Void Material  
- Programming  

Carlos  
- Sound Effects  
- Outlines  
- Particles  

## Explainations  
Void Material  
This works similar to the rim lighting shader but it allows transparency and and alpha value.  The Saturation of the dot product of the normalised view direction and normal is set as the rim and that is then multiplied by the rim power.  This rim value is then applied to the level of colour and transparency the object has so that the center is the most opaque and coloured and the outer edges are transparents and lack colour.  

Decal  
Gets two textures and applies one on top of the other to give it more detail such as bullet holes or scorch marks  

Particles  
Applies a material to a sprite in the particle system to have various properties and functions appied to it.  

Depth of Field  
Blurs what is rendered in the camera by how far it is from the  camera. 


# **Individual Assignment**  

## Alannis Davis
Project Manager + Gameplay Programmer  
 - Codes the main mechanics and systems of the game as well as manages the progress of the overall game.   


Link to Slides:  https://docs.google.com/presentation/d/1OFhTw9AdopR1gQyJs2cxFFqDmimmFOSGA32SsXMZ3lc/edit?usp=sharing  
Demo Video:  https://youtu.be/ZYAwaEgcMEQ
### Explaination & Documentation  
Worked on scripts for a base of the project as they were already started on in the project and it didnt make sense to redo them.
Probuilder and https://cpetry.github.io/NormalMap-Online/ were used. 


## Bradley Cameron 
Enemy and Level Designer


Link to Slides:  
Demo Video:  https://drive.google.com/file/d/1INVHxMU0O_wMb6BsLeTVq8Fezb1EG5mp/view?usp=share_link
Demo Video:  
### Explaination & Documentation  
Some scripts like the base player and genral kill scrips were made as a group as a base and then edited in the scenes as the project was in progression and it would not be optimal to recode the same scripts and mechanics for the project multiple times over.  
Probuilder was used.

## Carlos Lu
Sound Designer  
- Creates the sound effects and soundtrack of the game, also assisting with vfx when needed.  
- Has a mini project along side the main game for additional content.

Link to Slides:  https://docs.google.com/presentation/d/1awsoW2XGMIp2MkMkAQZ2CAZEJfHon6Xx0WO9VoaPU-U/edit?usp=sharing

Demo Video:  https://youtu.be/ZWYCuUv2rcc

### Explaination & Documentation 

**Third Party Plugins Used:** Cinemachine & New Input System 

