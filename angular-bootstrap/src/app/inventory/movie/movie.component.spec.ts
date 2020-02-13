import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { MovieComponent } from './movie.component';
import { AppModule } from 'src/app/app.module';
import { MovieService } from 'src/app/backend/services/movie.service';
import { AuthenticateService } from 'src/app/user/services/authenticate.service';
import { Observable } from 'rxjs';

describe('MovieComponent', () => {

  const fakeMovieService = jasmine.createSpyObj('MovieService', ['list']);
  const fakeAuthenticateService = {
    user: new Observable<firebase.User>()
  } as AuthenticateService;

  let component: MovieComponent;
  let fixture: ComponentFixture<MovieComponent>;

  beforeEach(() => TestBed.configureTestingModule({
    imports: [AppModule],
    providers: [
      { provide: MovieService, useValue: fakeMovieService },
      { provide: AuthenticateService, useValue: fakeAuthenticateService }
    ]
  }));

  it('should listen to userEvents in ngOnInit', async(() => {
    const fixture = TestBed.createComponent(MovieComponent);
    component = fixture.componentInstance;
    component.ngOnInit();
  }));
});
