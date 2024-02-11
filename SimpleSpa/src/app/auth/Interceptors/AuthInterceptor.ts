import { Injectable, Injector } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor,
} from '@angular/common/http';
import { Observable, catchError, switchMap, throwError } from 'rxjs';
import { AuthService } from '../../Services/Auth/auth.service';

@Injectable()
export class AuthInterceptor implements HttpInterceptor {
  constructor(private inject: Injector, private authservice: AuthService) {}

  intercept(
    request: HttpRequest<unknown>,
    next: HttpHandler
  ): Observable<HttpEvent<unknown>> {
    if (!this.authservice.isLoggedIn()) {
      return next.handle(request);
    }
    let authreq = request;
    authreq = this.AddTokenheader(request, this.authservice.GetToken());

    return next.handle(authreq);
  }

  AddTokenheader(request: HttpRequest<any>, token: any) {
    return request.clone({
      headers: request.headers.set('Authorization', 'bearer ' + token),
    });
  }
}
