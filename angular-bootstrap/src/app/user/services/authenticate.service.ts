import { Injectable, OnDestroy } from '@angular/core';
import { AngularFireAuth } from '@angular/fire/compat/auth';
import firebase from 'firebase/compat/app';
import { Subscription } from 'rxjs';
import { JwtInterceptorService } from './jwt-interceptor.service';

// see https://github.com/angular/angularfire/blob/master/docs/auth/getting-started.md
@Injectable({
  providedIn: 'root'
})
export class AuthenticateService implements OnDestroy {

  userEventsSubscription: Subscription;

  constructor(public auth: AngularFireAuth, private jwtInterceptorService: JwtInterceptorService) {
    this.userEventsSubscription = this.auth.user.subscribe(user => {
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
    // see https://firebase.google.com/docs/auth/web/github-auth
    this.auth.signInWithPopup(new firebase.auth.GithubAuthProvider());
  }

  logout() {
    this.jwtInterceptorService.removeJwtToken();
    this.auth.signOut();
  }

}
