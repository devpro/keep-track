import { Component, OnInit, OnDestroy } from '@angular/core';
import { Router } from '@angular/router';
import { Subscription } from 'rxjs';
import firebase from 'firebase/compat/app';
import { AuthenticateService } from 'src/app/user/services/authenticate.service';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent implements OnInit, OnDestroy {
  isExpanded = false;

  user = null as firebase.User | null;
  userEventsSubscription: Subscription | undefined;

  constructor(private authenticateService: AuthenticateService, private router: Router) { }

  ngOnInit() {
    this.userEventsSubscription = this.authenticateService.auth.user.subscribe({
      next: (user: firebase.User | null) => this.user = user
    });
  }

  ngOnDestroy() {
    if (this.userEventsSubscription) {
      this.userEventsSubscription.unsubscribe();
    }
  }

  logout(event: Event) {
    event.preventDefault();
    this.authenticateService.logout();
    this.router.navigate(['/login']);
  }

  collapse() {
    this.isExpanded = false;
  }

  toggle() {
    this.isExpanded = !this.isExpanded;
  }

}
