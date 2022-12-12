using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CartController : MonoBehaviour
{
    CinemachineDollyCart cart;
    float baseSpeed;
    [SerializeField] float trackSwitchSafety = 100f;
    [SerializeField] CinemachinePath mainTrack;
    [SerializeField] List<SideTrack> altTracks;
    private SideTrack currentSideTrack;
    private TrackSide cartDirectionVertical;
    private TrackSide cartDirectionHorizontal;
    private ArduinoControls arduinoControls;

    void Start()
    {
        cart = GetComponent<CinemachineDollyCart>();
        baseSpeed = cart.m_Speed;
        arduinoControls = FindObjectOfType<ArduinoControls>();
    }

    void Update()
    {
        //switch to an alternative track if there is one available in the direction the player is moving
        foreach (var altTrack in altTracks)
        {
            if (CheckTrackSwitchable(altTrack, cartDirectionVertical, cartDirectionHorizontal))
            {
                SetAltTrackAsMainTrack(altTrack);
                break;
            }
        }

        //set the speed of the cart
        if(arduinoControls != null && arduinoControls.isConnected()) cart.m_Speed = baseSpeed * arduinoControls.Speed;
        else if (Input.GetButton("Jump")) cart.m_Speed = baseSpeed * 2;
        else cart.m_Speed = baseSpeed;

        //check if the player is at the end of a sidetrack
        if (cart.m_Path != mainTrack && cart.m_Position == currentSideTrack.track.PathLength)
        {
            //set followed path & position
            cart.m_Path = currentSideTrack.trackToSwitchBackTo;
            cart.m_Position = currentSideTrack.transferBackPos;
            if (cart.m_Path != mainTrack) //cart is on sidetrack
            {
                //set currentSideTrack to the new track
                foreach (var altTrack in altTracks)
                {
                    if (altTrack.track == cart.m_Path)
                    {
                        currentSideTrack = altTrack;
                    }
                }
            }
        }
    }

    public void SetAltTrackAsMainTrack(SideTrack altTrack)
    {
        cart.m_Path = altTrack.track;
        cart.m_Position = 0f;
        currentSideTrack = altTrack;
    }

    //function to set the direction the cart is going
    public void SetDirection(TrackSide newCartdirectionVertical, TrackSide newCartDirectionHorizontal)
    {
        cartDirectionVertical = newCartdirectionVertical;
        cartDirectionHorizontal = newCartDirectionHorizontal;
    }

    //check if you can switch to the chosen sidetrack whilst moving in a given direction
    bool CheckTrackSwitchable(SideTrack sideTrack, TrackSide movementDirectionVertical, TrackSide movenmentDirectionHorizontal)
    {
        float transferMinPos = sideTrack.transferToPos - cart.m_Speed / trackSwitchSafety;
        if (cart.m_Position >= transferMinPos && cart.m_Position <= sideTrack.transferToPos &&
            cart.m_Path == sideTrack.trackToSwitchFrom &&
            sideTrack.unlocked &&
            (movementDirectionVertical == sideTrack.trackSide || movenmentDirectionHorizontal == sideTrack.trackSide))
            return true;
        return false;
    }
}
