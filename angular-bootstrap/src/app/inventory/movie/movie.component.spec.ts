import { TestBed, waitForAsync } from '@angular/core/testing';
import { Observable } from 'rxjs';
import firebase from 'firebase/compat/app';
import { AppModule } from 'src/app/app.module';
import { AuthenticateService } from 'src/app/user/services/authenticate.service';
import { MovieComponent } from './movie.component';
import { MovieService } from 'src/app/backend/services/movie.service';

describe('MovieComponent', () => {

  const fakeMovieService = jasmine.createSpyObj('MovieService', ['list']);
  const fakeAuthenticateService = jasmine.createSpyObj('AuthenticateService', ['auth']);

  let component: MovieComponent;

  beforeEach(() => TestBed.configureTestingModule({
    imports: [AppModule],
    providers: [
      { provide: MovieService, useValue: fakeMovieService },
      { provide: AuthenticateService, useValue: fakeAuthenticateService }
    ]
  }));

  beforeEach(() => {
    fakeAuthenticateService.auth = {
      user: new Observable<firebase.User>()
    };
  });

  it('should listen to userEvents in ngOnInit', waitForAsync(() => {
    const fixture = TestBed.createComponent(MovieComponent);
    component = fixture.componentInstance;
    component.ngOnInit();
  }));
});
