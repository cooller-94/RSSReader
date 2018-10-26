import { Injectable } from "@angular/core";
import { AppSettings } from "./app-config.service";
import { Observable } from "rxjs/Observable";
import { SyncFeedResult } from "../models/sync-feed-result.model";
import { HttpClient } from '@angular/common/http';

@Injectable()
export class SyncFeedService {

  constructor(private http: HttpClient, private appConfig: AppSettings) {}

  public syncAllFeeds(): Observable<SyncFeedResult[]> {
    const url = `${this.appConfig.settings.apiUrl}/feed/sync`;
    return this.http.get<SyncFeedResult[]>(url);
  }
}
