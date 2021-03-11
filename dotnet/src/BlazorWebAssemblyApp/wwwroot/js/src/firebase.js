export function saveUserInformation(displayName, email, token) {
  localStorage.display = displayName;
  localStorage.email = email;
  localStorage.token = token;
}

export function login(firebase, provider, instance) {
  // idea: set an authentication state observer and get user data (https://firebase.google.com/docs/auth/web/start)
  if (localStorage.token) {
    instance.invokeMethod('LoginCallback', localStorage.email, localStorage.display, localStorage.token);
  } else {
    firebase.auth().signInWithPopup(provider).then(function (result) {
      var user = result.user;
      saveUserInformation(user.displayName, user.email, user.za);
      instance.invokeMethod('LoginCallback', user.email, user.displayName, user.za);
    }).catch(function (error) {
      console.log(error);
      instance.invokeMethod('LoginErrorCallback', error.message);
    });
  }
}
