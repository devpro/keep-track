import { TestBed, waitForAsync } from '@angular/core/testing';
import { Observable } from 'rxjs';
import firebase from 'firebase/compat/app';
import { BookComponent } from './book.component';
import { AppModule } from 'src/app/app.module';
import { BookService } from 'src/app/backend/services/book.service';
import { AuthenticateService } from 'src/app/user/services/authenticate.service';

describe('BookComponent', () => {

  const fakeBookService = jasmine.createSpyObj('BookService', ['list']);
  const fakeAuthenticateService = jasmine.createSpyObj('AuthenticateService', ['auth']);

  let component: BookComponent;

  beforeEach(() => TestBed.configureTestingModule({
    imports: [AppModule],
    providers: [
      { provide: BookService, useValue: fakeBookService },
      { provide: AuthenticateService, useValue: fakeAuthenticateService }
    ]
  }));

  beforeEach(() => {
    fakeAuthenticateService.auth = {
      user: new Observable<firebase.User>()
    };
  });

  it('should listen to userEvents in ngOnInit', waitForAsync(() => {
    const fixture = TestBed.createComponent(BookComponent);
    component = fixture.componentInstance;
    component.ngOnInit();
  }));
});
