import { Injectable, OnDestroy } from '@angular/core';
import { AngularFireAuth } from '@angular/fire/auth';
import { Subscription } from 'rxjs';
import { Observable } from 'rxjs/internal/Observable';
import firebase from 'firebase/app';
import 'firebase/auth';
import { JwtInterceptorService } from './jwt-interceptor.service';

@Injectable({
  providedIn: 'root'
})
export class AuthenticateService implements OnDestroy {
  public user: Observable<firebase.User | null>;
  userEventsSubscription: Subscription;

  constructor(private firebaseAuth: AngularFireAuth, private jwtInterceptorService: JwtInterceptorService) {
    this.user = firebaseAuth.authState;
    this.userEventsSubscription = this.user.subscribe(user => {
      if (user) {
        user.getIdToken().then(token => {
          jwtInterceptorService.setJwtToken(token);
        });
      }
    });
  }

  ngOnDestroy() {
    if (this.userEventsSubscription) {
      this.userEventsSubscription.unsubscribe();
    }
  }

  signInWithGitHub() {
    // See https://firebase.google.com/docs/auth/web/github-auth
    const provider = new firebase.auth.GithubAuthProvider();
    firebase.auth()
      .signInWithPopup(provider)
        .then(result => {})
        .catch(error => {
          console.warn(error);
        });
  }

  logout() {
    this.jwtInterceptorService.removeJwtToken();
    firebase.auth()
      .signOut()
        .then(result => {})
        .catch(error => {
          console.warn(error);
        });
  }

}
