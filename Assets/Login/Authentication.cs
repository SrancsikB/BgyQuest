using System.Threading.Tasks;
using TMPro;
using Unity.Services.Authentication;
using Unity.Services.Core;
using UnityEngine;
using UnityEngine.Localization.Settings;
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
            ShowErrorMessage(ex.ErrorCode, ex.Message);
        }
        catch (RequestFailedException ex)
        {
            ShowErrorMessage(ex.ErrorCode, ex.Message);
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
            ShowErrorMessage(ex.ErrorCode, ex.Message);
        }
        catch (RequestFailedException ex)
        {
            ShowErrorMessage(ex.ErrorCode, ex.Message);
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
            ShowErrorMessage(ex.ErrorCode, ex.Message);

        }
        catch (RequestFailedException ex)
        {
            ShowErrorMessage(ex.ErrorCode, ex.Message);
        }
        finally
        {

        }
    }




    public void ShowErrorMessage(int errorCode, string message)
    {
        ErrorMessage.color = Color.red;
        ErrorMessage.text = errorCode.ToString();
        if (LocalizationSettings.SelectedLocale.Identifier == "hu")
        {
            if (errorCode == 10002)
                ErrorMessage.text += ": Felhasználónév és/vagy Jelszó formátuma nem megfelelõ";
            else if (errorCode == 10003)
                ErrorMessage.text += ": Felhasználónév már létezik";
            else if (message.Contains("Password does not match requirements"))
                ErrorMessage.text += ": Jelszó nem megfelelõ. Legalább tartalmaznia kell egy kisbetût, egy nagybetût, egy számot és egy szimbólumot. Minimum 8, maximum 30 karakter hosszú lehet.";//Password does not match requirements. Insert at least 1 uppercase, 1 lowercase, 1 digit and 1 symbol. With minimum 8 characters and a maximum of 30
            else if (message.Contains("Username does not match requirements"))
                ErrorMessage.text += ": Felhasználónév nem megfelelõ. Csak betût, számot és az alábbi szimbólumokat tartalmazhatja: {., -, _, @}. Minimum 3, maximum 20 karakter hosszú lehet.";//Username does not match requirements. Insert only letters, digits and symbols among {., -, _, @}. With a minimum of 3 characters and a maximum of 20
            else if (message.Contains("Invalid username or password"))
                ErrorMessage.text += ": Felhasználónév vagy jelszó nem megfelelõ";
        }
        else
        {
            //Keep english as it
            ErrorMessage.text += ": " + message;
        }



    }

    public void ShowSuccessMessage(string message)
    {
        ErrorMessage.color = Color.blue;
        ErrorMessage.text = message;
    }


}
