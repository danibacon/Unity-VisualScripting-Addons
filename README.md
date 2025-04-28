# Unity Visual Scripting More

A project for extending Unity Visual Scripting with custom nodes.  
Development is open to the community.

---

# Installing

1. Open your Unity project.  
2. New projects should first initialize the build in Unity Visual Scripting package:  
   Edit \> Project Settings \> Visual Scripting \> Initialize button.  
   The Unity.VisualScripting.Generated folder should be created in the projects Assets.  
3. Open the Unity Package Manager: Window \> Package Manager.  
4. Click the "+" button in the top-left corner, and choose to install from Git.

   ![Add package Screenshot](https://github.com/danibacon/Unity-VisualScripting-More/blob/main/Images/vsmore-add-package-screenshot.jpg)  
     
4. Add the following url and hit the Install button.  
   > [git@github.com:danibacon/Unity-VisualScripting-More.git](mailto:git@github.com)  
5. Refresh the visual scripting library: Edit \> Project Settings \> Visual Scripting \> Press the Regenerate Nodes button.  
6. That's it\! The new nodes will be available for use.

### Adding a custom node

1. Add a visual script to any Game Object (Add Component \> Script Machine).  
2. In the script graph add new node: Right click visual script canvas \> Add Node.  
3. Custom nodes will be found under the **More** category.

---

# What's included

Variables

- **Variable Add**  
- **Variable Subtract**  
- **Variable Divide**  
- **Variable Multiply**

Physics

- **Move Position X**  
- **Move Position Y**  
- **Translate**  
- **On Collision Enter 2D**  
- **On Collision Stay 2D**  
- **On Collision Exit 2D**  
- **On Trigger Enter 2D**  
- **On Trigger Stay 2D**  
- **On Trigger Exit 2D**

Collections

- **Random Item :** Optionally preventing repeats  
- **Next Item**

Control

- **If (Number / String / Boolean) :** Simplified for basic conditions, with optional else. 

Math

- **Counter :** With optional custom step  
- **Confine**

Time

- **StopWatch**  
- **Format Time**

Other

- **Get Position :** Get X, Y and Z directly and not a Vector3  
- **Get Mouse Position 2D :** Get Vector2 mouse position in world space units  
- **Debug Log :** With string text field for input

---

# Maintainers

- [danibacon](https://github.com/danibacon)
