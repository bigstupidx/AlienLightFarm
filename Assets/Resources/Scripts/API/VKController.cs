using UnityEngine;
using System.Collections;
using com.playGenesis.VkUnityPlugin;
using Facebook.MiniJSON;
using System.Collections.Generic;
using System;

public class VKController : MonoBehaviour
{

    public VkApi vkapi;

    string group_id = "122457260";

    Library library;


    // Use this for initialization
    void Start()
    {
        // vkapi = VkApi.VkApiInstance;    
        library = GameObject.FindObjectOfType<Library>();
    }

    public bool IsShowVkGroupButton()
    {
        if (!PreferencesSaver.IsEnterInVkGroup())
            return true;
        else
            return false;
    }

    public bool IsShowVkPostButton()
    {
        DateTime dateTime = PreferencesSaver.GetDatePostVk();

        if (DateTime.Now.Subtract(dateTime).TotalHours > 12)
            return true;
        else
            return false;
    }

    public void EnterInGroupVk()
    {
        if (vkapi.TokenValidFor() < 120)
            Login();

        JoinGroupRequest();
    }

    public void Login()
    {
        vkapi.Login();
    }

    public void InitializeGroupRequest()
    {
        library.screenController.endScreen.GetComponent<EndScreen>().HideEnterVkButton();

        vkapi.LoggedIn += EnterInGroupVk;
        Login();
    }



    public void JoinGroupRequest()
    {
        string request = "groups.join?group_id=" + group_id + "&access_token=" + VkApi.currentToken.access_token; ;

        vkapi.Call(request, JoinGroupHandler);
    }

    public void JoinGroupHandler(VkResponseRaw _raw, object[] _arguments)
    {
     //   if (_raw.ei != null && _raw.ei.error_code.Equals("17"))
    //    {
     //       Debug.Log("ОШибка при входе");
            PreferencesSaver.SetEnterInVkGroup();
            library.money.AddMoney(GameplayConstants.EnterVkGroupReward);

    //        library.screenController.endScreen.GetComponent<EndScreen>().HideEnterVkButton();
     //       return;
     //   }


   //     var dict = Json.Deserialize(_raw.text) as Dictionary<string, object>;
   //     long resp = (long)dict["response"];

        /*
                if (resp == 1)
                {       
                    PreferencesSaver.SetEnterInVkGroup();
                    library.money.AddMoney(GameplayConstants.EnterVkGroupReward);
                    Debug.Log("Зашло");

                }
                else
                {
                    Debug.Log("Не зашло");
                }*/
    //    PreferencesSaver.SetEnterInVkGroup();
       // library.money.AddMoney(GameplayConstants.EnterVkGroupReward);
    }

    /// <summary>
    /// ///
    /// </summary>
    public void WallPostInitialize()
    {
        /*
        if (Application.platform != RuntimePlatform.Android)
        {
            ExternalControl.VK_WallPostHandler(1);

            return;
        }*/

        /*
        if (!Logged)
        {*/
        library.screenController.endScreen.GetComponent<EndScreen>().HidePostVkButton();

        vkapi.LoggedIn += InviteFriendsWallPostRequest;
        Login();

       /* }
        else
        {
            InviteFriendsWallPostRequest();
        }*/
    }

    public void InviteFriendsWallPostRequest()
    {
        /*
        if (FirstAuthCheck())
        {
            PreAuthCheckFunc = new AfterAuthHandler(InviteFriendsWallPostRequest);

            return;
        }*/

        string request = "friends.get?user_id=" + VkApi.currentToken.user_id + "&order=random&count=20&fields=has_mobile,online,sex&name_case=nom&v=5.29&access_token=" + VkApi.currentToken.access_token;

        vkapi.Call(request, GetFriendsHandler);
    }

