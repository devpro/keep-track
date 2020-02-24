import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { BookComponent } from './book.component';
import { AppModule } from 'src/app/app.module';
import { BookService } from 'src/app/backend/services/book.service';
import { AuthenticateService } from 'src/app/user/services/authenticate.service';
import { Observable } from 'rxjs';

describe('BookComponent', () => {

  const fakeBookService = jasmine.createSpyObj('BookService', ['list']);
  const fakeAuthenticateService = {
    user: new Observable<firebase.User>()
  } as AuthenticateService;

  let component: BookComponent;

  beforeEach(() => TestBed.configureTestingModule({
    imports: [AppModule],
    providers: [
      { provide: BookService, useValue: fakeBookService },
      { provide: AuthenticateService, useValue: fakeAuthenticateService }
    ]
  }));

  it('should listen to userEvents in ngOnInit', async(() => {
    const fixture = TestBed.createComponent(BookComponent);
    component = fixture.componentInstance;
    component.ngOnInit();
  }));
});
