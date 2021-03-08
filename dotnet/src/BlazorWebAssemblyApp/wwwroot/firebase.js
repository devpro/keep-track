function saveUserInformation(displayName, email, token) {
  localStorage.display = displayName;
  localStorage.email = email;
  localStorage.token = token;
}

function login(provider, instance) {
  // TODO: Set an authentication state observer and get user data (https://firebase.google.com/docs/auth/web/start)

  if (localStorage.token) {
    console.log('Firefase token found');
    instance.invokeMethod('LoginCallback', localStorage.email, localStorage.display, localStorage.token);
  } else {
    console.log('Call Firefase auth signInWithPopup');
    firebase.auth().signInWithPopup(provider).then(function (result) {
      console.log('Result firefase auth signInWithPopup');
      var user = result.user;
      saveUserInformation(user.displayName, user.email, user.za);
      instance.invokeMethod('LoginCallback', user.email, user.displayName, user.za);
    }).catch(function (error) {
      console.log('Error Firefase auth signInWithPopup');
      console.log(error);
      instance.invokeMethod('LoginErrorCallback', error.message);
    });
  }
}

window.FirebaseLoginGoogle = (instance) => {
  console.log('FirebaseLoginGoogle start');
  var provider = new firebase.auth.GoogleAuthProvider();
  login(provider, instance);
};

window.FirebaseLoginGitHub = (instance) => {
  console.log('FirebaseLoginGitHub start');
  var provider = new firebase.auth.GithubAuthProvider();
  login(provider, instance);
};

window.FirebaseLogout = (instance) => {
  firebase.auth().signOut().then(() => {
    saveUserInformation('', '', '');
    instance.invokeMethod('LogoutCallback');
  });
};
