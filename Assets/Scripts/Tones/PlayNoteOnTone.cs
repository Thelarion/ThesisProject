using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayNoteOnTone : MonoBehaviour
{
    public AK.Wwise.Event C2;
    public AK.Wwise.Event D2;
    public AK.Wwise.Event E2;
    public AK.Wwise.Event F2;
    public AK.Wwise.Event G2;
    public AK.Wwise.Event A2;
    public AK.Wwise.Event B2;
    public AK.Wwise.Event C3;
    public AK.Wwise.Event D3;
    public AK.Wwise.Event E3;
    public AK.Wwise.Event F3;
    public AK.Wwise.Event G3;
    public AK.Wwise.Event A3;
    public AK.Wwise.Event B3;
    public AK.Wwise.Event C4;
    public AK.Wwise.Event D4;
    public AK.Wwise.Event E4;
    public AK.Wwise.Event F4;
    public AK.Wwise.Event G4;
    public AK.Wwise.Event A4;
    public AK.Wwise.Event B4;

    private AK.Wwise.Event currentNote = new AK.Wwise.Event();



    public void PlayNote(string materialToNote, string _nameGOOctave, GameObject tone)
    {
        switch (materialToNote)
        {
            case "C":
                switch (_nameGOOctave)
                {
                    case "2":
                        currentNote = C2;
                        break;
                    case "3":
                        currentNote = C3;
                        break;
                    case "4":
                        currentNote = C4;
                        break;
                }
                break;
            case "D":
                switch (_nameGOOctave)
                {
                    case "2":
                        currentNote = D2;
                        break;
                    case "3":
                        currentNote = D3;
                        break;
                    case "4":
                        currentNote = D4;
                        break;
                }
                break;
            case "E":
                switch (_nameGOOctave)
                {
                    case "2":
                        currentNote = E2;
                        break;
                    case "3":
                        currentNote = E3;
                        break;
                    case "4":
                        currentNote = E4;
                        break;
                }
                break;
            case "F":
                switch (_nameGOOctave)
                {
                    case "2":
                        currentNote = F2;
                        break;
                    case "3":
                        currentNote = F3;
                        break;
                    case "4":
                        currentNote = F4;
                        break;
                }
                break;
            case "G":
                switch (_nameGOOctave)
                {
                    case "2":
                        currentNote = G2;
                        break;
                    case "3":
                        currentNote = G3;
                        break;
                    case "4":
                        currentNote = G4;
                        break;
                }
                break;
            case "A":
                switch (_nameGOOctave)
                {
                    case "2":
                        currentNote = A2;
                        break;
                    case "3":
                        currentNote = A3;
                        break;
                    case "4":
                        currentNote = A4;
                        break;
                }
                break;
            case "B":
                switch (_nameGOOctave)
                {
                    case "2":
                        currentNote = B2;
                        break;
                    case "3":
                        currentNote = B3;
                        break;
                    case "4":
                        currentNote = B4;
                        break;
                }
                break;
        }
        currentNote.Post(tone);
    }

    // void PostNote(string note, GameObject tone)
    // {
    //     AkSoundEngine.SetSwitch("Notes", note, tone);
    //     AkSoundEngine.PostEvent("O2", tone);
    // }
}
