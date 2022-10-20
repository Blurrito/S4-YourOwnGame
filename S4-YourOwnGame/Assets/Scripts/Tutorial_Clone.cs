using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial_Clone : MonoBehaviour
{
    private IEnumerable StartBasicsTutorial()
    {
        //[Subtitle/voiceline: Go ahead and create a clone]


        //[Show popup: press C to create a clone]


        //+ ability: press C


        //{Wait until player creates clone}
        yield return new WaitUntil(() => true);

        //[Spawn the clone and give control, but don't start/show the timer]


        //[Subtitle/voiceline: Move to the button with the clone]


        //{Wait until player (still in clone character) touches button, door opens}


        //[Subtitle/voiceline: Dispose of the clone]


        //[Show popup: press X to dispose of the clone manually]


        //+ ability: press X


        //[Player enters protagonist control again, clone does not start replaying movement yet]


        //[Subtitle/voiceline: The clone will now repeats its movements, go through the door once the clone opens it.]


        //[Start the clones movement replay (after voiceline is finished!)]


    }
}
