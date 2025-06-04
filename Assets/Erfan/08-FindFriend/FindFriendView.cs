using Joyixir.GameManager.UI;
using UnityEngine;
using UnityEngine.UI;

public class FindFriendView : View
{
    public Image image;
    public FindFriendTextElement textElementPrefab;
    public Transform textParent;
    public void Initialize(FindFriendConfig.ZoneDifficultyConfig zoneConfig)
    {
        textElementPrefab.gameObject.SetActive(false);
        image.sprite = zoneConfig.sampleFriendPic;
        foreach (var mFriend in zoneConfig.Friends)
        {
            var friend = Instantiate(textElementPrefab, textParent);
            friend.gameObject.SetActive(true);
            friend.SetText(mFriend.mName);
            friend.friendType = mFriend.FriendType;
            friend.onClick += OnClickFriend;
        }
    }
    
    private void OnClickFriend(FindFriendConfig.FriendType friendType)
    {
        FindFriendGameHandler.Instance.OnClickFriend(friendType);
    }
    protected override void OnBackBtn()
    {
        
    }
}
