# EasyVRBuildChangerForUnity  

An easy way to switch the Android OpenXR target between Meta Quest and Pico platforms in Unity. (Tested in Unity 2023)  

## Requirements  
- Meta Quest and Pico packages must be installed.  

## How to Use  
1. Copy the file to the `Editor` folder inside your project's `Assets` directory.  
2. When you build your project, a pop-up will appear like this:  

     ![image](https://github.com/user-attachments/assets/c443e47e-accc-45a7-b745-c2f9d0bcab27)  

   - **Meta Quest**: Activates the Meta Quest feature group (disabling Pico’s) and sets the interaction profiles to Meta Quest controllers.  
     ![image](https://github.com/user-attachments/assets/f2a774d3-59b0-48ec-a03a-4ed24335dc58)  

   - **Pico**: Activates the Pico feature group (disabling Meta Quest’s) and sets the interaction profiles to Pico controllers.  
     ![image](https://github.com/user-attachments/assets/0f70a6a2-f4df-407b-a7bf-02a674e3064d)  

   - **Other**: Continues building without making any changes.  
