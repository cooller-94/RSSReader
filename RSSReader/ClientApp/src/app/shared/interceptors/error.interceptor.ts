import { Injectable } from '@angular/core';
import { HttpRequest, HttpHandler, HttpEvent, HttpInterceptor } from '@angular/common/http';
import { Observable } from 'rxjs/Observable';
import { catchError } from 'rxjs/operators';
import { AuthService } from "../../auth/services/auth.service";
import { UNAUTHORISED_STATUS_CODE } from '../../auth/constants/auth.constant';
import 'rxjs/add/observable/throw';

@Injectable()
export class ErrorInterceptor implements HttpInterceptor {
  constructor(private authService: AuthService) { }

  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    return next.handle(request).pipe(catchError(err => {
      if (err.status === UNAUTHORISED_STATUS_CODE) {
        this.authService.logout();
        location.reload(true);
      }

      return Observable.throw(err);
    }))
  }
}
