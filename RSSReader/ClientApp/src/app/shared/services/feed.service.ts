import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { FeedInformation } from '../models/feed-information.model';
import { Observable } from 'rxjs/Observable';
import { AppSettings } from './app-config.service';


@Injectable()
export class FeedService {
  constructor(private http: HttpClient, private appConfig: AppSettings) {

  }

  public getFeedInformation(): Observable<Array<FeedInformation>> {
    const url = `${this.appConfig.settings.apiUrl}/feed/info`;
    return this.http.get<Array<FeedInformation>>(url);
  }
}
