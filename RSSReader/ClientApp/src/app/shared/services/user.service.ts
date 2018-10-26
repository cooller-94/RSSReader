import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { User } from "../models/user.model";
import { ReplaySubject, Observable } from "rxjs";
import { AppSettings } from "./app-config.service";
import { map } from "rxjs/operators";
import { TOKEN_LOCAL_STORAGE_KEY } from "../../auth/constants/auth.constant";
import { Router } from "@angular/router";
import { AuthService } from "../../auth/services/auth.service";

@Injectable()
export class UserService {

  private userSubject = new ReplaySubject<User>(1);
  private userLoaded = false;

  constructor(private http: HttpClient, private appSettings: AppSettings) { }

  public getUser(): Observable<User> {
    if (!this.userLoaded) {
      this.loadCurrentUser();
    }

    return this.userSubject.asObservable();
  }

  public register(email: string, password: string, fullName: string): Observable<any> {
    const url = `${this.appSettings.settings.apiUrl}/account/register`;
    const data = { email: email, password: password, fullName: fullName };

    return this.http.post<any>(url, data);
  }

  private loadCurrentUser(): void {
    const url = `${this.appSettings.settings.apiUrl}/account/loggedIn`;

    this.http.get<User>(url).subscribe(
      user => {
        this.userLoaded = true;
        this.userSubject.next(user);
        this.userSubject.complete();
      },
      error => {
        this.userLoaded = false;
        this.userSubject.error(error);
      });
  }
}
