using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeControls : MonoBehaviour
{
    // --- Singleton (On garde ça pour que Movement.cs trouve le script tout seul) ---
    private static SwipeControls instance;
    public static SwipeControls Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindFirstObjectByType<SwipeControls>();
                if (instance == null)
                {
                    instance = new GameObject("Spawned SwipeControls", typeof(SwipeControls)).GetComponent<SwipeControls>();
                }
            }
            return instance;
        }
        set { instance = value; }
    }

    // --- Variables de commande ---
    // On garde les noms exacts "swipeleft" etc. pour que le script Movement fonctionne sans modification
    public bool swipeleft, swiperight, swipeUp, jump, fastFall;

    // --- Propriétés publiques (Lecture seule pour les autres scripts) ---
    public bool Swipeleft { get { return swipeleft; } }
    public bool Swiperight { get { return swiperight; } }
    public bool SwipeUp { get { return swipeUp; } }
    public bool Jump { get { return jump; } }
    public bool FastFall { get { return fastFall; } }

    public void LateUpdate()
    {
        // 1. On remet tout à zéro au début de l'image
        swipeleft = swiperight = swipeUp = jump = fastFall = false;

        // 2. On vérifie les touches du clavier (PC uniquement)
        CheckInput();
    }

    private void CheckInput()
    {
        // Déplacement GAUCHE (Flèche, A ou Q)
        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.Q))
        {
            swipeleft = true;
        }
        
        // Déplacement DROITE (Flèche ou D)
        else if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {
            swiperight = true;
        }
        
        // Saut (Espace, Z ou Flèche Haut)
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            jump = true;
        }
        
        // Descente rapide (S ou Flèche Bas)
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            fastFall = true;
        }
    }
}