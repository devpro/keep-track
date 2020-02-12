import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { RouterTestingModule } from '@angular/router/testing';
import { HeaderComponent } from './header.component';
import { AppModule } from 'src/app/app.module';

describe('HeaderComponent', () => {
  let component: HeaderComponent;
  let fixture: ComponentFixture<HeaderComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      imports: [AppModule, RouterTestingModule]
    });
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(HeaderComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('display a link to go the login', () => {
    const fixture = TestBed.createComponent(HeaderComponent);
    const element = fixture.nativeElement;
    fixture.detectChanges();

    fixture.componentInstance.user = null;
    fixture.detectChanges();

    const button = element.querySelector('a[href="/login"]');
    expect(button).not.toBeNull('You should have an `a` element to display the link to the login. Maybe you forgot to use `routerLink`?');
    expect(button.textContent).toContain('Login', 'The link should have a text');
  });
});
