import { TestBed, waitForAsync } from '@angular/core/testing';
import { Observable } from 'rxjs';

import { AppModule } from 'src/app/app.module';
import { AuthenticateService } from 'src/app/user/services/authenticate.service';
import { MovieComponent } from './movie.component';
import { MovieService } from 'src/app/backend/services/movie.service';
import { User } from '@angular/fire/auth';

describe('MovieComponent', () => {

  const fakeMovieService = jasmine.createSpyObj('MovieService', ['list']);
  const fakeAuthenticateService = jasmine.createSpyObj('AuthenticateService', ['authState$']);

  let component: MovieComponent;

  beforeEach(() => TestBed.configureTestingModule({
    imports: [AppModule],
    providers: [
      { provide: MovieService, useValue: fakeMovieService },
      { provide: AuthenticateService, useValue: fakeAuthenticateService }
    ]
  }));

  beforeEach(() => {
    fakeAuthenticateService.authState$ = {
      user: new Observable<User>()
    };
  });

  it('should listen to userEvents in ngOnInit', waitForAsync(() => {
    const fixture = TestBed.createComponent(MovieComponent);
    component = fixture.componentInstance;
    component.ngOnInit();
  }));
});
