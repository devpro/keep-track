import { TestBed, waitForAsync } from '@angular/core/testing';
import { Observable } from 'rxjs';
import firebase from 'firebase/app';
import { AppModule } from 'src/app/app.module';
import { AuthenticateService } from 'src/app/user/services/authenticate.service';
import { MovieComponent } from './movie.component';
import { MovieService } from 'src/app/backend/services/movie.service';

describe('MovieComponent', () => {

  const fakeMovieService = jasmine.createSpyObj('MovieService', ['list']);
  const fakeAuthenticateService = {
    user: new Observable<firebase.User>()
  } as AuthenticateService;

  let component: MovieComponent;

  beforeEach(() => TestBed.configureTestingModule({
    imports: [AppModule],
    providers: [
      { provide: MovieService, useValue: fakeMovieService },
      { provide: AuthenticateService, useValue: fakeAuthenticateService }
    ]
  }));

  it('should listen to userEvents in ngOnInit', waitForAsync(() => {
    const fixture = TestBed.createComponent(MovieComponent);
    component = fixture.componentInstance;
    component.ngOnInit();
  }));
});
