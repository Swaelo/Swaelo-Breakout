// ================================================================================================================================
// File:        MenuButtonHighlight.cs
// Description:	Changes the textures of the target meshes representing a button in one of the menus to indicate when mouse is over
// Author:	    Harley Laurie https://www.github.com/Swaelo/
// ================================================================================================================================

using UnityEngine;

public class MenuButtonHighlight : MonoBehaviour
{
    public Material MouseOffMaterial;   //Material applied to the target meshes when the mouse is away from the button
    public Material MouseOnMaterial;    //Material applied to the target meshes when the mouse is hovered over the button
    public MeshRenderer[] TargetMeshes; //List of mesh renderers which need to have their materials updated based on mouse cursor
    private bool MouseHovering = false; //Tracks if the mouse is currently hovered over this button or not

    //Applies the mouse off material to the target meshes
    public void SetMouseOff()
    {
        foreach (MeshRenderer Mesh in TargetMeshes)
            Mesh.material = MouseOffMaterial;
    }

    //Applies the mouse on material to the target meshes
    public void SetMouseOn()
    {
        foreach (MeshRenderer Mesh in TargetMeshes)
            Mesh.material = MouseOnMaterial;
    }
}
