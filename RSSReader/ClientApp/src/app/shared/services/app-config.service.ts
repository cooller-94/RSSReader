import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { environment } from "../../../environments/environment";
import { AppConfig } from "../models/app-config.model";

@Injectable()
export class AppSettings {
  public settings: AppConfig;

  constructor(private http: HttpClient) { }

  load() {
    const jsonFile = environment.production ? "config.prod.json" : "config.dev.json";

    return new Promise<void>((resolve, reject) => {
      this.http.get<AppConfig>(`assets/config/${jsonFile}`).toPromise().then((data: AppConfig) => {
        console.log(data)
        this.settings = data;
        resolve();
      }).catch((response: any) => {
        reject(`Could not load file '${jsonFile}': ${JSON.stringify(response)}`);
      });
    });
  }
}
