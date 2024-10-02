using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class RoomTransitionManager : MonoBehaviour
{
    public GameObject livingRoom;
    public GameObject cantina;
    public GameObject cube;
    public GameObject mezzanine;

    public Canvas fadeCanvas;
    public Image fadeImage;

    private void Start()
    {
        // Start with LivingRoom active
        livingRoom.SetActive(true);
        cantina.SetActive(false);
        cube.SetActive(false);
        mezzanine.SetActive(false);
        
        fadeCanvas.enabled = false; // To allow interaction
    }

    public void OnButtonPressed(string roomName)
    {
        StartCoroutine(TransitionToRoom(roomName));
    }

    private IEnumerator TransitionToRoom(string roomName)
    {
        fadeCanvas.enabled = true;

        // Fade to black
        yield return StartCoroutine(Fade(1));

        // Deactivate all rooms
        livingRoom.SetActive(false);
        cantina.SetActive(false);
        cube.SetActive(false);
        mezzanine.SetActive(false);

        // Activate the pressed room
        switch (roomName)
        {
            case "LivingRoom":
                livingRoom.SetActive(true);
                break;
            case "Cantina":
                cantina.SetActive(true);
                break;
            case "Cube":
                cube.SetActive(true);
                break;
            case "Mezzanine":
                mezzanine.SetActive(true);
                break;
        }

        // Fade back in
        yield return StartCoroutine(Fade(0));

        // Disable the FadeCanvas after fading back in
        fadeCanvas.enabled = false;
    }

    private IEnumerator Fade(float targetAlpha)
    {
        float fadeSpeed = 1f;
        Color color = fadeImage.color;

        while (!Mathf.Approximately(color.a, targetAlpha))
        {
            color.a = Mathf.MoveTowards(color.a, targetAlpha, fadeSpeed * Time.deltaTime);
            fadeImage.color = color;
            yield return null;
        }
    }
}