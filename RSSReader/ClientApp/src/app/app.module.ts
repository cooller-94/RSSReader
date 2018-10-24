import { BrowserModule } from '@angular/platform-browser';
import { NgModule, APP_INITIALIZER } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { FeedService } from './shared/services/feed.service';
import { AppSettings } from './shared/services/app-config.service';
import { PostService } from './shared/services/post.service';
import { FeedViewComponent } from './feed-view/feed-view.component';
import { TitleOnlyTableComponent } from './shared/components/title-only-table/title-only-table.component';
import { CategoryViewComponent } from './category-view/category-view.component';
import { ProgressBarComponent } from './shared/components/progress/porgress-bar.component';

export function initializeApp(appConfig: AppSettings) {
  return () => appConfig.load();
}

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    FeedViewComponent,
    TitleOnlyTableComponent,
    CategoryViewComponent,
    ProgressBarComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
      { path: '', pathMatch: 'full', redirectTo: 'latest' },
      { path: 'feeds/:feedId', component: FeedViewComponent, pathMatch: 'full' },
      { path: 'categories/:category', component: CategoryViewComponent, pathMatch: 'full' },
      { path: 'latest', component: CategoryViewComponent, pathMatch: 'full' },
    ])
  ],
  providers: [AppSettings, { provide: APP_INITIALIZER, useFactory: initializeApp, deps: [AppSettings], multi: true }, FeedService, PostService],
  bootstrap: [AppComponent]
})
export class AppModule { }
