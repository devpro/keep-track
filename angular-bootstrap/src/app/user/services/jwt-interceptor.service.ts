import { HttpEvent, HttpHandler, HttpRequest } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class JwtInterceptorService {

  private token: string | null = null;

  constructor() { }

  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    if (this.token) {
      const clone = req.clone({ setHeaders: { authorization: `Bearer ${this.token}` } });
      return next.handle(clone);
    }
    return next.handle(req);
  }

  getJwtToken(): string | null {
    return this.token;
  }

  setJwtToken(token: string) {
    this.token = token;
  }

  removeJwtToken() {
    this.token = null;
  }

}