    public void GetFriendsHandler(VkResponseRaw _raw, object[] _arguments)
    {
        if (_raw.ei == null)
        {
            List<VKUser> FriendsData = new List<VKUser>();
            Dictionary<long, VKUser> Users = new Dictionary<long, VKUser>();


            Debug.Log("vk: " + _raw.text);

            try
            {

                var dict = Json.Deserialize(_raw.text) as Dictionary<string, object>;
                var response = (object)dict["response"] as Dictionary<string, object>;


                var items = (List<object>)response["items"];

                foreach (var item in items)
                {
                    FriendsData.Add(VKUser.Deserialize(item));
                }
                //   var friendsResponse = JsonReader.Deserialize < VkResponse < VKList < VKUser»> (_raw.text);

                //     if (friendsResponse.response.items.Count > 0)
                //       FriendsData.AddRange(friendsResponse.response.items);
            }
            catch
            {
           //     ExternalControl.VK_WallPostHandler(0);

                return;
            }

            int FriendsCount = FriendsData.Count;

            if (FriendsCount > 0)
            {
                if (FriendsCount > 3)
                {
                    int PassCount = 0;

                    while (Users.Count < 3)
                    {
                        for (var i = 0; i < FriendsData.Count; i++)
                        {
                            VKUser UserData = FriendsData[i];

                            bool HasMobile = (UserData.has_mobile == 1);
                            bool NowOnline = (UserData.online == 1);
                            bool Man = (UserData.sex == 2);

                            bool PassCondition = false;

                            switch (PassCount)
                            {
                                case 0:
                                    PassCondition = NowOnline && HasMobile && Man;
                                    break;
                                case 1:
                                    PassCondition = NowOnline && HasMobile;
                                    break;
                                case 2:
                                    PassCondition = HasMobile;
                                    break;
                                case 3:
                                    PassCondition = true;
                                    break;
                            }

                            if (PassCondition && !Users.ContainsKey(UserData.id))
                            {
                                if (Users.Count < 3)
                                {
                                    Users.Add(UserData.id, UserData);
                                }
                                else
                                {
                                    break;
                                }
                            }
                        }

                        PassCount++;
                    }

                    FriendsCount = 3;
                }
                else
                {
                    for (var i = 0; i < FriendsData.Count; i++)
                    {
                        VKUser UserData = FriendsData[i];

                        Users.Add(UserData.id, UserData);
                    }
                }
            }

            string _friends = "";

            if (Users.Count > 0)
            {
                foreach (KeyValuePair<long, VKUser> _item in Users)
                {
                    _friends += string.Format("[id{0}|{1}], ", _item.Key, _item.Value.first_name + " " + _item.Value.last_name);
                }

                _friends = _friends.Substring(0, _friends.Length - 2);
            }

            //need develop 
            WallPostRequest(_friends);
        }
        else
        {
            Debug.Log("vk error: " + _raw.ei.error_msg);
        }
    }

    public void WallPostRequest(string _friends_str)
    {
        string _friends_total = (_friends_str != "" ? _friends_str + " и остальные, присоединяйтесь!" : "Присоединяйтесь!");

        string post = "Спорим, вы не наберёте больше меня очков в \"Sparkland\"? " + _friends_total + "\n\nВот ссылка на Google Play:\n"+GameplayConstants.marketURL+"\n\n #sparkland #game #games #android";

        string _photo = "photo87336767_273579052";

        string request = "wall.post?owner_id=" + VkApi.currentToken.user_id + "&attachments=" + _photo + "," + GameplayConstants.marketURL +"&v=5.45&access_token="+ VkApi.currentToken.access_token + "&message=" + post;

        vkapi.Call(request, WallPostHandler);
    }


    public void WallPostHandler(VkResponseRaw _raw, object[] _arguments)
    {

        if (_raw.ei == null)
        {
            //Debug.Log(_raw.text);

            PreferencesSaver.SetDatePostVk(DateTime.Now);
            library.money.AddMoney(GameplayConstants.PostVkReward);
            /*
            if (ExternalControl.GameControl.ReferalControl.Active)
                ExternalControl.GameControl.ReferalControl.InviteFriendsHandler(1);
            else
                ExternalControl.VK_WallPostHandler(1);

            Debug.Log("success");*/
        }
        else
        {
            /*
            if
            (ExternalControl.GameControl.ReferalControl.Active)
                ExternalControl.GameControl.ReferalControl.InviteFriendsHandler(1);
            else
                ExternalControl.VK_WallPostHandler(0);
                */
           // Debug.Log("vk error: " + _raw.ei.error_msg);
        }

    }
}