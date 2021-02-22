import { TestBed } from '@angular/core/testing';
import { Observable } from 'rxjs';
import firebase from 'firebase/app';
import { AuthGuard } from './auth.guard';
import { AuthenticateService } from './user/services/authenticate.service';

describe('AuthGuard', () => {

  const fakeAuthenticateService = {
    user: new Observable<firebase.User>()
  } as AuthenticateService;

  let guard: AuthGuard;

  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [
        { provide: AuthenticateService, useValue: fakeAuthenticateService }
      ]
    });
    guard = TestBed.inject(AuthGuard);
  });

  it('should be created', () => {
    expect(guard).toBeTruthy();
  });

});
