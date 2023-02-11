using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayNoteOnTone : MonoBehaviour
{
    public AK.Wwise.Event C;
    public AK.Wwise.Event D;
    public AK.Wwise.Event E;
    public AK.Wwise.Event F;
    public AK.Wwise.Event G;
    public AK.Wwise.Event A;
    public AK.Wwise.Event B;

    public AK.Wwise.Event currentNote;

    public void PlayNote(string name, GameObject tone)
    {
        switch (name)
        {
            case "C":
                currentNote = C;
                break;
            case "D":
                currentNote = D;
                break;
            case "E":
                currentNote = E;
                break;
            case "F":
                currentNote = F;
                break;
            case "G":
                currentNote = G;
                break;
            case "A":
                currentNote = A;
                break;
            case "B":
                currentNote = B;
                break;
            default:
                currentNote = C;
                break;
        }
        currentNote.Post(tone);
    }
}
