import firebase from 'firebase/app';
import "firebase/auth";
import { firebaseConfig } from './firebase.config';
import { saveUserInformation, login } from './firebase';

firebase.initializeApp(firebaseConfig);

window.FirebaseLoginGoogle = (instance) => {
  var provider = new firebase.auth.GoogleAuthProvider();
  login(firebase, provider, instance);
};

window.FirebaseLoginGitHub = (instance) => {
  var provider = new firebase.auth.GithubAuthProvider();
  login(firebase, provider, instance);
};

window.FirebaseLogout = (instance) => {
  if (!localStorage.token) {
    instance.invokeMethod('LoginCallback', '', '', '');
  } else {
    try {
      firebase.auth().signOut()
        .then(() => {
          saveUserInformation('', '', '');
          instance.invokeMethod('LogoutCallback');
        })
        .catch((error) => {
          console.log(error);
          saveUserInformation('', '', '');
          instance.invokeMethod('LogoutCallback');
        });
    }
    catch (err) {
      console.log(err);
      saveUserInformation('', '', '');
      instance.invokeMethod('LogoutCallback');
    }
  }
};
