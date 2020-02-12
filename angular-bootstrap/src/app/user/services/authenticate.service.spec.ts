import { TestBed } from '@angular/core/testing';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { AngularFireModule } from '@angular/fire';
import { AngularFireAuth, AngularFireAuthModule } from '@angular/fire/auth';
import { AuthenticateService } from './authenticate.service';
import { JwtInterceptorService } from './jwt-interceptor.service';
import { environment } from 'src/environments/environment.dev';

describe('AuthenticateService', () => {
  let authenticateService: AuthenticateService;
  let http: HttpTestingController;
  let firebaseAuth: AngularFireAuth;
  let jwtInterceptorService: JwtInterceptorService;

  beforeEach(() => TestBed.configureTestingModule({
    imports: [
      HttpClientTestingModule,
      AngularFireModule.initializeApp(environment.firebase),
      AngularFireAuthModule
    ]
  }));

  beforeEach(() => {
    http = TestBed.get(HttpTestingController);
    firebaseAuth = TestBed.get(AngularFireAuth);
    jwtInterceptorService = TestBed.get(JwtInterceptorService);
    authenticateService = TestBed.get(AuthenticateService);
  });

  afterAll(() => http.verify());

  it('should logout', () => {
    authenticateService.logout();
  });
});
