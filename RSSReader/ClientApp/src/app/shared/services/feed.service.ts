import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { FeedInformation } from '../models/feed-information.model';
import { Observable } from 'rxjs/Observable';
import { AppSettings } from './app-config.service';
import { SearchFeedResultModel } from '../models/search-feed-result.model';


@Injectable()
export class FeedService {
  constructor(private http: HttpClient, private appConfig: AppSettings) {

  }

  public getFeedInformation(): Observable<FeedInformation[]> {
    const url = `${this.appConfig.settings.apiUrl}/feed/info`;
    return this.http.get<FeedInformation[]>(url);
  }

  public findFeeds(term: string): Observable<SearchFeedResultModel[]> {
    const url = `${this.appConfig.settings.apiUrl}/feed/search?term=${term}`;
    return this.http.get<SearchFeedResultModel[]>(url);
  }
}
