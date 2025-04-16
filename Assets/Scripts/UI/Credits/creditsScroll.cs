using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using TMPro;

public class creditsScroll : MonoBehaviour
{

    [SerializeField] private CanvasGroup fadeIn;
    [SerializeField] private Transform[] locations;

    [SerializeField] private Transform titletext, scrollingText;
    [SerializeField] private GameObject firsttextToAppear;
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1f;
        fadeIn.alpha = 1.0f;
        fadeIn.DOFade(0, 3f).OnComplete(() => {
            buildText();
            titletext.DOMove(locations[0].position, 2f).SetEase(Ease.Linear).OnComplete(() => {
                firsttextToAppear.SetActive(true);
                scrollingText.DOMove(locations[1].position, 95f).SetEase(Ease.Linear);
            });
        });
    }

    public void onClickGoToTitle()
    {
        fadeIn.DOFade(1, 3f).OnComplete(() => {
            SceneManager.LoadScene("MainMenu");
        });
    }

    public void buildText()
    {
        string builtString = "";
        int condemned = BossSaveData.GetNumberOfCondemned();
        int saved = BossSaveData.GetNumberOfKilled();

        // FIRST PART OF THE DIALOGUE

        if (condemned == 3)
        {
            builtString += "In a single day, the Order of Truth condemned the knightslayer, reclaimed the " +
                "Scepter of Truth, and brought swift justice for Grest. They called it a triumph, an " +
                "opportunity that would bring in a new age of peace and order. With the passing of slightly " +
                "stricter laws, the people of Isen fell further under the Order's ideals. Never had its " +
                "influence been so complete.\r\n";
        } else if (saved == 3)
        {
            builtString += "With Severin dead, the Scepter's disappearance caused fixable deaths, a quiet " +
                "rebellion gained ground, and its grip on Isen weakened. Jael led the charge, stepping in " +
                "where Lucan would not.\r\n";
        } else
        {
            builtString += "Leora's choices rippled across Isen. She stood at the center of it all—unknown to " +
                "most, unthanked by many. Her decisions had saved lives, ruined others, and set forces into " +
                "motion that could not be undone.\r\n";
        }


        if (BossSaveData.bossStates["Lucan"] == 1)
        {
            //Play Lucan killed text
            builtString += "\r\nThe people of Zaro rejoiced at Lucan's execution. To them, he was a traitor, a " +
                "criminal, the last remnant of a shameful rebellion. But the outcasts saw it differently. " +
                "Without Lucan, their structure collapsed. Fragmented and leaderless, they lost all the " +
                "structure Lucan had built. They split, their collective suffering due to conflicts and " +
                "distrust for one another. Without Lucan, their found family became strangers.\r\n";
        }
        else
        {
            //Play Lucan alive text
            builtString += "\r\nTension simmered in the streets. Lucan's survival sparked something—an idea, a " +
                "memory, a quiet defiance. Jael, saved during the Fall of Grest, took up Lucan's cause. She " +
                "rallied the outcasts, forming a quiet resistance. Though she wished for Lucan to lead them, he " +
                "refused. He had no more to give, choosing instead to live out his days in seclusion.\r\n";
        }

        if (BossSaveData.bossStates["Ivar"] == 1)
        {
            //Play Ivar killed text
            builtString += "\r\nThe Scepter of Truth was returned without fanfare. Ivar, condemned and forgotten, was " +
                "just another name in the Order's ledger. At Leora's request, they investigated his family—but " +
                "Camille and Eleanor were never found. The mansion rotted on the hillside, a monument to loss. No " +
                "one came to mourn them.\r\n";
        }
        else
        {
            //Play ivar alive text
            builtString += "\r\nIvar succeeded. He bartered the Scepter of Truth for his family's safety, then vanished " +
                "beyond the Order's reach. For a time, the Scepter remained missing. Lives were lost waiting for its " +
                "healing. Eventually, the Order reclaimed it—ripped from the hands of those who once held Ivar's family " +
                "hostage. The cost, however, had already been paid.\r\n";
        }

        if (BossSaveData.bossStates["Viin"] == 1)
        {
            //Play viin killed text
            builtString += "\r\nIsen exhaled in relief when Viin was captured. She was publicly hanged shortly after her " +
                "capture, Zaro cheering and roaring as she was “given what she deserved”. Vaang was found shortly after, " +
                "angry red scratches around his neck—cold and lifeless. With his death, the orphanage fell under the " +
                "Order's control. Caretakers rotated frequently, and the children were forced to mourn in silence. The " +
                "warmth Vaang brought was gone, and the Order just wasn't the same.\r\n";
        }
        else
        {
            //Play viin alive text
            builtString += "\r\nViin earned her title—the Butcher of Knights. With growing confidence, she assassinated " +
                "Severin, the head of the knights. With Severin gone, crimes spread like wildfire, and the Order's grip " +
                "weakened. More children were orphaned, but Vaang did what he could, taking them in with compassion. Still, " +
                "no one could replace Severin, and Viin continued her senseless killing as she pleased.\r\n";
        }

        if (condemned == 3)
        {
            builtString += "\r\nLeora was promoted to High Justiciar, one of the highest ranks for a knight of Verita. With her " +
                "new rank and the burden of stricter laws, she led countless arrests and never questioned a command. There " +
                "was no time left for prayer, no space left for doubt.\r\nShe was the perfect knight.\r\n";
        }
        else if (saved == 3)
        {
            builtString += "\r\nLeora remained in her position, but the weight of her choices never left her. Why had some been " +
                "spared and others condemned? She obeyed the Order and spoke in Verita's name, yet she hesitated when it mattered " +
                "most. She often has the same nightmare, battling her faceless self, confronting her choices and identity as a " +
                "knight.\r\nShe always woke unsure of who had won.\r\n";
        }
        else
        {
            builtString += "\r\nIsen was unstable, but Leora remained. She stayed not for the Order, but for the people. She helped " +
                "where she could, walked among them as one of their own. She defied orders she didn't believe in, refused arrests " +
                "without reason, and only intervened when violence broke out. With so few knights left, the Order let her defiance " +
                "slide. She frequently prayed, not because she had to, but because she chose to.\r\nAnd for the first time in a " +
                "long while, the people believed in a knight again.\r\n";
        }

        scrollingText.GetComponent<TextMeshProUGUI>().text = builtString;

    }
}
