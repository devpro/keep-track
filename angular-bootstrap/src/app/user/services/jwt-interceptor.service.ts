import { Injectable } from '@angular/core';
import { HttpRequest, HttpHandler, HttpEvent } from '@angular/common/http/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class JwtInterceptorService {

  private token: string | null;

  constructor() { }

  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    if (this.token) {
      const clone = req.clone({ setHeaders: { Authorization: `Bearer ${this.token}` } });
      return next.handle(clone);
    }
    return next.handle(req);
  }

  getJwtToken(): string {
    return this.token;
  }

  setJwtToken(token: string) {
    this.token = token;
  }

  removeJwtToken() {
    this.token = null;
  }

}
