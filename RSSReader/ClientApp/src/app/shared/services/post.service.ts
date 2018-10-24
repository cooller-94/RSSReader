import { Injectable } from "@angular/core";
import { Observable } from "rxjs/Observable";
import { Post } from "../models/post.model";
import { AppSettings } from "./app-config.service";
import { HttpClient } from '@angular/common/http';

@Injectable()
export class PostService {

  constructor(private http: HttpClient, private appConfig: AppSettings) { }

  public getUnreadPostsByFeed(feedId: number): Observable<Array<Post>> {
    const url = `${this.appConfig.settings.apiUrl}/posts/unread/${feedId}`;
    return this.http.get<Array<Post>>(url);
  }

  public getUnreadPostsByCategory(category: string): Observable<Array<Post>> {
    const url = `${this.appConfig.settings.apiUrl}/posts/unread/categories?category=${category}`;
    return this.http.get<Array<Post>>(url);
  }

  public getAllUnreadPosts(): Observable<Array<Post>> {
    const url = `${this.appConfig.settings.apiUrl}/posts/latest`;
    return this.http.get<Array<Post>>(url);
  }
}
