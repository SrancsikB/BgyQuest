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

    string UserNameText = "User";
    string PasswordText = "User1234!";

    //async void Awake()
    //{
    //    await UnityServices.InitializeAsync();
    //}

    async void Start()
    {
        InputPassWord.contentType = TMP_InputField.ContentType.Password;
        await UnityServices.InitializeAsync();
        if (AuthenticationService.Instance.IsSignedIn)
            ShowSuccessMessage("Already signed in. " + AuthenticationService.Instance.PlayerId);
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

    async Task SignUpWithUsernamePasswordAsync(string username, string password)
    {
        try
        {
            await AuthenticationService.Instance.SignUpWithUsernamePasswordAsync(username, password);
            //Debug.Log("SignUp is successful. " + AuthenticationService.Instance.PlayerId);
            ShowSuccessMessage("SignUp is successful. " + AuthenticationService.Instance.PlayerId);
            SceneManager.LoadScene("Level01_1");
        }
        catch (AuthenticationException ex)
        {
            // Compare error code to AuthenticationErrorCodes
            // Notify the player with the proper error message
            //Debug.LogException(ex);
            //Debug.Log(ex.ErrorCode + " " + ex.Message);
            ShowErrorMessage(ex.ErrorCode + ": " + ex.Message);
        }
        catch (RequestFailedException ex)
        {
            // Compare error code to CommonErrorCodes
            // Notify the player with the proper error message
            //Debug.LogException(ex);
            //Debug.Log(ex.ErrorCode + " " + ex.Message);
            ShowErrorMessage(ex.ErrorCode + ": " + ex.Message);
        }
        finally
        {

        }
    }


    async Task SignInWithUsernamePasswordAsync(string username, string password)
    {
        try
        {
            await AuthenticationService.Instance.SignInWithUsernamePasswordAsync(username, password);
            //Debug.Log("SignIn is successful." + AuthenticationService.Instance.PlayerId);
            ShowSuccessMessage("SignIn is successful. " + AuthenticationService.Instance.PlayerId);
            SceneManager.LoadScene("Level01_1");
        }
        catch (AuthenticationException ex)
        {
            // Compare error code to AuthenticationErrorCodes
            // Notify the player with the proper error message
            //Debug.LogException(ex);
            ShowErrorMessage(ex.ErrorCode + ": " + ex.Message);

        }
        catch (RequestFailedException ex)
        {
            // Compare error code to CommonErrorCodes
            // Notify the player with the proper error message
            //Debug.LogException(ex);
            ShowErrorMessage(ex.ErrorCode + ": " + ex.Message);
        }
        finally
        {

        }
    }


    async Task SignInGuestAsync()
    {
        try
        {
            await AuthenticationService.Instance.SignInAnonymouslyAsync();
            //Debug.Log("SignIn is successful." + AuthenticationService.Instance.PlayerId);
            ShowSuccessMessage("SignIn as guest successful. " + AuthenticationService.Instance.PlayerId);
            SceneManager.LoadScene("Level01_1");
        }
        catch (AuthenticationException ex)
        {
            // Compare error code to AuthenticationErrorCodes
            // Notify the player with the proper error message
            //Debug.LogException(ex);
            ShowErrorMessage(ex.ErrorCode + ": " + ex.Message);

        }
        catch (RequestFailedException ex)
        {
            // Compare error code to CommonErrorCodes
            // Notify the player with the proper error message
            //Debug.LogException(ex);
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

    // Update is called once per frame
    //void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.N))
    //        CreateUser();
    //    if (Input.GetKeyDown(KeyCode.S))
    //        SignInUser();
    //    if (Input.GetKeyDown(KeyCode.E))
    //    {



    //    }

    //}
}
