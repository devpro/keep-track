import { Component, OnInit } from '@angular/core';
import { AuthenticateService } from '../services/authenticate.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {

  constructor(private authenticateService: AuthenticateService) { }

  signInWithGitHub() {
    this.authenticateService.signInWithGitHub();
  }

}
