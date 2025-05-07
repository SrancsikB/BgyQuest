using System.Threading.Tasks;
using TMPro;
using Unity.Services.Authentication;
using Unity.Services.Core;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Authentication : MonoBehaviour
{
    [SerializeField] TMP_InputField InputUserName;
    [SerializeField] TMP_InputField InputPassWord;
    [SerializeField] TextMeshProUGUI ErrorMessage;
    PlayerDataControl pdc;

    string UserNameText = "User";
    string PasswordText = "User1234!";

   

    async void Start()
    {
        pdc = PlayerDataControl.Instance;
        InputPassWord.contentType = TMP_InputField.ContentType.Password;
        await UnityServices.InitializeAsync();
        AuthenticationService.Instance.SignOut(true);//Temp, avoid auto login
        if (AuthenticationService.Instance.SessionTokenExists)
        {
            try
            {
                await AuthenticationService.Instance.SignInAnonymouslyAsync();
            }
            catch (System.Exception)
            {

            }

        }
        if (AuthenticationService.Instance.IsSignedIn)
        {
            ShowSuccessMessage("Already signed in. " + AuthenticationService.Instance.PlayerId + "/n" + AuthenticationService.Instance.PlayerInfo.Username);
            pdc.playerName = AuthenticationService.Instance.PlayerInfo.Username;
            pdc.LoadData();
            SceneManager.LoadScene("Map");
        }

    }

    public async void CreateUser()
    {
        UserNameText = InputUserName.text;
        PasswordText = InputPassWord.text;
        await SignUpWithUsernamePasswordAsync(UserNameText, PasswordText);
    }

    public async void SignInUser()
    {
        UserNameText = InputUserName.text;
        PasswordText = InputPassWord.text;
        await SignInWithUsernamePasswordAsync(UserNameText, PasswordText);

    }

    public async void SignInGuest()
    {
        await SignInGuestAsync();

    }

    async Task SignUpWithUsernamePasswordAsync(string username, string password) //New user creation
    {
        try
        {
            await AuthenticationService.Instance.SignUpWithUsernamePasswordAsync(username, password);
            ShowSuccessMessage("SignUp is successful. " + AuthenticationService.Instance.PlayerId + "/n" + AuthenticationService.Instance.PlayerInfo.Username);
            pdc.playerName = AuthenticationService.Instance.PlayerInfo.Username;
            pdc.InitData();
            SceneManager.LoadScene("Map");
        }
        catch (AuthenticationException ex)
        {
            ShowErrorMessage(ex.ErrorCode + ": " + ex.Message);
        }
        catch (RequestFailedException ex)
        {
            ShowErrorMessage(ex.ErrorCode + ": " + ex.Message);
        }
        finally
        {

        }
    }


    async Task SignInWithUsernamePasswordAsync(string username, string password) //User with authentication already
    {
        try
        {
            await AuthenticationService.Instance.SignInWithUsernamePasswordAsync(username, password);
            ShowSuccessMessage("SignIn is successful. " + AuthenticationService.Instance.PlayerId);
            pdc.playerName = AuthenticationService.Instance.PlayerInfo.Username;
            pdc.LoadData();
            SceneManager.LoadScene("Map");
        }
        catch (AuthenticationException ex)
        {
            ShowErrorMessage(ex.ErrorCode + ": " + ex.Message);
        }
        catch (RequestFailedException ex)
        {
            ShowErrorMessage(ex.ErrorCode + ": " + ex.Message);
        }
        finally
        {

        }
    }


    async Task SignInGuestAsync() //Guest login, handle as new player
    {
        try
        {
            await AuthenticationService.Instance.SignInAnonymouslyAsync();
            ShowSuccessMessage("SignIn as guest successful. " + AuthenticationService.Instance.PlayerId);
            pdc.playerName = "Guest";
            pdc.InitData();
            SceneManager.LoadScene("Map");
        }
        catch (AuthenticationException ex)
        {
            ShowErrorMessage(ex.ErrorCode + ": " + ex.Message);

        }
        catch (RequestFailedException ex)
        {
            ShowErrorMessage(ex.ErrorCode + ": " + ex.Message);
        }
        finally
        {

        }
    }




    public void ShowErrorMessage(string message)
    {
        ErrorMessage.color = Color.red;
        ErrorMessage.text = message;
    }

    public void ShowSuccessMessage(string message)
    {
        ErrorMessage.color = Color.blue;
        ErrorMessage.text = message;
    }


}
