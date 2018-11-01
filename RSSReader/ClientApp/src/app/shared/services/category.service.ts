import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs/Observable';
import { AppSettings } from './app-config.service';
import { Category } from '../models/category.model';


@Injectable()
export class CategoryService {
  constructor(private http: HttpClient, private appConfig: AppSettings) {

  }

  public getAllCategories(): Observable<Category[]> {
    const url = `${this.appConfig.settings.apiUrl}/categories/all`;
    return this.http.get<Category[]>(url);
  }
}
