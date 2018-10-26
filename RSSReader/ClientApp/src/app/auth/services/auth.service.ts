import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { AppSettings } from '../../shared/services/app-config.service';
import { map } from 'rxjs/operators';
import { Observable } from 'rxjs/Observable';
import { TOKEN_LOCAL_STORAGE_KEY } from '../constants/auth.constant';

@Injectable()
export class AuthService {

  constructor(private http: HttpClient, private appSettings: AppSettings) { }

  public login(email: string, password: string): Observable<any> {
    const url = `${this.appSettings.settings.apiUrl}/auth/login`;
    const data = { email: email, password: password };
    return this.http.post<any>(url, data).pipe(map(data => {
      if (data && data.auth_token) {
        localStorage.setItem(TOKEN_LOCAL_STORAGE_KEY, data.auth_token);
      }

      return data;
    }));
  }

  public logout(): void {
    localStorage.removeItem(TOKEN_LOCAL_STORAGE_KEY);
  }

  public isLoggedIn(): boolean {
    return !!localStorage.getItem(TOKEN_LOCAL_STORAGE_KEY);
  }
}
