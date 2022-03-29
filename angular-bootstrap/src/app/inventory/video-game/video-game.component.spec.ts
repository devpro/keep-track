import { TestBed, waitForAsync } from '@angular/core/testing';
import { Observable } from 'rxjs';
import firebase from 'firebase/compat/app';
import { AppModule } from 'src/app/app.module';
import { AuthenticateService } from 'src/app/user/services/authenticate.service';
import { VideoGameComponent } from './video-game.component';
import { VideoGameService } from 'src/app/backend/services/video-game.service';

describe('VideoGameComponent', () => {

  const fakeVideoGameService = jasmine.createSpyObj('VideoGameService', ['list']);
  const fakeAuthenticateService = jasmine.createSpyObj('AuthenticateService', ['auth']);

  let component: VideoGameComponent;

  beforeEach(() => TestBed.configureTestingModule({
    imports: [AppModule],
    providers: [
      { provide: VideoGameService, useValue: fakeVideoGameService },
      { provide: AuthenticateService, useValue: fakeAuthenticateService }
    ]
  }));

  beforeEach(() => {
    fakeAuthenticateService.auth = {
      user: new Observable<firebase.User>()
    };
  });

  it('should listen to userEvents in ngOnInit', waitForAsync(() => {
    const fixture = TestBed.createComponent(VideoGameComponent);
    component = fixture.componentInstance;
    component.ngOnInit();
  }));
});
