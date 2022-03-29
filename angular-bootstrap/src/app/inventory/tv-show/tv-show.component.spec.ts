import { TestBed, waitForAsync } from '@angular/core/testing';
import { Observable } from 'rxjs';
import firebase from 'firebase/compat/app';
import { TvShowComponent } from './tv-show.component';
import { AppModule } from 'src/app/app.module';
import { AuthenticateService } from 'src/app/user/services/authenticate.service';
import { TvShowService } from 'src/app/backend/services/tv-show.service';

describe('TvShowComponent', () => {

  const fakeTvShowService = jasmine.createSpyObj('TvShowService', ['list']);
  const fakeAuthenticateService = jasmine.createSpyObj('AuthenticateService', ['auth']);

  let component: TvShowComponent;

  beforeEach(() => TestBed.configureTestingModule({
    imports: [AppModule],
    providers: [
      { provide: TvShowService, useValue: fakeTvShowService },
      { provide: AuthenticateService, useValue: fakeAuthenticateService }
    ]
  }));

  beforeEach(() => {
    fakeAuthenticateService.auth = {
      user: new Observable<firebase.User>()
    };
  });

  it('should listen to userEvents in ngOnInit', waitForAsync(() => {
    const fixture = TestBed.createComponent(TvShowComponent);
    component = fixture.componentInstance;
    component.ngOnInit();
  }));
});
