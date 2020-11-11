using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;

public class MailyPlayer : MonoBehaviour
{

    private Vector2 screenBounds;
    SpamGamePlay spamGamePlay;
    private Vector3 homePosition = new Vector3(0, -3, 0);

    // Start is called before the first frame update
    void Start()
    {
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        spamGamePlay = Camera.main.GetComponent<SpamGamePlay>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (spamGamePlay.IsGameRunning)
        {
            float horizontalInput = Input.GetAxis("Horizontal");
            transform.Translate(new Vector3(horizontalInput, 0, 0) * 3 * Time.deltaTime);

            //clamp player to screen
            Vector3 viewPos = transform.position;
            viewPos.x = Mathf.Clamp(viewPos.x, screenBounds.x * -1, screenBounds.x);

            transform.position = viewPos;
        }
    }

    public void ResetToHomePosition()
    {
        transform.position = homePosition;
    }

}
